using System.Collections.Generic;
using System.ComponentModel;

using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Service.Forms;
using HKInc.Service.Handler;
using DevExpress.Utils;

namespace HKInc.Service.Controls
{
    [ToolboxItem(true)]
    public partial class TreeListEx : DevExpress.XtraEditors.XtraUserControl
    {
        private Dictionary<GridToolbarButton, BarButton> ButtonList = new Dictionary<GridToolbarButton, BarButton>();

        public event DevExpress.XtraBars.ItemClickEventHandler ActAddRowClicked;
        public event DevExpress.XtraBars.ItemClickEventHandler ActDeleteRowClicked;
        public event DevExpress.XtraBars.ItemClickEventHandler ActExportClicked;
        public event DevExpress.XtraBars.ItemClickEventHandler ActFileChooseClicked;

        private HKInc.Service.Handler.TreeListOptionHandler TreeHandler;

        public TreeListEx()
        {
            InitializeComponent();

            SetToolbarCaption();
            TreeHandler = new Handler.TreeListOptionHandler(treeList);
        }

        void SetToolbarCaption()
        {
            ButtonList.Add(GridToolbarButton.AddRow, new BarButton() { ToolbarButton = barButtonAdd, OnClicked = ActAddRow, ToolbarButtonCaption = "AddRow" });
            ButtonList.Add(GridToolbarButton.DeleteRow, new BarButton() { ToolbarButton = barButtonDelete, OnClicked = ActDeleteRow, ToolbarButtonCaption = "DeleteRow" });
            ButtonList.Add(GridToolbarButton.Export, new BarButton() { ToolbarButton = barButtonExport, OnClicked = ActExport, ToolbarButtonCaption = "Export" });
            ButtonList.Add(GridToolbarButton.FileChoose, new BarButton() { ToolbarButton = barButtonFileChoose, OnClicked = ActFileChoose, ToolbarButtonCaption = "FileChoose" });

            foreach (GridToolbarButton toolbarButton in ButtonList.Keys)
            {
                ButtonList[toolbarButton].ToolbarButton.Tag = (int)toolbarButton;
                ButtonList[toolbarButton].ToolbarButton.ItemClick += ToolbarButtonClick;
            }
        }

        void ToolbarButtonClick(object sender, ItemClickEventArgs e)
        {
            ButtonList[(GridToolbarButton)(e.Item.Tag.GetIntNullToZero())].OnClicked(sender, e);
        }

        void ActAddRow(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ActAddRowClicked?.Invoke(sender, e);
        }

        void ActDeleteRow(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ActDeleteRowClicked?.Invoke(sender, e);
        }

        void ActExport(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ActExportClicked?.Invoke(sender, e);
        }

        void ActFileChoose(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ActFileChooseClicked?.Invoke(sender, e);
        }

        public void BestFitColumns()
        {
            treeList.BestFitColumns();
        }

        public void SetToolbarVisible(bool visible)
        {
            this.barTools.Visible = visible;
        }

        public void SetToolbarButtonVisible(bool visible)
        {
            foreach (GridToolbarButton button in ButtonList.Keys)
                SetToolbarButtonVisible(button, visible);
        }

        public void SetToolbarButtonVisible(GridToolbarButton toolbarButton, bool visible)
        {
            ButtonList[toolbarButton].ToolbarButton.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        public void SetTreeListOption(bool checkBoxVisible = true)
        {
            TreeHandler.SetTreeListOption(checkBoxVisible);
        }

        public TreeListColumn AddColumn(string fieldName, string caption, bool visible = true)
        {
            return TreeHandler.AddColumn(fieldName, caption, visible);
        }

        public TreeListColumn AddColumn(string fieldName, bool visible = true)
        {
            return TreeHandler.AddColumn(fieldName, visible);
        }

        public TreeListColumn AddColumn(string fieldName, DevExpress.Utils.HorzAlignment horzAlignment, bool visible = true)
        {
            return TreeHandler.AddColumn(fieldName, horzAlignment, visible);
        }

        public TreeListColumn AddColumn(string fieldName, string caption, DevExpress.Utils.HorzAlignment horzAlignment, bool visible = true)
        {
            return TreeHandler.AddColumn(fieldName, caption, horzAlignment, visible);
        }
        public TreeListColumn AddColumn(string fieldName, string caption, HorzAlignment horzAlignment, FormatType formatType, string formatString, bool visible = true)
        {
            return TreeHandler.AddColumn(fieldName, caption, horzAlignment, formatType, formatString, visible);
        }

        public void ExpandAll()
        {
            treeList.ExpandAll();
        }

        public void Export()
        {
            HKInc.Service.Helper.ExcelExport.ExportToExcel(treeList);
        }

        [Browsable(false)]
        public object DataSource
        {
            get { return this.treeList.DataSource; }
            set { this.treeList.DataSource = value; }
        }

        [Browsable(false)]
        public DevExpress.XtraTreeList.TreeList TreeList
        {
            get { return treeList; }            
        }
        [Browsable(false)]
        public string ParentFieldName
        {
            get { return this.treeList.ParentFieldName; }
            set { this.treeList.ParentFieldName = value; }               
        }
        [Browsable(false)]
        public string KeyFieldName
        {
            get { return this.treeList.KeyFieldName; }
            set { this.treeList.KeyFieldName = value; }
        }
    }
}
