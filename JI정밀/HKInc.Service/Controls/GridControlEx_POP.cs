using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;

using HKInc.Service.Factory;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using DevExpress.Utils.Menu;
using HKInc.Service.Handler;

namespace HKInc.Service.Controls
{
    [ToolboxItem(true)]
    public partial class GridControlEx_POP : DevExpress.XtraEditors.XtraUserControl
    {
        private ILabelConvert FieldNameConverter = HelperFactory.GetLabelConvert();
        private GridViewType _GridViewType = GridViewType.GridView;
        private bool _ShowColumnHeaders = true;
        private bool IsEditableSetByUser = false;
        private WaitHandler WaitHandler = new WaitHandler();

        public GridControlEx_POP()
        {
            InitializeComponent();
            
            InitGridViewOptions();
            InitEmptyRepositoryItem();
            InitGridViewEvent();
        }

        void InitGridViewOptions()
        {            
            gridControl.ShowOnlyPredefinedDetails = true;
            view.DataController.AllowIEnumerableDetails = true;

            gridControl.UseEmbeddedNavigator = true;
            gridControl.EmbeddedNavigator.BackColor = Color.White;

            view.OptionsView.EnableAppearanceEvenRow = true;
            view.OptionsView.EnableAppearanceOddRow = true;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowDetailButtons = false;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsView.ShowFooter = false;
            view.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            view.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowForFocusedRow;

            view.OptionsSelection.EnableAppearanceFocusedRow = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = true;
            view.OptionsSelection.MultiSelect = true;
            view.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;

            view.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
            view.OptionsBehavior.ReadOnly = true;
            view.OptionsBehavior.Editable = false;

            view.FocusRectStyle = DrawFocusRectStyle.RowFocus;            
            view.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            view.OptionsNavigation.AutoFocusNewRow = true;
            view.OptionsMenu.ShowConditionalFormattingItem = true;          
        }

        public void UseEmbeddedNavigator(bool enable)
        {
            gridControl.UseEmbeddedNavigator = enable;
        }

        void InitEmptyRepositoryItem(bool visible = false)
        {
            if (gridControl.RepositoryItems["EmptyRepositoryItem"] == null)
            {
                RepositoryItemButtonEdit emptyRepositoryItem = new RepositoryItemButtonEdit();
                emptyRepositoryItem.Name = "EmptyRepositoryItem";
                emptyRepositoryItem.Buttons.Clear();
                emptyRepositoryItem.TextEditStyle = (visible) ? TextEditStyles.DisableTextEditor : TextEditStyles.HideTextEditor;
                gridControl.RepositoryItems.Add(emptyRepositoryItem);
            }
            else
            {
                ((RepositoryItemButtonEdit)gridControl.RepositoryItems["EmptyRepositoryItem"]).TextEditStyle =
                    (visible) ? TextEditStyles.DisableTextEditor : TextEditStyles.HideTextEditor;
            }
        }

        void InitGridViewEvent()
        {            
            view.CellValueChanged += View_CellValueChanged;
            view.PopupMenuShowing += View_PopupMenuShowing;
            view.CustomDrawRowIndicator += View_CustomDrawRowIndicator;
            view.RowCountChanged += View_RowCountChanged;
            gridControl.PaintEx += GridControl_PaintEx;
            //view.KeyDown += View_KeyDown; //phs 체크박스 복사기능 추가

            view.MouseWheel += View_MouseWheel;
        }

