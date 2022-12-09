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

namespace Pcroom_seat
{
    public partial class Form4 : Form
    {

        int order_quntity;       //주문한 수량
        int product_quntity;     //제품의 수량
        int order_value;         //주문한 가격
        String order_name;       //주문한 제품 이름

        OracleConnection con;
        OracleCommand dcom;
        OracleDataAdapter da; // Data Provider인 DBAdapter 입니다.

        DataSet ds;// DataSet 객체입니다.
        OracleDataReader dr; //DataReader 객체입니다.
        OracleCommandBuilder myCommandBuilder; // 추가, 수정, 삭제시에 필요한 명령문을 자동으로 작성해주는 객체입니다.
        DataTable foodTable;// DataTable 객체입니다.
        public Form4(int value , int quntity , String name)  //폼3에서 받은 인수들
        {
            order_name = name;
            order_value = value;
            order_quntity = quntity;
            InitializeComponent();
            label3.Text = value.ToString()+"원";
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e) //확인
        {

            try
            {
                String searchcommandString = "SELECT quntity from foods WHERE foodname=:food_name";
                                                    //일단 현제 남아있는 제품의 수량을 찾아야함
                string connstr = "User Id=Arc; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string cloudconnstr = "User Id=PCROOM; Password=PCROOM; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                
                con = new OracleConnection(connstr); // 코딩용                 <!시연할떄는 코드 바꿔야함!>
                //con = new OracleConnection(cloudconnstr);  //시연용          <!시연할떄는 코드 바꿔야함!>

                con.Open();

                dcom = new OracleCommand(searchcommandString, con);

                dcom.Parameters.Add("food_name", OracleDbType.Varchar2, 20).Value = order_name;

                dr = dcom.ExecuteReader();

                while (dr.Read())
                {
                    product_quntity = int.Parse(dr[0].ToString());      //현제 남아있는 제품의 수량
                }

                String commandString = "UPDATE foods SET quntity=:quntity WHERE foodname=:food_name";

                dcom = new OracleCommand(commandString, con);
                                   // UPDATE할 제품의 수량 = 현제 남아있는 제품 수량 - 주문한 수량
                dcom.Parameters.Add("quntity", OracleDbType.Int32).Value = product_quntity-order_quntity;
                dcom.Parameters.Add("food_name", OracleDbType.Varchar2, 20).Value = order_name;

                dcom.ExecuteNonQuery();  //DB 업데이트

            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
            MessageBox.Show("주문이 완료되었습니다!"); //주문 완료 메시지
            Close();                                   //끝
        }
    }
}
