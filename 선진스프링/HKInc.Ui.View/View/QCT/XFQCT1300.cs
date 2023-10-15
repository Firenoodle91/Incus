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

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 부적합관리
    /// </summary>
    public partial class XFQCT1300 : Service.Base.ListFormTemplate
    {
        IService<VI_QCT1300_LIST> ModelService = (IService<VI_QCT1300_LIST>)ProductionFactory.GetDomainService("VI_QCT1300_LIST");
        FtpFileButtonEditClickHandler OK_ButtonPressed;
        FtpFileButtonEditClickHandler BAD_ButtonPressed;
        TN_QCT1300 newObj;

        private bool buttonPressedFlag = false;

        public XFQCT1300()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            btn_Add_Edit.Click += Btn_Add_Edit_Click;
            btn_Delete.Click += Btn_Delete_Click;
            btn_Save.Click += Btn_Save_Click;
            btn_Print.Click += Btn_Print_Click;

            btn_Ok.EditValueChanged += Btn_Ok_EditValueChanged;
            btn_Ok.KeyDown += Btn_Ok_KeyDown;
            btn_Bad.EditValueChanged += Btn_Bad_EditValueChanged;
            btn_Bad.KeyDown += Btn_Bad_KeyDown;

            this.SizeChanged += XFQCT1300_SizeChanged;
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
            btn_Add_Edit.Text = LabelConvert.GetLabelText("AddRow") + "(&A)";

            dt_OccurDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_OccurDate.DateToEdit.DateTime = DateTime.Today;

            lcBadReportInfo.Enabled = false;

            lup_OccurTeam.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
            lup_Manager.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_CustomerCode2.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_CheckId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lup_OccurPosition.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.OccurPosition));
            lup_MachineCode.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList());
            lup_QcrType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.QcrType));
            lup_BadReportSolution.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadReportSolution));

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
            //OK_ButtonPressed.ClearDeleteButton();
            //BAD_ButtonPressed.ClearDeleteButton();

            lcPNO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //lup_CheckId.ReadOnly = true;
            tx_ItemCodeName.ReadOnly = true;
            lup_CustomerCode2.ReadOnly = true;
            tx_LotNo2.ReadOnly = true;
            tx_SrcCode.ReadOnly = true;

            spin_QcrQty.Properties.Buttons[0].Visible = false;
            spin_LossCost.Properties.Buttons[0].Visible = false;
            spin_OccurQty.Properties.Buttons[0].Visible = false;
            spin_CheckQty.Properties.Buttons[0].Visible = false;
            spin_ProcessTime.Properties.Buttons[0].Visible = false;
            spin_BadQty.Properties.Buttons[0].Visible = false;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.AddColumn("RowIndex", LabelConvert.GetLabelText("RowIndex"), false);
            GridExControl.MainGrid.AddColumn("PNO", LabelConvert.GetLabelText("PNO"));
            GridExControl.MainGrid.AddColumn("P_TYPE", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("OccurDate", LabelConvert.GetLabelText("OccurDate"),HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ProcTeamCode", LabelConvert.GetLabelText("OccurTeam"));            
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("ManagerName"));
            GridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId2"));
            GridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.TN_STD1100_SRC." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("SrcItemName"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));            
            GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
            //GridExControl.MainGrid.AddColumn("ProcessMinute", LabelConvert.GetLabelText("ProcessMinute"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("BadQty", LabelConvert.GetLabelText("BadQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("BadName"), LabelConvert.GetLabelText("BadType"));
            GridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.ProcTeamCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName");

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
            }
            else
            {
                lcBadReportInfo.Enabled = true;
                SetRefreshControl();
                SetReadOnlyControl(true);

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
                        SetSettingControl(TN_QCT1300);
                    }
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
                    newObj.OccurTeam = obj.TN_STD1100.ProcTeamCode;
                    newObj.ManagerId = obj.WorkId;
                    newObj.OccurDate = obj.OccurDate;
                    newObj.CheckId = obj.CheckId;
                    newObj.LoseCost = obj.BadQty.GetIntNullToZero() * obj.TN_STD1100.Cost.GetIntNullToZero();
                    newObj.CustomerCode2 = obj.CustomerCode;
                    newObj.MachineCode = obj.MachineCode;
                    newObj.QcrContent = obj.BadName.GetNullToEmpty();
                    newObj.Spec = obj.Spec.GetNullToEmpty();
                    newObj.OccurLocation = obj.P_TYPE == "고객" ? "02" : "01";

                    newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).First();

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
                    SetSettingControl(newObj);

                    //lup_CheckId.EditValue = GlobalVariable.LoginId;
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
                    SetSettingControl(updateObj);
                }
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

        private void SetSettingControl(TN_QCT1300 TN_QCT1300)
        {
            if (TN_QCT1300 == null) return;
            tx_PNO.EditValue = TN_QCT1300.PNo;
            lup_OccurTeam.EditValue = TN_QCT1300.OccurTeam;
            lup_Manager.EditValue = TN_QCT1300.ManagerId;
            dt_OccurDate2.DateTime = TN_QCT1300.OccurDate == null ? DateTime.Today : (DateTime)TN_QCT1300.OccurDate;
            lup_CheckId.EditValue = TN_QCT1300.CheckId;
            lup_CustomerCode2.EditValue = TN_QCT1300.CustomerCode2;
            tx_ItemCodeName.EditValue = TN_QCT1300.TN_STD1100 == null ? TN_QCT1300.ItemCode : (TN_QCT1300.ItemCode + "/" + TN_QCT1300.TN_STD1100.ItemName);
            spin_QcrQty.EditValue = TN_QCT1300.QcrQty;
            spin_ProcessTime.EditValue = TN_QCT1300.ProcessTime;
            tx_SrcCode.EditValue = TN_QCT1300.TN_STD1100 == null ? string.Empty : (TN_QCT1300.TN_STD1100.SrcCode);
            tx_LotNo2.EditValue = TN_QCT1300.ProductLotNo;
            spin_CheckQty.EditValue = TN_QCT1300.CheckQty;
            spin_BadQty.EditValue = TN_QCT1300.BadQty;
            lup_OccurPosition.EditValue = TN_QCT1300.OccurLocation;
            spin_OccurQty.EditValue = TN_QCT1300.OccurQty;
            lup_MachineCode.EditValue = TN_QCT1300.MachineCode;
            spin_LossCost.EditValue = TN_QCT1300.LoseCost;
            lup_QcrType.EditValue = TN_QCT1300.QcrType;
            memo_QcrContent.EditValue = TN_QCT1300.QcrContent;
            lup_BadReportSolution.EditValue = TN_QCT1300.SolutionContent;
            tx_Spec.EditValue = TN_QCT1300.Spec;
            memo_BadReportSituation.EditValue = TN_QCT1300.OccurContent;
            memo_Remarks.EditValue = TN_QCT1300.Memo;

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

                var occurTeam = lup_OccurTeam.EditValue.GetNullToNull();
                var managerId = lup_Manager.EditValue.GetNullToNull();
                var occurDate = dt_OccurDate2.DateTime;
                var checkId = lup_CheckId.EditValue.GetNullToNull();
                var customerCode2 = lup_CustomerCode2.EditValue.GetNullToNull();
                var itemCodeName = tx_ItemCodeName.EditValue.GetNullToNull();
                var qcrQty = spin_QcrQty.EditValue.GetDecimalNullToNull();
                var processTime = spin_ProcessTime.EditValue.GetDecimalNullToNull();
                var srcCode = tx_SrcCode.EditValue.GetNullToNull();
                var lotNo = tx_LotNo2.EditValue.GetNullToNull();
                var checkQty = spin_CheckQty.EditValue.GetDecimalNullToNull();
                var badQty = spin_BadQty.EditValue.GetDecimalNullToNull();
                var occurPosition = lup_OccurPosition.EditValue.GetNullToNull();
                var occurQty = spin_OccurQty.EditValue.GetIntNullToNull();
                var machineCode = lup_MachineCode.EditValue.GetNullToNull();
                var lossCost = spin_LossCost.EditValue.GetDecimalNullToNull();
                var qcrType = lup_QcrType.EditValue.GetNullToNull();
                var qcrContent = memo_QcrContent.EditValue.GetNullToNull();
                var solutionContent = lup_BadReportSolution.EditValue.GetNullToNull();
                var spec = tx_Spec.EditValue.GetNullToNull();
                var occurContent = memo_BadReportSituation.EditValue.GetNullToNull();
                var remarks = memo_Remarks.EditValue.GetNullToNull();
                var fileName = btn_Ok.Tag.GetNullToEmpty().Contains(MasterCodeSTR.FtpFolder_ProdImage) ? null : btn_Ok.EditValue.GetNullToNull();
                var fileUrl =  btn_Ok.Tag.GetNullToEmpty().Contains(MasterCodeSTR.FtpFolder_ProdImage) ? null : btn_Ok.Tag.GetNullToNull();
                var fileName2 = btn_Bad.EditValue.GetNullToNull();
                var fileUrl2 = btn_Bad.Tag.GetNullToNull();
                               
                if (PopupDataParam.GetValue(PopupParameter.EditMode).GetNullToEmpty() == "N")
                {
                    newObj.OccurTeam = occurTeam;
                    newObj.ManagerId = managerId;
                    newObj.OccurDate = occurDate;
                    newObj.CheckId = checkId;
                    newObj.CustomerCode2 = customerCode2;
                    newObj.QcrQty = qcrQty;
                    newObj.ProcessTime = processTime;
                    newObj.CheckQty = checkQty;
                    newObj.BadQty = badQty;
                    newObj.OccurLocation = occurPosition;
                    newObj.OccurQty = occurQty;
                    newObj.MachineCode = machineCode;
                    newObj.LoseCost = lossCost;
                    newObj.QcrType = qcrType;
                    newObj.QcrContent = qcrContent;
                    newObj.SolutionContent = solutionContent;
                    newObj.Spec = spec;
                    newObj.OccurContent = occurContent;
                    newObj.Memo = remarks;

                    //파일 CHECK
                    if (btn_Ok.EditValue.IsNullOrEmpty()) newObj.FileUrl = null;
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
                    if (btn_Bad.EditValue.IsNullOrEmpty()) newObj.FileUrl2 = null;
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
                    updateObj.OccurTeam = occurTeam;
                    updateObj.ManagerId = managerId;
                    updateObj.OccurDate = occurDate;
                    updateObj.CheckId = checkId;
                    updateObj.CustomerCode2 = customerCode2;
                    updateObj.QcrQty = qcrQty;
                    updateObj.ProcessTime = processTime;
                    updateObj.CheckQty = checkQty;
                    updateObj.BadQty = badQty;
                    updateObj.OccurLocation = occurPosition;
                    updateObj.OccurQty = occurQty;
                    updateObj.MachineCode = machineCode;
                    updateObj.LoseCost = lossCost;
                    updateObj.QcrType = qcrType;
                    updateObj.QcrContent = qcrContent;
                    updateObj.SolutionContent = solutionContent;
                    updateObj.Spec = spec;
                    updateObj.OccurContent = occurContent;
                    updateObj.Memo = remarks;

                    //파일 CHECK
                    if (btn_Ok.EditValue.IsNullOrEmpty()) updateObj.FileUrl = null;
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
                    if (btn_Bad.EditValue.IsNullOrEmpty()) updateObj.FileUrl2 = null;
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
            var obj = GridBindingSource.Current as VI_QCT1300_LIST;
            if (obj == null) return;
            if (obj.PNO.IsNullOrEmpty()) return;

            var TN_QCT1300 = ModelService.GetChildList<TN_QCT1300>(p => p.PNo == obj.PNO).FirstOrDefault();
            if (TN_QCT1300 == null) return;

            try
            {
                WaitHandler.ShowWait();
                var report = new QCT_POPUP.XRQCT1300(obj, TN_QCT1300);
                report.CreateDocument();
                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            finally { WaitHandler.CloseWait(); }
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

            lup_OccurTeam.EditValue = null;
            lup_Manager.EditValue = null;
            dt_OccurDate2.EditValue = null;
            lup_CheckId.EditValue = null;
            lup_CustomerCode2.EditValue = null;
            tx_ItemCodeName.EditValue = null;
            spin_QcrQty.EditValue = null;
            spin_ProcessTime.EditValue = null;
            tx_SrcCode.EditValue = null;
            tx_LotNo2.EditValue = null;
            spin_CheckQty.EditValue = null;
            spin_BadQty.EditValue = null;
            lup_OccurPosition.EditValue = null;
            spin_OccurQty.EditValue = null;
            lup_MachineCode.EditValue = null;
            spin_LossCost.EditValue = null;
            lup_QcrType.EditValue = null;
            memo_QcrContent.EditValue = null;
            lup_BadReportSolution.EditValue = null;
            tx_Spec.EditValue = null;
            memo_BadReportSituation.EditValue = null;
            memo_Remarks.EditValue = null;
            btn_Ok.EditValue = null;
            btn_Bad.EditValue = null;

            if (buttonPressedFlag)
            {
                btn_Ok.ButtonPressed -= OK_ButtonPressed.ButtonClick;
                btn_Bad.ButtonPressed -= BAD_ButtonPressed.ButtonClick;
                buttonPressedFlag = false;
            }
        }

        private void SetReadOnlyControl(bool flag)
        {
            lup_OccurTeam.ReadOnly = flag;
            lup_Manager.ReadOnly = flag;
            dt_OccurDate2.ReadOnly = flag;
            lup_CheckId.ReadOnly = flag;
            lup_CustomerCode2.ReadOnly = flag;
            //tx_ItemCodeName.ReadOnly = flag;
            spin_QcrQty.ReadOnly = flag;
            spin_ProcessTime.ReadOnly = flag;
            //tx_SrcCode.ReadOnly = flag;
            //tx_LotNo2.ReadOnly = flag;
            spin_CheckQty.ReadOnly = flag;
            spin_BadQty.ReadOnly = flag;
            lup_OccurPosition.ReadOnly = flag;
            spin_OccurQty.ReadOnly = flag;
            lup_MachineCode.ReadOnly = flag;
            spin_LossCost.ReadOnly = flag;
            lup_QcrType.ReadOnly = flag;
            memo_QcrContent.ReadOnly = flag;
            lup_BadReportSolution.ReadOnly = flag;
            tx_Spec.ReadOnly = flag;
            memo_BadReportSituation.ReadOnly = flag;
            memo_Remarks.ReadOnly = flag;
            pic_Bad.ReadOnly = flag;
            pic_Ok.ReadOnly = flag;
            btn_Ok.ReadOnly = flag;
            btn_Bad.ReadOnly = flag;
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

    }
}