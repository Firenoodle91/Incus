using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Service.Controls
{
    public partial class Uc_MC_State3 : UserControl
    {
        public Uc_MC_State3()
        {
            InitializeComponent();
        }


        //public string pnl_stateSetting
        //{
        //    get { return pnl_state.BackColor.ToString(); }
        //    set {
        //        if(value != null)
        //        {
        //            pnl_state.BackColor = Color.Black;
        //            pnl_state.Dock = DockStyle.Fill;
        //        }
        //        else
        //        {
        //            pnl_state.BackColor = Color.Black;
        //            pnl_state.Dock = DockStyle.Fill;
        //        }
        //    }
        //}        

        /*
         * 객체 설정
         */
        public string machineName
        {
            get {
                return lb_MC.Text;
            }
            set {
                lb_MC.Text = value;
            }
        }

        public string itemName {
            get {
                return lb_item.Text;
            }
            set {
                lb_item.Text = value;
            }
        }

        public string prodQtySetting
        {
            get {
                return tx_qty.Text;
            }
            set {
                tx_qty.Text = value;
            }
        }
        
        /*
         * 설비 상태 설정
         */
        public string mcState
        {
            set {
                switch (value)
                {
                    case "OFF":
                        this.BackgroundImage = Properties.Resources.offbg;
                        break;

                    case "RUN":
                        this.BackgroundImage = Properties.Resources.runbg;
                        break;

                    case "STOP":
                        this.BackgroundImage = Properties.Resources.stopbg;
                        break;                                           
                }
            }
        }

        /*
         * 안돈 색상 변경
         */
         public string andonSetting
        {
            set {
                switch (value)
                {
                    case "QC":
                        pic_andon.Image = Properties.Resources.qcimg;
                        break;
                    case "PROD":
                        pic_andon.Image = Properties.Resources.prodimg;
                        break;
                    case "RUN":
                        pic_andon.Image = null;
                        break;
                }
            }
        }
    }
}
