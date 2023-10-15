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
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;

namespace HKInc.Ui.View.STD
{
    public partial class XFSTD1500 :  HKInc.Service.Base.TreeListFormTemplate
    {
        IService<TN_STD1500> BomMasterService = (IService<TN_STD1500>)ProductionFactory.GetDomainService("TN_STD1500");
        public XFSTD1500()
        {
            InitializeComponent();
            TreeListExControl = treeListEx1;
        }
        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = HKInc.Utils.Common.GlobalVariable.IconImageCollection;
            TreeListExControl.SetToolbarButtonVisible(false);
            TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            TreeListExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            TreeListExControl.AddColumn("BomId", false);
            TreeListExControl.AddColumn("ParentBomId", false);
            TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), DevExpress.Utils.HorzAlignment.Center);
            TreeListExControl.AddColumn("TN_STD1100.ItemNm1", LabelConvert.GetLabelText("품번"), DevExpress.Utils.HorzAlignment.Center);
            TreeListExControl.AddColumn("Spec1", LabelConvert.GetLabelText("선경"), DevExpress.Utils.HorzAlignment.Center); 
            TreeListExControl.AddColumn("Spec2", LabelConvert.GetLabelText("외경"), DevExpress.Utils.HorzAlignment.Center); 
            TreeListExControl.AddColumn("Spec3", LabelConvert.GetLabelText("자유고"), DevExpress.Utils.HorzAlignment.Center); 
            TreeListExControl.AddColumn("Spec4", LabelConvert.GetLabelText("권수"), DevExpress.Utils.HorzAlignment.Center);             
            TreeListExControl.AddColumn("Unit", LabelConvert.GetLabelText("Unit"), DevExpress.Utils.HorzAlignment.Center);  //단위
            TreeListExControl.AddColumn("UseQty1", LabelConvert.GetLabelText("UseQty"), DevExpress.Utils.HorzAlignment.Center, FormatType.Numeric, "#,0.######");                 //소요량
            TreeListExControl.AddColumn("Memo");                   //비고
            TreeListExControl.AddColumn("CreateId", DevExpress.Utils.HorzAlignment.Center, false);
            TreeListExControl.AddColumn("CreateTime", DevExpress.Utils.HorzAlignment.Center, false);
            TreeListExControl.AddColumn("UpdateId", DevExpress.Utils.HorzAlignment.Center, false);
            TreeListExControl.AddColumn("UpdateTime", DevExpress.Utils.HorzAlignment.Center, false);

            TreeListExControl.ParentFieldName = "ParentBomId";
            TreeListExControl.KeyFieldName = "BomId";

            TreeListExControl.BestFitColumns();

        }

        protected override void InitRepository()
        {
          
            TreeListExControl.TreeList.Columns["Unit"].ColumnEdit = HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemLookUpEdit( DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
        
        }
        protected override void DataLoad()
        {

            BomMasterService.ReLoad();
            string ItemCode = textItemCode.EditValue.GetNullToEmpty();


            TreeListBindingSource.DataSource = BomMasterService.GetList(p => (p.TN_STD1100.ItemNm.Contains(ItemCode) || (p.ItemCode == ItemCode) || p.TN_STD1100.ItemNm1.Contains(ItemCode)) || string.IsNullOrEmpty(ItemCode) ? true : (p.ItemCode.Contains(ItemCode)))
                                                          .OrderBy(p => p.ParentBomId)
                                                          //.OrderBy(p => p.SortOrder)
                                                          .ToList();
            TreeListExControl.DataSource = TreeListBindingSource;

            SetRefreshMessage(TreeListBindingSource.Count);

            TreeListExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
        }
        protected override void DataSave()
        {
            BomMasterService.Save();
            DataLoad();
        }
        protected override void DeleteRow()
        {
            TN_STD1500 obj = TreeListBindingSource.Current as TN_STD1500;
            if (obj == null) return;

        
            BomMasterService.Delete(obj);
            TreeListBindingSource.RemoveCurrent();
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1500, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, BomMasterService);
            return param;
        }

    }
}
