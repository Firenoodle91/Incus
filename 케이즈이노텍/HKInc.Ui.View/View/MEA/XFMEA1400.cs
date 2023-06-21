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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using System.Collections.Specialized;
using System.IO;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 금형관리
    /// </summary>
    public partial class XFMEA1400 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1400> ModelService = (IService<TN_MEA1400>)ProductionFactory.GetDomainService("TN_MEA1400");

        public XFMEA1400()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var masterObj = GridBindingSource.Current as TN_MEA1400;
            if (masterObj == null) return;

            masterObj.EditRowFlag = "Y";

            if (e.Column.FieldName == "StPostion1")
            {
                masterObj.StPostion2 = null;
            }
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow,true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddColumn("MoldCode", LabelConvert.GetLabelText("MoldCode"));
            GridExControl.MainGrid.AddColumn("MoldName", LabelConvert.GetLabelText("MoldName"));
            GridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("MoldMakeCust", LabelConvert.GetLabelText("MoldMakeCust"));
            GridExControl.MainGrid.AddColumn("InputDt", LabelConvert.GetLabelText("InputDt"));            
            GridExControl.MainGrid.AddColumn("XCase", LabelConvert.GetLabelText("Cavity"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("StPostion1", LabelConvert.GetLabelText("StPostion1"));
            GridExControl.MainGrid.AddColumn("StPostion2", LabelConvert.GetLabelText("StPostion2"));
            GridExControl.MainGrid.AddColumn("StPostion3", LabelConvert.GetLabelText("StPostion3"), false);
            GridExControl.MainGrid.AddColumn("StdShotCnt", LabelConvert.GetLabelText("StdShotCnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("NextCheckDate", LabelConvert.GetLabelText("NextInspectDate"));
            GridExControl.MainGrid.AddColumn("ImgFile", LabelConvert.GetLabelText("FileName"));
            GridExControl.MainGrid.AddColumn("ImgUrl", LabelConvert.GetLabelText("Image"), false);
            GridExControl.MainGrid.AddColumn("UseYN", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MoldName", "Spec", "MoldMakeCust", "NextCheckDate", "InputDt", "XCase", "StPostion1", "StPostion2", "StPostion3", "StdShotCnt","Memo", "UseYN");
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("InputDt");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheckDate");        
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakeCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");

            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.MainGrid.MainView.Columns["ImgFile"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_MachineImage, "ImgFile", "ImgUrl");
            GridExControl.MainGrid.MainView.Columns["ImgFile"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StPostion1", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", "WhName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StPostion2", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYN");

            var StPostion2Edit = GridExControl.MainGrid.Columns["StPostion2"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            StPostion2Edit.Popup += StPostion2Edit_Popup;

        }
        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {          
            var detailObj = GridBindingSource.Current as TN_MEA1400;
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
                        detailObj.ImgFile = list[0];
                        detailObj.ImgUrl = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        detailObj.ImgFile = "Clipboard_Image";
                        detailObj.ImgUrl = "Clipboard_Image";
                        detailObj.localImage = GetImage;
                    }
                }
                GridExControl.BestFitColumns();
                detailObj.EditRowFlag = "Y";
                
            }
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MoldCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();               

            var instrCCodeName = tx_MoldCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
         
            GridBindingSource.DataSource = ModelService.GetList(p =>    ((p.MoldCode.Contains(instrCCodeName) || (p.MoldName == instrCCodeName))
                                                                    &&  (radioValue == "A" ? true : p.UseYN == radioValue)
                                                                  
                                                               ))
                                                               .OrderBy(p => p.MoldCode)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            #region 파일 CHECK
            if (GridBindingSource != null)
            {
                var masterList =GridBindingSource.List as List<TN_MEA1400>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(p => p.ImgUrl != null && (p.ImgUrl.Contains("\\") || p.ImgUrl == "Clipboard_Image")).ToList())
                    {
                        
                        string[] filename = v.ImgUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = v.MoldCode + "_" + filename[filename.Length - 1];
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineImage + "/" + realFileName;

                            FileHandler.UploadFTP(v.ImgUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineImage + "/");

                            v.ImgFile = realFileName;
                            v.ImgUrl = ftpFileUrl;
                        }
                        else if (v.ImgUrl == "Clipboard_Image")
                        {
                            var realFileName = v.MoldCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineImage + "/" + realFileName;
                            var localImage = v.localImage as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineImage + "/");

                            v.ImgFile = realFileName;
                            v.ImgUrl = ftpFileUrl;
                        }
                       
                    }             
                }
            }
            #endregion

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_MEA1400;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("MoldCode")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseYN = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override void AddRowClicked()
        {
            var newobj = new TN_MEA1400();
            newobj.MoldCode= DbRequestHandler.GetSeqStandard("Mold");
            newobj.UseYN = "Y";
            newobj.EditRowFlag = "Y";
            ModelService.Insert(newobj);
            GridBindingSource.Add(newobj);
            GridExControl.MainGrid.BestFitColumns();
        }

        private void StPostion2Edit_Popup(object sender, EventArgs e)
        {
            var obj = GridBindingSource.Current as TN_MEA1400;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (obj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + obj.StPostion1 + "'";
        }
    }
}