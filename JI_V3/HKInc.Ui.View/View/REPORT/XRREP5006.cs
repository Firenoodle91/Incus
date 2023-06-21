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
    public partial class XRREP5006 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1105> ModelService = (IService<TN_STD1105>)ProductionFactory.GetDomainService("TN_STD1105");

        IService<TN_STD1100> ItemModel = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_STD1400> CustModel = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");

        public XRREP5006()
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
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ItemModel.GetList(p => p.UseFlag == "Y" && (p.TopCategory != MasterCodeSTR.TopCategory_SPARE && p.TopCategory != MasterCodeSTR.TopCategory_TOOL)).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), CustModel.GetList(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("CustomerCode"));
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
            MasterGridExControl.MainGrid.AddColumn("Weight", LabelConvert.GetLabelText("Weight"), HorzAlignment.Far, FormatType.Numeric, "n2");

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"),false);
            DetailGridExControl.MainGrid.AddColumn("StartDate", "시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("EndDate", "종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            //DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate", "Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "StartDate", "EndDate");

            //SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            SubDetailGridExControl.MainGrid.AddColumn("StartDate", "시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            SubDetailGridExControl.MainGrid.AddColumn("EndDate", "종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            SubDetailGridExControl.MainGrid.AddColumn("SrcCost", "원자재\n기준금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            SubDetailGridExControl.MainGrid.AddColumn("BarfeederCNCflag", "Bar\nfeeder\nCNC");
            SubDetailGridExControl.MainGrid.AddColumn("BarfeederCNCcycleTime", "Bar\nfeeder\nCNC\n1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("CNCflag", "CNC");
            SubDetailGridExControl.MainGrid.AddColumn("CNC1cycleTime", "CNC\n1차\n1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("CNC2cycleTime", "CNC\n2차\n1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("CNC3cycleTime", "CNC\n3차\n1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("MCTflag", "MCT");
            SubDetailGridExControl.MainGrid.AddColumn("MCTcycleTime", "MCT\n1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn("TRPPINflag", "TRPPIN");
            SubDetailGridExControl.MainGrid.AddColumn("TRPPINcycleTime", "TRPPIN\n1개당\n소요시간", HorzAlignment.Far, FormatType.Numeric, "n2");

            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "SrcCost", "BarfeederCNCflag", "BarfeederCNCcycleTime", "CNCflag",
                "CNC1cycleTime", "CNC2cycleTime", "CNC3cycleTime", "MCTflag", "MCTcycleTime", "TRPPINflag", "TRPPINcycleTime");

        }

        protected override void InitRepository()
        {
            //SubDetailGridExControl.MainGrid.MainView.ColumnPanelRowHeight = 100;
            SubDetailGridExControl.MainGrid.MainView.OptionsView.ColumnAutoWidth = false;
            SubDetailGridExControl.MainGrid.MainView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

            SubDetailGridExControl.MainGrid.MainView.Columns["SrcCost"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["BarfeederCNCflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["BarfeederCNCcycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["CNCflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["CNC1cycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["CNC2cycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["CNC3cycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["MCTflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["MCTcycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["TRPPINflag"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            SubDetailGridExControl.MainGrid.MainView.Columns["TRPPINcycleTime"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;


            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcCode", ModelService.GetChildList<TN_STD1100>(p => true).ToList(), "ItemCode", "ItemName");

            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("BarfeederCNCflag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("CNCflag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("MCTflag", "N");
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("TRPPINflag", "N");


            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));

        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ItemModel.ReLoad();
            CustModel.ReLoad();
            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ItemModel.GetList(p => p.UseFlag == "Y"
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
            
            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1105>(p => p.ItemCode == itemcode).OrderBy(p => p.StartDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            DetailFocusedRowChanged();
        }

        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();

            var masterObj =  MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var DetailObj = DetailGridBindingSource.Current as TN_STD1105;
            if (DetailObj == null) return;

            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.Clear();


            //if(DetailObj.NewRowFlag == "Y")
            //{
            //1----------------------------------------------------------------------

            //var newobj = new TN_STD1105()
            //{
            //    ItemCode = DetailObj.ItemCode,
            //    StartDate = DetailObj.StartDate,
            //    EndDate = DetailObj.EndDate,
            //    BarfeederCNCflag = "N",
            //    CNCflag = "N",
            //    MCTflag = "N",
            //    TRPPINflag = "N"
            //};
            //SubDetailGridBindingSource.Add(newobj);

            //2----------------------------------------------------------------------

            //SubDetailGridExControl.MainGrid.Clear();
            //SubDetailGridBindingSource.DataSource = null;
            //SubDetailGridExControl.DataSource = SubDetailGridBindingSource;

            //3----------------------------------------------------------------------

            //SubDetailGridExControl.MainGrid.Clear();
            //SubDetailGridExControl.DataSource = null;

            //4----------------------------------------------------------------------

            //SubDetailAddRowClicked();

            //5----------------------------------------------------------------------

            //SubDetailGridExControl.MainGrid.MainView.AddNewRow();

            //6----------------------------------------------------------------------


            //}
            //else
            //{

            //if (DetailObj.NewRowFlag == "Y") return;
            if (DetailObj.NewRowFlag == "Y")
            {
                SubDetailAddRowClicked();
            }
            else
            {
                string itemcode = DetailObj.ItemCode.GetNullToEmpty();
                DateTime start = Convert.ToDateTime(DetailObj.StartDate.GetNullToNull());
                DateTime end = Convert.ToDateTime(DetailObj.EndDate.GetNullToNull());

                SubDetailGridBindingSource.DataSource = ModelService.GetChildList<TN_STD1105>(p => p.ItemCode == itemcode
                                                                                                 && p.StartDate == start
                                                                                                 && p.EndDate == end
                                                                                             )
                                                                                             .OrderBy(p => p.StartDate)
                                                                                             .ToList();
                SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
                SubDetailGridExControl.BestFitColumns();
            }
            //}
        }


        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            var newObj = new TN_STD1105()
            {
                NewRowFlag = "Y",
                ItemCode = masterObj.ItemCode,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1)
            };

            DetailGridBindingSource.Add(newObj);
            DetailGridExControl.BestFitColumns();

        }
        protected override void SubDetailAddRowClicked()
        {
            SubDetailGridExControl.MainGrid.Clear();
            SubDetailGridBindingSource.Clear();

            var DetailObj = DetailGridBindingSource.Current as TN_STD1105;

            if (DetailObj.NewRowFlag != "Y") return;

            var newobj = new TN_STD1105()
            {
                NewRowFlag = "Y",
                ItemCode = DetailObj.ItemCode,
                StartDate = DetailObj.StartDate,
                EndDate = DetailObj.EndDate,
                BarfeederCNCflag = "N",
                CNCflag = "N",
                MCTflag = "N",
                Tappingflag = "N"
            };
            SubDetailGridBindingSource.Add(newobj);
            SubDetailGridExControl.BestFitColumns();

        }

        private void DetailMainView_CellvalueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView gridview = sender as GridView;

                //if (gridview == null)
                //    return;

                //var subdetailObj = SubDetailGridBindingSource.Current as TN_STD1103;

                //if (subdetailObj == null)
                //    return;

                List<TN_STD1105> DetailList = DetailGridBindingSource.DataSource as List<TN_STD1105>;

                string fieldName = e.Column.FieldName;
                dynamic checkDate = null;

                if (fieldName != "StartDate" && fieldName != "EndDate")
                    return;

                checkDate = gridview.GetRowCellValue(e.RowHandle, gridview.Columns[fieldName]);

                //최초 등록은 제외
                if (DetailList.Count > 1)
                {
                    foreach (var v in DetailList)
                    {
                        if (v.StartDate <= checkDate && checkDate <= v.EndDate && v.NewRowFlag != "Y")
                        {
                            HKInc.Service.Handler.MessageBoxHandler.Show("중복되는 기간 등록 불가");
                            //gridview.SetFocusedRowCellValue(fieldName, null);
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

            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();

            GridRowLocator.GetCurrentRow();

            var List = SubDetailGridBindingSource.Current as TN_STD1105;

            if(List.NewRowFlag == "Y")
            {
                ModelService.Insert(List);
            }
            else if (List.NewRowFlag == "N" && List.EditRowFlag == "Y")
            {
                ModelService.Update(List);
            }
            
            //ModelService.Insert(List);
            
            ModelService.Save();

            DataLoad();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DeleteDetailRow()
        {
            //삭제는 보류

            /*
            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            TN_MOLD1101 delObj = DetailGridBindingSource.Current as TN_MOLD1101;

            if (MasterObj != null)
            {
                MasterObj.TN_MOLD1101List.Remove(delObj);
                DetailGridBindingSource.RemoveCurrent();
            }
            */
        }

    }


}