using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Forms;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;

using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Main
{
    public partial class FAlam1 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_ALAM002> ModelService = (IService<TN_ALAM002>)ProductionFactory.GetDomainService("TN_ALAM002");
        public FAlam1()
        {
            InitializeComponent();
            SetToolbarVisible(false);
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            InitCombo();

            InitGrid();
            InitRepository();
            DataLoad();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TN_ALAM002 obj = GridBindingSource.Current as TN_ALAM002;
            if (obj == null) return;
            lupHh.EditValue = obj.Hh;
            lupMm.EditValue = obj.Mm;
            tx_memo.EditValue = obj.Memo.GetNullToEmpty();
        }

        protected override void InitCombo()
        {
            lupHh.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.AlamHH));
            lupMm.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.AlamMM));
        }
        protected override void InitGrid()
        {
            //GridExControl.SetToolbarButtonVisible(false);
            //GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "알람시간추가");
            //GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "알람시간삭제");
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("No",false);
            GridExControl.MainGrid.AddColumn("Hh", "알람시간", true,HorzAlignment.Center);
            GridExControl.MainGrid.AddColumn("Mm", "알람분", true, HorzAlignment.Center);
            GridExControl.MainGrid.AddColumn("Memo", "비고");
           
        }
        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("Hh", DbRequestHandler.GetCommCode(MasterCodeSTR.AlamHH), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("Mm", DbRequestHandler.GetCommCode(MasterCodeSTR.AlamMM), "Mcode", "Codename");
      //  GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }
        protected override void AddRowClicked()
        {
            TN_ALAM002 obj = new TN_ALAM002();
            GridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
        protected override void DeleteRow()
        {
            GridBindingSource.RemoveCurrent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ModelService.Save();
            int i = DbRequestHandler.SetDataQury("update tn_memo set memo ='" + memoEdit1.EditValue + "' where idx=1");
            this.Close();
        }
        protected override void DataLoad()
        {
            GridBindingSource.DataSource = ModelService.GetList(p => 1 == 1).OrderBy(o => o.Hh).ToList();
            GridExControl.DataSource = GridBindingSource;
         memoEdit1.Text=   DbRequestHandler.GetCellValue("select memo from tn_memo", 0).GetNullToEmpty();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TN_ALAM002 obj = new TN_ALAM002() { No = DbRequestHandler.GetRequestNumberNew("ALam"), Hh = lupHh.EditValue.GetNullToEmpty(), Mm = lupMm.EditValue.GetNullToEmpty(), Memo = tx_memo.EditValue.GetNullToEmpty() };
            GridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            TN_ALAM002 obj = GridBindingSource.Current as TN_ALAM002;
            if (obj == null) return;
            GridBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
        }
        protected override void GridRowDoubleClicked()
        {
            TN_ALAM002 obj = GridBindingSource.Current as TN_ALAM002;
            if (obj == null) return;
            lupHh.EditValue = obj.Hh;
            lupMm.EditValue = obj.Mm;
            tx_memo.EditValue = obj.Memo.GetNullToEmpty();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장후 종료하시겠습니까?", "알림",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ModelService.Save();

            }
            this.Close();
        }
    }
}
