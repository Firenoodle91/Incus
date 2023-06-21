using System;
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
using HKInc.Service.Helper;

namespace HKInc.Service.Controls
{
    [ToolboxItem(true)]
    public partial class SearchLookUpEditEx : SearchLookUpEdit
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

        public bool isImeModeDisable { get; set; } = false;

        private string ActiveFilter = string.Empty;

        public object Constraint { get; set; }

        public object Value_1 { get; set; }

        private string ValueMemberName = string.Empty;
        private string DisplayMemberName = string.Empty;

        #region Constructor

        public SearchLookUpEditEx()
        {
            InitializeComponent();

            this.ButtonPressed += SearchLookUpEditEx_ButtonPressed;
            this.EnabledChanged += LookUpEditEx_EnabledChanged;
            this.Popup += SearchLookUpEditEx_Popup;
            this.KeyDown += SearchLookUpEditEx_KeyDown1;
            
            //this.KeyDown += SearchLookUpEditEx_KeyDown;
            this.NullText = null;
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

        #endregion

        #region pulbic functions

        public void Clear()
        {
            this.EditValue = null;

            //Change the field via reflection
            //FieldInfo field = typeof(GridLookUpEdit).GetField("fOldEditValue", BindingFlags.Instance | BindingFlags.NonPublic);

            //if (field != null)
            //    field.SetValue(this, this.EditValue);
        }

        /// <summary>
        /// SetDefault 재 호출시 검색이 안되는 버그로 인해 추가
        /// </summary>
        public void DataSourceClear()
        {
            this.BeginUpdate();

            this.DataSource = null;
            this.Properties.View.Columns.Clear();
            this.Properties.ValueMember = null;
            this.Properties.DisplayMember = null;

            this.EndUpdate();
        }

        public void SetDefault(bool bDeleteButtonVisible = true, string valueMember = "CodeVal", string displayMember = "CodeName", object dataSource = null, TextEditStyles textEditStyles = TextEditStyles.Standard, HorzAlignment horz = HorzAlignment.Center, EventHandler PopupEvent = null)
        {
            DataSourceClear();

            ValueMemberName = valueMember;
            DisplayMemberName = displayMember;

            SetHAlignment(horz);

            Init(valueMember, displayMember, textEditStyles);

            if (!this.Enabled) ClearButton(ButtonPredefines.Combo);

            if (bDeleteButtonVisible) AddButton(ButtonPredefines.Delete);
            this.Properties.ShowClearButton = bDeleteButtonVisible;

            if (dataSource != null) DataSource = dataSource;

            if (PopupEvent != null) this.Popup += PopupEvent;

            if (valueMember == "ItemCode" || valueMember == "ProductItemcode") //품목LookUp Column Setting
            {
                ItemCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
            else if (valueMember == "MachineMCode")
            {
                MachineMCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
            else if (valueMember == "MoldMCode")
            {
                MoldMCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
        }

        public void SetDefaultPOP(bool bDeleteButtonVisible = true, string valueMember = "CodeVal", string displayMember = "CodeName", object dataSource = null, TextEditStyles textEditStyles = TextEditStyles.Standard, HorzAlignment horz = HorzAlignment.Center, EventHandler PopupEvent = null)
        {
            ValueMemberName = valueMember;
            DisplayMemberName = displayMember;

            SetHAlignment(horz);

            Init(valueMember, displayMember, textEditStyles);

            if (!this.Enabled) ClearButton(ButtonPredefines.Combo);

            if (bDeleteButtonVisible) AddButton(ButtonPredefines.Delete);
            this.Properties.ShowClearButton = bDeleteButtonVisible;
            
            if (dataSource != null) DataSource = dataSource;

            if (PopupEvent != null) this.Popup += PopupEvent;

            if (valueMember == "ItemCode" || valueMember == "ProductItemcode") //품목LookUp Column Setting
            {
                ItemCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
            else if (valueMember == "MachineMCode")
            {
                MachineMCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
            else if (valueMember == "MoldMCode")
            {
                MoldMCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }

            this.Properties.View.RowHeight = 50;
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            SetFontSize(new Font("맑은 고딕", 15));
            Properties.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            //this.GetPopupEditForm().Visible = false;
            this.Popup += POP_SearchLookUpEditEx_Popup;

        }

        private void POP_SearchLookUpEditEx_Popup(object sender, EventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            Control f = (sender as DevExpress.Utils.Win.IPopupControl).PopupWindow;
            string[] controlNames = new string[3] { "teFind", "btClear", "btFind" };
            foreach (string name in controlNames)
            {
                Control[] controls = f.Controls.Find(name, true);
                if (controls.Length > 0)
                {
                    Control ctr = controls[0];
                    LayoutControl layoutControl = ctr.Parent as LayoutControl;
                    if (layoutControl != null)
                        layoutControl.GetItemByControl(ctr).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }
        }

        public void SetDefault(bool itemCodeFlag, bool bDeleteButtonVisible = true, string valueMember = "CodeVal", string displayMember = "CodeName", object dataSource = null, TextEditStyles textEditStyles = TextEditStyles.Standard, HorzAlignment horz = HorzAlignment.Center, EventHandler PopupEvent = null)
        {
            DataSourceClear();

            ValueMemberName = valueMember;
            DisplayMemberName = displayMember;

            SetHAlignment(horz);

            Init(valueMember, displayMember, textEditStyles);

            if (!this.Enabled) ClearButton(ButtonPredefines.Combo);

            if (bDeleteButtonVisible) AddButton(ButtonPredefines.Delete);
            this.Properties.ShowClearButton = bDeleteButtonVisible;

            if (dataSource != null) DataSource = dataSource;

            if (PopupEvent != null) this.Popup += PopupEvent;

            if (itemCodeFlag && (valueMember == "ItemCode" || valueMember == "ProductItemcode")) //품목LookUp Column Setting
            {
                ItemCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
            else if (valueMember == "MachineMCode")
            {
                MachineMCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
            else if (valueMember == "MoldMCode")
            {
                MoldMCodeColumnSetting(Properties, valueMember, valueMember, displayMember);
            }
        }

        public void UpdateDataSource(object oDatasource)
        {
            if (oDatasource != null)
                DataSource = oDatasource;
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
            Properties.BestFitMode = BestFitMode.BestFit;            
            Properties.ValueMember = valueMember;            
            Properties.DisplayMember = displayMember;
            Properties.TextEditStyle = textEditStyles;
            Properties.TextEditStyle = TextEditStyles.Standard;
            Properties.ImmediatePopup = true;
            //Properties.PopupFormSize = new Size(this.Width, 150);

            Properties.View.Appearance.Row.Font = Font;
            Properties.View.OptionsView.ShowColumnHeaders = false;
            Properties.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            ClearButton(ButtonPredefines.Delete);
        }

        private void ItemCodeColumnSetting(RepositoryItemSearchLookUpEdit edit, string fieldName, string valueMember, string displayMember)
        {
            var culture = DataConvert.GetCultureIndex();

            edit.View.OptionsView.ShowColumnHeaders = true;
            //edit.PopupFormMinSize = new Size(400, 300);
            edit.View.Columns[0].Visible = false;
            edit.View.Columns[1].Visible = false;

            if (displayMember == "ItemCode")
            {
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemCode",
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("ItemCode"),
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = DataConvert.GetCultureDataFieldName("ItemName"),
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("ItemName"),
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = DataConvert.GetCultureDataFieldName("ItemName1"),
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("ItemName1"),
                    Visible = true
                });
            }
            else if(displayMember == "MoldCode")
            {
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemCode",
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("MoldCode"),
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = DataConvert.GetCultureDataFieldName("ItemName"),
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("MoldSubCode"),
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = DataConvert.GetCultureDataFieldName("ItemName1"),
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("MoldName"),
                    Visible = true
                });
            }
            else
            {
                edit.DisplayMember = "ItemCode";

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemCode",
                    Caption = HelperFactory.GetLabelConvert().GetLabelText("ItemCode"),
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = DataConvert.GetCultureDataFieldName("ItemName"),
                    Caption = HelperFactory.GetLabelConvert().GetLabelText(DataConvert.GetCultureDataFieldName("ItemName")),
                    Visible = true
                });

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = DataConvert.GetCultureDataFieldName("ItemName1"),
                    Caption = HelperFactory.GetLabelConvert().GetLabelText(DataConvert.GetCultureDataFieldName("ItemName1")),
                    Visible = true
                });
            }


        }

        private void MachineMCodeColumnSetting(RepositoryItemSearchLookUpEdit edit, string fieldName, string valueMember, string displayMember)
        {
            var culture = DataConvert.GetCultureIndex();

            edit.View.OptionsView.ShowColumnHeaders = true;
            edit.PopupFormMinSize = new Size(1000, 300);
            edit.View.Columns[0].Visible = false;
            edit.View.Columns[1].Visible = false;

            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = valueMember,
                Caption = HelperFactory.GetLabelConvert().GetLabelText(valueMember),
                Visible = true
            });

            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "MachineCode",
                Caption = HelperFactory.GetLabelConvert().GetLabelText("MachineCode"),
                Visible = true
            });

            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = DataConvert.GetCultureDataFieldName(displayMember),
                Caption = HelperFactory.GetLabelConvert().GetLabelText(displayMember),
                Visible = true
            });
        }

        private void MoldMCodeColumnSetting(RepositoryItemSearchLookUpEdit edit, string fieldName, string valueMember, string displayMember)
        {
            var culture = DataConvert.GetCultureIndex();

            edit.View.OptionsView.ShowColumnHeaders = true;
            //edit.PopupFormMinSize = new Size(400, 300);
            edit.View.Columns[0].Visible = false;
            edit.View.Columns[1].Visible = false;

            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = valueMember,
                Caption = HelperFactory.GetLabelConvert().GetLabelText(valueMember),
                Visible = true
            });

            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "MoldCode",
                Caption = HelperFactory.GetLabelConvert().GetLabelText("MoldCode"),
                Visible = true
            });

            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = DataConvert.GetCultureDataFieldName(displayMember),
                Caption = HelperFactory.GetLabelConvert().GetLabelText(displayMember),
                Visible = true
            });
        }
        public void ShowColumnHeaders(bool Visible)
        {
            Properties.View.OptionsView.ShowColumnHeaders = Visible;
        }

        /// <summary>
        /// SetDefault 위에 위치해야 함 
        /// </summary>
        /// <param name="horz"></param>
        public void SetHAlignment(HorzAlignment horz = HorzAlignment.Center)
        {
            Properties.View.Appearance.Row.Options.UseTextOptions = true;
            Properties.View.Appearance.Row.TextOptions.HAlignment = horz;
            Properties.View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            Properties.View.Appearance.HeaderPanel.TextOptions.HAlignment = horz;
            //gridView.Appearance.HeaderPanel.TextOptions.HAlignment = horz;            
            Properties.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            Properties.View.Name = "gridView";
            Properties.View.OptionsSelection.EnableAppearanceFocusedCell = false;
            Properties.View.OptionsView.ShowGroupPanel = false;
            Properties.View.OptionsView.ShowIndicator = false;

            Properties.Appearance.TextOptions.HAlignment = horz;
            Properties.AppearanceDropDown.TextOptions.HAlignment = horz;
            
            // 20210628 오세완 차장 아래 부분이 오류를 일으켜서 위형식으로 변경
            //try
            //{
            //    GridView gridView = new GridView();
            //    gridView.Appearance.Row.Options.UseTextOptions = true;
            //    gridView.Appearance.Row.TextOptions.HAlignment = horz;
            //    gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            //    gridView.Appearance.HeaderPanel.TextOptions.HAlignment = horz;
            //    //gridView.Appearance.HeaderPanel.TextOptions.HAlignment = horz;            
            //    gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            //    gridView.Name = "gridView";
            //    gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            //    gridView.OptionsView.ShowGroupPanel = false;
            //    gridView.OptionsView.ShowIndicator = false;
            //    Properties.PopupView = gridView;

            //    Properties.Appearance.TextOptions.HAlignment = horz;
            //    Properties.AppearanceDropDown.TextOptions.HAlignment = horz;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

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
        
        private void SearchLookUpEditEx_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                this.EditValue = null;     

                //Change the field via reflection
                //FieldInfo field = typeof(GridLookUpEdit).GetField("fOldEditValue", BindingFlags.Instance | BindingFlags.NonPublic);

                //if (field != null)
                //    field.SetValue(this, this.EditValue);
            }
        }        

        private void LookUpEditEx_EnabledChanged(object sender, EventArgs e)
        {
            SearchLookUpEditEx lup = sender as SearchLookUpEditEx;

            for (int i = 0; i < lup.Properties.Buttons.Count; i++)
            {
                if (lup.Properties.Buttons[i].Kind == ButtonPredefines.Search)
                    lup.Properties.Buttons[i].Visible = true; 
                else
                    lup.Properties.Buttons[i].Visible = lup.Enabled;
            }            
        }
        
        private void SearchLookUpEditEx_Popup(object sender, EventArgs e)
        {            
            if (DataSource != null)
            {
                Type type = this.Properties.DataSource.GetType();
                PropertyInfo[] prop = type.GetProperties();
                Type objType = Type.GetType(prop[2].PropertyType.AssemblyQualifiedName);
                PropertyInfo[] fields = objType.GetProperties();
                if (fields.Any(p => p.Name.Equals("UseFlag")))
                    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[UseFlag] == 'Y'" : ActiveFilter;
                else if (fields.Any(p => p.Name.Equals("UseYn")))
                    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[UseYn] == 'Y'" : ActiveFilter;
                else if (fields.Any(p => p.Name.Equals("UseYN")))
                    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[UseYN] == 'Y'" : ActiveFilter;
                else if (fields.Any(p => p.Name.Equals("Active")))
                    Properties.View.ActiveFilter.NonColumnFilter = string.IsNullOrEmpty(ActiveFilter) ? "[Active] == 'Y'" : ActiveFilter;
                else if (!string.IsNullOrEmpty(ActiveFilter))
                    Properties.View.ActiveFilter.NonColumnFilter = ActiveFilter;

                TextEdit editor = ((sender as DevExpress.Utils.Win.IPopupControl).PopupWindow.Controls[3].Controls[0].Controls[7] as TextEdit);
                editor.KeyDown += SearchLookUpEditEx_KeyDown;
                //this.KeyDown += SearchLookUpEditEx_KeyDown;
            }

            if (isImeModeDisable)
            {
                DevExpress.Utils.Win.IPopupControl popupControl = sender as DevExpress.Utils.Win.IPopupControl;
                DevExpress.XtraEditors.Popup.PopupSearchLookUpEditForm window = popupControl.PopupWindow as DevExpress.XtraEditors.Popup.PopupSearchLookUpEditForm;
                DevExpress.XtraGrid.Editors.SearchEditLookUpPopup popup = window.Controls.OfType<DevExpress.XtraGrid.Editors.SearchEditLookUpPopup>().FirstOrDefault();
                TextEdit find = popup.FindTextBox;
                if (find != null)
                {
                    find.ImeMode = ImeMode.Disable;
                }
            }
        }

        private void SearchLookUpEditEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Properties.View.RowCount == 1)
                {
                    Properties.View.FocusedRowHandle = Properties.View.GetVisibleRowHandle(0);
                    EditValue = Properties.View.GetFocusedRowCellValue(ValueMemberName);
                    this.ClosePopup();
                }
            }
        }

        private void SearchLookUpEditEx_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if(this.Text != "")
                    Clipboard.SetText(this.Text.GetNullToEmpty());
                else
                    Clipboard.Clear();
            }
        }

        #endregion
    }
}
