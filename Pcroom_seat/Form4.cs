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
        int errorcheck;

        int seat_num;
        int order_quntity;       //주문한 수량
        int product_quntity;     //제품의 수량
        int order_value;         //주문한 가격
        String order_name;       //주문한 제품 이름
        Boolean isordercompleted; //주문이 완료되었는지
        String product_id;

        OracleConnection con;
        OracleCommand dcom;
        OracleDataAdapter da; // Data Provider인 DBAdapter 입니다.

        DataSet ds;// DataSet 객체입니다.
        OracleDataReader dr; //DataReader 객체입니다.
        OracleCommandBuilder myCommandBuilder; // 추가, 수정, 삭제시에 필요한 명령문을 자동으로 작성해주는 객체입니다.
        DataTable foodTable;// DataTable 객체입니다.
        public Form4(int value , int quntity , String name , int seatnum)  //폼3에서 받은 인수들
        {
            seat_num = seatnum;
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
                String searchcommandString = "SELECT * from foods WHERE foodname=:food_name";
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
                    errorcheck = 1;
                    product_quntity = Convert.ToInt32(dr.GetString(5));
                    errorcheck = 10;                                         //현제 남아있는 제품의 수량
                    product_id = Convert.ToString(dr.GetValue(0));
                    errorcheck = 2;
                }

                if (product_quntity-order_quntity < 0)  //제품의 수량이 부족할때
                {
                    isordercompleted = false;
                    MessageBox.Show("제품의 수량이 부족해 결제가 실패했습니다!");
                    Close();
                }
                else
                {
                    int ordernum = 1;
                    isordercompleted = true;
                    String commandString = "SELECT * FROM ORDERLIST ";

                    dcom = new OracleCommand(commandString, con);

                    dr = dcom.ExecuteReader();
                    errorcheck = 3;
                    while (dr.Read())
                    {
                        ordernum = dr.GetInt32(0);      
                    }
                    errorcheck = 4;

                    commandString = "UPDATE foods SET quntity=:quntity WHERE foodname=:food_name";

                    dcom = new OracleCommand(commandString, con);
                    // UPDATE할 제품의 수량 = 현제 남아있는 제품 수량 - 주문한 수량
                    dcom.Parameters.Add("quntity", OracleDbType.Int32).Value = product_quntity - order_quntity;
                    dcom.Parameters.Add("food_name", OracleDbType.Varchar2, 20).Value = order_name;

                    dcom.ExecuteNonQuery();  //DB 업데이트


                    DateTime dateTime = DateTime.Now;

                    String dateformant = dateTime.ToString("[yyyy-MM-dd] HH-mm-ss");
                    errorcheck = 3;
                    commandString = "INSERT INTO ORDERLIST (ORDERLIST_ID , ORDERNUMBER , MENU_ID , ORDERDATE , ORDER_QUANTITY , MESSAGE , SEAT_ID ) VALUES ( :orderid , :ordernum , :menuid , :orderdate , :orderquntity , :message , :seatid)";

                    dcom = new OracleCommand(commandString, con);
                    dcom.Parameters.Add("orderid", OracleDbType.Int32).Value = ordernum;
                    dcom.Parameters.Add("ordernum", OracleDbType.Int32).Value = ordernum;
                    dcom.Parameters.Add("menuid", OracleDbType.Int32).Value = product_id;
                    dcom.Parameters.Add("orderdate", OracleDbType.Varchar2, 50).Value = dateformant;
                    dcom.Parameters.Add("orderquntity", OracleDbType.Int32).Value = order_quntity;
                    dcom.Parameters.Add("message", OracleDbType.Varchar2, 500).Value = textBox1.Text;
                    dcom.Parameters.Add("seatid", OracleDbType.Int32).Value = seat_num;

                    dcom.ExecuteNonQuery();
                }

            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message+ errorcheck);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message+ errorcheck);
            }
            if (isordercompleted)
            {
                MessageBox.Show("주문이 완료되었습니다!"); //주문 완료 메시지
                Close();
            }                                               //끝
        }
    }
}
