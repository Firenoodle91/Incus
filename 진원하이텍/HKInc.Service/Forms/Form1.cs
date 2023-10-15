using HKInc.Utils.Common;
using HKInc.Utils.Encrypt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Service.Forms
{
    public partial class Form1 : Form
    {
        //string aa = @"c:\" + Application.CompanyName.ToString()  +"\\DB" + Application.ProductName + ".ini";
        //string ab = @"c:\" + Application.CompanyName.ToString();
        string aa = Application.StartupPath + "\\DB.ini";
        string ab = Application.StartupPath;
        iniFile ini;// = new iniFile(ab, aa);
        public Form1()
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
            try
            {
                tx_ip.Text = AESEncrypt256.Decrypt(ini.IniReadValue("server", "ip", aa), "hkinc");   //로그인창에 DB설정으로 들어가면 db.ini파일에서 ip정보 불러와서 화면에보여주는 부분 -  김주임추가
                tx_user.Text = AESEncrypt256.Decrypt(ini.IniReadValue("server", "user", aa), "hkinc"); //SID부분
                tx_passwd.Text = AESEncrypt256.Decrypt(ini.IniReadValue("server", "passwd", aa), "hkinc");  //ID부분
                tx_db.Text = AESEncrypt256.Decrypt(ini.IniReadValue("server", "db", aa), "hkinc"); //PASSWD부분
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ini.IniWriteValue("server", "ip", AESEncrypt256.Encrypt(tx_ip.Text,"hkinc"), aa);
            ini.IniWriteValue("server", "user", AESEncrypt256.Encrypt(tx_user.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "passwd", AESEncrypt256.Encrypt(tx_passwd.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "db", AESEncrypt256.Encrypt(tx_db.Text, "hkinc"), aa);
            GlobalVariable.DatabaseIP = tx_ip.Text;
            GlobalVariable.DBName = tx_db.Text;
            GlobalVariable.DBuser = tx_user.Text;
            GlobalVariable.DBPasswd = tx_passwd.Text;
            ServerInfo.Database = tx_db.Text;
            ServerInfo.UserId = tx_user.Text;
            ServerInfo.Password = tx_passwd.Text;
            ServerInfo.Server = tx_ip.Text;
            ServerInfo.ProductionDatabase = tx_db.Text;

            ServerInfo.ConnectStringListChange();

            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label5.Text = AESEncrypt256.Encrypt(tx_passwd.Text,"1234");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Text = AESEncrypt256.Decrypt(label5.Text, "1234");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tx_admin.Text == "incus6745")
            {
                panel2.Visible = false;
                panel1.Visible = true;
            }
            else {
                MessageBox.Show("관리자 암호가 틀립니다.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            tx_ip.Text = "";
            GlobalVariable.LocalIP = "N";
            ini.IniWriteValue("server", "ip", AESEncrypt256.Encrypt(tx_ip.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "user", AESEncrypt256.Encrypt(tx_user.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "passwd", AESEncrypt256.Encrypt(tx_passwd.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "db", AESEncrypt256.Encrypt(tx_db.Text, "hkinc"), aa);
            GlobalVariable.DatabaseIP = tx_ip.Text;
            GlobalVariable.DBName = tx_db.Text;
            GlobalVariable.DBuser = tx_user.Text;
            GlobalVariable.DBPasswd = tx_passwd.Text;
            ServerInfo.Database = tx_db.Text;
            ServerInfo.UserId = tx_user.Text;
            ServerInfo.Password = tx_passwd.Text;
            ServerInfo.Server = tx_ip.Text;
            ServerInfo.ProductionDatabase = tx_db.Text;

            ServerInfo.ConnectStringListChange();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GlobalVariable.LocalIP = "Y";
            tx_ip.Text = "";
            tx_db.Text = "";
            ini.IniWriteValue("server", "ip", AESEncrypt256.Encrypt(tx_ip.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "user", AESEncrypt256.Encrypt(tx_user.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "passwd", AESEncrypt256.Encrypt(tx_passwd.Text, "hkinc"), aa);
            ini.IniWriteValue("server", "db", AESEncrypt256.Encrypt(tx_db.Text, "hkinc"), aa);
            GlobalVariable.DatabaseIP = tx_ip.Text;
            GlobalVariable.DBName = tx_db.Text;
            GlobalVariable.DBuser = tx_user.Text;
            GlobalVariable.DBPasswd = tx_passwd.Text;
            ServerInfo.Database = tx_db.Text;
            ServerInfo.UserId = tx_user.Text;
            ServerInfo.Password = tx_passwd.Text;
            ServerInfo.Server = tx_ip.Text;
            ServerInfo.ProductionDatabase = tx_db.Text;

            ServerInfo.ConnectStringListChange();
            this.Close();
        }
    }
}
