using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Ui.View
{
    public partial class FWORKTIME : Form
    {
        public string returnval;
        public string returnval1;
        public FWORKTIME()
        {
            InitializeComponent();
        }
        public FWORKTIME(string s)
        {
            InitializeComponent();
            layoutControlItem1.Text = "время";
            layoutControlItem2.Text = "минут";
            simpleButton5.Text = "совершить";
            simpleButton6.Text = "отменен";
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (cb_time.SelectedIndex == -1) { cb_time.SelectedIndex = 0; }
            else { cb_time.SelectedIndex++; }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (cb_time.SelectedIndex == -1) { cb_time.SelectedIndex = 0; }
            else { cb_time.SelectedIndex--; }
          
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cb_min.SelectedIndex == -1) { cb_min.SelectedIndex = 0; }
            else { cb_min.SelectedIndex++; }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (cb_min.SelectedIndex == -1) { cb_min.SelectedIndex = 0; }
            else { cb_min.SelectedIndex--; }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

            int time = 0;
            int min = 0;
            string stime = "00";
            string smin = "00";
            try { time= Convert.ToInt32(cb_time.SelectedItem.ToString()) * 60;
                stime = cb_time.SelectedItem.ToString();
            }
            catch { time = 0; stime = "00"; }
            try { min = Convert.ToInt32(cb_min.SelectedItem.ToString());
                smin = cb_min.SelectedItem.ToString();
            }
            catch { min = 0; smin = "00"; }
            returnval = Convert.ToString(time+min);
            returnval1 = stime + ":" + smin;
            this.Close();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
