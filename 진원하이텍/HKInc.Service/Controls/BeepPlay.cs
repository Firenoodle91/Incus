using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Forms;
using System.Media;

namespace HKInc.Service.Controls
{
    public partial class BeepPlay : UserControl
    {
    public event EventHandler beepevent;
        public BeepPlay()
        {
            InitializeComponent();
          //  timer1.Start();
        }
        public string Stime="-1";
        public string SMin = "-1";
        public string Etime = "-1";
        public string EMin = "-1";
    
   
        public string sTime
        {
            set { Stime = value; }
            get {
                return Stime; ;
            }
        }
        public string sMin
        {
            set { SMin = value; }
            get
            {
                return SMin; ;
            }
        }
        public string eTime
        {
            set { Etime = value; }
            get
            {
                return Etime; ;
            }
        }
        public string eMin
        {
            set { EMin = value; }
            get
            {
                return EMin; ;
            }
        }

   
        private void timer1_Tick(object sender, EventArgs e)
        {
            //일단 죽여놓음
            return;

            timer1.Stop();
            try
            {
                SoundPlayer wp = new SoundPlayer("http://61.79.149.167:2001/Sound/Canary.wav");
                string sql = "SELECT [HH]      ,[MM]       FROM [dbo].[TN_ALAM002T]";//" SELECT[ST1]      ,[SM1]      ,[ST2]      ,[SM2] FROM[TN_ALAM001T]";
                DataSet ds = HKInc.Service.Service.DbRequesHandler.GetDataQury(sql);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count >= 1)
                    {
                        string t = DateTime.Now.Hour.ToString();
                        string m = DateTime.Now.Minute.ToString();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (t == ds.Tables[0].Rows[i][0].GetNullToEmpty())
                            {
                                if (m == ds.Tables[0].Rows[i][1].GetNullToEmpty())
                                {
                                    wp.PlaySync();

                                }
                            }
                         }
                        
                    }
                }



                //Stime = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 0);
                //SMin = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 1);
                //Etime = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 2);
                //eMin = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 3);
            }
            catch { }
            //SoundPlayer wp = new SoundPlayer("http://61.79.149.167:2001/Sound/Canary.wav");
            //string t = DateTime.Now.Hour.ToString();
            //string m = DateTime.Now.Minute.ToString();
        //    MessageBox.Show(t + ":" + m);
            //if (t == Stime)
            //{
            //    if (m == SMin)
            //    {
            //        wp.PlaySync();
              
            //    }
            //}
            //if (t == Etime)
            //{
            //    if (m == eMin)
            //    {
            //        wp.PlaySync();
                   
            //    }
            //}
            timer1.Start();
        }

        private void BeepPlay_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.beepevent != null)
                this.beepevent(this, e);
        }
    }
}
