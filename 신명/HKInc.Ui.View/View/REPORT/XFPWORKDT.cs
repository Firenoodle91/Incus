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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using System.Collections.Specialized;
using System.IO;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 툴 기준정보관리
    /// </summary>
    public partial class XFWORKDT : Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFWORKDT()
        {
            InitializeComponent();
            GridExControl = gridEx1;

         //   rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            //GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ToolCode"));
            //GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ToolName"));
            //GridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("ToolNameENG"));
            //GridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("ToolNameCHN"));
            //GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            //GridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            //GridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            //GridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            //GridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            //GridExControl.MainGrid.AddColumn("ToolStockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"));
            //GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition"));
            //GridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"));
            //GridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            //GridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            //GridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            //GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            //GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            //GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemName", "ItemNameENG", "ItemNameCHN", "MainCustomerCode", "Spec1", "Spec2", "Spec3", "Spec4", "SafeQty", "StockPosition", "ProdFileName", "UseFlag", "Memo");

            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1100>(GridExControl);
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StockPosition", ModelService.GetChildList<TN_WMS2000>(p => true).ToList(), "PositionCode", Service.Helper.DataConvert.GetCultureDataFieldName("PositionName"),true);
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            //GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl", true);
            //GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            ////GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, GridExControl, "ProdFileName", "ProdFileUrl", true);
            //GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            //GridExControl.BestFitColumns();
        }
        //lup_StockPosition.SetDefault(true, "PositionCode", "PositionName", Std1100Service.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList());
        //    lup_StockPosition.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
        //    {
        //        FieldName = "TN_WMS1000." + DataConvert.GetCultureDataFieldName("WhName"),
        //        Caption = LabelConvert.GetLabelText(DataConvert.GetCultureDataFieldName("WhName")),
        //        Visible = true
        //    });
        protected override void DataLoad()
        {
            //GridRowLocator.GetCurrentRow("ItemCode");

            GridExControl.MainGrid.Clear();

            //ModelService.ReLoad();
            GridExControl.MainGrid.Columns.Clear();
            //var toolCodeName = tx_ToolCodeName.EditValue.GetNullToEmpty();
            //var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            //GridBindingSource.DataSource = ModelService.GetList(p =>    (string.IsNullOrEmpty(toolCodeName) ? true : (p.ItemCode.Contains(toolCodeName) || (p.ItemName.Contains(toolCodeName)) || p.ItemNameENG.Contains(toolCodeName) || p.ItemNameCHN.Contains(toolCodeName)))
            //                                                        && (radioValue == "A" ? true : p.UseFlag == radioValue)
            //                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)
            //                                                   )
            //                                                   .OrderBy(p => p.ItemName)
            //                                                   .ToList();
            //GridExControl.DataSource = GridBindingSource;
            //GridExControl.BestFitColumns();

            //GridRowLocator.SetCurrentRow();
            DataSet ds = DbRequestHandler.GetDataQury("exec sp_workdt");
            GridBindingSource.DataSource = ds.Tables[0];
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

        }





    }
}