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

        private void button1_Click(object sender, EventArgs e)
        {
            int BloodID = 50501;
            for (int i = 0; i < 10; ++i)
            {

                HSPRNG.Randomize(BloodID);
                HSPRNG.ExRandomize(BloodID);
                int var_001 = 4 + HSPRNG.Rnd(12);
                Console.WriteLine(BloodID + "," + var_001);
                BloodID++;

            }
        }
    }
}
