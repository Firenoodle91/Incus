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
    /// <summary>
    /// 품목한도이력관리화면
    /// </summary>
    public partial class XFSTD2000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFSTD2000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
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
            MasterGridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");
            MasterGridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
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
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
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
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), "CustomerCode", "CustomerName");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("AppendDt");
            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("AppendUser", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Qcnote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.MainView.Columns["Imagefile"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(DetailGridExControl, "Imagefile");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Qcname", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) 
                                                                        && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta)
                                                                     )
                                                                     .OrderBy(p => p.ItemNm1)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if(obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_QCT1001List.OrderByDescending(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null) return;

            TN_QCT1001 new_obj = new TN_QCT1001()
            {
                ItemCode = obj.ItemCode
                , Seq = obj.TN_QCT1001List.Count == 0 ? 1 : obj.TN_QCT1001List.Max(c => c.Seq).GetIntNullToZero() + 1
                , Add_Or_Modify_Flag = "Y"
            };
            DetailGridBindingSource.Add(new_obj);
            obj.TN_QCT1001List.Add(new_obj);
        }

        protected override void DeleteDetailRow()
        {
            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (MasterObj == null) return;
            TN_QCT1001 obj = DetailGridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;
            DetailGridBindingSource.Remove(obj);
            MasterObj.TN_QCT1001List.Remove(obj);
        }

        protected override void DataSave()
        {
            lSave();
            DataLoad();
        }

        private void lSave()
        {
            var MasterList = MasterGridBindingSource.List as List<TN_STD1100>;
            if (MasterList != null)
            {
                var FtpSaveList = MasterList.Where(c => c.TN_QCT1001List.Any(x => x.Add_Or_Modify_Flag == "Y")).ToList();

                foreach (var v in FtpSaveList) 
                {
                    foreach (var c in v.TN_QCT1001List)
                    {
                        if(c.Add_Or_Modify_Flag == "Y")
                        {
                            string _file = c.Imagefile.GetNullToEmpty();
                            string[] _ftp = c.Imagefile.GetNullToEmpty().Split('\\');
                            if (_ftp.Length > 1)
                            {
                                FileHandler.UploadFile1(_file, GlobalVariable.FTP_SERVER + "QCSTD/");
                                c.Imagefile = "QCSTD/" + _ftp[_ftp.Length - 1];                                
                            }
                        }
                    }
                }                
            }
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            ModelService.Save();
        }
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName == "Imagefile")
            {
                TN_QCT1001 obj = DetailGridBindingSource.Current as TN_QCT1001;
                if (obj == null) return;
                obj.Add_Or_Modify_Flag = "Y";
            }
        }

    }
}