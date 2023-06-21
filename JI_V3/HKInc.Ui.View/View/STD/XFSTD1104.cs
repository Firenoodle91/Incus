using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using System.Collections.Specialized;
using System.IO;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 품목한도이력관리
    /// </summary>
    public partial class XFSTD1104 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFSTD1104()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"), false);
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            MasterGridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("CombineSpec", LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            MasterGridExControl.BestFitColumns();

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn("CheckDivision", LabelConvert.GetLabelText("InspectionDivision"));
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"));
            DetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("CheckList"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            DetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn("FileData", LabelConvert.GetLabelText("FileData"), false);
            DetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("ApplyDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");           
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckList", "Memo", "FileName", "CheckDivision", "ProcessCode", "ApplyDate");
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            DetailGridExControl.BestFitColumns();
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            //DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_ItemLimitImage, "FileName", "FileUrl");
            //DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            //DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, DetailGridExControl, "FileName", "FileUrl");
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(DetailGridExControl, "FileData", "FileName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitCombo()
        {
            // 20210813 오세완 차장 수입검사 성적서에 이미지가 나와야 해서 여기에 출력 조건을 원자재도 추가
            //IupItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" &&  (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList());
            IupItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => (p.TopCategory == MasterCodeSTR.TopCategory_WAN || 
                                                                                                                                                  p.TopCategory == MasterCodeSTR.TopCategory_BAN || 
                                                                                                                                                  p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing || 
                                                                                                                                                  p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCode = IupItemCode.EditValue.GetNullToEmpty();

            // 20210813 오세완 차장 수입검사 성적서에 이미지가 나와야 해서 여기에 출력 조건을 원자재도 추가
            //MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
            //                                                               && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing))
            //                                                         .ToList();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                           && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || 
                                                                               p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing || p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }
        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_STD1104List.OrderByDescending(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
           
            #region FTP 업로드 체크
            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_STD1100>;
            //    var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();
            //    if (editList.Count > 0)
            //    {
            //            foreach (var v in editList.Where(p => p.TN_STD1104List.Any(c => c.CheckDivision != "Q01")).ToList())
            //            {
            //                foreach (var d in v.TN_STD1104List)
            //                {
            //                    if (d.ProcessCode.GetNullToEmpty() == "")
            //                    {
            //                        MessageBox.Show(v.ItemName + "(" + v.ItemCode + ")의 공정이 누락 되었습니다.");
            //                        return;
            //                    }
            //                }
            //            }

            //          foreach (var v in editList.Where(p => p.TN_STD1104List.Any(c => c.FileUrl != null && (c.FileUrl.Contains("\\") || c.FileUrl == "Clipboard_Image"))).ToList())
            //          {
            //            foreach (var d in v.TN_STD1104List.Where(c => c.FileUrl != null && (c.FileUrl.Contains("\\") || c.FileUrl == "Clipboard_Image")).ToList())
            //            {
            //                string[] filename = d.FileUrl.ToString().Split('\\');
            //                if (filename.Length != 1)
            //                {
            //                    var realFileName = d.ItemCode + "_" + filename[filename.Length - 1];
            //                    var ftpFileUrl = MasterCodeSTR.FtpFolder_ItemLimitImage + "/" + realFileName;

            //                    FileHandler.UploadFTP(d.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ItemLimitImage + "/");

            //                    d.FileName = realFileName;
            //                    d.FileUrl = ftpFileUrl;
            //                }
            //                else if (d.FileUrl == "Clipboard_Image")
            //                {
            //                    var realFileName = d.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            //                    var ftpFileUrl = MasterCodeSTR.FtpFolder_ItemLimitImage + "/" + realFileName;
            //                    var localImage = d.localImage as Image;
            //                    FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ItemLimitImage + "/");

            //                    d.FileName = realFileName;
            //                    d.FileUrl = ftpFileUrl;
            //                }
            //            }
            //        }
            //        //foreach (var v in masterList.Where(p => p.TN_STD1104List.Any(c => !c.DeleteFilePath.IsNullOrEmpty())).ToList())
            //        //{
            //        //    foreach (var d in v.TN_STD1104List.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
            //        //    {
            //        //        try
            //        //        {
            //        //            FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
            //        //        }
            //        //        catch { }
            //        //    }
            //        //}

            //        //foreach (var v in masterList.Where(p => p.TN_STD1104List.Any(c => !c.UploadFilePath.IsNullOrEmpty())).ToList())
            //        //{
            //        //    foreach (var d in v.TN_STD1104List.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
            //        //    {
            //        //        try
            //        //        {
            //        //            FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/{3}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_ItemLimitImage, d.ItemCode, d.Seq));
            //        //            d.FileUrl = string.Format("{0}/{1}/{2}/{3}", MasterCodeSTR.FtpFolder_ItemLimitImage, d.ItemCode, d.Seq, d.FileName);
            //        //        }
            //        //        catch { }
            //        //    }
            //        //}
            //    }
            //}
            #endregion

            ModelService.Save();
            DataLoad();
        }
        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_STD1104;
            if (detailObj == null) return;

            masterObj.TN_STD1104List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var newObj = new TN_STD1104()
            {
                ItemCode = masterObj.ItemCode,
                Seq = masterObj.TN_STD1104List.Count == 0 ? 1 : masterObj.TN_STD1104List.Max(p => p.Seq) + 1
                ,ApplyDate=DateTime.Today
            };
            masterObj.EditRowFlag = "Y";
            masterObj.TN_STD1104List.Add(newObj);
            DetailGridBindingSource.Add(newObj);
        }

        /// <summary>
        /// 디테일 셀 변경 시 마스터 Edit 체크를 위함.
        /// </summary>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            masterObj.EditRowFlag = "Y";
        }

        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_STD1104;
            if (detailObj == null) return;

            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                StringCollection list = Clipboard.GetFileDropList();
                if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
                {
                    using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        detailObj.localImage = fileData;
                        detailObj.FileName = list[0];
                        detailObj.FileUrl = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        detailObj.FileName = "Clipboard_Image";
                        detailObj.FileUrl = "Clipboard_Image";
                        detailObj.localImage = GetImage;
                    }
                }
                DetailGridExControl.BestFitColumns();
                detailObj.EditRowFlag = "Y";
                masterObj.EditRowFlag = "Y";
            }
        }
    }
}

