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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using System.Collections.Specialized;
using HKInc.Service.Helper;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 설비스페어파트관리
    /// </summary>
    public partial class XFMEA1200 : Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFMEA1200()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            GridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        /// <summary>
        /// 2021-05-27 김진우 주임       조회조건 TEXTBOX 형식에서 LIST 형식으로 변경하여 추가
        /// </summary>
        protected override void InitCombo()
        {
            lup_SpareCodeName.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.TopCategory == MasterCodeSTR.TopCategory_SPARE || p.TopCategory == MasterCodeSTR.TopCategory_Mold).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("MachineMoldCheck", LabelConvert.GetLabelText("Division"));            // 2021-05-26 김진우 주임 추가
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("SpareCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("SpareName"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"), false);           // 2021-05-27 김진우 주임 수정
            GridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("SpareNameENG"));
            GridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("SpareNameCHN"));
            GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            GridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            GridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            GridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            GridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"));
            GridExControl.MainGrid.AddColumn("SpareStockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"));
            GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition"));
            GridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"), false);
            GridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            GridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"), false);
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MachineMoldCheck", "ItemName", "ItemName1",  "ItemNameENG", "ItemNameCHN", "MainCustomerCode", "Spec1", "Spec2", "Spec3", "Spec4", "Cost", "SafeQty", "StockPosition", "ProdFileName", "UseFlag", "Memo");
           
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1100>(GridExControl);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StockPosition", ModelService.GetChildList<TN_WMS2000>(p => true).ToList(), "PositionCode", Service.Helper.DataConvert.GetCultureDataFieldName("PositionName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineMoldCheck", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineMoldCheck), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));       // 2021-05-27 김진우 주임 추가
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));        // 2021-06-16 김진우 주임 추가
            GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl", true);
            GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            //GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, GridExControl, "ProdFileName", "ProdFileUrl", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("Cost", DefaultBoolean.Default, "n2");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            // 2021-05-27 김진우 주임 수정         기존 TEXTBOX 형식에서 LIST형으로 변경
            var spareCodeName = lup_SpareCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p =>    (string.IsNullOrEmpty(spareCodeName) ? true : (p.ItemCode.Contains(spareCodeName) || (p.ItemName.Contains(spareCodeName) || p.ItemNameENG.Contains(spareCodeName) || p.ItemNameCHN.Contains(spareCodeName))))
                                                                    && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    && (p.TopCategory == MasterCodeSTR.TopCategory_SPARE || p.TopCategory == MasterCodeSTR.TopCategory_Mold)        // 2021-05-27 김진우 주임    기존 TopCategory_SPARE 만 조회되어서 금형 추가
                                                               )
                                                               .OrderBy(p => p.ItemName)
                                                               .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            var newObj = new TN_STD1100()
            {
                NewRowFlag = "Y",
                UseFlag = "Y",                
            };

            GridBindingSource.Add(newObj);
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_STD1100;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("SpareInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        /// <summary>
        /// 2021-05-26 김진우 주임 수정
        /// 기존에 일반저장에서 추가 버튼 누를시에 한줄 추가되고 그 행에 입력되어 있는 데이터값들을 DB에 저장시킴
        /// 2021-06-16 김진우 주임 추가 수정
        /// 수정시 이미지 삭제후 저장이 안되어 수정
        /// </summary>
        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            if (GridBindingSource != null && GridBindingSource.DataSource != null)
            {
                List<TN_STD1100> masterList = GridBindingSource.List as List<TN_STD1100>;
                foreach (var v in masterList)
                {

                    if (v.NewRowFlag == "Y")
                    {
                        //v.MachineMoldCheck = v.MachineMoldCheck;
                        //v.ItemCode = v.ItemCode;
                        //v.ItemName = v.ItemName;
                        //v.TopCategory = v.TopCategory;
                        //?????????????????v.ItemName1 = v.ItemName;
                        //v.ItemNameENG = v.ItemNameENG;
                        //v.ItemNameCHN = v.ItemNameCHN;
                        //v.MainCustomerCode = v.MainCustomerCode;
                        //v.Spec1 = v.Spec1;
                        //v.Spec2 = v.Spec2;
                        //v.Spec3 = v.Spec3;
                        //v.Spec4 = v.Spec4;
                        //v.SafeQty = v.SafeQty;
                        //v.Cost = v.Cost;
                        //v.Memo = v.Memo;
                        //v.StockPosition = v.StockPosition;

                        //v.UseFlag = "Y";
                        //v.EditRowFlag = v.EditRowFlag;


                        if (v.ProdFileUrl != null && (v.ProdFileUrl.Contains("\\") || v.ProdFileUrl == "Clipboard_Image"))      // 수정시 구분
                        {
                            string[] filename = v.ProdFileUrl.ToString().Split('\\');
                            if (filename.Length != 1)
                            {
                                var realFileName = v.ItemCode + "_" + filename[filename.Length - 1];
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;

                                FileHandler.UploadFTP(v.ProdFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                                v.ProdFileName = realFileName;
                                v.ProdFileUrl = ftpFileUrl;
                            }
                            else if (v.ProdFileUrl == "Clipboard_Image")
                            {
                                var realFileName = v.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;
                                var localImage = v.localImage as Image;
                                FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                                v.ProdFileName = realFileName;
                                v.ProdFileUrl = ftpFileUrl;
                            }
                        }

                        ModelService.Insert(v);

                    }
                    else if (v.EditRowFlag == "Y")
                    {
                        if (v.ProdFileUrl != null && (v.ProdFileUrl.Contains("\\") || v.ProdFileUrl == "Clipboard_Image"))      // 수정시 구분
                        {
                            string[] filename = v.ProdFileUrl.ToString().Split('\\');
                            if (filename.Length != 1)
                            {
                                var realFileName = v.ItemCode + "_" + filename[filename.Length - 1];
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;

                                FileHandler.UploadFTP(v.ProdFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                                v.ProdFileName = realFileName;
                                v.ProdFileUrl = ftpFileUrl;
                            }
                            else if (v.ProdFileUrl == "Clipboard_Image")
                            {
                                var realFileName = v.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;
                                var localImage = v.localImage as Image;
                                FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                                v.ProdFileName = realFileName;
                                v.ProdFileUrl = ftpFileUrl;
                            }
                        }
                        ModelService.Update(v);
                    }

                        //if (v.ProdFileUrl != null && (v.ProdFileUrl.Contains("\\") || v.ProdFileUrl == "Clipboard_Image"))      // 수정시 구분
                        //{
                        //    string[] filename = v.ProdFileUrl.ToString().Split('\\');
                        //    if (filename.Length != 1)
                        //    {
                        //        var realFileName = NewObj.ItemCode + "_" + filename[filename.Length - 1];
                        //        var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;

                        //        FileHandler.UploadFTP(v.ProdFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                        //        NewObj.ProdFileName = realFileName;
                        //        NewObj.ProdFileUrl = ftpFileUrl;
                        //    }
                        //    else if (v.ProdFileUrl == "Clipboard_Image")
                        //    {
                        //        var realFileName = NewObj.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        //        var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;
                        //        var localImage = v.localImage as Image;
                        //        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                        //        NewObj.ProdFileName = realFileName;
                        //        NewObj.ProdFileUrl = ftpFileUrl;
                        //    }
                        //}

                    
                }
            }
            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 셀 변경 시 Edit 체크를 위함.
        /// </summary>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = GridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            // 2021-05-27 김진우 주임        새로 생성된 행에 대해서 채번
            if (masterObj.NewRowFlag == "Y" && masterObj.ItemCode == null)
            {
                if (masterObj.MachineMoldCheck == "01")
                {
                    masterObj.ItemCode = DbRequestHandler.GetSeqStandard("SPARE");
                    masterObj.TopCategory = MasterCodeSTR.TopCategory_SPARE;
                }
                else if (masterObj.MachineMoldCheck == "02")
                {
                    masterObj.ItemCode = DbRequestHandler.GetSeqStandard("MSPARE");
                    masterObj.TopCategory = MasterCodeSTR.TopCategory_Mold;
                }
            }

            masterObj.EditRowFlag = "Y";
        }

        private void MainView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_STD1100 Obj = GridBindingSource.Current as TN_STD1100;
            GridView gv = sender as GridView;

            if (e.Column.Name.ToString() == "MachineMoldCheck")
            {
                if (Obj.MachineMoldCheck == null)
                    gv.Columns[0].OptionsColumn.AllowEdit = true;
                else
                    gv.Columns[0].OptionsColumn.AllowEdit = false;
            }
        }

        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = GridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

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

                        masterObj.localImage = fileData;
                        masterObj.ProdFileName = list[0];
                        masterObj.ProdFileUrl = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        masterObj.ProdFileName = "Clipboard_Image";
                        masterObj.ProdFileUrl = "Clipboard_Image";
                        masterObj.localImage = GetImage;
                    }
                }
                GridExControl.BestFitColumns();
                masterObj.EditRowFlag = "Y";
            }
        }

        /// <summary>
        /// 2021-06-07 김진우 주임
        /// 구분에서 선택시 수정불가하도록 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            TN_STD1100 Obj = GridBindingSource.Current as TN_STD1100;
            GridView gv = sender as GridView;


            if (Obj.MachineMoldCheck == null)
                gv.Columns[0].OptionsColumn.AllowEdit = true;
            else
                gv.Columns[0].OptionsColumn.AllowEdit = false;
        }

        /// <summary>
        /// 2021-06-07 김진우 주임
        /// 구분에서 선택시 수정불가하도록 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            TN_STD1100 Obj = GridBindingSource.Current as TN_STD1100;
            GridView gv = sender as GridView;

            if (e.Column.Name.ToString() == "MachineMoldCheck")
            {
                if (Obj.MachineMoldCheck == null)
                    gv.Columns[0].OptionsColumn.AllowEdit = true;
                else
                    gv.Columns[0].OptionsColumn.AllowEdit = false;
            }
        }
    }
}