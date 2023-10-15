using System.Linq;
using System.Windows.Forms;

using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;

using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class ModuleList : HKInc.Service.Base.BaseForm
    {
        BindingSource ModuleSource = new BindingSource();        
        IService<Module> ModuleService = (IService <Module>)ServiceFactory.GetDomainService("Module");

        public ModuleList()
        {
            InitializeComponent();
        }
        
        protected override void InitControls()
        {
            UserRight = MenuFactory.GetUserRight(FormMenu.MenuId, GlobalVariable.UserId);
            PrintGridControl = gridEx1.MainGrid;
        }

        protected override void InitToolbarButton()
        {
            gridEx1.SetToolbarVisible(UserRight.HasEdit);
            gridEx1.SetToolbarButtonVisible(false);
            gridEx1.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            gridEx1.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);

            gridEx1.ActAddRowClicked += GridEx1_ActAddRowClicked;
            gridEx1.ActDeleteRowClicked += GridEx1_ActDeleteRowClicked;
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {            
            gridEx1.MainGrid.AddColumn("ModuleId");
            gridEx1.MainGrid.AddColumn("ModuleName");
            gridEx1.MainGrid.AddColumn("ModuleName2");
            gridEx1.MainGrid.AddColumn("ModuleName3");
            gridEx1.MainGrid.AddColumn("Assembly");
            gridEx1.MainGrid.AddColumn("Description");
            gridEx1.MainGrid.AddColumn("UpdateId");
            gridEx1.MainGrid.AddColumn("UpdateTime");
            gridEx1.MainGrid.AddColumn("CreateId");
            gridEx1.MainGrid.AddColumn("CreateTime");

            gridEx1.MainGrid.SetEditable("Description");

            gridEx1.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            gridEx1.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
            gridEx1.MainGrid.MainView.Columns["Description"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {            
            ModuleService.ReLoad();

            ModuleSource.DataSource = ModuleService.GetList(p => p.ModuleName.Contains(textModuleName.Text) || 
                                                                 p.ModuleName2.Contains(textModuleName.Text) || 
                                                                 p.ModuleName3.Contains(textModuleName.Text))
                                                           .OrderBy(p=>p.ModuleId).ToList();
            gridEx1.DataSource = ModuleSource;

            SetRefreshMessage(gridEx1.MainGrid.RecordCount);

            gridEx1.BestFitColumns();           
        }

        protected override void DataSave()
        {           
            ModuleService.Save();            
            DataLoad();
        }

        #region Event Handler

        private void GridEx1_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            // Show message to confirm
            
            Module obj = ModuleSource.Current as Module;

            if(obj != null)
            {
                ModuleSource.RemoveCurrent(); //gridEx1.MainGrid.MainView.DeleteRow(gridEx1.MainGrid.FocusedRowHandle); // Grid에서 삭제하면 와 같다                
                ModuleService.Delete(obj);   // DbContext에서 삭제   
            }
        }

        private void GridEx1_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {           
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            
            OpenPopup(param);
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null) return;

            if(e.Clicks == 2)
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);
                param.SetValue(PopupParameter.KeyValue, (Module)ModuleSource.Current);

                OpenPopup(param);
            }
        }
        #endregion

        #region Function
        void OpenPopup(PopupDataParam param)
        {            
            param.SetValue(PopupParameter.Service, ModuleService);
            param.SetValue(PopupParameter.UserRight, UserRight);     
            
            IPopupForm form = HKInc.Ui.View.PopupFactory.ProductionPopupFactory.GetPopupForm(HKInc.Ui.View.PopupFactory.ProductionPopupView.Module, param, PopupRefreshCallback);
           
            form.ShowPopup(true);
        }
        #endregion
    }
}