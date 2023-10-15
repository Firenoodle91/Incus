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
    public partial class Uc_MC_State2 : UserControl
    {
        public Uc_MC_State2()
        {
            InitializeComponent();
        }


        public string pnl_stateSetting
        {
            get { return pnl_state.BackColor.ToString(); }
            set {
                if(value != null)
                {
                    pnl_state.BackColor = Color.Black;
                    pnl_state.Dock = DockStyle.Fill;
                }
                else
                {
                    pnl_state.BackColor = Color.Black;
                    pnl_state.Dock = DockStyle.Fill;
                }
            }
        }        

        /*
         * 객체 설정
         */
        public string lbl_machineNameSetting
        {
            get {
                return lbl_machine.Text;
            }
            set {
                lbl_machine.Text = value;
            }
        }

        public string lbl_itemNameSetting {
            get {
                return lbl_itemName.Text;
            }
            set {
                lbl_itemName.Text = value;
            }
        }

        public string lbl_prodQtySetting
        {
            get {
                return lbl_prodQty.Text;
            }
            set {
                lbl_prodQty.Text = value;
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
                        pnl_state.BackColor = Color.Black;
                        break;

                    case "RUN":
                        pnl_state.BackColor = Color.Lime;
                        break;

                    case "STOP":
                        pnl_state.BackColor = Color.Red;
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
                        lbl_Qual.Appearance.ForeColor = Color.Yellow;
                        break;
                    case "PROD":
                        lbl_Qual.Appearance.ForeColor = Color.Red;
                        break;
                }
            }
        }
    }
}
