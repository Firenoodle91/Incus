using HKInc.Utils.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Ui.View.View.POP
{
    public partial class XFPOP_SELECT : Form
    {
        public XFPOP_SELECT()
        {
            InitializeComponent();
            btn_ProductionPop.Click += Btn_ProductionPop_Click;
            btn_ReworkPop.Click += Btn_ReworkPop_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
        }

        private void Btn_ProductionPop_Click(object sender, EventArgs e)
        {
            GlobalVariable.KeyPad = false;
            this.Close();
            //var pop = new Ui.View.View.POP.XFPOP1000();
            // 20210603 오세완 차장 변경 처리
            var pop = new Ui.View.View.POP.XFPOP1000_V2();
            pop.Show();
        }

        private void Btn_ReworkPop_Click(object sender, EventArgs e)
        {
            GlobalVariable.KeyPad = false;
            this.Close();
            //var pop = new Ui.View.View.POP.XFPOP_REWORK();
            var pop = new Ui.View.View.POP.XFPOP_PLC_V2(); // 20210715 오세완 차장 plc로 변경
            pop.Show();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
