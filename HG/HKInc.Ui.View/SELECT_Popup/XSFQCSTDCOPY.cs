using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.SELECT_Popup
{
    public partial class XSFQCSTDCOPY : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_QCT1000> ModelService = (IService<TN_QCT1000>)ProductionFactory.GetDomainService("TN_QCT1000");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        private string qctype = string.Empty;
        public XSFQCSTDCOPY()
        {
            InitializeComponent();
         

        }
        public XSFQCSTDCOPY(PopupDataParam parameter, PopupCallback callback) :this()
        {
          
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Value_1))
                qctype= parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            if (parameter.ContainsKey(PopupParameter.Constraint))
                DepartmentCode = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            this.Text = "검사규격정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
            lupitemcode.SetDefault(false, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());
            lupitemcode.isRequired = true;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("RowId", false);
            gridEx1.MainGrid.AddColumn("Seq", false);
            gridEx1.MainGrid.AddColumn("Temp2", "검사순서", true, HorzAlignment.Far);
            gridEx1.MainGrid.AddColumn("ItemCode", "품목코드", false);
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            gridEx1.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");            
            gridEx1.MainGrid.AddColumn("ProcessCode", "공정명");
            gridEx1.MainGrid.AddColumn("ProcessGu", "검사구분");
            gridEx1.MainGrid.AddColumn("CheckName", "검사항목");
            gridEx1.MainGrid.AddColumn("CheckProv", "검사방법");
            gridEx1.MainGrid.AddColumn("Temp1", "계측기종류");
            gridEx1.MainGrid.AddColumn("CheckStand", "기준", true, HorzAlignment.Far);
            gridEx1.MainGrid.AddColumn("UpQuad", "상한공차", true, HorzAlignment.Far);
            gridEx1.MainGrid.AddColumn("DownQuad", "하한공차", true, HorzAlignment.Far);
            gridEx1.MainGrid.AddColumn("UseYn", "사용여부", true, HorzAlignment.Center);
            gridEx1.MainGrid.AddColumn("Memo", "메모");

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("UseYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessGu", DbRequesHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp1", DbRequesHandler.GetCommCode(MasterCodeSTR.VCTYPE), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");

        }

        protected override void InitDataLoad()
        {
            //DataLoad();
        }

        protected override void DataLoad()
        {
            //if (lupitemcode.RequireCheck()) return;
            gridEx1.MainGrid.Clear();
            ModelService.ReLoad();

            //string itemName = lupitemcode.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            if (Item.IsNullOrEmpty())
            {
                MessageBoxHandler.Show("품번 및 품명은 필수 조건 입니다.", "경고");
                return;
            }
            bindingSource.DataSource = ModelService.GetList(p => (p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item))
                                                           )
                                                           .OrderBy(p => p.ItemCode)
                                                           .ThenBy(p => p.ProcessGu)
                                                           .ThenBy(p => p.ProcessCode)
                                                           .ThenBy(p => p.Seq)
                                                           .ToList();
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TP_QC1000> itemMasterList = new List<TP_QC1000>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    TP_QC1000 tn = new TP_QC1000();
                    tn.Temp1 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp1").GetNullToEmpty();
                    tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty();
                    tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessCode").GetNullToEmpty();
                    tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessGu").GetNullToEmpty();
                    tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckName").GetNullToEmpty();
                    tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckProv").GetNullToEmpty();
                    tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckStand").GetNullToEmpty();
                    tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "UpQuad").GetDoubleNullToZero();
                    tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DownQuad").GetDoubleNullToZero();
                    itemMasterList.Add(tn);
                }

                param.SetValue(PopupParameter.ReturnObject, itemMasterList);
            }
          

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            //PopupDataParam param = new PopupDataParam();
           
            //if (e.Clicks == 2)
            //{

            //    List<TP_QC1000> itemMasterList = new List<TP_QC1000>();

            //    foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
            //    {
            //        TP_QC1000 tn = new TP_QC1000();
            //        tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessCode").GetNullToEmpty();
            //        tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessGu").GetNullToEmpty();
            //        tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckName").GetNullToEmpty();
            //        tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckProv").GetNullToEmpty();
            //        tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckStand").GetNullToEmpty();
            //        tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "UpQuad").GetDoubleNullToZero();
            //        tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DownQuad").GetDoubleNullToZero();
            //        tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty();
            //        itemMasterList.Add(tn);
            //    }
            //    param.SetValue(PopupParameter.ReturnObject, itemMasterList);
            //}
            //else
            //  {
            //    List<TP_QC1000> itemMasterList1 = new List<TP_QC1000>();
            //    TP_QC1000 tn = new TP_QC1000();
            //    tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "ProcessCode").GetNullToEmpty();
            //    tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "ProcessGu").GetNullToEmpty();
            //    tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "CheckName").GetNullToEmpty();
            //    tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "CheckProv").GetNullToEmpty();
            //    tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "CheckStand").GetNullToEmpty();
            //    tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "UpQuad").GetDoubleNullToZero();
            //    tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "DownQuad").GetDoubleNullToZero();
            //    tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "Temp2").GetNullToEmpty();
            //    itemMasterList1.Add(tn);
            //    param.SetValue(PopupParameter.ReturnObject, itemMasterList1);
            //}

            //    ReturnPopupArgument = new PopupArgument(param);

            //    base.ActClose();
            

        }
    }
}

