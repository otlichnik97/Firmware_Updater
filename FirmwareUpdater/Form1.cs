
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirmwareUpdater
{

    public partial class Form1 : Form
    {

        const int PACKET_128 = 128;
        const int PACKET_1024 = 1024;
        const byte YMODEM_SOH = 0x01;
        const byte YMODEM_STX = 0x02;
        const byte YMODEM_EOT = 0x04;
        const byte YMODEM_ACK = 0x06;
        const byte YMODEM_NAK = 0x15;
        const byte YMODEM_CAN = 0x18;
        const byte YMODEM_C_ = 0x43;
        const byte YMODEM_R_ = 0x52;

        enum TState
        {
            WAIT,
            HANDSHAKE,
            TRANSMIT,
            ENDING_1,
            ENDING_2,
            SUCCES,
            ERROR
        }

        TState state = TState.WAIT;

        // Поток для открытия файла прошивки
        FileStream fs;

        byte[] buffer;
        byte[][] packet;
        long packet_quantity = 0;
        long current_packet = 0;
        int try_counter = 0;
        int stages = 10;


        bool file_ready = false;
        static Queue<byte> Received = new Queue<byte>(1024);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pic_disconnect.Visible = false;
            pic_connect.Visible = true;
            pic_firm_off.Visible = true;
            pic_firm_v.Visible = false;
            pic_firm_y.Visible = false;
            pic_firm_g.Visible = false;
            pic_firm_r.Visible = false;
            string[] ports = SerialPort.GetPortNames();
            comboBox_com_ports.Items.AddRange(ports);
            timer1.Start();
        }

        short Ymodem_CRC16(int packet_number)
        {
            short crc = 0x0000;  // Начальное значение CRC

            long length = packet[packet_number].Length - 5;
            int index = 3;

            while (length != 0)
            {
                length--;
                crc ^= (short)((packet[packet_number][index]) << 8);  // XOR текущего байта с верхним байтом CRC
                index++;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc = (short)((crc << 1) ^ 0x1021);  // XOR с полиномом
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }
            return crc;  // Готовый 16-битный CRC
        }

        private void pic_connect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox_com_ports.Text;
                serialPort1.BaudRate = 115200;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                serialPort1.Open();
                pic_disconnect.Visible = true;
                pic_connect.Visible = false;
                timer1.Start();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pic_disconnect.Visible = false;
                pic_connect.Visible = true;
                timer1.Stop();
            }
        }

        private void pic_disconnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                pic_disconnect.Visible = false;
                pic_connect.Visible = true;
                timer1.Stop();
            }
        }

        private void pic_update_Click(object sender, EventArgs e)
        {
            comboBox_com_ports.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBox_com_ports.Items.AddRange(ports);
        }

        void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "BIN-файлы | *.bin"; // Фильтр типов файлов

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    string fileName = Path.GetFileName(filePath);
                    long file_size = fs.Length;
                    label_file_status.Text = "Push RESET for loading";

                    buffer = new byte[fs.Length];
                    label_firmware_size.Text = "Size: " + file_size.ToString() + " bytes";

                    long data_packet_quantity = (file_size + 1024) / PACKET_1024;

                    packet_quantity = data_packet_quantity + 2;

                    stages = (int)(packet_quantity + 3);
                    progressBar1.Step = 1;
                    progressBar1.Maximum = stages;
                    progressBar1.Value = 0;

                    packet = new byte[packet_quantity][];

                    fs.Read(buffer, 0, buffer.Length);

                    fs.Close();

                    // start packet

                    int index = 0;
                    short crc;
                    byte crc_h;
                    byte crc_l;
                    long start = 0;

                    packet[start] = new byte[128 + 5];
                    packet[start][0] = YMODEM_SOH;
                    packet[start][1] = (byte)0;
                    packet[start][2] = (byte)0xFF;

                    byte[] name_bytes = Encoding.ASCII.GetBytes(fileName);
                    Array.Copy(name_bytes, 0, packet[start], 3, name_bytes.Length);
                    index = 3 + name_bytes.Length;
                    packet[0][index++] = (byte)0;
                    byte[] size_bytes = Encoding.ASCII.GetBytes(file_size.ToString());
                    Array.Copy(size_bytes, 0, packet[start], index, size_bytes.Length);
                    index += size_bytes.Length;

                    while (index < PACKET_128 + 5 - 2)
                    {
                        packet[start][index] = (byte)0;
                        index++;
                    }
                    crc = Ymodem_CRC16(0);
                    crc_h = (byte)((crc & 0xFF00) >> 8);
                    crc_l = (byte)(crc & 0x00FF);
                    packet[start][PACKET_128 + 5 - 2] = crc_h;
                    packet[start][PACKET_128 + 5 - 1] = crc_l;

                    // data packets

                    index = 0;
                    int remaining_data = buffer.Length;

                    for (int i = 1; i <= data_packet_quantity; ++i)
                    {
                        byte addr = (byte)i;
                        packet[i] = new byte[PACKET_1024 + 5];
                        packet[i][0] = YMODEM_STX;
                        packet[i][1] = addr;
                        packet[i][2] = (byte)(0xFF - addr);

                        if (remaining_data >= PACKET_1024)
                        {
                            Array.Copy(buffer, index, packet[i], 3, PACKET_1024);
                            index += PACKET_1024;
                            remaining_data -= PACKET_1024;
                        }
                        else // последний пакет
                        {
                            Array.Copy(buffer, index, packet[i], 3, remaining_data);
                            // дополнить пакет
                        }

                        crc = Ymodem_CRC16(i);
                        crc_h = (byte)((crc & 0x0000FF00) >> 8);
                        crc_l = (byte)(crc & 0x000000FF);
                        packet[i][PACKET_1024 + 5 - 2] = crc_h;
                        packet[i][PACKET_1024 + 5 - 1] = crc_l;
                    }

                    // stop packet
                    int stop = (int)(packet_quantity - 1);
                    packet[stop] = new byte[128 + 5];
                    packet[stop][0] = YMODEM_SOH;
                    packet[stop][1] = (byte)0;
                    packet[stop][2] = (byte)0xFF;

                    while (index < PACKET_128 + 5 - 2)
                    {
                        packet[stop][index] = (byte)0;
                        index++;
                    }
                    crc = Ymodem_CRC16(stop);
                    crc_h = (byte)((crc & 0x0000FF00) >> 8);
                    crc_l = (byte)(crc & 0x000000FF);
                    packet[stop][PACKET_128 + 5 - 2] = crc_h;
                    packet[stop][PACKET_128 + 5 - 1] = crc_l;

                    file_ready = true;

                    pic_firm_off.Visible = false;
                    pic_firm_v.Visible = true;
                    pic_firm_y.Visible = false;
                    pic_firm_g.Visible = false;
                    pic_firm_r.Visible = false;
                }
                catch (Exception ex)
                {
                    label_file_status.Text = "Error open. Try again!";
                    label_firmware_size.Text = "Size: ";
                    file_ready = false;

                    pic_firm_off.Visible = true;
                    pic_firm_v.Visible = false;
                    pic_firm_y.Visible = false;
                    pic_firm_g.Visible = false;
                    pic_firm_r.Visible = false;
                }
            }
        }

        private void pic_firm_off_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void pic_firm_v_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void pic_firm_g_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void pic_firm_r_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int buffer_size = serialPort1.BytesToRead;
            byte[] buffer = new byte[buffer_size];
            serialPort1.Read(buffer, 0, buffer_size);
            for (int i = 0; i < buffer.Length; ++i)
            {
                Received.Enqueue(buffer[i]);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] answer = new byte[1];
            if (Received.Count > 0)
            {
                byte data = Received.Dequeue();

                switch (data)
                {
                    case YMODEM_R_:
                        if (state == TState.WAIT) 
                        {
                            if (file_ready == true)
                            {
                                answer[0] = YMODEM_ACK;
                                serialPort1.Write(answer, 0, 1);
                                state = TState.HANDSHAKE;
                                label_file_status.Text = "Waiting handshake...";
                                pic_firm_off.Visible = false;
                                pic_firm_v.Visible = false;
                                pic_firm_y.Visible = true;
                                pic_firm_g.Visible = false;
                                pic_firm_r.Visible = false;
                                progressBar1.Value = 1;
                            }
                            else
                            {
                                answer[0] = YMODEM_CAN;
                                serialPort1.Write(answer, 0, 1);
                                label_file_status.Text = "Cancelled. Need open file!";
                                pic_firm_off.Visible = false;
                                pic_firm_v.Visible = false;
                                pic_firm_y.Visible = false;
                                pic_firm_g.Visible = false;
                                pic_firm_r.Visible = true;
                                progressBar1.Value = 0;
                                timer2.Start();
                            }
                        }
                        break;

                    case YMODEM_C_:
                        if (state == TState.HANDSHAKE)
                        {
                            serialPort1.Write(packet[current_packet], 0, packet[current_packet].Length);
                            label_file_status.Text = "Sending packet N: " + current_packet.ToString();
                            try_counter++;
                            progressBar1.Value = (int)current_packet+2;
                        }
                        else if (state == TState.TRANSMIT)
                        {
                            serialPort1.Write(packet[current_packet], 0, packet[current_packet].Length);
                            label_file_status.Text = "Sending packet N: " + current_packet.ToString();
                            try_counter++;
                            progressBar1.Value = (int)current_packet+2;
                        }
                        break;

                    case YMODEM_ACK:
                        if (state == TState.HANDSHAKE)
                        {
                            current_packet++;
                            try_counter = 0;
                            state = TState.TRANSMIT;
                            progressBar1.Value = 2;
                        }
                        else if (state == TState.TRANSMIT)
                        {
                            current_packet++;
                            try_counter = 0;
                            if (current_packet == packet_quantity - 1)
                            {
                                state = TState.ENDING_1;
                                answer[0] = YMODEM_EOT;
                                serialPort1.Write(answer, 0, 1);
                            }
                            else
                            {
                                serialPort1.Write(packet[current_packet], 0, packet[current_packet].Length);
                                label_file_status.Text = "Sending packet N: " + current_packet.ToString();
                                try_counter++;
                                progressBar1.Value = (int)current_packet+2;
                            }
                        }
                        else if (state == TState.ENDING_1)
                        {
                            state = TState.ERROR;
                            answer[0] = YMODEM_CAN;
                            serialPort1.Write(answer, 0, 1);
                            label_file_status.Text = "Error. Code = 0x01";
                            pic_firm_off.Visible = false;
                            pic_firm_v.Visible = false;
                            pic_firm_y.Visible = false;
                            pic_firm_g.Visible = false;
                            pic_firm_r.Visible = true;
                            progressBar1.Value = 0;
                        }
                        else if (state == TState.ENDING_2)
                        {
                            state = TState.SUCCES;
                            label_file_status.Text = "Firmware uploaded!";
                            progressBar1.Value = stages;
                            pic_firm_off.Visible = false;
                            pic_firm_v.Visible = false;
                            pic_firm_y.Visible = false;
                            pic_firm_g.Visible = true;
                            pic_firm_r.Visible = false;
                            timer2.Start();
                        }
                        break;

                    case YMODEM_NAK:

                        if (state == TState.TRANSMIT || state == TState.HANDSHAKE)
                        {
                            if (try_counter < 4)
                            {
                                serialPort1.Write(packet[current_packet], 0, packet[current_packet].Length);
                                label_file_status.Text = "Sending packet N: " + current_packet.ToString();
                                try_counter++;
                                progressBar1.Value = (int)current_packet+2;
                            } 
                            else
                            {
                                answer[0] = YMODEM_CAN;
                                serialPort1.Write(answer, 0, 1);
                                state = TState.ERROR;
                                label_file_status.Text = "Device can't receive packet N: " + current_packet.ToString();
                                pic_firm_off.Visible = false;
                                pic_firm_v.Visible = false;
                                pic_firm_y.Visible = false;
                                pic_firm_g.Visible = false;
                                pic_firm_r.Visible = true;
                                timer2.Start();
                            }
                        }
                        else if (state == TState.ENDING_1)
                        {
                            answer[0] = YMODEM_EOT;
                            serialPort1.Write(answer, 0, 1);
                            state = TState.ENDING_2;
                        }
                        else if (state == TState.ENDING_2)
                        {
                            state = TState.ERROR;
                            answer[0] = YMODEM_CAN;
                            serialPort1.Write(answer, 0, 1);
                            label_file_status.Text = "Error. Code = 0x02";
                            pic_firm_off.Visible = false;
                            pic_firm_v.Visible = false;
                            pic_firm_y.Visible = false;
                            pic_firm_g.Visible = false;
                            pic_firm_r.Visible = true;
                            progressBar1.Value = 0;
                        }
                        break;

                    default:
                        break;
                }

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            current_packet = 0;
            try_counter = 0;
            state = TState.WAIT;
            progressBar1.Value = 0;
            file_ready = false;
            label_file_status.Text = "No new firmware";
            label_firmware_size.Text = "Size:";
            pic_firm_off.Visible = true;
            pic_firm_v.Visible = false;
            pic_firm_y.Visible = false;
            pic_firm_g.Visible = false;
            pic_firm_r.Visible = false;
        }


    }
}
