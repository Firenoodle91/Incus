using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using HKInc.Service.Helper;
using DevExpress.XtraEditors;
using HKInc.Service.Handler;

namespace HKInc.Service.Controls
{
    public partial class HKRS232 : UserControl
    {
        string aa = "C:\\qmack\\Serial.ini";
        string ab = "C:\\qmack";
        //string aa = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Serial.ini";
        //string ab = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        iniFile ini;
        int AC = 0;

        public static string lsPort { set; get; }//portno
        public static string lsmcA { set; get; }
        public  Int64 lsACnt { get { return Convert.ToInt64(ACount.Text); } set { ACount.Text = value.ToString(); }  }
        //public int lsBaudRate { set; get; }//속도
        //public int lsDataBits { set; get; }//데이터비트
        //public StopBits lsStopBits { set; get; }//스톱비트
        //public Parity lsParity { set; get; }//페리티

        public HKRS232()
        {
            InitializeComponent();

            ini = new iniFile(ab, aa);
            lsPort = ini.IniReadValue("Serial", "port", aa);
            lsmcA = ini.IniReadValue("Serial", "mcA", aa);
            ACount.Text=  ini.IniReadValue("Serial", "ACnt", aa);
            if (ACount.Text == "") ACount.Text = "0";
        }

        public void Open()
        {
            if (!HKserialPort.IsOpen)
            {
                HKserialPort.Dispose();
                HKserialPort.Close();
                try
                {
                    if (lsPort == null || lsPort == "")
                    {
                        MessageBoxHandler.Show("인터페이스 설정에서 포트를 설정해주세요.");
                        return;
                    }
                    HKserialPort.PortName = lsPort;
                    HKserialPort.BaudRate = 115200;
                    HKserialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecived);
                    HKserialPort.Open();
                    simpleButton4.Text = "종료";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    simpleButton4.Text = "시작";
                }
            }
        }

        private void DataRecived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(MySerialReceived));
        }

        private void MySerialReceived(object s, EventArgs e)
        {
            try
            {
                string inData = HKserialPort.ReadLine();
                SetText(inData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void SetText(string inDate )
        {
            string sval = inDate.Replace("\r","");

            switch (sval)
            {
                case "ACNT":
                    AC++;
                    if (AC == 1)
                    {
                        ACount.Text = Convert.ToString(Convert.ToInt64(ACount.Text) + 1);
                        ini.IniWriteValue("Serial", "ACnt", ACount.Text, aa);
                    }
                    break;
                case "AA":
                    AC = 0;
                    break;
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
                simpleButton4.Text = "시작";
            }
        }

        private void Root_Click(object sender, EventArgs e)
        {
            //Close();
       
            //FMCSETTING fm = new FMCSETTING();
            //fm.ShowDialog();
            //Open();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (simpleButton4.Text == "시작")
                Open();
            else
                Close();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            ACount.Text = "0";
            ini.IniWriteValue("Serial", "ACnt", "0", aa);
        }
    }
}
