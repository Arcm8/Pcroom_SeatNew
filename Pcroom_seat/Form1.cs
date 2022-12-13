using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Globalization;

namespace Pcroom_seat
{
    public partial class Form1 : Form
    {   
        String lefttime_secound; //남은시간 DB에서 담을 문자열

        OracleConnection con;
        OracleCommand dcom;

        OracleDataReader dr; //DataReader

        int lefttime_int;

        int refresh_term;

        String currusername; //현재 유저의 이름을 담을 문자열

        int  seatno = 4; //자리번호
        public Form1()
        {
            InitializeComponent();
        }



        private void timer1_Tick(object sender, EventArgs e) //타이머 1틱
        {

            refresh_term++;  
            if (refresh_term == 5) //10초마다 DB 갱신 [5초로 변경]
            {

                try
                {

                    string connstr = "User Id=Arc; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                    string cloudconnstr = "User Id=PCROOM; Password=PCROOM; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                    String commandString = "UPDATE USER_DATA SET LEFT_TIME=:LEFT_TIME WHERE user_id =: USER_ID";

                    con = new OracleConnection(connstr); // 코딩용          <!시연할떄는 코드 바꿔야함!>
                    //con = new OracleConnection(cloudconnstr);  //시연용   <!시연할떄는 코드 바꿔야함!>

                    con.Open();

                    dcom = new OracleCommand(commandString, con);

                    dcom.Parameters.Add("LEFT_TIME", OracleDbType.Int32).Value = lefttime_int;
                    dcom.Parameters.Add("USER_ID", OracleDbType.Varchar2, 20).Value = currusername;

                    dcom.ExecuteNonQuery();

                    commandString = "UPDATE SEAT SET USER_ID=:curruser WHERE SEAT_ID=:currseat";
                    dcom = new OracleCommand(commandString, con);
                    dcom.Parameters.Add("USER_ID", OracleDbType.Varchar2, 20).Value = currusername;
                    dcom.Parameters.Add("SEAT_ID", OracleDbType.Int32).Value = seatno;

                    dcom.ExecuteNonQuery();

                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message);
                }
                refresh_term = 0;  //갱신주기 초기화
            }

            if (lefttime_int == 0)  //시간이 만료되었을때
            {
                this.timer1.Stop();     //타이머 종료
                MessageBox.Show("시간이 만료되었습니다!","",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
                                                                // DB에서 받은 String 타입의 남은시간을  |
                                                                // int타입으로 바꾸는 과정               |
                                                                // Date 타입으로 바꾸면                  |
                                                                // 24시간이 넘어갈때 exception 발생      V
            int temptime_h = ((lefttime_int) / 3600);
            if ((temptime_h) < 10)
            {
                temptime_h = 0 + temptime_h;
            }
            int temptime_m = (((lefttime_int) % 3600) / 60);
            int temptime_s = ((((lefttime_int) % 3600) % 60));


            string temptime_format = temptime_h.ToString() + ":" + temptime_m.ToString() + ":" + temptime_s.ToString();

            lefttime_int = lefttime_int - 1; //1틱마다 1초 감소
            this.label1.Text = "[ 남은시간 ] : " + temptime_format;  //남은시간 표시

        }

        private void DB_connect(String username)   //DB에서 남은시간 가져오는 코드
        {
            currusername = username; //현재 유저이름 변경
            try
            {
                string connstr = "User Id=Arc; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string cloudconnstr = "User Id=PCROOM; Password=PCROOM; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";

                string commandString = "select LEFT_TIME from USER_DATA where USER_ID =: USER_ID ";

                con = new OracleConnection(connstr); // 코딩용             <!시연할떄는 코드 바꿔야함!>
                //con = new OracleConnection(cloudconnstr);  //시연용      <!시연할떄는 코드 바꿔야함!>

                con.Open();

                dcom = new OracleCommand(commandString, con);

                dcom.Parameters.Add("USER_ID", OracleDbType.Varchar2,20).Value = username;


                dr = dcom.ExecuteReader();

                while (dr.Read())
                {
                    lefttime_secound = dr[0].ToString();
                }
                lefttime_int = int.Parse(lefttime_secound);
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DB_connect("arc"); //기본 유저 설정
   
            this.label2.Text = "No."+seatno.ToString();
            this.label1.Text = "[ 남은시간 ] : --:--:-- ";
            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;

            try // Initial SEAT INSERT문
            {
                String initialsql = "INSERT INTO SEAT (SEAT_ID , USER_ID , IS_ON ) VALUES (:currseatid , :curruser , '1')";
                dcom = new OracleCommand(initialsql, con);
                dcom.Parameters.Add("currseatid", OracleDbType.Int32).Value = seatno;
                dcom.Parameters.Add("curruser", OracleDbType.Varchar2, 20).Value = currusername;

                dcom.ExecuteNonQuery();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newform2 = new Form2(seatno);
            newform2.ShowDialog();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

                     //아래로 디버그용 코드
        private void button2_Click(object sender, EventArgs e) //woo
        {
            DB_connect("woo29864");
        }

        private void button3_Click(object sender, EventArgs e) //arc
        {
            DB_connect("arc");
        }

        private void button4_Click(object sender, EventArgs e) //random
        {
            DB_connect("randomuser1");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //폼 닫을때 DB 삭제
        {
            try
            {
                String sql = "DELETE FROM SEAT WHERE SEAT_ID =:currseatid";
                dcom = new OracleCommand(sql,con);
                dcom.Parameters.Add("currseatid", OracleDbType.Int32).Value = seatno;

                dcom.ExecuteNonQuery();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }
    }
}
