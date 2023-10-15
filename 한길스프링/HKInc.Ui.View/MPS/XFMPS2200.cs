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
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using HKInc.Utils.Enum;
using DevExpress.XtraReports.UI;
using HKInc.Service.Helper;
using HKInc.Service.Handler;
using HKInc.Utils.Common;


namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 실적수량변경
    /// </summary>
    public partial class XFMPS2200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        List<TN_STD1100> ItemList;
        List<TN_MPS1400> WorkList;

        public XFMPS2200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            MasterGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
        }

        protected override void InitCombo()
        {
          //  processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
        }

        protected override void InitGrid()
        {
            //MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(false);
            //MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("RowId", LabelConvert.GetLabelText("RowId"), false);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("ProcessTurn", LabelConvert.GetLabelText("ProcessSeq"), HorzAlignment.Far, FormatType.Numeric, "N0", false);
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            MasterGridExControl.MainGrid.AddColumn("TN_MPS1201.TN_MPS1200.OutProcFlag", LabelConvert.GetLabelText("OutProcFlag"), false);
            MasterGridExControl.MainGrid.AddColumn("LotNo", LabelConvert.GetLabelText("ProductLotNo"));
            MasterGridExControl.MainGrid.AddColumn("Itemmoveno", LabelConvert.GetLabelText("ItemMoveNo"));
            //MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
      //     MasterGridExControl.MainGrid.AddColumn("TN_MPS1201.TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("OkQty"));
            MasterGridExControl.MainGrid.AddColumn("FailQty", LabelConvert.GetLabelText("BadQty"));
               MasterGridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkEmp"));

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OkQty", "FailQty", "WorkId");
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("OkQty");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("FailQty");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BadType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.BadType_POP), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("RowId");

            MasterGridExControl.MainGrid.Clear();

            var workNo = tx_WorkNo.EditValue.GetNullToEmpty();
            var itemMoveNo = tx_ItemMoveNo.EditValue.GetNullToEmpty();

            if (workNo.IsNullOrEmpty() && itemMoveNo.IsNullOrEmpty())
            {
                //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("WorkNo")));
                MessageBoxHandler.Show("작업지시번호 또는 이동표번호 필수입니다.");
                return;
            }

            if (!workNo.IsNullOrEmpty() && itemMoveNo.IsNullOrEmpty())
                MasterGridBindingSource.DataSource = ModelService.GetList(p => p.WorkNo == workNo).OrderBy(p => p.RowId).ToList();
            else if (workNo.IsNullOrEmpty() && !itemMoveNo.IsNullOrEmpty())
                MasterGridBindingSource.DataSource = ModelService.GetList(p => p.Itemmoveno == itemMoveNo).OrderBy(p => p.RowId).ToList();
            else
                MasterGridBindingSource.DataSource = ModelService.GetList(p => p.WorkNo == workNo && p.Itemmoveno == itemMoveNo).OrderBy(p => p.RowId).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }
        protected override void DeleteRow()
        {
            TN_MPS1401 obj = MasterGridBindingSource.Current as TN_MPS1401;
            //if(obj.OkQty.GetNullToZero()!=0)
            //{ MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_126)));
            //    return; }
            MasterGridBindingSource.Remove(obj);
            ModelService.Delete(obj);



        }
        protected override void MasterFocusedRowChanged()
        {
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();

            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var list = MasterGridBindingSource.List as List<TN_MPS1401>;
                if (list != null)
                {
                    var editList = list.Where(p => p.EditRowFlag == "Y").ToList();
                    var updateId = GlobalVariable.LoginId;
                    var updateTime = DateTime.Now;
                                       
                    foreach (var v in editList)
                    {
                        v.UpdateTime = updateTime;
                        v.UpdateId = updateId;
                        v.ResultQty = v.OkQty.GetIntNullToZero() + v.FailQty.GetIntNullToZero();

                        var list2 = list.Where(p => p.WorkNo == v.WorkNo
                                                    && p.ProcessCode == v.ProcessCode
                                                    && p.ProcessTurn == v.ProcessTurn
                                                    && p.LotNo == v.LotNo).ToList();
                        //마스터 UPD
                        var sumOkQty = list2.Sum(p => p.OkQty).GetDecimalNullToZero();
                        var sumBadQty = list2.Sum(p => p.FailQty).GetDecimalNullToZero();
                        var sumResultQty = sumOkQty + sumBadQty;
                        
                    
                    }
                }
            }

            MasterGridBindingSource.EndEdit();
            ModelService.Save();
            DataLoad();
            
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var list = MasterGridBindingSource.List as List<TN_MPS1401>;
                if (list != null)
                {
                    var editWorkNoArray = list.Where(p => p.EditRowFlag == "Y").Select(p=>p.WorkNo).Distinct().ToArray();
                    foreach (var v in editWorkNoArray)
                    {
                      //  DbRequestHandler.USP_UPD_XFMPS2200(v);
                    }
                }
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            WorkList = ModelService.GetChildList<TN_MPS1400>(p=>p.PSeq==1).ToList();
            ItemList = ModelService.GetChildList<TN_STD1100>(p => 1 == 1).ToList();
            if (e.Column.FieldName == "ItemNm1")
            {
                //var outProcFlag = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "OutProcFlag").GetNullToEmpty();
                //if (outProcFlag == "Y")
                //{
                //    e.DisplayText += "(" + LabelConvert.GetLabelText("Outsourcing") + ")";
                //}
                var workno = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "WorkNo").GetNullToEmpty();
                var itemcode = WorkList.Where(p => p.WorkNo == workno && p.PSeq == 1).Select(s=>s.ItemCode).FirstOrDefault();
                var item = ItemList.Where(p => p.ItemCode== itemcode).FirstOrDefault();
               
                if (item != null)
                {
                    e.DisplayText = item.ItemNm1.GetNullToEmpty();
                }
            }
            if (e.Column.FieldName == "ItemNm")
            {
                var workno = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "WorkNo").GetNullToEmpty();
                var itemcode = WorkList.Where(p => p.WorkNo == workno && p.PSeq == 1).Select(s => s.ItemCode).FirstOrDefault();
                var item = ItemList.Where(p => p.ItemCode == itemcode).FirstOrDefault();

                if (item != null)
                {
                    e.DisplayText = item.ItemNm.GetNullToEmpty();
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var obj = MasterGridBindingSource.Current as TN_MPS1401;
            obj.EditRowFlag = "Y";
            var updateId = GlobalVariable.LoginId;
            var updateTime = DateTime.Now;
            obj.UpdateTime = updateTime;
            obj.UpdateId = updateId;
        }

    }
}