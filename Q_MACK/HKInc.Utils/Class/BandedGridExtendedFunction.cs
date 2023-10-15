using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace HKInc.Utils.Class
{
    public static class BandedGridEx
    {
        public static void InitEx(this AdvBandedGridView view)
        {          
            view.DataController.AllowIEnumerableDetails = true;

            view.OptionsView.EnableAppearanceEvenRow = true;
            view.OptionsView.EnableAppearanceOddRow = true;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowDetailButtons = false;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsView.ShowFooter = false;
            view.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            view.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowForFocusedRow;

            view.OptionsSelection.EnableAppearanceFocusedRow = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
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

            view.OptionsView.ShowIndicator = true;
            
            InitGridViewEvent(view);

        }
        public static void InitEx(this BandedGridView view)
        {
            view.DataController.AllowIEnumerableDetails = true;

            view.OptionsView.EnableAppearanceEvenRow = true;
            view.OptionsView.EnableAppearanceOddRow = true;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowDetailButtons = false;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsView.ShowFooter = false;
            view.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            view.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowForFocusedRow;

            view.OptionsSelection.EnableAppearanceFocusedRow = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
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

            view.OptionsView.ShowIndicator = true;

            InitGridViewEvent(view);
        }

        public static BandedGridColumn AddColumnEx(this AdvBandedGridView view,
                                                   string fieldName
                                                  , string caption = null
                                                  , HorzAlignment align = HorzAlignment.Near
                                                  , FormatType formatType = FormatType.None
                                                  , string formatString = null
                                                  , bool bVisible = true
                                                  , bool bReadonly = true
                                                  , bool bSummary = false
                                                  , SummaryItemType summaryType = SummaryItemType.Sum
                                                  , bool bGroup = false
                                                  , int width = 0)
        {
            try
            {
                BandedGridColumn col = new BandedGridColumn();

                col.Name = view.Name + "_" + fieldName;

                col.AppearanceCell.Options.UseTextOptions = true;
                col.AppearanceCell.TextOptions.HAlignment = align;
                col.OptionsColumn.AllowMerge = DefaultBoolean.False;
                col.Caption = caption;
                col.CustomizationCaption = caption;
                col.FieldName = fieldName;

                if (formatString != "")
                {
                    col.DisplayFormat.FormatString = formatString;
                    col.GroupFormat.FormatString = formatString;
                }
                col.DisplayFormat.FormatType = formatType;
                col.GroupFormat.FormatType = formatType;

                if (bSummary)
                {
                    col.SummaryItem.SummaryType = summaryType;
                    col.SummaryItem.FieldName = fieldName;
                    if (formatString != "")
                    {
                        if (formatString.IndexOf("{") > 0)
                            col.SummaryItem.DisplayFormat = formatString;
                        else
                            col.SummaryItem.DisplayFormat = "{0:" + formatString + "}";
                    }

                    if (view.OptionsView.ShowGroupPanel && bGroup)
                    {
                        GridSummaryItem item = new GridSummaryItem();
                        item.FieldName = fieldName;
                        if (formatString != "")
                        {
                            if (formatString.IndexOf("{") > 0)
                                item.DisplayFormat = formatString;
                            else
                                item.DisplayFormat = "{0:" + formatString + "}";
                        }
                        item.SummaryType = SummaryItemType.Sum;
                    }
                }

                if (bReadonly || !view.OptionsBehavior.Editable || view.OptionsBehavior.ReadOnly)
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
                    col.AppearanceCell.Options.UseBackColor = true;
                    col.AppearanceCell.BackColor = Color.AntiqueWhite;
                    col.AppearanceCell.BackColor2 = Color.AntiqueWhite;
                }

                if (width > 0)
                {
                    col.OptionsColumn.AllowSize = false;
                    col.Width = width;
                }

                col.Visible = bVisible;
                if (bVisible) col.VisibleIndex = view.Columns.Count;

                view.Columns.Add(col);

                return col;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static BandedGridColumn AddColumnEx(this BandedGridView view,
                                                   string fieldName
                                                 , string caption = null
                                                 , HorzAlignment align = HorzAlignment.Near
                                                 , FormatType formatType = FormatType.None
                                                 , string formatString = null
                                                 , bool bVisible = true
                                                 , bool bReadonly = true
                                                 , bool bSummary = false
                                                 , SummaryItemType summaryType = SummaryItemType.Sum
                                                 , bool bGroup = false
                                                 , int width = 0)
        {
            try
            {
                BandedGridColumn col = new BandedGridColumn();

                col.Name = view.Name + "_" + fieldName;

                col.AppearanceCell.Options.UseTextOptions = true;
                col.AppearanceCell.TextOptions.HAlignment = align;
                col.OptionsColumn.AllowMerge = DefaultBoolean.False;
                col.Caption = caption;
                col.CustomizationCaption = caption;
                col.FieldName = fieldName;
                col.DisplayFormat.FormatType = formatType;
                col.GroupFormat.FormatType = formatType;

                if (!String.IsNullOrEmpty(formatString))
                {
                    col.DisplayFormat.FormatString = formatString;
                    col.GroupFormat.FormatString = formatString;
                }

                if (bSummary)
                {
                    col.SummaryItem.SummaryType = summaryType;
                    col.SummaryItem.FieldName = fieldName;
                    if (formatString != "")
                    {
                        if (formatString.IndexOf("{") > 0)
                            col.SummaryItem.DisplayFormat = formatString;
                        else
                            col.SummaryItem.DisplayFormat = "{0:" + formatString + "}";
                    }

                    if (view.OptionsView.ShowGroupPanel && bGroup)
                    {
                        GridSummaryItem item = new GridSummaryItem();
                        item.FieldName = fieldName;
                        if (formatString != "")
                        {
                            if (formatString.IndexOf("{") > 0)
                                item.DisplayFormat = formatString;
                            else
                                item.DisplayFormat = "{0:" + formatString + "}";
                        }
                        item.SummaryType = SummaryItemType.Sum;
                    }
                }

                if (bReadonly || !view.OptionsBehavior.Editable || view.OptionsBehavior.ReadOnly)
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
                    col.AppearanceCell.Options.UseBackColor = true;
                    col.AppearanceCell.BackColor = Color.AntiqueWhite;
                    col.AppearanceCell.BackColor2 = Color.AntiqueWhite;
                }

                if (width > 0)
                {
                    col.OptionsColumn.AllowSize = false;
                    col.Width = width;
                }

                col.Visible = bVisible;
                if (bVisible) col.VisibleIndex = view.Columns.Count;

                view.Columns.Add(col);

                return col;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void SetGridBandEx(this AdvBandedGridView view, string bandCaption, params string[] columns)
        {
            var gridBand = new GridBand();
            gridBand.Caption = bandCaption;
            int nrOfColumns = columns.Length;
            BandedGridColumn[] bandedColumns = new BandedGridColumn[nrOfColumns];
            for (int i = 0; i < nrOfColumns; i++)
            {
                bandedColumns[i] = (BandedGridColumn)view.Columns.AddField(columns[i]);
                bandedColumns[i].OwnerBand = gridBand;
                bandedColumns[i].Visible = true;
            }
        }

        public static void SetGridBandAddedEx(this AdvBandedGridView view, string bandCaption, params string[] columns)
        {
            GridBand firstBand = null;

            for (int i = 0; i < columns.Length; i++)
            {
                if (i == 0)
                {
                    firstBand = view.Columns[columns[i]].OwnerBand;
                }
                else
                {
                    view.Columns[columns[i]].OwnerBand.Visible = false;
                }
            }
            firstBand.Caption = bandCaption;
            firstBand.OptionsBand.ShowCaption = true;
            firstBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            foreach (string fieldName in columns)
            {
                view.Columns[fieldName].OwnerBand = firstBand;
                view.Columns[fieldName].Visible = true;
            }
        }

        public static void SetGridBandAddedEx(this GridView baseView, string bandCaption, string bandName, params string[] columns)
        {
            if (baseView is AdvBandedGridView)
            {
                AdvBandedGridView view = baseView as AdvBandedGridView;
                GridBand firstBand = null;
                for (int i = 0; i < columns.Length; i++)
                {
                    if (i == 0)
                    {
                        firstBand = view.Columns[columns[i]].OwnerBand;                   
                    }
                    else
                    {
                        view.Columns[columns[i]].OwnerBand.Visible = false;
                    }
                }
                firstBand.Name = bandName;
                firstBand.Caption = bandCaption;
                firstBand.OptionsBand.ShowCaption = true;
                firstBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                foreach (string fieldName in columns)
                {
                    view.Columns[fieldName].OwnerBand = firstBand;
                }
            }
            else if (baseView is BandedGridView)
            {
                BandedGridView view = baseView as BandedGridView;
                GridBand firstBand = null;

                for (int i = 0; i < columns.Length; i++)
                {
                    if (i == 0)
                    {
                        firstBand = view.Columns[columns[i]].OwnerBand;
                    }
                    else
                    {
                        view.Columns[columns[i]].OwnerBand.Visible = false;
                    }
                }
                firstBand.Name = bandName;
                firstBand.Caption = bandCaption;
                firstBand.OptionsBand.ShowCaption = true;
                firstBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                foreach (string fieldName in columns)
                {
                    view.Columns[fieldName].OwnerBand = firstBand;
                }
            }

        }

        private static void InitGridViewEvent(object view)
        {
           var AdvBandedGridView = view as AdvBandedGridView;
           if (AdvBandedGridView != null)
           {
               AdvBandedGridView.CustomDrawRowIndicator += View_CustomDrawRowIndicator;
               AdvBandedGridView.RowCountChanged += View_RowCountChanged;
           }
           else
           {
               var BandedGridView = view as BandedGridView;
               BandedGridView.CustomDrawRowIndicator += View_CustomDrawRowIndicator;
               BandedGridView.RowCountChanged += View_RowCountChanged;
           }
        }
        private static void View_RowCountChanged(object sender, EventArgs e)
        {
            var AdvBandedGridView = sender as AdvBandedGridView;
            if (AdvBandedGridView != null)
            {
                if (!AdvBandedGridView.GridControl.IsHandleCreated) return;
                if (AdvBandedGridView.RowCount == 0) return;
                Graphics gr = Graphics.FromHwnd(AdvBandedGridView.GridControl.Handle);
                SizeF size = gr.MeasureString(AdvBandedGridView.RowCount.ToString(), AdvBandedGridView.PaintAppearance.Row.GetFont());
                AdvBandedGridView.IndicatorWidth = Convert.ToInt32(size.Width + 0.999f) + DevExpress.XtraGrid.Views.Grid.Drawing.GridPainter.Indicator.ImageSize.Width + 20;
            }
            else
            {
                var BandedGridView = sender as BandedGridView;
                if (!BandedGridView.GridControl.IsHandleCreated) return;
                if (BandedGridView.RowCount == 0) return;
                Graphics gr = Graphics.FromHwnd(BandedGridView.GridControl.Handle);
                SizeF size = gr.MeasureString(BandedGridView.RowCount.ToString(), BandedGridView.PaintAppearance.Row.GetFont());
                BandedGridView.IndicatorWidth = Convert.ToInt32(size.Width + 0.999f) + DevExpress.XtraGrid.Views.Grid.Drawing.GridPainter.Indicator.ImageSize.Width + 20;
            }
        }

        private static void View_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            }
        }
    }
}
