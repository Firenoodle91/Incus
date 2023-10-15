using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;

using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Service.Forms;

namespace HKInc.Service.Controls
{
    [ToolboxItem(true)]
    public partial class GridEx : DevExpress.XtraEditors.XtraUserControl
    {
        private Dictionary<GridToolbarButton, BarButton> ButtonList = new Dictionary<GridToolbarButton, BarButton>();

        public event DevExpress.XtraBars.ItemClickEventHandler ActAddRowClicked;
        public event DevExpress.XtraBars.ItemClickEventHandler ActDeleteRowClicked;
        public event DevExpress.XtraBars.ItemClickEventHandler ActExportClicked;
        public event DevExpress.XtraBars.ItemClickEventHandler ActFileChooseClicked;

        public GridEx()
        {
            InitializeComponent();

            SetToolbarCaption();
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
            Factory.LogFactory.GetMenuEventService().UpdateMenuEventLog(this.Menu.GetIntNullToZero(), e.Item.Name);
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
            gridLink.BestFitColumns();
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

        public void SetToolbarButtonCaption(GridToolbarButton toolbarButton, string caption)
        {
            ButtonList[toolbarButton].ToolbarButton.Caption = caption;
        }
        public void SetToolbarButtonFont(System.Drawing.Font Font)
        {
            foreach (GridToolbarButton button in ButtonList.Keys)
                SetToolbarButtonFont(button,Font);
        }
        public void SetToolbarButtonFont(GridToolbarButton toolbarButton, System.Drawing.Font Font)
        {
            ButtonList[toolbarButton].ToolbarButton.ItemAppearance.SetFont(Font);         
        }

        /// <summary>
        /// 한개의 Grid에 적용 시 모든 Grid에 적용됨
        /// </summary>
        /// <param name="flag"></param>
        public void SetToolbarButtonLargeIconVisible(bool flag)
        {
            barTools.Manager.LargeIcons = flag;
        }


        [Browsable(false)]
        public HKInc.Utils.Enum.GridViewType ViewType
        {
            get { return gridLink.ViewType; }
            set { gridLink.ViewType = value; }
        }

        [Browsable(false)]
        public GridControlEx MainGrid
        {
            get { return gridLink; }
        }

        [Browsable(false)]
        public object DataSource
        {
            get { return this.gridLink.DataSource; }
            set { this.gridLink.DataSource = value; }
        }

        [Browsable(false)]
        public Bar BarTools
        {
            get { return barTools; }
        }

        //화면별 이벤트 로그 남기기위해 추가
        public decimal mesMenuId;
        public decimal Menu
        {
            get { return mesMenuId; }
            set { this.mesMenuId = value; }
        }

    }
}
