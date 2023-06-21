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
    /// 포장라벨출력/박스라벨출력
    /// </summary>
    public partial class XPFPACK_BARCODE_PRINT_RUS : Service.Base.PopupCallbackFormTemplate
    {
        TEMP_XFPOP_PACK TEMP_XFPOP_PACK;
        VI_MPS1800_LIST VI_MPS1800_LIST;
        string division = string.Empty; // 1 : 포장라벨출력 , 2 : 박스라벨출력
        public XPFPACK_BARCODE_PRINT_RUS()
        {
            InitializeComponent();
        }

        public XPFPACK_BARCODE_PRINT_RUS(PopupDataParam parameter, PopupCallback callback) : this()
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

            pic_ProdImage.DoubleClick += Pic_DoubleClick;
            pic_PackPlasticImage.DoubleClick += Pic_DoubleClick;
            pic_OutBoxImage.DoubleClick += Pic_DoubleClick;
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
                dt_PrintDate.DateTime = DateTime.Today;

                var TN_STD1100 = VI_MPS1800_LIST.TN_STD1100;
                if (!TN_STD1100.ProdFileUrl.IsNullOrEmpty())
                {
                    pic_ProdImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.ProdFileUrl);
                }
                if (TN_STD1100.TN_STD1100_PACK_PLASTIC != null)
                {
                    if (!TN_STD1100.TN_STD1100_PACK_PLASTIC.ProdFileUrl.IsNullOrEmpty())
                    {
                        pic_PackPlasticImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.TN_STD1100_PACK_PLASTIC.ProdFileUrl);
                    }
                }
                if (TN_STD1100.TN_STD1100_OUT_BOX != null)
                {
                    if (!TN_STD1100.TN_STD1100_OUT_BOX.ProdFileUrl.IsNullOrEmpty())
                    {
                        pic_OutBoxImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.TN_STD1100_OUT_BOX.ProdFileUrl);
                    }
                }
            }
            else
            {
                var itemObj = ModelService.GetList(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
                spin_PerBoxQty.EditValue = itemObj == null ? 0 : itemObj.PackQty.GetDecimalNullToZero();

                var TN_ITEM_MOVE = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == TEMP_XFPOP_PACK.ItemMoveNo).OrderBy(p => p.ProcessSeq).FirstOrDefault();
                if (TN_ITEM_MOVE == null)
                    dt_PrintDate.DateTime = DateTime.Today;
                else
                {
                    dt_PrintDate.DateTime = TN_ITEM_MOVE.CreateTime.Date;
                }

                var TN_STD1100 = itemObj;
                if (!TN_STD1100.ProdFileUrl.IsNullOrEmpty())
                {
                    pic_ProdImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.ProdFileUrl);
                }
                if (TN_STD1100.TN_STD1100_PACK_PLASTIC != null)
                {
                    if (!TN_STD1100.TN_STD1100_PACK_PLASTIC.ProdFileUrl.IsNullOrEmpty())
                    {
                        pic_PackPlasticImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.TN_STD1100_PACK_PLASTIC.ProdFileUrl);
                    }
                }
                if (TN_STD1100.TN_STD1100_OUT_BOX != null)
                {
                    if (!TN_STD1100.TN_STD1100_OUT_BOX.ProdFileUrl.IsNullOrEmpty())
                    {
                        pic_OutBoxImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1100.TN_STD1100_OUT_BOX.ProdFileUrl);
                    }
                }
            }

            cbo_PrintDivision.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_PrintDivision.Properties.Items.Add("바코드용지");
            cbo_PrintDivision.Properties.Items.Add("A4용지");
            cbo_PrintDivision.SelectedIndex = 0;
        }

        protected override void DataPrint()
        {
            try
            {
                var printQty = spin_PrintQty.EditValue.GetIntNullToZero();
                var perBoxQty = spin_PerBoxQty.EditValue.GetIntNullToZero();
                var printDate = dt_PrintDate.DateTime.ToShortDateString();

                
                WaitHandler.ShowWait();

                if (division == "3")
                {
                    if (VI_MPS1800_LIST == null || printQty == 0) return;
                    if (cbo_PrintDivision.SelectedIndex == 0)
                    {
                        //var mainReport = new REPORT.XRPACK();
                        // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                        var mainReport = new REPORT.XRPACK();
                        if (printQty <= 1)
                        {
                            //var report = new REPORT.XRPACK(VI_MPS1800_LIST, perBoxQty, printDate);
                            // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                            var report = new REPORT.XRPACK(VI_MPS1800_LIST, perBoxQty, printDate);
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                        else
                        {
                            for (int i = 0; i < printQty; i++)
                            {
                                //var report = new REPORT.XRPACK(VI_MPS1800_LIST, perBoxQty, printDate);
                                // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                                var report = new REPORT.XRPACK(VI_MPS1800_LIST, perBoxQty, printDate);
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }
                        }
                        mainReport.PrintingSystem.ShowMarginsWarning = false;
                        mainReport.ShowPrintStatusDialog = false;
                        mainReport.ShowPreview();
                    }
                    else
                    {
                        var mainReport = new REPORT.XRPACK_A4();
                        if (printQty < 11)
                        {
                            var report = new REPORT.XRPACK_A4(VI_MPS1800_LIST, perBoxQty, printDate, printQty);
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                        else
                        {
                            var valueCount = printQty / 10;
                            var modCount = printQty % 10;

                            for (int i = 1; i <= valueCount; i++)
                            {
                                var report = new REPORT.XRPACK_A4(VI_MPS1800_LIST, perBoxQty, printDate, 10);
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }

                            if (modCount != 0)
                            {
                                var report = new REPORT.XRPACK_A4(VI_MPS1800_LIST, perBoxQty, printDate, modCount);
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }
                        }
                        mainReport.PrintingSystem.ShowMarginsWarning = false;
                        mainReport.ShowPrintStatusDialog = false;
                        mainReport.ShowPreview();
                    }
                }
                else
                {
                    if (TEMP_XFPOP_PACK == null || printQty == 0) return;
                    if (cbo_PrintDivision.SelectedIndex == 0)
                    {
                        // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                        var mainReport = new REPORT.XRPACK();
                        //var mainReport = new REPORT.XRPACK();
                        if (printQty <= 1)
                        {
                            //var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, printDate);
                            // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                            var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, printDate);
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                        else
                        {
                            for (int i = 0; i < printQty; i++)
                            {
                                //var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, printDate);
                                // 20210819 오세완 차장 대영 요청으로 10*6 크기로 변경
                                var report = new REPORT.XRPACK(TEMP_XFPOP_PACK, perBoxQty, printDate);
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }
                        }
                        mainReport.PrintingSystem.ShowMarginsWarning = false;
                        mainReport.ShowPrintStatusDialog = false;
                        mainReport.ShowPreview();
                    }
                    else
                    {
                        var mainReport = new REPORT.XRPACK_A4();
                        if (printQty < 11)
                        {
                            var report = new REPORT.XRPACK_A4(TEMP_XFPOP_PACK, perBoxQty, printDate, printQty);
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                        else
                        {
                            var valueCount = printQty / 10;
                            var modCount = printQty % 10;

                            for (int i = 1; i <= valueCount; i++)
                            {
                                var report = new REPORT.XRPACK_A4(TEMP_XFPOP_PACK, perBoxQty, printDate, 10);
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }

                            if (modCount != 0)
                            {
                                var report = new REPORT.XRPACK_A4(TEMP_XFPOP_PACK, perBoxQty, printDate, modCount);
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }
                        }
                        mainReport.PrintingSystem.ShowMarginsWarning = false;
                        mainReport.ShowPrintStatusDialog = false;
                        mainReport.ShowPreview();
                    }
                }
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                WaitHandler.CloseWait();
                ActClose();
            }
        }

        private void Pic_DoubleClick(object sender, EventArgs e)
        {
            var pictureEidt = sender as PictureEdit;
            if (pictureEidt.EditValue == null) return;
            var imgForm = new POP_POPUP.XPFPOPIMG(LabelConvert.GetLabelText(pictureEidt.Name.Replace("pic_", "")), pictureEidt.EditValue);
            imgForm.ShowDialog();
        }
    }
}
