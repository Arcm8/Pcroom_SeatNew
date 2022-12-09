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
        int quntity = 0; //수량
        int res;         //결과값
        int objvalue;    //제품 가격

        String product_name;  //제품 이름
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string name,int value,Image img) //폼 2에서 가져온 인수들로 이미지,텍스트 등 체움
        {
            objvalue=value;
            InitializeComponent();
            pictureBox1.Image = img;
            label2.Text = name;
            product_name = name;
            label3.Text = value.ToString() + " 원";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  //수량 +
        {
            quntity++;
            label1.Text = "수량 : "+quntity.ToString()+" 개";
        }

        private void button2_Click(object sender, EventArgs e)   //수량 -
        {
            quntity--;
            label1.Text = "수량 : " + quntity.ToString() + " 개";
        }

        private void button4_Click(object sender, EventArgs e)  //확인
        {
            res = quntity * objvalue;                       //총 가격 계산해서 전달 (추가기능 제작 대비용)
            Form4 newform4 = new Form4(res, quntity, product_name);  //수량,제품이름 인수로 폼4 생성
            newform4.ShowDialog();
            Close();                                                 //폼4가 닫히면 폼3도 자동으로 닫힘
        }

        private void button3_Click(object sender, EventArgs e)  //취소
        {
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }
    }
    }

