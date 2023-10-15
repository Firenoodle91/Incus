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
    public partial class XFUPH1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public XFUPH1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged; 
        }

       

        protected override void InitCombo()
        {
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
        }


        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);            
            MasterGridExControl.MainGrid.AddColumn("MachineMCode", LabelConvert.GetLabelText("MachineMCode"));
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MachineName"), LabelConvert.GetLabelText("MachineName"));
            MasterGridExControl.MainGrid.AddColumn("Model", LabelConvert.GetLabelText("Model"));
            MasterGridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));
            MasterGridExControl.MainGrid.AddColumn("InstallDate", LabelConvert.GetLabelText("InstallDate"),false);
            MasterGridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"), false);
            MasterGridExControl.MainGrid.AddColumn("CheckTurn", LabelConvert.GetLabelText("CheckTurn"), false);
            MasterGridExControl.MainGrid.AddColumn("NextCheckDate", LabelConvert.GetLabelText("NextCheckDate"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("FileName2", LabelConvert.GetLabelText("CheckPointFileName"), false);
            MasterGridExControl.MainGrid.AddColumn("FileUrl2", LabelConvert.GetLabelText("CheckPointFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineMCode"), false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemName1", LabelConvert.GetLabelText("ItemName1"));

            DetailGridExControl.MainGrid.AddColumn("Uph", LabelConvert.GetLabelText("Uph"), HorzAlignment.Far, FormatType.Numeric, "n0");
            

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

            barButtonMachineCheckCopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            DetailGridExControl.BarTools.AddItem(barButtonMachineCheckCopy);

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "Uph");

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Model", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineModel, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMaker, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");
            MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineImage, "FileName", "FileUrl", true);
            MasterGridExControl.MainGrid.MainView.Columns["FileName2"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MachineCheckPoint, "FileName2", "FileUrl2");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");

            DetailGridExControl.MainGrid.SetRepositoryItemCodeLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN) && p.UseFlag == "Y").ToList());  // 2021-07-15 김진우 주임 SetRepositoryItemSearchLookUpEdit 에서 SetRepositoryItemCodeLookUpEdit 로 변경

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

            var machineMCode = lup_Machine.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(machineMCode) ? true : p.MachineMCode == machineMCode)
                                                                        && (p.UseFlag == "Y")
                                                                    
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

            DetailGridBindingSource.DataSource = masterObj.TN_UPH1000List.ToList();//ModelService.GetChildList<TN_UPH1000>(p => p.MachineMCode == masterObj.MachineMCode).ToList();
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

            var newObj = new TN_UPH1000()
            {
                MachineMCode = masterObj.MachineMCode,
              
             
            };
            masterObj.TN_UPH1000List.Add(newObj);
            //ModelService.InsertChild<TN_UPH1000>(newObj);            
            DetailGridBindingSource.Add(newObj);
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_MEA1000;
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_UPH1000;
            if (detailObj == null) return;


            masterObj.TN_UPH1000List.Remove(detailObj);
            // ModelService.RemoveChild<TN_UPH1000>(detailObj);
            DetailGridBindingSource.RemoveCurrent();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            var detailObj =  DetailGridBindingSource.Current as TN_UPH1000;
            if (detailObj == null) return;

            if (e.Column.FieldName == "ItemCode")
            {
                TN_STD1100 iteminfo = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == detailObj.ItemCode).FirstOrDefault();

                detailObj.TN_STD1100 = iteminfo;

                DetailGridExControl.MainGrid.BestFitColumns();
            }
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
                    MachineCode = masterObj.MachineMCode,
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