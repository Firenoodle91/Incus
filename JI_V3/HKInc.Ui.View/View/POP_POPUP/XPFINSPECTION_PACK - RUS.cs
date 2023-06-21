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
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using DevExpress.XtraEditors.Controls;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Service.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 출하검사 포장POP 팝업 창
    /// </summary>
    public partial class XPFINSPECTION_PACK_RUS : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1001> ModelService = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");
        IService<TN_QCT1100> QcModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        TEMP_XFPOP_PACK TEMP_XFPOP_PACK;
        string ProductLotNo;
        int rowid;

        public XPFINSPECTION_PACK_RUS(TEMP_XFPOP_PACK obj, string productLotNo)
        {
            InitializeComponent();

            this.Text = LabelConvert.GetLabelText("ShipmentInsp");
            
            GridExControl = gridEx1;

            TEMP_XFPOP_PACK = obj;
            ProductLotNo = productLotNo;

            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            tx_Reading.KeyDown += Tx_Reading_KeyDown;
            tx_Reading.GotFocus += Tx_Reading_GotFocus;

            lup_ReadingNumber.EditValueChanged += Lup_ReadingNumber_EditValueChanged;

            btn_Apply.Click += Btn_Apply_Click;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            lup_Eye.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), TextEditStyles.DisableTextEditor);
            lup_ReadingNumber.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), TextEditStyles.DisableTextEditor);
            //lup_CheckId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p=>p.Active=="Y").ToList(), TextEditStyles.DisableTextEditor);
            var procTeamCode = TEMP_XFPOP_PACK.ProcTeamCode.GetNullToNull();
            lup_CheckId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => (string.IsNullOrEmpty(procTeamCode) ? true : p.ProductTeamCode == procTeamCode)
                                                                                                                                                        && p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_CheckId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_ReadingNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_Eye.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            btn_Apply.Text = LabelConvert.GetLabelText("Apply") + "(&Y)";

            lup_CheckId.EditValue = GlobalVariable.LoginId;
            lup_CheckId.ReadOnly = true;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            //GridExControl.SetToolbarButtonVisible(false);
            //GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("Apply") + "[F3]", IconImageList.GetIconImage("actions/apply"));

            GridExControl.MainGrid.MainView.OptionsView.ShowIndicator = false;

            GridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            GridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            GridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);
            GridExControl.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);           
            GridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), HorzAlignment.Center, false);
            GridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), HorzAlignment.Center, false);
            GridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), HorzAlignment.Center, false);
            GridExControl.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            GridExControl.BestFitColumns();
        }

        protected override void GridRowDoubleClicked() { }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            IsFirstLoaded = true;
            //var TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).First();

            //if (TN_STD1100.ShipmentInspFlag != "Y")
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_67), LabelConvert.GetLabelText("ShipmentInspFlag")));
            //    ActClose();
            //    return;
            //}

            GridExControl.MainGrid.Clear();

            rowid = 0;
            lup_CheckId.EditValue = GlobalVariable.LoginId;
            lup_ReadingNumber.EditValue = 1;
            lup_Eye.EditValue = null;
            tx_Reading.EditValue = null;

            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                ActClose();
                return;
            }

            var qualityList = ModelService.GetList(p => p.RevNo == qcRev.RevNo
                                                        && p.ItemCode == TEMP_XFPOP_PACK.ItemCode
                                                        && p.CheckDivision == MasterCodeSTR.InspectionDivision_Shipment
                                                        && (p.ProcessCode == TEMP_XFPOP_PACK.ProcessCode || p.ProcessCode == null)
                                                        && p.UseFlag == "Y"
                                                    )
                                                    .OrderBy(p => p.DisplayOrder)
                                                    .ToList();
            if (qualityList.Count == 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                ActClose();
                return;
            }

            var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP_PACK.WorkNo
                                                                                    && p.WorkSeq == TEMP_XFPOP_PACK.ProcessSeq
                                                                                    && (p.ProcessCode == TEMP_XFPOP_PACK.ProcessCode || p.ProcessCode == null)
                                                                                    && p.CheckDivision == MasterCodeSTR.InspectionDivision_Shipment).FirstOrDefault();
            if (TN_QCT1100_OldObj != null)
            {
                var oldList = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
                if (oldList.Count > 0)
                {
                    foreach (var v in qualityList)
                    {
                        var Obj = oldList.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                        if (Obj != null)
                        {
                            v.Reading1 = Obj.Reading1;
                            v.Reading2 = Obj.Reading2;
                            v.Reading3 = Obj.Reading3;
                            v.Reading4 = Obj.Reading4;
                            v.Reading5 = Obj.Reading5;
                            v.Reading6 = Obj.Reading6;
                            v.Reading7 = Obj.Reading7;
                            v.Reading8 = Obj.Reading8;
                            v.Reading9 = Obj.Reading9;
                            v.Judge = Obj.Judge;
                        }
                    }
                }
            }

            GridBindingSource.DataSource = qualityList;
            gridEx1.DataSource = GridBindingSource;
            gridEx1.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 적용 클릭이벤트
        /// </summary>
        private void Btn_Apply_Click(object sender, EventArgs e)
        {
            var view = GridExControl.MainGrid.MainView as GridView;
            var list = GridBindingSource.List as List<TN_QCT1001>;

            var readingNumber = "Reading" + lup_ReadingNumber.EditValue.GetNullToEmpty();
            int readingNumberCount = ((List<TN_STD1000>)lup_ReadingNumber.DataSource).Count;

            if (list == null) return;

            //if (view.GetRowCellValue(rowid, view.Columns["CheckWay"]).ToString() == MasterCodeSTR.InspectionWay_Eye)
            //{
            //    string val = lup_Eye.EditValue.GetNullToEmpty();
            //    if (val.IsNullOrEmpty())
            //    {
            //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
            //        return;
            //    }

            //    view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
            //    rowid++;
            //    if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
            //    {
            //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
            //        rowid--;
            //    }
            //    else if (rowid >= view.RowCount)
            //    {
            //        rowid = 0;
            //        lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
            //    }
            //    view.FocusedRowHandle = rowid;
            //}
            //else
            //{
            //    string val = tx_Reading.EditValue.GetNullToEmpty();
            //    if (val.IsNullOrEmpty())
            //    {
            //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
            //        return;
            //    }

            //    view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
            //    tx_Reading.Text = "";
            //    rowid++;
            //    if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
            //    {
            //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
            //        rowid--;
            //    }
            //    else if (rowid >= view.RowCount)
            //    {
            //        rowid = 0;
            //        lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
            //    }
            //    view.FocusedRowHandle = rowid;
            //}

            var maxReading = view.GetRowCellValue(rowid, view.Columns["MaxReading"]).GetNullToEmpty();
            if (!maxReading.IsNullOrEmpty() && maxReading.GetDecimalNullToZero() < lup_ReadingNumber.EditValue.GetDecimalNullToZero())
            {
                rowid++;
                if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
                    rowid--;
                }
                else if (rowid >= view.RowCount)
                {
                    rowid = 0;
                    lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
                }
                view.FocusedRowHandle = rowid;
                return;
            }

            if (view.GetRowCellValue(rowid, view.Columns["CheckDataType"]).ToString() == MasterCodeSTR.CheckDataType_C)
            {
                string val = lup_Eye.EditValue.GetNullToEmpty();
                if (val.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
                    return;
                }

                view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
                rowid++;
                if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
                    rowid--;
                }
                else if (rowid >= view.RowCount)
                {
                    rowid = 0;
                    lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
                }
                view.FocusedRowHandle = rowid;
            }
            else
            {
                string val = tx_Reading.EditValue.GetNullToEmpty();
                if (val.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
                    return;
                }

                view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
                tx_Reading.Text = "";
                rowid++;
                if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
                    rowid--;
                }
                else if (rowid >= view.RowCount)
                {
                    rowid = 0;
                    lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
                }
                view.FocusedRowHandle = rowid;
            }
        }

        //protected override void AddRowClicked()
        //{
        //    var view = GridExControl.MainGrid.MainView as GridView;
        //    var list = GridBindingSource.List as List<TN_QCT1001>;

        //    var readingNumber = "Reading" + lup_ReadingNumber.EditValue.GetNullToEmpty();
        //    int readingNumberCount = ((List<TN_STD1000>)lup_ReadingNumber.DataSource).Count;

        //    if (list == null) return;

        //    if (view.GetRowCellValue(rowid, view.Columns["CheckWay"]).ToString() == MasterCodeSTR.InspectionWay_Eye)
        //    {
        //        string val = lup_Eye.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //    else
        //    {
        //        string val = tx_Reading.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        tx_Reading.Text = "";
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //}

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            var view = GridExControl.MainGrid.MainView as GridView;
            var list = GridBindingSource.List as List<TN_QCT1001>;
            if (list == null || list.Count == 0) return;

            if (list.Count == list.Where(p => p.Judge.IsNullOrEmpty()).Count())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("CheckingInspection")));
                return;
            }

            var checkId = lup_CheckId.EditValue.GetNullToEmpty();
            if (checkId.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("CheckId2")));
                return;
            }

            var masterCheckResult = list.Any(p => p.Judge == "NG") ? "NG" : "OK";

            var TN_QCT1100_NewObj = new TN_QCT1100()
            {
                InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_Shipment),
                CheckDivision = MasterCodeSTR.InspectionDivision_Shipment,
                CheckPoint = MasterCodeSTR.CheckPoint_General,
                WorkNo = TEMP_XFPOP_PACK.WorkNo,
                WorkSeq = TEMP_XFPOP_PACK.ProcessSeq,
                WorkDate = TEMP_XFPOP_PACK.WorkDate,
                ItemCode = TEMP_XFPOP_PACK.ItemCode,
                CustomerCode = TEMP_XFPOP_PACK.CustomerCode,
                ProcessCode = TEMP_XFPOP_PACK.ProcessCode,
                ProductLotNo = ProductLotNo,
                CheckDate = DateTime.Today,
                CheckId = checkId,
                CheckResult = masterCheckResult,
            };

            foreach (var v in list)
            {
                var TN_QCT1101_NewObj = new TN_QCT1101()
                {
                    InspNo = TN_QCT1100_NewObj.InspNo,
                    InspSeq = TN_QCT1100_NewObj.TN_QCT1101List.Count == 0 ? 1 : TN_QCT1100_NewObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
                    RevNo = v.RevNo,
                    ItemCode = v.ItemCode,
                    Seq = v.Seq,
                    CheckWay = v.CheckWay,
                    CheckList = v.CheckList,
                    CheckMax = v.CheckMax,
                    CheckMin = v.CheckMin,
                    CheckSpec = v.CheckSpec,
                    CheckUpQuad = v.CheckUpQuad,
                    CheckDownQuad = v.CheckDownQuad,
                    CheckDataType = v.CheckDataType,
                    Reading1 = v.Reading1,
                    Reading2 = v.Reading2,
                    Reading3 = v.Reading3,
                    Reading4 = v.Reading4,
                    Reading5 = v.Reading5,
                    Reading6 = v.Reading6,
                    Reading7 = v.Reading7,
                    Reading8 = v.Reading8,
                    Reading9 = v.Reading9,
                    Judge = v.Judge
                };
                TN_QCT1100_NewObj.TN_QCT1101List.Add(TN_QCT1101_NewObj);
            }

            QcModelService.Insert(TN_QCT1100_NewObj);
            QcModelService.Save();
            SetIsFormControlChanged(false);
            DialogResult = DialogResult.OK;
            ActClose();
        }

        protected override void ActClose()
        {
            SetIsFormControlChanged(false);
            base.ActClose();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as TN_QCT1001;
            rowid = e.FocusedRowHandle;
            //if (obj.CheckWay == MasterCodeSTR.InspectionWay_Eye)
            //{
            //    lup_Eye.Focus();
            //    tx_Reading.ReadOnly = true;
            //    lup_Eye.ReadOnly = false;
            //}
            //else
            //{
            //    tx_Reading.Focus();
            //    tx_Reading.ReadOnly = false;
            //    lup_Eye.ReadOnly = true;

            //    var CheckDataType = obj.CheckDataType;
            //    if (!CheckDataType.IsNullOrEmpty())
            //    {
            //        tx_Reading.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //        tx_Reading.Properties.Mask.EditMask = string.Format("N{0}", CheckDataType);
            //        tx_Reading.Properties.Mask.UseMaskAsDisplayFormat = true;
            //    }
            //    else
            //    {
            //        tx_Reading.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //        tx_Reading.Properties.Mask.EditMask = string.Format("N{0}", 0);
            //    }
            //}

            if (obj.CheckDataType == MasterCodeSTR.CheckDataType_C)
            {
                lup_Eye.Focus();
                tx_Reading.ReadOnly = true;
                lup_Eye.ReadOnly = false;
            }
            else
            {
                tx_Reading.Focus();
                tx_Reading.ReadOnly = false;
                lup_Eye.ReadOnly = true;

                var CheckDataType = obj.CheckDataType;
                if (!CheckDataType.IsNullOrEmpty())
                {
                    tx_Reading.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    tx_Reading.Properties.Mask.EditMask = string.Format("N{0}", CheckDataType);
                    tx_Reading.Properties.Mask.UseMaskAsDisplayFormat = true;
                }
                else
                {
                    tx_Reading.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    tx_Reading.Properties.Mask.EditMask = string.Format("N{0}", 0);
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;

            if (e.Column.FieldName.Contains("Reading"))
            {
                //var inspectionWay = obj.CheckWay.GetNullToEmpty();
                //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                //{
                //    CheckInput(obj);
                //}
                //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                //{
                //    CheckEye(obj);
                //}
                //else
                //{
                //    CheckInput(obj);
                //}

                var checkDataType = obj.CheckDataType.GetNullToEmpty();
                if (checkDataType == MasterCodeSTR.CheckDataType_C)
                {
                    CheckEye(obj);
                }
                else
                {
                    CheckInput(obj);
                }
            }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                    //{
                    //    //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                    //    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    //    if (checkDataType.IsNullOrEmpty())
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    }
                    //    else
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                    //    }

                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}
                    //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    //    var readingValue = e.CellValue.GetNullToEmpty();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    //}
                    //else
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}

                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        var readingValue = e.CellValue.GetNullToEmpty();
                        e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    }
                    else
                    {
                        //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                        var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                        var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                        var readingValue = e.CellValue.GetDecimalNullToNull();
                        e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    }
                }
                else if (e.Column.FieldName == "Judge")
                {
                    var judgeValue = view.GetRowCellValue(e.RowHandle, "Judge").GetNullToEmpty();
                    if (judgeValue == "NG")
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        /// <summary>
        /// 측정치 키 이벤트
        /// </summary> 
        private void Tx_Reading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!tx_Reading.EditValue.IsNullOrEmpty())
                    AddRowClicked();
            }
        }

        /// <summary>
        /// 시료수 변경 시 이벤트
        /// </summary>
        private void Lup_ReadingNumber_EditValueChanged(object sender, EventArgs e)
        {
            var view = GridExControl.MainGrid.MainView as GridView;
            view.FocusedRowHandle = 0;
        }

        /// <summary>
        /// 치수검사 체크
        /// </summary>
        private void CheckInput(TN_QCT1001 obj)
        {
            var checkSpec = obj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null) return;
            var checkUpQuad = obj.CheckUpQuad.GetDecimalNullToZero();
            var checkDownQuad = obj.CheckDownQuad.GetDecimalNullToZero();
            var checkUp = checkSpec + checkUpQuad;
            var checkDown = checkSpec - checkDownQuad;

            int NgQty = 0;
            int OkQty = 0;

            var reading1 = obj.Reading1.GetDecimalNullToNull();
            var reading2 = obj.Reading2.GetDecimalNullToNull();
            var reading3 = obj.Reading3.GetDecimalNullToNull();
            var reading4 = obj.Reading4.GetDecimalNullToNull();
            var reading5 = obj.Reading5.GetDecimalNullToNull();
            var reading6 = obj.Reading6.GetDecimalNullToNull();
            var reading7 = obj.Reading7.GetDecimalNullToNull();
            var reading8 = obj.Reading8.GetDecimalNullToNull();
            var reading9 = obj.Reading9.GetDecimalNullToNull();

            if (reading1 != null)
            {
                if (reading1 >= checkDown && reading1 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 >= checkDown && reading2 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 >= checkDown && reading3 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 >= checkDown && reading4 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 >= checkDown && reading5 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 >= checkDown && reading6 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 >= checkDown && reading7 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 >= checkDown && reading8 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 >= checkDown && reading9 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                obj.Judge = null;
            }
            else if (NgQty > 0)
            {
                obj.Judge = "NG";
            }
            else
            {
                obj.Judge = "OK";
            }

            GridExControl.BestFitColumns();
        }

        /// <summary>
        /// 육안검사 체크
        /// </summary>
        /// <param name="detailObj"></param>
        private void CheckEye(TN_QCT1001 obj)
        {
            int NgQty = 0;
            int OkQty = 0;

            var reading1 = obj.Reading1.GetNullToNull();
            var reading2 = obj.Reading2.GetNullToNull();
            var reading3 = obj.Reading3.GetNullToNull();
            var reading4 = obj.Reading4.GetNullToNull();
            var reading5 = obj.Reading5.GetNullToNull();
            var reading6 = obj.Reading6.GetNullToNull();
            var reading7 = obj.Reading7.GetNullToNull();
            var reading8 = obj.Reading8.GetNullToNull();
            var reading9 = obj.Reading9.GetNullToNull();

            if (reading1 != null)
            {
                if (reading1 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                obj.Judge = null;
            }
            else if (NgQty > 0)
            {
                obj.Judge = "NG";
            }
            else
            {
                obj.Judge = "OK";
            }

            GridExControl.BestFitColumns();
        }

        private void Tx_Reading_GotFocus(object sender, EventArgs e)
        {
            tx_Reading.SelectAll();
        }

        private Color DetailCheckInputColor(decimal? checkSpec, decimal checkUpQuad, decimal checkDownQuad, decimal? readingValue)
        {
            //var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null)
                return Color.Black;
            else
            {
                //var checkUpQuad = detailObj.CheckUpQuad.GetDecimalNullToZero();
                //var checkDownQuad = detailObj.CheckDownQuad.GetDecimalNullToZero();
                var checkUp = checkSpec + checkUpQuad;
                var checkDown = checkSpec - checkDownQuad;

                if (readingValue != null)
                {
                    if (readingValue >= checkDown && readingValue <= checkUp)
                        return Color.Black;
                    else
                        return Color.Red;
                }
                else
                    return Color.Black;
            }
        }

        private Color DetailCheckInputColor(string readingValue)
        {
            if (!readingValue.IsNullOrEmpty())
            {
                if (readingValue == "OK")
                    return Color.Black;
                else
                    return Color.Red;
            }
            else
                return Color.Black;
        }
    }
}
