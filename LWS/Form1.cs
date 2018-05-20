using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LWS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            String TextBox1 = textBox1.Text;
            int SracheID = int.Parse(TextBox1);
            int SeedID = 50501;
            int ListNo = 1;
            int PageNo;
            Console.WriteLine("No,Page,Id,Blood");
            for (int i = 0; i < SracheID; ++i)
            {
                int PageNo1 = SeedID - 50501;
                PageNo = PageNo1 / 16 + 1;
                HSPRNG.Randomize(SeedID);
                HSPRNG.ExRandomize(SeedID);
                int BloodLV = 4 + HSPRNG.Rnd(12);
                Console.WriteLine(ListNo + "," + PageNo + "," + SeedID + "," + BloodLV);
                SeedID++;
                ListNo++;



            }
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void textBox2_TextChanged(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            }
        }
    }