        private void View_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                view.CloseEditor();
            }
            finally { }
        }

        //row 선택 시 Border 이벤트
        private void GridControl_PaintEx(object sender, PaintExEventArgs e)
        {            
            var views = view.GridControl.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView;
            var viewInfo = views.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            var rowInfo = viewInfo.GetGridRowInfo(views.FocusedRowHandle);
            if (rowInfo == null)
                return;
            Rectangle r = Rectangle.Empty;
            r = rowInfo.Bounds;
            if (r != Rectangle.Empty)
            {
                r.Height -= 2;
                if (view.RowHeight == 50)
                {
                    if (view.OptionsView.ShowIndicator)
                        r.Width = viewInfo.ViewRects.ColumnTotalWidth + views.IndicatorWidth - 5;
                    else
                        r.Width = viewInfo.ViewRects.ColumnTotalWidth;
                }
                else
                {
                    if (view.OptionsView.ShowIndicator)
                        r.Width = viewInfo.ViewRects.ColumnTotalWidth + views.IndicatorWidth;
                    else
                        r.Width = viewInfo.ViewRects.ColumnTotalWidth;
                }
                e.Cache.Graphics.DrawRectangle(Pens.Green, r);
            }
        }

        //private void View_KeyDown(object sender, KeyEventArgs e) //phs 체크박스 복사기능 추가
        //{
        //    if (e.Control && e.KeyCode == Keys.C)
        //    {
        //        Clipboard.SetText(view.GetFocusedDisplayText()); e.Handled = true;
        //    }
        //}

        //Indicator width calc 이벤트
        private void View_RowCountChanged(object sender, EventArgs e)
        {
            GridView gridView = ((GridView)sender);
            if (!gridView.GridControl.IsHandleCreated) return;
            if (gridView.RowCount == 0) return;
            Graphics gr = Graphics.FromHwnd(gridView.GridControl.Handle);
            SizeF size = gr.MeasureString(gridView.RowCount.ToString(), gridView.PaintAppearance.Row.GetFont());
            gridView.IndicatorWidth = Convert.ToInt32(size.Width + 0.999f) + DevExpress.XtraGrid.Views.Grid.Drawing.GridPainter.Indicator.ImageSize.Width + 15;            
        }

        //Indicator No 부여 이벤트
        private void View_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            }
        }


        Image S_IndicatorImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/formatastable_16x16.png"); //9000 Indicator
        Image S_GridLayoutSaveImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/grid/fixedwidth_16x16.png"); //9001 GridLayoutSave
        Image S_GridLayoutInitImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/remove_16x16.png"); //9002 GridLayoutInit

        Image L_IndicatorImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/formatastable_16x16.png"); //9000 Indicator
        Image L_GridLayoutSaveImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/grid/fixedwidth_16x16.png"); //9001 GridLayoutSave
        Image L_GridLayoutInitImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/remove_16x16.png"); //9002 GridLayoutInit

        Image L_ExportImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/export_16x16.png");
        Image S_ExportImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/export_16x16.png");
        Image L_ExportXlsxImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxlsx_16x16.png");
        Image S_ExportXlsxImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxlsx_16x16.png");

        //grid context menu 추가 이벤트
        private void View_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                
                DevExpress.XtraGrid.Menu.GridViewColumnMenu menu = e.Menu as DevExpress.XtraGrid.Menu.GridViewColumnMenu;
                if (menu.Column != null)
                {
                    menu.Items["Conditional Formatting"].Visible = false;

                    //줄 번호 버튼 커스텀
                    if (view.OptionsView.ShowIndicator)
                        menu.Items.Add(CreateGridIndicatorPmenuItem(FieldNameConverter.GetLabelText("Grid_Indicator_Pmenu_Hide"), menu.Column, S_IndicatorImage, L_IndicatorImage));
                    else
                        menu.Items.Add(CreateGridIndicatorPmenuItem(FieldNameConverter.GetLabelText("Grid_Indicator_Pmenu_Show"), menu.Column, S_IndicatorImage, L_IndicatorImage));

                    //엑셀 내보내기
                    menu.Items.Add(CreateExportPmenuItem(FieldNameConverter.GetLabelText("Export"), menu.Column, S_ExportImage, L_ExportImage));

                    if (!string.IsNullOrEmpty(this.gridControl.Parent.Parent.Name))
                    {
                        //Layout 저장 및 초기화 커스텀                        
                        menu.Items.Add(CreateGridLayoutSavePmenuItem("UI저장", menu.Column, S_GridLayoutSaveImage, L_GridLayoutSaveImage));
                        menu.Items.Add(CreateGridLayoutInitPmenuItem("UI초기화", menu.Column, S_GridLayoutInitImage, L_GridLayoutInitImage));
                        //menu.Items.Add(CreateGridLayoutSavePmenuItem(FieldNameConverter.GetLabelText("Grid_Layout_Pmenu_Save"), menu.Column, S_GridLayoutSaveImage, L_GridLayoutSaveImage));
                        //menu.Items.Add(CreateGridLayoutInitPmenuItem(FieldNameConverter.GetLabelText("Grid_Layout_Pmenu_Init"), menu.Column, S_GridLayoutInitImage, L_GridLayoutInitImage));
                    }
                }
            }
        }

        #region Header Context Menu Button Custom

        DXMenuCheckItem CreateGridIndicatorPmenuItem(string caption, GridColumn column, Image s_image = null, Image l_image = null)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption)
            {
                Tag = column,
                BeginGroup = true
            };
            item.Checked = !column.OptionsColumn.AllowMove;
            item.CheckedChanged += new EventHandler(Grid_Indicator_Pmenu_Click);
            if(s_image != null)
            {
                item.ImageOptions.Image = s_image;
                item.ImageOptions.LargeImage = l_image;
            }
            return item;
        }

        DXMenuCheckItem CreateGridLayoutSavePmenuItem(string caption, GridColumn column, Image s_image = null, Image l_image = null)
        {
            //DXMenuCheckItem item = new DXMenuCheckItem(caption,
            //  !column.OptionsColumn.AllowMove, image, new EventHandler(Grid_LayoutSave_Pmenu_Click));
            //item.Tag = column;

            DXMenuCheckItem item = new DXMenuCheckItem(caption)
            {
                Tag = column,
                BeginGroup = true
            };
            item.Checked = !column.OptionsColumn.AllowMove;
            item.CheckedChanged += new EventHandler(Grid_LayoutSave_Pmenu_Click);
            if (s_image != null)
            {
                item.ImageOptions.Image = s_image;
                item.ImageOptions.LargeImage = l_image;
            }

            return item;
            
        }

        DXMenuCheckItem CreateGridLayoutInitPmenuItem(string caption, GridColumn column, Image s_image = null, Image l_image = null)
        {
            //DXMenuCheckItem item = new DXMenuCheckItem(caption,
            //  !column.OptionsColumn.AllowMove, image, new EventHandler(Grid_LayoutInit_Pmenu_Click));
            //item.Tag = column;

            DXMenuCheckItem item = new DXMenuCheckItem(caption)
            {
                Tag = column
            };
            item.Checked = !column.OptionsColumn.AllowMove;
            item.CheckedChanged += new EventHandler(Grid_LayoutInit_Pmenu_Click);
            if (s_image != null)
            {
                item.ImageOptions.Image = s_image;
                item.ImageOptions.LargeImage = l_image;
            }
            return item;
        }

        DXMenuItem CreateExportPmenuItem(string caption, GridColumn column, Image s_image = null, Image l_image = null)
        {
            DXSubMenuItem Hitem = new DXSubMenuItem(caption);
            if (s_image != null)
            {
                Hitem.ImageOptions.Image = s_image;
                Hitem.ImageOptions.LargeImage = l_image;
            }

            Hitem.Items.Add(CreateExportSubPmenuItem(FieldNameConverter.GetLabelText("Excel Export"), S_ExportXlsxImage, L_ExportXlsxImage));
            Hitem.BeginGroup = true;
            return Hitem;
        }
        DXMenuItem CreateExportSubPmenuItem(string caption, Image s_image = null, Image l_image = null)
        {
            DXMenuItem menuItemExcel = new DXMenuItem(caption, new EventHandler(Grid_ExcelExport_Pmenu_Click));
            //menuItemDeleteRow.Tag = new RowInfo(view, rowHandle);
            //menuItemDeleteRow.Enabled = view.IsDataRow(rowHandle) || view.IsGroupRow(rowHandle);

            if (s_image != null)
            {
                menuItemExcel.ImageOptions.Image = s_image;
                menuItemExcel.ImageOptions.LargeImage = l_image;
            }
            return menuItemExcel;
        }
        #endregion

        #region Header Context Menu Button Event Custom

        private void Grid_Indicator_Pmenu_Click(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            GridColumn info = item.Tag as GridColumn;
            if (info == null) return;
            //info.OptionsColumn.AllowMove = !item.Checked;
            if (view.OptionsView.ShowIndicator) view.OptionsView.ShowIndicator = false;
            else view.OptionsView.ShowIndicator = true;
        }

        private void Grid_LayoutSave_Pmenu_Click(object sender, EventArgs e)
        {
            //서비스 선언
            Utils.Interface.Service.IService<Ui.Model.Domain.GridLayout> ModelService =
                (Utils.Interface.Service.IService<Ui.Model.Domain.GridLayout>)ServiceFactory.GetDomainService("GridLayout");

            DXMenuCheckItem item = sender as DXMenuCheckItem;
            GridColumn info = item.Tag as GridColumn;
            if (info == null) return;
            //info.OptionsColumn.AllowMove = !item.Checked;

            string LoginId = Utils.Common.GlobalVariable.LoginId;
            string FormName = string.Empty;
            string NameSpace = string.Empty;
            if (this.ParentForm.Name.Split('.').Count() > 1)
                FormName = this.ParentForm.Name.Split('.')[this.ParentForm.Name.Split('.').Count() - 2].GetNullToEmpty();
            else
                FormName = this.ParentForm.Name;
            NameSpace = this.ParentForm.CompanyName;            

            System.IO.Stream str = new System.IO.MemoryStream();
            this.gridControl.MainView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            System.IO.StreamReader reader = new System.IO.StreamReader(str);
            string LayoutData = reader.ReadToEnd();
            var obj = ModelService.GetList(p => p.LoginId == LoginId && p.NameSpace == NameSpace && p.FormName == FormName && p.GridName == this.gridControl.Parent.Parent.Name).FirstOrDefault();
            if (obj != null)
            {
                DialogResult result = Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_42), FieldNameConverter.GetLabelText("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    obj.LayoutData = LayoutData;
                    ModelService.Update(obj);
                    ModelService.Save();
                    Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_43), FieldNameConverter.GetLabelText("Confirm"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Ui.Model.Domain.GridLayout obj2 = new Ui.Model.Domain.GridLayout()
                {
                    LoginId = LoginId,
                    NameSpace = NameSpace,
                    FormName = FormName,
                    GridName = this.gridControl.Parent.Parent.Name,
                    LayoutData = LayoutData,
                    CreateTime = DateTime.Now,
                    CreateId = LoginId,
                    UpdatetTime = DateTime.Now,
                    UpdateId = LoginId
                };
                ModelService.Insert(obj2);
                ModelService.Save();
                Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_43), FieldNameConverter.GetLabelText("Confirm"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ModelService.Dispose();
        }

        private void Grid_LayoutInit_Pmenu_Click(object sender, EventArgs e)
        {
            GridLayoutInit();
        }

        private void Grid_ExcelExport_Pmenu_Click(object sender, EventArgs e)
        {
            HKInc.Service.Helper.ExcelExport.ExportToExcel(view);
        }
        #endregion


        private void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            if (e.Column.FieldName == "_Check")
            {
                if (!((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).GetIsFormControlChanged())
                    ((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).SetIsFormControlChanged(false);
            }
            else
            {
                BindingSource CurrentBindingSoruce = gv.DataSource as BindingSource;
                if (CurrentBindingSoruce == null) return;

                object currentObj = CurrentBindingSoruce.Current;
                if (currentObj == null) return;

                Type type = currentObj.GetType();

                #region Key 값 수정 시 Check
                PropertyInfo EditProperty = type.GetProperty(e.Column.FieldName);
                if (EditProperty != null)
                {
                    var KeyCheck = Attribute.GetCustomAttribute(EditProperty, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) as System.ComponentModel.DataAnnotations.KeyAttribute;
                    if (KeyCheck != null && gv.ActiveEditor != null && type.GetProperty("NewRowFlag").GetValue(currentObj).ToString() == "N")
                    {
                        MessageBoxHandler.Show(string.Format(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_38), HelperFactory.GetLabelConvert().GetLabelText("PrimaryKey")));
                        EditProperty.SetValue(currentObj, gv.ActiveEditor.OldEditValue);
                        ((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).SetIsFormControlChanged(true);
                        gv.BestFitColumns();
                        return;
                    }
                }
                #endregion

                PropertyInfo updateId = type.GetProperty("UpdateId");
                PropertyInfo updateTime = type.GetProperty("UpdateTime");

                if (updateId != null)
                    updateId.SetValue(currentObj, HKInc.Utils.Common.GlobalVariable.LoginId);
                if (updateTime != null)
                    updateTime.SetValue(currentObj, DateTime.Now);

                var thisForm = this.FindForm() as HKInc.Utils.Interface.Forms.IFormControlChanged;
                if(thisForm != null)
                    thisForm.SetIsFormControlChanged(true);

                gv.BestFitColumns();
            }
        }

        #region CheckBoxMulti를 위한 변수 및 이벤트 Gird AddColumn으로 RowId와 _Check 필요 RowId는 Visible false 로 해주길 바람.

        private int? ShiftRowHandleCheck = null;
        private string _KeyCoulmn = null;
        private void MainView_MouseUp_Check(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            if (_KeyCoulmn == null) return;

            BindingSource GridBindingSource = view.DataSource as BindingSource;
            if (GridBindingSource == null) return;
               
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = view.CalcHitInfo(e.Location);
                if (hInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Column && hInfo.Column == view.Columns["_Check"])
                {
                    try
                    {
                        view.CellValueChanged -= MainView_CellValueChanged_Check;

                        WaitHandler.ShowWait();
                        int RowCount = view.RowCount;
                        if (RowCount == 0) return;

                        var FilterList = new List<object>();
                        for (int i = 0; i < RowCount; i++)
                            FilterList.Add(view.GetRowCellValue(i, _KeyCoulmn).GetNullToEmpty());

                        foreach (var v in GridBindingSource.List)
                        {
                            Type type = v.GetType();
                            PropertyInfo Check = type.GetProperty("_Check");
                            PropertyInfo KeyInfo = type.GetProperty(_KeyCoulmn);

                            if (KeyInfo == null || Check == null) return;
                            if (FilterList.Contains(KeyInfo.GetValue(v).ToString()) && Check.GetValue(v).ToString() == "Y")
                            {
                                RowCount--;
                            }
                        }

                        if (RowCount == 0)
                        {
                            foreach (var v in GridBindingSource.List)
                            {
                                Type type = v.GetType();
                                PropertyInfo Check = type.GetProperty("_Check");
                                PropertyInfo KeyInfo = type.GetProperty(_KeyCoulmn);
                                if (FilterList.Contains(KeyInfo.GetValue(v).ToString()))
                                    Check.SetValue(v, "N");
                            }
                        }
                        else
                        {
                            foreach (var v in GridBindingSource.List)
                            {
                                Type type = v.GetType();
                                PropertyInfo Check = type.GetProperty("_Check");
                                PropertyInfo KeyInfo = type.GetProperty(_KeyCoulmn);
                                if (FilterList.Contains(KeyInfo.GetValue(v).ToString()))
                                    Check.SetValue(v, "Y");
                            }
                        }

                        view.RefreshData();
                        if (!((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).GetIsFormControlChanged())
                            ((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).SetIsFormControlChanged(false);
                    }
                    finally
                    {
                        WaitHandler.CloseWait();
                        view.CellValueChanged += MainView_CellValueChanged_Check;
                    }
                }
                else if (Control.ModifierKeys == Keys.Shift && hInfo.Column == view.Columns["_Check"] && ShiftRowHandleCheck != null)
                {
                    try
                    {
                        view.CellValueChanged -= MainView_CellValueChanged_Check;

                        if (view.GetRowCellValue((int)ShiftRowHandleCheck, "_Check").GetNullToEmpty() == "Y")
                            view.SetRowCellValue((int)ShiftRowHandleCheck, "_Check", "N");
                        else
                            view.SetRowCellValue((int)ShiftRowHandleCheck, "_Check", "Y");

                        WaitHandler.CloseWait();
                        WaitHandler.ShowWait();
                        int MaxRowHandle;
                        int MinRowHandle;
                        int RowHandle = view.FocusedRowHandle;
                        if (ShiftRowHandleCheck > RowHandle)
                        {
                            MaxRowHandle = ShiftRowHandleCheck.GetIntNullToZero();
                            MinRowHandle = RowHandle.GetIntNullToZero();
                        }
                        else if (ShiftRowHandleCheck < RowHandle)
                        {
                            MaxRowHandle = RowHandle.GetIntNullToZero();
                            MinRowHandle = ShiftRowHandleCheck.GetIntNullToZero();
                        }
                        else
                        {
                            MaxRowHandle = ShiftRowHandleCheck.GetIntNullToZero();
                            MinRowHandle = ShiftRowHandleCheck.GetIntNullToZero();
                        }

                        var FilterList = new List<object>();
                        var FilterList2 = new List<object>();
                        for (int i = MinRowHandle; i <= MaxRowHandle; i++)
                        {
                            if (view.GetRowCellValue(i, "_Check").GetNullToEmpty() == "Y")
                                FilterList2.Add(view.GetRowCellValue(i, _KeyCoulmn).GetNullToEmpty());
                            else
                                FilterList.Add(view.GetRowCellValue(i, _KeyCoulmn).GetNullToEmpty());
                        }

                        foreach (var v in GridBindingSource.List)
                        {
                            Type type = v.GetType();
                            PropertyInfo KeyInfo = type.GetProperty(_KeyCoulmn);

                            if (KeyInfo == null) return;

                            if (FilterList.Contains(KeyInfo.GetValue(v).GetNullToEmpty()))
                            {
                                PropertyInfo Check = type.GetProperty("_Check");
                                if (Check == null) return;
                                Check.SetValue(v, "Y");
                            }
                        }

                        foreach (var v in GridBindingSource.List)
                        {
                            Type type = v.GetType();
                            PropertyInfo KeyInfo = type.GetProperty(_KeyCoulmn);
                            if (KeyInfo == null) return;

                            if (FilterList2.Contains(KeyInfo.GetValue(v).GetNullToEmpty()))
                            {
                                PropertyInfo Check = type.GetProperty("_Check");
                                if (Check == null) return;
                                Check.SetValue(v, "N");
                            }
                        }
                        view.RefreshData();
                        if (!((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).GetIsFormControlChanged())
                            ((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).SetIsFormControlChanged(false);
                    }
                    finally
                    {
                        WaitHandler.CloseWait();
                        ShiftRowHandleCheck = null;
                        view.CellValueChanged += MainView_CellValueChanged_Check;
                    }
                } //다중선택시 
                else if (Control.ModifierKeys == Keys.None && hInfo.Column == view.Columns["_Check"] && ShiftRowHandleCheck == null)
                {
                    if (view.GetFocusedRowCellValue("_Check").GetNullToEmpty() == "Y")
                        view.SetFocusedRowCellValue("_Check", "N");
                    else
                        view.SetFocusedRowCellValue("_Check", "Y");
                } //다중선택이후 
            }
        }

        private void MainView_CellValueChanged_Check(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            if (_KeyCoulmn == null) return;

            BindingSource CurrentBindingSoruce = view.DataSource as BindingSource;
            if (CurrentBindingSoruce == null) return;

            object currentObj = CurrentBindingSoruce.Current;
            Type type = currentObj.GetType();

            PropertyInfo _Check = type.GetProperty("_Check");
            if (_Check != null)
            {
                ShiftRowHandleCheck = view.FocusedRowHandle;
                if (!((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).GetIsFormControlChanged())
                    ((HKInc.Utils.Interface.Forms.IFormControlChanged)this.FindForm()).SetIsFormControlChanged(false);
            }
        }

        #endregion

        #region Property

        public bool ShowColumnHeaders { get { return _ShowColumnHeaders; } set { _ShowColumnHeaders = value; } }

        public object DataSource
        {
            get { return gridControl.DataSource; }
            set { gridControl.DataSource = value; }
        }

        public GridControl MainGrid
        {
            get { return gridControl; }
        }

        public GridView MainView
        {
            get { return view; }
            set { view = value; }
        }
        
        public bool EnableAppearanceEvenRow
        {
            get { return view.OptionsView.EnableAppearanceEvenRow; }
            set { view.OptionsView.EnableAppearanceEvenRow = value; }
        }

        public bool EnableAppearanceOddRow
        {
            get { return view.OptionsView.EnableAppearanceOddRow; }
            set { view.OptionsView.EnableAppearanceOddRow = value; }
        }

        public bool ShowGroupPanel
        {
            get { return view.OptionsView.ShowGroupPanel; }
            set { view.OptionsView.ShowGroupPanel = value; }
        }

        public bool ShowDetailButtons
        {
            get { return view.OptionsView.ShowDetailButtons; }
            set { view.OptionsView.ShowDetailButtons = value; }
        }

        public bool MultiSelect
        {
            get { return view.OptionsSelection.MultiSelect; }
            set { view.OptionsSelection.MultiSelect = value; }
        }

        public GridMultiSelectMode MultiSelectMode
        {
            get { return view.OptionsSelection.MultiSelectMode; }
            set { view.OptionsSelection.MultiSelectMode = value; }
        }

        public bool ColumnAutoWidth
        {
            get { return view.OptionsView.ColumnAutoWidth; }
            set { view.OptionsView.ColumnAutoWidth = value; }
        }

        public bool EnableAppearanceFocusedRow
        {
            get { return view.OptionsSelection.EnableAppearanceFocusedRow; }
            set { view.OptionsSelection.EnableAppearanceFocusedRow = value; }
        }

        public bool EnableAppearanceFocusedCell
        {
            get { return view.OptionsSelection.EnableAppearanceFocusedCell; }
            set { view.OptionsSelection.EnableAppearanceFocusedCell = value; }
        }

        public bool ShowFooter
        {
            get { return view.OptionsView.ShowFooter; }
            set { view.OptionsView.ShowFooter = value; }
        }

        public DrawFocusRectStyle FocusRectStyle
        {
            get { return view.FocusRectStyle; }
            set { view.FocusRectStyle = value; }
        }

        public int RecordCount
        {
            get { return view.DataRowCount; }
        }

        public bool ReadOnly
        {
            get { return view.OptionsBehavior.ReadOnly; }
            set { view.OptionsBehavior.ReadOnly = value; }
        }

        public bool Editable
        {
            get { return view.OptionsBehavior.Editable; }
            set { view.OptionsBehavior.Editable = value; }
        }

        public GridColumnCollection Columns
        {
            get { return view.Columns; }
        }
        
        public GridColumn FocusedColumn
        {
            get { return view.FocusedColumn; }
            set { view.FocusedColumn = value; }
        }

        public int FocusedRowHandle
        {
            get { return view.FocusedRowHandle; }
            set { view.FocusedRowHandle = value; }
        }

        public int SelectedRowCount
        {
            get { return view.SelectedRowsCount; }
        }

        public int[] SelectedRows
        {
            get { return view.GetSelectedRows(); }
        }

        private void GridLookUp_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit lookup = sender as GridLookUpEdit;
            if (lookup == null) return;

            if (lookup.Properties.DataSource != null)
            {
                Type type = lookup.Properties.DataSource.GetType();

                FieldInfo[] fields = type.GetFields();

                if (fields.Any(p => p.Name.Equals("UserFlag")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[UseFlag] == 'Y'";
                else if (fields.Any(p => p.Name.Equals("Active")))
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[Active] == 'Y'";
            }
        }

        public GridCell[] SelectedCells
        {
            get { return view.GetSelectedCells(); }
        }
        
        public GridViewType ViewType
        {
            get { return _GridViewType; }
            set
            {
                if (value == GridViewType.AdvBandedGridView)
                {
                    AdvBandedGridView advBandedView = new AdvBandedGridView();

                    gridControl.ViewCollection.Clear();
                    view = advBandedView;
                    gridControl.MainView = advBandedView;
                    gridControl.ViewCollection.AddRange(new BaseView[] {advBandedView});
                    advBandedView.GridControl = gridControl;
                    advBandedView.Name = "view";

                    this.view.OptionsView.ShowColumnHeaders = _ShowColumnHeaders;                                        
                }
                else if (value == GridViewType.BandedGridView)
                {
                    BandedGridView bandedView = new BandedGridView();

                    gridControl.ViewCollection.Clear();
                    view = bandedView;
                    gridControl.MainView = bandedView;
                    gridControl.ViewCollection.AddRange(new BaseView[] {bandedView});
                    bandedView.GridControl = gridControl;
                    bandedView.Name = "view";

                    this.view.OptionsView.ShowColumnHeaders = _ShowColumnHeaders;
                }
                else if (value == GridViewType.POP_GridView)
                {
                    Grid_POP_View PopView = new Grid_POP_View();
                    gridControl.ViewCollection.Clear();
                    view = PopView;
                    gridControl.MainView = PopView;
                    gridControl.ViewCollection.AddRange(new BaseView[] { PopView });
                    PopView.GridControl = gridControl;
                    PopView.Name = "view";

                    this.view.OptionsView.ShowColumnHeaders = _ShowColumnHeaders;

                    InitGridViewOptions();
                    InitEmptyRepositoryItem();
                    InitGridViewEvent();
                }

                _GridViewType = value;
            }            
        }

        #endregion

        #region Function

        public void Init()
        {
            InitGridViewOptions();
        }

        /// <summary>
        /// Check Repository 다중선택기능 
        /// </summary>
        /// <param name="isEdit">UserRight HasEdit</param>
        /// <param name="KeyColumn"> 고유 값 컬럼 필드명</param>
        /// <param name="flag">사용여부</param>
        public void CheckBoxMultiSelect(bool isEdit, string KeyColumn, bool flag)
        {
            if (isEdit)
            {
                if (flag)
                {
                    this.MainView.MouseUp += MainView_MouseUp_Check;
                    this.MainView.CellValueChanged += MainView_CellValueChanged_Check;
                    this._KeyCoulmn = KeyColumn;
                }
                else
                {
                    this.MainView.MouseUp -= MainView_MouseUp_Check;
                    this.MainView.CellValueChanged -= MainView_CellValueChanged_Check;
                    this._KeyCoulmn = null;
                }
            }
        }

        public void BestFitColumns(int MaxRowCount = 200)
        {
            view.OptionsView.BestFitMaxRowCount = MaxRowCount; //상단 가시 행에서 10개 행까지만 
            view.BestFitColumns();
        }

        public void PostEditor() {view.PostEditor();}

        public void UpdateCurrentRow() {view.UpdateCurrentRow();}
        
        public void SetColumnWidth(int colIndex, int width)
        {
            view.Columns[colIndex].OptionsColumn.AllowSize = false;
            view.Columns[colIndex].Width = width;
        }

        public void SetColumnWidth(string fieldName, int width)
        {
            view.Columns[fieldName].OptionsColumn.AllowSize = false;
            view.Columns[fieldName].Width = width;
        }

        public void SetColumnEditable(int colIndex, bool editable)
        {
            view.Columns[colIndex].OptionsColumn.ReadOnly = !editable;
            view.Columns[colIndex].OptionsColumn.AllowEdit = editable;
            view.Columns[colIndex].OptionsColumn.AllowFocus = editable;
            SetEditStyle();
        }

        public void SetColumnEditable(string fieldName, bool editable)
        {
            view.Columns[fieldName].OptionsColumn.ReadOnly = !editable;
            view.Columns[fieldName].OptionsColumn.AllowEdit = editable;
            view.Columns[fieldName].OptionsColumn.AllowFocus = editable;
            if (editable)
            {
                if (view.Columns[fieldName].ColumnEdit is CommentGridButtonEdit && ((RepositoryItemButtonEdit)view.Columns[fieldName].ColumnEdit).TextEditStyle == TextEditStyles.DisableTextEditor)
                    view.Columns[fieldName].AppearanceCell.Options.UseBackColor = false;
                else
                    view.Columns[fieldName].AppearanceCell.BackColor = HKInc.Utils.Common.GlobalVariable.GridEditableColumnColor;
            }
            else
                view.Columns[fieldName].AppearanceCell.Options.UseBackColor = false;

            SetEditStyle();
        }

        private void SetEditStyle()
        {
            bool bEditable = false;
            bEditable = view.Columns.Any(p => p.OptionsColumn.AllowEdit == true);

            view.OptionsSelection.EnableAppearanceFocusedCell = bEditable;
            view.Appearance.FocusedCell.BorderColor = Color.Red;
            //view.Appearance.FocusedCell.BackColor = Color.Yellow; //2018.08.25 phs 기존주석 막음
            view.Appearance.FocusedCell.ForeColor = Color.Black;
            view.Appearance.FocusedCell.Options.UseBorderColor = bEditable;
            view.Appearance.FocusedCell.Options.UseBackColor = bEditable;
            view.FocusRectStyle = (bEditable) ? DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus : DrawFocusRectStyle.RowFocus;
        }

        public void SetEditable(bool editable)
        {
            Editable = editable;
            ReadOnly = !editable;
            SetEditStyle();
            IsEditableSetByUser = true;
        }

        public void SetEditable(params string[] columns)
        {
            if (IsEditableSetByUser && !Editable) return;

            if (columns != null && columns.Length > 0)
            {                
                SetEditable(true);
                foreach (string col in columns)
                {
                    if (!String.IsNullOrEmpty(col))
                    {
                        SetColumnEditable(col, true);
                    }
                }
            }
            SetEditStyle();
        }
                   
        public void SetEditable(bool isEdit, params string[] columns)
        {
            if (columns != null && columns.Length > 0)
            {
                SetEditable(isEdit);
                SetEditable(columns);
            }
            SetEditStyle();
        }

        public void SetHeaderColor(Color color, params string[] columns)
        {
            if (columns != null && columns.Length > 0)
            {
                foreach (var fieldName in columns)
                {
                    view.Columns[fieldName].AppearanceHeader.ForeColor = color;
                }
            }
        }

        public void SetEditDisable(params string[] columns)
        {
            if (IsEditableSetByUser && !Editable) return;

            if (columns != null && columns.Length > 0)
            {
                SetEditable(true);
                foreach (string col in columns)
                {
                    if (!String.IsNullOrEmpty(col))
                    {
                        SetColumnEditable(col, false);
                    }
                }
            }
            SetEditStyle();
        }

        public void SetEditDisable(bool isEdit, params string[] columns)
        {
            if (columns != null && columns.Length > 0)
            {
                SetEditDisable(isEdit);
                SetEditDisable(columns);
            }
            SetEditStyle();
        }

        public bool IsExitsColumn(string fieldName)
        {
            return view.Columns.Cast<GridColumn>().Any(x => x.FieldName == fieldName);
        }

        public void Clear()
        {
            if (this.DataSource != null)
            {
                if (DataSource is DataTable)                
                    (DataSource as DataTable).Rows.Clear();                                    
                else                
                    DataSource = null;
                
                this.MainView.RefreshData();
            }
        }

        public void SummaryItemAdd(int colIndex)
        {
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = view.Columns[colIndex].FieldName;
            item.ShowInGroupColumnFooter = view.Columns[colIndex];
            item.ShowInGroupColumnFooterName = view.Columns[colIndex].Name;
            item.SummaryType = SummaryItemType.Sum;
            item.DisplayFormat = view.Columns[colIndex].DisplayFormat.FormatString;
            view.GroupSummary.Add(item);
        }

        public void SetGridFont(GridView view, Font font)
        {
            
            foreach (AppearanceObject ap in view.Appearance)
                ap.Font = font;
        }

        public void GridLayoutInit()
        {
            Utils.Interface.Service.IService<Ui.Model.Domain.GridLayout> ModelService =
                (Utils.Interface.Service.IService<Ui.Model.Domain.GridLayout>)ServiceFactory.GetDomainService("GridLayout");

            string LoginId = Utils.Common.GlobalVariable.LoginId;
            string FormName = string.Empty;
            string NameSpace = string.Empty;
            if (this.ParentForm.Name.Split('.').Count() > 1)
                FormName = this.ParentForm.Name.Split('.')[this.ParentForm.Name.Split('.').Count() - 2].GetNullToEmpty();
            else
                FormName = this.ParentForm.Name;
            NameSpace = this.ParentForm.CompanyName;

            var obj = ModelService.GetList(p => p.LoginId == LoginId && p.NameSpace == NameSpace && p.FormName == FormName && p.GridName == this.gridControl.Parent.Parent.Name).FirstOrDefault();
            if (obj != null)
            {
                ModelService.Delete(obj);
                ModelService.Save();
            }
            ModelService.Dispose();
            MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_44));
        }

        public void GridLayoutRestore()
        {
            Utils.Interface.Service.IService<Ui.Model.Domain.GridLayout> ModelService =
                  (Utils.Interface.Service.IService<Ui.Model.Domain.GridLayout>)ServiceFactory.GetDomainService("GridLayout");
            string LoginId = Utils.Common.GlobalVariable.LoginId;
            string FormName = string.Empty;
            string NameSpace = string.Empty;
            if (this.ParentForm.Name.Split('.').Count() > 1)
                FormName = this.ParentForm.Name.Split('.')[this.ParentForm.Name.Split('.').Count() - 2].GetNullToEmpty();
            else
                FormName = this.ParentForm.Name;
            NameSpace = this.ParentForm.CompanyName;

            var obj = ModelService.GetList(p => p.LoginId == LoginId && p.NameSpace == NameSpace && p.FormName == FormName && p.GridName == this.gridControl.Parent.Parent.Name).FirstOrDefault();
            if (obj != null)
            {
                // RESTORE
                string text = obj.LayoutData;
                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(text);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                this.gridControl.MainView.RestoreLayoutFromStream(stream);
            }
            ModelService.Dispose();
        }

        #endregion

        #region AddColumn() Function

        public GridColumn AddUnboundColumn(string fieldName, string caption, UnboundColumnType columnType, string unboundExpression, FormatType formatType, string formatString)
        {
            ILabelConvert LabelConvert = HelperFactory.GetLabelConvert();

            if (view is AdvBandedGridView)
            {
                BandedGridColumn column = (view as AdvBandedGridView).AddColumnEx(fieldName, string.IsNullOrEmpty(caption) ? LabelConvert.GetLabelText(fieldName) : caption);

                GridBand gridBand = (view as AdvBandedGridView).Bands.Add();
                gridBand.Caption = caption;
                gridBand.Columns.Add(column);
                column.OwnerBand = gridBand;
                gridBand.Visible = true;

                column.VisibleIndex = view.Columns.Count;
                column.UnboundType = columnType;
                column.OptionsColumn.AllowEdit = false;
                column.UnboundExpression = unboundExpression;
                column.DisplayFormat.FormatType = formatType;
                column.DisplayFormat.FormatString = formatString;
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                if (columnType == UnboundColumnType.Decimal || columnType == UnboundColumnType.Integer)
                    column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                return column;
            }
            else if (view is BandedGridView)
            {
                BandedGridColumn column = (view as BandedGridView).AddColumnEx(fieldName, string.IsNullOrEmpty(caption) ? LabelConvert.GetLabelText(fieldName) : caption);

                GridBand gridBand = (view as BandedGridView).Bands.Add();
                gridBand.Caption = caption;
                gridBand.Columns.Add(column);
                column.OwnerBand = gridBand;
                gridBand.Visible = true;

                column.VisibleIndex = view.Columns.Count;
                column.UnboundType = columnType;
                column.OptionsColumn.AllowEdit = false;
                column.UnboundExpression = unboundExpression;
                column.DisplayFormat.FormatType = formatType;
                column.DisplayFormat.FormatString = formatString;
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                if (columnType == UnboundColumnType.Decimal || columnType == UnboundColumnType.Integer)
                    column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                return column;
            }
            else
            {
                GridColumn column = view.Columns.AddField(fieldName);
                column.Caption = string.IsNullOrEmpty(caption) ? LabelConvert.GetLabelText(fieldName) : caption;
                column.VisibleIndex = view.Columns.Count;
                column.UnboundType = columnType;
                column.OptionsColumn.AllowEdit = false;
                column.UnboundExpression = unboundExpression;
                column.DisplayFormat.FormatType = formatType;
                column.DisplayFormat.FormatString = formatString;
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                if (columnType == UnboundColumnType.Decimal || columnType == UnboundColumnType.Integer)
                    column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

                return column;
            }
           
        }

        public GridColumn AddColumn() {return view.Columns.Add();}

        public GridColumn AddColumn(string fieldName, string caption, System.Drawing.Color foreColor, string fileField = null)
        {
            GridColumn column = AddColumn(fieldName, caption, HorzAlignment.Center, true);
            column.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            column.AppearanceCell.Font = new System.Drawing.Font(column.AppearanceCell.Font, System.Drawing.FontStyle.Underline);

            if (!string.IsNullOrEmpty(fileField))
                AddColumn(fileField, false);

            return column;
        }

        public BandedGridColumn AddBandedColumn(string fieldName, string caption, GridBand ownerBand, HorzAlignment align = HorzAlignment.Center)
        {
            BandedGridColumn bandedColumn = (view as BandedGridView).AddColumnEx(fieldName, caption, align, FormatType.Numeric, "n0", true, true, false, SummaryItemType.None, false, 0);

            bandedColumn.OptionsColumn.AllowSize = true;
            bandedColumn.OptionsColumn.AllowMove = true;
            bandedColumn.BestFit();
            bandedColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;           
            bandedColumn.OwnerBand = ownerBand;
            ownerBand.Visible = true;

            return bandedColumn;
        }
        //3
        public GridColumn AddColumn(string fieldName, string caption, HorzAlignment align, int width, FormatType formatType, string formatString, bool visible, bool readOnly, bool summary, SummaryItemType summaryType, bool group, int fieldNo)
        {
            if (Columns[fieldName] != null)
                return Columns[fieldName];

            try
            {                
                if (view is AdvBandedGridView)
                {
                    BandedGridColumn bandedColumn = (view as AdvBandedGridView).AddColumnEx(fieldName, caption, align, formatType, formatString, visible, readOnly, summary, summaryType, group, width);

                    bandedColumn.OptionsColumn.AllowSize = true;
                    bandedColumn.OptionsColumn.AllowMove = true;
                    bandedColumn.BestFit();
                    bandedColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                    GridBand gridBand = (view as AdvBandedGridView).Bands.Add();
                    gridBand.Caption = caption;
                    gridBand.Columns.Add(bandedColumn);
                    bandedColumn.OwnerBand = gridBand;
                    gridBand.Visible = visible;

                    bandedColumn.OptionsColumn.AllowFocus = true; //2018.08.25 phs 추가

                    return bandedColumn;
                }
                else if (view is BandedGridView)
                {
                    BandedGridColumn bandedColumn = (view as BandedGridView).AddColumnEx(fieldName, caption, align, formatType, formatString, visible, readOnly, summary, summaryType, group, width);

                    bandedColumn.OptionsColumn.AllowSize = true;
                    bandedColumn.OptionsColumn.AllowMove = true;
                    bandedColumn.BestFit();
                    bandedColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                    GridBand gridBand = (view as BandedGridView).Bands.Add();
                    gridBand.Caption = caption;
                    gridBand.Columns.Add(bandedColumn);
                    bandedColumn.OwnerBand = gridBand;
                    gridBand.Visible = visible;

                    bandedColumn.OptionsColumn.AllowFocus = true; //2018.08.25 phs 추가

                    return bandedColumn;
                }
                else
                {
                    GridColumn col = AddColumn();
                    col.Name = fieldName;
                    col.AppearanceCell.Options.UseTextOptions = true;
                    col.AppearanceCell.TextOptions.HAlignment = align;
                    col.OptionsColumn.AllowMerge = DefaultBoolean.False;
                    col.Caption = caption;
                    col.CustomizationCaption = caption;
                    col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    col.FieldName = fieldName;
                    col.DisplayFormat.FormatType = formatType;
                    col.GroupFormat.FormatType = formatType;
                    if(fieldName == "CreateTime")
                    {
                        col.DisplayFormat.FormatType = FormatType.DateTime;
                        col.GroupFormat.FormatType = FormatType.DateTime;
                        col.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
                        col.GroupFormat.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
                    }
                    else if(fieldName == "UpdateTime")
                    {
                        col.DisplayFormat.FormatType = FormatType.DateTime;
                        col.GroupFormat.FormatType = FormatType.DateTime;
                        col.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
                        col.GroupFormat.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
                    }
                    else if (fieldName == "Seq")
                    {
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.GroupFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "n0";
                        col.GroupFormat.FormatString = "n0";
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    }

                    if (formatType == FormatType.DateTime)
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    else if(formatType == FormatType.Numeric)
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;


                    if (!String.IsNullOrEmpty(formatString))
                    {
                        col.DisplayFormat.FormatString = formatString;
                        col.GroupFormat.FormatString = formatString;
                    }
                    
                    if (summary)
                    {
                        col.SummaryItem.SummaryType = summaryType;
                        col.SummaryItem.FieldName = fieldName;

                        if (!String.IsNullOrEmpty(formatString))                        
                            col.SummaryItem.DisplayFormat = String.Format("{{0:{0}}}", formatString);                        

                        if (ShowGroupPanel && group)
                        {
                            GridGroupSummaryItem item = new GridGroupSummaryItem()
                            {
                                FieldName = fieldName,
                                ShowInGroupColumnFooter = col,
                                ShowInGroupColumnFooterName = fieldName,
                                SummaryType = summaryType,
                                Tag = fieldName
                            };
                            if (!String.IsNullOrEmpty(formatString))
                                item.DisplayFormat = String.Format("{{0:{0}}}", formatString);

                            view.GroupSummary.Add(item);
                            col.OptionsColumn.AllowGroup = DefaultBoolean.True;
                        }
                    }

                    if (ReadOnly || Editable == false || readOnly)
                    {
                        col.OptionsColumn.ReadOnly = true;
                        col.OptionsColumn.AllowEdit = false;
                        col.OptionsColumn.AllowFocus = false;
                    }
                    else
                    {
                        col.OptionsColumn.ReadOnly = false;
                        col.OptionsColumn.AllowEdit = true;
                        col.OptionsColumn.AllowFocus = true;
                    }
                    if (width > 0)
                    {
                        col.OptionsColumn.AllowSize = false;
                        col.Width = width;
                    }

                    col.OptionsColumn.AllowFocus = true;
                    col.Visible = visible;
                    if (visible) col.VisibleIndex = view.Columns.Count;

                    return col;
                }
            }
            catch (Exception ex)
            {
                Handler.MessageBoxHandler.ErrorShow(ex);
                return null;
            }
        }

        public GridColumn AddColumn(string fieldName, bool visible, HorzAlignment alignment = HorzAlignment.Near)
        {
            return AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), alignment, FormatType.None, null, visible);
        }

        public GridColumn AddColumn(string fieldName, bool visible)
        {
            return AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), HorzAlignment.Near, FormatType.None, null, visible);
        }

        public GridColumn AddColumn(string fieldName)
        {
            return AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), HorzAlignment.Near, FormatType.None, null, true);
        }

        public GridColumn AddColumn(string fieldName, HorzAlignment alignment = HorzAlignment.Near, bool visible = true)
        {
            return AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), alignment, FormatType.None, null, visible);
        }               

        public GridColumn AddColumn(string fieldName, HorzAlignment alignment, FormatType formatType = FormatType.Numeric, string formatString = "n0", bool visible = true)
        {
            return AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), alignment, formatType, formatString, visible);
        }

        public GridColumn AddColumn(string fieldName, HorzAlignment alignment, FormatType formatType = FormatType.Numeric, string formatString = "n0")
        {
            return AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), alignment, formatType, formatString, true);
        }

        public GridColumn AddColumn(string fieldName, string caption, bool visible, HorzAlignment alignment = HorzAlignment.Near)
        {
            return AddColumn(fieldName, caption, alignment, FormatType.None, null, visible);
        }

        public GridColumn AddColumn(string fieldName, string caption)
        {
            return AddColumn(fieldName, caption, HorzAlignment.Near, FormatType.None, null, true);
        }
        //1
        public GridColumn AddColumn(string fieldName, string caption, HorzAlignment alignment = HorzAlignment.Near, bool visible = true)
        {
            return AddColumn(fieldName, caption, alignment, FormatType.None, null, visible);
        }
        public GridColumn AddColumn(string fieldName, string caption, HorzAlignment alignment = HorzAlignment.Near, bool visible = true,int width=0)
        {
            return AddColumn(fieldName, caption, alignment, FormatType.None, null, visible, width);
        }
        //2
        public GridColumn AddColumn(string fieldName, string caption, HorzAlignment alignment, FormatType formatType = FormatType.Numeric, string formatString = "n0", bool visible = true,int width=0)
        {
            return AddColumn(fieldName, caption, alignment, width, formatType, formatString, visible, true, false, SummaryItemType.None, false, 0);
        }
        //2
        public GridColumn AddColumn(string fieldName, string caption, HorzAlignment alignment, FormatType formatType = FormatType.Numeric, string formatString = "n0", bool visible = true)
        {
            return AddColumn(fieldName, caption, alignment, 0, formatType, formatString, visible, true, false, SummaryItemType.None, false, 0);
        }

        public GridColumn AddColumn(string fieldName, string caption, HorzAlignment alignment, FormatType formatType = FormatType.Numeric, string formatString = "n0")
        {
            return AddColumn(fieldName, caption, alignment, 0, formatType, formatString, true, true, false, SummaryItemType.None, false, 0);
        }

        public GridColumn AddColumn(string fieldName, HorzAlignment align, FormatType formatType, string formatString, SummaryItemType summaryType, bool group = false)
        {
            return AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), align, 0, formatType, formatString, true, true, true, summaryType, group, 0);
        }

        public GridColumn AddColumn(string fieldName, string caption, HorzAlignment align, FormatType formatType, string formatString, SummaryItemType summaryType, bool group = false)
        {
            return AddColumn(fieldName, caption, align, 0, formatType, formatString, true, true, true, summaryType, group, 0);
        }
                
        #endregion

        public HKInc.Ui.Model.Domain.Screen ContainScreen
        {
            get { return ContainScreen; }
            set { ContainScreen = value; }
        }
    }
}
