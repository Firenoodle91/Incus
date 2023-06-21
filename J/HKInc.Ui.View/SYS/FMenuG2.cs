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
    
    public partial class FMenuG2 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<GroupMenu> ModelService = (IService<GroupMenu>)ServiceFactory.GetDomainService("GroupMenu");
        protected BindingSource TreeListBindingSource = new BindingSource();  // Control에 사용한 BindingSource 다자인때 설정한 것이므로 컨스트럭터에서 설정한다.
        protected HKInc.Service.Controls.TreeListEx TreeListExControl;
        Boolean isnew = false;
        public FMenuG2()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            TreeListExControl = treeListEx1;
            OutPutRadioGroup = radioGroup1;
            RadioGroupType = RadioGroupType.ActiveAll;
            MasterGridExControl.MainGrid.MainView.CellValueChanging += MainView_CellValueChanging;
            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            TreeListExControl.TreeList.DoubleClick += TreeList_DoubleClick;
        }

        private void TreeList_DoubleClick(object sender, EventArgs e)
        {
           // HKInc.Ui.Model.Domain.Menu obj = TreeListBindingSource.Current as HKInc.Ui.Model.Domain.Menu;
            UserGroup obj = MasterGridBindingSource.Current as UserGroup;
            if (obj == null) return;
            HKInc.Ui.Model.Domain.Menu mm = TreeListBindingSource.Current as HKInc.Ui.Model.Domain.Menu;
            if (mm == null) return;
            List<HKInc.Ui.Model.Domain.Menu> nml = TreeListBindingSource.DataSource as List<HKInc.Ui.Model.Domain.Menu>;
            List<GroupMenu> oldlist = DetailGridBindingSource.DataSource as List<GroupMenu>;
            if (oldlist.Count(p => p.MenuId == mm.MenuId) == 1) return;
            GroupMenu newobj = new GroupMenu() { UserGroupId = obj.UserGroupId, MenuId = mm.MenuId,
                Read = "N"
                                               ,
                Insert = "N"
                                               ,
                Write = "N"
                                               ,
                Export = "N"
                                               ,
                Print = "N"
            };

            if (nml.Count(p => p.UpperMenuId == mm.MenuId) <= 1)
            {
                TreeListBindingSource.Remove(mm);
            }
            DetailGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            if (mm.UpperMenuId.GetDecimalNullToNull() != null)
            {
                decimal upmid = Convert.ToDecimal(mm.UpperMenuId.GetDecimalNullToNull());
                if (oldlist.Count(p => p.MenuId == upmid) == 1) return;
                GroupMenu newUPobj = new GroupMenu() { UserGroupId = obj.UserGroupId, MenuId = upmid,
                    Read = "N"
                                               ,
                    Insert = "N"
                                               ,
                    Write = "N"
                                               ,
                    Export = "N"
                                               ,
                    Print = "N"
                };
                DetailGridBindingSource.Add(newUPobj);
                ModelService.Insert(newUPobj);
            }
            TreeListExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
            isnew = true;
        }
        protected override void InitDataLoad()
        {
            DataLoad();
        }
        private void MainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            UserGroup obj = MasterGridBindingSource.Current as UserGroup;
            if (obj == null) return;
            if (e.Clicks == 2)
            {

                FUserGList fm = new FUserGList(obj);
                fm.ShowDialog();

            }
        }

        private void MainView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name.ToString() != "Active") return;

            GridView v = sender as GridView;
            UserGroup obj = MasterGridBindingSource.Current as UserGroup;
            if (e.Value.GetNullToEmpty() != "N") return;
            string aa = v.ActiveEditor.OldEditValue.GetNullToEmpty();
            int cnt = ModelService.GetList(p => p.UserGroupId == obj.UserGroupId).Count();
            if (cnt > 0)
            {

                MessageBox.Show("부여된 권한이 있어서 그룹을 삭제 할 수 없습니다.");
                obj.Active = aa;


            }
            else {

                if (obj.Active == "Y") { obj.Active = "N"; }
                else { obj.Active = "Y"; }

            }
            MasterGridExControl.BestFitColumns();

           
        }

        protected override void InitGrid()
        {
           
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);



            MasterGridExControl.MainGrid.AddColumn("GroupMenuId",false);
            MasterGridExControl.MainGrid.AddColumn("UserGroupId");
            MasterGridExControl.MainGrid.AddColumn("UserGroupName");
            MasterGridExControl.MainGrid.AddColumn("Active","사용여부");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetEditable("UserGroupName", "Active");
            MasterGridExControl.MainGrid.BestFitColumns();


            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("GroupMenuId", false);
            DetailGridExControl.MainGrid.AddColumn("MenuId");
            DetailGridExControl.MainGrid.AddColumn("UserGroupId", false);
            DetailGridExControl.MainGrid.AddColumn("Read");
            DetailGridExControl.MainGrid.AddColumn("Insert");
            DetailGridExControl.MainGrid.AddColumn("Write");
            DetailGridExControl.MainGrid.AddColumn("Export");
            DetailGridExControl.MainGrid.AddColumn("Print");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetEditable( "Read", "Insert", "Write", "Export", "Print");
            DetailGridExControl.MainGrid.BestFitColumns();


   
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = HKInc.Utils.Common.GlobalVariable.IconImageCollection;
            //TreeListExControl.SetToolbarVisible(false);
            TreeListExControl.SetToolbarButtonVisible(false);
            TreeListExControl.AddColumn("MenuId", false);
            TreeListExControl.AddColumn("UpperMenuId", false);
            TreeListExControl.AddColumn("MenuName");


            TreeListExControl.ParentFieldName = "UpperMenuId";
            TreeListExControl.KeyFieldName = "MenuId";

            TreeListExControl.BestFitColumns();

            TreeListExControl.TreeList.GetStateImage += TreeList_GetStateImage;



        }
        void TreeList_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree == null) return;

            e.NodeImageIndex = e.Node["IconIndex"].GetIntNullToZero();
        }
        protected override void InitRepository()
        {
            //List<UserGroup> userGroupList = ModelService.GetChildList<UserGroup>(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active));

            //MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("UserGroupId", userGroupList, "UserGroupId", "UserGroupName");

            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            List<HKInc.Ui.Model.Domain.Menu> menuList = ModelService.GetChildList<HKInc.Ui.Model.Domain.Menu>(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active));

            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MenuId", menuList, "MenuId", "MenuName");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Read", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Insert", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Write", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Export", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Print", "N");

        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
           
            string radioValue = radioGroup1.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetChildList<UserGroup>(p => (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue)).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
        protected override void MasterFocusedRowChanged()
        {
            if (isnew) {

                DialogResult dlg = MessageBox.Show("수정된 내용이 있습니다. 저장하시겠습니까?","", MessageBoxButtons.OKCancel);
                if (dlg == DialogResult.OK)
                {
                    DetailGridBindingSource.EndEdit();
                    ModelService.Save();
                    isnew = false;
                }
                else { isnew = false; }
            }
            DetailGridExControl.MainGrid.Clear();
            //TreeListExControl.DataSource = null;
            UserGroup obj = MasterGridBindingSource.Current as UserGroup;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.UserGroupId==obj.UserGroupId).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();

            TreeListBindingSource.DataSource = ModelService.GetChildList<HKInc.Ui.Model.Domain.Menu>(p =>1==1)
                                                        .OrderBy(p => p.UpperMenuId)
                                                        .OrderBy(p => p.SortOrder).ToList();
          foreach(GroupMenu m in DetailGridBindingSource)
            {

                HKInc.Ui.Model.Domain.Menu nm = ModelService.GetChildList<HKInc.Ui.Model.Domain.Menu>(p => p.MenuId == m.MenuId).FirstOrDefault();
                if (ModelService.GetChildList<HKInc.Ui.Model.Domain.Menu>(p => p.UpperMenuId == nm.MenuId).Count() == 0)
                {
                    TreeListBindingSource.Remove(nm);
                }
            }

            TreeListExControl.DataSource = TreeListBindingSource;

            SetRefreshMessage(TreeListBindingSource.Count);

            TreeListExControl.BestFitColumns();
           TreeListExControl.ExpandAll();
        }
        protected override void AddRowClicked()
        {
       
           UserGroup obj = new UserGroup() {Active="Y" };
            MasterGridBindingSource.Add(obj);
            ModelService.InsertChild<UserGroup>(obj);
        }
        protected override void DeleteRow()
        {
            UserGroup obj = MasterGridBindingSource.Current as UserGroup;
            int cnt = ModelService.GetList(p => p.UserGroupId == obj.UserGroupId).Count();
            if (cnt > 0)
            {
                MessageBox.Show("부여된 권한이 있어서 그룹을 삭제 할 수 없습니다.");

            }
            else {
                obj.Active = "N";
                MasterGridBindingSource.Remove(obj);
                ModelService.UpdateChild<UserGroup>(obj);
            }
        }
        protected override void DetailAddRowClicked()
        {

            UserGroup obj = MasterGridBindingSource.Current as UserGroup;
            if (obj == null) return;
            HKInc.Ui.Model.Domain.Menu mm = TreeListBindingSource.Current as HKInc.Ui.Model.Domain.Menu;
            if (mm == null) return;
            List<HKInc.Ui.Model.Domain.Menu> nml = TreeListBindingSource.DataSource as List<HKInc.Ui.Model.Domain.Menu>;
            List<GroupMenu> oldlist = DetailGridBindingSource.DataSource as List<GroupMenu>;
            if (oldlist.Count(p => p.MenuId == mm.MenuId) == 1) return;
            GroupMenu newobj = new GroupMenu() { UserGroupId = obj.UserGroupId
                                               , MenuId = mm.MenuId              
                                               ,  Read= "N"               
                                               , Insert="N"
                                               , Write= "N"
                                               , Export= "N"
                                               , Print="N"         };
          
            if (nml.Count(p => p.UpperMenuId == mm.MenuId) <= 1)
            {
                TreeListBindingSource.Remove(mm);
            }
            DetailGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            if (mm.UpperMenuId.GetDecimalNullToNull() != null)
            {
                decimal upmid =Convert.ToDecimal(mm.UpperMenuId.GetDecimalNullToNull());
                if (oldlist.Count(p => p.MenuId == upmid) == 1) return;
                GroupMenu newUPobj = new GroupMenu() { UserGroupId = obj.UserGroupId, MenuId = upmid,
                    Read = "N"
                                               ,
                    Insert = "N"
                                               ,
                    Write = "N"
                                               ,
                    Export = "N"
                                               ,
                    Print = "N"
                };
                DetailGridBindingSource.Add(newUPobj);
                ModelService.Insert(newUPobj);
            }
            TreeListExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
            isnew = true;
        }
        protected override void DeleteDetailRow()
        {
            GroupMenu obj = DetailGridBindingSource.Current as GroupMenu;
            if (obj == null) return;

            List<HKInc.Ui.Model.Domain.Menu> nml = TreeListBindingSource.DataSource as List<HKInc.Ui.Model.Domain.Menu>;
            HKInc.Ui.Model.Domain.Menu nm = ModelService.GetChildList<HKInc.Ui.Model.Domain.Menu>(p => p.MenuId == obj.MenuId).FirstOrDefault();
            if (nml.Count(p => p.MenuId == nm.MenuId) == 0)
            {
                nml.Add(nm);
                //TreeListBindingSource.Add(nm);

                TreeListBindingSource.DataSource = null;
                TreeListBindingSource.DataSource = nml;

               
            }
                DetailGridBindingSource.Remove(obj);
                ModelService.Delete(obj);
                isnew = true;
            TreeListExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
        }
        protected override void DataSave()
        {
           
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
            isnew = false;
            DataLoad();

        }

        
        
    }
}