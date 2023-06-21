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
using System.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using System.Reflection;
using HKInc.Service.Factory;
using DevExpress.XtraTreeList;
using System;

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

        public void SetToolbarShotKeyChange(GridToolbarButton toolbarButton, BarShortcut Key)
        {
            var OldCaption = ButtonList[toolbarButton].ToolbarButton.Caption;
            var OldShotKey = ButtonList[toolbarButton].ToolbarButton.ShortcutKeyDisplayString;
            ButtonList[toolbarButton].ToolbarButton.ShortcutKeyDisplayString = Key.ToString();
            ButtonList[toolbarButton].ToolbarButton.ItemShortcut = new BarShortcut(Key);
            if (OldShotKey != "")
                ButtonList[toolbarButton].ToolbarButton.Caption = OldCaption.Replace(OldShotKey, Key.ToString());
        }

        public void SetToolbarButtonCaption(GridToolbarButton toolbarButton, string caption, Image icon = null)
        {
            ButtonList[toolbarButton].ToolbarButton.Caption = caption;
            if (icon != null)
            {
                ButtonList[toolbarButton].ToolbarButton.ImageOptions.Image = icon;
            }
        }
        public void SetToolbarButtonFont(System.Drawing.Font Font)
        {
            foreach (GridToolbarButton button in ButtonList.Keys)
                SetToolbarButtonFont(button, Font);
        }

        public void SetToolbarButtonFont(GridToolbarButton toolbarButton, System.Drawing.Font Font)
        {
            ButtonList[toolbarButton].ToolbarButton.ItemAppearance.SetFont(Font);
        }

        public void SetToolbarButtonEnable(GridToolbarButton toolbarButton, bool enable)
        {
            ButtonList[toolbarButton].ToolbarButton.Enabled = enable;
        }

        public void SetTreeListOption(bool checkBoxVisible = true)
        {
            TreeHandler.SetTreeListOption(checkBoxVisible);
        }

        public void SetTreeListEditable(bool flag, params string[] column)
        {
            TreeHandler.SetTreeListEditable(flag, column);
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
        public void SetRepositoryItem(string fieldName, RepositoryItem item, bool CustomOnlyNumeric = false)
        {
            treeList.RepositoryItems.Add(item);
            treeList.Columns[fieldName].ColumnEdit = item;
            treeList.Columns[fieldName].ShowButtonMode = ShowButtonModeEnum.ShowForFocusedRow;

            if (item.GetType() == typeof(RepositoryItemSpinEdit))
            {
                RepositoryItemSpinEdit con = item as RepositoryItemSpinEdit;
                con.AllowMouseWheel = true;
                treeList.Columns[fieldName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            }
            else if (item.GetType() == typeof(RepositoryItemPictureEdit))
            {
                treeList.RowHeight = 50;
                treeList.Columns[fieldName].OptionsFilter.AllowAutoFilter = false;
            }
            else if (item.GetType() == typeof(RepositoryItemCheckEdit))
                treeList.Columns[fieldName].OptionsColumn.AllowSort = false;
            else if (item.GetType() == typeof(RepositoryItemTextEdit))
            {
                var con = item as RepositoryItemTextEdit;
                if (CustomOnlyNumeric)
                {
                    con.KeyPress += Con_KeyPress;
                }
            }
        }

        private void Con_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                if (e.KeyChar != 45 && e.KeyChar != 46)
                    e.Handled = true;
            }
        }

        public RepositoryItemSpinEdit SetRepositoryItemSpinEdit(string fieldName, bool spinButton = true, DefaultBoolean allownullinput = DefaultBoolean.Default, string editmask = "n0", bool usemaskasdisplayformat = true)
        {
            RepositoryItemSpinEdit obj = new RepositoryItemSpinEdit();

            obj.AllowNullInput = allownullinput;
            obj.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            if (!string.IsNullOrEmpty(editmask)) obj.Mask.EditMask = editmask;

            obj.Mask.UseMaskAsDisplayFormat = usemaskasdisplayformat;
            obj.Buttons[0].Visible = spinButton;
            SetRepositoryItem(fieldName, obj);
            return obj;
        }

        public RepositoryItemSearchLookUpEdit SetRepositoryItemSearchLookUpEdit(string fieldName, object dataSource, string valueMember = "CodeId", string displayMember = "CodeName", bool addDeleteButton = false, bool addEllipsisButton = false, ButtonPressedEventHandler ellipsisButtonPressedHandler = null, string nullText = "", EventHandler gridLookUpBeforePopupHandler = null, TextEditStyles textEditStyles = TextEditStyles.Standard)
        {
            //RepositoryItemSpinEdit obj = new RepositoryItemSpinEdit();

            //obj.AllowNullInput = allownullinput;
            //obj.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            //if (!string.IsNullOrEmpty(editmask)) obj.Mask.EditMask = editmask;

            //obj.Mask.UseMaskAsDisplayFormat = usemaskasdisplayformat;
            //obj.Buttons[0].Visible = spinButton;
            //SetRepositoryItem(fieldName, obj);
            //return obj;

            RepositoryItemSearchLookUpEdit lookup = new RepositoryItemSearchLookUpEdit()
            {
                ValueMember = valueMember,
                DisplayMember = displayMember
            };

            #region Property

            lookup.View.OptionsView.ShowColumnHeaders = true;
            lookup.NullText = nullText;
            lookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            lookup.View.OptionsBehavior.AllowIncrementalSearch = true;
            lookup.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            lookup.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;            
            lookup.ValueMember = valueMember;
            lookup.DisplayMember = displayMember;
            lookup.TextEditStyle = textEditStyles;

            #endregion

            #region Event

            lookup.EditValueChanged += LookupEditValueChanged;
            lookup.Popup += SearchLookUpEditEx_Popup;
            if (gridLookUpBeforePopupHandler != null) lookup.Popup += gridLookUpBeforePopupHandler;
            lookup.ButtonPressed += SearchLookup_ButtonPressed;
            lookup.KeyDown += SearchLookUpEditEx_KeyDown1;
            #endregion

            if (addDeleteButton)
                AddButton(lookup, ButtonPredefines.Delete);

            if (addEllipsisButton)
                AddButton(lookup, ButtonPredefines.Ellipsis);

            if (ellipsisButtonPressedHandler != null)
                lookup.ButtonPressed += ellipsisButtonPressedHandler;

            //if (fieldName == "ItemCode" || fieldName == "ProductItemCode") //품목LookUp Column Setting
            //{
            //    ItemCodeColumnSetting(lookup, fieldName, valueMember, displayMember);
            //}
            //else
            //{
            //    lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            //    {
            //        FieldName = valueMember,
            //        Caption = HelperFactory.GetLabelConvert().GetLabelText(fieldName),
            //        Visible = true //코드표시
            //    });

            //    lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            //    {
            //        FieldName = displayMember,
            //        Caption = HelperFactory.GetLabelConvert().GetLabelText(fieldName),
            //        Visible = true
            //    });
            //}
            lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = valueMember,
                Caption = HelperFactory.GetLabelConvert().GetLabelText(valueMember),
                Visible = true //코드표시
            });

            lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = displayMember,
                Caption = HelperFactory.GetLabelConvert().GetLabelText(displayMember),
                Visible = true
            });
            lookup.DataSource = dataSource;
            treeList.RepositoryItems.Add(lookup);
            treeList.Columns[fieldName].ColumnEdit = lookup;
            treeList.Columns[fieldName].ShowButtonMode = ShowButtonModeEnum.ShowForFocusedRow;

            return lookup;
        }

        private void LookupEditValueChanged(object sender, EventArgs e)
        {
            treeList.PostEditor();
        }

        private void SearchLookUpEditEx_Popup(object sender, EventArgs e)
        {
            SearchLookUpEdit lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            if (lookup.Properties.DataSource != null)
            {
                Type type = lookup.Properties.DataSource.GetType();
                PropertyInfo[] prop = type.GetProperties();
                Type objType = Type.GetType(prop[2].PropertyType.AssemblyQualifiedName);
                PropertyInfo[] fields = objType.GetProperties();
                if (fields.Any(p => p.Name.Equals("UseYN")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYN] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("UseYN")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYN] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("UseYN")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYN] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("Active")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[Active] == 'Y'";

                //TextEdit editor = ((sender as DevExpress.Utils.Win.IPopupControl).PopupWindow.Controls[2].Controls[0].Controls[7] as TextEdit);
                TextEdit editor = ((sender as DevExpress.Utils.Win.IPopupControl).PopupWindow.Controls[3].Controls[0].Controls[7] as TextEdit);
                editor.KeyDown += SearchLookUpEditEx_KeyDown;
            }
        }

        private void SearchLookUpEditEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEdit = sender as TextEdit;
                SearchLookUpEdit lookup = textEdit.Tag as SearchLookUpEdit;
                if (lookup == null) return;

                if (lookup.Properties.View.RowCount == 1)
                {
                    lookup.Properties.View.FocusedRowHandle = lookup.Properties.View.GetVisibleRowHandle(0);
                    lookup.EditValue = lookup.Properties.View.GetFocusedRowCellValue(lookup.Properties.ValueMember);
                    lookup.ClosePopup();
                }
            }
        }

        private void SearchLookup_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            if (e.Button.Kind == ButtonPredefines.Delete)
                edit.EditValue = null;
        }

        private void SearchLookUpEditEx_KeyDown1(object sender, KeyEventArgs e)
        {
            SearchLookUpEdit lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (lookup.Text != "")
                    Clipboard.SetText(lookup.Text.GetNullToEmpty());
                else
                    Clipboard.Clear();
            }
            else if (e.KeyCode != Keys.Escape && e.KeyCode != Keys.Right && e.KeyCode != Keys.Left && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                lookup.ShowPopup();
            }
        }

        private void AddButton(RepositoryItemSearchLookUpEdit lookup, ButtonPredefines buttonKind)
        {
            if (this.Enabled)
            {
                foreach (var item in lookup.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == buttonKind))
                    lookup.Buttons.RemoveAt(item.Index);

                lookup.Buttons.Add(new EditorButton() { Kind = buttonKind });
            }
        }

        public void ExpandAll()
        {
            treeList.ExpandAll();
        }

        public void Clear()
        {
            if (treeList.DataSource != null)
            {
                if (treeList.DataSource is DataTable)
                    (treeList.DataSource as DataTable).Rows.Clear();
                else
                    DataSource = null;

                treeList.RefreshDataSource();
            }
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
