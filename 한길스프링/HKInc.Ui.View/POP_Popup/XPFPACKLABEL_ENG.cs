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
using HKInc.Service.Handler;
using HKInc.Ui.View.REPORT;

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFPACKLABEL_ENG : XtraForm
    {
        IService<TN_MPS1402> ModelService = (IService<TN_MPS1402>)ProductionFactory.GetDomainService("TN_MPS1402");
        private TN_MPS1401 TN_MPS1401_Obj;
        private bool PopFlag;

        public XPFPACKLABEL_ENG()
        {
            InitializeComponent();
        }

        public XPFPACKLABEL_ENG(int resultQty,string lotNo, string itemCode, string itemName, TN_MPS1401 TN_MPS1401, int StdPackQty, bool popFlag = true)
        {
            InitializeComponent();
            textItemCode.EditValue = itemCode;
            textItemName.EditValue = itemName;
            textLotNo.EditValue = lotNo;
            spinResultQty.EditValue = resultQty;
            if(StdPackQty != 0)
                spin_BoxQty.EditValue = StdPackQty;
            else
                spin_BoxQty.EditValue = resultQty;

            TN_MPS1401_Obj = TN_MPS1401;
            if(TN_MPS1401_Obj == null)
            {
                MessageBoxHandler.Show("Results Added doesn't exist.", "Warning");
                Close();
            }

            PopFlag = popFlag;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            WaitHandler waitHandler = new WaitHandler();
            try
            {
                waitHandler.ShowWait();
                var ResultQty = spinResultQty.EditValue.GetDecimalNullToZero();
                var BoxQty = spin_BoxQty.EditValue.GetDecimalNullToZero();
                if (BoxQty == 0) return;

                var ItemCode = textItemCode.EditValue.GetNullToEmpty();
                var ItemName = textItemName.EditValue.GetNullToEmpty();
                var LotNo = textLotNo.EditValue.GetNullToEmpty();
                var quotient = System.Math.Truncate(ResultQty / BoxQty);
                var remains = ResultQty % BoxQty;
                
                var FirstReport = new RPACKLABLE();

                var PackLotNoCnt = DbRequesHandler.GetPackLotNoMaxSeq("HG_" + LotNo + "_");
                for (int i = 0; i < quotient; i++)
                {
                    var PackLotNo = "HG_" + LotNo + "_" + PackLotNoCnt.ToString();

                    //포장LOT 
                    var TN_MPS1402_Obj = ModelService.GetList(p => p.PackLotNo == PackLotNo).FirstOrDefault();
                    if (TN_MPS1402_Obj == null)
                    {
                        ModelService.Insert(new TN_MPS1402()
                        {
                            PackLotNo = PackLotNo,
                            WorkDate = (DateTime)TN_MPS1401_Obj.WorkDate,
                            WorkNo = TN_MPS1401_Obj.WorkNo,
                            Seq = TN_MPS1401_Obj.Seq,
                            ProcessCode = TN_MPS1401_Obj.ProcessCode,
                            PackQty = (int)BoxQty
                        });
                    }
                    else
                    {
                        TN_MPS1402_Obj.PackQty = (int)BoxQty;
                    }

                    //라벨출력
                    var prt = new PRT_OUTLABLE()
                    {
                        ItemCode = ItemCode,
                        ItemNm = ItemName,
                        Qty = BoxQty,
                        LotNo = PackLotNo
                    };
                    var report = new RPACKLABLE(prt);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                    PackLotNoCnt++;
                }
                if (remains != 0)
                {
                    var PackLotNo = "HG_" + LotNo + "_" + PackLotNoCnt.ToString();

                    //포장LOT 
                    var TN_MPS1402_Obj = ModelService.GetList(p => p.PackLotNo == PackLotNo).FirstOrDefault();
                    if (TN_MPS1402_Obj == null)
                    {
                        ModelService.Insert(new TN_MPS1402()
                        {
                            PackLotNo = PackLotNo,
                            WorkDate = (DateTime)TN_MPS1401_Obj.WorkDate,
                            WorkNo = TN_MPS1401_Obj.WorkNo,
                            Seq = TN_MPS1401_Obj.Seq,
                            ProcessCode = TN_MPS1401_Obj.ProcessCode,
                            PackQty = (int)remains
                        });
                    }
                    else
                    {
                        TN_MPS1402_Obj.PackQty = (int)BoxQty;
                    }

                    var prt = new PRT_OUTLABLE()
                    {
                        ItemCode = ItemCode,
                        ItemNm = ItemName,
                        Qty = remains,
                        LotNo = PackLotNo
                    };
                    var report = new RPACKLABLE(prt);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                }
                //FirstReport.ShowPrintStatusDialog = false;
                //FirstReport.ShowPreview();
                if (PopFlag)
                {
                    FirstReport.PrintingSystem.ShowMarginsWarning = false;
                    FirstReport.ShowPrintStatusDialog = false;
                    FirstReport.Print();
                }
                else
                {
                    FirstReport.PrintingSystem.ShowMarginsWarning = false;
                    FirstReport.ShowPrintStatusDialog = false;
                    FirstReport.ShowPreview();
                }
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                ModelService.Save();
                ModelService.ReLoad();
                waitHandler.CloseWait();
            }
            waitHandler = null;
            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void spin_BoxQty_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            spin_BoxQty.EditValue = keypad.returnval;
            spin_BoxQty.BeginInvoke(new MethodInvoker(delegate {
                //txtPassword.SelectionLength = txtPassword.Text.Length;
                spin_BoxQty.SelectionStart = spin_BoxQty.Text.Length;
            }));
        }
    }
    
}
