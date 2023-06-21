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
using System.IO;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.TOOL
{
    /// <summary>
    /// 툴 기준정보관리
    /// </summary>
    public partial class XFTOOL1000 : Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFTOOL1000()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ToolCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ToolName"));
            GridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("ToolNameENG"));
            GridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("ToolNameCHN"));
            GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            GridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            GridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            GridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            GridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            GridExControl.MainGrid.AddColumn("ToolStockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"));
            GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition"));
            GridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"));
            GridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            GridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            GridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemName", "ItemNameENG", "ItemNameCHN", "MainCustomerCode", "Spec1", "Spec2", "Spec3", "Spec4", "SafeQty", "StockPosition", "ProdFileName", "UseFlag", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1100>(GridExControl);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StockPosition", ModelService.GetChildList<TN_WMS2000>(p => true).ToList(), "PositionCode", Service.Helper.DataConvert.GetCultureDataFieldName("PositionName"),true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl", true);
            GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            //GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, GridExControl, "ProdFileName", "ProdFileUrl", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            GridExControl.BestFitColumns();
        }
        //lup_StockPosition.SetDefault(true, "PositionCode", "PositionName", Std1100Service.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList());
        //    lup_StockPosition.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
        //    {
        //        FieldName = "TN_WMS1000." + DataConvert.GetCultureDataFieldName("WhName"),
        //        Caption = LabelConvert.GetLabelText(DataConvert.GetCultureDataFieldName("WhName")),
        //        Visible = true
        //    });
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            var toolCodeName = tx_ToolCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p =>    (string.IsNullOrEmpty(toolCodeName) ? true : (p.ItemCode.Contains(toolCodeName) || (p.ItemName.Contains(toolCodeName)) || p.ItemNameENG.Contains(toolCodeName) || p.ItemNameCHN.Contains(toolCodeName)))
                                                                    && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    && (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)
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
                ItemCode = DbRequestHandler.GetSeqStandard("TOOL"),
                TopCategory = MasterCodeSTR.TopCategory_TOOL,
                UseFlag = "Y",
            };
            ModelService.Insert(newObj);
            GridBindingSource.Add(newObj);
            GridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_STD1100;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("ToolInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();

            #region FTP 업로드 체크
            if (GridBindingSource != null && GridBindingSource.DataSource != null)
            {
                var masterList = GridBindingSource.List as List<TN_STD1100>;
                if (masterList.Count > 0)
                {
                    foreach (var v in masterList.Where(c => c.ProdFileUrl != null && (c.ProdFileUrl.Contains("\\") || c.ProdFileUrl == "Clipboard_Image")).ToList())
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
                    //foreach (var d in masterList.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
                    //    }
                    //    catch { }
                    //}

                    //foreach (var d in masterList.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_ProdImage, d.ItemCode));
                    //        d.ProdFileUrl = string.Format("{0}/{1}/{2}", MasterCodeSTR.FtpFolder_ProdImage, d.ItemCode, d.ProdFileName);
                    //    }
                    //    catch { }
                    //}
                }
            }
            #endregion

            ModelService.Save();
            DataLoad();
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
    }
}