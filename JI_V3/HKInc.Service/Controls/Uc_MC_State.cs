using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Utils.Class;

namespace HKInc.Service.Controls
{
    public partial class Uc_MC_State : UserControl
    {
        public Uc_MC_State()
        {
            InitializeComponent();
        }
        public string lb_mc
        {
            get { return lb_MC.Text; }
            set { lb_MC.Text = value; }
        }
        public Image pic_mcimg
        {
            get { return pic_mc.Image; }
            set {
                if (value != null)
                {
                    pic_mc.Image = value;
                }
                else {
                    pic_mc.Image = Properties.Resources.mc;
                }
            }
        }
        public string lb_itemName
        {
            get { return lb_item.Text; }
            set { lb_item.Text = value; }
        }
        public int qty
        {
            get { return tx_qty.EditValue.GetIntNullToZero(); }
            set { tx_qty.EditValue = value; }
        }
        public string mcstate
        {
            //get { return pic_state.Image; }
            set {

                switch (value)
                {
                    case "OFF":
                        pic_state.Image = Properties.Resources.bar_black;
                        break;
                    case "RUN":
                        pic_state.Image = Properties.Resources.bar_blue;
                        break;
                    case "STOP":
                        pic_state.Image = Properties.Resources.bar_red;
                        break;
                    case "ON":
                        pic_state.Image = Properties.Resources.bar_blue;
                        break;
                }
                  }
        }
        public string pic_setandon
        {
           // get { return pic_state.Image; }
            set
            {
                switch (value)
                {
                    case "QC":
                        pic_andon.Image = Properties.Resources.bt_3;
                        break;
                    case "PROD":
                        pic_andon.Image = Properties.Resources.bt_2;
                        break;
                    case "NoCall":
                        pic_andon.Image = Properties.Resources.bt_4;

                        break;
                    case "RUN":
                        pic_andon.Image = Properties.Resources.bt_1;
                        break;
                   
                }
            }
        }




    }
}
