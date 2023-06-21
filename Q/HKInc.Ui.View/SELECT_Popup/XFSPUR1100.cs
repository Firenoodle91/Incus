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

using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using System.Data.SqlClient;

namespace HKInc.Ui.View.SELECT_Popup
{
    public partial class XFSPUR1100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_PUR1100> ModelService = (IService<TN_PUR1100>)ProductionFactory.GetDomainService("TN_PUR1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        #endregion

        public XFSPUR1100()
        {
            InitializeComponent();
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
        }
        public XFSPUR1100(PopupDataParam parameter, PopupCallback callback) :this()
        {
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-20);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(20);
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);
            
            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            if (parameter.ContainsKey(PopupParameter.Constraint))
                DepartmentCode = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            this.Text = "발주정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.MainGrid.AddColumn("ReqNo", "발주번호");
            gridEx1.MainGrid.AddColumn("ReqDate", "발주일");
            gridEx1.MainGrid.AddColumn("DueDate", "납기예정일");
            //gridEx1.MainGrid.AddColumn("ReqId", "발주자");
            gridEx1.MainGrid.AddColumn("UserName", "발주자"); // 20220217 오세완 차장 프로시저로 변경 후 코드명을 바로 변환해서 출력하기 때문
            //gridEx1.MainGrid.AddColumn("CustomerCode", "거래처");
            gridEx1.MainGrid.AddColumn("CustomerName", "거래처"); // 20220217 오세완 차장 프로시저로 변경 후 코드명을 바로 변환해서 출력하기 때문

            gridEx1.MainGrid.AddColumn("Memo");
            gridEx1.MainGrid.AddColumn("Temp2", "발주확정");

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            // 20220217 오세완 차장 프로시저로 변경 후 코드명을 바로 변환해서 출력하기 때문
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            //gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            gridEx1.MainGrid.SetRepositoryItemDateEdit("DueDate");
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("Temp2", "N");
        }

        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            string cust = lupcust.EditValue.GetNullToEmpty();

            //bindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_date.DateFrEdit.DateTime.Date && p.ReqDate <= dp_date.DateToEdit.DateTime.Date) && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust)&&p.Temp2=="Y").OrderBy(o => o.CustomerCode).OrderBy(o => o.ReqDate).ToList();
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Customercode = new SqlParameter("@CUSTOMER_CODE", cust);
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", dp_date.DateFrEdit.DateTime.ToShortDateString());
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", dp_date.DateToEdit.DateTime.ToShortDateString());

                // 20220217 오세완 차장 입고완료처리한 건을 제외하고 조회하는 프로시저
                var vResult = context.Database.SqlQuery<TP_XFSPUR1100_LIST>("USP_GET_XFSPUR1100_LIST @DATE_FROM, @DATE_TO, @CUSTOMER_CODE", sp_Datefrom, sp_Dateto, sp_Customercode).ToList();
                if (vResult != null)
                    bindingSource.DataSource = vResult;
                else
                    bindingSource.Clear();
            }

            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TN_PUR1100> Pur1100List = new List<TN_PUR1100>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    string Reqno = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ReqNo").GetNullToEmpty();

                    TN_PUR1100 pur1100 = ModelService.GetList(p => p.ReqNo == Reqno).FirstOrDefault();
                    if(pur1100 != null) // 20220217 오세완 차장 예외처리 추가 
                        Pur1100List.Add(ModelService.Detached(pur1100));
                }
                param.SetValue(PopupParameter.ReturnObject, Pur1100List);
            }
            else
            {
                // 20220217 오세완 차장 프로시저로 변경해서 수정
                //param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_PUR1100)bindingSource.Current));
                TP_XFSPUR1100_LIST temp = bindingSource.Current as TP_XFSPUR1100_LIST;
                if(temp != null)
                {
                    TN_PUR1100 Pur1100 = ModelService.GetList(p => p.ReqNo == temp.ReqNo).FirstOrDefault();
                    if (Pur1100 != null)
                        param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(Pur1100));
                }

            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                // 20220217 오세완 차장 프로시저로 변경해서 수정
                //TN_PUR1100 Pur1100 = (TN_PUR1100)bindingSource.Current;
                TP_XFSPUR1100_LIST temp = bindingSource.Current as TP_XFSPUR1100_LIST;
                if(temp != null)
                {
                    TN_PUR1100 Pur1100 = ModelService.GetList(p => p.ReqNo == temp.ReqNo).FirstOrDefault();
                    if (IsmultiSelect)
                    {
                        List<TN_PUR1100> Pur1100List = new List<TN_PUR1100>();
                        if (Pur1100 != null)
                            Pur1100List.Add(ModelService.Detached(Pur1100));

                        param.SetValue(PopupParameter.ReturnObject, Pur1100List);
                    }
                    else
                    {
                        param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(Pur1100));
                    }

                    ReturnPopupArgument = new PopupArgument(param);
                }

                base.ActClose();
            }

        }
    }
}

