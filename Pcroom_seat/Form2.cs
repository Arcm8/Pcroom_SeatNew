using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Pcroom_seat
{
    public partial class Form2 : Form
    {
        int category;
        Image img;
        int item_value;
        public Form2()
        {
            InitializeComponent();
            button1.BackColor = Color.CadetBlue;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 newform3 = new Form3();
            newform3.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            img = Image.FromFile(@"C:\pcroom\jja.jpg");
            Form3 newform3 = new Form3("짜장라면",2500, img);
            newform3.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //pictureBox3.Load(@"C:\Users\user\OneDrive\바탕화면\pcroom.jja.jpg");  
        }
        void draw_img(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(img,12,270,90,90);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            img = Image.FromFile(@"C:\pcroom\sin.jpg");
            Form3 newform3 = new Form3("신라면", 2000,img);
            newform3.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            category = 1;
            button1.BackColor = Color.CadetBlue;
            button2.BackColor = Color.Gray;
            //changecategory();
            Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            category = 2;
            button2.BackColor = Color.CadetBlue;
            button1.BackColor = Color.Gray;
            //changecategory();
            Invalidate();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            img = Image.FromFile(@"C:\pcroom\kimbab.png");
            Form3 newform3 = new Form3("김밥", 3000, img);
            newform3.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            img = Image.FromFile(@"C:\pcroom\dduck.png");
            Form3 newform3 = new Form3("떡볶이", 3500, img);
            newform3.ShowDialog();
        }

        private void changecategory()
        {
            string type_search;
            switch (category)
            {
                case 1:
                    type_search = "A";
                    break;
                case 2:
                    type_search = "B";
                    break;
                case 3:
                    type_search = "C";
                    break;
            }
            for (int i=0; i<5; i++)
            {
                //this.Controls["des"+i+"_2"].Text
                //this.Controls["des" + i + "_1"].Text
                //this.Controls["Des" + i + "_button"].Text
                //this.Controls["picturebox"+i].image=img;
            }
        }
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            switch (category)
            {
                case 1:
                  /*
                    for (int i = 0; i < 5; i++)
                    {
                        for (int ii = 0; ii<5; ii++)
                        {
                            //g.DrawImage(img, 12+(i*115), 70+(148*ii), 90, 90);
                            //this.Controls["des"+i+"_2"].Text
                            //this.Controls["des" + i + "_1"].Text
                            //this.Controls["Des" + i + "_button"].Text
                        }
                    }
                   */
                    img = Image.FromFile(@"C:\pcroom\jja.jpg");
                    g.DrawImage(img, 12, 70, 90, 90);
                    item_value = 2000;
                    //des1_2.Text = item_value + "원";
                    des1_2.Text = "2000 원";
                    des1_1.Text = "짜장라면";

                    img = Image.FromFile(@"C:\pcroom\sin.jpg");
                    g.DrawImage(img, 12 +115, 70, 90, 90);
                    des2_1.Text = "신라면";
                    des2_2.Text = "2500 원";

                    break;

                case 2:

                    for (int i = 0; i < 5; i++)
                    {
                    }
                    img = Image.FromFile(@"C:\pcroom\kimbab.png");
                    g.DrawImage(img, 12, 70, 90, 90);
                    des1_2.Text = "3000 원";

                    img = Image.FromFile(@"C:\pcroom\dduck.png");
                    g.DrawImage(img, 12 + 115, 70, 90, 90);
                    des1_2.Text ="3500 원";
                    break;
            }
        }

        private void Des1_button_Click(object sender, EventArgs e)
        {

            switch (category)
            {
                case 1:
                    img = Image.FromFile(@"C:\pcroom\jja.jpg");
                    break;
                case 2:
                    img = Image.FromFile(@"C:\pcroom\kimbab.png");
                    break;
            }
            int numonlyvalue = int.Parse(Regex.Replace(des1_2.Text, @"\D", ""));
            Form3 newform3 = new Form3(des1_1.Text, numonlyvalue, img);
            newform3.ShowDialog();
        }

        private void Des2_button_Click(object sender, EventArgs e)
        {
            switch (category)
            {
                case 1:
                    img = Image.FromFile(@"C:\pcroom\sin.jpg");
                    break;
                case 2:
                    img = Image.FromFile(@"C:\pcroom\dduck.png");
                    break;
            }
            int numonlyvalue = int.Parse(Regex.Replace(des2_2.Text, @"\D", ""));
            Form3 newform3 = new Form3(des2_1.Text, numonlyvalue, img);
            newform3.ShowDialog();
        }
    }
}
