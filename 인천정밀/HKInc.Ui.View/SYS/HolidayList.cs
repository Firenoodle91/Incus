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
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using DevExpress.XtraEditors.Mask;
using DevExpress.Utils;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 휴일관리
    /// </summary>
    public partial class HolidayList : Service.Base.ListFormTemplate
    {
        IService<Holiday> ModelService = (IService<Holiday>)ServiceFactory.GetDomainService("Holiday");

        public HolidayList()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dt_YearMonth.EditValueChanged += Dt_YearMonth_EditValueChanged;
            GridExControl.MainGrid.MainView.RowStyle += MainView_RowStyle;

            dt_YearMonth.SetFormat(DateFormat.Month);
            dt_YearMonth.DateTime = DateTime.Today;
            dt_YearMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        protected override void InitCombo()
        {
            
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("Date", LabelConvert.GetLabelText("Date"), HorzAlignment.Center, FormatType.DateTime, "d일");
            GridExControl.MainGrid.AddColumn("DayOfWeek", LabelConvert.GetLabelText("DayOfWeek"), HorzAlignment.Center, FormatType.DateTime, "dddd");
            GridExControl.MainGrid.AddColumn("HolidayFlag", LabelConvert.GetLabelText("HolidayFlag"));
            GridExControl.MainGrid.AddColumn("OvertimeFlag", LabelConvert.GetLabelText("OvertimeFlag"));
            GridExControl.MainGrid.AddColumn("WorkTime", LabelConvert.GetLabelText("WorkTime"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "HolidayFlag", "OvertimeFlag", "Memo", "WorkTime");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("HolidayFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("OvertimeFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("WorkTime");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("Date");

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            GridBindingSource.DataSource = ModelService.GetList(p => p.Date.Year == dt_YearMonth.DateTime.Year && p.Date.Month == dt_YearMonth.DateTime.Month).OrderBy(p => p.Date).ToList();

            GridExControl.DataSource = GridBindingSource;

            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        protected override void GridRowDoubleClicked(){ }

        private void Dt_YearMonth_EditValueChanged(object sender, EventArgs e)
        {
            if(IsFirstLoaded)
                ActRefresh();
        }

        private void MainView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                var holidayFlag = view.GetRowCellValue(e.RowHandle, "HolidayFlag");
                var overtimeFlag = view.GetRowCellValue(e.RowHandle, "OvertimeFlag");
                if (holidayFlag.GetNullToEmpty() == "Y")
                    e.Appearance.ForeColor = Color.Red;

            }
        }
    }
}