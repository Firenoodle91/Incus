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
using HKInc.Service.Handler;
using System.IO;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.MEA
{
    public partial class XFMEA1503 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001");
       
        public XFMEA1503()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_MOLD001 obj = MasterGridBindingSource.Current as TN_MOLD001;
            TN_MOLD003 obj3 = DetailGridBindingSource.Current as TN_MOLD003;
            if (e.Column.Name == "StInposition1") { obj.StPosition1 = obj3.StInposition1; }
            if (e.Column.Name == "StInposition2") { obj.StPosition2 = obj3.StInposition2; }
            if (e.Column.Name == "StInposition3") { obj.StPosition3 = obj3.StInposition3; }
            MasterGridExControl.BestFitColumns();
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("MoldMcode", "관리번호");
            MasterGridExControl.MainGrid.AddColumn("MoldCode", "금형코드");
            MasterGridExControl.MainGrid.AddColumn("MoldName", "금형명");          
            MasterGridExControl.MainGrid.AddColumn("MoldMakecust", "제작처");
            MasterGridExControl.MainGrid.AddColumn("StPosition1", "위치1");
            MasterGridExControl.MainGrid.AddColumn("StPosition2", "위치2");
            MasterGridExControl.MainGrid.AddColumn("StPosition3", "위치3");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");
            MasterGridExControl.MainGrid.AddColumn("MoldClass", "등급");
            MasterGridExControl.MainGrid.AddColumn("Imgurl", "이미지");


            DetailGridExControl.MainGrid.AddColumn("MoldMcode",false);
            DetailGridExControl.MainGrid.AddColumn("MoldCode",false);
            DetailGridExControl.MainGrid.AddColumn("Seq","순번");
            DetailGridExControl.MainGrid.AddColumn("InoutDt", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("StOutposition1", "출고위치1");
            DetailGridExControl.MainGrid.AddColumn("StOutposition2", "출고위치1");
            DetailGridExControl.MainGrid.AddColumn("StOutposition3", "출고위치1");
            DetailGridExControl.MainGrid.AddColumn("StInposition1", "입고위치1");
            DetailGridExControl.MainGrid.AddColumn("StInposition2", "입고위치2");
            DetailGridExControl.MainGrid.AddColumn("StInposition3", "입고위치3");
            DetailGridExControl.MainGrid.AddColumn("InoutId", "입출고자");






            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InoutDt", "StOutposition1", "StOutposition2"
               , "StOutposition3"
               , "StInposition1"
               , "StInposition2"
               , "StInposition3"
               , "InoutId"
                );





        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {
            
          
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MoldMakecust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");        
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.MainView.Columns["Imgurl"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx1, "Imgurl", "Imgurl");
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MoldClass", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDCLASS), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InoutDt");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutposition1", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutposition2",  DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutposition3", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 3), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInposition1",  DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInposition2",  DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInposition3", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 3), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InoutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
          
        }
        protected override void DataLoad()
        {



           
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MoldMcode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string lMachinecode = tx_MachineCode.Text;
          
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(lMachinecode) ? true : (p.MoldMcode.Contains(lMachinecode) || p.MoldCode.Contains(lMachinecode)))).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {

            TN_MOLD001 obj = MasterGridBindingSource.Current as TN_MOLD001;
            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_MOLD003>(p => p.MoldMcode == obj.MoldMcode).OrderBy(o=>o.InoutDt).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            //    base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_MOLD001 obj1 = MasterGridBindingSource.Current as TN_MOLD001;
            if (obj1 != null)
            {
                TN_MOLD003 obj = new TN_MOLD003();
                obj.MoldMcode = obj1.MoldMcode;
                obj.MoldCode = obj1.MoldCode;
                obj.MoldName = obj1.MoldName;
                string sql= "SELECT isnull(max(seq),0)+1 FROM [TN_MOLD003T] where MOLD_MCODE='" + obj1.MoldMcode + "'";
                obj.Seq= Convert.ToInt32(DbRequestHandler.GetCellValue(sql,0));
                obj.StOutposition1 = obj1.StPosition1;
                obj.StOutposition2 = obj1.StPosition2;
                obj.StOutposition3 = obj1.StPosition3;

                DetailGridBindingSource.Add(obj);
                ModelService.InsertChild<TN_MOLD003>(obj);
                DetailGridBindingSource.MoveLast();
              
            }
       
            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {

            TN_MOLD001 obj1 = MasterGridBindingSource.Current as TN_MOLD001;
            TN_MOLD003 obj = DetailGridBindingSource.Current as TN_MOLD003;

            if (obj != null)
            {
                obj1.StPosition1 = obj.StOutposition1;
                obj1.StPosition2 = obj.StOutposition2;
                obj1.StPosition3 = obj.StOutposition3;
                ModelService.RemoveChild<TN_MOLD003>(obj);
                DetailGridBindingSource.RemoveCurrent();
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DataSave()
        {
      

            base.DataSave();
            ModelService.Save();
            DataLoad();
        }
    }
}