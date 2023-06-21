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


namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 품목이슈관리
    /// </summary>
    public partial class XFSTD1102 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFSTD1102()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailGridView_CellValueChanged;
            SubDetailGridExControl = gridEx3;
        }

        private void DetailGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
         
            
            var detailObj = DetailGridBindingSource.Current as TN_STD1102;
                 // 2021-06-25 김진우 주임 추가     디테일에 있는 예방보전값을 가진 목록들

            if (e.Column.FieldName == "EndDate")
            {
                if (detailObj.StartDate > detailObj.EndDate)
                {
                    MessageBox.Show("시작일자보다 종료일이 빠를수 없습니다.");
                    detailObj.EndDate = detailObj.StartDate;
                    
                }
            }
          
    }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 품목별 이슈라서 반제품(외주)를 추가 처리
            lup_ProductTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            MasterGridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));

            MasterGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);


            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("Issue", LabelConvert.GetLabelText("Issue"));
            DetailGridExControl.MainGrid.AddColumn("StartDate", LabelConvert.GetLabelText("StartDate"));
            DetailGridExControl.MainGrid.AddColumn("EndDate", LabelConvert.GetLabelText("EndDate"));
            DetailGridExControl.MainGrid.AddColumn("IssueId", LabelConvert.GetLabelText("IssueId"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Issue", "StartDate", "EndDate");
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);


            SubDetailGridExControl.SetToolbarVisible(false);
            //SubDetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "이슈" + LabelConvert.GetLabelText("AddRow") + "[Alt+A]", IconImageList.GetIconImage("navigation/previous"));
            //SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            SubDetailGridExControl.MainGrid.AddColumn("ClaimNo", LabelConvert.GetLabelText("ClaimNo"));
            SubDetailGridExControl.MainGrid.AddColumn("ClaimDate", LabelConvert.GetLabelText("ClaimDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            SubDetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            SubDetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            SubDetailGridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            SubDetailGridExControl.MainGrid.AddColumn("ClaimQty", LabelConvert.GetLabelText("Qty2"), HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("ClaimType", LabelConvert.GetLabelText("ClaimType"));
            SubDetailGridExControl.MainGrid.AddColumn("ClaimId", LabelConvert.GetLabelText("ClaimId"));
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            SubDetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            SubDetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            SubDetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            SubDetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1102>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            DetailGridExControl.MainGrid.MainView.Columns["Issue"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Issue", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("StartDate", true);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("EndDate", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("IssueId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ClaimType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ClaimType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ClaimId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, SubDetailGridExControl, MasterCodeSTR.FtpFolder_ClaimFile, "FileName", "FileUrl", true);
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(SubDetailGridExControl, "Memo");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var productTeamCode = lup_ProductTeamCode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(productTeamCode) ? true : p.ProcTeamCode == productTeamCode)
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing) &&  // 20210524 오세완 차장 반제품을 타사 / 자사 처리하여 추가 처리
                                                                        (p.UseFlag == "Y")).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_STD1102List.OrderByDescending(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            //클레임 목록은 오늘날짜로 2개월 전까지만 데이터 불러오기.
            var todayDate = DateTime.Today;
            SubDetailGridBindingSource.DataSource = masterObj.TN_QCT1400List.Where(p=>p.ClaimDate >= todayDate.AddMonths(-2) && p.ClaimDate <= todayDate).OrderBy(p => p.ClaimDate).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;
            var subobj = SubDetailGridBindingSource.Current as TN_QCT1400;
            var newObj = new TN_STD1102()
            {
                ItemCode = masterObj.ItemCode,
                Seq = masterObj.TN_STD1102List.Count == 0 ? 1 : masterObj.TN_STD1102List.Max(p => p.Seq) + 1,
                Issue = subobj == null ? "" : subobj.Memo.GetNullToEmpty(),
                IssueId = GlobalVariable.LoginId,
            };
            masterObj.TN_STD1102List.Add(newObj);
            DetailGridBindingSource.Add(newObj);
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_STD1102;
            if (detailObj == null) return;

            masterObj.TN_STD1102List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
        }

        protected override void SubDetailAddRowClicked()
        {
            //var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            //if (masterObj == null) return;

            //var subObj = SubDetailGridBindingSource.Current as TN_QCT1400;
            //if (subObj == null) return;

            //var newObj = new TN_STD1102()
            //{
            //    ItemCode = masterObj.ItemCode,
            //    Seq = masterObj.TN_STD1102List.Count == 0 ? 1 : masterObj.TN_STD1102List.Max(p => p.Seq) + 1,
            //};

            //masterObj.TN_STD1102List.Add(newObj);
            //DetailGridBindingSource.Add(newObj);
        }
    }
}