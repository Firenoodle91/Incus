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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Specialized;
using System.IO;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 도면관리화면
    /// </summary>
    public partial class XFSTD1101 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFSTD1101()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_ProductTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
        }

        protected override void InitGrid()
        {

            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemCode", LabelConvert.GetLabelText("CustomerItemCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerItemName", LabelConvert.GetLabelText("CustomerItemName"));
            MasterGridExControl.MainGrid.AddColumn("ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            MasterGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("DesignFileName", LabelConvert.GetLabelText("DesignFileName"));
            DetailGridExControl.MainGrid.AddColumn("DesignFileUrl", LabelConvert.GetLabelText("DesignFileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DesignFileName");
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1101>(DetailGridExControl);
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

            //DetailGridExControl.MainGrid.MainView.Columns["DesignFileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, DetailGridExControl, "DesignFileName", "DesignFileUrl");
            DetailGridExControl.MainGrid.MainView.Columns["DesignFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_DesignImage, "DesignFileName", "DesignFileUrl");
            DetailGridExControl.MainGrid.MainView.Columns["DesignFileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var productTeamCode = lup_ProductTeamCode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (string.IsNullOrEmpty(productTeamCode) ? true :p.ProcTeamCode == productTeamCode)
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)
                                                                     )
                                                                     .ToList();
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

            DetailGridBindingSource.DataSource = masterObj.TN_STD1101List.OrderByDescending(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 파일 CHECK
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_STD1100>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(p => p.TN_STD1101List.Any(c => c.DesignFileUrl != null && (c.DesignFileUrl.Contains("\\") || c.DesignFileUrl == "Clipboard_Image"))).ToList())
                    {
                        foreach (var d in v.TN_STD1101List.Where(c => c.DesignFileUrl != null && (c.DesignFileUrl.Contains("\\") || c.DesignFileUrl == "Clipboard_Image")).ToList())
                        {
                            string[] filename = d.DesignFileUrl.ToString().Split('\\');
                            if (filename.Length != 1)
                            {
                                var realFileName = d.ItemCode + "_" + filename[filename.Length - 1];
                                realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_DesignImage + "/" + realFileName;

                                FileHandler.UploadFTP(d.DesignFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_DesignImage + "/");

                                d.DesignFileName = realFileName;
                                d.DesignFileUrl = ftpFileUrl;
                            }
                            else if (d.DesignFileUrl == "Clipboard_Image")
                            {
                                var realFileName = d.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                                realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_DesignImage + "/" + realFileName;
                                var localImage = d.localImage as Image;
                                FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_DesignImage + "/");

                                d.DesignFileName = realFileName;
                                d.DesignFileUrl = ftpFileUrl;
                            }
                        }
                    }
                    //foreach (var v in masterList.Where(p => p.TN_STD1101List.Any(c => !c.DeleteFilePath.IsNullOrEmpty())).ToList())
                    //{
                    //    foreach (var d in v.TN_STD1101List.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
                    //    {
                    //        try
                    //        {
                    //            FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
                    //        }
                    //        catch { }
                    //    }
                    //}
                    
                    //foreach (var v in masterList.Where(p => p.TN_STD1101List.Any(c => !c.UploadFilePath.IsNullOrEmpty())).ToList())
                    //{
                    //    foreach (var d in v.TN_STD1101List.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
                    //    {
                    //        try
                    //        {
                    //            FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/{3}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_DesignImage, d.ItemCode, d.Seq));
                    //            d.DesignFileUrl = string.Format("{0}/{1}/{2}/{3}", MasterCodeSTR.FtpFolder_DesignImage, d.ItemCode, d.Seq, d.DesignFileName);
                    //        }
                    //        catch { }
                    //    }
                    //}
                }
            }
            #endregion

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var newObj = new TN_STD1101()
            {

                ItemCode = masterObj.ItemCode,
                Seq = masterObj.TN_STD1101List.Count == 0 ? 1 : masterObj.TN_STD1101List.Max(p => p.Seq) + 1
        
            };
            
            masterObj.NewRowFlag = "Y";       
            masterObj.TN_STD1101List.Add(newObj);
            DetailGridBindingSource.Add(newObj);
        }

        protected override void DeleteDetailRow()
        {
           
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var delobj = DetailGridBindingSource.Current as TN_STD1101;
            if (delobj == null) return;

            //2021-11-04 기존 내역 삭제 가능 기능 회복으로 주석 처리
            //if (delobj.NewRowFlag != "Y")
            //{
            //    return;
            //}

            masterObj.TN_STD1101List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.MainGrid.BestFitColumns();
           
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

            var detailObj = DetailGridBindingSource.Current as TN_STD1101;
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
                        detailObj.DesignFileName = list[0];
                        detailObj.DesignFileUrl = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        detailObj.DesignFileName = "Clipboard_Image";
                        detailObj.DesignFileUrl = "Clipboard_Image";
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