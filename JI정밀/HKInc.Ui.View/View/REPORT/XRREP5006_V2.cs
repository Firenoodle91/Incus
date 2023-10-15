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
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;


namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 납품원가기준관리
    /// </summary>
    public partial class XRREP5006_V2 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1105> ModelService = (IService<TN_STD1105>)ProductionFactory.GetDomainService("TN_STD1105");

        public XRREP5006_V2()
        {   
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            DetailGridExControl.MainGrid.MainView.RowCellStyle += DetailMainView_RowCellStyle;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellvalueChanged;

        }

        protected override void InitCombo()
        {
            //lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());

            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", "주거래처");
            //MasterGridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemName"));
            //MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "원소재명");
            MasterGridExControl.MainGrid.AddColumn("Texture", "재종");
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            MasterGridExControl.MainGrid.AddColumn("Weight", LabelConvert.GetLabelText("Weight"), HorzAlignment.Far, FormatType.Numeric, "{0:#,#0");
            //MasterGridExControl.MainGrid.AddColumn("Weight", LabelConvert.GetLabelText("Weight"), HorzAlignment.Far, FormatType.Numeric, "{0:F3");

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.MainGrid.AddColumn("COGS", LabelConvert.GetLabelText("COGS"), false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"),false);
            DetailGridExControl.MainGrid.AddColumn("StartDate", "시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("EndDate", "종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");

            DetailGridExControl.MainGrid.AddColumn("SrcCost", "원자재\n기준금액", HorzAlignment.Far, FormatType.Numeric, "{0:#,#0.###}");
            DetailGridExControl.MainGrid.AddColumn("BarfeederCNCflag", "Bar\nfeeder\nCNC", false);
            DetailGridExControl.MainGrid.AddColumn("BarfeederCNCcycleTime", "Bar\nfeeder\nCNC\n1개당\n소요시간\n(초)", HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("CNCflag", "CNC", false);
            DetailGridExControl.MainGrid.AddColumn("CNC1cycleTime", "CNC\n1차\n1개당\n소요시간\n(초)", HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("CNC2cycleTime", "CNC\n2차\n1개당\n소요시간\n(초)", HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("CNC3cycleTime", "CNC\n3차\n1개당\n소요시간\n(초)", HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("MCTflag", "MCT", false);
            DetailGridExControl.MainGrid.AddColumn("MCTcycleTime", "MCT\n1개당\n소요시간\n(초)", HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn("Tappingflag", "Tapping", false);
            DetailGridExControl.MainGrid.AddColumn("TappingcycleTime", "Tapping\n1개당\n소요시간\n(초)", HorzAlignment.Far, FormatType.Numeric, "n2");

            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate", "SrcCost", "BarfeederCNCflag", "BarfeederCNCcycleTime", "CNCflag",
                "CNC1cycleTime", "CNC2cycleTime", "CNC3cycleTime", "MCTflag", "MCTcycleTime", "Tappingflag", "TappingcycleTime", "Memo");

            //MasterGridExControl.BestFitColumns();
            //DetailGridExControl.BestFitColumns();

        }

        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");

            DetailGridExControl.MainGrid.MainView.OptionsView.ColumnAutoWidth = false;
            DetailGridExControl.MainGrid.MainView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

            //DetailGridExControl.MainGrid.MainView.Columns["SrcCost"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["BarfeederCNCflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["BarfeederCNCcycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["CNCflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["CNC1cycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["CNC2cycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["CNC3cycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["MCTflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["MCTcycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["Tappingflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //DetailGridExControl.MainGrid.MainView.Columns["TappingcycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("BarfeederCNCflag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("CNCflag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MCTflag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Tappingflag", "N");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].Width = 500;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y"
                                                                                         && p.TopCategory == "A00"
                                                                                         //&& (p.SrcCode != null || p.SrcCode != "")
                                                                                         && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                                         && (string.IsNullOrEmpty(customerCode) ? true : p.MainCustomerCode == customerCode)
                                                                                       )
                                                                                        .OrderBy( p => p.ItemCode)
                                                                                        .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            string itemcode = masterObj.ItemCode.GetNullToEmpty();
            //DateTime start = Convert.ToDateTime(DetailObj.StartDate.GetNullToNull());
            //DateTime end = Convert.ToDateTime(DetailObj.EndDate.GetNullToNull());

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == itemcode
                                                                                         )
                                                                                         .OrderBy(p => p.StartDate)
                                                                                         .ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var newObj = new TN_STD1105()
            {
                COGS = DbRequestHandler.GetSeqStandard("COGS"),
                NewRowFlag = "Y",
                ItemCode = masterObj.ItemCode,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(6),
                SrcCost = DbRequestHandler.GetCustItemCost(masterObj.MainCustomerCode, masterObj.ItemCode, DateTime.Now.ToString("yyyy-MM-dd"), "M"),

                BarfeederCNCflag = "N",
                CNCflag = "N",
                MCTflag = "N",
                Tappingflag = "N"
            };

            DetailGridBindingSource.Add(newObj);
            DetailGridExControl.BestFitColumns();

        }

        private void DetailMainView_CellvalueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView gv = sender as GridView;

                //if (gridview == null)
                //    return;

                //var subdetailObj = SubDetailGridBindingSource.Current as TN_STD1103;

                //if (subdetailObj == null)
                //    return;

                List<TN_STD1105> DetailList = DetailGridBindingSource.DataSource as List<TN_STD1105>;

                string fieldName = e.Column.FieldName;
                dynamic checkDate = null;
                dynamic rowid = null;

                if (fieldName != "StartDate" && fieldName != "EndDate")
                    return;

                checkDate = gv.GetRowCellValue(e.RowHandle, gv.Columns[fieldName]);
                rowid = gv.GetRowCellValue(e.RowHandle, gv.Columns["RowId"]);//수정 대상은 비교 제외

                //최초 등록은 제외
                if (DetailList.Count > 1)
                {
                    foreach (var v in DetailList)
                    {
                        if ( v.StartDate <= checkDate && checkDate <= v.EndDate && v.NewRowFlag != "Y") //&& rowid != v.RowId
                        {
                            HKInc.Service.Handler.MessageBoxHandler.Show("중복되는 기간 등록 불가");

                            DetailGridExControl.MainGrid.MainView.CellValueChanged -= DetailMainView_CellvalueChanged;
                            gv.SetFocusedRowCellValue(fieldName, v.EndDate.AddDays(1));
                            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellvalueChanged;
                            gv.SelectRow(e.RowHandle);

                            //gridview.SelectCell(e.RowHandle, Column["SrcCost"]);
                            return;
                        }
                    }
                }

            }
            catch { }
        }

        private void DetailMainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                var DetailObj = DetailGridBindingSource.Current as TN_STD1103;

                //if (DetailObj != null)
                //{
                //    //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_38), LabelConvert.GetLabelText("PoConfirm")));
                //    e.Cancel = true;
                //}

                if (DetailObj.NewRowFlag != "Y")
                    e.Cancel = true;
            }
            catch { }
        }

        protected override void DataSave()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var DetailObj = DetailGridBindingSource.Current as TN_STD1105;
            if (DetailObj == null) return;

            GridRowLocator.GetCurrentRow();

            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            List<TN_STD1105> detailList = DetailGridBindingSource.DataSource as List<TN_STD1105>;

            foreach (var v in detailList)
            {
                //추가
                if (v.NewRowFlag == "Y")
                {
                    ModelService.Insert(v);
                }
                //수정
                else if (v.NewRowFlag == "N" && v.EditRowFlag == "Y")
                {
                    ModelService.Update(v);
                }
            }

            ModelService.Save();

            DataLoad();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            var detailObj = DetailGridBindingSource.Current as TN_STD1105;

            if (masterObj == null || detailObj == null) return;


            //if (detailObj.TN_MOLD1701List.Count != 0)
            //{
            //    MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_142));
            //    return;
            //}

            DetailGridBindingSource.RemoveCurrent();

            if (detailObj.NewRowFlag != "Y")
                ModelService.Delete(detailObj);
            
        }

    }


}