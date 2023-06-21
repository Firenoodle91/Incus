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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;

namespace HKInc.Ui.View.ORD
{
 
    public partial class XFSORD1100 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_ORD1100> ModelService = (IService<TN_ORD1100>)ProductionFactory.GetDomainService("TN_ORD1100");
        public XFSORD1100()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            datePeriodEditEx1.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            datePeriodEditEx1.DateToEdit.DateTime = DateTime.Today.AddDays(+30);
        }
        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
         //   lupitemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(p => p.ItemNm1).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("OrderCust"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustOrderNo", LabelConvert.GetLabelText("CustOrderNo"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("CustOrderId", LabelConvert.GetLabelText("VanEmp"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("PeriodDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("EmpCode"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo1"));
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), HorzAlignment.Center, false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("No"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("차종"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");         
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", LabelConvert.GetLabelText("품번"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("Qty"), HorzAlignment.Far, FormatType.Numeric, "n0");            
            DetailGridExControl.MainGrid.AddColumn("PeriodDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Center, true);
            
            DetailGridExControl.BestFitColumns();

            SubDetailGridExControl.MainGrid.Init();
            //   IsSubDetailGridButtonExportEnabled = true;
            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", LabelConvert.GetLabelText("품번"), HorzAlignment.Center, true);            
            SubDetailGridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivPlanDate"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("DelivPlanQty"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            SubDetailGridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("ProdYn", "생산의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("OutConfirmflag", "출고의뢰", HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DelivDate", "DelivQty", "ProdYn", "OutConfirmflag", "Memo");

            SubDetailGridExControl.BestFitColumns();
        }
        protected override void InitRepository()
        {

         
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OrderDate");       
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag != "N"), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("PeriodDate");

            
            SubDetailGridExControl.MainGrid.SetRepositoryItemDateEdit("DelivDate");
            SubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("DelivQty", 0, DefaultBoolean.Default, MaskType.Numeric, "n0", true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<UserView>(p => p.Active != "N"), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(SubDetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProdYn", DbRequestHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutConfirmflag", DbRequestHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
          
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            DetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_2));
            SubDetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_3));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_2, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            
            string CustCode = lupCustcode.EditValue.GetNullToEmpty();
            string Itemcode = tx_item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetChildList<VI_ORD1100>(p => (p.OrderType=="개발")&&(string.IsNullOrEmpty(CustCode) ? true : p.CustomerCode == CustCode) &&

                                                                          (string.IsNullOrEmpty(Itemcode) ? true : (p.ItemCode.Contains(Itemcode) || p.TN_STD1100.ItemNm.Contains(Itemcode) || p.TN_STD1100.ItemNm1.Contains(Itemcode))) &&
                                                                           (p.OrderDate >= datePeriodEditEx1.DateFrEdit.DateTime &&
                                                                            p.OrderDate <= datePeriodEditEx1.DateToEdit.DateTime)
                                                                    ).GroupBy(g => g.OrderNo).Select(s => s.First()).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

        }
        protected override void MasterFocusedRowChanged()
        {
            VI_ORD1100 obj = MasterGridBindingSource.Current as VI_ORD1100;
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_ORD1002>(p => p.OrderNo == obj.OrderNo).OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
           
            SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
        }
        protected override void DetailFocusedRowChanged()
        {
            TN_ORD1002 obj = DetailGridBindingSource.Current as TN_ORD1002;
            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == obj.OrderNo&&p.Seq==obj.Seq).OrderBy(o => o.DelivSeq).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();

            SetRefreshMessage(SubDetailGridExControl.MainGrid.RecordCount);
        }
        protected override void SubDetailAddRowClicked()
        {
            if (!UserRight.HasEdit) return;
            var Dobj = DetailGridBindingSource.Current as TN_ORD1002;
            if (Dobj == null) return;
            TN_ORD1100 obj = new TN_ORD1100()
            {
                DelivSeq = DbRequestHandler.GetRequestNumber("DLV"),
                OrderNo = Dobj.OrderNo,
                Seq = Dobj.Seq,
                ItemCode=Dobj.ItemCode,
                DelivDate = DateTime.Today,
                DelivQty = 0,
                DelivId = Utils.Common.GlobalVariable.LoginId,
                ProdYn = "N",
                OutConfirmflag = "N",
                Temp=Dobj.Temp

            };
          
            SubDetailGridBindingSource.Add(obj);
            ModelService.Insert(obj);
            SubDetailGridExControl.MainGrid.BestFitColumns();
            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, obj);
            #endregion
        }
        protected override void DeleteSubDetailRow()
        {
            if (!UserRight.HasEdit) return;
            var Dobj = DetailGridBindingSource.Current as TN_ORD1002;
            if (Dobj == null) return;

            var obj = SubDetailGridBindingSource.Current as TN_ORD1100;
            if (obj != null)
            {
                ModelService.Delete(obj);
                SubDetailGridBindingSource.RemoveCurrent();
            }
        }
        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
    }
}
}
