using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Service.Helper;
using System.IO.Ports;
using HKInc.Service.Service;

namespace HKInc.Service.Controls
{
    public partial class HKRS232_RUS : UserControl
    {
        string aa = "C:\\jimes\\Serial.ini";
        string ab = "C:\\jimes";
        iniFile ini;
        string mcf = "N";
        Boolean isup = false;
        public static string lsPort { set; get; }//portno
        public static string lsmc { set; get; }
        public  string lsportstate { set; get; }

        public Int64 lsACnt { get { return Convert.ToInt64(ACount.Text); } set { ACount.Text = value.ToString(); } }
        public Int64 lsBCnt { get { return Convert.ToInt64(BCount.Text); } set { BCount.Text = value.ToString(); } }
        public HKRS232_RUS()
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
            lsPort = ini.IniReadValue("Serial", "port", aa);
            lsmc = ini.IniReadValue("Serial", "mc", aa);
            ACount.Text = ini.IniReadValue("Serial", "ACnt", aa);
            BCount.Text = ini.IniReadValue("Serial", "BCnt", aa);
            if (ACount.Text == "") ACount.Text = "0";
            if (BCount.Text == "") BCount.Text = "0";
        }
        public void  Open()
        {
            if (!HKserialPort.IsOpen)
            {
                HKserialPort.Dispose();
                HKserialPort.Close();
                try
                {
                    HKserialPort.PortName = lsPort;
                    HKserialPort.BaudRate = 115200;
                    HKserialPort.DataBits = 8;
                    HKserialPort.Parity = Parity.None;
                    HKserialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecived);
                    HKserialPort.Open();
                    lsportstate = "Open";
                    DbRequestHandler.SetDataQury("UPDATE TN_MC set MACHINE_STATE='ON' where MACHINE_CODE='" + lsmc + "'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    lsportstate = "Close";
                    DbRequestHandler.SetDataQury("UPDATE TN_MC set MACHINE_STATE='OFF' where MACHINE_CODE='" + lsmc + "'");

                }
            }
        }
        private void DataRecived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(MySerialReceived));
        }
        private void MySerialReceived(object s, EventArgs e)
        {
            string inData = HKserialPort.ReadLine();
            SetText(inData);
        }
        private void SetText(string inDate)
        {
            string sval = inDate.Replace("\r", "");
            switch (sval)
            {
                case "ACNT":
                            ACount.Text = Convert.ToString(Convert.ToInt64(ACount.Text) + 1);
                            ini.IniWriteValue("Serial", "ACnt", ACount.Text, aa);
                            break;
                case "AA":  break;
                case "BCNT":
                            BCount.Text = Convert.ToString(Convert.ToInt64(BCount.Text) + 1);
                            ini.IniWriteValue("Serial", "BCnt", ACount.Text, aa);
                            break;
                case "BB":  break;
                case "CCNT":
                    DbRequestHandler.SetDataQury("UPDATE TN_MC set MACHINE_STATE='RUN' where MACHINE_CODE='" + lsmc + "'");
                    break;
                case "CC":
                    if (mcf == "N")
                    {
                        DbRequestHandler.SetDataQury("UPDATE TN_MC set MACHINE_STATE='STOP' where MACHINE_CODE='" + lsmc + "'");
                    }
                    break;
                case "DCNT":
                    mcf = "Y";
                    ACount.Text = Convert.ToString(Convert.ToInt64(ACount.Text) + 1);
                    ini.IniWriteValue("Serial", "ACnt", ACount.Text, aa);
                    if (!isup)
                    {
                        DbRequestHandler.SetDataQury("UPDATE TN_MC set MACHINE_STATE='RUN' where MACHINE_CODE='" + lsmc + "'");
                        isup = true;
                    }
                    break;
                case "DD": break;
            }
        }
        public void Close()
        {
            if (HKserialPort.IsOpen)  //시리얼포트가 열려 있으면
            {
                HKserialPort.ReadExisting(); // 시리얼 읽기, 비우기
                HKserialPort.DiscardInBuffer();
                HKserialPort.DiscardOutBuffer();
                HKserialPort.Close();  //시리얼포트 닫기
                lsportstate = "Close";
                DbRequestHandler.SetDataQury("UPDATE TN_MC set MACHINE_STATE='OFF' where MACHINE_CODE='" + lsmc + "'");
                isup =false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ACount.Text = "0";

            ini.IniWriteValue("Serial", "ACnt", "0", aa);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            BCount.Text = "0";

            ini.IniWriteValue("Serial", "BCnt", "0", aa);
        }
    }
}
