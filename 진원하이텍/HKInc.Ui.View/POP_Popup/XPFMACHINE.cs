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
using HKInc.Service.Service;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFMACHINE : XtraForm
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        public string machine = "";
        public XPFMACHINE()
        {
            InitializeComponent();
            lupMachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            machine = lupMachine.EditValue.GetNullToEmpty();
            if (machine == "") { MessageBox.Show("설비를 선택하세요.[select machine code]"); return; }
            else {
                DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
