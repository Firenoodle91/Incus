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
    /// <summary>
    /// 알람
    /// 2022-08-18 김진우 추가
    /// </summary>
    public partial class Alarm : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_ALAM002> ModelService = (IService<TN_ALAM002>)ProductionFactory.GetDomainService("TN_ALAM002");

        public Alarm()
        {
            InitializeComponent();
            SetToolbarVisible(false);
            GridExControl = gridEx1;

            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;

            InitGrid();
            DataLoad();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TN_ALAM002 obj = GridBindingSource.Current as TN_ALAM002;
            if (obj == null) return;

            SpHh.EditValue = obj.Hh;
            SpMm.EditValue = obj.Mm;
            tx_memo.EditValue = obj.Memo.GetNullToEmpty();
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("No",false);
            GridExControl.MainGrid.AddColumn("Hh", "알람시간", true,HorzAlignment.Center);
            GridExControl.MainGrid.AddColumn("Mm", "알람분", true, HorzAlignment.Center);
            GridExControl.MainGrid.AddColumn("Memo", "비고");
           
        }

        protected override void DataLoad()
        {
            GridBindingSource.DataSource = ModelService.GetList(p => true).OrderBy(o => o.Hh).ToList();
            GridExControl.DataSource = GridBindingSource;
            memoEdit1.Text=   DbRequestHandler.GetCellValue("select memo from tn_memo", 0).GetNullToEmpty();
        }

        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(SpHh.Text) >= 24 || Convert.ToInt32(SpMm.Text) >= 60 || Convert.ToInt32(SpHh.Text) < 0 || Convert.ToInt32(SpMm.Text) < 0)
            {
                MessageBoxHandler.Show("시간을 다시 설정해주세요.");
                return;
            }

            TN_ALAM002 obj = new TN_ALAM002()
            {
                No = DbRequestHandler.GetRequestNumberNew("ALam"),
                Hh = SpHh.EditValue.GetNullToEmpty(),
                Mm = SpMm.EditValue.GetNullToEmpty(),
                Memo = tx_memo.EditValue.GetNullToEmpty()
            };

            GridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, EventArgs e)
        {
            TN_ALAM002 obj = GridBindingSource.Current as TN_ALAM002;
            if (obj == null) return;
            GridBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
        }

        /// <summary>
        /// 저장 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            ModelService.Save();
            int i = DbRequestHandler.SetDataQury("update tn_memo set memo ='" + memoEdit1.EditValue + "' where idx=1");
            this.Close();
        }

        /// <summary>
        /// 종료 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장후 종료하시겠습니까?", "알림", MessageBoxButtons.OKCancel) == DialogResult.OK)
                ModelService.Save();

            this.Close();
        }

    }
}
