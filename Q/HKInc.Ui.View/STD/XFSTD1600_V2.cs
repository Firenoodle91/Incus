using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using System.Collections.Specialized;
using HKInc.Service.Helper;
using System.IO;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.STD
{
    /// <summary>
    /// 20220322 오세완 차장 
    /// ftp방식으로 변경한 버전
    /// 도면관리
    /// </summary>
    public partial class XFSTD1600_V2 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_STD1600> ModelService = (IService<TN_STD1600>)ProductionFactory.GetDomainService("TN_STD1600");
        #endregion

        public XFSTD1600_V2()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null)
                return;

            obj.EditRowFlag = "Y";
        }

        /// <summary>
        /// 2022-02-28 김진우 추가
        /// </summary>
        protected override void MasterFocusedRowChanged()
        {
            if (MasterGridExControl.MainGrid.MainView.RowCount == 0)
            {
                DetailGridExControl.MainGrid.Clear();
            }
            else
            {
                TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
                if (obj == null)
                    return;

                DetailGridExControl.MainGrid.Clear();
                //ModelService.ReLoad();

                List<TN_STD1600> tempList = ModelService.GetList(p => p.ItemCode == obj.ItemCode).OrderByDescending(o => o.Seq).ToList();
                if (tempList == null)
                    DetailGridBindingSource.Clear();
                else if (tempList.Count == 0)
                    DetailGridBindingSource.Clear();
                else
                    DetailGridBindingSource.DataSource = tempList;

                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.MainGrid.BestFitColumns();
            }
        }

        protected override void InitCombo()
        {
            lupItemtype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);           // 2022-02-28 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품목명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("MainCust", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("CustItemCode", "고객사품번");

            MasterGridExControl.MainGrid.AddColumn("CustItemNm", "고객사품명");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");

            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");      
            MasterGridExControl.MainGrid.AddColumn("Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
          
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("DesignFile", "도면");
            DetailGridExControl.MainGrid.AddColumn("DesignFileUrl", false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DesignFile");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");            
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList(), 
                "CustomerCode", "CustomerName");

            DetailGridExControl.MainGrid.MainView.Columns["DesignFile"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, 
                MasterCodeSTR.FtpFolder_DesignImage, "DesignFile", "DesignFileUrl");
            DetailGridExControl.MainGrid.MainView.Columns["DesignFile"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
        }

        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            var detailObj = DetailGridBindingSource.Current as TN_STD1600;
            if (detailObj == null)
                return;

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

                        detailObj.DesignMap = fileData;
                        detailObj.DesignFile = list[0];
                        detailObj.DesignFileUrl = list[0];
                    }
                }
                else
                {
                    // 20220322 오세완 차장 이방법을 사용하면 DesignMap에 데이타가 존재하게 된다. 
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        // 20220322 오세완 차장 이방법은 오류가 생겨서 아래 방법으로 교체
                        //using(MemoryStream ms = new MemoryStream())
                        //{
                        //    GetImage.Save(ms, GetImage.RawFormat);
                        //    detailObj.DesignMap = ms.ToArray();
                        //}

                        ImageConverter ig = new ImageConverter();
                        byte[] bArr = (byte[])ig.ConvertTo(GetImage, typeof(byte[]));
                        if (bArr != null)
                            detailObj.DesignMap = bArr;
                        
                        detailObj.DesignFile = "Clipboard_Image";
                        detailObj.DesignFileUrl = "Clipboard_Image";
                    }
                }
                DetailGridExControl.BestFitColumns();
                masterObj.EditRowFlag = "Y";
            }
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();     // 2022-07-27 김진우 추가

            MasterGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo();
            InitRepository();

            string sItemcode_name = tx_itemname.Text.GetNullToEmpty();
            string cta = lupItemtype.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1100>(p => (string.IsNullOrEmpty(sItemcode_name) ? true : p.ItemNm.Contains(tx_itemname.Text) || (p.ItemCode == tx_itemname.Text) || p.ItemNm1.Contains(tx_itemname.Text)) && 
                                                                                            (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta) && 
                                                                                            (p.UseYn == "Y")).OrderBy(p => p.ItemNm).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();     // 2022-07-27 김진우 추가
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null)
                return;

            TN_STD1600 new_obj = new TN_STD1600() { ItemCode = obj.ItemCode };
            obj.EditRowFlag = "Y";
            DetailGridBindingSource.Add(new_obj);
            ModelService.Insert(new_obj);
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            #region 파일 CHECK
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                List<TN_STD1100> masterList = MasterGridBindingSource.List as List<TN_STD1100>;
                List<TN_STD1100> editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach(TN_STD1100 each in editList)
                    {
                        List<TN_STD1600> detailList = DetailGridBindingSource.List as List<TN_STD1600>;
                        if (detailList != null)
                            if (detailList.Count > 0)
                                detailList = detailList.Where(p => p.ItemCode == each.ItemCode &&
                                                                   p.DesignFileUrl != null &&
                                                                   (p.DesignFileUrl.Contains("\\") || p.DesignFileUrl == "Clipboard_Image")).ToList();

                        if(detailList != null)
                            if(detailList.Count > 0)
                            {
                                string sRealFileName = "";
                                string sFTP_FileName = "";
                                bool bFileProcess = false;

                                foreach (TN_STD1600 each_Det in detailList)
                                {
                                    string[] sFilename = each_Det.DesignFileUrl.Split('\\');
                                    if (sFilename.Length != 1)
                                    {
                                        sRealFileName = each.ItemCode + "_" + sFilename[sFilename.Length - 1];
                                        sRealFileName = FileHandler.CheckFileName(sRealFileName);
                                        sFTP_FileName = MasterCodeSTR.FtpFolder_DesignImage + "/" + sRealFileName;
                                        FileHandler.UploadFTP(each_Det.DesignFileUrl, sRealFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_DesignImage + "/");
                                        bFileProcess = true;
                                    }
                                    else if(each_Det.DesignFile == "Clipboard_Image")
                                    {
                                        sRealFileName = each.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                                        sRealFileName = FileHandler.CheckFileName(sRealFileName);
                                        sFTP_FileName = MasterCodeSTR.FtpFolder_DesignImage + "/" + sRealFileName;
                                        using(MemoryStream ms = new MemoryStream(each_Det.DesignMap))
                                        {
                                            using(Image im = Image.FromStream(ms))
                                            {
                                                FileHandler.UploadFTP(im, sRealFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_DesignImage + "/");
                                            }
                                        }
                                        bFileProcess = true;
                                    }
                                    
                                    if(bFileProcess)
                                    {
                                        each_Det.DesignFile = sRealFileName;
                                        each_Det.DesignFileUrl = sFTP_FileName;
                                    }
                                }
                            }
                    }
                }
            }
            #endregion

            ModelService.Save();
            DataLoad();
        }
    }
}