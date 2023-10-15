using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.POP_Popup
{
    /// <summary>
    /// POP 작업종료 팝업창
    /// </summary>
    public partial class XPFJOBEND_ENG : XtraForm
    {
        public  string lstatus;

        public XPFJOBEND_ENG()
        {
            InitializeComponent();
        }
       
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            lstatus = "qtytostop";
            this.Close();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            lstatus = "exit";
            this.Close();
        }

        private void btn_qtytoend_Click(object sender, EventArgs e)
        {

            lstatus = "qtytoend";
            this.Close();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Do you want to pause without registering your performance?", "Notice", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
            {
                lstatus = "stop";
                this.Close();
            }
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Do you want to finish the work without registering the results?", "Notice", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
            {
                lstatus = "end";
                this.Close();
            }
        }
    }
    
}
