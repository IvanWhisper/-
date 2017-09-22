using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 重启关机注销测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsExit.PowerOff();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowsExit.Reboot();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowsExit.LogOff();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowsExit.Lock();

        }
    }
}
