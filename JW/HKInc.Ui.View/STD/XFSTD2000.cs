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
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.STD

{
    public partial class XFSTD2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_QCT1001> ModelServiceQCT1001 = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");
        public XFSTD2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
            {
                DetailGridExControl.MainGrid.Clear();
                ModelServiceQCT1001.ReLoad();
            }
            else
            {
                TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
                DetailGridExControl.MainGrid.Clear();
                ModelServiceQCT1001.ReLoad();
                DetailGridBindingSource.DataSource = ModelServiceQCT1001.GetList(p => p.ItemCode == obj.ItemCode).OrderByDescending(o=>o.Seq).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
            }
        }

        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));

        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
            MasterGridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
        
            //GridExControl.MainGrid.AddColumn("ItemGbn");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "선경");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "외경");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "자유고");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "권수");      
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("Qcname", "검사항목");
            DetailGridExControl.MainGrid.AddColumn("Qcnote", "메모");
            DetailGridExControl.MainGrid.AddColumn("Imagefile", "적용이미지");
            DetailGridExControl.MainGrid.AddColumn("AppendDt", "적용일");
            DetailGridExControl.MainGrid.AddColumn("AppendUser", "등록자");
            
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Qcname", "Qcnote", "Imagefile", "AppendDt", "AppendUser");

        }
        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("AppendDt");
            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("AppendUser", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Qcnote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Imagefile"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx2, "Imagefile");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Qcname", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
        }
        protected override void DataLoad()
        {

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta))
                                                         .OrderBy(p => p.ItemNm)
                                                       .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
        }
        protected override void DetailAddRowClicked()
        {
        //    if (DetailGridExControl.MainGrid.MainView.RowCount >= 1) return;
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (DetailGridBindingSource.Count >= 1)
            {
              
                    if (DialogResult.OK == MessageBox.Show("저장후 추가하시겠습니까?", "알림", MessageBoxButtons.OKCancel))
                    { lSave(); }
                    else { return; }
               
            }
            
            TN_QCT1001 new_obj = new TN_QCT1001() { ItemCode = obj.ItemCode};
            DetailGridBindingSource.Add(new_obj);
            ModelServiceQCT1001.Insert(new_obj);
            DetailGridBindingSource.EndEdit();

        }
        protected override void DeleteDetailRow()
        {
            TN_QCT1001 obj = DetailGridBindingSource.Current as TN_QCT1001;
            DetailGridBindingSource.Remove(obj);
            ModelServiceQCT1001.Delete(obj);
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
              
                string _file = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "Imagefile").GetNullToEmpty());


               
                string[] _ftp = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "Imagefile").GetNullToEmpty()).Split('\\');
                if (_ftp.Length > 1)
                {
                    FileHandler.UploadFile1(_file, GlobalVariable.FTP_SERVER + "QCSTD/");
                    gridEx2.MainGrid.MainView.SetRowCellValue(rowHandle, "Imagefile", "QCSTD/" + _ftp[_ftp.Length - 1]);

                }

            }

            ModelServiceQCT1001.Save();
        }
    }
}