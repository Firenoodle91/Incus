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
using HKInc.Ui.Model.Domain.VIEW;
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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 4M관리대장화면
    /// </summary>
    public partial class XFSTD4M001 : Service.Base.ListFormTemplate
    {
        IService<TN_STD4M001> ModelService = (IService<TN_STD4M001>)ProductionFactory.GetDomainService("TN_STD4M001");

        public XFSTD4M001()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
        }

        protected override void InitCombo()
        {
            lup_ProductTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("L4mno", LabelConvert.GetLabelText("L4mno"), false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"), false);
            GridExControl.MainGrid.AddColumn("CustCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));
            GridExControl.MainGrid.AddColumn("ChgDate", LabelConvert.GetLabelText("ChangeDate"));
            GridExControl.MainGrid.AddColumn("ChgCust", LabelConvert.GetLabelText("ChangeCustomer"));
            GridExControl.MainGrid.AddColumn("ChgNote", LabelConvert.GetLabelText("ChangeContent"));
            GridExControl.MainGrid.AddColumn("ChgMemo", LabelConvert.GetLabelText("ChangeCause"));
            GridExControl.MainGrid.AddColumn("ReqCust", LabelConvert.GetLabelText("ReceiptCustomer"));
            GridExControl.MainGrid.AddColumn("ReqUser", LabelConvert.GetLabelText("ReceiptUser"));
            GridExControl.MainGrid.AddColumn("ChkCust1Cha", LabelConvert.GetLabelText("CheckFirstCustomer"));
            GridExControl.MainGrid.AddColumn("ChkDate1Cha", LabelConvert.GetLabelText("CheckFirstDate"));
            GridExControl.MainGrid.AddColumn("ChkQc1Cha", LabelConvert.GetLabelText("CheckFirstQC"));
            GridExControl.MainGrid.AddColumn("ChkQcUser1Cha", LabelConvert.GetLabelText("CheckFirstUser"));
            GridExControl.MainGrid.AddColumn("ChkQcFile1Cha", LabelConvert.GetLabelText("CheckFirstFile"));
            GridExControl.MainGrid.AddColumn("ChkCust2Cha", LabelConvert.GetLabelText("CheckSecondCustomer"));
            GridExControl.MainGrid.AddColumn("ChkDate2Cha", LabelConvert.GetLabelText("CheckSecondDate"));
            GridExControl.MainGrid.AddColumn("ChkQc2Cha", LabelConvert.GetLabelText("CheckSecondQC"));
            GridExControl.MainGrid.AddColumn("ChkQcUser2Cha", LabelConvert.GetLabelText("CheckSecondUser"));
            GridExControl.MainGrid.AddColumn("ChkQcFile2Cha", LabelConvert.GetLabelText("CheckSecondFile"));
            GridExControl.MainGrid.AddColumn("ChkCust3Cha", LabelConvert.GetLabelText("CheckThirdCustomer"));
            GridExControl.MainGrid.AddColumn("ChkDate3Cha", LabelConvert.GetLabelText("CheckThirdDate"));
            GridExControl.MainGrid.AddColumn("ChkQc3Cha", LabelConvert.GetLabelText("CheckThirdQC"));
            GridExControl.MainGrid.AddColumn("ChkQcUser3Cha", LabelConvert.GetLabelText("CheckThirdUser"));
            GridExControl.MainGrid.AddColumn("ChkQcFile3Cha", LabelConvert.GetLabelText("CheckThirdFile"));
            GridExControl.MainGrid.AddColumn("FinalUser", LabelConvert.GetLabelText("FinalConfirmUser"));
            GridExControl.MainGrid.AddColumn("ProdWorkDate", LabelConvert.GetLabelText("MassProductDate"));
            GridExControl.MainGrid.AddColumn("ReqDoc", LabelConvert.GetLabelText("RequestDocument"));
            GridExControl.MainGrid.AddColumn("EtcFile1", LabelConvert.GetLabelText("EtcFile1"));
            GridExControl.MainGrid.AddColumn("EtcFile2", LabelConvert.GetLabelText("EtcFile2"));
            GridExControl.MainGrid.AddColumn("EtcFile3", LabelConvert.GetLabelText("EtcFile3"));
            GridExControl.MainGrid.AddColumn("EtcFile4", LabelConvert.GetLabelText("EtcFile4"));
            GridExControl.MainGrid.AddColumn("EtcFile5", LabelConvert.GetLabelText("EtcFile5"));
            GridExControl.MainGrid.AddColumn("Memo1", LabelConvert.GetLabelText("Memo1"));
            GridExControl.MainGrid.AddColumn("Memo2", LabelConvert.GetLabelText("Memo2"));
            GridExControl.MainGrid.AddColumn("Memo3", LabelConvert.GetLabelText("Memo3"));
            GridExControl.MainGrid.AddColumn("Memo4", LabelConvert.GetLabelText("Memo4"));

            GridExControl.MainGrid.Columns["ChkQcFile1Cha"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["ChkQcFile1Cha"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["ChkQcFile1Cha"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["ChkQcFile2Cha"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["ChkQcFile2Cha"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["ChkQcFile2Cha"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["ChkQcFile3Cha"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["ChkQcFile3Cha"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["ChkQcFile3Cha"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["ReqDoc"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["ReqDoc"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["ReqDoc"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["EtcFile1"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["EtcFile1"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["EtcFile1"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["EtcFile2"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["EtcFile2"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["EtcFile2"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["EtcFile3"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["EtcFile3"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["EtcFile3"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["EtcFile4"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["EtcFile4"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["EtcFile4"].AppearanceCell.Font, FontStyle.Underline);
            GridExControl.MainGrid.Columns["EtcFile5"].AppearanceCell.ForeColor = Color.Blue;
            GridExControl.MainGrid.Columns["EtcFile5"].AppearanceCell.Font = new Font(GridExControl.MainGrid.Columns["EtcFile5"].AppearanceCell.Font, FontStyle.Underline);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChgDate");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChkDate1Cha");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChkDate2Cha");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ChkDate3Cha");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ProdWorkDate");

            GridExControl.MainGrid.MainView.Columns["ChgNote"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "ChgNote");
            GridExControl.MainGrid.MainView.Columns["ChgMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "ChgMemo");
            GridExControl.MainGrid.MainView.Columns["Memo1"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo1");
            GridExControl.MainGrid.MainView.Columns["Memo2"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo2");
            GridExControl.MainGrid.MainView.Columns["Memo3"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo3");
            GridExControl.MainGrid.MainView.Columns["Memo4"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo4");

            //GridExControl.MainGrid.MainView.Columns["ReqDoc"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "ReqDoc", "ReqDoc");
            //GridExControl.MainGrid.MainView.Columns["ChkQcFile1Cha"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "ChkQcFile1Cha", "ChkQcFile1Cha");
            //GridExControl.MainGrid.MainView.Columns["ChkQcFile2Cha"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "ChkQcFile2Cha", "ChkQcFile2Cha");
            //GridExControl.MainGrid.MainView.Columns["ChkQcFile3Cha"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "ChkQcFile3Cha", "ChkQcFile3Cha");
            //GridExControl.MainGrid.MainView.Columns["EtcFile1"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "EtcFile1", "EtcFile1");
            //GridExControl.MainGrid.MainView.Columns["EtcFile2"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "EtcFile2", "EtcFile2");
            //GridExControl.MainGrid.MainView.Columns["EtcFile3"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "EtcFile3", "EtcFile3");
            //GridExControl.MainGrid.MainView.Columns["EtcFile4"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "EtcFile4", "EtcFile4");
            //GridExControl.MainGrid.MainView.Columns["EtcFile5"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, "EtcFile5", "EtcFile5");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChgCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ReqCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChkCust1Cha", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChkCust2Cha", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ChkCust3Cha", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CarType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CarType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));


            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqUser", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChkQcUser1Cha", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChkQcUser2Cha", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ChkQcUser3Cha", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FinalUser", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            GridExControl.BestFitColumns();
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

            string itemCodeName = tx_item.EditValue.GetNullToEmpty();
            var productTeamCode = lup_ProductTeamCode.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCodeName) ? true : (p.ItemCode.Contains(itemCodeName) || (p.TN_STD1100.ItemName.Contains(itemCodeName) || p.TN_STD1100.ItemNameENG.Contains(itemCodeName) || p.TN_STD1100.ItemNameCHN.Contains(itemCodeName))))
                                                                 && (string.IsNullOrEmpty(productTeamCode) ? true : p.TN_STD1100.ProcTeamCode == productTeamCode)).ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl);
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
                ModelService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {
                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "ChkQcFile1Cha")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }

                    if (e.Column.Name.ToString() == "ChkQcFile2Cha")
                    {
                        ImageLoad(e, gv, e.Column.Name.ToString());
                    }
                    if (e.Column.Name.ToString() == "ChkQcFile3Cha")
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

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFStd4M001, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
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

    }
}