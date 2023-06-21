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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_MPS1000> ModelServiceMps1000 = (IService<TN_MPS1000>)ProductionFactory.GetDomainService("TN_MPS1000");
        public XFMPS1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }
        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
          //  luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            //MasterGridExControl.MainGrid.AddColumn("fullName", "품목명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            //GridExControl.MainGrid.AddColumn("ItemGbn");
          //  MasterGridExControl.MainGrid.AddColumn("Temp5", "팀");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("Lctype", "기종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "선경");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "외경");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "자유고");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "권수");
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", "공정순서");
            DetailGridExControl.MainGrid.AddColumn("WorkStantadnm", "작업표준서");
            DetailGridExControl.MainGrid.AddColumn("FileData", false);
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부");
            DetailGridExControl.MainGrid.AddColumn("OutProc", "외주여부");
            DetailGridExControl.MainGrid.AddColumn("STD", "표준작업소요일");
            DetailGridExControl.MainGrid.AddColumn("RowId",false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessSeq", "OutProc", "WorkStantadnm", "UseYn", "STD");
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            //    MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            //    DetailGridExControl.MainGrid.MainView.Columns["WorkStantadnm"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(gridEx2, "FileData", "WorkStantadnm");
            //DetailGridExControl.MainGrid.MainView.Columns["WorkStantadnm"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx2, "WorkStantadnm");
            DetailGridExControl.MainGrid.MainView.Columns["WorkStantadnm"].ColumnEdit = new Service.Controls.FileGridButtonEdit(gridEx2.MainGrid, "FileData", "WorkStantadnm");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UseYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProc", "N");// DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("STD", DbRequesHandler.GetCommCode(MasterCodeSTR.STD), "Mcode", "Codename");
        }

        private void lupItemtype_EditValueChanged(object sender, EventArgs e)
        {
            //if (lupItemtype.EditValue.GetNullToEmpty() == "") return;
            //string itemtype = lupItemtype.EditValue.GetNullToEmpty();
            //lupItemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetList(p=>(string.IsNullOrEmpty(itemtype)?true: p.TopCategory==itemtype)&&p.UseYn=="Y").OrderBy(o=>o.ItemNm1).ToList());
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion
            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            string itemcode = tx_item.EditValue.GetNullToEmpty();
         //   string tem = luptem.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemcode) ? true : (p.ItemCode.Contains(itemcode) || p.ItemNm.Contains(itemcode) || p.ItemNm1.Contains(itemcode))) && (string.IsNullOrEmpty(cta) ? p.TopCategory != "P03" : p.TopCategory == cta))
                                                           .OrderBy(p => p.ItemNm1)
                                                         .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
        protected override void MasterFocusedRowChanged()
        {
            imgsave();
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null) return;
            DetailGridExControl.MainGrid.Clear();
            DetailGridBindingSource.DataSource = ModelServiceMps1000.GetList(p => p.ItemCode == obj.ItemCode).OrderBy(o => o.ProcessSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;

            SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {
            TN_MPS1000 obj = DetailGridBindingSource.Current as TN_MPS1000;
            if (obj == null) return;
            DetailGridBindingSource.Remove(obj);
            ModelServiceMps1000.Delete(obj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DetailAddRowClicked()
        {
            TN_STD1100 item = MasterGridBindingSource.Current as TN_STD1100;
            if (item == null) return;
            TN_MPS1000 newobj = new TN_MPS1000()
            {
                ItemCode = item.ItemCode,
                UseYn = "Y",
                STD = "1"
                
            };
            DetailGridBindingSource.Add(newobj);
            ModelServiceMps1000.Insert(newobj);
            DetailGridExControl.MainGrid.BestFitColumns();

        }
        private void imgsave()
        {
            for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
            {
                string _std = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "STD").GetNullToEmpty());


                if (_std == null || _std == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("표준작업소요일" + Convert.ToInt32(rowHandle + 1) + "행의 점검일은 필수입력 사항입니다.");
                    return;
                }

                string _WorkStantadnm = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "WorkStantadnm").GetNullToEmpty());
                if (_WorkStantadnm != "" || _WorkStantadnm != null)
                {
                    //string[] ChkQcfile1cha = _WorkStantadnm.Split('\\');
                    //if (ChkQcfile1cha.Length > 2)
                    //{
                    //    FileHandler.UploadFile1(_WorkStantadnm, GlobalVariable.FTP_SERVER + "JOBSTD/");
                    //    gridEx2.MainGrid.MainView.SetRowCellValue(rowHandle, "WorkStantadnm", "JOBSTD/" + ChkQcfile1cha[ChkQcfile1cha.Length - 1]);
                    //}
                }

            }

            ModelServiceMps1000.Save();
        }
        protected override void DataSave()
        {
            for(int rowHandle=0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++ )
            {
                string _std = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "STD").GetNullToEmpty());
             

                if (_std == null || _std == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("표준작업소요일" + Convert.ToInt32(rowHandle + 1) + "행의 점검일은 필수입력 사항입니다.");
                    return;
                }

                string _WorkStantadnm = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "WorkStantadnm").GetNullToEmpty());
                if (_WorkStantadnm != "" || _WorkStantadnm!=null)
                {
                    //string[] ChkQcfile1cha = _WorkStantadnm.Split('\\');
                    //if (ChkQcfile1cha.Length > 2)
                    //{
                    //    FileHandler.UploadFile1(_WorkStantadnm, GlobalVariable.FTP_SERVER + "JOBSTD/");
                    //    gridEx2.MainGrid.MainView.SetRowCellValue(rowHandle, "WorkStantadnm", "JOBSTD/" + ChkQcfile1cha[ChkQcfile1cha.Length - 1]);
                    //}
                }

            }

            ModelServiceMps1000.Save();
            DataLoad();
        }
       
    }
    
}
