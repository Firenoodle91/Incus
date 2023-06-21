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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 검사의뢰서관리화면
    /// </summary>
    public partial class XFQC2200 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT2200> ModelService = (IService<TN_QCT2200>)ProductionFactory.GetDomainService("TN_QCT2200");

        public XFQC2200()
        {
            InitializeComponent();
            dp_reqdate.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dp_reqdate.DateToEdit.DateTime = DateTime.Today.AddDays(30);
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("No","의뢰번호");
            GridExControl.MainGrid.AddColumn("ReqDate", "의뢰일");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);          
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1","품번");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("CustCode", "거래처");
            GridExControl.MainGrid.AddColumn("ReqUser", "의뢰자");
            GridExControl.MainGrid.AddColumn("ReqQty", "의뢰수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ReturnDate", "의뢰시한");
            GridExControl.MainGrid.AddColumn("ReqFile", "의뢰서");
            GridExControl.MainGrid.AddColumn("CheckDate", "검사일");
            GridExControl.MainGrid.AddColumn("CheckId", "검사담당자"); // 10
            GridExControl.MainGrid.AddColumn("CheckFile", "성적서"); // 10
            GridExControl.MainGrid.AddColumn("Memo","비고");
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {

            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            //GridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");          
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequesHandler.GetCommCode(MasterCodeSTR.MCMAKER), "Mcode", "Codename");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            //GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("No", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string item = tx_item.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = ModelService.GetList(p => (p.ReqDate >= dp_reqdate.DateFrEdit.DateTime 
                                                                   && p.ReqDate <= dp_reqdate.DateToEdit.DateTime) 
                                                                   && (string.IsNullOrEmpty(item) ? true : (p.TN_STD1100.ItemNm1.Contains(item) || p.TN_STD1100.ItemNm.Contains(item)))
                                                                )
                                                                .OrderBy(o => o.No)
                                                                .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            TN_QCT2200 obj = GridBindingSource.Current as TN_QCT2200;

            if (obj != null)
            {
                GridBindingSource.Remove(obj);
                ModelService.Delete(obj);
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT2200, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {
                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "ReqFile")
                    {
                        String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["ReqFile"]).ToString();
                        byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);
                        string[] lfileName = filename.Split('/');
                        if (lfileName.Length > 1)
                        {
                            File.WriteAllBytes(lfileName[lfileName.Length - 1], img);
                            HKInc.Service.Handler.FileHandler.StartProcess(lfileName[lfileName.Length - 1]);
                        }
                        else
                        {
                            File.WriteAllBytes(filename, img);
                            HKInc.Service.Handler.FileHandler.StartProcess(filename);
                        }
                    }
                    if (e.Column.Name.ToString() == "CheckFile")
                    {
                        String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["CheckFile"]).ToString();
                        byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);
                        string[] lfileName = filename.Split('/');
                        if (lfileName.Length > 1)
                        {
                            File.WriteAllBytes(lfileName[lfileName.Length - 1], img);
                            HKInc.Service.Handler.FileHandler.StartProcess(lfileName[lfileName.Length - 1]);
                        }
                        else
                        {
                            File.WriteAllBytes(filename, img);
                            HKInc.Service.Handler.FileHandler.StartProcess(filename);
                        }
                    }
                }
            }
            catch (Exception ex) { }

        }
    }
}