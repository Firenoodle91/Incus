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

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFSTOP_ENG : XtraForm
    {
        IService<TN_MPS1600> ModelService = (IService<TN_MPS1600>)ProductionFactory.GetDomainService("TN_MPS1600");
        Control[] btn;
        string machine;
        public XPFSTOP_ENG()
        {
            InitializeComponent();
        }
            public XPFSTOP_ENG(string obj)
        {
            InitializeComponent();
            //lupmachine.SetDefault(false, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
            //lupmachine.EditValue = obj;
            machine = obj;
            DataSet ds = DbRequesHandler.GetDataQury("SELECT       [MCODE]      ,[CODE_NAME1] FROM [TN_STD1000T] where CODE_MAIN='P003' and CODE_MID!='00'");
            btn = new Control[ds.Tables[0].Rows.Count];
            int j = ds.Tables[0].Rows.Count / 3+1;
            int k = 280 / j>70 ? 70 : 280 / j;

           // MessageBox.Show(j.ToString() + "_"+k.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                btn[i] = new SimpleButton();
                btn[i].Font = new Font("Tahoma", 13f, FontStyle.Bold);
                btn[i].Name = ds.Tables[0].Rows[i][0].ToString();
                btn[i].Parent = this.p_btn;
                switch (i % 3)
                {
                    case 0:
                          btn[i].Location = new Point(10, i/3*k+10);
                       
                        break;
                    case 1:
                        btn[i].Location = new Point(160, i/3 * k+10);
                     
                        break;
                    case 2:
                        btn[i].Location = new Point(310, i/3 * k+10);                   
                        break;
                }
                btn[i].Size = new Size(150, k);
                btn[i].Text = ds.Tables[0].Rows[i][1].ToString();
                btn[i].Click += new EventHandler(btn_click);



            }




        }

        private void btn_click(object sender, EventArgs e)
        {
            //      MessageBox.Show(DateTime.Now.ToString());
            Control ctl = sender as Control;
            if (ctl != null)
            {
                TN_MPS1600 obj = new TN_MPS1600()
                {
                    StopDate = DateTime.Today,
                    StopSeq = DbRequesHandler.GetRequestNumber("STOP"),
                    MachineCode = machine,
                    StopCode = ctl.Name,
                    StopStarttime = DateTime.Now


            };
            ModelService.Insert(obj);
        }
        ModelService.Save();
            DialogResult= System.Windows.Forms.DialogResult.OK;
            this.Close();
    }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
    
}
