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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;
using System.Reflection;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 납기회의관리
    /// </summary>
    public partial class XFORD1102 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1102> ModelService = (IService<TN_ORD1102>)ProductionFactory.GetDomainService("TN_ORD1102");

        public XFORD1102()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            IsMasterGridFocusedRowChangedEnabled = false;
            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged1;
            
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }

        protected override void InitCombo()
        {
            dt_DelivDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_DelivDate.DateToEdit.DateTime = DateTime.Today;

            lup_OrderId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<VI_BUSINESS_MANAGEMENT_USER>(p => true).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());
            lup_DelivStates.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.DelivConferenceStates));
            lup_JobStates.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.DelivConferenceStates));

            ControlEnableList.Add("ConferenceDate", dt_ConferenceDate);
            ControlEnableList.Add("BeforeDelivDate", dt_BeforeDelivDate);
            ControlEnableList.Add("AfterDelivDate", dt_AfterDelivDate);
            ControlEnableList.Add("Memo", memo_Conference);
            LayoutControlHandler.SetRequiredLabelText<TN_ORD1102>(new TN_ORD1102(), ControlEnableList, this.Controls);

            dt_ConferenceDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            dt_ConferenceDate.ReadOnly = true;
            dt_BeforeDelivDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            dt_BeforeDelivDate.ReadOnly = true;
            dt_AfterDelivDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            lcConferenceContent.Enabled = false;

            // UserRight권한에 따라 Control 설정            
            foreach (var controlName in ControlEnableList.Keys)
            {
                ControlEnableList[controlName].Enabled = UserRight.HasEdit;
                if (UserRight.HasEdit)
                    ((BaseEdit)ControlEnableList[controlName]).EditValueChanged += ListFormTemplate_EditValueChanged;
            }
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("DetailView") + "[F3]", IconImageList.GetIconImage("business%20objects/botask"));
            MasterGridExControl.MainGrid.AddColumn("DelivStates", LabelConvert.GetLabelText("DelivStates"));
            MasterGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"), false);            
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("OrderId"));
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"));
            MasterGridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("PlanWorkQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"));
            MasterGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("PackBeforeResultQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("PackOkQty", LabelConvert.GetLabelText("PackQty2"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("ChangeQty", LabelConvert.GetLabelText("ChangeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            DetailGridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            DetailGridExControl.MainGrid.AddColumn("ConferenceDate", LabelConvert.GetLabelText("ConferenceDate"));
            DetailGridExControl.MainGrid.AddColumn("BeforeDelivDate", LabelConvert.GetLabelText("BeforeDelivDate"));
            DetailGridExControl.MainGrid.AddColumn("AfterDelivDate", LabelConvert.GetLabelText("AfterDelivDate"));
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ConferenceDate", "AfterDelivDate");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.DelivConferenceStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.DelivConferenceStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ConferenceDate");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("BeforeDelivDate", false, 125);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("AfterDelivDate", false, 125);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            IsFormControlChanged = false;

            GridRowLocator.GetCurrentRow("DelivNo");
            DetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_2));
            PopupDataParam.SetValue(PopupParameter.GridRowId_2, null);

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
            }
            #endregion
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var DelivDateFr = new SqlParameter("@DelivDateFr", dt_DelivDate.DateFrEdit.DateTime.GetNullToDateTime() == null ? "" : dt_DelivDate.DateFrEdit.DateTime.ToShortDateString());
                var DelivDateTo = new SqlParameter("@DelivDateTo", dt_DelivDate.DateToEdit.DateTime.GetNullToDateTime() == null ? "" : dt_DelivDate.DateToEdit.DateTime.ToShortDateString());
                var ItemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var CustomerCode = new SqlParameter("@CustomerCode", lup_Customer.EditValue.GetNullToEmpty());
                var DelivState = new SqlParameter("@DelivState", lup_DelivStates.EditValue.GetNullToEmpty());
                var JobState = new SqlParameter("@JobState", lup_JobStates.EditValue.GetNullToEmpty());
                var OrderId = new SqlParameter("@OrderId", lup_OrderId.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XFORD1102_MASTER>("USP_GET_XFORD1102_MASTER @DelivDateFr, @DelivDateTo, @ItemCode ,@CustomerCode ,@DelivState, @JobState, @OrderId"
                                                                                , DelivDateFr, DelivDateTo, ItemCode, CustomerCode, DelivState, JobState, OrderId).ToList();

                MasterGridBindingSource.DataSource = result.OrderBy(p => p.DelivDate).ToList();
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        //마스터그리드
        private void MainView_FocusedRowChanged1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (IsFormControlChanged)
            {
                DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    ActSave();
                    return;
                }
                else
                {
                    ActRefresh();
                    return;
                }
            }

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
            }
            #endregion

            var masterObj = MasterGridBindingSource.Current as TEMP_XFORD1102_MASTER;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();

                return;
            }

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.OrderNo == masterObj.OrderNo
                                                                        && p.OrderSeq == masterObj.OrderSeq
                                                                        && p.DelivNo == masterObj.DelivNo
                                                                    )
                                                                    .OrderBy(p => p.RowId)
                                                                    .ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            MainView_FocusedRowChanged(null, null);
        }

        //디테일그리드
        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dt_ConferenceDate.ReadOnly = true;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1102;
            if (detailObj == null)
            {
                lcConferenceContent.Enabled = false;
                dt_ConferenceDate.EditValue = null;
                dt_AfterDelivDate.EditValue = null;
                dt_BeforeDelivDate.EditValue = null;
                memo_Conference.EditValue = null;
                return;
            }
            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", detailObj, control.Key, true));
            }
            #endregion

            if (detailObj.NewRowFlag != "Y")
            {
                var detailList = DetailGridBindingSource.List as List<TN_ORD1102>;
                if (detailList.Any(p => p.NewRowFlag == "Y"))
                    lcConferenceContent.Enabled = false;
                else
                {
                    var lastRowId = detailList.OrderBy(p => p.RowId).Last().RowId;
                    if (detailObj.RowId == lastRowId)
                        lcConferenceContent.Enabled = true;
                    else
                        lcConferenceContent.Enabled = false;
                }
            }
            else
            {
                lcConferenceContent.Enabled = true;
                dt_ConferenceDate.ReadOnly = false;
            }
        }
        
        /// <summary>
        /// 상세보기
        /// </summary>
        protected override void AddRowClicked()
        {
            var obj = MasterGridBindingSource.Current as TEMP_XFORD1102_MASTER;
            if (obj == null) return;
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj.DelivNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFORD1102, param, null);
            form.ShowPopup(true);
        }

        protected void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null) return;

            if (e.Clicks == 2)
            {
                try { AddRowClicked(); } catch (Exception ex) { MessageBoxHandler.ErrorShow(ex); }
            }
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_XFORD1102_MASTER;
            if (masterObj == null)
                return;

            var detailList = DetailGridBindingSource.List as List<TN_ORD1102>;
            if (detailList == null)
                return;

            var newObjCheck = detailList.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
            if (newObjCheck != null)
            {
                DetailGridExControl.MainGrid.MainView.FocusedRowHandle = DetailGridExControl.MainGrid.MainView.LocateByValue("RowId", newObjCheck.RowId);
            }
            else
            {
                var newObj = new TN_ORD1102()
                {
                    OrderNo = masterObj.OrderNo,
                    OrderSeq = masterObj.OrderSeq,
                    DelivNo = masterObj.DelivNo,
                    ConferenceDate = DateTime.Today,
                    BeforeDelivDate = masterObj.DelivDate,
                    AfterDelivDate = DateTime.Today,
                    NewRowFlag = "Y"
                };

                PopupDataParam.SetValue(PopupParameter.GridRowId_2, newObj.RowId);

                DetailGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);
                DetailGridExControl.BestFitColumns();
                IsFormControlChanged = true;
            }
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            var detailList = DetailGridBindingSource.List as List<TN_ORD1102>;
            if (detailList != null && detailList.Count > 0)
            {
                var newObj = detailList.Where(p => p.NewRowFlag == "Y").FirstOrDefault();
                if (newObj != null)
                {
                    var TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.DelivNo == newObj.DelivNo).FirstOrDefault();
                    TN_ORD1100.DelivDate = newObj.AfterDelivDate;
                    TN_ORD1100.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_ORD1100);
                }
                else
                {
                    var lastObj = detailList.OrderBy(p => p.RowId).LastOrDefault();
                    var TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.DelivNo == lastObj.DelivNo).FirstOrDefault();
                    TN_ORD1100.DelivDate = lastObj.AfterDelivDate;
                    TN_ORD1100.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_ORD1100);
                }
            }

            ModelService.Save();

            DataLoad();
        }

        private void ListFormTemplate_EditValueChanged(object sender, EventArgs e)
        {
            IsFormControlChanged = false;

            if (!IsFirstLoaded) return;

            BaseEdit edit = sender as BaseEdit;
            if (edit == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1102;
            if (detailObj == null)
                return;

            if (edit.DataBindings.Count == 0) return;

            //BindingSource CurrentBindingSoruce = edit.DataBindings[0].DataSource as BindingSource;
            //if (CurrentBindingSoruce.Current == null) return;

            //object currentObj = CurrentBindingSoruce.Current;
            //Type type = currentObj.GetType();

            //PropertyInfo updateId = type.GetProperty("UpdateId");
            //PropertyInfo updateTime = type.GetProperty("UpdateTime");

            //updateId.SetValue(currentObj, HKInc.Utils.Common.GlobalVariable.LoginId);
            //updateTime.SetValue(currentObj, DateTime.Now);
            detailObj.UpdateId = GlobalVariable.LoginId;
            detailObj.UpdateTime = DateTime.Now;

            ((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).SetIsFormControlChanged(true);

            IsFormControlChanged = true;

            if (edit.Name.Contains("dt_ConferenceDate"))
            {
                detailObj.ConferenceDate = ((DateEdit)edit).DateTime;
                DetailGridExControl.BestFitColumns();
            }
            else if (edit.Name.Contains("dt_AfterDelivDate"))
            {
                detailObj.AfterDelivDate = ((DateEdit)edit).DateTime;
                DetailGridExControl.BestFitColumns();
            }
            else if (edit.Name.Contains("memo_Conference"))
            {
                detailObj.Memo = ((MemoEdit)edit).EditValue.GetNullToNull();
                DetailGridExControl.BestFitColumns();
            }
        }
    }
}