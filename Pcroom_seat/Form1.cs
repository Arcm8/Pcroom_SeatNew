using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pcroom_seat
{
    public partial class Form1 : Form
    {
        DateTime dt1 = DateTime.Now;
        String lefttime_secound = "37230";

       

        DateTime lefttime_d;

        String seatno = "4";
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lefttime_d = lefttime_d.AddSeconds(-0.5);
            this.label1.Text = "[ 남은시간 ] : " + lefttime_d.ToString("HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String temptime_h = (int.Parse(lefttime_secound) / 3600).ToString();
            if (int.Parse(temptime_h) < 10)
            {
                temptime_h = 0 + temptime_h;
            }
            String temptime_m = ((int.Parse(lefttime_secound) % 3600) / 60).ToString();
            String temptime_s = (((int.Parse(lefttime_secound) % 3600) % 60)).ToString();

            string temptime_format = temptime_h + temptime_m + temptime_s;

            this.label2.Text = "No."+seatno;
            this.label1.Text = "[ 남은시간 ] : --:--:-- ";
            this.timer1.Interval = 1000;
            this.timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Enabled = true;
            lefttime_d = DateTime.ParseExact(temptime_format, "HHmmss", System.Globalization.CultureInfo.InvariantCulture);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newform2 = new Form2();
            newform2.ShowDialog();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
