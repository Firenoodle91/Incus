using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 외주발주 시 작업지시 Select 팝업
    /// </summary>
    public partial class XSFMPS1200 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1200> ModelService = (IService<TN_MPS1200>)ProductionFactory.GetDomainService("TN_MPS1200");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint;

        public XSFMPS1200()
        {
            InitializeComponent();
        }

        public XSFMPS1200(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Name);

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (PopupParam.ContainsKey(PopupParameter.Constraint))
                Constraint = PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.CheckBoxMultiSelect(true, "ItemCode", IsmultiSelect);

            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));

            if (Constraint == "TN_PUR1400") // 외주 발주 디테일
            {
                GridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"), false);
                GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
                GridExControl.MainGrid.AddColumn("ItemMoveNo", LabelConvert.GetLabelText("ItemMoveNo"));
                GridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"));
                GridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
                GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
                GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
                GridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
                GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            }
            else
            {
                //GridExControl.MainGrid.AddColumn("JobStates", LabelConvert.GetLabelText("JobStates"), HorzAlignment.Center, true);
                //GridExControl.MainGrid.AddColumn("EmergencyFlag", LabelConvert.GetLabelText("EmergencyFlag"));
                GridExControl.MainGrid.AddColumn("PlanNo", LabelConvert.GetLabelText("PlanNo"), false);
                GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
                GridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"));
                GridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
                GridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
                //GridExControl.MainGrid.AddColumn("MachineFlag", LabelConvert.GetLabelText("MachineFlag"), false);
                //GridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"));
                GridExControl.MainGrid.AddColumn("WorkQty", LabelConvert.GetLabelText("WorkQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
                //GridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));
                //GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
                GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
                //GridExControl.MainGrid.AddColumn("ToolUseFlag", LabelConvert.GetLabelText("ToolUseFlag"));
                //GridExControl.MainGrid.AddColumn("JobSettingFlag", LabelConvert.GetLabelText("JobSettingFlag"));
            }

            GridExControl.MainGrid.SetEditable("_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", DbRequestHandler.GetCommTopCode(MasterCodeSTR.JobStates), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("EmergencyFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("MachineFlag", "N");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("ToolUseFlag", "N");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("JobSettingFlag", "N");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            if (Constraint == "TN_PUR1400") //외주발주 디테일 추가 시
            {
                List<TN_PUR1400> newMasterList = null;
                if (PopupParam.ContainsKey(PopupParameter.Value_1))
                    newMasterList = PopupParam.GetValue(PopupParameter.Value_1) as List<TN_PUR1400>;

                List<string> checkList = new List<string>();

                if (newMasterList != null)
                {
                    foreach (var v in newMasterList)
                    {
                        foreach (var c in v.TN_PUR1401List)
                        {
                            //checkList.Add(c.WorkNo + "_" + c.ProcessSeq.ToString());
                            checkList.Add(c.ItemMoveNo);
                        }
                    }
                    //var detailList = newMasterList.Select(p => p.TN_PUR1401List.Select(c => new object[] { c.WorkNo, c.ProcessCode, c.ProcessSeq })).ToList();

                }

                //bindingSource.DataSource = ModelService.GetList(p => p.OutProcFlag == "Y"
                //                                                    && p.JobStates == MasterCodeSTR.JobStates_Wait
                //                                                    && !checkList.Contains((p.WorkNo + "_" + p.ProcessSeq))
                //                                                )
                //                                                .OrderBy(p => p.WorkNo)
                //                                                .ToList();
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var strDate = new SqlParameter("@StrDate", string.Empty);
                    var endDate = new SqlParameter("@EndDate", string.Empty);
                    var itemCode = new SqlParameter("@ItemCode", string.Empty);

                    var result = context.Database.SqlQuery<TEMP_XPFPUR1400_LIST>("USP_GET_XPFPUR1400_LIST @StrDate, @EndDate ,@ItemCode", strDate, endDate, itemCode ).OrderBy(o => o.WorkNo).ToList();

                    bindingSource.DataSource = result.Where(x => !checkList.Contains(x.ItemMoveNo)).ToList();
                }

            }
            else
            {

                bindingSource.DataSource = ModelService.GetList(p => true
                                                                )
                                                                .OrderBy(p => p.WorkNo)
                                                                .ToList();
            }
            GridExControl.DataSource = bindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

        protected override void Confirm()
        {
            if (bindingSource == null || bindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            //var returnList = new List<TEMP_XPFPUR1400_LIST>();
            var dataList = bindingSource.List as List<TEMP_XPFPUR1400_LIST>;
            var checkList = dataList.Where(p => p._Check == "Y").ToList();
            foreach (var v in checkList)
            {
                //var returnObj = ModelService.GetList(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                //if (returnObj != null)
                //returnList.Add(ModelService.Detached(v));
            }
            param.SetValue(PopupParameter.ReturnObject, checkList);
            
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var obj = (TEMP_XPFPUR1400_LIST)bindingSource.Current;
                var rtnList = new List<TEMP_XPFPUR1400_LIST> { obj };
                
                param.SetValue(PopupParameter.ReturnObject, rtnList);
                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}