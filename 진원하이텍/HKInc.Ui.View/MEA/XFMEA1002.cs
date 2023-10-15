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
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
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
        }

        protected override void InitCombo()
        {
            lup_Machine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y"));
        }


        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);            
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"), false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn("MachineName", LabelConvert.GetLabelText("MachineName"));
            MasterGridExControl.MainGrid.AddColumn("Model", LabelConvert.GetLabelText("Model"));
            MasterGridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));
            MasterGridExControl.MainGrid.AddColumn("InstallDate", LabelConvert.GetLabelText("InstallDate"));
            MasterGridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"));
            MasterGridExControl.MainGrid.AddColumn("CheckTurn", LabelConvert.GetLabelText("CheckTurn"));
            MasterGridExControl.MainGrid.AddColumn("NextCheckDate", LabelConvert.GetLabelText("NextCheckDate"));
            MasterGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName2", LabelConvert.GetLabelText("CheckPointFileName"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl2", LabelConvert.GetLabelText("CheckPointFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"), false);
            DetailGridExControl.MainGrid.AddColumn("CheckSeq", LabelConvert.GetLabelText("CheckSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            DetailGridExControl.MainGrid.AddColumn("CheckPosition", LabelConvert.GetLabelText("CheckPosition"));
            DetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("CheckList"));
            DetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("CheckWay"));
            DetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("EyeCheckFlag"));
            DetailGridExControl.MainGrid.AddColumn("CheckCycle", LabelConvert.GetLabelText("CheckCycle"));
            DetailGridExControl.MainGrid.AddColumn("CheckStandardDate", LabelConvert.GetLabelText("CheckStandardDate"));
            DetailGridExControl.MainGrid.AddColumn("ManagementStandard", LabelConvert.GetLabelText("ManagementStandard"));
            DetailGridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            var barButtonMachineCheckCopy = new DevExpress.XtraBars.BarButtonItem();
            barButtonMachineCheckCopy.Id = 4;
            barButtonMachineCheckCopy.ImageOptions.Image = IconImageList.GetIconImage("miscellaneous/wizard");
            barButtonMachineCheckCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.I));
            barButtonMachineCheckCopy.Name = "barButtonItemCopy";
            barButtonMachineCheckCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonMachineCheckCopy.ShortcutKeyDisplayString = "Alt+I";
            barButtonMachineCheckCopy.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonMachineCheckCopy.Caption = LabelConvert.GetLabelText("OtherMachineCheckCopy") + "[Alt+I]";
            barButtonMachineCheckCopy.ItemClick += BarButtonMachineCheckCopy_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonMachineCheckCopy);

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Temp", "Division", "CheckPosition", "CheckList", "CheckWay", "CheckCycle", "CheckStandardDate", "DisplayOrder", "Memo", "ManagementStandard");

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Model", DbRequesHandler.GetCommCode(MasterCodeSTR.MachineModel, 1), "CodeVal", "CodeName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequesHandler.GetCommCode(MasterCodeSTR.MCMAKER, 1), "CodeVal", "CodeName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", DbRequesHandler.GetCommCode(MasterCodeSTR.CHECKCYCLE, 1), "CodeVal", "CodeName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineImage, "FileName", "FileUrl", true);
            MasterGridExControl.MainGrid.MainView.Columns["FileName2"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineCheckPoint, "FileName2", "FileUrl2");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequesHandler.GetCommTopCode(MasterCodeSTR.MachineStandardCheckDivision), "CodeVal", "CodeName", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequesHandler.GetCommTopCode(MasterCodeSTR.MachineCheckPosition), "CodeVal", "CodeName", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequesHandler.GetCommTopCode(MasterCodeSTR.MachineCheckList), "CodeVal", "CodeName", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequesHandler.GetCommTopCode(MasterCodeSTR.MachineCheckWay), "CodeVal", "CodeName", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequesHandler.GetCommTopCode(MasterCodeSTR.CHECKCYCLE), "CodeVal", "CodeName", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequesHandler.GetCommTopCode(MasterCodeSTR.MachineCheckStandardDate), "CodeVal", "CodeName", true);
            DetailGridExControl.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "ManagementStandard", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

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
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_MEA1002;
            if (detailObj == null) return;

            if (detailObj.TN_MEA1003List.Count > 0)
            {
               // MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("MachineStandardInfo"), LabelConvert.GetLabelText("MachineCheckInfo"), LabelConvert.GetLabelText("MachineCheckInfo")));
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

                var newObj = new TN_MEA1002()
                {
                    MachineCode = masterObj.MachineCode,
                    CheckSeq = masterObj.TN_MEA1002List.Count == 0 ? 1 : masterObj.TN_MEA1002List.Max(p => p.CheckSeq) + 1,
                    DisplayOrder = masterObj.TN_MEA1002List.Count == 0 ? 1 : masterObj.TN_MEA1002List.Max(p => p.DisplayOrder) + 1,
                    Division = v.Division,
                    CheckPosition = v.CheckPosition,
                    CheckList = v.CheckList,
                    CheckWay = v.CheckWay,
                    CheckCycle = v.CheckCycle,
                    CheckStandardDate = v.CheckStandardDate,
                    ManagementStandard = v.ManagementStandard,
                    Memo = v.Memo,
                    Temp = v.Temp
                };
                DetailGridBindingSource.Add(newObj);
                masterObj.TN_MEA1002List.Add(newObj);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                DetailGridExControl.BestFitColumns();
            }
        }
    }
}