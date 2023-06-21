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
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using HKInc.Service.Helper;
using System.Collections.Specialized;
using System.IO;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 제품별표준공정관리화면
    /// </summary>
    public partial class XFMPS1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFMPS1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"),
                ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" &&
                                                     (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            //MasterGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1")); // 20210824 오세완 차장 품목명 누락 추가
            MasterGridExControl.MainGrid.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("CombineSpec", LabelConvert.GetLabelText("Spec"));

            //IsDetailGridButtonFileChooseEnabled = UserRight.HasEdit;
            //DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("TypeAdd") +  "[Alt+R]", IconImageList.GetIconImage("spreadsheet/grouprows"));
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", LabelConvert.GetLabelText("ProcessSeq"));
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            DetailGridExControl.MainGrid.AddColumn("MachineGroupCode", LabelConvert.GetLabelText("MachineGroup"));
            DetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("WorkStandardDocument"));

            DetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            DetailGridExControl.MainGrid.AddColumn("OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"));

            //DetailGridExControl.MainGrid.AddColumn("JobSettingFlag", LabelConvert.GetLabelText("JobSettingFlag"));
            // 20210520 오세완 차장 작업설정검사여부를 재가동TO여부로 변경
            //DetailGridExControl.MainGrid.AddColumn("RestartToFlag", LabelConvert.GetLabelText("RestartToFlag")); // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략

            //금형일상점검여부
            DetailGridExControl.MainGrid.AddColumn("MoldDayInspFlag", LabelConvert.GetLabelText("MoldDayInspFlag"),false);


            // 20210520 오세완 차장 설비사용여부 및 툴사용여부 생략처리
            //DetailGridExControl.MainGrid.AddColumn("MachineFlag", LabelConvert.GetLabelText("MachineFlag"), false);
            //DetailGridExControl.MainGrid.AddColumn("ToolUseFlag", LabelConvert.GetLabelText("ToolUseFlag"));            
            DetailGridExControl.MainGrid.AddColumn("StdWorkDay", LabelConvert.GetLabelText("StdWorkDay"));
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessSeq", "MachineGroupCode", "FileName", "UseFlag", "OutProcFlag", "JobSettingFlag", "MachineFlag", "ToolUseFlag", "StdWorkDay");
            // 20210520 오세완 차장 작업설정검사여부를 재가동TO여부로 변경, 설비사용여부 및 툴사용여부 생략처리
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessSeq", "MachineGroupCode", "FileName", "UseFlag", "OutProcFlag", "RestartToFlag", "MoldDayInspFlag", "StdWorkDay");
            // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessSeq", "MachineGroupCode", "FileName", "UseFlag", "OutProcFlag", "MoldDayInspFlag", "StdWorkDay");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MPS1000>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ProcessSeq");
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_WorkStandardDocumentFile , "FileName", "FileUrl");
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProcFlag", "N");

            // 20210520 오세완 차장 작업설정검사여부를 재가동TO여부로 변경
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("RestartToFlag", "N"); // 20210628 오세완 차장 재가동TO를 작업지시에 설정하지 말라는 이사님 지시로 생략
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("JobSettingFlag", "N");

            //금형일상점검여부
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MoldDayInspFlag", "N");

            // 20210520 오세완 차장 설비사용여부 및 툴사용여부 생략처리
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MachineFlag", "N");
            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("ToolUseFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StdWorkDay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.StdWorkDay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();
            
            InitRepository();
            InitCombo();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                        && (p.UseFlag == "Y")
                                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).OrderBy(p => p.ItemCode).ToList(); // 20210520 오세완 차장 반제품 조건 추가
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            DetailGridBindingSource.DataSource = obj.TN_MPS1000List.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)).OrderBy(o => o.ProcessSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 공정순서 Check          
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_STD1100>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();

                if (editList.Count > 0)
                {
                    //if (MasterList.Where(p => p.TN_MPS1000List.Any(c => c.ProcessSeq == 1 && (c.ProcessCode != MasterCodeSTR.ProcessCode_Heat && c.ProcessCode != MasterCodeSTR.ProcessCode_Surface)) == true).Count() > 0)
                    //{
                    //    MessageBoxHandler.Show("1번째 공정은 필수로 열처리 또는 표면처리가 등록되어야 합니다.", "경고");
                    //    return;
                    //}

                    foreach (var v in editList)
                    {
                        int i = 1;
                        foreach (var c in v.TN_MPS1000List.Where(p => p.UseFlag == "Y").OrderBy(p => p.ProcessSeq).ToList())
                        {
                            if (c.ProcessSeq != i)
                            {
                                MessageBoxHandler.Show("공정순서는 차례대로 기입하셔야 합니다.");
                                return;
                            }
                            i++;
                        }
                    }

                    if (editList.Any(p => p.TN_MPS1000List.Where(c => c.UseFlag == "Y").GroupBy(c => c.ProcessCode).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_87), LabelConvert.GetLabelText("Process")));
                        //MessageBoxHandler.Show("공정은 중복될 수 없습니다.", "경고");
                        return;
                    }

                    if (editList.Any(p => p.TN_MPS1000List.Where(c => c.UseFlag == "Y").GroupBy(c => c.ProcessSeq).Where(c => c.Count() > 1).Count() > 0))
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_87), LabelConvert.GetLabelText("ProcessSeq")));
                        //MessageBoxHandler.Show("공정순서는 중복될 수 없습니다.", "경고");
                        return;
                    }

                    //if (MasterList.Where(p => !p.TN_MPS1000List.Any(c => c.ProcessCode == MasterCodeSTR.ProcessCode_Drawing) == true).Count() > 0)
                    //{
                    //    MessageBoxHandler.Show("인발 공정은 필수로 등록되어야 합니다.", "경고");
                    //    return;
                    //}

                    //if (editList.Where(p => !p.TN_MPS1000List.Any(c => c.ProcessCode == MasterCodeSTR.Process_Packing)).Count() > 0)
                    //{
                    //    MessageBoxHandler.Show("포장 공정은 필수로 등록되어야 합니다.", "경고");
                    //    return;
                    //}

                    //var CheckCnt2 = editList.Where(p => p.TN_MPS1000List.Where(c => c.ProcessCode == MasterCodeSTR.Process_Packing).First().ProcessSeq < p.TN_MPS1000List.Count).Count();
                    //if (CheckCnt2 != 0)
                    //{
                    //    MessageBoxHandler.Show("포장 공정은 마지막으로 등록되어야 합니다.", "경고");
                    //    return;
                    //}
                }

                #region 파일 CHECK
                foreach (var v in editList.Where(p => p.TN_MPS1000List.Any(c => c.FileUrl != null && (c.FileUrl.Contains("\\") || c.FileUrl == "Clipboard_Image"))).ToList())
                {
                    foreach (var d in v.TN_MPS1000List.Where(c => c.FileUrl != null && (c.FileUrl.Contains("\\") || c.FileUrl == "Clipboard_Image")).ToList())
                    {
                        string[] filename = d.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = d.ItemCode + "_" + filename[filename.Length - 1];
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/" + realFileName;

                            FileHandler.UploadFTP(d.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/");

                            d.FileName = realFileName;
                            d.FileUrl = ftpFileUrl;
                        }
                        else if (d.FileUrl == "Clipboard_Image")
                        {
                            var realFileName = d.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/" + realFileName;
                            var localImage = d.localImage as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/");

                            d.FileName = realFileName;
                            d.FileUrl = ftpFileUrl;
                        }
                    }
                }
                ////삭제할 파일 체크
                //foreach (var v in masterList.Where(p => p.TN_MPS1000List.Any(c => !c.DeleteFilePath.IsNullOrEmpty())).ToList())
                //{
                //    foreach (var d in v.TN_MPS1000List.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
                //    {
                //        try
                //        {
                //            FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
                //        }
                //        catch { }
                //    }
                //}

                ////업로드할 파일 체크
                //foreach (var v in masterList.Where(p => p.TN_MPS1000List.Any(c => !c.UploadFilePath.IsNullOrEmpty())).ToList())
                //{
                //    foreach (var d in v.TN_MPS1000List.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
                //    {
                //        try
                //        {
                //            FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/{3}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_WorkStandardDocumentFile, d.ItemCode, d.Seq));
                //            d.FileUrl = string.Format("{0}/{1}/{2}/{3}", MasterCodeSTR.FtpFolder_WorkStandardDocumentFile, d.ItemCode, d.Seq, d.FileName);
                //        }
                //        catch { }
                //    }
                //}
                #endregion
            }
            #endregion


            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var materObj = MasterGridBindingSource.Current as TN_STD1100;
            if (materObj == null) return;

            var newObj = new TN_MPS1000()
            {
                ItemCode = materObj.ItemCode,
                Seq = materObj.TN_MPS1000List.Count == 0 ? 1 : materObj.TN_MPS1000List.Max(c => c.Seq) + 1,
                UseFlag = "Y",
                ProcessSeq = materObj.TN_MPS1000List.Count == 0 ? 1 : materObj.TN_MPS1000List.Max(c => c.ProcessSeq) + 1,
                OutProcFlag = "N",
                //RestartToFlag = "N", // 20210520 오세완 차장 재가동TO여부로 변경
                //JobSettingFlag = "N",
                MachineFlag = "N",
                //ToolUseFlag = "N",
                StdWorkDay = "0"
            };
            materObj.EditRowFlag = "Y";

            DetailGridBindingSource.Add(newObj);
            materObj.TN_MPS1000List.Add(newObj);
            DetailGridExControl.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var materObj = MasterGridBindingSource.Current as TN_STD1100;
            var detailObj = DetailGridBindingSource.Current as TN_MPS1000;
            if (materObj == null || detailObj == null) return;

            var deleteCheck = ModelService.GetChildList<TN_MPS1200>(p => p.ItemCode == detailObj.ItemCode && p.ProcessCode == detailObj.ProcessCode).FirstOrDefault();
            if (deleteCheck != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("StandardProcessInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    detailObj.UseFlag = "N";
                    DetailGridExControl.BestFitColumns();
                }
            }
            else
            {
                materObj.EditRowFlag = "Y";
                DetailGridBindingSource.Remove(detailObj);
                materObj.TN_MPS1000List.Remove(detailObj);
                DetailGridExControl.BestFitColumns();
            }

            var detailList = DetailGridBindingSource.List as List<TN_MPS1000>;
            int i = 1;
            foreach (var v in detailList.Where(p => p.UseFlag == "Y").OrderBy(p => p.ProcessSeq).ThenBy(p => p.Seq).ToList())
            {
                v.ProcessSeq = i;
                i++;
            }
            DetailGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 디테일 셀 변경 시 마스터 Edit 체크를 위함.
        /// </summary>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_MPS1000;
            if (detailObj == null) return;

            masterObj.EditRowFlag = "Y";

            if (e.Column.FieldName == "MachineGroupCode")
            {
                if (e.Value.IsNullOrEmpty())
                    detailObj.MachineFlag = "N";
                else
                    detailObj.MachineFlag = "Y";
            }
            else if (e.Column.FieldName == "UseFlag")
            {
                var detailList = DetailGridBindingSource.List as List<TN_MPS1000>;
                int i = 1;
                foreach (var v in detailList.Where(p => p.UseFlag == "Y").OrderBy(p => p.ProcessSeq).ThenBy(p => p.Seq).ToList())
                {
                    v.ProcessSeq = i;
                    i++;
                }
                DetailGridExControl.BestFitColumns();
            }
        }

        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_MPS1000;
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

        protected override void DetailFileChooseClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XFSMPS1000, param, AddTypeCallBack);
            form.ShowPopup(true);
        }

        private void AddTypeCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            TN_STD1100 item = MasterGridBindingSource.Current as TN_STD1100;
            if (item == null) return;
            if (e == null) return;

            var typeCode = e.Map.GetValue(PopupParameter.ReturnObject).GetNullToEmpty();
            var processList = ModelService.GetChildList<TN_MPS1002>(p => p.TypeCode == typeCode).OrderBy(p => p.ProcessSeq).ToList();

            foreach (var v in processList)
            {
                var newObj = new TN_MPS1000()
                {
                    ItemCode = item.ItemCode,
                    Seq = item.TN_MPS1000List.Count == 0 ? 1 : item.TN_MPS1000List.Max(c => c.Seq) + 1,
                    UseFlag = "Y",
                    ProcessSeq = item.TN_MPS1000List.Count == 0 ? 1 : item.TN_MPS1000List.Max(c => c.ProcessSeq) + 1,
                    ProcessCode = v.ProcessCode,
                    OutProcFlag = v.OutProcFlag, // 20210520 오세완 차장 팝업에서 설정된 내용 받아올 수 있게 수정
                    //OutProcFlag = "N",
                    //RestartToFlag = v.RestartToFlag, // 20210520 오세완 차장 팝업에서 설정된 내용 받아올 수 있게 수정
                    //JobSettingFlag = "N",
                    MachineFlag = "N",
                    //ToolUseFlag = "N",
                    //StdWorkDay = "0"
                    StdWorkDay = v.StdWorkDay // 20210520 오세완 차장 팝업에서 설정된 내용 받아올 수 있게 수정
                };
                item.EditRowFlag = "Y";
                DetailGridBindingSource.Add(newObj);
                item.TN_MPS1000List.Add(newObj);
            }

            if (processList.Count > 0)
                SetIsFormControlChanged(true);

            DetailGridExControl.MainGrid.BestFitColumns();
        }
    }
}