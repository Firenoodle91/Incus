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
    public partial class XFMEA1700 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_INDOO001> ModelService = (IService<TN_INDOO001>)ProductionFactory.GetDomainService("TN_INDOO001");
       
        public XFMEA1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
           
        }

  

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {

             
                object  realcnt= View.GetRowCellValue(e.RowHandle, View.Columns["RealtipCnt"]);
                object wcnt= View.GetRowCellValue(e.RowHandle, View.Columns["WrCnt"]);
                
            

                if (realcnt.GetDoubleNullToZero() != 0)
                {
                    if ( realcnt.GetDoubleNullToZero()>= (wcnt.GetDoubleNullToZero()==0?0: (wcnt.GetDoubleNullToZero()/100*80)))
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

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //GridView gv = sender as GridView;
          
            //TN_INDOO001 tn = MasterGridBindingSource.Current as TN_INDOO001;
            //TN_INDOO002 tn1 = DetailGridBindingSource.Current as TN_INDOO002;
            
            //if(e.Column.Name== "Tipcnt")
            //{
            //    tn.RealtipCnt = tn.RealtipCnt.GetDecimalNullToZero() + tn1.Tipcnt.GetDecimalNullToZero();
            //    tn.SumtipCnt = tn.SumtipCnt.GetDecimalNullToZero() + tn1.Tipcnt.GetDecimalNullToZero();
            //}
            //if (e.Column.Name == "Tipchange")
            //{

            //    if (tn1.Tipchange == "Y")
            //    {
                  
            //            tn.RealtipCnt = tn1.Tipcnt.GetDecimalNullToZero();
              
            //    }
            //    else {

            //        MessageBox.Show("교체를 취소 할 수 없습니다.");
                 
            //        tn1.Tipchange = "Y";
            //       // return;
            //    }
            //}
          
         
            //MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("IndooNo", "인두코드");
            MasterGridExControl.MainGrid.AddColumn("IndooMno", "인두관리코드");
            MasterGridExControl.MainGrid.AddColumn("IndooName", "인두명");
            MasterGridExControl.MainGrid.AddColumn("IndooModel", "모델번호");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조회사");
            MasterGridExControl.MainGrid.AddColumn("SumtipCnt", "누적팁");
            MasterGridExControl.MainGrid.AddColumn("RealtipCnt", "현재팁");
            MasterGridExControl.MainGrid.AddColumn("MangtipCnt", "교채기준팁");
            MasterGridExControl.MainGrid.AddColumn("WrCnt", "경고기준");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "IndooMno", "IndooName", "IndooModel", "Maker", "MangtipCnt", "WrCnt");



            DetailGridExControl.MainGrid.AddColumn("IndooNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "일자");
            DetailGridExControl.MainGrid.AddColumn("IndooMno", false);            
            DetailGridExControl.MainGrid.AddColumn("Tipcnt", "팁횟수");
            DetailGridExControl.MainGrid.AddColumn("Tipchange", "교체여부");
            DetailGridExControl.MainGrid.AddColumn("InUser", "작업자");
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");


            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WorkDate", "Tipcnt", "Memo", "InUser", "Tipchange");





        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {
  
           
           
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].Width = 150;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InUser", ModelService.GetChildList<UserView>(p => p.Active=="Y"), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Tipchange", "N");
        }
        protected override void DataLoad()
        {



         
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("IndooNo");

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
            DetailGridBindingSource.DataSource = obj.TN_INDOO002List.OrderBy(o=>o.WorkDate).ToList();//.GetList(p => (p.MachineCode == obj.MachineCode)).OrderBy(p => p.MachineSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            //    base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_INDOO001 obj1 = MasterGridBindingSource.Current as TN_INDOO001;
            if (obj1 != null)
            {
                TN_INDOO002 obj = new TN_INDOO002();
              
                obj.WorkDate = DateTime.Today;
                obj.IndooNo = obj1.IndooNo;
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
                //if (obj.Tipcnt.GetIntNullToZero() != 0||obj.InUser.GetNullToEmpty()!="")
                //{
                //    MessageBox.Show("삭제할수 없습니다.");

                //}
                //else
                //{
                    obj1.TN_INDOO002List.Remove(obj);
                    DetailGridBindingSource.RemoveCurrent();
                //}
            }
        }
        protected override void AddRowClicked()
        {
            TN_INDOO001 obj = new TN_INDOO001();
            obj.IndooNo = DbRequesHandler.GetRequestNumberNew("INDOO");
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
            TN_INDOO001 obj1 = MasterGridBindingSource.Current as TN_INDOO001;

            GridView gv = DetailGridExControl.MainGrid.MainView as GridView;
            int sumcnt = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                sumcnt += gv.GetRowCellValue(i, "Tipcnt").GetIntNullToZero();                
            }
            if (gv.GetRowCellValue(gv.RowCount - 1, "Tipchange").GetNullToEmpty() == "Y")
            {
                obj1.SumtipCnt = sumcnt;
                obj1.RealtipCnt = 0;
            }
            else
            {
                obj1.SumtipCnt = sumcnt;
                obj1.RealtipCnt = sumcnt;
            }

            base.DataSave();
            ModelService.Save();
            DataLoad();
        }
    }
}