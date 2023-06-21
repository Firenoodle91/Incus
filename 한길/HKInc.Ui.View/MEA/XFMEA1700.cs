using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 인두관리화면
    /// </summary>
    public partial class XFMEA1700 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_INDOO001> ModelService = (IService<TN_INDOO001>)ProductionFactory.GetDomainService("TN_INDOO001");
       
        public XFMEA1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;           
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("IndooMno", "관리코드");
            MasterGridExControl.MainGrid.AddColumn("IndooNo", "인두코드");
            MasterGridExControl.MainGrid.AddColumn("IndooName", "인두명");
            MasterGridExControl.MainGrid.AddColumn("IndooModel", "모델번호");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조회사");
            MasterGridExControl.MainGrid.AddColumn("SumtipCnt", "누적팁");
            MasterGridExControl.MainGrid.AddColumn("RealtipCnt", "현재팁");
            MasterGridExControl.MainGrid.AddColumn("MangtipCnt", "교채기준팁");
            MasterGridExControl.MainGrid.AddColumn("WrCnt", "경고기준");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "IndooNo", "IndooName", "IndooModel", "Maker", "MangtipCnt", "WrCnt");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "일자");
            DetailGridExControl.MainGrid.AddColumn("IndooMno", false);            
            DetailGridExControl.MainGrid.AddColumn("Tipcnt", "팁횟수");
            DetailGridExControl.MainGrid.AddColumn("Tipchange", "교체여부");
            DetailGridExControl.MainGrid.AddColumn("InUser", "작업자");
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WorkDate", "Tipcnt", "Memo", "InUser", "Tipchange");
        }

        protected override void InitRepository()
        {
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].Width = 150;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InUser", ModelService.GetChildList<UserView>(p => p.Active=="Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Tipchange", "N");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("IndooNo");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string lMachinecode = tx_MachineCode.Text;

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.IndooName.Contains(lMachinecode) || p.IndooNo.Contains(lMachinecode)))).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_INDOO001 obj = MasterGridBindingSource.Current as TN_INDOO001;
            if(obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = obj.TN_INDOO002List.OrderBy(o=>o.WorkDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            if (!UserRight.HasEdit) return;
            TN_INDOO001 obj1 = MasterGridBindingSource.Current as TN_INDOO001;
            if (obj1 != null)
            {
                TN_INDOO002 obj = new TN_INDOO002();
              
                obj.WorkDate = DateTime.Today;
                obj.IndooMno = obj1.IndooMno;
                obj.Tipchange = "N";

                DetailGridBindingSource.Add(obj);
                obj1.TN_INDOO002List.Add(obj);
                DetailGridBindingSource.MoveLast();
              
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_INDOO001 obj1 = MasterGridBindingSource.Current as TN_INDOO001;
            TN_INDOO002 obj = DetailGridBindingSource.Current as TN_INDOO002;

            if (obj != null)
            {
                obj1.TN_INDOO002List.Remove(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void AddRowClicked()
        {
            TN_INDOO001 obj = new TN_INDOO001();
            obj.IndooMno = DbRequesHandler.GetRequestNumberNew("INDOO");
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }

        protected override void DeleteRow()
        {
            TN_INDOO001 obj1 = MasterGridBindingSource.Current as TN_INDOO001;
            if (obj1.TN_INDOO002List.Count >= 1) { MessageBox.Show("상세내역이 있어 삭제 불가능합니다."); }
            else
            {
                MasterGridBindingSource.Remove(obj1);
                ModelService.Delete(obj1);
            }
        }

        protected override void DataSave()
        {
            var obj1 = MasterGridBindingSource.List as List<TN_INDOO001>;

            foreach(var v in obj1)
            {
                var detail = v.TN_INDOO002List;
                int sumcnt = 0;
                foreach (var c in v.TN_INDOO002List)
                {
                    sumcnt += c.Tipcnt.GetIntNullToZero();
                }
                var d = v.TN_INDOO002List.OrderBy(p => p.WorkDate).LastOrDefault();
                if(d != null)
                {
                    if(d.Tipchange == "Y")
                        v.RealtipCnt = 0;
                }
                else
                {
                    v.RealtipCnt = sumcnt;
                }
                v.SumtipCnt = sumcnt;

            }

            //if()
            //GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
            
            //for (int i = 0; i < gv.RowCount; i++)
            //{
            //    sumcnt += gv.GetRowCellValue(i, "Tipcnt").GetIntNullToZero();                
            //}
            //if (gv.GetRowCellValue(gv.RowCount - 1, "Tipchange").GetNullToEmpty() == "Y")
            //{
            //    obj1.SumtipCnt = sumcnt;
            //    obj1.RealtipCnt = 0;
            //}
            //else
            //{
            //    obj1.SumtipCnt = sumcnt;
            //    obj1.RealtipCnt = sumcnt;
            //}

            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            ModelService.Save();
            DataLoad();
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                object realcnt = View.GetRowCellValue(e.RowHandle, View.Columns["RealtipCnt"]);
                object wcnt = View.GetRowCellValue(e.RowHandle, View.Columns["WrCnt"]);

                if (realcnt.GetDoubleNullToZero() != 0)
                {
                    if (realcnt.GetDoubleNullToZero() >= (wcnt.GetDoubleNullToZero() == 0 ? 0 : (wcnt.GetDoubleNullToZero() / 100 * 80)))
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    if (realcnt.GetDoubleNullToZero() >= (wcnt.GetDoubleNullToZero() == 0 ? 0 : (wcnt.GetDoubleNullToZero() / 100 * 90)))
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }
    }
}