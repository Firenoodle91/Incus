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
    public partial class XSFPUR1800D : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string DepartmentCode = string.Empty;
        public XSFPUR1800D()
        {
            InitializeComponent();
         
        }
        public XSFPUR1800D(PopupDataParam parameter, PopupCallback callback) :this()
        {
          

            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

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
            this.Text = "외주공정";
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            
            gridEx1.MainGrid.AddColumn("WorkDate", "작업지시일");
            gridEx1.MainGrid.AddColumn("WorkNo", "작업지시번호");
            gridEx1.MainGrid.AddColumn("Process", "공정");
            gridEx1.MainGrid.AddColumn("Pseq", "순서");
            gridEx1.MainGrid.AddColumn("MachineCode", "설비");
            gridEx1.MainGrid.AddColumn("ItemCode", "품목");
            gridEx1.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("LotNo", "제조LOT");
            gridEx1.MainGrid.AddColumn("OkQty", "생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("Memo", "비고");
       
            gridEx1.MainGrid.AddColumn("JobStatus", false);
            gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {            
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
            
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {


                var result = context.Database
                      .SqlQuery<TP_PUR1700LIST>("SP_OUT_JOBLIST").OrderBy(p => p.WorkNo).ToList();
                bindingSource.DataSource = result.OrderBy(o => o.PSeq).ToList();

            }

           
            
            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();
      
                if (IsmultiSelect)
                {
                    List<TP_PUR1700LIST> itemMasterList = new List<TP_PUR1700LIST>();

                    foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                    {
                    TP_PUR1700LIST tn = new TP_PUR1700LIST();
                    tn.WorkDate = Convert.ToDateTime(gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "WorkDate").GetNullToEmpty()); 
                    tn.WorkNo = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "WorkNo").GetNullToEmpty(); 
                    tn.Process = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Process").GetNullToEmpty();
                    tn.PSeq = Convert.ToInt32(gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "PSeq").GetNullToZero());
                    tn.PlanQty = Convert.ToInt32(gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "PlanQty").GetNullToZero());
                    tn.MachineCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "MachineCode").GetNullToEmpty();
                    tn.ItemCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ItemCode").GetNullToEmpty();
                    tn.LotNo = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "LotNo").GetNullToEmpty();
                    tn.OkQty= Convert.ToInt32(gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "OkQty").GetNullToZero());
                    itemMasterList.Add(tn);
                    }
                    param.SetValue(PopupParameter.ReturnObject, itemMasterList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, (TP_PUR1700LIST)bindingSource.Current);
                }
           
            ReturnPopupArgument = new PopupArgument(param);
            base.Close();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {

                TP_PUR1700LIST itemMaster = (TP_PUR1700LIST)bindingSource.Current;

                    if (IsmultiSelect)
                    {
                        List<TP_PUR1700LIST> itemMasterList = new List<TP_PUR1700LIST>();
                        if (itemMaster != null)
                            itemMasterList.Add(itemMaster);

                        param.SetValue(PopupParameter.ReturnObject, itemMasterList);
                    }
                    else
                    {
                        param.SetValue(PopupParameter.ReturnObject, itemMaster);
                    }
                
                ReturnPopupArgument = new PopupArgument(param);

                base.ActClose();
            }

        }
    }
}

