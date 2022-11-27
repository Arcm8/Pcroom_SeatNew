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
    public partial class Form3 : Form
    {
        int quntity = 0;
        int res;
        int objvalue;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string name,int value,Image img)
        {
            objvalue=value;
            InitializeComponent();
            pictureBox1.Image = img;
            label2.Text = name;
            label3.Text = value.ToString() + " 원";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            quntity++;
            label1.Text = "수량 : "+quntity.ToString()+" 개";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            quntity--;
            label1.Text = "수량 : " + quntity.ToString() + " 개";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            res = quntity * objvalue;
            Form4 newform4 = new Form4(res);
            newform4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }
    }
    }

