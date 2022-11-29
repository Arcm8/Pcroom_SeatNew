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
using Oracle.DataAccess.Client;

namespace Pcroom_seat
{
    public partial class Form2 : Form
    {
        string category;
        Image img;
        int item_value;

        OracleConnection con;
        OracleCommand dcom;
        OracleDataAdapter da; // Data Provider인 DBAdapter 입니다.

        DataSet ds;// DataSet 객체입니다.
        OracleDataReader dr; //DataReader 객체입니다.
        OracleCommandBuilder myCommandBuilder; // 추가, 수정, 삭제시에 필요한 명령문을 자동으로 작성해주는 객체입니다.
        DataTable foodTable;// DataTable 객체입니다.

        public void DB_Open_food()
        {
     
        }
        public Form2()
        {
            InitializeComponent();


            button1.BackColor = Color.CadetBlue;
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
            category = "A";
            changecategory("A");
            Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            category = "B";
            changecategory("B");
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

        private void changecategory(string category_selected)
        {
            string type_search = category_selected;

            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.Gray;
            button5.BackColor = Color.Gray;
            button6.BackColor = Color.Gray;
            button7.BackColor = Color.Gray;

            if (type_search == "A")
            {
                button1.BackColor = Color.CadetBlue;
            } else if(type_search == "B"){
                button2.BackColor = Color.CadetBlue;
            } else if(type_search == "C"){
                button4.BackColor = Color.CadetBlue;
            } else if(type_search == "D"){
                button5.BackColor = Color.CadetBlue;
            } else if(type_search == "E"){
                button6.BackColor = Color.CadetBlue;
            } else if(type_search == "F"){
                button7.BackColor = Color.CadetBlue;
            }



            try
            {
                for (int j = 1; j < 40; j++)
                {
                    this.Controls["picturebox" + j].BackgroundImage = null;
                    this.Controls["des" + j + "_2"].Text = "(음식 가격)";
                    this.Controls["des" + j + "_1"].Text = "(음식 이름)";
                }
                string connstr = "User Id=Arc; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string commandString = "select * from foods where foodcategory =:selectedcategory";

                if (type_search == "All")
                {
                    button3.BackColor = Color.CadetBlue;
                    commandString = "select * from foods";
                }

                con = new OracleConnection(connstr);
                con.Open();
                dcom = new OracleCommand(commandString, con);

                dcom.Parameters.Add("selectedcategory", OracleDbType.Varchar2, 20);
                dcom.Parameters["selectedcategory"].Value = type_search;

                dr = dcom.ExecuteReader();
                int i = 1;
                while (dr.Read())
                {

                    this.Controls["des"+i+"_2"].Text=dr["foodvalue"].ToString()+" 원";
                    this.Controls["des" + i + "_1"].Text = dr["foodname"].ToString();
                    //img = Image.FromFile(@"C:\pcroom\jja.jpg");
                    img = Image.FromFile(@dr["img"].ToString());
                    this.Controls["picturebox"+i].BackgroundImage=img;
                    this.Controls["picturebox" + i].BackgroundImageLayout = ImageLayout.Stretch;
                    //this.Controls.OfType<PictureBox>

                    i++;
                }
                dr.Close();
                con.Close();

            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            } catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
            /*
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
            */
        }

        private void Des1_button_Click(object sender, EventArgs e)
        {
            int numonlyvalue = int.Parse(Regex.Replace(des1_2.Text, @"\D", ""));
            Form3 newform3 = new Form3(des1_1.Text, numonlyvalue, img);
            newform3.ShowDialog();
        }

        private void Des2_button_Click(object sender, EventArgs e)
        {  
            int numonlyvalue = int.Parse(Regex.Replace(des2_2.Text, @"\D", ""));
            Form3 newform3 = new Form3(des2_1.Text, numonlyvalue, img);
            newform3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e) //전체
        {
            category = "All";
            changecategory("All");
            Invalidate();
        }

        private void button4_Click(object sender, EventArgs e) //밥류
        {
            category = "C";
            changecategory("C");
            Invalidate();
        }

        private void button5_Click(object sender, EventArgs e) //음료
        {
            category = "D";
            changecategory("D");
            Invalidate();
        }

        private void button6_Click(object sender, EventArgs e) //과자
        {
            category = "E";
            changecategory("E");
            Invalidate();
        }
        private void button7_Click(object sender, EventArgs e) //기타
        {
            category = "F";
            changecategory("F");
            Invalidate();
        }

        private void des6_2_Click(object sender, EventArgs e)
        {

        }
    }
}
