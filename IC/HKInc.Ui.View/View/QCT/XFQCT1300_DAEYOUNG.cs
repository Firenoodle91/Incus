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
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using HKInc.Service.Handler.EventHandler;
using HKInc.Utils.Interface.Forms;
using DevExpress.XtraReports.UI;
using System.Collections.Specialized;
using System.IO;
using HKInc.Ui.View.View.REPORT;
using DevExpress.XtraBars;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 부적합관리
    /// </summary>
    public partial class XFQCT1300_DAEYOUNG : Service.Base.ListFormTemplate
    {
        IService<VI_QCT1300_LIST> ModelService = (IService<VI_QCT1300_LIST>)ProductionFactory.GetDomainService("VI_QCT1300_LIST");
        FtpFileButtonEditClickHandler OK_ButtonPressed;
        FtpFileButtonEditClickHandler BAD_ButtonPressed;
        TN_QCT1300 newObj;
        List<TN_STD1000> StdList_OccurMoment;
        List<TN_STD1000> StdList_OccurDivision;
        List<TN_STD1000> StdList_OccurGrade;
        List<TN_STD1000> StdList_OccurQty;
        List<TN_STD1000> StdList_BadType;
        List<TN_STD1000> StdList_BadSol;
        List<TN_STD1000> StdList_MeasureNeed;
        List<TN_STD1000> StdList_SubCost;

        private bool buttonPressedFlag = false;

        public XFQCT1300_DAEYOUNG()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            btn_Add_Edit.Click += Btn_Add_Edit_Click;
            btn_Delete.Click += Btn_Delete_Click;
            btn_Save.Click += Btn_Save_Click;
            btn_Print.Click += Btn_Print_Click;
            //btn_Rework.Click += Btn_Rework_Click; //부적합공정 생성 삭제

            btn_Ok.EditValueChanged += Btn_Ok_EditValueChanged;
            btn_Ok.KeyDown += Btn_Ok_KeyDown;
            btn_Bad.EditValueChanged += Btn_Bad_EditValueChanged;
            btn_Bad.KeyDown += Btn_Bad_KeyDown;

            this.SizeChanged += XFQCT1300_SizeChanged;
            dt_OccurDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_OccurDate.DateToEdit.DateTime = DateTime.Today;

            btn_Add_Edit.Text = LabelConvert.GetLabelText("AddRow") + "(&A)";
            btn_Rework.Text = LabelConvert.GetLabelText("ReworkProcessY");



            lcPNO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;              //
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; //리워크공정 버튼 숨김

            lup_CheckId.ReadOnly = true;
            txt_BadType.ReadOnly = true;

            spin_BadReportQty.Properties.Buttons[0].Visible = false;
            spin_LossCost.Properties.Buttons[0].Visible = false;
            spin_OccurQty.Properties.Buttons[0].Visible = false;
            spin_CheckQty.Properties.Buttons[0].Visible = false;
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == DataConvert.GetCultureDataFieldName("BadName"))
            {
                var P_TYPE = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "P_TYPE").GetNullToEmpty();
                var badType = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, DataConvert.GetCultureDataFieldName("BadName")).GetNullToEmpty();
                if (P_TYPE != "사내" && P_TYPE != "고객")
                {
                    e.DisplayText = badType.GetNullToEmpty();
                }
            }
        }

        private void Btn_Ok_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                StringCollection list = Clipboard.GetFileDropList();
                if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
                {
                    using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        btn_Ok.Tag = fileData;
                        btn_Ok.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_Ok.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_Ok.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }

        private void Btn_Bad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                StringCollection list = Clipboard.GetFileDropList();
                if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
                {
                    using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        btn_Bad.Tag = fileData;
                        btn_Bad.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_Bad.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_Bad.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }

        protected override void InitCombo()
        {
            lcBadReportInfo.Enabled = false;

            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_CheckId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            
            //OK_ButtonPressed.ClearDeleteButton();
            //BAD_ButtonPressed.ClearDeleteButton();
        }

        protected override void InitGrid()
        {
            //InitCombo에 선언시 계속 추가되고 생성자에 선언시 UserRight 가 null임. UserRight는 폼 생성 후에 선언되기때문에
            OK_ButtonPressed = new FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                    , UserRight.HasEdit
                                                    , btn_Ok
                                                    , true
                                                    , MasterCodeSTR.FtpFolder_BadReport_File);
            BAD_ButtonPressed = new FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                , UserRight.HasEdit
                                                                , btn_Bad
                                                                , true
                                                                , MasterCodeSTR.FtpFolder_BadReport_File);

            //GridExControl.SetToolbarVisible(true);
            GridExControl.SetToolbarButtonVisible(false);

            GridExControl.MainGrid.AddColumn("RowIndex", LabelConvert.GetLabelText("RowIndex"), false);
            GridExControl.MainGrid.AddColumn("PNO", LabelConvert.GetLabelText("PNO"));
            GridExControl.MainGrid.AddColumn("P_TYPE", LabelConvert.GetLabelText("Division"));
            GridExControl.MainGrid.AddColumn("OccurDate", LabelConvert.GetLabelText("OccurDate"),HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ProcTeamCode", LabelConvert.GetLabelText("OccurTeam"), false);
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("DelivId"));
            GridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            //GridExControl.MainGrid.AddColumn("TN_STD1100.TN_STD1100_SRC." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("SrcItemName"), false);
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
            GridExControl.MainGrid.AddColumn("ProcessMinute", LabelConvert.GetLabelText("ProcessMinute"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("BadName"), LabelConvert.GetLabelText("BadType"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            //GridExControl.MainGrid.AddColumn("ReworkFlag", LabelConvert.GetLabelText("ReworkFlag"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.ProcTeamCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("ReworkFlag", "N");

            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("RowIndex");

            PopupDataParam.SetValue(PopupParameter.EditMode, "S");
            GridExControl.MainGrid.Clear();
            lcBadReportInfo.Enabled = false;
            SetRefreshControl();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            EditComboDataLoad();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();
            var productLotNo = tx_LotNo.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                    && (string.IsNullOrEmpty(productLotNo) ? true : p.ProductLotNo == productLotNo)
                                                                    && ((p.OccurDate >= dt_OccurDate.DateFrEdit.DateTime && p.OccurDate <= dt_OccurDate.DateToEdit.DateTime))
                                                                )
                                                                .OrderBy(p => p.OccurDate)
                                                                .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (!PopupDataParam.GetValue(PopupParameter.EditMode).GetNullToEmpty().IsNullOrEmpty() && (PopupDataParam.GetValue(PopupParameter.EditMode).GetNullToEmpty() != "S"))
            {
                DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes) 
                    Btn_Save_Click(null, null);
                else
                    PopupDataParam.SetValue(PopupParameter.EditMode, "S");
            }

            var obj = GridBindingSource.Current as VI_QCT1300_LIST;
            if (obj == null)
            {
                lcBadReportInfo.Enabled = false;
                SetRefreshControl();
                EditCombo();
            }
            else
            {
                lcBadReportInfo.Enabled = true;
                SetRefreshControl();
                SetReadOnlyControl(true);
                EditCombo();

                if (UserRight.HasEdit)
                {
                    btn_Add_Edit.Enabled = true;
                    if (obj.PNO.IsNullOrEmpty())
                    {
                        btn_Add_Edit.Text = LabelConvert.GetLabelText("AddRow") + "(&A)";
                        btn_Delete.Enabled = false;
                        btn_Save.Enabled = false;
                        btn_Print.Enabled = false;

                    }
                    else
                    {
                        btn_Add_Edit.Text = LabelConvert.GetLabelText("EditRow") + "(&E)";
                        btn_Delete.Enabled = false;
                        btn_Save.Enabled = false;
                        btn_Print.Enabled = true;

                        var TN_QCT1300 = ModelService.GetChildList<TN_QCT1300>(p => p.PNo == obj.PNO).FirstOrDefault();
                        SetSettingControl(TN_QCT1300, obj);
                    }

                    //if(obj.ReworkFlag.GetNullToEmpty().Equals("Y"))
                    //    btn_Rework.Text = LabelConvert.GetLabelText("ReworkProcessN");
                    //else
                    //    btn_Rework.Text = LabelConvert.GetLabelText("ReworkProcessY");

                    ////작업생산 불량 시에만 리워크공정 생성가능
                    //if (obj.ProcessCode != MasterCodeSTR.Process_Rework)
                    //{
                    //    if(obj.P_TYPE == "사내" || obj.P_TYPE == "최종검사")
                    //        btn_Rework.Enabled = true;
                    //    else
                    //        btn_Rework.Enabled = false;
                    //}
                    //else
                    //    btn_Rework.Enabled = false;
                }
                else
                {
                    btn_Add_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                    btn_Save.Enabled = false;
                    if (!obj.PNO.IsNullOrEmpty())
                        btn_Print.Enabled = true;
                }
            }            
        }

        protected override void GridRowDoubleClicked(){}

        private void Btn_Add_Edit_Click(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as VI_QCT1300_LIST;
            if (obj == null) return;

            try
            {
                WaitHandler.ShowWait();
                SetReadOnlyControl(false);
                if (obj.PNO.IsNullOrEmpty())
                {
                    //NEW
                    btn_Add_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                    btn_Save.Enabled = true;
                    btn_Print.Enabled = false;

                    newObj = new TN_QCT1300();
                    newObj.PNo = DbRequestHandler.GetSeqMonth("QCR");
                    newObj.PType = obj.P_TYPE;
                    newObj.PKey = obj.P_KEY;
                    newObj.ItemCode = obj.ItemCode;
                    newObj.CustomerCode = obj.CustomerCode;
                    newObj.WorkNo = obj.WorkNo;
                    newObj.ProcessCode = obj.ProcessCode;
                    newObj.ProcessSeq = obj.ProcessSeq;
                    newObj.ProductLotNo = obj.ProductLotNo;
                    newObj.BadType = obj.BadType;
                    newObj.CheckDate = DateTime.Today;

                    tx_PNO.EditValue = newObj.PNo;

                    if (!buttonPressedFlag)
                    {
                        OK_ButtonPressed.key2 = newObj.PNo;
                        BAD_ButtonPressed.key2 = newObj.PNo;
                        btn_Ok.ButtonPressed += OK_ButtonPressed.ButtonClick;
                        btn_Bad.ButtonPressed += BAD_ButtonPressed.ButtonClick;
                        buttonPressedFlag = true;
                    }
                    PopupDataParam.SetValue(PopupParameter.EditMode, "N");
                    SetSettingControl(newObj, obj);

                    lup_CheckId.EditValue = GlobalVariable.LoginId;
                }
                else
                {
                    //UPDATE
                    btn_Add_Edit.Enabled = false;
                    btn_Delete.Enabled = true;
                    btn_Save.Enabled = true;
                    btn_Print.Enabled = false;

                    var updateObj = ModelService.GetChildList<TN_QCT1300>(p => p.PNo == obj.PNO).FirstOrDefault();

                    if (!buttonPressedFlag)
                    {
                        OK_ButtonPressed.key2 = updateObj.PNo;
                        BAD_ButtonPressed.key2 = updateObj.PNo;
                        btn_Ok.ButtonPressed += OK_ButtonPressed.ButtonClick;
                        btn_Bad.ButtonPressed += BAD_ButtonPressed.ButtonClick;
                        buttonPressedFlag = true;
                    }
                    PopupDataParam.SetValue(PopupParameter.EditMode, "U");
                    SetSettingControl(updateObj, obj);
                }
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        private void SetSettingControl(TN_QCT1300 TN_QCT1300, VI_QCT1300_LIST VI_QCT1300)
        {
            if (TN_QCT1300 == null) return;
            tx_PNO.EditValue = TN_QCT1300.PNo;
            lup_CheckId.EditValue = TN_QCT1300.CheckId;
            spin_CheckQty.EditValue = TN_QCT1300.CheckQty;
            tx_OccurPosition.EditValue = TN_QCT1300.OccurLocation;
            spin_OccurQty.EditValue = TN_QCT1300.OccurQty;
            memo_BadReportMemo.EditValue = TN_QCT1300.BadContent;
            spin_BadReportQty.EditValue = TN_QCT1300.BadQty;
            memo_BadReportSituation1.EditValue = TN_QCT1300.OccurConent;
            spin_LossCost.EditValue = TN_QCT1300.LoseCost;
            memo_BadReportSolution.EditValue = TN_QCT1300.SolutionContent;
            memo_Remarks.EditValue = TN_QCT1300.Memo;

            lup_OccurMoment.EditValue = TN_QCT1300.OccurMoment;
            lup_OccurDivision.EditValue = TN_QCT1300.OccurDivision;
            dt_ReceiptDate.EditValue = TN_QCT1300.CheckDate;
            lup_OccurGrade.EditValue = TN_QCT1300.OccurGrade;
            lup_OccurQty.EditValue = TN_QCT1300.OccurQtyCode;
            //txt_BadType.EditValue = TN_QCT1300.BadType;
            txt_BadType.EditValue = VI_QCT1300.BadName;
            lup_badSolution.EditValue = TN_QCT1300.SolutionContent;
            txt_ManHour.EditValue = TN_QCT1300.ManHour;
            lup_MeasureNeeds.EditValue = TN_QCT1300.MeasureNeeds;
            dt_MeasureNeedDate.EditValue = TN_QCT1300.MeasureNeedsDate;
            dt_AnswerDate.EditValue = TN_QCT1300.MeasureAnswerDate;
            lup_SubtractionCost.EditValue = TN_QCT1300.SubtractionCost;
            txt_LossTime.EditValue = TN_QCT1300.LoseTime;
            txt_MeasureForm.EditValue = TN_QCT1300.MeasureForm;

            if (TN_QCT1300.FileUrl.IsNullOrEmpty())
            {
                var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TN_QCT1300.ItemCode).FirstOrDefault();
                var limitLastObj = itemObj.TN_STD1104List.OrderBy(p => p.Seq).LastOrDefault();
                if (limitLastObj == null || limitLastObj.FileUrl.IsNullOrEmpty())
                {
                    if (itemObj != null)
                    {
                        if (!itemObj.ProdFileUrl.IsNullOrEmpty())
                        {
                            btn_Ok.Tag = itemObj.ProdFileUrl;
                            btn_Ok.EditValue = itemObj.ProdFileName;
                        }
                    }
                    else
                    {
                        btn_Ok.Tag = TN_QCT1300.FileUrl;
                        btn_Ok.EditValue = TN_QCT1300.FileName;
                    }
                }
                else
                {
                    btn_Ok.Tag = limitLastObj.FileUrl;
                    btn_Ok.EditValue = limitLastObj.FileName;
                }
            }
            else
            {
                btn_Ok.Tag = TN_QCT1300.FileUrl;
                btn_Ok.EditValue = TN_QCT1300.FileName;
            }

            if (TN_QCT1300.FileUrl2.IsNullOrEmpty())
            {
                if (TN_QCT1300.PType == "고객")
                {
                    var TN_QCT1400 = ModelService.GetChildList<TN_QCT1400>(p => p.ClaimNo == TN_QCT1300.PKey).FirstOrDefault();
                    if (TN_QCT1400 != null && !TN_QCT1400.FileUrl.IsNullOrEmpty())
                    {
                        btn_Bad.Tag = TN_QCT1400.FileUrl;
                        btn_Bad.EditValue = TN_QCT1400.FileName;
                    }
                }
            }
            else
            {
                btn_Bad.Tag = TN_QCT1300.FileUrl2;
                btn_Bad.EditValue = TN_QCT1300.FileName2;
            }
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                SetRefreshControl();
                SetReadOnlyControl(true);

                btn_Add_Edit.Enabled = false;
                btn_Delete.Enabled = false;
                btn_Save.Enabled = true;
                btn_Print.Enabled = false;

                PopupDataParam.SetValue(PopupParameter.EditMode, "D");
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //var obj = GridBindingSource.Current as VI_QCT1300_LIST;
            //if (obj == null) return;
            var PNO = tx_PNO.EditValue.GetNullToEmpty();
            if (PNO.IsNullOrEmpty()) return;

            try
            {
                WaitHandler.ShowWait();

                btn_Add_Edit.Enabled = true;
                btn_Delete.Enabled = false;
                btn_Save.Enabled = false;
                btn_Print.Enabled = true;

                var updateObj = ModelService.GetChildList<TN_QCT1300>(p => p.PNo == PNO).FirstOrDefault();

                var checkId = lup_CheckId.EditValue.GetNullToNull();
                var checkQty = spin_CheckQty.EditValue.GetDecimalNullToNull();
                var occurPosition = tx_OccurPosition.EditValue.GetNullToNull();
                var occurQty = spin_OccurQty.EditValue.GetIntNullToNull();
                var badReportMemo = memo_BadReportMemo.EditValue.GetNullToNull();
                var badReportQty = spin_BadReportQty.EditValue.GetDecimalNullToNull();
                //var badReportSituation = memo_BadReportSituation1.EditValue.GetNullToNull();
                var lossCost = spin_LossCost.EditValue.GetDecimalNullToNull();
                var badReportSolution = memo_BadReportSolution.EditValue.GetNullToNull();
                var remarks = memo_Remarks.EditValue.GetNullToNull();

                var occurWhen = lup_OccurMoment.EditValue.GetNullToNull();
                var occurDivision = lup_OccurDivision.EditValue.GetNullToNull();
                var checkDate = dt_ReceiptDate.DateTime.GetNullToDateTime();
                var occurGrade = lup_OccurGrade.EditValue.GetNullToNull();
                var occurQtyCode = lup_OccurQty.EditValue.GetNullToNull();
                var badSolution = lup_badSolution.EditValue.GetNullToNull();
                var manHour = txt_ManHour.EditValue.GetNullToNull();
                var measureNeed = lup_MeasureNeeds.EditValue.GetNullToNull();
                var measureNeedDate = dt_MeasureNeedDate.DateTime.GetNullToDateTime();
                var answerDate = dt_AnswerDate.DateTime.GetNullToDateTime();
                var loseTime = txt_LossTime.EditValue.GetNullToNull();
                var subtractionCost = lup_SubtractionCost.EditValue.GetNullToNull();
                var measureForm = txt_MeasureForm.EditValue.GetNullToNull();

                //var fileName = btn_Ok.Tag.GetNullToEmpty().Contains(MasterCodeSTR.FtpFolder_ProdImage) ? null : btn_Ok.EditValue.GetNullToNull();
                //var fileUrl =  btn_Ok.Tag.GetNullToEmpty().Contains(MasterCodeSTR.FtpFolder_ProdImage) ? null : btn_Ok.Tag.GetNullToNull();
                var fileName = btn_Ok.Tag.GetNullToEmpty().Contains(MasterCodeSTR.FtpFolder_BadReport_File) ? null : btn_Ok.EditValue.GetNullToNull();
                var fielUrl = btn_Ok.Tag.GetNullToEmpty().Contains(MasterCodeSTR.FtpFolder_BadReport_File) ? null : btn_Ok.Tag.GetNullToNull();
                var fileName2 = btn_Bad.EditValue.GetNullToNull();
                var fileUrl2 = btn_Bad.Tag.GetNullToNull();



                if (PopupDataParam.GetValue(PopupParameter.EditMode).GetNullToEmpty() == "N")
                {
                    newObj.CheckId = checkId;
                    newObj.CheckQty = checkQty;
                    newObj.OccurLocation = occurPosition;
                    newObj.OccurQty = occurQty;
                    newObj.BadContent = badReportMemo;
                    newObj.BadQty = badReportQty;
                    //newObj.OccurConent = badReportSituation;
                    newObj.LoseCost = lossCost;
                    //newObj.SolutionContent = badReportSolution;
                    newObj.Memo = remarks;
                    newObj.OccurConent = badReportSolution;

                    newObj.OccurMoment = occurWhen;
                    newObj.OccurDivision = occurDivision;
                    newObj.CheckDate = checkDate;
                    newObj.OccurGrade = occurGrade;
                    newObj.OccurQtyCode = occurQtyCode;
                    newObj.SolutionContent = badSolution;
                    newObj.ManHour = manHour;
                    newObj.MeasureNeeds = measureNeed;
                    newObj.MeasureNeedsDate = measureNeedDate;
                    newObj.MeasureAnswerDate = answerDate;
                    newObj.LoseTime = loseTime;
                    newObj.SubtractionCost = subtractionCost;
                    newObj.MeasureForm = measureForm;

                    //newObj.FileName = fileName;
                    //newObj.FileUrl = fileUrl;
                    //newObj.FileName2 = fileName2;
                    //newObj.FileUrl2 = fileUrl2;

                    //파일 CHECK
                    if (btn_Ok.EditValue.IsNullOrEmpty())
                    {
                        newObj.FileUrl = null;
                        newObj.FileName = null;
                    } 
                    else
                    {
                        string[] filename = btn_Ok.EditValue.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = newObj.PNo + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;

                            FileHandler.UploadFTP(btn_Ok.EditValue.ToString(), realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Ok.EditValue = realFileName;
                            btn_Ok.Tag = null;
                            newObj.FileName = realFileName;
                            newObj.FileUrl = ftpFileUrl;
                        }
                        else if (newObj.FileUrl == "Clipboard_Image")
                        {
                            var realFileName = newObj.PNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;
                            var localImage = btn_Ok.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Ok.EditValue = realFileName;
                            btn_Ok.Tag = null;
                            newObj.FileName = realFileName;
                            newObj.FileUrl = ftpFileUrl;
                        }
                    }

                    //파일 CHECK
                    if (btn_Bad.EditValue.IsNullOrEmpty())
                    {
                        newObj.FileName2 = null;
                        newObj.FileUrl2 = null;
                    }
                    else
                    {
                        string[] filename = btn_Bad.EditValue.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = newObj.PNo + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;

                            FileHandler.UploadFTP(btn_Bad.EditValue.ToString(), realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Bad.EditValue = realFileName;
                            btn_Bad.Tag = null;
                            newObj.FileName2 = realFileName;
                            newObj.FileUrl2 = ftpFileUrl;
                        }
                        else if (newObj.FileUrl2 == "Clipboard_Image")
                        {
                            var realFileName = newObj.PNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;
                            var localImage = btn_Bad.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Bad.EditValue = realFileName;
                            btn_Bad.Tag = null;
                            newObj.FileName2 = realFileName;
                            newObj.FileUrl2 = ftpFileUrl;
                        }
                    }

                    ModelService.InsertChild(newObj);
                    ModelService.Save();
                }
                else if (PopupDataParam.GetValue(PopupParameter.EditMode).GetNullToEmpty() == "U")
                {
                    updateObj.CheckId = checkId;
                    updateObj.CheckQty = checkQty;
                    updateObj.OccurLocation = occurPosition;
                    updateObj.OccurQty = occurQty;
                    updateObj.BadContent = badReportMemo;
                    updateObj.BadQty = badReportQty;
                    //updateObj.OccurConent = badReportSituation;
                    updateObj.LoseCost = lossCost;
                    //updateObj.SolutionContent = badReportSolution;
                    updateObj.Memo = remarks;
                    updateObj.OccurConent = badReportSolution;

                    updateObj.OccurMoment = occurWhen;
                    updateObj.OccurDivision = occurDivision;
                    updateObj.CheckDate = checkDate;
                    updateObj.OccurGrade = occurGrade;
                    updateObj.OccurQtyCode = occurQtyCode;
                    updateObj.SolutionContent = badSolution;
                    updateObj.ManHour = manHour;
                    updateObj.MeasureNeeds = measureNeed;
                    updateObj.MeasureNeedsDate = measureNeedDate;
                    updateObj.MeasureAnswerDate = answerDate;
                    updateObj.LoseTime = loseTime;
                    updateObj.SubtractionCost = subtractionCost;
                    updateObj.MeasureForm = measureForm;

                    //updateObj.FileName = fileName;
                    //updateObj.FileUrl = fileUrl;
                    //updateObj.FileName2 = fileName2;
                    //updateObj.FileUrl2 = fileUrl2;

                    //파일 CHECK
                    if (btn_Ok.EditValue.IsNullOrEmpty())
                    {
                        updateObj.FileName = null;
                        updateObj.FileUrl = null;
                    }
                    else
                    {
                        string[] filename = btn_Ok.EditValue.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = updateObj.PNo + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;

                            FileHandler.UploadFTP(btn_Ok.EditValue.ToString(), realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Ok.EditValue = realFileName;
                            btn_Ok.Tag = null;
                            updateObj.FileName = realFileName;
                            updateObj.FileUrl = ftpFileUrl;
                        }
                        else if (updateObj.FileUrl == "Clipboard_Image")
                        //else if (newObj.FileUrl == "Clipboard_Image")
                        {
                            //var realFileName = newObj.PNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            var realFileName = updateObj.PNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;
                            var localImage = btn_Ok.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Ok.EditValue = realFileName;
                            btn_Ok.Tag = null;
                            updateObj.FileName = realFileName;
                            updateObj.FileUrl = ftpFileUrl;
                            //newObj.FileName = realFileName;
                            //newObj.FileUrl = ftpFileUrl;
                        }
                    }

                    //파일 CHECK
                    if (btn_Bad.EditValue.IsNullOrEmpty())
                    {
                        updateObj.FileName2 = null;
                        updateObj.FileUrl2 = null;
                    }
                    else
                    {
                        string[] filename = btn_Bad.EditValue.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = updateObj.PNo + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;

                            FileHandler.UploadFTP(btn_Bad.EditValue.ToString(), realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Bad.EditValue = realFileName;
                            btn_Bad.Tag = null;
                            updateObj.FileName2 = realFileName;
                            updateObj.FileUrl2 = ftpFileUrl;
                        }
                        else if (updateObj.FileUrl2 == "Clipboard_Image")
                        //else if (newObj.FileUrl2 == "Clipboard_Image")
                        {
                            //var realFileName = newObj.PNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            var realFileName = updateObj.PNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_BadReport_File + "/" + realFileName;
                            var localImage = btn_Bad.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_BadReport_File + "/");

                            btn_Bad.EditValue = realFileName;
                            btn_Bad.Tag = null;
                            updateObj.FileName2 = realFileName;
                            updateObj.FileUrl2 = ftpFileUrl;
                            //newObj.FileName2 = realFileName;
                            //newObj.FileUrl2 = ftpFileUrl;
                        }
                    }

                    updateObj.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(updateObj);
                    ModelService.Save();
                }
                else if (PopupDataParam.GetValue(PopupParameter.EditMode).GetNullToEmpty() == "D")
                {                    
                    ModelService.RemoveChild(updateObj);
                    ModelService.Save();
                }
            }
            finally
            {
                WaitHandler.CloseWait();
                PopupDataParam.SetValue(PopupParameter.EditMode, "S");
                ActRefresh();
            }
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            //var obj = GridBindingSource.Current as VI_QCT1300_LIST;
            //if (obj == null) return;
            //if (obj.PNO.IsNullOrEmpty()) return;

            //var TN_QCT1300 = ModelService.GetChildList<TN_QCT1300>(p => p.PNo == obj.PNO).FirstOrDefault();
            //if (TN_QCT1300 == null) return;

            //try
            //{
            //    WaitHandler.ShowWait();

            //    XRQCT1301_DATA reportObj = new XRQCT1301_DATA(lup_OccurMoment.Text, lup_OccurDivision.Text, lup_OccurGrade.Text, lup_badSolution.Text
            //        , lup_MeasureNeeds.Text, lup_SubtractionCost.Text, obj.OccurDate, obj.BadName, lup_OccurQty.Text);

            //    var report = new XRQCT1301(TN_QCT1300, reportObj);
            //    report.CreateDocument();
            //    report.PrintingSystem.ShowMarginsWarning = false;
            //    report.ShowPrintStatusDialog = false;
            //    report.ShowPreview();
            //}
            //finally { WaitHandler.CloseWait(); }
        }

        private void Btn_Ok_EditValueChanged(object sender, EventArgs e)
        {
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_Ok.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_Ok.EditValue = localfileData;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_Ok.EditValue = localImage;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + button.Tag.GetNullToEmpty());
                        pic_Ok.EditValue = fileData;
                    }
                }
            }
        }

        private void Btn_Bad_EditValueChanged(object sender, EventArgs e)
        {
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_Bad.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_Bad.EditValue = localfileData;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_Bad.EditValue = localImage;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + button.Tag.GetNullToEmpty());
                        pic_Bad.EditValue = fileData;
                    }
                }
            }
        }

        private void SetRefreshControl()
        {
            //tx_PNO.EditValue = null;
            newObj = null;
            lup_CheckId.EditValue = null;
            spin_CheckQty.EditValue = null;
            tx_OccurPosition.EditValue = null;
            spin_OccurQty.EditValue = null;
            memo_BadReportMemo.EditValue = null;
            spin_BadReportQty.EditValue = null;
            memo_BadReportSituation1.EditValue = null;
            spin_LossCost.EditValue = null;
            memo_BadReportSolution.EditValue = null;
            memo_Remarks.EditValue = null;
            btn_Ok.EditValue = null;
            btn_Bad.EditValue = null;

            lup_OccurMoment.EditValue = null;         //발생시기
            lup_OccurDivision.EditValue = null;      //발생구분
            dt_ReceiptDate.EditValue = null;         //접수일
            lup_OccurGrade.EditValue = null;         //등급
            lup_OccurQty.EditValue = null;          //발생횟수
            txt_BadType.EditValue = null;          //불량유형
            lup_badSolution.EditValue = null;        //불량품처리
            txt_ManHour.EditValue = null;            //임율
            lup_MeasureNeeds.EditValue = null;       //대책요구
            dt_MeasureNeedDate.EditValue = null;     //대책요구일
            dt_AnswerDate.EditValue = null;          //회신일
            lup_SubtractionCost.EditValue = null;    //비용공제
            txt_LossTime.EditValue = null;           //손실시간
            txt_MeasureForm.EditValue = null;        //대책서

            if (buttonPressedFlag)
            {
                btn_Ok.ButtonPressed -= OK_ButtonPressed.ButtonClick;
                btn_Bad.ButtonPressed -= BAD_ButtonPressed.ButtonClick;
                buttonPressedFlag = false;
            }
        }

        private void SetReadOnlyControl(bool flag)
        {
            //lup_CheckId.ReadOnly = flag;
            spin_CheckQty.ReadOnly = flag;
            tx_OccurPosition.ReadOnly = flag;
            spin_OccurQty.ReadOnly = flag;
            memo_BadReportMemo.ReadOnly = flag;
            spin_BadReportQty.ReadOnly = flag;
            memo_BadReportSituation1.ReadOnly = flag;
            spin_LossCost.ReadOnly = flag;
            memo_BadReportSolution.ReadOnly = flag;
            memo_Remarks.ReadOnly = flag;
            pic_Bad.ReadOnly = flag;
            pic_Ok.ReadOnly = flag;
            btn_Ok.ReadOnly = flag;
            btn_Bad.ReadOnly = flag;

            lup_OccurMoment.ReadOnly = flag;          //발생시기
            lup_OccurDivision.ReadOnly = flag;      //발생구분
            dt_ReceiptDate.ReadOnly = flag;         //접수일
            lup_OccurGrade.ReadOnly = flag;         //등급
            lup_OccurQty.ReadOnly = flag;           //발생횟수
            //txt_BadType.ReadOnly = flag;          //불량유형
            lup_badSolution.ReadOnly = flag;        //불량품처리
            txt_ManHour.ReadOnly = flag;            //임율
            lup_MeasureNeeds.ReadOnly = flag;       //대책요구
            dt_MeasureNeedDate.ReadOnly = flag;     //대책요구일
            dt_AnswerDate.ReadOnly = flag;          //회신일
            lup_SubtractionCost.ReadOnly = flag;    //비용공제
            txt_LossTime.ReadOnly = flag;           //손실시간
            txt_MeasureForm.ReadOnly = flag;        //대책서
        }

        private void XFQCT1300_SizeChanged(object sender, EventArgs e)
        {
            SetToolbarButtonVisible(false);
            //if (this.MdiParent != null)
            //    ((IToolBar)this.MdiParent).SetToolbarButtonVisible(false);

            if (UserRight != null)
            {
                SetToolbarButtonVisible(ToolbarButton.Refresh, UserRight.HasSelect);
                //if (this.MdiParent != null)
                //    ((IToolBar)this.MdiParent).SetToolbarButtonVisible(ToolbarButton.Refresh, UserRight.HasSelect);
            }

            SetToolbarButtonVisible(ToolbarButton.Close, true);
            //if (this.MdiParent != null)
            //    ((IToolBar)this.MdiParent).SetToolbarButtonVisible(ToolbarButton.Close, true);
            //else
                ((IToolBar)this).SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        private void EditComboDataLoad()
        {
            StdList_OccurMoment = new List<TN_STD1000>();
            StdList_OccurDivision = new List<TN_STD1000>();
            StdList_OccurGrade = new List<TN_STD1000>();
            StdList_OccurQty = new List<TN_STD1000>();
            //StdList_BadType = new List<TN_STD1000>();
            StdList_BadSol = new List<TN_STD1000>();
            StdList_MeasureNeed = new List<TN_STD1000>();
            StdList_SubCost = new List<TN_STD1000>();

            StdList_OccurMoment = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.OccurMoment).ToList();
            StdList_OccurDivision = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.OccurDivision).ToList();
            StdList_OccurGrade = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.BadReportGrade).ToList();
            StdList_OccurQty = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.OccurQty).ToList();
            //StdList_BadType = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.ClaimType).ToList();
            StdList_BadSol = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.BadHandling).ToList();
            StdList_MeasureNeed = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.MeasureRequest).ToList();
            StdList_SubCost = ModelService.GetChildList<TN_STD1000>(x => x.UseYN == "Y" && x.CodeVal != null && x.CodeMain == MasterCodeSTR.DeductionCost).ToList();
        }

        private void EditCombo()
        {
            lup_OccurMoment.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_OccurMoment);//발생시기
            lup_OccurDivision.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_OccurDivision);//발생구분
            lup_OccurGrade.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_OccurGrade);//등급
            lup_OccurQty.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_OccurQty);//재발횟수
            //txt_BadType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_BadType);//불량유형
            lup_badSolution.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_BadSol);//불량처리
            lup_MeasureNeeds.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_MeasureNeed);//대책요구
            lup_SubtractionCost.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), StdList_SubCost);//비용공제
        }
    }

    /// <summary>
    /// 부적합 출력물 보내기전 데이터 가공
    /// </summary>
    public class XRQCT1301_DATA
    {
        public string OccurMoment { get; set; }
        public string OccurDivision { get; set; }
        public string OccurGrade { get; set; }
        public string SolutionContent { get; set; }
        public string MeasureNeeds { get; set; }
        public string SubtractionCost { get; set; }
        public string OccurDate { get; set; }
        public string BadType { get; set; }
        public string OccurQtyStr { get; set; }

        public XRQCT1301_DATA(string moment, string division, string grade, string solution, string need, string cost, DateTime OccurDate, string badtype, string occurQty)
        {
            this.OccurMoment = moment;
            this.OccurDivision = division;
            this.OccurGrade = grade;
            this.SolutionContent = solution;
            this.MeasureNeeds = need;
            this.SubtractionCost = cost;
            this.OccurDate = OccurDate.ToShortDateString();
            this.BadType = badtype;
            this.OccurQtyStr = occurQty;
        }

        public XRQCT1301_DATA(DateTime? dt)
        {
            dt?.ToString("yyyy-MM-dd");
        }
    }
}