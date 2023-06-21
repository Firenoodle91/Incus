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
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Ui.View.View.MEA
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
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
        }


        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"), false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MachineName"), LabelConvert.GetLabelText("MachineName"));
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
            MasterGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));        // 2021-05-21 김진우 주임 추가
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"), false);       // 2021-05-21 김진우 주임    안보이게 처리

            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineMCode"), false);
            DetailGridExControl.MainGrid.AddColumn("CheckSeq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, FormatType.Numeric, "n0");
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

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Temp", "Division", "CheckSeq", "CheckPosition", "CheckList", "CheckWay", "CheckCycle", "CheckStandardDate", "DisplayOrder", "Memo", "ManagementStandard");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1002>(DetailGridExControl);       // 2021-05-21 김진우 주임 추가

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Model", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineModel, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMaker, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "Y");
            //MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineImage, "FileName", "FileUrl", true);
            //MasterGridExControl.MainGrid.MainView.Columns["FileName2"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineCheckPoint, "FileName2", "FileUrl2");
            MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(MasterGridExControl, false, "FileData", "FileName");
            MasterGridExControl.MainGrid.MainView.Columns["FileName2"].ColumnEdit = new HKInc.Service.Controls.FileGridButtonEdit(MasterGridExControl, false, "FileData2", "FileName2");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineStandardCheckDivision), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckPosition), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckList), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckWay), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckCycle), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckStandardDate), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), true);
            DetailGridExControl.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "ManagementStandard", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineMCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var machineCode = lup_Machine.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(machineCode) ? true : p.MachineMCode == machineCode)
                                                                        && (p.UseFlag == "Y")
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
                MachineCode = masterObj.MachineMCode,
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
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("MachineStandardInfo"), LabelConvert.GetLabelText("MachineCheckInfo"), LabelConvert.GetLabelText("MachineCheckInfo")));
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
     //       int i = masterObj.TN_MEA1002List.Count;
            foreach (var v in returnList)
            {
                if (masterObj.TN_MEA1002List.Any(p => p.Division == v.Division && p.CheckPosition == v.CheckPosition && p.CheckList == v.CheckList))
                    continue;
                v.CheckSeq = masterObj.TN_MEA1002List.Count == 0 ? 1 : masterObj.TN_MEA1002List.Max(p => p.CheckSeq) + 1;// masterObj.TN_MEA1002List.OrderBy(o => o.CheckSeq).LastOrDefault().CheckSeq == 0 ? 1 : masterObj.TN_MEA1002List.OrderBy(o => o.CheckSeq).LastOrDefault().CheckSeq + 1;
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