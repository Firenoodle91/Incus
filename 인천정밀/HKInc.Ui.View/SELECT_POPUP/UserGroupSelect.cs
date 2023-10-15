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

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 권한그룹관리 선택창
    /// </summary>
    public partial class UserGroupSelect : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<UserGroup> UserGroupService;
        BindingSource bindingSource = new BindingSource();
        private string scmyn;

        public UserGroupSelect()
        {
            InitializeComponent();
        }

        public UserGroupSelect(PopupDataParam parameter, PopupCallback callback) :this()
        {            
            this.PopupParam = parameter;
            this.Callback = callback;

            if (parameter.ContainsKey(PopupParameter.KeyValue))
                scmyn = (string)parameter.GetValue(PopupParameter.KeyValue);


            UserGroupService = (IService<UserGroup>)ServiceFactory.GetDomainService("UserGroup");

            this.Text = LabelConvert.GetLabelText("AuthGroupList");
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

            treeListHandler.AddColumn("UserGroupId", LabelConvert.GetLabelText("AuthGroupId"), false);
            treeListHandler.AddColumn("UpperUserGroupId", false);
            treeListHandler.AddColumn(Service.Helper.DataConvert.GetCultureDataFieldName("UserGroupName", "UserGroupName2", "UserGroupName3"), LabelConvert.GetLabelText("AuthGroupName"));
            treeListHandler.AddColumn("Description", LabelConvert.GetLabelText("Memo"));
                                   
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
            if(scmyn == "01")
            {
                bindingSource.DataSource = UserGroupService.GetList(p => (p.Active == "Y" || string.IsNullOrEmpty(p.Active)) && p.UserGroupId == 15)
                                                       .OrderBy(p => p.UserGroupName).ToList();
            }
            else
            {
                bindingSource.DataSource = UserGroupService.GetList(p => p.Active == "Y" || string.IsNullOrEmpty(p.Active))
                                                       .OrderBy(p => p.UserGroupName).ToList();
            }

            
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
