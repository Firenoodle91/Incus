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
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.DEV
{
    public partial class XFDEV1000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_DEV1000> ModelService = (IService<TN_DEV1000>)ProductionFactory.GetDomainService("TN_DEV1000");

        public XFDEV1000()
        {
            InitializeComponent();

            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            dt_ReqDate.SetTodayIsMonth();
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ReqNo", LabelConvert.GetLabelText("ReqNo2"));
            GridExControl.MainGrid.AddColumn("ReqDate", LabelConvert.GetLabelText("ReqDate2"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"), false);
            GridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ReqId", LabelConvert.GetLabelText("ReqId2"));
            GridExControl.MainGrid.AddColumn("ReqQty", LabelConvert.GetLabelText("ReqQty2"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("ReturnDate", LabelConvert.GetLabelText("ReturnDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("ReqFileName", LabelConvert.GetLabelText("ReqFileName"));
            GridExControl.MainGrid.AddColumn("ReqFileUrl", LabelConvert.GetLabelText("ReqFileUrl"), false);
            GridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate2"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId2"));
            GridExControl.MainGrid.AddColumn("CheckFileName", LabelConvert.GetLabelText("CheckReport"));
            GridExControl.MainGrid.AddColumn("CheckFileUrl", LabelConvert.GetLabelText("CheckFileUrl"), false);
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
        }

        protected override void InitRepository()
        {
            var userList = ModelService.GetChildList<User>(p => true).ToList();
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqId", userList, "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", userList, "LoginId", "UserName");

            GridExControl.MainGrid.MainView.Columns["ReqFileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_ReqFile, "ReqFileName", "ReqFileUrl");
            GridExControl.MainGrid.MainView.Columns["CheckFileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_ReqFile, "CheckFileName", "CheckFileUrl");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ReqNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                    && (p.ReqDate >= dt_ReqDate.DateFrEdit.DateTime && p.ReqDate <= dt_ReqDate.DateToEdit.DateTime)
                                                               )
                                                               .OrderBy(p => p.ReqDate)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_DEV1000;

            if (obj != null)
            {
                ModelService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFDEV1000, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }
    }
}