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
            this.Text = "품목정보";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
            lupitemcode.SetDefault(false, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("ItemCode");
            gridEx1.MainGrid.AddColumn("Temp2");
            gridEx1.MainGrid.AddColumn("Temp1");
            gridEx1.MainGrid.AddColumn("ProcessCode");
            gridEx1.MainGrid.AddColumn("CheckName");
            gridEx1.MainGrid.AddColumn("ProcessGu");
            gridEx1.MainGrid.AddColumn("CheckProv");
            gridEx1.MainGrid.AddColumn("CheckStand");
            gridEx1.MainGrid.AddColumn("UpQuad");
            gridEx1.MainGrid.AddColumn("DownQuad");
          

            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
        
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessGu", DbRequesHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp1", DbRequesHandler.GetCommCode(MasterCodeSTR.VCTYPE), "Mcode", "Codename");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            string itemName = lupitemcode.EditValue.GetNullToEmpty();


            bindingSource.DataSource = ModelService.GetList(p=> p.ItemCode == itemName).OrderBy(p => p.Seq).ToList();

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
                    tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessCode").GetNullToEmpty();
                    tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessGu").GetNullToEmpty();
                    tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckName").GetNullToEmpty();
                    tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckProv").GetNullToEmpty();
                    tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckStand").GetNullToEmpty();
                    tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "UpQuad").GetDoubleNullToZero();
                    tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DownQuad").GetDoubleNullToZero();
                    tn.Temp1 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp1").GetNullToEmpty();
                    tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty();
                    itemMasterList.Add(tn);
                }
                param.SetValue(PopupParameter.ReturnObject, itemMasterList);
            }
          

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                List<TP_QC1000> itemMasterList = new List<TP_QC1000>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    TP_QC1000 tn = new TP_QC1000();
                    tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessCode").GetNullToEmpty();
                    tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessGu").GetNullToEmpty();
                    tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckName").GetNullToEmpty();
                    tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckProv").GetNullToEmpty();
                    tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckStand").GetNullToEmpty();
                    tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "UpQuad").GetDoubleNullToZero();
                    tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DownQuad").GetDoubleNullToZero();
                    tn.Temp1 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp1").GetNullToEmpty();
                    tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty();
                    itemMasterList.Add(tn);
                }
                param.SetValue(PopupParameter.ReturnObject, itemMasterList);

                ReturnPopupArgument = new PopupArgument(param);
                base.ActClose();
            }
            //else
            //{
            //    List<TP_QC1000> itemMasterList = new List<TP_QC1000>();
            //    var obj = ModelBindingSource.Current as TN_QCT1000;

            //    TP_QC1000 tn = new TP_QC1000();
            //    tn.ProcessCode = obj.ProcessCode;
            //    tn.ProcessGu = obj.ProcessGu;
            //    tn.CheckName = obj.CheckName;
            //    tn.CheckProv = obj.CheckProv;
            //    tn.CheckStand = obj.CheckStand;
            //    tn.UpQuad = obj.UpQuad;
            //    tn.DownQuad = obj.DownQuad;
            //    tn.Temp1 = obj.Temp1;
            //    tn.Temp2 = obj.Temp2;
            //    itemMasterList.Add(tn);
            //    param.SetValue(PopupParameter.ReturnObject, itemMasterList);
            //}

            //ReturnPopupArgument = new PopupArgument(param);
            //base.ActClose();
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




        }
    }
}

