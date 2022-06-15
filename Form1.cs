using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace Fractionation
{

    public partial class Fractionation : Form
    {

        
       
        int xpos = 0;
        int ypos = 0;
        char xposplate = '1';
        char yposplate = 'A';
        bool cancel = false;
        
        
        char[] mvmt_data = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' }; //string that stores the commands to be sent to the arduino
        int[] btn_count = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0 };

        int k;
        bool needle_pos = true; //used to determine if the column is lowered or not
        int startpos_index = 0;
        char[] select_x = new char[96]; //stores the selected x position
        char[] select_y = new char[96]; //stores the selected y position
        int[] plateclicked = new int[96];
        int[] wellspertest = new int[96]; //number of wells per sequence
        int[] num_passes = new int[96]; //number of passes
        int sequence_num = 0; //number of sequences
        int select_num = 0; //number of well selections
        int well_num;
        int[] well_time = new int[96]; //array to store ammount of time spent in each well during a custom path
        int temp_test = 1;
        int select_num_index = 0;
        char[] xposplate2 = new char[12] { '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<' }; //x position characters used to communicate with the arduino
        char[] yposplate2 = new char[8] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};                       //y position characters used to communicate with the arduino
        int timeleft = 0;
        List<PictureBox> wellImageList = new List<PictureBox>(); //populates a list referencing to all the well buttons


        public Fractionation()
        {
            InitializeComponent();
            getAvailablePorts();
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            tabControl1.TabPages.Remove(tab_Callibration);
            // Store picture boxes into an array (pictureboxes represent the wells on the plate)
            for (int i = 0; i < 95; i++)
            {
                wellImageList.Add(btn_A1);
                wellImageList.Add(btn_A2);
                wellImageList.Add(btn_A3);
                wellImageList.Add(btn_A4);
                wellImageList.Add(btn_A5);
                wellImageList.Add(btn_A6);
                wellImageList.Add(btn_A7);
                wellImageList.Add(btn_A8);
                wellImageList.Add(btn_A9);
                wellImageList.Add(btn_A10);
                wellImageList.Add(btn_A11);
                wellImageList.Add(btn_A12);
                wellImageList.Add(btn_B1);
                wellImageList.Add(btn_B2);
                wellImageList.Add(btn_B3);
                wellImageList.Add(btn_B4);
                wellImageList.Add(btn_B5);
                wellImageList.Add(btn_B6);
                wellImageList.Add(btn_B7);
                wellImageList.Add(btn_B8);
                wellImageList.Add(btn_B9);
                wellImageList.Add(btn_B10);
                wellImageList.Add(btn_B11);
                wellImageList.Add(btn_B12);
                wellImageList.Add(btn_C1);
                wellImageList.Add(btn_C2);
                wellImageList.Add(btn_C3);
                wellImageList.Add(btn_C4);
                wellImageList.Add(btn_C5);
                wellImageList.Add(btn_C6);
                wellImageList.Add(btn_C7);
                wellImageList.Add(btn_C8);
                wellImageList.Add(btn_C9);
                wellImageList.Add(btn_C10);
                wellImageList.Add(btn_C11);
                wellImageList.Add(btn_C12);
                wellImageList.Add(btn_D1);
                wellImageList.Add(btn_D2);
                wellImageList.Add(btn_D3);
                wellImageList.Add(btn_D4);
                wellImageList.Add(btn_D5);
                wellImageList.Add(btn_D6);
                wellImageList.Add(btn_D7);
                wellImageList.Add(btn_D8);
                wellImageList.Add(btn_D9);
                wellImageList.Add(btn_D10);
                wellImageList.Add(btn_D11);
                wellImageList.Add(btn_D12);
                wellImageList.Add(btn_E1);
                wellImageList.Add(btn_E2);
                wellImageList.Add(btn_E3);
                wellImageList.Add(btn_E4);
                wellImageList.Add(btn_E5);
                wellImageList.Add(btn_E6);
                wellImageList.Add(btn_E7);
                wellImageList.Add(btn_E8);
                wellImageList.Add(btn_E9);
                wellImageList.Add(btn_E10);
                wellImageList.Add(btn_E11);
                wellImageList.Add(btn_E12);
                wellImageList.Add(btn_F1);
                wellImageList.Add(btn_F2);
                wellImageList.Add(btn_F3);
                wellImageList.Add(btn_F4);
                wellImageList.Add(btn_F5);
                wellImageList.Add(btn_F6);
                wellImageList.Add(btn_F7);
                wellImageList.Add(btn_F8);
                wellImageList.Add(btn_F9);
                wellImageList.Add(btn_F10);
                wellImageList.Add(btn_F11);
                wellImageList.Add(btn_F12);
                wellImageList.Add(btn_G1);
                wellImageList.Add(btn_G2);
                wellImageList.Add(btn_G3);
                wellImageList.Add(btn_G4);
                wellImageList.Add(btn_G5);
                wellImageList.Add(btn_G6);
                wellImageList.Add(btn_G7);
                wellImageList.Add(btn_G8);
                wellImageList.Add(btn_G9);
                wellImageList.Add(btn_G10);
                wellImageList.Add(btn_G11);
                wellImageList.Add(btn_G12);
                wellImageList.Add(btn_H1);
                wellImageList.Add(btn_H2);
                wellImageList.Add(btn_H3);
                wellImageList.Add(btn_H4);
                wellImageList.Add(btn_H5);
                wellImageList.Add(btn_H6);
                wellImageList.Add(btn_H7);
                wellImageList.Add(btn_H8);
                wellImageList.Add(btn_H9);
                wellImageList.Add(btn_H10);
                wellImageList.Add(btn_H11);
                wellImageList.Add(btn_H12);

                //wellImageList[0].Image = 

            }
        }

            void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            ComboBox_AvailableSerialPorts.Text = "Available Serial Ports";
            ComboBox_AvailableSerialPorts.Items.AddRange(ports);


            SerialPort1.ReadTimeout = 2000;

            Button_Connect.Visible = true;
            Button_Disconnect.Visible = false;
            tabControl1.TabPages.Remove(tab_Keypad);
            tabControl1.TabPages.Remove(tab_Plate);
            tabControl1.TabPages.Remove(tab_Selection);

        }

            private void btn_refresh_Click(object sender, EventArgs e)
        {
            ComboBox_AvailableSerialPorts.Items.Clear();
            getAvailablePorts();
        }

        private void Button_Connect_Click(object sender, EventArgs e)
        {
            SerialPort1.BaudRate = 9600; //sets the baud rate for the serial port
            SerialPort1.PortName = ComboBox_AvailableSerialPorts.SelectedItem.ToString(); //Selects the chosen serial port

            Button_Connect.Visible = false;
            btn_refresh.Visible = false;
            Button_Disconnect.Visible = true;



            if (SerialPort1.IsOpen == false) //Opens the serial port if it is closed
            {
                SerialPort1.Open();
            }

            
            reset_plate();
            reset();
            tabControl1.TabPages.Add(tab_Selection);
            tabControl1.TabPages.Add(tab_Plate);
            tabControl1.TabPages.Add(tab_Keypad);
            tabControl1.TabPages.Add(tab_Callibration);
            
            
        }
        private void Button_Disconnect_Click(object sender, EventArgs e)
        {
            reset();
            if (SerialPort1.IsOpen == true)
            {
                SerialPort1.Close();
            }

            Button_Connect.Visible = true;
            Button_Disconnect.Visible = false;
            btn_refresh.Visible = true;
            tabControl1.TabPages.Remove(tab_Keypad);
            tabControl1.TabPages.Remove(tab_Plate);
            tabControl1.TabPages.Remove(tab_Selection);
            tabControl1.TabPages.Remove(tab_Callibration);


        }

        //keypad functions
        private void xright_Click(object sender, EventArgs e)
        {
            if (xpos == 11)
            {
                return;
            }
            if (startpos_index == 0)
            {
                startpos();
                startpos_index++;
            }
            xposplate++;
            move(yposplate, xposplate);
        }
        private void xleft_Click(object sender, EventArgs e)
        {
            if (xpos == 0)
            {
                return;
            }
            if (startpos_index == 0)
            {
                startpos();
                startpos_index++;
            }
            xposplate--;
            move(yposplate, xposplate);
        }
        private void yup_Click(object sender, EventArgs e)
        {
            if (ypos == 0)
            {
                return;
            }
            if (startpos_index == 0)
            {
                startpos();
                startpos_index++;
            }
            yposplate--;
            move(yposplate, xposplate);
        }
        private void ydown_Click(object sender, EventArgs e)
        {
            if (ypos == 7)
            {
                return;
            }
            if (startpos_index == 0)
            {
                startpos();
                startpos_index++;
            }
            yposplate++;
            move(yposplate, xposplate);
        }


        private void Button_ok_Click(object sender, EventArgs e) //moves to the selected well on the plate
        {
            if (startpos_index == 0)
            {
                startpos();
                startpos_index++;
            }
            move(yposplate, xposplate);
        } 
        void move(char yposplate1, char xposplate1)
        {
            check_needle_pos();

            int ypos1 = yposplate1 - 'A';
            int xpos1 = xposplate1 - '1';
            int temp1 = xpos;
            int temp2 = ypos;
            char xdist = (char)(Math.Abs(temp1 - xpos1)+65); //measure distance between current x coordinate and the desired x coordinate
            char ydist = (char)(Math.Abs(temp2 - ypos1)+65); //measure distance between current y coordinate and the desired y coordinate

            mvmt_data[0] = 'A'; //indicates the start of the message packet
            mvmt_data[1] = 'M'; //indicates movement
            direction(xpos1, ypos1);
            mvmt_data[4] = xdist;
            mvmt_data[5] = ydist;
            mvmt_data[7] = '*'; //indicates the end of the message packet

            string s = new string(mvmt_data);


            SerialPort1.Write(s); //sends the message packet to the arduino

            xpos = xpos1;
            ypos = ypos1;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {

            reset_plate();
            reset();
        } 

        private void direction(int xdir, int ydir) //determines the direction of the movement based on the currrent position and the desired position
        {
            if ((ypos - ydir) < 0)
            {
                mvmt_data[3] = 'v';
            }
            else
            {
                mvmt_data[3] = '^';
            }
            if ((xpos - xdir) < 0)
            {
                mvmt_data[2] = '>';
            }
            else
            {
                mvmt_data[2] = '<';
            }
        }

        void reset()
        {
            check_needle_pos();
            startpos_index = 0;
            mvmt_data[0] = 'A';
            mvmt_data[1] = 'H';
            mvmt_data[7] = '*';

            string s = new string(mvmt_data);
            SerialPort1.Write(s);

            xpos = -1;
            ypos = -1;
            xposplate = '1';
            yposplate = 'A';
            select_num_index = 0;
        } //returns the column to the waste tray

        //Buttons for plate schematic
        private void btn_A1_Click(object sender, EventArgs e)
        {
            if (btn_count[0] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 0;
                select_num_index++;
            }
            else
            {
                btn_A1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            
            btn_count[0]++;
          
        }
        private void btn_A2_Click(object sender, EventArgs e)
        {
            if (btn_count[1] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 1;
                select_num_index++;
            }
            else
            {
                btn_A2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;

            }
            btn_count[1]++;
        }
        private void btn_A3_Click(object sender, EventArgs e)
        {
            if (btn_count[2] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 2;
                select_num_index++;
            }
            else
            {
                btn_A3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[2]++;
        }
        private void btn_A4_Click(object sender, EventArgs e)
        {
            if (btn_count[3] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 3;
                select_num_index++;
            }
            else
            {
                btn_A4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[3]++;
        }
        private void btn_A5_Click(object sender, EventArgs e)
        {
            if (btn_count[4] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 4;
                select_num_index++;
            }
            else
            {
                btn_A5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[4]++;
        }
        private void btn_A6_Click(object sender, EventArgs e)
        {
            if (btn_count[5] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 5;
                select_num_index++;
            }
            else
            {
                btn_A6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[5]++;
        }
        private void btn_A7_Click(object sender, EventArgs e)
        {
            if (btn_count[6] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 6;
                select_num_index++;
            }
            else
            {
                btn_A7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[6]++;
        }
        private void btn_A8_Click(object sender, EventArgs e)
        {
            if (btn_count[7] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 7;
                select_num_index++;
            }
            else
            {
                btn_A8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[7]++;
        }
        private void btn_A9_Click(object sender, EventArgs e)
        {
            if (btn_count[8] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 8;
                select_num_index++;
            }
            else
            {
                btn_A9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[8]++;
        }
        private void btn_A10_Click(object sender, EventArgs e)
        {
            if (btn_count[9] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 9;
                select_num_index++;
            }
            else
            {
                btn_A10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[9]++;
        }
        private void btn_A11_Click(object sender, EventArgs e)
        {
            if (btn_count[10] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 10;
                select_num_index++;
            }
            else
            {
                btn_A11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[10]++;
        }
        private void btn_A12_Click(object sender, EventArgs e)
        {
            if (btn_count[11] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_A12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[0];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 11;
                select_num_index++;
            }
            else
            {
                btn_A12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[11]++;
        }
        private void btn_B1_Click(object sender, EventArgs e)
        {
            if (btn_count[12] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 12;
                select_num_index++;
            }
            else
            {
                btn_B1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[12]++;
        }
        private void btn_B2_Click(object sender, EventArgs e)
        {
            if (btn_count[13] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 13;
                select_num_index++;
            }
            else
            {
                btn_B2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[13]++;
        }
        private void btn_B3_Click(object sender, EventArgs e)
        {
            if (btn_count[14] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 14;
                select_num_index++;
            }
            else
            {
                btn_B3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[14]++;
        }
        private void btn_B4_Click(object sender, EventArgs e)
        {
            if (btn_count[15] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 15;
                select_num_index++;
            }
            else
            {
                btn_B4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[15]++;
        }
        private void btn_B5_Click(object sender, EventArgs e)
        {
            if (btn_count[16] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 16;
                select_num_index++;
            }
            else
            {
                btn_B5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[16]++;
        }
        private void btn_B6_Click(object sender, EventArgs e)
        {
            if (btn_count[17] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 17;
                select_num_index++;
            }
            else
            {
                btn_B6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[17]++;
        }
        private void btn_B7_Click(object sender, EventArgs e)
        {
            if (btn_count[18] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 18;
                select_num_index++;
            }
            else
            {
                btn_B7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[18]++;
        }
        private void btn_B8_Click(object sender, EventArgs e)
        {
            if (btn_count[19] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 19;
                select_num_index++;
            }
            else
            {
                btn_B8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[19]++;
        }
        private void btn_B9_Click(object sender, EventArgs e)
        {
            if (btn_count[20] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 20;
                select_num_index++;
            }
            else
            {
                btn_B9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[20]++;
        }
        private void btn_B10_Click(object sender, EventArgs e)
        {
            if (btn_count[21] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 21;
                select_num_index++;
            }
            else
            {
                btn_B10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[21]++;
        }
        private void btn_B11_Click(object sender, EventArgs e)
        {
            if (btn_count[22] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 22;
                select_num_index++;
            }
            else
            {
                btn_B11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[22]++;
        }
        private void btn_B12_Click(object sender, EventArgs e)
        {
            if (btn_count[23] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_B12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[1];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 23;
                select_num_index++;
            }
            else
            {
                btn_B12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[23]++;
        }
        private void btn_C1_Click(object sender, EventArgs e)
        {
            if (btn_count[24] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 24;
                select_num_index++;
            }
            else
            {
                btn_C1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[24]++;
        }
        private void btn_C2_Click(object sender, EventArgs e)
        {
            if (btn_count[25] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 25;
                select_num_index++;
            }
            else
            {
                btn_C2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[25]++;
        }
        private void btn_C3_Click(object sender, EventArgs e)
        {
            if (btn_count[26] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 26;
                select_num_index++;
            }
            else
            {
                btn_C3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[26]++;
        }
        private void btn_C4_Click(object sender, EventArgs e)
        {
            if (btn_count[27] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 27;
                select_num_index++;
            }
            else
            {
                btn_C4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[27]++;
        }
        private void btn_C5_Click(object sender, EventArgs e)
        {
            if (btn_count[28] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 28;
                select_num_index++;
            }
            else
            {
                btn_C5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[28]++;
        }
        private void btn_C6_Click(object sender, EventArgs e)
        {
            if (btn_count[29] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 29;
                select_num_index++;
            }
            else
            {
                btn_C6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[29]++;
        }
        private void btn_C7_Click(object sender, EventArgs e)
        {
            if (btn_count[30] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 30;
                select_num_index++;
            }
            else
            {
                btn_C7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[30]++;
        }
        private void btn_C8_Click(object sender, EventArgs e)
        {
            if (btn_count[31] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 31;
                select_num_index++;
            }
            else
            {
                btn_C8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[31]++;
        }
        private void btn_C9_Click(object sender, EventArgs e)
        {
            if (btn_count[32] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 32;
                select_num_index++;
            }
            else
            {
                btn_C9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[32]++;
        }
        private void btn_C10_Click(object sender, EventArgs e)
        {
            if (btn_count[33] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 33;
                select_num_index++;
            }
            else
            {
                btn_C10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[33]++;
        }
        private void btn_C11_Click(object sender, EventArgs e)
        {
            if (btn_count[34] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 34;
                select_num_index++;
            }
            else
            {
                btn_C11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[34]++;
        }
        private void btn_C12_Click(object sender, EventArgs e)
        {
            if (btn_count[35] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_C12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[2];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 35;
                select_num_index++;
            }
            else
            {
                btn_C12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[35]++;
        }
        private void btn_D1_Click(object sender, EventArgs e)
        {
            if (btn_count[36] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 36;
                select_num_index++;
            }
            else
            {
                btn_D1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[36]++;
        }
        private void btn_D2_Click(object sender, EventArgs e)
        {
            if (btn_count[37] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 37;
                select_num_index++;
            }
            else
            {
                btn_D2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[37]++;
        }
        private void btn_D3_Click(object sender, EventArgs e)
        {
            if (btn_count[38] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 38;
                select_num_index++;
            }
            else
            {
                btn_D3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[38]++;
        }
        private void btn_D4_Click(object sender, EventArgs e)
        {
            if (btn_count[39] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 39;
                select_num_index++;
            }
            else
            {
                btn_D4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[39]++;
        }
        private void btn_D5_Click(object sender, EventArgs e)
        {
            if (btn_count[40] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 40;
                select_num_index++;
            }
            else
            {
                btn_D5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[40]++;
        }
        private void btn_D6_Click(object sender, EventArgs e)
        {
            if (btn_count[41] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 41;
                select_num_index++;
            }
            else
            {
                btn_D6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[41]++;
        }
        private void btn_D7_Click(object sender, EventArgs e)
        {
            if (btn_count[42] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 42;
                select_num_index++;
            }
            else
            {
                btn_D7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[42]++;
        }
        private void btn_D8_Click(object sender, EventArgs e)
        {
            if (btn_count[43] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 43;
                select_num_index++;
            }
            else
            {
                btn_D8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[43]++;
        }
        private void btn_D9_Click(object sender, EventArgs e)
        {
            if (btn_count[44] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 44;
                select_num_index++;
            }
            else
            {
                btn_D9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[44]++;
        }
        private void btn_D10_Click(object sender, EventArgs e)
        {
            if (btn_count[45] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 45;
                select_num_index++;
            }
            else
            {
                btn_D10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[45]++;
        }
        private void btn_D11_Click(object sender, EventArgs e)
        {
            if (btn_count[46] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 46;
                select_num_index++;
            }
            else
            {
                btn_D11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[46]++;
        }
        private void btn_D12_Click(object sender, EventArgs e)
        {
            if (btn_count[47] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_D12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[3];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 47;
                select_num_index++;
            }
            else
            {
                btn_D12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[47]++;
        }
        private void btn_E1_Click(object sender, EventArgs e)
        {
            if (btn_count[48] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 48;
                select_num_index++;
            }
            else
            {
                btn_E1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[48]++;
        }
        private void btn_E2_Click(object sender, EventArgs e)
        {
            if (btn_count[49] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 49;
                select_num_index++;
            }
            else
            {
                btn_E2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[49]++;
        }
        private void btn_E3_Click(object sender, EventArgs e)
        {
            if (btn_count[50] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 50;
                select_num_index++;
            }
            else
            {
                btn_E3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[50]++;
        }
        private void btn_E4_Click(object sender, EventArgs e)
        {
            if (btn_count[51] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 51;
                select_num_index++;
            }
            else
            {
                btn_E4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[51]++;
        }
        private void btn_E5_Click(object sender, EventArgs e)
        {
            if (btn_count[52] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 52;
                select_num_index++;
            }
            else
            {
                btn_E5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[52]++;
        }
        private void btn_E6_Click(object sender, EventArgs e)
        {
            if (btn_count[53] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 53;
                select_num_index++;
            }
            else
            {
                btn_E6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[53]++;
        }
        private void btn_E7_Click(object sender, EventArgs e)
        {
            if (btn_count[54] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 54;
                select_num_index++;
            }
            else
            {
                btn_E7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[54]++;
        }
        private void btn_E8_Click(object sender, EventArgs e)
        {
            if (btn_count[55] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 55;
                select_num_index++;
            }
            else
            {
                btn_E8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[55]++;
        }
        private void btn_E9_Click(object sender, EventArgs e)
        {
            if (btn_count[56] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 56;
                select_num_index++;
            }
            else
            {
                btn_E9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[56]++;
        }
        private void btn_E10_Click(object sender, EventArgs e)
        {
            if (btn_count[57] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 57;
                select_num_index++;
            }
            else
            {
                btn_E10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[57]++;
        }
        private void btn_E11_Click(object sender, EventArgs e)
        {
            if (btn_count[58] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 58;
                select_num_index++;
            }
            else
            {
                btn_E11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[58]++;
        }
        private void btn_E12_Click(object sender, EventArgs e)
        {
            if (btn_count[59] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_E12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[4];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 59;
                select_num_index++;
            }
            else
            {
                btn_E12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[59]++;
        }
        private void btn_F1_Click(object sender, EventArgs e)
        {
            if (btn_count[60] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 60;
                select_num_index++;
            }
            else
            {
                btn_F1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[60]++;
        }
        private void btn_F2_Click(object sender, EventArgs e)
        {
            if (btn_count[61] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 61;
                select_num_index++;
            }
            else
            {
                btn_F2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[61]++;
        }
        private void btn_F3_Click(object sender, EventArgs e)
        {
            if (btn_count[62] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 62;
                select_num_index++;
            }
            else
            {
                btn_F3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[62]++;
        }
        private void btn_F4_Click(object sender, EventArgs e)
        {
            if (btn_count[63] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 63;
                select_num_index++;
            }
            else
            {
                btn_F4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[63]++;
        }
        private void btn_F5_Click(object sender, EventArgs e)
        {
            if (btn_count[64] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 64;
                select_num_index++;
            }
            else
            {
                btn_F5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[64]++;
        }
        private void btn_F6_Click(object sender, EventArgs e)
        {
            if (btn_count[65] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 65;
                select_num_index++;
            }
            else
            {
                btn_F6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[65]++;
        }
        private void btn_F7_Click(object sender, EventArgs e)
        {
            if (btn_count[66] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 66;
                select_num_index++;
            }
            else
            {
                btn_F7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[66]++;
        }
        private void btn_F8_Click(object sender, EventArgs e)
        {
            if (btn_count[67] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 67;
                select_num_index++;
            }
            else
            {
                btn_F8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[67]++;
        }
        private void btn_F9_Click(object sender, EventArgs e)
        {
            if (btn_count[68] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 68;
                select_num_index++;
            }
            else
            {
                btn_F9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[68]++;
        }
        private void btn_F10_Click(object sender, EventArgs e)
        {
            if (btn_count[69] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 69;
                select_num_index++;
            }
            else
            {
                btn_F10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[69]++;
        }
        private void btn_F11_Click(object sender, EventArgs e)
        {
            if (btn_count[70] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 70;
                select_num_index++;
            }
            else
            {
                btn_F11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[70]++;
        }
        private void btn_F12_Click(object sender, EventArgs e)
        {
            if (btn_count[71] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_F12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[5];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 71;
                select_num_index++;
            }
            else
            {
                btn_F12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[71]++;
        }
        private void btn_G1_Click(object sender, EventArgs e)
        {
            if (btn_count[72] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 72;
                select_num_index++;
            }
            else
            {
                btn_G1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[72]++;
        }
        private void btn_G2_Click(object sender, EventArgs e)
        {
            if (btn_count[73] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 73;
                select_num_index++;
            }
            else
            {
                btn_G2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[73]++;
        }
        private void btn_G3_Click(object sender, EventArgs e)
        {
            if (btn_count[74] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 74;
                select_num_index++;
            }
            else
            {
                btn_G3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[74]++;
        }
        private void btn_G4_Click(object sender, EventArgs e)
        {
            if (btn_count[75] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 75;
                select_num_index++;
            }
            else
            {
                btn_G4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[75]++;
        }
        private void btn_G5_Click(object sender, EventArgs e)
        {
            if (btn_count[76] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 76;
                select_num_index++;
            }
            else
            {
                btn_G5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[76]++;
        }
        private void btn_G6_Click(object sender, EventArgs e)
        {
            if (btn_count[77] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 77;
                select_num_index++;
            }
            else
            {
                btn_G6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[77]++;
        }
        private void btn_G7_Click(object sender, EventArgs e)
        {
            if (btn_count[78] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 78;
                select_num_index++;
            }
            else
            {
                btn_G7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[78]++;
        }
        private void btn_G8_Click(object sender, EventArgs e)
        {
            if (btn_count[79] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 79;
                select_num_index++;
            }
            else
            {
                btn_G8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[79]++;
        }
        private void btn_G9_Click(object sender, EventArgs e)
        {
            if (btn_count[80] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 80;
                select_num_index++;
            }
            else
            {
                btn_G9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[80]++;
        }
        private void btn_G10_Click(object sender, EventArgs e)
        {
            if (btn_count[81] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 81;
                select_num_index++;
            }
            else
            {
                btn_G10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[81]++;
        }
        private void btn_G11_Click(object sender, EventArgs e)
        {
            if (btn_count[82] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 82;
                select_num_index++;
            }
            else
            {
                btn_G11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[82]++;
        }
        private void btn_G12_Click(object sender, EventArgs e)
        {
            if (btn_count[83] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_G12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[6];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 83;
                select_num_index++;
            }
            else
            {
                btn_G12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[83]++;
        }
        private void btn_H1_Click(object sender, EventArgs e)
        {
            if (btn_count[84] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H1.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[0];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 84;
                select_num_index++;
            }
            else
            {
                btn_H1.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[84]++;
        }
        private void btn_H2_Click(object sender, EventArgs e)
        {
            if (btn_count[85] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H2.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[1];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 85;
                select_num_index++;
            }
            else
            {
                btn_H2.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[85]++;
        }
        private void btn_H3_Click(object sender, EventArgs e)
        {
            if (btn_count[86] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H3.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[2];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 86;
                select_num_index++;
            }
            else
            {
                btn_H3.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[86]++;
        }
        private void btn_H4_Click(object sender, EventArgs e)
        {
            if (btn_count[87] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H4.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[3];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 87;
                select_num_index++;
            }
            else
            {
                btn_H4.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[87]++;
        }
        private void btn_H5_Click(object sender, EventArgs e)
        {
            if (btn_count[88] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H5.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[4];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 88;
                select_num_index++;
            }
            else
            {
                btn_H5.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[88]++;
        }
        private void btn_H6_Click(object sender, EventArgs e)
        {
            if (btn_count[89] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H6.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[5];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 89;
                select_num_index++;
            }
            else
            {
                btn_H6.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[89]++;
        }
        private void btn_H7_Click(object sender, EventArgs e)
        {
            if (btn_count[90] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H7.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[6];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 90;
                select_num_index++;
            }
            else
            {
                btn_H7.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[90]++;
        }
        private void btn_H8_Click(object sender, EventArgs e)
        {
            if (btn_count[91] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H8.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[7];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 91;
                select_num_index++;
            }
            else
            {
                btn_H8.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[91]++;
        }
        private void btn_H9_Click(object sender, EventArgs e)
        {
            if (btn_count[92] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H9.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[8];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 92;
                select_num_index++;
            }
            else
            {
                btn_H9.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[92]++;
        }
        private void btn_H10_Click(object sender, EventArgs e)
        {
            if (btn_count[93] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H10.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[9];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 93;
                select_num_index++;
            }
            else
            {
                btn_H10.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[93]++;
        }
        private void btn_H11_Click(object sender, EventArgs e)
        {
            if (btn_count[94] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H11.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[10];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 94;
                select_num_index++;
            }
            else
            {
                btn_H11.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[94]++;
        }
        private void btn_H12_Click(object sender, EventArgs e)
        {
            if (btn_count[95] % 2 == 0)
            {
                if (tabControl1.SelectedTab == tab_Plate)
                {
                    reset_plate();
                }
                btn_H12.Image = global::Fractionation.Properties.Resources.circleclicked;
                xposplate = xposplate2[11];
                yposplate = yposplate2[7];
                select_x[select_num_index] = xposplate;
                select_y[select_num_index] = yposplate;
                plateclicked[select_num_index] = 95;
                select_num_index++;
            }
            else
            {
                btn_H12.Image = global::Fractionation.Properties.Resources.circle12;
                select_num_index--;
            }
            btn_count[95]++;
        }

        void reset_plate()
        {
            for(int i = 0; i < 118; i++)
            {
                btn_count[i] = 1;
            }

            btn_A1_Click(this.btn_A1, null);
            btn_A2_Click(this.btn_A2, null);
            btn_A3_Click(this.btn_A3, null);
            btn_A4_Click(this.btn_A4, null);
            btn_A5_Click(this.btn_A5, null);
            btn_A6_Click(this.btn_A6, null);
            btn_A7_Click(this.btn_A7, null);
            btn_A8_Click(this.btn_A8, null);
            btn_A9_Click(this.btn_A9, null);
            btn_A10_Click(this.btn_A10, null);
            btn_A11_Click(this.btn_A11, null);
            btn_A12_Click(this.btn_A12, null);
            btn_B1_Click(this.btn_B1, null);
            btn_B2_Click(this.btn_B2, null);
            btn_B3_Click(this.btn_B3, null);
            btn_B4_Click(this.btn_B4, null);
            btn_B5_Click(this.btn_B5, null);
            btn_B6_Click(this.btn_B6, null);
            btn_B7_Click(this.btn_B7, null);
            btn_B8_Click(this.btn_B8, null);
            btn_B9_Click(this.btn_B9, null);
            btn_B10_Click(this.btn_B10, null);
            btn_B11_Click(this.btn_B11, null);
            btn_B12_Click(this.btn_B12, null);
            btn_C1_Click(this.btn_C1, null);
            btn_C2_Click(this.btn_C2, null);
            btn_C3_Click(this.btn_C3, null);
            btn_C4_Click(this.btn_C4, null);
            btn_C5_Click(this.btn_C5, null);
            btn_C6_Click(this.btn_C6, null);
            btn_C7_Click(this.btn_C7, null);
            btn_C8_Click(this.btn_C8, null);
            btn_C9_Click(this.btn_C9, null);
            btn_C10_Click(this.btn_C10, null);
            btn_C11_Click(this.btn_C11, null);
            btn_C12_Click(this.btn_C12, null);
            btn_D1_Click(this.btn_D1, null);
            btn_D2_Click(this.btn_D2, null);
            btn_D3_Click(this.btn_D3, null);
            btn_D4_Click(this.btn_D4, null);
            btn_D5_Click(this.btn_D5, null);
            btn_D6_Click(this.btn_D6, null);
            btn_D7_Click(this.btn_D7, null);
            btn_D8_Click(this.btn_D8, null);
            btn_D9_Click(this.btn_D9, null);
            btn_D10_Click(this.btn_D10, null);
            btn_D11_Click(this.btn_D11, null);
            btn_D12_Click(this.btn_D12, null);
            btn_E1_Click(this.btn_E1, null);
            btn_E2_Click(this.btn_E2, null);
            btn_E3_Click(this.btn_E3, null);
            btn_E4_Click(this.btn_E4, null);
            btn_E5_Click(this.btn_E5, null);
            btn_E6_Click(this.btn_E6, null);
            btn_E7_Click(this.btn_E7, null);
            btn_E8_Click(this.btn_E8, null);
            btn_E9_Click(this.btn_E9, null);
            btn_E10_Click(this.btn_E10, null);
            btn_E11_Click(this.btn_E11, null);
            btn_E12_Click(this.btn_E12, null);
            btn_F1_Click(this.btn_F1, null);
            btn_F2_Click(this.btn_F2, null);
            btn_F3_Click(this.btn_F3, null);
            btn_F4_Click(this.btn_F4, null);
            btn_F5_Click(this.btn_F5, null);
            btn_F6_Click(this.btn_F6, null);
            btn_F7_Click(this.btn_F7, null);
            btn_F8_Click(this.btn_F8, null);
            btn_F9_Click(this.btn_F9, null);
            btn_F10_Click(this.btn_F10, null);
            btn_F11_Click(this.btn_F11, null);
            btn_F12_Click(this.btn_F12, null);
            btn_G1_Click(this.btn_G1, null);
            btn_G2_Click(this.btn_G2, null);
            btn_G3_Click(this.btn_G3, null);
            btn_G4_Click(this.btn_G4, null);
            btn_G5_Click(this.btn_G5, null);
            btn_G6_Click(this.btn_G6, null);
            btn_G7_Click(this.btn_G7, null);
            btn_G8_Click(this.btn_G8, null);
            btn_G9_Click(this.btn_G9, null);
            btn_G10_Click(this.btn_G10, null);
            btn_G11_Click(this.btn_G11, null);
            btn_G12_Click(this.btn_G12, null);
            btn_H1_Click(this.btn_H1, null);
            btn_H2_Click(this.btn_H2, null);
            btn_H3_Click(this.btn_H3, null);
            btn_H4_Click(this.btn_H4, null);
            btn_H5_Click(this.btn_H5, null);
            btn_H6_Click(this.btn_H6, null);
            btn_H7_Click(this.btn_H7, null);
            btn_H8_Click(this.btn_H8, null);
            btn_H9_Click(this.btn_H9, null);
            btn_H10_Click(this.btn_H10, null);
            btn_H11_Click(this.btn_H11, null);
            btn_H12_Click(this.btn_H12, null);

            select_num_index = 0;

        } //unclicks all the wells on the plate diagram

        //enables user to select all the wells in a specific row instead of clicking 12 times
        private void lbl_A_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 12; i++)
            {
                if (btn_count[96] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_A1_Click(this.btn_A1, null);
            btn_A2_Click(this.btn_A2, null);
            btn_A3_Click(this.btn_A3, null);
            btn_A4_Click(this.btn_A4, null);
            btn_A5_Click(this.btn_A5, null);
            btn_A6_Click(this.btn_A6, null);
            btn_A7_Click(this.btn_A7, null);
            btn_A8_Click(this.btn_A8, null);
            btn_A9_Click(this.btn_A9, null);
            btn_A10_Click(this.btn_A10, null);
            btn_A11_Click(this.btn_A11, null);
            btn_A12_Click(this.btn_A12, null);

            btn_count[96]++;
        } 
        private void lbl_B_Click(object sender, EventArgs e)
        {
            for (int i = 12; i < 24; i++)
            {
                if (btn_count[97] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_B1_Click(this.btn_B1, null);
            btn_B2_Click(this.btn_B2, null);
            btn_B3_Click(this.btn_B3, null);
            btn_B4_Click(this.btn_B4, null);
            btn_B5_Click(this.btn_B5, null);
            btn_B6_Click(this.btn_B6, null);
            btn_B7_Click(this.btn_B7, null);
            btn_B8_Click(this.btn_B8, null);
            btn_B9_Click(this.btn_B9, null);
            btn_B10_Click(this.btn_B10, null);
            btn_B11_Click(this.btn_B11, null);
            btn_B12_Click(this.btn_B12, null);

            btn_count[97]++;
        }
        private void lbl_C_Click(object sender, EventArgs e)
        {
            for (int i = 24; i < 36; i++)
            {
                if (btn_count[98] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_C1_Click(this.btn_C1, null);
            btn_C2_Click(this.btn_C2, null);
            btn_C3_Click(this.btn_C3, null);
            btn_C4_Click(this.btn_C4, null);
            btn_C5_Click(this.btn_C5, null);
            btn_C6_Click(this.btn_C6, null);
            btn_C7_Click(this.btn_C7, null);
            btn_C8_Click(this.btn_C8, null);
            btn_C9_Click(this.btn_C9, null);
            btn_C10_Click(this.btn_C10, null);
            btn_C11_Click(this.btn_C11, null);
            btn_C12_Click(this.btn_C12, null);

            btn_count[98]++;
        }
        private void lbl_D_Click(object sender, EventArgs e)
        {
            for (int i = 36; i < 48; i++)
            {
                if (btn_count[99] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_D1_Click(this.btn_D1, null);
            btn_D2_Click(this.btn_D2, null);
            btn_D3_Click(this.btn_D3, null);
            btn_D4_Click(this.btn_D4, null);
            btn_D5_Click(this.btn_D5, null);
            btn_D6_Click(this.btn_D6, null);
            btn_D7_Click(this.btn_D7, null);
            btn_D8_Click(this.btn_D8, null);
            btn_D9_Click(this.btn_D9, null);
            btn_D10_Click(this.btn_D10, null);
            btn_D11_Click(this.btn_D11, null);
            btn_D12_Click(this.btn_D12, null);

            btn_count[99]++;
        }
        private void lbl_E_Click(object sender, EventArgs e)
        {
            for (int i = 48; i < 60; i++)
            {
                if (btn_count[100] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_E1_Click(this.btn_E1, null);
            btn_E2_Click(this.btn_E2, null);
            btn_E3_Click(this.btn_E3, null);
            btn_E4_Click(this.btn_E4, null);
            btn_E5_Click(this.btn_E5, null);
            btn_E6_Click(this.btn_E6, null);
            btn_E7_Click(this.btn_E7, null);
            btn_E8_Click(this.btn_E8, null);
            btn_E9_Click(this.btn_E9, null);
            btn_E10_Click(this.btn_E10, null);
            btn_E11_Click(this.btn_E11, null);
            btn_E12_Click(this.btn_E12, null);

            btn_count[100]++;
        }
        private void lbl_F_Click(object sender, EventArgs e)
        {
            for (int i = 60; i < 72; i++)
            {
                if (btn_count[101] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_F1_Click(this.btn_F1, null);
            btn_F2_Click(this.btn_F2, null);
            btn_F3_Click(this.btn_F3, null);
            btn_F4_Click(this.btn_F4, null);
            btn_F5_Click(this.btn_F5, null);
            btn_F6_Click(this.btn_F6, null);
            btn_F7_Click(this.btn_F7, null);
            btn_F8_Click(this.btn_F8, null);
            btn_F9_Click(this.btn_F9, null);
            btn_F10_Click(this.btn_F10, null);
            btn_F11_Click(this.btn_F11, null);
            btn_F12_Click(this.btn_F12, null);

            btn_count[101]++;
        }  
        private void lbl_G_Click(object sender, EventArgs e)
        {
            for (int i = 72; i < 84; i++)
            {
                if (btn_count[102] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_G1_Click(this.btn_G1, null);
            btn_G2_Click(this.btn_G2, null);
            btn_G3_Click(this.btn_G3, null);
            btn_G4_Click(this.btn_G4, null);
            btn_G5_Click(this.btn_G5, null);
            btn_G6_Click(this.btn_G6, null);
            btn_G7_Click(this.btn_G7, null);
            btn_G8_Click(this.btn_G8, null);
            btn_G9_Click(this.btn_G9, null);
            btn_G10_Click(this.btn_G10, null);
            btn_G11_Click(this.btn_G11, null);
            btn_G12_Click(this.btn_G12, null);

            btn_count[102]++;
        }
        private void lbl_H_Click(object sender, EventArgs e)
        {
            for (int i = 84; i < 96; i++)
            {
                if (btn_count[103] % 2 != 0)
                {
                    btn_count[i] = 0;
                }
                else
                {
                    btn_count[i] = 1;
                }
            }

            btn_H1_Click(this.btn_H1, null);
            btn_H2_Click(this.btn_H2, null);
            btn_H3_Click(this.btn_H3, null);
            btn_H4_Click(this.btn_H4, null);
            btn_H5_Click(this.btn_H5, null);
            btn_H6_Click(this.btn_H6, null);
            btn_H7_Click(this.btn_H7, null);
            btn_H8_Click(this.btn_H8, null);
            btn_H9_Click(this.btn_H9, null);
            btn_H10_Click(this.btn_H10, null);
            btn_H11_Click(this.btn_H11, null);
            btn_H12_Click(this.btn_H12, null);

            btn_count[103]++;
        }

        void select_wells(int a)
        {
            lbl_testnum.Text = String.Format("Sequence {0}", a);
        }

        private void btn_slctOK_Click(object sender, EventArgs e)
        {
            btn_return.Visible = true;
            reset();
            reset_plate();
            well_selection(0);
            select_wells(1);
            timeleft = Int32.Parse(numericUpDown_purge.Text);
            temp_test = sequence_num - 2;
            k = sequence_num;
        }

        private void start()
        {

            
           
            
            //startpos(); //moves the column to well A1

            btn_start.Visible = false;
            char incoming;
            char incomingchar;
            groupbox_wells.Visible = false;
            int l = 0;
            well_num = select_num;
            int progress_max = 0;
            for(int i = 0; i < 96; i++)
            {
                if (num_passes[i] < 0 || num_passes[i]>10) //skips the indexes that aren't included in the current run
                {
                    break;
                }
                progress_max = progress_max + num_passes[i] * wellspertest[i]; //scales the max of the progress bar based on the number of wells that need to be filled

            }
            progressBar_selection.Maximum = progress_max;
            
            int temp = sequence_num;

            while (temp > 0)
            {
                mvmt_data[0] = 'A';
                mvmt_data[1] = '0';
                mvmt_data[2] = '0';
                mvmt_data[3] = '0';
                mvmt_data[4] = '0';
                mvmt_data[5] = '0';
                mvmt_data[6] = 'L';
                mvmt_data[7] = '*';

                string r = new string(mvmt_data);
                SerialPort1.Write(r);
                while(SerialPort1.BytesToRead == 0)
                {
                    
                }
               incomingchar = Convert.ToChar(SerialPort1.ReadByte());
                if (incomingchar == 'x')
                {

                    mvmt_data[0] = 'A';
                    mvmt_data[1] = 'H';
                    mvmt_data[7] = '*';

                    r = new string(mvmt_data);
                    SerialPort1.Write(r);
                    xpos = -1;
                    ypos = -1;
                }
                while (incomingchar == 'x')
                {
                    txt_waiting.Visible = true;
                    if (SerialPort1.BytesToRead != 0)
                    {
                        SerialPort1.DiscardInBuffer();
                        while (SerialPort1.BytesToRead == 0)
                        {
                           
                        }
                        incomingchar = Convert.ToChar(SerialPort1.ReadByte());
                       if (incomingchar == 's')
                        {
                            
                            
                            break;
                        }

                        
                    }
                    if (cancel == true)
                    {
                        break;
                    }
                }
                mvmt_data[0] = 'A';
                mvmt_data[1] = '0';
                mvmt_data[2] = '0';
                mvmt_data[3] = '0';
                mvmt_data[4] = '0';
                mvmt_data[5] = '0';
                mvmt_data[6] = '0';
                mvmt_data[7] = '*';
                r = new string(mvmt_data);
                SerialPort1.Write(r);
                SerialPort1.DiscardInBuffer();
                txt_purge.Visible = true;
                txt_waiting.Visible = false;
                WaitNSeconds(timeleft);
                txt_purge.Visible = false;
                progressBar_selection.Visible = true;

                timeleft = 0;
                startpos();
                

                for (int i = num_passes[sequence_num - temp]; i > 0; i--)
                {
                    int j = 0;
                    
                    incoming = '0';
                    move(select_y[l], select_x[l]);
                    SerialPort1.DiscardInBuffer();
                    //progressBar_selection.PerformStep();
                    while (j < wellspertest[sequence_num - temp]) //performs all of the moves in the order that they were clicked
                    {
                        
                        wellImageList[plateclicked[l]].Image = global::Fractionation.Properties.Resources.circleclicked;
                        if (SerialPort1.BytesToRead != 0)
                        {
                            incoming = Convert.ToChar(SerialPort1.ReadByte());
                        }
                        else
                        {
                            continue;
                        }
                        if (incoming == 'd') //waits for motors to move to new position before waiting in the well
                        {
                            mvmt_data[1] = 'Z';
                            mvmt_data[6] = 'D';
                            string s = new string(mvmt_data);
                            SerialPort1.Write(s);
                            needle_pos = false;
                            WaitNSeconds(well_time[l]);
                            //Thread.Sleep(well_time[l] * 1000);
                            //await Task.Delay(well_time[l] * 1000); //waits a specific ammount of time in each well
                            progressBar_selection.PerformStep();
                            mvmt_data[6] = 'U';
                            s = new string(mvmt_data);
                            SerialPort1.Write(s);
                            needle_pos = true;
                            
                            mvmt_data[1] = '0';
                            mvmt_data[6] = '0';

                            l++;
                            j++;
                        }
                        else
                        {
                            continue;
                        }
                        if (j == wellspertest[sequence_num - temp]) //breaks the loop before the 'move' function is called again
                        {
                            break;
                        }
                        move(select_y[l], select_x[l]);
                        SerialPort1.DiscardInBuffer();
                    }

                    if (i != 1) //only runs if all the passes for a given sequence have been completed
                    {
                        l = l - wellspertest[sequence_num - temp];
                    }
                    
                    
                }

                
                temp--;
                
                
            }
            progressBar_selection.PerformStep();
            reset();  //brings the column back over the waste container w=once all the sequences have been run
            progressBar_selection.Visible = false;
            txt_done.Visible = true;
            btn_okdone.Visible = true;
            btn_start.Visible = false;

            

        }

        private void btn_okwells_Click(object sender, EventArgs e)
        {
            
            well_num = select_num;
            select_num = Int32.Parse(numericUpDown_wells.Text)+select_num; //stores the total number of well selections
            num_passes[sequence_num - k] = Int32.Parse(numericUpDown_numpasses.Text); //stores the number of passes for each sequence
            wellspertest[sequence_num - k] = Int32.Parse(numericUpDown_wells.Text); //stores the number of wells to fill for each sequence
            for(int i = well_num;i<select_num;i++)
            {
                well_time[i] = Int32.Parse(numericUpDown_time.Text); //stores the amount of time spent in each well for each sequence
            }

            groupBox3.Enabled = true;
            btn_okwells.Visible = false;
            btn_next.Visible = true;
            btn_back.Visible = true;
            numericUpDown_wells.Enabled = false;
            numericUpDown_time.Enabled = false;
            numericUpDown_numpasses.Enabled = false;
            k--;

           
           



        }

        private async void btn_next_Click(object sender, EventArgs e) 
        {
            if (select_num_index != select_num) //If the user doesn't select the right ammount of wells the program doesn't continue
            {
                MessageBox.Show(String.Format("Select {0} more well(s)", select_num - select_num_index)); //message box prompts the user to select more or less wells
                return;
            }

            groupBox3.Enabled = false;

            numericUpDown_wells.Enabled = true;
            numericUpDown_time.Enabled = true;
            numericUpDown_numpasses.Enabled = true;
            for (int i = 0; i < 96; i++) //removes all the wells that have been selected
            {
                if (btn_count[i] % 2 != 0)
                {
                    wellImageList[i].Image = global::Fractionation.Properties.Resources.circlegray;
                    wellImageList[i].Enabled = false;
                }
            }
            if (temp_test >= 0) //continues prompting user to select the number of wells for a test untill all the tests have been defined
            {
                select_wells(sequence_num - temp_test);
                
                temp_test--;
                btn_next.Visible = false;
                btn_back.Visible = false;
                btn_okwells.Visible = true;
            }

            else
            {
                mvmt_data[0] = 'A';
                mvmt_data[1] = '0';
                mvmt_data[2] = '0';
                mvmt_data[3] = '0';
                mvmt_data[4] = '0';
                mvmt_data[5] = '0';
                mvmt_data[6] = 'L';
                mvmt_data[7] = '*'; 

                string s = new string(mvmt_data);


                SerialPort1.Write(s);

                cancel = false;
                
                lbl_testnum.Visible = false;
                btn_next.Visible = false;
                btn_back.Visible = false;
                
                groupbox_wells.Visible = false;
                
                char incoming;
                txt_waiting.Visible = true;
                while (0!=1)
                {
                    await Task.Delay(25);
                    if (SerialPort1.BytesToRead != 0)
                    {
                        SerialPort1.DiscardInBuffer();
                        incoming = Convert.ToChar(SerialPort1.ReadByte());

                        if (incoming == 's')
                        {
                            start();
                            break;
                        }
                    }
                    if (cancel == true)
                    {
                        break;
                    }
              
                    
                    
                }
                mvmt_data[0] = 'A';
                mvmt_data[1] = '0';
                mvmt_data[2] = '0';
                mvmt_data[3] = '0';
                mvmt_data[4] = '0';
                mvmt_data[5] = '0';
                mvmt_data[6] = '0';
                mvmt_data[7] = '*';
                s = new string(mvmt_data);
                SerialPort1.Write(s);
                SerialPort1.DiscardInBuffer();
                txt_waiting.Visible = false;
            }
        }

        void well_selection(int x)
        {
            tabControl1.TabPages.Remove(tab_Plate);
            tabControl1.TabPages.Remove(tab_Keypad);
            tabControl1.TabPages.Remove(tab_Callibration);
            tab_Plate.Controls.Remove(groupBox3);
            tab_Selection.Controls.Add(groupBox3);
            groupBox3.Enabled = false;
            lbl_testnum.Visible = true;

            select_num_index = x;
            select_num = x;
            sequence_num = Int32.Parse(numericUpDown_select.Text); //stores the number of sequences
            
            groupbox_test.Visible = false;

            
            groupbox_wells.Visible = true;
            btn_okwells.Visible = true;
            btn_next.Visible = false;
            btn_back.Visible = false;
            btn_start.Visible = false;
            numericUpDown_wells.Enabled = true;
            numericUpDown_time.Enabled = true;
            numericUpDown_numpasses.Enabled = true;

            while (select_num_index<select_num)
            {
                for(int i = 0; i < 96; i++)
                {
                    if (btn_count[i] % 2 != 0)
                    {
                        wellImageList[i].Enabled = false; //disable the wells that have already been selected
                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            for (int i=0;i<96;i++)
            {
                if ((wellImageList[i].Enabled == true) & (btn_count[i]%2 != 0))
                {
                    wellImageList[i].Enabled = true;
                    wellImageList[i].Image = global::Fractionation.Properties.Resources.circle12;
                    btn_count[i]++;
                }
            }
            k++;
            well_selection(select_num- Int32.Parse(numericUpDown_wells.Text));
        }

        private void btn_okdone_Click(object sender, EventArgs e) //returns the plate schematic to the "plate" tab and resets the selection tab
        {
            tab_Selection.Controls.Remove(groupBox3);
            tab_Plate.Controls.Add(groupBox3);
            tabControl1.TabPages.Add(tab_Plate);
            tabControl1.TabPages.Add(tab_Keypad);
            tabControl1.TabPages.Add(tab_Callibration);
            txt_done.Visible = false;
            btn_okdone.Visible = false;
            btn_return.Visible = false;
            progressBar_selection.Value = 0;
            select_num = 0;
            groupbox_test.Visible = true;
            for (int i = 0; i < 95; i++)
            {
                wellImageList[i].Enabled = true; //enables all of the wells after all the sequences have been completed
            }
            reset_plate();
            reset();
            groupBox3.Enabled = true;
            select_num_index = 0;
        }  

        void startpos() //sets the tip of the column to well A1
        {
            if (xpos < 0 || ypos < 0)
            {
                 check_needle_pos();
                 mvmt_data[0] = 'A';
                 mvmt_data[1] = 'S';
                /*mvmt_data[2] = '>';
                mvmt_data[3] = 'v';
                mvmt_data[4] = 'F';
                mvmt_data[5] = 'D';*/
                mvmt_data[6] = '0';
                 mvmt_data[7] = '*';

                 string s = new string(mvmt_data);
                 SerialPort1.Write(s); //sends the encoded string to the arduino

                 string incoming_char;
                 incoming_char = SerialPort1.ReadExisting();

                 while (incoming_char != "k")
                 {
                     incoming_char = SerialPort1.ReadExisting(); //waits for the column to reach its starting position
                 }
                 xpos = 0;
                 ypos = 0; 
                
            }
            else
            {
                move('A', '1');
                
            }
        }

        private void btn_zup_Click(object sender, EventArgs e)
        {
            needleup();
        }

        private void btn_zdown_Click(object sender, EventArgs e)
        {
            needledown();
        }

        void check_needle_pos()
        {
            //string incoming = "0";
            if (needle_pos == false)
            {
                needleup();
                //btn_zup.PerformClick();
                btn_zdown.Visible = true;
                btn_zup.Visible = false;

                needle_pos = true;
            }
            
        } //checks if the column is in a well, if it is, retracts column before moving
        
        //Buttons in the callibration tab
        private void btn_xcal_Click(object sender, EventArgs e)
        {
            startpos();
           
            mvmt_data[0] = 'A';
            mvmt_data[1] = 'C';
            mvmt_data[2] = 'X';

            tabControl1.TabPages.Remove(tab_Keypad);
            tabControl1.TabPages.Remove(tab_Plate);
            tabControl1.TabPages.Remove(tab_Selection);
            groupBox_chooseaxis.Visible = false;
            groupBox_cal.Enabled = true;
            btn_donecal.Visible = true;
            btn_upcal.PerformClick();
            btn_downcal.Visible = true;
            btn_cornertest.Visible = true;
            groupBox_cal.Text = "Callibrate X";

        } 
        private void btn_ycal_Click(object sender, EventArgs e)
        {
            startpos();
           
            mvmt_data[0] = 'A';
            mvmt_data[1] = 'C';
            mvmt_data[2] = 'Y';

            tabControl1.TabPages.Remove(tab_Keypad);
            tabControl1.TabPages.Remove(tab_Plate);
            tabControl1.TabPages.Remove(tab_Selection);
            groupBox_chooseaxis.Visible = false;
            groupBox_cal.Enabled = true;
            btn_donecal.Visible = true;
            btn_upcal.PerformClick();
            btn_downcal.Visible = true;
            btn_cornertest.Visible = true;
            groupBox_cal.Text = "Callibrate Y";

        }
        private void btn_zcal_Click(object sender, EventArgs e)
        {
            startpos();
           
            mvmt_data[0] = 'A';
            mvmt_data[1] = 'C';
            mvmt_data[2] = 'Z';

            tabControl1.TabPages.Remove(tab_Keypad);
            tabControl1.TabPages.Remove(tab_Plate);
            tabControl1.TabPages.Remove(tab_Selection);
            groupBox_chooseaxis.Visible = false;
            groupBox_cal.Enabled = true;
            btn_donecal.Visible = true;
            btn_upcal.PerformClick();
            btn_downcal.Visible = true;
            btn_cornertest.Visible = true;
            groupBox_cal.Text = "Callibrate Z";
            btn_minustenmm.Enabled = false;
            btn_plustenmm.Enabled = false;

        }
        private void btn_minushalfmm_Click(object sender, EventArgs e)
        {
            mvmt_data[3] = 'M';
            mvmt_data[4] = 'H';
            mvmt_data[7] = '*';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);

        }
        private void btn_plushalfmm_Click(object sender, EventArgs e)
        {
            mvmt_data[3] = 'P';
            mvmt_data[4] = 'H';
            mvmt_data[7] = '*';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);
        }
        private void btn_minusmm_Click(object sender, EventArgs e)
        {
            mvmt_data[3] = 'M';
            mvmt_data[4] = 'O';
            mvmt_data[7] = '*';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);
        }
        private void btn_plusmm_Click(object sender, EventArgs e)
        {
            mvmt_data[3] = 'P';
            mvmt_data[4] = 'O';
            mvmt_data[7] = '*';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);
        }
        private void btn_minustenmm_Click(object sender, EventArgs e)
        {
            mvmt_data[3] = 'M';
            mvmt_data[4] = 'T';
            mvmt_data[7] = '*';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);
        }
        private void btn_plustenmm_Click(object sender, EventArgs e)
        {
            mvmt_data[3] = 'P';
            mvmt_data[4] = 'T';
            mvmt_data[7] = '*';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);
        }
        private void btn_donecal_Click(object sender, EventArgs e)
        {
            check_needle_pos();
            btn_upcal.Visible = false;
            btn_downcal.Visible = false;
            groupBox_cal.Text = "";
            groupBox_cal.Enabled = false;
            groupBox_chooseaxis.Visible = true;
            btn_donecal.Visible = false;
            btn_upcal.Visible = false;
            btn_downcal.Visible = false;
            btn_cornertest.Visible = false;
            tabControl1.TabPages.Add(tab_Selection);
            tabControl1.TabPages.Add(tab_Plate);
            tabControl1.TabPages.Add(tab_Keypad);
        }
        private void btn_upcal_Click(object sender, EventArgs e)
        {
            needleup();
            mvmt_data[1] = 'C';

        }
        private void btn_downcal_Click(object sender, EventArgs e)
        {
            needledown();
            mvmt_data[1] = 'C';
        }
        //

        void needleup()
        {
            mvmt_data[0] = 'A';
           
            mvmt_data[1] = 'Z';
            mvmt_data[6] = 'U';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);
            mvmt_data[6] = '0';
            btn_zdown.Visible = true;
            btn_downcal.Visible = true;
            btn_zup.Visible = false;
            btn_upcal.Visible = false;
            needle_pos = true;
        } //retracts the column

        void needledown()
        {
            mvmt_data[0] = 'A';
           
            mvmt_data[1] = 'Z';
            mvmt_data[6] = 'D';
            string s = new string(mvmt_data);
            SerialPort1.Write(s);
            mvmt_data[6] = '0';
            btn_zup.Visible = true;
            btn_upcal.Visible = true;
            btn_downcal.Visible = false;
            btn_zdown.Visible = false;
            needle_pos = false;
        } //lowers the column

        

        private void btn_cornertest_Click(object sender, EventArgs e)
        {
            check_needle_pos();
            move('A', '<');
            WaitNSeconds(3);
            needledown();
            WaitNSeconds(2);
            needleup();
            move('H', '<');
            WaitNSeconds(2);
            needledown();
            WaitNSeconds(2);
            needleup();
            move('H', '1');
            WaitNSeconds(3);
            needledown();
            WaitNSeconds(2);
            needleup();
            move('A', '1');
            WaitNSeconds(2);
            needledown();
            WaitNSeconds(2);
            needleup();
        } //lowers the column into all 4 corners to ensure that it is properly callibrated

        private void btn_return_Click(object sender, EventArgs e)
        {
            cancel = true;
            tab_Selection.Controls.Remove(groupBox3);
            tab_Plate.Controls.Add(groupBox3);
            tabControl1.TabPages.Add(tab_Plate);
            tabControl1.TabPages.Add(tab_Keypad);
            tabControl1.TabPages.Add(tab_Callibration);
            progressBar_selection.Visible = false;
            btn_start.Visible = false;
            groupbox_wells.Visible = false;
            lbl_testnum.Visible = false;
            btn_return.Visible = false;
            txt_done.Visible = false;
            btn_okdone.Visible = false;
            progressBar_selection.Value = 0;
            select_num = 0;
            groupbox_test.Visible = true;
            for (int i = 0; i < 95; i++)
            {
                wellImageList[i].Enabled = true; //enables all of the wells after all the sequences have been completed
            }
            reset_plate();

            groupBox3.Enabled = true;
            select_num_index = 0;
        }

        
        private void WaitNSeconds(int seconds)
        {
            if (seconds < 1) return;
            DateTime _desired = DateTime.Now.AddSeconds(seconds);
            while (DateTime.Now < _desired)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

       
    }
    
}
