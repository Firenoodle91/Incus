using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using DevExpress.XtraTreeList;

namespace HKInc.Ui.View.SYS
{
    public partial class UserGroupSelect : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<UserGroup> UserGroupService;
        BindingSource bindingSource = new BindingSource();

        public UserGroupSelect()
        {
            InitializeComponent();
        }

        public UserGroupSelect(PopupDataParam parameter, PopupCallback callback) :this()
        {            
            this.PopupParam = parameter;
            this.Callback = callback;

            UserGroupService = (IService<UserGroup>)ServiceFactory.GetDomainService("UserGroup");
        }

        protected override void InitControls()
        {
            base.InitControls();
            treeList.DoubleClick += TreeList_DoubleClick;
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Close, true);            
        }

        protected override void InitGrid()
        {
            HKInc.Service.Handler.TreeListOptionHandler treeListHandler = new Service.Handler.TreeListOptionHandler(treeList);

            treeListHandler.SetTreeListOption(true);

            treeListHandler.AddColumn("UserGroupId", false);
            treeListHandler.AddColumn("UpperUserGroupId", false);
            treeListHandler.AddColumn(HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("UserGroup"));
            treeListHandler.AddColumn("Description");
                                   
            treeList.ParentFieldName = "UpperUserGroupId";
            treeList.KeyFieldName = "UserGroupId";

            treeList.BestFitColumns();
        }
        
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            bindingSource.DataSource = UserGroupService.GetList(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active))
                                                       .OrderBy(p=>p.UserGroupName).ToList();
            treeList.DataSource = bindingSource;

            treeList.ExpandAll();            
        }

        protected override void ActClose()
        {
            List<decimal> userGroupIdList = new List<decimal>();            
            List<TreeListNode> checkedNodes = treeList.GetAllCheckedNodes();

            TreeListColumn idColumn = treeList.Columns["UserGroupId"];
            foreach (var node in checkedNodes)
            {
                if (!node.HasChildren)
                    userGroupIdList.Add((decimal)node.GetValue(idColumn));
            }

            PopupClose(userGroupIdList);
        }

        private void TreeList_DoubleClick(object sender, System.EventArgs e)
        {
            TreeList tree = sender as TreeList;
            TreeListHitInfo hint = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
            if (hint.Node != null)
            {
                List<decimal> userGroupIdList = new List<decimal>();
                List<TreeListNode> checkedNodes = treeList.GetAllCheckedNodes();

                TreeListColumn idColumn = treeList.Columns["UserGroupId"];
                if (!hint.Node.HasChildren)
                {
                    userGroupIdList.Add((decimal)hint.Node.GetValue(idColumn));
                    PopupClose(userGroupIdList);
                }


            }
        }

        private void PopupClose(List<decimal> userGroupIdList)
        {
            DataParam map = new DataParam();
            map.SetValue("UserGroupIdList", userGroupIdList);

            PopupDataParam param = new PopupDataParam();
            param.SetValue(Utils.Enum.PopupParameter.DataParam, map);

            ReturnPopupArgument = new PopupArgument(param);
            base.ActClose();
        }

    }
}
