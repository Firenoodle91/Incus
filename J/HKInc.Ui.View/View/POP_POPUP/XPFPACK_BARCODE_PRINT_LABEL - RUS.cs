using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraEditors.Mask;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 포장라벨출력/박스라벨출력(바코드라벨)
    /// </summary>
    public partial class XPFPACK_BARCODE_PRINT_LABEL_RUS : Service.Base.PopupCallbackFormTemplate
    {
        TEMP_XFPOP_PACK TEMP_XFPOP_PACK;
        VI_MPS1800_LIST VI_MPS1800_LIST;
        string division = string.Empty; // 1 : 포장라벨출력 , 2 : 박스라벨출력
        public XPFPACK_BARCODE_PRINT_LABEL_RUS()
        {
            InitializeComponent();
        }

        public XPFPACK_BARCODE_PRINT_LABEL_RUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            division = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            if (division == "1")
            {
                this.Text = LabelConvert.GetLabelText("PackBarcodePrint");
                TEMP_XFPOP_PACK = (TEMP_XFPOP_PACK)PopupParam.GetValue(PopupParameter.KeyValue);
            }
            else if (division == "2")
            {
                this.Text = LabelConvert.GetLabelText("BoxBarcodePrint");
                TEMP_XFPOP_PACK = (TEMP_XFPOP_PACK)PopupParam.GetValue(PopupParameter.KeyValue);
            }
            else
            {
                this.Text = LabelConvert.GetLabelText("BarcodePrint");
                VI_MPS1800_LIST = (VI_MPS1800_LIST)PopupParam.GetValue(PopupParameter.KeyValue);
            }
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Print, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

            spin_PrintQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_PrintQty.Properties.Mask.EditMask = "n0";
            spin_PrintQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_PrintQty.Properties.Buttons[0].Visible = false;

            spin_PerBoxQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_PerBoxQty.Properties.Mask.EditMask = "n0";
            spin_PerBoxQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_PerBoxQty.Properties.Buttons[0].Visible = false;

            if (division == "3")
            {
                spin_PerBoxQty.EditValue = VI_MPS1800_LIST.TN_STD1100.PackQty.GetDecimalNullToZero();

                var TN_STD1100 = VI_MPS1800_LIST.TN_STD1100;
            }
            else
            {
                var itemObj = ModelService.GetList(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
                spin_PerBoxQty.EditValue = itemObj == null ? 0 : itemObj.PackQty.GetDecimalNullToZero();

                var TN_ITEM_MOVE = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TEMP_XFPOP_PACK.ItemMoveNo).OrderBy(p => p.ProcessSeq).FirstOrDefault();
                
                var TN_STD1100 = itemObj;
            }
        }

        protected override void DataPrint()
        {
            try
            {
                var printQty = spin_PrintQty.EditValue.GetIntNullToZero();
                var perBoxQty = spin_PerBoxQty.EditValue.GetIntNullToZero();
                
                WaitHandler.ShowWait();

                if (TEMP_XFPOP_PACK == null || printQty == 0) return;

                //var mainReport = new REPORT.XRPACK();
                // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                var mainReport = new REPORT.XRPACK();
                if (printQty <= 1)
                {
                    //var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, DateTime.Today.ToShortDateString());
                    // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                    var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, DateTime.Today.ToShortDateString());
                    report.CreateDocument();
                    mainReport.Pages.AddRange(report.Pages);
                }
                else
                {
                    for (int i = 0; i < printQty; i++)
                    {
                        //var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, DateTime.Today.ToShortDateString());
                        // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                        var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, DateTime.Today.ToShortDateString());
                        report.CreateDocument();
                        mainReport.Pages.AddRange(report.Pages);
                    }
                }
                mainReport.PrintingSystem.ShowMarginsWarning = false;
                mainReport.ShowPrintStatusDialog = false;
                mainReport.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                WaitHandler.CloseWait();
                ActClose();
            }
        }
    }
}
