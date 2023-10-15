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
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using System.Collections.Specialized;
using HKInc.Service.Helper;
using System.IO;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 20220318 오세완 차장 
    /// 제품별표준공정관리 
    /// 이미지 저장 ftp변경 외 저장 로직 개선
    /// </summary>
    public partial class XFMPS1000_V2 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1000> ModelService = (IService<TN_MPS1000>)ProductionFactory.GetDomainService("TN_MPS1000");
        #endregion

        public XFMPS1000_V2()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            TN_MPS1000 detailObj = DetailGridBindingSource.Current as TN_MPS1000;
            if (detailObj == null)
                return;

            masterObj.EditRowFlag = "Y";

            if(e.Column.FieldName == "UseYn")
            {
                List<TN_MPS1000> tempArr = DetailGridBindingSource.List as List<TN_MPS1000>;
                if(tempArr != null)
                    if(tempArr.Count > 0)
                    {
                        tempArr = tempArr.Where(p => p.UseYn == "Y").OrderBy(o => o.ProcessSeq).ToList();
                        int iSeq = 1;
                        foreach(TN_MPS1000 each in tempArr)
                        {
                            each.ProcessSeq = iSeq;
                            iSeq++;
                        }
                    }
            }
        }

        protected override void InitCombo()
        {
            // 20220318 오세완 차장 완제품만 구성하면 되어서 처리 
            List<TN_STD1000> std_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", "");
            std_Arr = std_Arr.Where(p => p.Mcode == MasterCodeSTR.Topcategory_Final_Product).ToList();

            lupItemtype.SetDefault(true, "Mcode", "Codename", std_Arr);
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");

            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("Lctype", "기종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");

            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            MasterGridExControl.MainGrid.AddColumn("Memo");

            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "표준공정타입");

            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("ProcessSeq", "공정순서");
            DetailGridExControl.MainGrid.AddColumn("MachineGroupCode", "설비그룹");
            DetailGridExControl.MainGrid.AddColumn("WorkStantadnm", "작업표준서");
            DetailGridExControl.MainGrid.AddColumn("WorkStandardUrl", false);
            DetailGridExControl.MainGrid.AddColumn("OutProc", "외주여부");

            DetailGridExControl.MainGrid.AddColumn("STD", "표준작업소요일");
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessSeq", "OutProc", "WorkStantadnm", "UseYn", "STD", "MachineGroupCode");
        }
        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.MainView.Columns["WorkStantadnm"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_WorkStandardDocumentFile, "WorkStantadnm", "WorkStandardUrl");
            DetailGridExControl.MainGrid.MainView.Columns["WorkStantadnm"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UseYn", DbRequestHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProc", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("STD", DbRequestHandler.GetCommCode(MasterCodeSTR.STD), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineGroup), "Mcode", "Codename");
        }

        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            TN_MPS1000 detailObj = DetailGridBindingSource.Current as TN_MPS1000;
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

                        detailObj.localImage = fileData;
                        detailObj.WorkStantadnm = list[0];
                        detailObj.WorkStandardUrl = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        detailObj.WorkStantadnm = "Clipboard_Image";
                        detailObj.WorkStandardUrl = "Clipboard_Image";
                        detailObj.localImage = GetImage;
                    }
                }

                DetailGridExControl.BestFitColumns();
                detailObj.EditRowFlag = "Y";
                masterObj.EditRowFlag = "Y";
            }
        }

        protected override void DataLoad()
        {
            #region Grid Focus 가져오기
            GridRowLocator.GetCurrentRow("ItemCode");
            #endregion
            MasterGridExControl.MainGrid.Clear();       // 2022-03-14 김진우 주석 제거
            ModelService.ReLoad();

            string cta = lupItemtype.EditValue.GetNullToEmpty();
            string itemcode = tx_item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1100>(p => (string.IsNullOrEmpty(itemcode) ? true : (p.ItemCode.Contains(itemcode) || p.ItemNm.Contains(itemcode) || p.ItemNm1.Contains(itemcode))) && 
                                                                                            (string.IsNullOrEmpty(cta) ? p.TopCategory != "P03" : p.TopCategory == cta) && 
                                                                                            p.UseYn == "Y").OrderBy(p => p.ItemNm1).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            #region grid focus 설정
            GridRowLocator.SetCurrentRow();
            #endregion
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null)
                return;

            DetailGridExControl.MainGrid.Clear();
            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == obj.ItemCode).OrderBy(o => o.ProcessSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            SetRefreshMessage(DetailGridExControl.MainGrid.RecordCount);
        }

        protected override void DeleteDetailRow()
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            TN_MPS1000 detailObj = DetailGridBindingSource.Current as TN_MPS1000;
            if (detailObj == null)
                return;

            List<TN_MPS1400> work_Arr = ModelService.GetChildList<TN_MPS1400>(p => p.ItemCode == detailObj.ItemCode &&
                                                                                   p.Process == detailObj.ProcessCode);
            bool bDelete = false;
            if (work_Arr == null)
                bDelete = true;
            else if (work_Arr.Count == 0)
                bDelete = true;

            if(bDelete)
            {
                masterObj.EditRowFlag = "Y";
                DetailGridBindingSource.Remove(detailObj);
                ModelService.Delete(detailObj);
            }
            else
            {
                DialogResult dr = MessageBoxHandler.Show("표준공정정보의 삭제는 시스템 전체에 영향을 미치므로 삭제가 불가합니다. 사용여부 해제를 하시겠습니까? ", "경고", MessageBoxButtons.YesNo);
                if(dr == DialogResult.OK)
                    detailObj.UseYn = "N";
            }

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1100 masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
                return;

            List<TN_MPS1000> DetailObj = DetailGridBindingSource.DataSource as List<TN_MPS1000>;                        // 2022-03-14 김진우 추가

            TN_MPS1000 newobj = new TN_MPS1000()
            {
                ItemCode = masterObj.ItemCode,
                ProcessSeq = DetailObj.Max(p => p.ProcessSeq) == null ? 1 : DetailObj.Max(p => p.ProcessSeq + 1),       // 2022-03-14 김진우 추가    채번
                UseYn = "Y",
                STD = "1"
            };

            masterObj.EditRowFlag = "Y";
            DetailGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            DetailGridExControl.MainGrid.BestFitColumns();
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
            if (e == null)
                return;

            TN_STD1100 item = MasterGridBindingSource.Current as TN_STD1100;
            if (item == null)
                return;
            
            var typeCode = e.Map.GetValue(PopupParameter.ReturnObject).GetNullToEmpty();
            var processList = ModelService.GetChildList<TN_MPS1011>(p => p.TypeCode == typeCode).OrderBy(p => p.ProcessSeq).ToList();

            foreach (var v in processList)
            {
                var newObj = new TN_MPS1000()
                {
                    ItemCode = item.ItemCode,
                    ProcessSeq = v.ProcessSeq,
                    ProcessCode = v.ProcessCode,
                    OutProc = v.OutProcFlag, 
                    STD = v.StdWorkDay,
                    MachineGroupCode = v.MachineGroupCode,
                    UseYn = "Y",
                    NewRowFlag = "Y"
                };
                item.EditRowFlag = "Y";
                DetailGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);
            }

            if (processList.Count > 0)
                SetIsFormControlChanged(true);

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 2021-12-21 김진우 주임
        /// 기존 오류 수정
        /// </summary>
        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 데이터 검증
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_STD1100>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();

                bool bBreak = false;
                if (editList.Count > 0)
                {
                    #region 공정순서 순차
                    foreach (TN_STD1100 each in editList)
                    {
                        int iTemp_ProcessSeq = 1;
                        List<TN_MPS1000> tempArr = DetailGridBindingSource.List as List<TN_MPS1000>;
                        if (tempArr != null)
                            if (tempArr.Count > 0)
                                tempArr = tempArr.Where(p => p.ItemCode == each.ItemCode &&
                                                             p.UseYn == "Y").ToList();

                        if (tempArr != null)
                            if(tempArr.Count > 0)
                            {
                                tempArr = tempArr.OrderBy(o => o.ProcessSeq).ToList();
                                foreach (TN_MPS1000 each_Det in tempArr)
                                {
                                    if (each_Det.ProcessSeq != iTemp_ProcessSeq)
                                        bBreak = true;

                                    if (bBreak)
                                        break;
                                    else
                                        iTemp_ProcessSeq++;
                                }
                            }

                        if (bBreak)
                            break;
                    }

                    if (bBreak)
                    {
                        MessageBoxHandler.Show("공정순서는 차례대로 기입하셔야 합니다.", "경고");
                        return;
                    }
                    #endregion

                    #region 공정코드 중복
                    foreach (TN_STD1100 each in editList)
                    {
                        List<TN_MPS1000> tempArr = DetailGridBindingSource.List as List<TN_MPS1000>;
                        if (tempArr != null)
                            if (tempArr.Count > 0)
                                tempArr = tempArr.Where(p => p.ItemCode == each.ItemCode &&
                                                             p.UseYn == "Y").ToList();

                        if (tempArr != null)
                            if (tempArr.Count > 0)
                            {
                                if (tempArr.Where(p => p.UseYn == "Y").GroupBy(g => g.ProcessCode).Where(c => c.Count() > 1).Count() > 0)
                                    bBreak = true;
                            }

                        if (bBreak)
                            break;
                    }

                    if (bBreak)
                    {
                        MessageBoxHandler.Show("공정은 중복될 수 없습니다.", "경고");
                        return;
                    }
                    #endregion

                    #region 공정순서 중복
                    foreach (TN_STD1100 each in editList)
                    {
                        List<TN_MPS1000> tempArr = DetailGridBindingSource.List as List<TN_MPS1000>;
                        if (tempArr != null)
                            if (tempArr.Count > 0)
                                tempArr = tempArr.Where(p => p.ItemCode == each.ItemCode &&
                                                             p.UseYn == "Y").ToList();

                        if (tempArr != null)
                            if (tempArr.Count > 0)
                            {
                                if (tempArr.Where(p => p.UseYn == "Y").GroupBy(g => g.ProcessSeq).Where(c => c.Count() > 1).Count() > 0)
                                    bBreak = true;
                            }

                        if (bBreak)
                            break;
                    }

                    if (bBreak)
                    {
                        MessageBoxHandler.Show("공정순서는 중복될 수 없습니다.", "경고");
                        return;
                    }
                    #endregion

                    #region ftp file upload
                    foreach (TN_STD1100 each in editList)
                    {
                        List<TN_MPS1000> tempArr = DetailGridBindingSource.List as List<TN_MPS1000>;
                        if (tempArr != null)
                            if (tempArr.Count > 0)
                                tempArr = tempArr.Where(p => p.ItemCode == each.ItemCode &&
                                                             p.WorkStandardUrl != null &&
                                                             (p.WorkStandardUrl.Contains("\\") || p.WorkStandardUrl == "Clipboard_Image") &&
                                                             p.UseYn == "Y").ToList();
                        if (tempArr != null)
                            if (tempArr.Count > 0)
                            {
                                foreach(TN_MPS1000 each_Det in tempArr)
                                {
                                    string[] sFile_Location = each_Det.WorkStandardUrl.Split('\\');
                                    if(sFile_Location.Length != 1)
                                    {
                                        string sConvert_Filename = each_Det.ItemCode + "_" + sFile_Location[sFile_Location.Length - 1];
                                        sConvert_Filename = FileHandler.CheckFileName(sConvert_Filename);
                                        string sFtp_File_Url = MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/" + sConvert_Filename;

                                        FileHandler.UploadFTP(each_Det.WorkStandardUrl, sConvert_Filename, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/");

                                        each_Det.WorkStandardUrl = sFtp_File_Url;
                                        each_Det.WorkStantadnm = sConvert_Filename;
                                    }
                                    else if(each_Det.WorkStandardUrl == "Clipboard_Image")
                                    {
                                        string sClipboard_Filename = each_Det.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                                        sClipboard_Filename = FileHandler.CheckFileName(sClipboard_Filename);
                                        string sFtp_File_Url_Clip = MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/" + sClipboard_Filename;
                                        var vLocal_Image = each_Det.localImage as Image;

                                        FileHandler.UploadFTP(vLocal_Image, sFtp_File_Url_Clip, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/");

                                        each_Det.WorkStandardUrl = sFtp_File_Url_Clip;
                                        each_Det.WorkStantadnm = sClipboard_Filename;
                                    }
                                }
                            }
                    }
                    #endregion
                }
            }
            #endregion

            ModelService.Save();
            DataLoad();
        }
       
    }
    
}
