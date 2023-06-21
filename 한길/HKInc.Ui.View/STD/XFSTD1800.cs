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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;

namespace HKInc.Ui.View.STD
{
    /// <summary>
    /// 품목이슈관리화면
    /// </summary>
    public partial class XFSTD1800 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        //IService<TN_STD1800> ModelServicestd1800 = (IService<TN_STD1800>)ProductionFactory.GetDomainService("TN_STD1800");

        public XFSTD1800()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }

        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
            MasterGridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
        
            //GridExControl.MainGrid.AddColumn("ItemGbn");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            //MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            //MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            //MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            //MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");   
            MasterGridExControl.MainGrid.AddColumn("Spec1", "원재료)");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "색상)");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "재료규격");
            MasterGridExControl.MainGrid.AddColumn("Temp2", "실리콘농도");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "제품규격");
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("Note", "이슈사항");
            DetailGridExControl.MainGrid.AddColumn("StartDt", "시작일");
            DetailGridExControl.MainGrid.AddColumn("EndDt", "종료일");
            DetailGridExControl.MainGrid.AddColumn("Workuser", "등록자");
            
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Note", "StartDt", "EndDt", "Workuser");

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("StartDt");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("EndDt");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Workuser", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Note"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string cta = lupItemtype.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text))
                                                                        && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta)
                                                                     )
                                                                     .OrderBy(p => p.ItemNm1)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if(obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = obj.TN_STD1800List.OrderByDescending(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null) return;
            TN_STD1800 new_obj = new TN_STD1800() { ItemCode = obj.ItemCode };
            DetailGridBindingSource.Add(new_obj);
            obj.TN_STD1800List.Add(new_obj);
        }

        protected override void DeleteDetailRow()
        {
            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (MasterObj == null) return;
            TN_STD1800 obj = DetailGridBindingSource.Current as TN_STD1800;
            if (obj == null) return;
            DetailGridBindingSource.Remove(obj);
            MasterObj.TN_STD1800List.Remove(obj);
        }
    }
}