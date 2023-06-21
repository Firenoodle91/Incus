using HKInc.Service.Factory;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Interface.Helper;
using System.Data.SqlClient;
using HKInc.Service.Forms;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.POP_POPUP
{
    public partial class XFPRSSET_RUS : Form
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        string aa = "C:\\jimes\\Serial.ini";
        string ab = "C:\\jimes";
        iniFile ini;
        public XFPRSSET_RUS()
        {
            InitializeComponent();
            ini = new iniFile(ab, aa);
            List<string> com = new List<string>(SerialPort.GetPortNames());
            if (com.Count >= 1)
            {
                cb_combo.Properties.Items.AddRange(com);
                cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);

                cb_combo.SelectedIndex = com.Count - 1;
            }
            cb_mc.SetDefault(false, "MachineMCode", "MachineName", ModelService.GetList(P => P.UseFlag == "Y"));
            cb_process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process).Where(p => p.UseYN == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            List<TN_STD1100> tempArr = ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList();
            cb_item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), tempArr, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            try
            {
                cb_combo.EditValue = ini.IniReadValue("Serial", "port", aa);
                cb_mc.EditValue = ini.IniReadValue("Serial", "mc", aa);
                cb_process.EditValue = ini.IniReadValue("Serial", "process", aa);
                cb_item.EditValue = ini.IniReadValue("Serial", "item", aa);
                tx_time.Text = ini.IniReadValue("Serial", "TT", aa);
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

          ini.IniWriteValue("Serial", "port", cb_combo.SelectedItem.GetNullToEmpty(), aa);
            ini.IniWriteValue("Serial", "mc", cb_mc.EditValue == null ? "" : cb_mc.EditValue.ToString(), aa);
            ini.IniWriteValue("Serial", "process", cb_process.EditValue == null ? "" : cb_process.EditValue.ToString(), aa);
            ini.IniWriteValue("Serial", "item", cb_item.EditValue == null ? "" : cb_item.EditValue.ToString(), aa);
            ini.IniWriteValue("Serial", "TT", tx_time.Text.GetNullToEmpty(), aa);
            HKInc.Service.Controls.HKRS232_RUS.lsPort = cb_combo.SelectedItem.GetNullToEmpty();
            HKInc.Service.Controls.HKRS232_RUS.lsmc = cb_mc.EditValue.GetNullToEmpty();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
