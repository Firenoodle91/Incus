using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using HKInc.Service.Factory;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

using HKInc.Utils.Common;
using HKInc.Utils.Class;

namespace HKInc.Service.Controls
{
    partial class GridControlEx
    {
        public void SetRepositoryItem(string fieldName, RepositoryItem item, bool CustomOnlyNumeric = false)
        {
            MainGrid.RepositoryItems.Add(item);
            Columns[fieldName].ColumnEdit = item;
            Columns[fieldName].ShowButtonMode = ShowButtonModeEnum.ShowForFocusedRow;
            if (item.GetType() == typeof(RepositoryItemSpinEdit))
            {
                RepositoryItemSpinEdit con = item as RepositoryItemSpinEdit;
                con.AllowMouseWheel = true;
                Columns[fieldName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                //con.CustomDisplayText += Con_CustomDisplayText;
            }
            else if (item.GetType() == typeof(RepositoryItemPictureEdit))
            {
                MainView.RowHeight = 50;
                Columns[fieldName].OptionsFilter.AllowAutoFilter = false;
            }
            else if (item.GetType() == typeof(RepositoryItemCheckEdit))
                Columns[fieldName].OptionsColumn.AllowSort = DefaultBoolean.False;
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

        private void Con_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            try
            {
                if (e.Value != null)
                {
                    var v = int.Parse(e.Value.ToString());
                    e.DisplayText = v.ToString();

                }
            }
            catch (Exception ex)
            {

            }
        }

        public RepositoryItemSpinEdit SetRepositoryItemSpinEdit(string fieldName, DefaultBoolean allownullinput = DefaultBoolean.Default, string editmask = "n0", bool usemaskasdisplayformat = true, bool spinButton = true)
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
        public void SetRepositoryItemPictureEdit(string fieldName, string NullText = "이미지 없음", PictureSizeMode SizeMode = PictureSizeMode.Squeeze)
        {
            RepositoryItemPictureEdit obj = new RepositoryItemPictureEdit();
            obj.SizeMode = SizeMode;
            obj.NullText = NullText;

            SetRepositoryItem(fieldName, obj);
        }

        public RepositoryItemTextEdit SetRepositoryItemTextEdit(string fieldName, int maxlength, DefaultBoolean allownullinput, DevExpress.XtraEditors.Mask.MaskType masktype, string editmask, bool usemaskasdisplayformat, bool CustomOnlyNumeric = false)
        {
            RepositoryItemTextEdit text = new RepositoryItemTextEdit();

            text.MaxLength = maxlength;
            text.AllowNullInput = allownullinput;
            text.Mask.MaskType = masktype;

            if (!string.IsNullOrEmpty(editmask)) text.Mask.EditMask = editmask;

            text.Mask.UseMaskAsDisplayFormat = usemaskasdisplayformat;

            //text.EditValueChanged += TextEditValueChanged;

            SetRepositoryItem(fieldName, text, CustomOnlyNumeric);
            return text;
        }

        public RepositoryItemMemoEdit SetRepositoryItemMemoEdit(string fieldName)
        {
            RepositoryItemMemoEdit Memo = new RepositoryItemMemoEdit();

            //text.MaxLength = maxlength;
            //text.AllowNullInput = allownullinput;
            //text.Mask.MaskType = masktype;

            //if (!string.IsNullOrEmpty(editmask)) text.Mask.EditMask = editmask;

            //text.Mask.UseMaskAsDisplayFormat = usemaskasdisplayformat;

            //text.EditValueChanged += TextEditValueChanged;

            SetRepositoryItem(fieldName, Memo);
            return Memo;
        }

        private void TextEditValueChanged(object sender, EventArgs e)
        {
            MainView.PostEditor();
        }

        private RepositoryItemDateEdit GetRepositoryItemDate(HKInc.Utils.Enum.DateFormat formatFlag)
        {
            return HKInc.Service.Factory.RepositoryFactory.GetRepositoryItemDate(formatFlag);
        }

        public void SetRepositoryItemDateEdit(string fieldName, int? bestFitWidth = 110)
        {
            RepositoryItemDateEdit date = GetRepositoryItemDate(HKInc.Utils.Enum.DateFormat.Day);

            if (bestFitWidth != null)
                date.BestFitWidth = (int)bestFitWidth;

            SetRepositoryItem(fieldName, date);
        }

        public void SetRepositoryItemDateEdit(string fieldName, HKInc.Utils.Enum.DateFormat dateFormat, int? bestFitWidth = 110)
        {
            RepositoryItemDateEdit date = GetRepositoryItemDate(dateFormat);

            if (bestFitWidth != null)
                date.BestFitWidth = (int)bestFitWidth;

            SetRepositoryItem(fieldName, date);
        }

        public void SetRepositoryItemDateTimeEdit(string fieldName, int? bestFitWidth = 140)
        {
            RepositoryItemDateEdit date = GetRepositoryItemDate(HKInc.Utils.Enum.DateFormat.DateAndTime);

            //date.CalendarTimeProperties.Appearance.Font = new Font(date.CalendarTimeProperties.Appearance.Font.FontFamily, 8f);
            if (bestFitWidth != null)
                date.BestFitWidth = (int)bestFitWidth;

            SetRepositoryItem(fieldName, date);
        }

        public void SetRepositoryItemFullDateTimeEdit(string fieldName, int? bestFitWidth = 140)
        {
            RepositoryItemDateEdit date = GetRepositoryItemDate(HKInc.Utils.Enum.DateFormat.DateAndTimeSecond);

            if (bestFitWidth != null)
                date.BestFitWidth = (int)bestFitWidth;

            SetRepositoryItem(fieldName, date);
        }

        public void SetRepositoryItemLookUpEdit(string fieldName, object dataSource, string valueMember = "CodeId", string displayMember = "CodeName", bool addDeleteButton = false, bool addEllipsisButton = false, ButtonPressedEventHandler ellipsisButtonPressedHandler = null, string nullText = "", EventHandler gridLookUpBeforePopupHandler = null, TextEditStyles textEditStyles = TextEditStyles.DisableTextEditor)
        {
            SetRepositoryItemGridLookUpEdit(fieldName, dataSource, valueMember, displayMember, addDeleteButton, addEllipsisButton, ellipsisButtonPressedHandler, nullText, gridLookUpBeforePopupHandler, textEditStyles);
        }

        public void SetRepositoryItemGridLookUpEdit(string fieldName, object dataSource, string valueMember = "CodeId", string displayMember = "CodeName", bool addDeleteButton = false, bool addEllipsisButton = false, ButtonPressedEventHandler ellipsisButtonPressedHandler = null, string nullText = "", EventHandler gridLookUpBeforePopupHandler = null, TextEditStyles textEditStyles = TextEditStyles.DisableTextEditor)
        {
            RepositoryItemGridLookUpEdit lookup = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = valueMember,
                DisplayMember = displayMember
            };

            #region Property

            lookup.View.OptionsView.ShowColumnHeaders = false;
            lookup.NullText = nullText;
            lookup.BestFitMode = BestFitMode.BestFit;
            lookup.AutoComplete = true;
            lookup.View.OptionsBehavior.AllowIncrementalSearch = true;
            lookup.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            lookup.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            lookup.ValueMember = valueMember;
            lookup.DisplayMember = displayMember;
            lookup.TextEditStyle = textEditStyles;
            lookup.AutoComplete = true;

            #endregion

            #region Event

            lookup.EditValueChanged += LookupEditValueChanged;
            lookup.Popup += GridLookUp_BeforePopup;
            if (gridLookUpBeforePopupHandler != null) lookup.Popup += gridLookUpBeforePopupHandler;
            lookup.ButtonPressed += Lookup_ButtonPressed;
            #endregion

            if (addDeleteButton)
                AddButton(lookup, ButtonPredefines.Delete);

            if (addEllipsisButton)
                AddButton(lookup, ButtonPredefines.Ellipsis);

            if (ellipsisButtonPressedHandler != null)
                lookup.ButtonPressed += ellipsisButtonPressedHandler;

            if (fieldName == "ItemCode" || fieldName == "ProductItemcode") //품목LookUp Column Setting
            {
                ItemCodeColumnSetting(lookup, fieldName, valueMember, displayMember);
            }
            else
            {
                lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = valueMember,
                    Caption = HelperFactory.GetLabelConvert().GetLabelText(fieldName),
                    Visible = true //코드표시
                });

                lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = displayMember,
                    Caption = HelperFactory.GetLabelConvert().GetLabelText(fieldName),
                    Visible = true
                });
            }

            Columns[fieldName].ColumnEdit = lookup;
            lookup.DataSource = dataSource;
        }

        public void SetRepositoryItemSearchLookUpEdit(string fieldName, object dataSource, string valueMember = "CodeId", string displayMember = "CodeName", bool addDeleteButton = false, bool addEllipsisButton = false, ButtonPressedEventHandler ellipsisButtonPressedHandler = null, string nullText = "", EventHandler gridLookUpBeforePopupHandler = null, TextEditStyles textEditStyles = TextEditStyles.Standard)
        {
            RepositoryItemSearchLookUpEdit lookup = new RepositoryItemSearchLookUpEdit()
            {
                ValueMember = valueMember,
                DisplayMember = displayMember
            };

            #region Property

            lookup.View.OptionsView.ShowColumnHeaders = false;
            lookup.NullText = nullText;
            lookup.BestFitMode = BestFitMode.BestFitResizePopup;
            lookup.View.OptionsBehavior.AllowIncrementalSearch = true;
            lookup.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            lookup.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
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
            else
                lookup.ShowClearButton = false;

            if (addEllipsisButton)
                AddButton(lookup, ButtonPredefines.Ellipsis);

            if (ellipsisButtonPressedHandler != null)
                lookup.ButtonPressed += ellipsisButtonPressedHandler;

            if (fieldName == "ItemCode" || fieldName == "ProductItemcode") //품목LookUp Column Setting
            {
                ItemCodeColumnSetting(lookup, fieldName, valueMember, displayMember);
            }
            else
            {
                lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = valueMember,
                    Caption = HelperFactory.GetLabelConvert().GetLabelText(fieldName),
                    Visible = true //코드표시
                });

                lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = displayMember,
                    Caption = HelperFactory.GetLabelConvert().GetLabelText(fieldName),
                    Visible = true
                });
            }

            Columns[fieldName].ColumnEdit = lookup;
            lookup.DataSource = dataSource;
        }

        public void SetRepositoryItemGridLookUpEdit_Code(string fieldName, object dataSource, string valueMember = "CodeId", string displayMember = "CodeName", bool addDeleteButton = false, bool addEllipsisButton = false, ButtonPressedEventHandler ellipsisButtonPressedHandler = null, string nullText = "", EventHandler gridLookUpBeforePopupHandler = null, TextEditStyles textEditStyles = TextEditStyles.DisableTextEditor)
        {
            RepositoryItemGridLookUpEdit lookup = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = valueMember,
                DisplayMember = displayMember
            };

            lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = valueMember,
                Caption = HelperFactory.GetLabelConvert().GetLabelText(fieldName),
                Visible = true
            });

            lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = displayMember,
                Caption = HelperFactory.GetLabelConvert().GetLabelText(displayMember),
                Visible = true
            });

            lookup.EditValueChanged += LookupEditValueChanged;
            lookup.Popup += GridLookUp_BeforePopup;
            if (gridLookUpBeforePopupHandler != null) lookup.Popup += gridLookUpBeforePopupHandler;
            lookup.ButtonPressed += Lookup_ButtonPressed;

            lookup.NullText = nullText;
            lookup.BestFitMode = BestFitMode.BestFitResizePopup;
            lookup.AutoComplete = true;
            lookup.View.OptionsBehavior.AllowIncrementalSearch = true;
            lookup.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            //lookup.PopupFormSize = new Size(this.Width, 150);
            lookup.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            lookup.View.OptionsView.ShowColumnHeaders = true;
            lookup.ValueMember = valueMember;
            lookup.DisplayMember = valueMember;
            lookup.TextEditStyle = textEditStyles;
            lookup.AutoComplete = true;

            if (addDeleteButton)
                AddButton(lookup, ButtonPredefines.Delete);

            if (addEllipsisButton)
                AddButton(lookup, ButtonPredefines.Ellipsis);

            if (ellipsisButtonPressedHandler != null)
                lookup.ButtonPressed += ellipsisButtonPressedHandler;

            Columns[fieldName].ColumnEdit = lookup;
            //Columns[fieldName_n].ColumnEdit = lookup;
            lookup.DataSource = dataSource;
        }

        public void SetRepositoryItemGridLookUpEditAddColumnDisplay(RepositoryItem item, string fieldName, int visibleIndex, bool isVisible)
        {
            AddColumn(item, fieldName, HelperFactory.GetLabelConvert().GetLabelText(fieldName), visibleIndex, isVisible);
        }

        public void SetRepositoryItemGridLookUpEditAddColumnDisplay(RepositoryItem item, string fieldName, string displayName, int visibleIndex, bool isVisible)
        {
            AddColumn(item, fieldName, HelperFactory.GetLabelConvert().GetLabelText(displayName), visibleIndex, isVisible);
        }

        public void SetRepositoryItemGridLookUpEditColumnCpationChange(RepositoryItem item, string fieldName, string displayName)
        {
            RepositoryItemGridLookUpEdit lookup = item as RepositoryItemGridLookUpEdit;
            if (lookup != null)
            {
                lookup.View.Columns[fieldName].Caption = displayName;
            }
        }

        public void SetRepositoryItemGridLookUpEditColumnVisible(RepositoryItem item, string fieldName, bool isVisible)
        {
            RepositoryItemGridLookUpEdit lookup = item as RepositoryItemGridLookUpEdit;
            if (lookup != null)
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in lookup.View.Columns)
                {
                    if (col.FieldName.Equals(fieldName))
                        col.Visible = isVisible;
                }
            }
        }

        public void SetRepositoryItemGridLookUpEditColumnVisibleIndexChange(RepositoryItem item, string fieldName, int visibleIndex)
        {
            RepositoryItemGridLookUpEdit lookup = item as RepositoryItemGridLookUpEdit;
            if (lookup != null)
            {
                lookup.View.Columns[fieldName].VisibleIndex = visibleIndex;
            }
        }

        public RepositoryItemComboBox SetRepositoryItemComboBox(string fieldName, List<object> dataSource, string nullText = "", TextEditStyles textEditStyles = TextEditStyles.Standard)
        {
            RepositoryItemComboBox lookup = new RepositoryItemComboBox();
            lookup.Items.AddRange(dataSource);
            lookup.ButtonPressed += Lookup_ButtonPressed;
            lookup.NullText = nullText;
            lookup.AutoComplete = true;
            lookup.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            lookup.TextEditStyle = textEditStyles;
            lookup.AutoComplete = true;
            Columns[fieldName].ColumnEdit = lookup;
            return lookup;
        }

        private void AddColumn(RepositoryItem item, string fieldName, string caption, int visibleIndex, bool isVisible)
        {
            RepositoryItemGridLookUpEdit lookup = item as RepositoryItemGridLookUpEdit;
            if (lookup != null)
            {
                lookup.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn()
                {
                    FieldName = fieldName,
                    Caption = caption,
                    Visible = isVisible,
                });
            }
        }

        private void AddButton(RepositoryItemGridLookUpEdit lookup, ButtonPredefines buttonKind)
        {
            if (this.Enabled)
            {
                foreach (var item in lookup.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == buttonKind))
                    lookup.Buttons.RemoveAt(item.Index);

                lookup.Buttons.Add(new EditorButton() { Kind = buttonKind });
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

        private void Lookup_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit == null) return;

            if (e.Button.Kind == ButtonPredefines.Delete)
                edit.EditValue = null;
        }

        private void SearchLookup_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            if (edit == null) return;

            if (e.Button.Kind == ButtonPredefines.Delete)
                edit.EditValue = null;
        }

        private void GridLookUp_BeforePopup(object sender, EventArgs e)
        {
            GridLookUpEdit lookup = sender as GridLookUpEdit;
            if (lookup == null) return;

            if (lookup.Properties.DataSource != null)
            {
                //Type type = lookup.Properties.DataSource.GetType();

                //FieldInfo[] fields = type.GetFields();

                //if (fields.Any(p => p.Name.Equals("UseFlag")))
                //    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseFlag] == 'Y'";
                //else if (fields.Any(p => p.Name.Equals("UseYn")))
                //    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYn] == 'Y'";
                //else if (fields.Any(p => p.Name.Equals("Active")))
                //    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[Active] == 'Y'";

                Type type = lookup.Properties.DataSource.GetType();
                PropertyInfo[] prop = type.GetProperties();
                Type objType = Type.GetType(prop[2].PropertyType.AssemblyQualifiedName);
                PropertyInfo[] fields = objType.GetProperties();
                if (fields.Any(p => p.Name.Equals("UseFlag")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseFlag] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("UseYn")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYn] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("UseYN")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYN] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("Active")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[Active] == 'Y'";
            }
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
                if (fields.Any(p => p.Name.Equals("UseFlag")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseFlag] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("UseYn")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYn] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("UseYN")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseYN] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("Active")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[Active] == 'Y'";

                // 2021-10-28 김진우 주임 주석
                //TextEdit editor = ((sender as DevExpress.Utils.Win.IPopupControl).PopupWindow.Controls[2].Controls[0].Controls[7] as TextEdit);
                //editor.Tag = lookup;
                //editor.KeyDown += SearchLookUpEditEx_KeyDown;

                // 2021-11-04 김진우 주임 추가
                DevExpress.Utils.Win.IPopupControl ipc = sender as DevExpress.Utils.Win.IPopupControl;
                if (ipc != null)
                {
                    if (ipc.PopupWindow.Controls != null)
                        if (ipc.PopupWindow.Controls.Count > 2)
                            if (ipc.PopupWindow.Controls[2].Controls != null)
                                if (ipc.PopupWindow.Controls[2].Controls.Count >= 1)
                                    if (ipc.PopupWindow.Controls[2].Controls[0].Controls != null)
                                        if (ipc.PopupWindow.Controls[2].Controls[0].Controls.Count > 7)
                                        {
                                            TextEdit editor = ((sender as DevExpress.Utils.Win.IPopupControl).PopupWindow.Controls[2].Controls[0].Controls[7] as TextEdit);
                                            editor.Tag = lookup;
                                            editor.KeyDown += SearchLookUpEditEx_KeyDown;
                                        }
                }

            }
        }
        private void LookupEditValueChanged(object sender, EventArgs e)
        {
            MainView.PostEditor();
        }

        public void SetRepositoryItemCheckEdit(string fieldName, string nullValue = null)
        {
            RepositoryItemCheckEdit check = new RepositoryItemCheckEdit() { ValueChecked = "Y", ValueUnchecked = "N" };

            if (nullValue != null)
            {
                check.NullStyle = StyleIndeterminate.Unchecked;
                check.NullText = nullValue;
            }
            check.EditValueChanged += CheckEditValueChanged;
            SetRepositoryItem(fieldName, check);
        }

        public void SetRepositoryItemRadioGroup(string fieldName, HKInc.Utils.Enum.RadioGroupType radioGroupType)
        {
            RepositoryItemRadioGroup radioGroup = new RepositoryItemRadioGroup();
            HKInc.Service.Handler.RadioGroupHandler.SetRepositoryRadioGroup(radioGroup, radioGroupType);

            radioGroup.EditValueChanged += CheckEditValueChanged;
            SetRepositoryItem(fieldName, radioGroup);
        }

        private void CheckEditValueChanged(object sender, EventArgs e)
        {
            MainView.PostEditor();
        }

        private void ItemCodeColumnSetting(RepositoryItemGridLookUpEdit edit, string fieldName, string valueMember, string displayMember)
        {
            edit.View.OptionsView.ShowColumnHeaders = true;

            edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = valueMember,
                Caption = "품번",
                Visible = true //코드표시
            });

            if (displayMember == "ItemCode")
            {
                //edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                //{
                //    FieldName = "ItemNm1",
                //    Caption = "품번",
                //    Visible = true
                //});

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm",
                    Caption = "품명",
                    Visible = true
                });
            }
            else
            {
                //edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                //{
                //    FieldName = displayMember,
                //    Caption = "품번",
                //    Visible = true
                //});

                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm",
                    Caption = "품명",
                    Visible = true
                });
            }
        }

        private void ItemCodeColumnSetting(RepositoryItemSearchLookUpEdit edit, string fieldName, string valueMember, string displayMember)
        {
            edit.View.OptionsView.ShowColumnHeaders = true;

            if (displayMember == "ItemCode")
            {
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn        // 2022-01-25 김진우 대리 추가
                {
                    FieldName = "ItemCode",
                    Caption = "품목코드",
                    Visible = true
                });
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm",
                    Caption = "품명",
                    Visible = true
                });
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm1",
                    Caption = "품번",
                    Visible = true
                });
            }
            else
            {
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = "ItemNm",
                    Caption = "품명",
                    Visible = true
                });
                edit.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
                {
                    FieldName = displayMember,
                    Caption = "품번",
                    Visible = true
                });
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

        private void SearchLookUpEditEx_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                SearchLookUpEdit lookup = sender as SearchLookUpEdit;
                if (lookup == null) return;

                if (lookup.Text != "")
                    Clipboard.SetText(lookup.Text.GetNullToEmpty());
                else
                    Clipboard.Clear();
            }
        }

    }
}