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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.STD
{
    public partial class XFSTD1700 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD4M001> ModelService = (IService<TN_STD4M001>)ProductionFactory.GetDomainService("TN_STD4M001");
        public XFSTD1700()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
         
        }
        protected override void InitCombo()
        {
           // luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
            // lupitemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y").OrderBy(o=>o.ItemNm1).ToList());
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {

                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "ChkQcfile1cha")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }

                    if (e.Column.Name.ToString() == "ChkQcfile2cha")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }
                    if (e.Column.Name.ToString() == "ChkQcfile3cha")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }
                    if (e.Column.Name.ToString() == "ReqDoc")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }
                    if (e.Column.Name.ToString() == "EtcFile1")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }
                    if (e.Column.Name.ToString() == "EtcFile2")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }
                    if (e.Column.Name.ToString() == "EtcFile3")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }
                    if (e.Column.Name.ToString() == "EtcFile4")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }

                    if (e.Column.Name.ToString() == "EtcFile5")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }


                }
            }
            catch { }
        }

        private static void ImageLoad(RowCellClickEventArgs e, GridView gv, string fieldname)
        {
            String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns[fieldname]).ToString();

         
            if (!string.IsNullOrEmpty(filename))
            {
                string[] lfileName1 = filename.Split(':');
                if (lfileName1.Length < 2)
                {
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
                else
                {
                    byte[] imgfile = FileHandler.FileToByte(filename);
                    string[] lfileName2 = filename.Split('\\');
                    File.WriteAllBytes(lfileName2[lfileName2.Length - 1], imgfile);
                    HKInc.Service.Handler.FileHandler.StartProcess(lfileName2[lfileName2.Length - 1]);
                }
            }
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("L4mno", false);
            GridExControl.MainGrid.AddColumn("ItemCode", "품번");
            GridExControl.MainGrid.AddColumn("Seq", "순번");
            GridExControl.MainGrid.AddColumn("CustCode", "고객명");
            GridExControl.MainGrid.AddColumn("CarType", "차종");
            GridExControl.MainGrid.AddColumn("ChgDate", "변경일");
            GridExControl.MainGrid.AddColumn("ChgCust", "변경처");
            GridExControl.MainGrid.AddColumn("ChgNote", "변경내용");
            GridExControl.MainGrid.AddColumn("ChgMemo", "변경원인");
            GridExControl.MainGrid.AddColumn("ReqCust", "접수처");
            GridExControl.MainGrid.AddColumn("ReqUser", "접수자");
            GridExControl.MainGrid.AddColumn("ChkCust1cha", "1차검토처");
            GridExControl.MainGrid.AddColumn("ChkDate1cha", "1차검토일");
            GridExControl.MainGrid.AddColumn("ChkQc1cha", "1차품질");
            GridExControl.MainGrid.AddColumn("ChkQcuser1cha", "1차검토자");
            GridExControl.MainGrid.AddColumn("ChkQcfile1cha", "1차파일");
            GridExControl.MainGrid.AddColumn("ChkCust2cha", "2차검토처");
            GridExControl.MainGrid.AddColumn("ChkDate2cha", "2차검토일");
            GridExControl.MainGrid.AddColumn("ChkQc2cha", "2차품질");
            GridExControl.MainGrid.AddColumn("ChkQcuser2cha", "2차검토자");
            GridExControl.MainGrid.AddColumn("ChkQcfile2cha", "2차파일");
            GridExControl.MainGrid.AddColumn("ChkCust3cha", "3차검토처");
            GridExControl.MainGrid.AddColumn("ChkDate3cha", "3차검토일");
            GridExControl.MainGrid.AddColumn("ChkQc3cha", "3차품질");
            GridExControl.MainGrid.AddColumn("ChkQcuser3cha", "3차검토자");
            GridExControl.MainGrid.AddColumn("ChkQcfile3cha", "3차파일");
            GridExControl.MainGrid.AddColumn("FinalUser", "최종승인자");
            GridExControl.MainGrid.AddColumn("ProdWorkdate", "양산일");
            GridExControl.MainGrid.AddColumn("ReqDoc", "요청서");
            GridExControl.MainGrid.AddColumn("EtcFile1", "첨부1");
            GridExControl.MainGrid.AddColumn("EtcFile2", "첨부2");
            GridExControl.MainGrid.AddColumn("EtcFile3", "첨부3");
            GridExControl.MainGrid.AddColumn("EtcFile4", "첨부4");
            GridExControl.MainGrid.AddColumn("EtcFile5", "첨부5");
            GridExControl.MainGrid.AddColumn("Memo1", "비고1");
            GridExControl.MainGrid.AddColumn("Memo2", "비고2");
            GridExControl.MainGrid.AddColumn("Memo3", "비고3");
            GridExControl.MainGrid.AddColumn("Memo4", "비고4");
            

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {

            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChgDate");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChkDate1cha");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChkDate2cha");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChkDate3cha");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ProdWorkdate");

            GridExControl.MainGrid.MainView.Columns["ChgNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["ChgMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Memo1"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Memo2"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Memo3"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Memo4"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
                        
            GridExControl.MainGrid.MainView.Columns["ChkQcfile1cha"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1,true,"ChkQcfile1cha");
            GridExControl.MainGrid.MainView.Columns["ChkQcfile2cha"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1, true, "ChkQcfile2cha");
            GridExControl.MainGrid.MainView.Columns["ChkQcfile3cha"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1, true, "ChkQcfile3cha");
            GridExControl.MainGrid.MainView.Columns["EtcFile1"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1, true, "EtcFile1");
            GridExControl.MainGrid.MainView.Columns["EtcFile2"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1, true, "EtcFile2");
            GridExControl.MainGrid.MainView.Columns["EtcFile3"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1, true, "EtcFile3");
            GridExControl.MainGrid.MainView.Columns["EtcFile4"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1, true, "EtcFile4");
            GridExControl.MainGrid.MainView.Columns["EtcFile5"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(gridEx1, true, "EtcFile5");

            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChgCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ReqCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChkCust1cha", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChkCust2cha", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChkCust3cha", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CarType", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");


            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqUser", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChkQcuser1cha", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChkQcuser2cha", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChkQcuser3cha", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FinalUser", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
          
          
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            // string itemcode = lupitemcode.EditValue.GetNullToEmpty();
        
            string itemcode = tx_item.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = ModelService.GetList(p =>string.IsNullOrEmpty(itemcode)?true:(p.ItemCode==itemcode||p.TN_STD1100.ItemNm.Contains(itemcode)||p.TN_STD1100.ItemNm1.Contains(itemcode)))
                                                            .OrderBy(p => p.ItemCode)
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
            TN_STD4M001 obj = GridBindingSource.Current as TN_STD4M001;

            if (obj != null)
            {

                GridBindingSource.RemoveCurrent();





            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFStd4M001, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }
    }
}