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
        Image img;
        OracleConnection con;
        OracleCommand dcom;
        OracleDataAdapter da; // Data Provider인 DBAdapter 입니다.

        DataSet ds;// DataSet 객체입니다.
        OracleDataReader dr; //DataReader 객체입니다.
        OracleCommandBuilder myCommandBuilder; // 추가, 수정, 삭제시에 필요한 명령문을 자동으로 작성해주는 객체입니다.
        DataTable foodTable;// DataTable 객체입니다.

        public Form2()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 newform3 = new Form3();
            newform3.ShowDialog();
        }


        private void Form2_Load(object sender, EventArgs e)         //기본적으로 카테고리는 전체를 선택함
        {
            changecategory("All");
        }
        void draw_img(object sender, PaintEventArgs e)
        {
        }


        private void changecategory(string category_selected)  //카테고리 변경
        {
            string type_search = category_selected;

            button1.BackColor = Color.Gray;  //버튼 색 변경(어느 카테고리인지 알 수 있게)
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.Gray;
            button5.BackColor = Color.Gray;
            button6.BackColor = Color.Gray;
            button7.BackColor = Color.Gray;

            if (type_search == "A")                         //선택한 카테고리가 무었인지
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
                button7.BackColor = Color.CadetBlue;            //선택된 카테고리의 버튼 색 변경
            }



            try
            {
                for (int j = 1; j < 40; j++)                        // 컨트롤 내용 초기화
                {
                    this.Controls["picturebox" + j].BackColor = SystemColors.Control;
                    this.Controls["picturebox" + j].BackgroundImage = null;
                    this.Controls["des" + j + "_2"].Text = "(음식 가격)";
                    this.Controls["des" + j + "_1"].Text = "(음식 이름)"; 
                }

                string cloudconnstr = "User Id=PCROOM; Password=PCROOM; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string connstr = "User Id=Arc; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string commandString = "select * from foods where foodcategory =:selectedcategory";

                if (type_search == "All")           //찾는 타입이 '전체' 일 경우
                {
                    button3.BackColor = Color.CadetBlue;
                    commandString = "select * from foods";
                }
                //con = new OracleConnection(cloudconnstr);  //시연용           <!시연할떄는 코드 바꿔야함!>
                con = new OracleConnection(connstr);     //코딩용               <!시연할떄는 코드 바꿔야함!>
                con.Open();
                dcom = new OracleCommand(commandString, con);

                dcom.Parameters.Add("selectedcategory", OracleDbType.Varchar2, 20);
                dcom.Parameters["selectedcategory"].Value = type_search;

                dr = dcom.ExecuteReader();
                int i = 1;
                while (dr.Read())
                {
                    this.Controls["des"+i+"_2"].Text=dr["foodvalue"].ToString()+" 원";   //DB에서 찾은 내용
                    this.Controls["des" + i + "_1"].Text = dr["foodname"].ToString();    //컨트롤에 순차적으로 적용
                    
                    if (int.Parse(dr["quntity"].ToString()) <= 0)           //음식 제고가 0이면
                    {
                        img = Image.FromFile(@"C:\pcroom\soldout.png");
                        this.Controls["picturebox" + i].BackColor = Color.White;  //백컬러 색을 화이트로
                                                                                  //해서 제고가 0인 곳을 구분
                    }
                    else
                    {
                        img = Image.FromFile(@dr["img"].ToString());
                    }
                    this.Controls["picturebox"+i].BackgroundImage=img;
                    this.Controls["picturebox" + i].BackgroundImageLayout = ImageLayout.Stretch;
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
        }

        private void Des1_button_Click(object sender, EventArgs e) //이제 안씀
        {
        }

        private void Des2_button_Click(object sender, EventArgs e)  //이제 안씀
        {  
        }

        private void button1_Click(object sender, EventArgs e) //카테고리_A (라면)
        {
            changecategory("A");
        }

        private void button2_Click(object sender, EventArgs e) //카테고리_B (분식)
        {
            changecategory("B");
        }

        private void button3_Click(object sender, EventArgs e) //카테고리_전체
        {
            changecategory("All");
        }

        private void button4_Click(object sender, EventArgs e) //카테고리_C (밥류)
        {
            changecategory("C");
        }

        private void button5_Click(object sender, EventArgs e) //카테고리_D (음료)
        {
            changecategory("D");
        }

        private void button6_Click(object sender, EventArgs e) //카테고리_E (과자)
        {
            changecategory("E");
        }
        private void button7_Click(object sender, EventArgs e) //카테고리_F (기타)
        {
            changecategory("F");
        }

        private void des6_2_Click(object sender, EventArgs e)
        {

        }

        private void foodSelected(int i)                        //음식을 선택하였을떄
        {
            if(this.Controls["picturebox" + i].BackgroundImage != null)   //공백인 컨트롤은 선택되지 않도록
            {
                if(this.Controls["picturebox" + i].BackColor!=Color.White) {
                                                                                //soldout된 제품은 선택 X
                    img = this.Controls["picturebox" + i].BackgroundImage;
                    int numonlyvalue = int.Parse(Regex.Replace(this.Controls["des" + i + "_2"].Text, @"\D", ""));
                    Form3 newform3 = new Form3(this.Controls["des" + i + "_1"].Text, numonlyvalue, img);
                    newform3.ShowDialog();
                    Close();
                }                          //img= 선택한 backgroundimgage에서 추출
            }                                                    //numonlyvalue = 가격에서 정규화로 숫자만 추출
        }                                                        //제품 이름 = 컨트롤에서 text 추출
                                                                 //위 3개를 인수로 폼 3 생성
        private void pictureBox1_Click(object sender, EventArgs e)      //아래로 이미지박스(1~15) 선택할때 코드
        {
            foodSelected(1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            foodSelected(2);
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            foodSelected(3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            foodSelected(4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            foodSelected(5);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            foodSelected(6);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            foodSelected(7);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            foodSelected(8);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            foodSelected(9);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            foodSelected(10);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            foodSelected(11);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            foodSelected(12);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            foodSelected(13);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            foodSelected(14);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            foodSelected(15);
        }
    }
}
