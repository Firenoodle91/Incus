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
        IService<TN_MPS1001> ModelServiceMps1001 = (IService<TN_MPS1001>)ProductionFactory.GetDomainService("TN_MPS1001");
        public XFMPS1001()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }
        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
         //   luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");

            //  MasterGridExControl.MainGrid.AddColumn("fullName", "품목명");
            //GridExControl.MainGrid.AddColumn("ItemGbn");
          //  MasterGridExControl.MainGrid.AddColumn("Temp5", "팀");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("Lctype", "기종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("ItemCode",false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("MachineCode", "설비명");
            DetailGridExControl.MainGrid.AddColumn("Rev", "리비전");
            DetailGridExControl.MainGrid.AddColumn("FileName","사출조건표");
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부");
           
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "MachineCode", "FileName", "UseYn");
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(gridEx2, "FileName");
            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UseYn", DbRequestHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y"), "MachineCode", "MachineName");

        }

        private void lupItemtype_EditValueChanged(object sender, EventArgs e)
        {
            //if (lupItemtype.EditValue.GetNullToEmpty() == "") return;
            //string itemtype = lupItemtype.EditValue.GetNullToEmpty();
            //lupItemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetList(p=>(string.IsNullOrEmpty(itemtype)?true: p.TopCategory==itemtype)&&p.UseYn=="Y").OrderBy(o=>o.ItemNm1).ToList());
        }
        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            string itemcode = tx_item.EditValue.GetNullToEmpty();
           // string tem = luptem.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemcode)?true:(p.ItemCode.Contains(itemcode)||p.ItemNm.Contains(itemcode)||p.ItemNm1.Contains(itemcode))) && (string.IsNullOrEmpty(cta) ? p.TopCategory!="P03" : p.TopCategory == cta) )
                                                         .OrderBy(p => p.ItemNm1)
                                                       .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            MasterGridExControl.BestFitColumns();
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null) return;
            DetailGridExControl.MainGrid.Clear();
            DetailGridBindingSource.DataSource = ModelServiceMps1001.GetList(p => p.ItemCode == obj.ItemCode).OrderByDescending(o => o.Rev).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {
            TN_MPS1001 obj = DetailGridBindingSource.Current as TN_MPS1001;
            if (obj == null) return;
            DetailGridBindingSource.Remove(obj);
            ModelServiceMps1001.Delete(obj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DetailAddRowClicked()
        {
            TN_STD1100 item = MasterGridBindingSource.Current as TN_STD1100;
            if (item == null) return;
            TN_MPS1001 dobj = DetailGridBindingSource.Current as TN_MPS1001;
            if (dobj != null)
            {
                if (DialogResult.OK == MessageBox.Show("저장후 추가하시겠습니까?", "알림", MessageBoxButtons.OKCancel))
                { lSave(); }
                else { return; }
            }
            TN_MPS1001 newobj = new TN_MPS1001()
            {
                ItemCode = item.ItemCode,
                UseYn = "Y",
                Rev = Convert.ToInt32(DbRequestHandler.GetCellValue("SELECT  isnull(max([REV]),0)+1 FROM [TN_mps1001t] where ITEM_CODE='"+ item.ItemCode + "'", 0))
                
            };
            DetailGridBindingSource.Add(newobj);
            ModelServiceMps1001.Insert(newobj);
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

            ModelServiceMps1001.Save();
        }
    }
}
