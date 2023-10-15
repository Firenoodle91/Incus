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
    public partial class XFMPS1001 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        //IService<TN_MPS1001> ModelServiceMps1001 = (IService<TN_MPS1001>)ProductionFactory.GetDomainService("TN_MPS1001");
        public XFMPS1001()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }
        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            //MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            //MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            //MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            //MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "원재료)");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "색상)");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "재료규격");
            MasterGridExControl.MainGrid.AddColumn("Temp2", "실리콘농도");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "제품규격");
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("MachineCode", "설비명");
            DetailGridExControl.MainGrid.AddColumn("Rev", "리비전", HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.AddColumn("FileName","사출조건표");
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부", HorzAlignment.Center, true);
           
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "MachineCode", "FileName", "UseYn");
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");

            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(DetailGridExControl, "FileName");            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UseYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");

        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            string itemcode = tx_item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemcode) ? true: (p.ItemNm.Contains(itemcode)|| p.ItemNm1.Contains(itemcode))) 
                                                                        && (string.IsNullOrEmpty(cta) ? p.TopCategory!="P03" : p.TopCategory == cta)
                                                                     )
                                                                     .OrderBy(p => p.ItemNm1)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            //DetailGridBindingSource.DataSource = ModelServiceMps1001.GetList(p => p.ItemCode == obj.ItemCode).OrderByDescending(o => o.Rev).ToList();
            DetailGridBindingSource.DataSource = obj.TN_MPS1001List.OrderByDescending(o => o.Rev).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {
            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            TN_MPS1001 obj = DetailGridBindingSource.Current as TN_MPS1001;
            if (obj == null || MasterObj == null) return;
            DetailGridBindingSource.Remove(obj);
            //ModelServiceMps1001.Delete(obj);
            MasterObj.TN_MPS1001List.Remove(obj);
        }
        protected override void DetailAddRowClicked()
        {
            TN_STD1100 item = MasterGridBindingSource.Current as TN_STD1100;
            if (item == null) return;
            //TN_MPS1001 dobj = DetailGridBindingSource.Current as TN_MPS1001;
            //if (dobj != null)
            //{
            //    if (DialogResult.OK == MessageBox.Show("저장후 추가하시겠습니까?", "알림", MessageBoxButtons.OKCancel))
            //    { lSave(); }
            //    else { return; }
            //}
            TN_MPS1001 newobj = new TN_MPS1001()
            {
                ItemCode = item.ItemCode,
                UseYn = "Y",
                Rev = Convert.ToInt32(DbRequesHandler.GetCellValue("SELECT  isnull(max([REV]),0)+1 FROM [TN_mps1001t] where ITEM_CODE='"+ item.ItemCode + "'", 0))
                
            };
            DetailGridBindingSource.Add(newobj);
            //ModelServiceMps1001.Insert(newobj);
            item.TN_MPS1001List.Add(newobj);
            DetailGridExControl.MainGrid.BestFitColumns();

        }
        protected override void DataSave()
        {
            lSave();
         
            DataLoad();
        }
        private void lSave()
        {
            for (int rowHandle = 0; rowHandle < gridEx2.MainGrid.MainView.RowCount; rowHandle++)
            {
                string _machine = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "MachineCode").GetNullToEmpty());
                string _process = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessCode").GetNullToEmpty());
                string _file = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "FileName").GetNullToEmpty());


                if (_machine == null || _machine == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("사출조건표" + Convert.ToInt32(rowHandle + 1) + "행의 설비는 필수입력 사항입니다.");
                    return;
                }
                if (_process == null || _process == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("사출조건표" + Convert.ToInt32(rowHandle + 1) + "행의 공정는 필수입력 사항입니다.");
                    return;
                }
                if (_file == null || _file == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("사출조건표" + Convert.ToInt32(rowHandle + 1) + "행의 사출조건파일은 필수입력 사항입니다.");
                    return;
                }
                string[] _ftp = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "FileName").GetNullToEmpty()).Split('\\');
                if (_ftp.Length>1)
                {
                    FileHandler.UploadFile1(_file, GlobalVariable.FTP_SERVER + "INJECTION/");
                    gridEx2.MainGrid.MainView.SetRowCellValue(rowHandle, "FileName", "INJECTION/"+_ftp[_ftp.Length - 1]);
                      
                }

            }

            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            //ModelServiceMps1001.Save();
            ModelService.Save();
        }
    }
}
