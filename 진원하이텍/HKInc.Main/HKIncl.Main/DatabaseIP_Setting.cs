using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Service;

namespace HKInc.Main
{
    public partial class DatabaseIP_Setting : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        string aa = @"c:\" + Application.CompanyName.ToString() + "\\DB.ini";
        string ab = @"c:\" + Application.CompanyName.ToString();
        iniFile ini;// = new iniFile(ab, aa);
        public DatabaseIP_Setting()
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
        }

        public DatabaseIP_Setting(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarVisible(false);
        }

        protected override void InitControls()
        {
            base.InitControls();
            SetMessage(string.Format("현재 Setting IP : {0}", GlobalVariable.DatabaseIP));
            simpleButton1.Click += SimpleButton1_Click;
            simpleButton2.Click += SimpleButton2_Click;
            tx_ip.EditValue = ini.IniReadValue("server", "ip", aa);   //로그인창에 DB설정으로 들어가면 db.ini파일에서 ip정보 불러와서 화면에보여주는 부분 -  김주임추가
            tx_user.EditValue = ini.IniReadValue("server", "user", aa); //SID부분
            tx_passwd.EditValue = ini.IniReadValue("server", "passwd", aa);  //ID부분
            tx_db.EditValue = ini.IniReadValue("server", "db", aa); //PASSWD부분
        }

     

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            
          
            ini.IniWriteValue("server", "ip", tx_ip.EditValue.GetNullToNull(), aa);
            ini.IniWriteValue("server", "user", tx_user.EditValue.GetNullToNull(), aa);
            ini.IniWriteValue("server", "passwd", tx_passwd.EditValue.GetNullToNull(), aa);
            ini.IniWriteValue("server", "db", tx_db.EditValue.GetNullToNull(), aa);
            GlobalVariable.DatabaseIP = tx_ip.EditValue.GetNullToNull();
            GlobalVariable.DBName = tx_db.EditValue.GetNullToNull();
            GlobalVariable.DBuser = tx_user.EditValue.GetNullToNull();
            GlobalVariable.DBPasswd = tx_passwd.EditValue.GetNullToNull();
            ServerInfo.Database= tx_db.EditValue.GetNullToNull();
            ServerInfo.UserId = tx_user.EditValue.GetNullToNull();
            ServerInfo.Password = tx_passwd.EditValue.GetNullToNull();
            ServerInfo.Server = tx_ip.EditValue.GetNullToNull();
            ServerInfo.ProductionDatabase = tx_db.EditValue.GetNullToNull();
          
            ServerInfo.ConnectStringListChange();
            
            Close();
           
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

     
    }
}