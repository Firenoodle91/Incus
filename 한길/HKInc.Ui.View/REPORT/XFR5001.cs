using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.REPORT
{
    public partial class XFR5001 : HKInc.Service.Base.BaseForm
    {
        private BindingSource bindingSource1 = new BindingSource();

        public XFR5001()
        {
            InitializeComponent();
            gridEx1.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.CustomDrawColumnHeader += MainView_CustomDrawColumnHeader;
        }

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0 && e.Column.FieldName == "Status")
            {
                var RemainQty = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "RemainQty").GetIntNullToZero();
                if (RemainQty > 0)
                    e.Appearance.ForeColor = Color.Red;
            }
        }

        private void MainView_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return;
            Rectangle rect = e.Bounds;
            //I am setting both the border and the backcolor of the Column as same (In this case white color)
            ControlPaint.DrawBorder(e.Graphics, e.Bounds, Color.White, ButtonBorderStyle.Solid);

            Brush brush =
                e.Cache.GetGradientBrush(rect, Color.Black,
                e.Column.AppearanceHeader.BackColor2, e.Column.AppearanceHeader.GradientMode);
            rect.Inflate(-1, -1);
            // Fill column headers with the specified colors.
            e.Graphics.FillRectangle(brush, rect);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            // Draw the filter and sort buttons.
            foreach (DevExpress.Utils.Drawing.DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                DevExpress.Utils.Drawing.ObjectPainter.DrawObject(e.Cache, info.ElementPainter,
                    info.ElementInfo);
            }
            e.Handled = true;
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Status")
            {
                var RemainQty = gridEx1.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "RemainQty").GetIntNullToZero();                
                if (RemainQty > 0)
                    e.DisplayText = "미출고";                
                else
                    e.DisplayText = "출고";
            }
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();
            SetToolbarVisible(false);
            SetStaticBarVisible(false);
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.UseEmbeddedNavigator(false);

            gridEx1.MainGrid.MainView.Appearance.HeaderPanel.BackColor = Color.Black;
            gridEx1.MainGrid.MainView.Appearance.Row.BackColor = Color.Black;

            gridEx1.MainGrid.MainView.Appearance.HeaderPanel.ForeColor = Color.White;
            gridEx1.MainGrid.MainView.Appearance.Row.ForeColor = Color.White;
            gridEx1.MainGrid.MainView.Appearance.VertLine.BackColor = Color.White;
            gridEx1.MainGrid.MainView.Appearance.HorzLine.BackColor = Color.White;

            gridEx1.MainGrid.MainView.OptionsView.EnableAppearanceEvenRow = false;
            gridEx1.MainGrid.MainView.OptionsView.EnableAppearanceOddRow = false;

            gridEx1.MainGrid.MainView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gridEx1.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridEx1.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;

            gridEx1.SetToolbarVisible(false);

            gridEx1.MainGrid.MainView.OptionsView.ShowIndicator = false;

            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 15));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 50;
            gridEx1.MainGrid.MainView.RowHeight = 50;
            
            gridEx1.MainGrid.AddColumn("RowNumber", "번호", HorzAlignment.Center, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("Status", "진행상황", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CustomerName", "거래처", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("ItemName", "품목", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("DelivQty", "출고지시수량", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("RemainQty", "잔량", HorzAlignment.Center, true);

            gridEx1.MainGrid.MainView.OptionsView.ColumnAutoWidth = true;
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("DelivQty", DefaultBoolean.Default, "n0", true, false);
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("OutQty", DefaultBoolean.Default, "n0", true, false);
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("RemainQty", DefaultBoolean.Default, "n0", true, false);
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database.SqlQuery<TempModel>("USP_GET_XFR5001").ToList();
                bindingSource1.DataSource = result.OrderBy(p => p.RowNumber).ToList();
            }
            gridEx1.DataSource = bindingSource1;

            gridEx1.BestFitColumns();
        }

        class TempModel
        {
            public Int64 RowNumber { get; set; }
            public string CustomerName { get; set; }
            public string ItemName { get; set; }
            public int? DelivQty { get; set; }
            public int? OutQty { get; set; }
            public int? BadQty { get; set; }
            public int? RemainQty { get; set; }
        }
    }
}