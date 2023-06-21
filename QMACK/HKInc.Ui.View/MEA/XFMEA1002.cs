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
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 설비점검기준관리
    /// </summary>
    public partial class XFMEA1002 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public XFMEA1002()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailView_ShowingEditor;
        }

        protected override void InitCombo()
        {
            lup_Machine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y"));
        }


        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비코드");
            MasterGridExControl.MainGrid.AddColumn("MachineName", "설비명");
            MasterGridExControl.MainGrid.AddColumn("ModelNo", "모델명");
            MasterGridExControl.MainGrid.AddColumn("Maker", "제조사");
            MasterGridExControl.MainGrid.AddColumn("InstallDate", "설치일");

            MasterGridExControl.MainGrid.AddColumn("SerialNo", "일련번호");
            MasterGridExControl.MainGrid.AddColumn("CheckTurn", "점검주기");
            MasterGridExControl.MainGrid.AddColumn("NextCheckDate", "점검예정일");
            MasterGridExControl.MainGrid.AddColumn("FileName", "파일명");
            MasterGridExControl.MainGrid.AddColumn("FileUrl", false);

            MasterGridExControl.MainGrid.AddColumn("FileName2", "점검포인트파일명");
            MasterGridExControl.MainGrid.AddColumn("FileUrl2", false);
            MasterGridExControl.MainGrid.AddColumn("UseFlag", "사용여부");

            DetailGridExControl.MainGrid.AddColumn("CheckSeq", false);
            DetailGridExControl.MainGrid.AddColumn("Division", "구분");
            DetailGridExControl.MainGrid.AddColumn("CheckPosition", "점검위치");
            DetailGridExControl.MainGrid.AddColumn("CheckList", "점검항목");
            DetailGridExControl.MainGrid.AddColumn("CheckWay", "점검방법");

            DetailGridExControl.MainGrid.AddColumn("Temp", "육안검사여부");
            DetailGridExControl.MainGrid.AddColumn("CheckCycle", "점검주기");
            DetailGridExControl.MainGrid.AddColumn("CheckStandardDate", "점검기준일");
            DetailGridExControl.MainGrid.AddColumn("ManagementStandard", "관리기준");
            DetailGridExControl.MainGrid.AddColumn("DisplayOrder", "표시순서");

            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");

            var barButtonMachineCheckCopy = new DevExpress.XtraBars.BarButtonItem();
            barButtonMachineCheckCopy.Id = 4;
            barButtonMachineCheckCopy.ImageOptions.Image = IconImageList.GetIconImage(0);
            barButtonMachineCheckCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.I));
            barButtonMachineCheckCopy.Name = "barButtonItemCopy";
            barButtonMachineCheckCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonMachineCheckCopy.ShortcutKeyDisplayString = "Alt+I";
            barButtonMachineCheckCopy.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonMachineCheckCopy.Caption = "다른 설비점검기준 복사[Alt+I]";
            barButtonMachineCheckCopy.ItemClick += BarButtonMachineCheckCopy_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonMachineCheckCopy);

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Temp", "Division", "CheckSeq", "CheckPosition", "CheckList", "CheckWay", "CheckCycle", "CheckStandardDate", "DisplayOrder", "Memo", "ManagementStandard");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1002>(DetailGridExControl);       // 2021-05-21 김진우 주임 추가

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MCMAKER), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "Y");
            MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineImage, "FileName", "FileUrl", true);

            MasterGridExControl.MainGrid.MainView.Columns["FileName2"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineCheckPoint, "FileName2", "FileUrl2");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineStandardCheckDivision), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckPosition), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckList), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckWay), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckStandardDate), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var machineCode = lup_Machine.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(machineCode) ? true : p.MachineCode == machineCode)
                                                                        && (p.UseYn == "Y")
                                                                        && (p.DailyCheckFlag == "Y")
                                                               )
                                                               .OrderBy(p => p.MachineName)
                                                               .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MEA1002List.OrderBy(p => p.DisplayOrder).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null) return;

            var newObj = new TN_MEA1002()
            {
                MachineCode = masterObj.MachineCode,
                CheckSeq = masterObj.TN_MEA1002List.Count == 0 ? 1 : masterObj.TN_MEA1002List.Max(p => p.CheckSeq) + 1,
                DisplayOrder = masterObj.TN_MEA1002List.Count == 0 ? 1 : masterObj.TN_MEA1002List.Max(p => p.DisplayOrder) + 1,
                Temp = "N",
            };
            masterObj.TN_MEA1002List.Add(newObj);
            DetailGridBindingSource.Add(newObj);
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null)
                return;

            var detailObj = DetailGridBindingSource.Current as TN_MEA1002;
            if (detailObj == null)
                return;

            if (detailObj.TN_MEA1003List.Count > 0)
            {
                string sMessage = "점검기준정보에 점검정보가 존재합니다. 삭제를 하기위해서는 먼저 점검정보를 삭제해 주십시오.";
                MessageBoxHandler.Show(sMessage);
                return;
            }

            masterObj.TN_MEA1002List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
        }

        /// <summary>
        /// 다른설비점검 복사
        /// </summary>
        private void BarButtonMachineCheckCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null)
                return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_MACHINE_CHECK_COPY, param, MachineCopyPopupCallback);

            form.ShowPopup(true);
        }

        private void MachineCopyPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<TN_MEA1002>)e.Map.GetValue(PopupParameter.ReturnObject);

            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null)
                return;
     
            foreach (var v in returnList)
            {
                if (masterObj.TN_MEA1002List.Any(p => p.Division == v.Division && p.CheckPosition == v.CheckPosition && p.CheckList == v.CheckList))
                    continue;

                v.CheckSeq = masterObj.TN_MEA1002List.Count == 0 ? 1 : masterObj.TN_MEA1002List.Max(p => p.CheckSeq) + 1;
                v.DisplayOrder = masterObj.TN_MEA1002List.Count == 0 ? 1 : masterObj.TN_MEA1002List.Max(p => p.DisplayOrder) + 1;
                // 2021-05-21 김진우 주임         기존에 팝업에서 MEA1000의 형태로 가져와서 MEA1002의 형태로 변경
                DetailGridBindingSource.Add(v);
                masterObj.TN_MEA1002List.Add(v);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                DetailGridExControl.BestFitColumns();
            }
        }

        /// <summary>
        /// 2021-06-25 김진우 주임 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            TN_MEA1002 DetailObj = DetailGridBindingSource.Current as TN_MEA1002;

            if (e.Column.FieldName == "CheckWay")
            {
                if (DetailObj.CheckWay == "01")
                    DetailObj.Temp = "Y";
                else
                    DetailObj.Temp = "N";
            }
        }

        /// <summary>
        /// 2021-06-30 김진우 주임 추가
        /// 점검방법이 육안일경우 육안검사여부 수정 안되게 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;
            TN_MEA1002 DetailObj = DetailGridBindingSource.Current as TN_MEA1002;
            if (DetailObj == null) return;

            if (gv.FocusedColumn.FieldName == "Temp")
            {
                if (DetailObj.CheckWay == "01")
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
         
        }
    }
}