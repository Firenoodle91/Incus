using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;

using DevExpress.XtraGrid.Views.Grid;

using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Service.Forms
{
    public partial class CodeHelpPopupForm : HKInc.Service.Base.BaseForm
    {
        private DataTable returnDataTable;
        private DataTable gridDataSource;
        private string[] gridDisplayField;    

        public DataTable ReturnDataTable { get { return returnDataTable; } }
        public string KeyColumnName { get; set; }
        public bool SearchEnabled
        {
            get { return textSearchText.Enabled; }
            set { textSearchText.Enabled = value; }
        }

        public CodeHelpPopupForm(DataTable dataSource, string[] displayField)
        {
            InitializeComponent();

            this.gridDataSource = dataSource;
            this.gridDisplayField = displayField;

            this.DialogResult = DialogResult.OK;
        }
        
        protected override void InitControls()
        {
            SetToolbarButtonVisible(ToolbarButton.Refresh, false);
            SetToolbarButtonVisible(ToolbarButton.Print, false);
            SetToolbarButtonVisible(ToolbarButton.Save, false);
            SetToolbarButtonVisible(ToolbarButton.Export, false);

            gridEx1.MainView.RowClick += MainView_RowClick;
            textSearchText.KeyDown += TextSearchText_KeyDown;
        }

        protected override void InitGrid()
        {
            gridEx1.Init();

            foreach (var columnName in gridDisplayField)
                gridEx1.AddColumn(columnName);

            gridEx1.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            gridEx1.DataSource = gridDataSource;
            gridEx1.BestFitColumns();
        }

        protected override void ActClose()
        {
            if (returnDataTable == null)
                returnDataTable = new DataTable();
        }


        private void TextSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // KeyColumnName을 이용하여 Filtering한다.

            }
            else if (e.KeyCode == Keys.Down)
            {
                if (gridEx1.RecordCount > 0)
                {
                    gridEx1.FocusedRowHandle = 0;
                    gridEx1.Focus();
                }
            }
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            if (e.Clicks == 2)
            {
                //선택Line을 DataTable로 만든다.
                DataTable tempTable = this.gridDataSource.Clone();         
                tempTable.ImportRow((gridEx1.MainView.GetFocusedRow() as DataRowView).Row);

                returnDataTable = tempTable;
                ActClose();
            }
        }

    }
}