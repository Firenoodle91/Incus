﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Data;

using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

using HKInc.Service.Factory;
using HKInc.Utils.Class;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors.Repository;

namespace HKInc.Service.Controls
{
    [ToolboxItem(true)]
    public partial class GridLookUpEditEx : GridLookUpEdit
    {
        private ToolTipController defController = ToolTipController.DefaultController;

        private bool _isRequired = false;

        public bool isRequired
        {
            get
            {
                return _isRequired;
            }
            set
            {
                _isRequired = value;

                this.SetColor();
            }
        }

        private string ActiveFilter = string.Empty;

        public object Constraint { get; set; }

        public object Value_1 { get; set; }

        #region Constructor

        public GridLookUpEditEx()
        {
            InitializeComponent();

            this.ButtonPressed += GridLookUpEditEx_ButtonPressed;
            this.EnabledChanged += LookUpEditEx_EnabledChanged;
            this.Popup += GridLookUpEditEx_Popup;
            this.QueryPopUp += GridLookUpEditEx_QueryPopUp;
            this.KeyDown += GridLookUpEditEx_KeyDown;
            this.EditValueChanged += GridLookUpEditEx_EditValueChanged;
        }
        #endregion

        #region Extended Properties

        public object DataSource
        {
            get { return this.Properties.DataSource; }
            set
            {
                this.BeginUpdate();

                this.Properties.DataSource = value;
                this.Properties.View.Columns.Clear();
                this.AddColumn(this.ValueMember, 1, true); 
                this.AddColumn(this.DisplayMember, 2, true);

                this.EndUpdate();
            }
        }

        public string ValueMember
        {
            get { return this.Properties.ValueMember; }
            set { this.Properties.ValueMember = value; }
        }

        public string DisplayMember
        {
            get { return this.Properties.DisplayMember; }
            set { this.Properties.DisplayMember = value; }
        }

        public GridColumnCollection Columns
        {
            get { return this.Properties.View.Columns; }
        }

        public string NullText
        {
            get { return this.Properties.NullText; }
            set { this.Properties.NullText = value; }
        }

        public int RowCount
        {
            get { return this.Properties.View.RowCount; }
        }

        public int SelectedIndex
        {
            get
            {
                object key = this.EditValue;
                return this.Properties.GetIndexByKeyValue(key);
            }
            set
            {
                object keyValue = this.Properties.GetKeyValue(value);
                this.EditValue = keyValue;
            }
        }

        #endregion

        #region pulbic functions

        public void Clear()
        {
            this.EditValue = null;

            //Change the field via reflection
            FieldInfo field = typeof(GridLookUpEdit).GetField("fOldEditValue", BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
                field.SetValue(this, this.EditValue);
        }

        public void SetDefault(bool bDeleteButtonVisible = true, string valueMember = "CodeVal", string displayMember = "CodeName", object dataSource = null, TextEditStyles textEditStyles = TextEditStyles.Standard, HorzAlignment horz = HorzAlignment.Near, EventHandler PopupEvent = null)
        {
            SetHAlignment(horz);

            Init(valueMember, displayMember, textEditStyles);

            if (!this.Enabled) ClearButton(ButtonPredefines.Combo);

            if (bDeleteButtonVisible) AddButton(ButtonPredefines.Delete);

            if (dataSource != null) DataSource = dataSource;

            if (PopupEvent != null) this.Popup += PopupEvent;

            if (valueMember == "ItemCode" || valueMember == "ProductItemcode") //품목LookUp Column Setting
            {
                ItemCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
            else
            {
                Properties.View.Columns[valueMember].Visible = false;
            }
        }
        
        public void SetEnable(bool bEnable = false)
        {
            Properties.AllowFocused = bEnable;
            Properties.ReadOnly = !bEnable;

            if (Properties.Buttons.Count > 0)
            {
                foreach (EditorButton btn in Properties.Buttons)
                {
                    btn.Visible = bEnable;
                }
            }
        }

        public void ClearButton(ButtonPredefines buttonKind)
        {
            foreach (var item in this.Properties.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == buttonKind))
            {
                this.Properties.Buttons[item.Index].Visible = false;
            }
        }

        public void AddButton(ButtonPredefines buttonKind)
        {
            if (this.Enabled)
            {
                if (!this.ReadOnly)
                {
                    foreach (var item in this.Properties.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == buttonKind))
                    {
                        this.Properties.Buttons.RemoveAt(item.Index);
                    }

                    this.Properties.Buttons.Add(new EditorButton() { Kind = buttonKind });
                }
            }
            
        }

