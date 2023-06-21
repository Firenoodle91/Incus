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

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 성적서샘플링관리
    /// </summary>
    public partial class XFQCT1600 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1600> ModelService = (IService<TN_QCT1600>)ProductionFactory.GetDomainService("TN_QCT1600");

        public XFQCT1600()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            lup_AQL.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.AQL));
            lup_Customer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }


        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            GridExControl.MainGrid.AddColumn("AQL", LabelConvert.GetLabelText("AQL"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("Customer"));
            GridExControl.MainGrid.AddColumn("MinValue", LabelConvert.GetLabelText("Range1"));
            GridExControl.MainGrid.AddColumn("MaxValue", LabelConvert.GetLabelText("Range2"));
            GridExControl.MainGrid.AddColumn("CheckQty", LabelConvert.GetLabelText("CheckQty"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "AQL", "CustomerCode", "MinValue", "MaxValue", "CheckQty", "Memo");

        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("AQL", DbRequestHandler.GetCommCode(MasterCodeSTR.AQL, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("MinValue", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("MaxValue", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("CheckQty", DefaultBoolean.Default, "n0");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineCode");

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var valAQL = lup_AQL.EditValue.GetNullToEmpty();
            var customer = lup_Customer.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(valAQL) ? true : p.AQL == valAQL)
                                                                        && (string.IsNullOrEmpty(customer) ? true : p.CustomerCode == customer)
                                                               )
                                                               .OrderBy(p => p.AQL)
                                                               .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            GridBindingSource.EndEdit();
            GridExControl.MainGrid.PostEditor();
            
            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_QCT1600 newObj = (TN_QCT1600)GridBindingSource.AddNew();

            ModelService.Insert(newObj);
            GridExControl.BestFitColumns();
        }
        
        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_QCT1600;

            if (obj != null)
            {
                ModelService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }
    }
}