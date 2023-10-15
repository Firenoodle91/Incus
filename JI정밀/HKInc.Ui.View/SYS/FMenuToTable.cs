using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;
using DevExpress.XtraTreeList;

namespace HKInc.Ui.View.SYS
{
    
    public partial class FMenuToTable : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<MENUTOTABLE> ModelService = (IService<MENUTOTABLE>)ServiceFactory.GetDomainService("MENUTOTABLE");
        Boolean isnew = false;
        public FMenuToTable()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            
            OutPutRadioGroup = radioGroup1;
            RadioGroupType = RadioGroupType.ActiveAll;
       
        }

    
        protected override void InitDataLoad()
        {
            DataLoad();
        }
      

     

        protected override void InitGrid()
        {
           
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MenuId");
            MasterGridExControl.MainGrid.AddColumn("MenuName");            
            MasterGridExControl.MainGrid.AddColumn("Active","사용여부");
            
            MasterGridExControl.MainGrid.BestFitColumns();


            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("MenuId");
            DetailGridExControl.MainGrid.AddColumn("TableName", "테이블명");            
            DetailGridExControl.MainGrid.AddColumn("Active", "사용여부");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "TableName", "Active");

            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TableName", ModelService.GetChildList<VI_TABLESIZE>(p => true).ToList(), "TableName", "TableDescription", true);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
           
            string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetChildList< Model.Domain.Menu>(p => (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue)).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
        protected override void MasterFocusedRowChanged()
        {
     
            DetailGridExControl.MainGrid.Clear();
            
            Model.Domain.Menu obj = MasterGridBindingSource.Current as Model.Domain.Menu;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.MenuId==obj.MenuId).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            ModelService.Save();
        }
        protected override void DetailAddRowClicked()
        {

            Model.Domain.Menu obj = MasterGridBindingSource.Current as Model.Domain.Menu;
            if (obj == null) return;
            MENUTOTABLE newobj = new MENUTOTABLE() { MenuId = obj.MenuId };
            DetailGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }
        protected override void DeleteDetailRow()
        {
            Model.Domain.Menu obj = MasterGridBindingSource.Current as Model.Domain.Menu;
            if (obj == null) return;
            MENUTOTABLE dobj = DetailGridBindingSource.Current as MENUTOTABLE;
            if (dobj == null) return;

            DetailGridBindingSource.Remove(dobj);
                ModelService.Delete(dobj);
                
        }
        protected override void DataSave()
        {
           
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
            
            DataLoad();

        }

        
        
    }
}