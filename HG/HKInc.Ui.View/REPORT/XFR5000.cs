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
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.REPORT
{
    public partial class XFR5000 : HKInc.Service.Base.BaseForm
    {
        private BindingSource bindingSource1 = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();

        public XFR5000()
        {
            InitializeComponent();
            gridEx1.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            gridEx1.MainGrid.MainView.CustomDrawColumnHeader += MainView_CustomDrawColumnHeader;
            gridEx2.MainGrid.MainView.CustomDrawColumnHeader += MainView_CustomDrawColumnHeader;

            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
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

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0 && e.Column.FieldName == "Status")
            {
                string Status = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]).ToString();
                if (Status == "작업중")
                {
                    e.Appearance.ForeColor = Color.Yellow;
                }
                else if (Status == "작업종료")
                {

                }                
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
            gridEx2.MainGrid.UseEmbeddedNavigator(false);

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


            gridEx2.MainGrid.MainView.Appearance.HeaderPanel.BackColor = Color.Black;
            gridEx2.MainGrid.MainView.Appearance.Row.BackColor = Color.Black;

            gridEx2.MainGrid.MainView.Appearance.HeaderPanel.ForeColor = Color.White;
            gridEx2.MainGrid.MainView.Appearance.Row.ForeColor = Color.White;
            gridEx2.MainGrid.MainView.Appearance.VertLine.BackColor = Color.White;
            gridEx2.MainGrid.MainView.Appearance.HorzLine.BackColor = Color.White;

            gridEx2.MainGrid.MainView.OptionsView.EnableAppearanceEvenRow = false;
            gridEx2.MainGrid.MainView.OptionsView.EnableAppearanceOddRow = false;

            gridEx2.MainGrid.MainView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gridEx2.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridEx2.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;

            gridEx1.SetToolbarVisible(false);
            gridEx2.SetToolbarVisible(false);

            gridEx1.MainGrid.MainView.OptionsView.ShowIndicator = false;
            gridEx2.MainGrid.MainView.OptionsView.ShowIndicator = false;

            gridEx1.MainGrid.MainView.Appearance.Row.BorderColor = Color.White;
            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 15));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 50;
            gridEx1.MainGrid.MainView.RowHeight = 50;

            gridEx2.MainGrid.SetGridFont(gridEx2.MainGrid.MainView, new Font("맑은 고딕", 15));
            gridEx2.MainGrid.MainView.ColumnPanelRowHeight = 50;
            gridEx2.MainGrid.MainView.RowHeight = 50;

            gridEx1.MainGrid.AddColumn("RowNumber", "번호", HorzAlignment.Center, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("Status", "진행상황", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("ProcessCode", "공정", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("CustomerName", "거래처", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("ItemName", "품목", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("WorkOrderQty", "지시수량", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("ResultQty", "생산수량", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("BadQty", "불량수량", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("RemainQty", "남은수량", HorzAlignment.Center, true);

            gridEx2.MainGrid.AddColumn("RowNumber", "번호", HorzAlignment.Center, FormatType.Numeric, "n0");
            gridEx2.MainGrid.AddColumn("Status", "진행상황", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("ProcessCode", "공정", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("MachineCode", "설비", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("CustomerName", "거래처", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("ItemName", "품목", HorzAlignment.Center, true);
            gridEx2.MainGrid.AddColumn("WorkOrderQty", "지시수량", HorzAlignment.Center, true);

            gridEx1.MainGrid.Columns["ResultQty"].AppearanceCell.ForeColor = Color.Yellow;
            gridEx1.MainGrid.Columns["RemainQty"].AppearanceCell.ForeColor = Color.Red;

            gridEx1.MainGrid.MainView.OptionsView.ColumnAutoWidth = true;
            gridEx2.MainGrid.MainView.OptionsView.ColumnAutoWidth = true;
        }

        protected override void InitRepository()
        {
            IService<VI_MEA1000_NOT_FILE_LIST> ModelService = (IService<VI_MEA1000_NOT_FILE_LIST>)ProductionFactory.GetDomainService("VI_MEA1000_NOT_FILE_LIST");
            var StatusList = MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList();
            var ProcessList = DbRequesHandler.GetCommCode(MasterCodeSTR.Process);
            var MachineList = ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList();
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", ProcessList, "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", MachineList, "MachineCode", "MachineName");
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("WorkOrderQty", DefaultBoolean.Default, "n0", true, false);
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("ResultQty", DefaultBoolean.Default, "n0", true, false);
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("BadQty", DefaultBoolean.Default, "n0", true, false);
            gridEx1.MainGrid.SetRepositoryItemSpinEdit("RemainQty", DefaultBoolean.Default, "n0", true, false);
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Status", StatusList);

            gridEx2.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", ProcessList, "Mcode", "Codename");
            gridEx2.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", MachineList, "MachineCode", "MachineName");
            gridEx2.MainGrid.SetRepositoryItemSpinEdit("WorkOrderQty", DefaultBoolean.Default, "n0", true, false);
            gridEx2.MainGrid.SetRepositoryItemSearchLookUpEdit("Status", StatusList);
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var result = context.Database.SqlQuery<TempModel>("USP_GET_XFR5000").ToList();
                bindingSource1.DataSource = result.Where(p=>p.Division == 1).OrderBy(p => p.RowNumber).ToList();
                bindingSource2.DataSource = result.Where(p => p.Division == 2).OrderBy(p => p.RowNumber).ToList();
            }
            gridEx1.DataSource = bindingSource1;
            gridEx2.DataSource = bindingSource2;

            gridEx1.BestFitColumns();
            gridEx2.BestFitColumns();
        }

        class TempModel
        {
            public Int64 RowNumber { get; set; }
            public string Status { get; set; }
            public string ProcessCode { get; set; }
            public string MachineCode { get; set; }
            public string CustomerName { get; set; }
            public string ItemName { get; set; }
            public int? WorkOrderQty { get; set; }
            public int? ResultQty { get; set; }
            public int? BadQty { get; set; }
            public int? RemainQty { get; set; }
            public int Division { get; set; }
        }
    }
}