        #endregion

        #region functions

        private void Init(string valueMember, string displayMember, TextEditStyles textEditStyles)
        {
            Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            Properties.PopupWidthMode = PopupWidthMode.ContentWidth;
            Properties.AutoComplete = true;
            Properties.View.OptionsBehavior.AllowIncrementalSearch = true;
            Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            Properties.PopupFormSize = new Size(this.Width, 150);
            Properties.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            Properties.ValueMember = valueMember;            
            Properties.DisplayMember = displayMember;
            Properties.View.OptionsView.ShowColumnHeaders = false;
            Properties.TextEditStyle = textEditStyles;

            Properties.PopupFilterMode = PopupFilterMode.Contains;
            Properties.ImmediatePopup = true;
            //Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            ClearButton(ButtonPredefines.Delete);
        }

        private void ItemCodeColumnSetting(RepositoryItemGridLookUpEdit edit, string fieldName, string valueMember, string displayMember)
        {
            edit.View.OptionsView.ShowColumnHeaders = true;
            edit.PopupFormMinSize = new Size(400, 300);
            edit.View.Columns[0].Visible = false;
            edit.View.Columns[1].Visible = false;
            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = valueMember,
                Caption = "품목코드",
                Visible = true //코드표시
            });

            if (displayMember == "ItemCode")
            {
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm1",
                    Caption = "품번",
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm",
                    Caption = "품명",
                    Visible = true
                });
            }
            else
            {
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = displayMember,
                    Caption = "품번",
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm",
                    Caption = "품명",
                    Visible = true
                });
            }
        }
        public void ShowColumnHeaders(bool Visible)
        {
            Properties.View.OptionsView.ShowColumnHeaders = Visible;
        }

        /// <summary>
        /// SetDefault 위에 위치해야 함 
        /// </summary>
        /// <param name="horz"></param>
        public void SetHAlignment(HorzAlignment horz = HorzAlignment.Default)
        {
            GridView gridView = new GridView();
            gridView.Appearance.Row.Options.UseTextOptions = true;
            gridView.Appearance.Row.TextOptions.HAlignment = horz;
            gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView.Appearance.HeaderPanel.TextOptions.HAlignment = horz;
            //gridView.Appearance.HeaderPanel.TextOptions.HAlignment = horz;            
            gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView.Name = "gridView";
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ShowIndicator = false;            
            Properties.PopupView = gridView;

            Properties.Appearance.TextOptions.HAlignment = horz;
            Properties.AppearanceDropDown.TextOptions.HAlignment = horz;
            
        }

        public void SetFontSize(Font font)
        {
            foreach (AppearanceObject ap in Properties.View.Appearance)
                ap.Font = font;
            Properties.Appearance.Font = font;
            Properties.AppearanceDisabled.Font = font;
            Properties.AppearanceReadOnly.Font = font;

            //foreach (EditorButton button in Properties.Buttons)
            //    button.Appearance.FontSizeDelta = 20;
        }     
        
        public string GetGridFilter()
        {
            return ActiveFilter;
        }

        public void SetGridFilter(string filterString)
        {
            if (DataSource != null)
            {
                // 마스트코드인경우 활성 코드만 표시한다
                Type type = this.Properties.DataSource.GetType();

                FieldInfo[] fields = type.GetFields();

                if (fields.Any(p => p.Name.Equals("UseFlag")))
                    ActiveFilter = string.Format("[UseFlag] == 'Y' &&  {0}", filterString);
                else if (fields.Any(p => p.Name.Equals("UseYn")))
                    ActiveFilter = string.Format("[UseYn] == 'Y' && {0}", filterString);
                else if (fields.Any(p => p.Name.Equals("Active")))
                    ActiveFilter = string.Format("[Active] == 'Y' && {0}", filterString);
                else
                    ActiveFilter = filterString;
            }
        }

        private void AddColumn(string fieldName, int visibleIndex, bool isVisible)
        {
            AddColumn(fieldName, HelperFactory.GetLabelConvert().GetLabelText(fieldName), visibleIndex, isVisible);
        }

        public void AddColumnDisplay(string fieldName, int visibleIndex, bool isVisible)
        {
            AddColumn(fieldName, HelperFactory.GetLabelConvert().GetLabelText(fieldName), visibleIndex, isVisible);
        }

        public void AddColumnDisplay(string fieldName, string displayName, int visibleIndex, bool isVisible)
        {
            AddColumn(fieldName, HelperFactory.GetLabelConvert().GetLabelText(displayName), visibleIndex, isVisible);
        }

        public void ColumnCpationChange(string fieldName, string displayName)
        {
            Properties.View.Columns[fieldName].Caption = displayName;
        }

        public void ColumnVisibleIndexChange(string fieldName, int visibleIndex)
        {
            Properties.View.Columns[fieldName].VisibleIndex = visibleIndex;
        }

        private void AddColumn(string fieldName, string caption, int visibleIndex, bool isVisible)
        {
            if (!string.IsNullOrEmpty(fieldName))
            {
                this.Properties.View.Columns.Add(new GridColumn()
                {
                    FieldName = fieldName,
                    Caption = caption,
                    VisibleIndex = visibleIndex
                });
                this.Properties.View.Columns[fieldName].Visible = isVisible;
            }
        }

        public void SetColumnVisible(string fieldName, bool isVisible)
        {
            //foreach (GridColumn col in Properties.View.Columns)
            //{
            //    if (col.FieldName.Equals(fieldName))
            //        col.Visible = isVisible;
            //}
            Properties.View.Columns[fieldName].Visible = isVisible;
        }

        /// <summary>
        /// 속성에 따른 배경색 결정
        /// </summary>
        private void SetColor()
        {
            if (this.Enabled == true)
            {
                //필수
                if (_isRequired == true)
                {
                    this.Properties.Appearance.BackColor = Color.LightGoldenrodYellow;

                    this.Properties.Appearance.ForeColor = Color.Black;

                    this.Properties.Appearance.Options.UseBackColor = true;

                    this.Properties.Appearance.Options.UseForeColor = true;
                }
                else
                {
                    this.Properties.Appearance.BackColor = Color.White;

                    this.Properties.Appearance.ForeColor = Color.Black;

                    this.Properties.Appearance.Options.UseBackColor = false;

                    this.Properties.Appearance.Options.UseForeColor = false;
                }
            }
        }

        public bool RequireCheck()
        {
            if (this._isRequired && string.IsNullOrEmpty(this.EditValue.GetNullToEmpty()))
            {                                
                defController.Appearance.BackColor = Color.AntiqueWhite;
                defController.ShowBeak = true;                 
                defController.ShowHint("필수 항목입니다.", this, ToolTipLocation.RightTop);
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Event Handler

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        private void GridLookUpEditEx_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (e.KeyData == (System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V))
            {
                e.Handled = true;
                BeginInvoke(new Action(() => {
                    edit.AutoSearchText = edit.Text;
                    edit.ShowPopup();
                }));
            }
        }

        private void GridLookUpEditEx_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                this.EditValue = null;     

                //Change the field via reflection
                FieldInfo field = typeof(GridLookUpEdit).GetField("fOldEditValue", BindingFlags.Instance | BindingFlags.NonPublic);

                if (field != null)
                    field.SetValue(this, this.EditValue);
            }
            
        }        

        private void LookUpEditEx_EnabledChanged(object sender, EventArgs e)
        {
            GridLookUpEditEx lup = sender as GridLookUpEditEx;

            for (int i = 0; i < lup.Properties.Buttons.Count; i++)
            {
                if (lup.Properties.Buttons[i].Kind == ButtonPredefines.Search)
                    lup.Properties.Buttons[i].Visible = true; 
                else
                    lup.Properties.Buttons[i].Visible = lup.Enabled;
            }            
        }
        
        private void GridLookUpEditEx_Popup(object sender, EventArgs e)
        {            
            if (DataSource != null)
            {
                // 마스트코드인경우 활성 코드만 표시한다
                //Type type = this.Properties.DataSource.GetType();

                //FieldInfo[] fields = type.GetFields();

                //if (fields.Any(p => p.Name.Equals("UseFlag")))
                //    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[UseFlag] == 'Y'" : ActiveFilter;
                //else if (fields.Any(p => p.Name.Equals("UseYn")))
                //    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[UseYn] == 'Y'" : ActiveFilter;
                //else if (fields.Any(p => p.Name.Equals("Active")))
                //    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[Active] == 'Y'" : ActiveFilter;
                //else if(!string.IsNullOrEmpty(ActiveFilter))
                //    Properties.View.ActiveFilter.NonColumnFilter = ActiveFilter;

                Type type = this.Properties.DataSource.GetType();
                PropertyInfo[] prop = type.GetProperties();
                Type objType = Type.GetType(prop[2].PropertyType.AssemblyQualifiedName);
                PropertyInfo[] fields = objType.GetProperties();
                if (fields.Any(p => p.Name.Equals("UseFlag")))
                    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[UseFlag] == 'Y'" : ActiveFilter;
                else if (fields.Any(p => p.Name.Equals("UseYn")))
                    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[UseYn] == 'Y'" : ActiveFilter;
                else if (fields.Any(p => p.Name.Equals("Active")))
                    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[Active] == 'Y'" : ActiveFilter;
                else if (!string.IsNullOrEmpty(ActiveFilter))
                    Properties.View.ActiveFilter.NonColumnFilter = ActiveFilter;
            }
        }

        private void GridLookUpEditEx_QueryPopUp(object sender, CancelEventArgs e)
        {
            //Properties.PopupFormSize = new Size(this.Width, 150);

            var lookUp = sender as GridLookUpEdit;
            var currentView = Properties.View as GridView;
            var controller = currentView.DataController as DevExpress.Data.GridDataController;
            var opType = Properties.PopupFilterMode == PopupFilterMode.StartsWith ? DevExpress.Data.Filtering.FunctionOperatorType.StartsWith :
                                                                                    DevExpress.Data.Filtering.FunctionOperatorType.Contains;
            var op = new DevExpress.Data.Filtering.FunctionOperator(opType, new DevExpress.Data.Filtering.OperandProperty(Properties.DisplayMember),
                                                                                                                                            lookUp.Text);
            controller.FilterExpression = DevExpress.Data.Filtering.CriteriaOperator.ToString(op);
            if (controller.VisibleCount == 0)
            {
                e.Cancel = true;
            }
            else
            {
                lookUp.Properties.PopupFormSize = new Size(lookUp.Width, 300);
                //lookUp.Properties.PopupFormSize = new Size(lookUp.Width, 
                //    (currentView.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo).CalcRealViewHeight(new Rectangle(0, 0, 500, 500)));
            }
        }

        private void GridLookUpEditEx_EditValueChanged(object sender, EventArgs e)
        {
            if(this.Properties.TextEditStyle == TextEditStyles.Standard)
            {
                if (string.IsNullOrEmpty(this.EditValue.GetNullToEmpty()))
                {
                    Clear();
                }
            }
        }

        #endregion
    }

    public class MyGridLookUpEditBestPopupFormSizeHelper
    {
        private Size _LastBestSize;

        private readonly GridLookUpEdit _Edit;

        public MyGridLookUpEditBestPopupFormSizeHelper(GridLookUpEdit edit)
        {
            _Edit = edit;
            int initialHeight = 800;
            _Edit.Properties.PopupFormSize = new Size(500, initialHeight);
            _Edit.Popup += _Edit_Popup;
            _Edit.Properties.View.OptionsView.ColumnAutoWidth = false;
            _Edit.Closed += _Edit_Closed;
        }

        void _Edit_Closed(object sender, ClosedEventArgs e)
        {
            _Edit.Properties.PopupFormSize = _LastBestSize;
        }

        void _Edit_Popup(object sender, EventArgs e)
        {
            GridView view = _Edit.Properties.View;
            view.BestFitColumns();
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo vinfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            int bestWidth = vinfo.ColumnsInfo.LastColumnInfo.Bounds.Right + 20;
            Control popupWindow = (sender as DevExpress.Utils.Win.IPopupControl).PopupWindow;
            int footerHeight = popupWindow.Height - view.GridControl.Height;
            int bestHeight = vinfo.RowsInfo.GetInfoByHandle(vinfo.RowsInfo.GetLastVisibleRowIndex()).Bounds.Bottom + footerHeight;
            _LastBestSize = new Size(bestWidth, 500);
            popupWindow.Size = _LastBestSize;
        }
    }
}
