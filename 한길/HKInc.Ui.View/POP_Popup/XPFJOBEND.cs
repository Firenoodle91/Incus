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
    public partial class XPFJOBEND : XtraForm
    {
        public  string lstatus;

        public XPFJOBEND()
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
            DialogResult = MessageBox.Show( "실적등록없이 일시정지하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
            {
                lstatus = "stop";
                this.Close();
            }
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("실적등록없이 작업종료 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
            {
                lstatus = "end";
                this.Close();
            }
        }
    }
    
}
