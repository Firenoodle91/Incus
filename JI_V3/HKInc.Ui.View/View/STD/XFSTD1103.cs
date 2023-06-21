using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;


namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 단가이력관리
    /// </summary>
    public partial class XFSTD1103 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1103> ModelService = (IService<TN_STD1103>)ProductionFactory.GetDomainService("TN_STD1103");
        //IService<TN_STD1103_MASTER> ModelService2 = (IService<TN_STD1103_MASTER>)ProductionFactory.GetDomainService("TN_STD1103_MASTER");

        public XFSTD1103()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            //DetailGridExControl.MainGrid.MainView.RowCellStyle += DetailMainView_RowCellStyle;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellvalueChanged;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += SubDetailMainView_CellvalueChanged;
        }


        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)).ToList());
            lup_TopCategory.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1).Where(p => p.CodeVal != MasterCodeSTR.TopCategory_SPARE && p.CodeVal != MasterCodeSTR.TopCategory_TOOL).ToList());

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));


            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            DetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            //DetailGridExControl.MainGrid.AddColumn("CustomCustomerCode", LabelConvert.GetLabelText("CustomerName"));
            //DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CustomerCode");


            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("Unit", "단위");
            //SubDetailGridExControl.MainGrid.AddColumn("CostManageNo", "단가관리번호", false);
            SubDetailGridExControl.MainGrid.AddColumn("Seq", "순번");

            SubDetailGridExControl.MainGrid.AddColumn("NewRowFlag", "N", false);
            SubDetailGridExControl.MainGrid.AddColumn("EditRowFlag", "E", false);
            SubDetailGridExControl.MainGrid.AddColumn("DeleteRowFlag", "D", false);

            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", "ItemCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("CustomerCode", "CustomerCode", false);
            SubDetailGridExControl.MainGrid.AddColumn("ItemCost", "단가", HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("StartDate", "변경시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("EndDate", "변경종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate", "ItemCost", "Memo");

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var topCode = lup_TopCategory.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y"
                                                    && (p.TopCategory == MasterCodeSTR.TopCategory_WAN 
                                                        || p.TopCategory == MasterCodeSTR.TopCategory_BAN
                                                        || p.TopCategory == MasterCodeSTR.TopCategory_MAT)
                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                    && (string.IsNullOrEmpty(topCode) ? true : p.TopCategory == topCode)
                                                    ).OrderBy( p => p.ItemCode).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var itemcode = masterObj.ItemCode;

            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1103_MASTER>(p => p.ItemCode == itemcode
                                                                                             )
                                                                                             .OrderBy(p => p.ItemCode).ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

            SetRefreshMessage(DetailGridExControl);

        }

        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();

            var masterObj =  MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var DetailObj = DetailGridBindingSource.Current as TN_STD1103_MASTER;
            if (DetailObj == null) return;
            
            
            string itemcode = DetailObj.ItemCode.GetNullToEmpty();
            string custcode = DetailObj.CustomerCode.GetNullToEmpty();

            SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1103>(p => p.ItemCode == itemcode
                                                                                       && p.CustomerCode == custcode
                                                                                       ).OrderBy(p => p.StartDate).ToList();

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();
        }


        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            List<TN_STD1103_MASTER> DetailList = DetailGridBindingSource.DataSource as List<TN_STD1103_MASTER>;

            var newObj = new TN_STD1103_MASTER()
            {
                NewRowFlag = "Y"
                , ItemCode = masterObj.ItemCode
                , CustomerCode = ""
                //, Seq = DetailList.Count == 0 ? 1 : DetailList.Max(m => m.Seq) + 1
            };

            masterObj.TN_STD1103_MASTERList.Add(newObj);
            DetailGridBindingSource.Add(newObj);
        }
        private void DetailMainView_CellvalueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gridview = sender as GridView;

            List<TN_STD1103_MASTER> DetailList = DetailGridBindingSource.DataSource as List<TN_STD1103_MASTER>;

            string fieldName = e.Column.FieldName;

            var checkCust = gridview.GetRowCellValue(e.RowHandle, gridview.Columns[fieldName]).GetNullToEmpty();

            int rrr = e.RowHandle;

            if (checkCust == "") return;

            foreach (var v in DetailList)
            {
                if (checkCust == v.CustomerCode && v.RowId != 0)
                {
                    MessageBox.Show("한 품목에 같은 거래처 등록 불가");
                    gridview.SetFocusedRowCellValue(fieldName, null);
                    return;
                }
            }


        }
        protected override void SubDetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            var DetailObj = DetailGridBindingSource.Current as TN_STD1103_MASTER;
            if (DetailObj == null) return;

            //List<TN_STD1103> SubDetailList = SubDetailGridBindingSource.DataSource as List<TN_STD1103>;
            List<TN_STD1103> SubDetailList = SubDetailGridBindingSource.List as List<TN_STD1103>;

            var newObj = new TN_STD1103()
            {
                Unit = masterObj.Unit,
                Seq = SubDetailList.Count == 0 ? 1 : SubDetailList.Max(m => m.Seq) + 1,
                ItemCode = DetailObj.ItemCode,
                CustomerCode = DetailObj.CustomerCode,
                ItemCost = 0,
                StartDate = DateTime.Today,
                NewRowFlag = "Y"
            };

            SubDetailGridBindingSource.Add(newObj);
            DetailObj.TN_STD1103List.Add(newObj);
            SubDetailGridExControl.BestFitColumns();
        }

        private void SubDetailMainView_CellvalueChanged(object sender, CellValueChangedEventArgs e)
        {
            var MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            var SubDetailObj = SubDetailGridBindingSource.Current as TN_STD1103;
            if (MasterObj == null || SubDetailObj == null) return;

            string column = e.Column.FieldName;


            if (column == "StartDate")
            {
                if (DataCheck(MasterObj.TN_STD1103List, SubDetailObj))
                {
                    MessageBox.Show("시작일이 중복되지 않게 처리");
                    SubDetailObj.StartDate = null;
                }
            }
            else if (column == "EndDate")
            {
                if (SubDetailObj.StartDate > SubDetailObj.EndDate)
                {
                    MessageBox.Show("종료일은 시작일보다 전 일수 없음");
                    SubDetailObj.EndDate = null;
                    return;
                }
            }

            SubDetailGridExControl.MainGrid.BestFitColumns();
        }
        private bool DataCheck(ICollection<TN_STD1103> list, TN_STD1103 obj)
        {
            var data = list.Where(x => x.Seq != obj.Seq &&
                                 ((x.EndDate != null && (x.StartDate <= obj.StartDate && obj.StartDate <= x.EndDate)) || (x.EndDate == null && (x.StartDate <= obj.StartDate || obj.StartDate <= x.EndDate)))).ToList();

            int cnt = data.Count;
            foreach (var s in data)
            {
                if (s.EndDate == null)
                {
                    DateTime endDt = obj.StartDate.Value.AddDays(-1);
                    if (s.StartDate > endDt)
                        return true;
                    else
                    {
                        s.EndDate = endDt;
                        cnt += -1;
                        s.EditRowFlag = "Y";
                    }

                }
            }

            //과거값을 넣었을경우
            var data2 = list.Where(x => x.Seq != obj.Seq && obj.StartDate < x.StartDate).ToList();
            if (data2.Count > 0)
            {
                obj.EndDate = obj.StartDate;
                obj.EditRowFlag = "Y";
            }

            if (cnt > 0)
                return true;
            else
                return false;
        }

        private void DetailMainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                //거래처 등록이 되어 있으면 수정 불가 처리

                var DetailObj = DetailGridBindingSource.Current as TN_STD1103_MASTER;

                //if (DetailObj != null)
                //{
                //    //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_38), LabelConvert.GetLabelText("PoConfirm")));
                //    e.Cancel = true;
                //}

                if (DetailObj.NewRowFlag != "Y")
                    e.Cancel = true;
            }
            catch { }
        }




        protected override void DataSave()
        {
            
            List<TN_STD1103> List = SubDetailGridBindingSource.DataSource as List<TN_STD1103>;
            

            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();
            
            ModelService.Save();

            DataLoad();

            GridRowLocator.SetCurrentRow();
        }

        protected override void DeleteDetailRow()
        {
            //거래처 모두 삭제
            var DetailObj = DetailGridBindingSource.Current as TN_STD1103_MASTER;
            if (DetailObj == null) return;

            DetailGridBindingSource.RemoveCurrent();
            ModelService.RemoveChild(DetailObj);
        }

        protected override void DeleteSubDetailRow()
        {
            //거래처별 단가 삭제
            var DetailObj = DetailGridBindingSource.Current as TN_STD1103_MASTER;
            if (DetailObj == null) return;

            var SubDetailObj = SubDetailGridBindingSource.Current as TN_STD1103;
            if (SubDetailObj == null) return;

            SubDetailGridBindingSource.RemoveCurrent();
            ModelService.RemoveChild(SubDetailObj);
        }

    }


}