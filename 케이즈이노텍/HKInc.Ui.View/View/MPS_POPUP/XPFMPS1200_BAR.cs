using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using DevExpress.XtraEditors.Mask;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using HKInc.Service.Handler;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.MPS_POPUP
{
    public partial class XPFMPS1200_BAR : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        /// <summary>
        /// 20211217 오세완 차장 
        /// 팝업 파라미터값 저장
        /// </summary>
        private TN_MPS1200 g_mps1200;
        #endregion

        public XPFMPS1200_BAR()
        {
            InitializeComponent();
        }

        public XPFMPS1200_BAR(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("BarcodePrint");
            g_mps1200 = (TN_MPS1200)PopupParam.GetValue(PopupParameter.KeyValue);

        }

        protected override void InitCombo()
        {
            spin_PrintQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_PrintQty.Properties.Mask.EditMask = "n0";
            spin_PrintQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_PrintQty.Properties.Buttons[0].Visible = false;

            spin_PerBoxQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_PerBoxQty.Properties.Mask.EditMask = "n0";
            spin_PerBoxQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_PerBoxQty.Properties.Buttons[0].Visible = false;

            // 20220103 오세완 차장 이태식 차장 요청으로 버튼 숨김 처리
            this.SetToolbarButtonVisible(ToolbarButton.Export, false); // 내보내기
            this.SetToolbarButtonVisible(ToolbarButton.Save, false); // 저장
            this.SetToolbarButtonVisible(ToolbarButton.Refresh, false); // 조회
            this.SetToolbarButtonVisible(ToolbarButton.Confirm, false); // 확인
        }

        protected override void DataPrint()
        {
            try
            {
                int iPrintQty = spin_PrintQty.EditValue.GetIntNullToZero();
                int iPerBoxQty = spin_PerBoxQty.EditValue.GetIntNullToZero();

                WaitHandler.ShowWait();

                var vMainReprot = new REPORT.XRPACK_V2();
                if(iPrintQty <= 1)
                {
                    var vOnePage = new REPORT.XRPACK_V2(g_mps1200, iPerBoxQty);
                    vOnePage.CreateDocument();
                    vMainReprot.Pages.AddRange(vOnePage.Pages);
                }
                else
                {
                    for(int i=0; i< iPrintQty; i++)
                    {
                        var vEachPage = new REPORT.XRPACK_V2(g_mps1200, iPerBoxQty);
                        vEachPage.CreateDocument();
                        vMainReprot.Pages.AddRange(vEachPage.Pages);
                    }
                }

                vMainReprot.PrintingSystem.ShowMarginsWarning = false;
                vMainReprot.ShowPrintStatusDialog = false;
                vMainReprot.ShowPreview();

            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
                ActClose();
            }
        }
    }
}