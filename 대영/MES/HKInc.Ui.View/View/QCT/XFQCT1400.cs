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
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 클레임관리
    /// </summary>
    public partial class XFQCT1400 : Service.Base.ListFormTemplate
    {
        private List<TN_QCT1400> removeFileList = new List<TN_QCT1400>();

        IService<TN_QCT1400> ModelService = (IService<TN_QCT1400>)ProductionFactory.GetDomainService("TN_QCT1400");

        public XFQCT1400()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dt_ClaimDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_ClaimDate.DateToEdit.DateTime = DateTime.Today;

        }

        protected override void InitCombo()
        {          
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 반제품(타사)에 대한 클레임을 제기할 수도 있어서 추가 처리
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ClaimNo", LabelConvert.GetLabelText("ClaimNo"));
            GridExControl.MainGrid.AddColumn("ClaimDate", LabelConvert.GetLabelText("ClaimDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("TN_STD1400.CustomerName", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            GridExControl.MainGrid.AddColumn("ClaimQty", LabelConvert.GetLabelText("Qty2"), HorzAlignment.Far, FormatType.Numeric, "n2");
            GridExControl.MainGrid.AddColumn("ClaimType", LabelConvert.GetLabelText("ClaimType"));
            GridExControl.MainGrid.AddColumn("ClaimId", LabelConvert.GetLabelText("ClaimId")); 
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            GridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ClaimType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.ClaimType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ClaimId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_ClaimFile, "FileName", "FileUrl", true);
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("ClaimNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            removeFileList.Clear();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(customerCode) ? true : p.TN_STD1400.CustomerCode == customerCode)
                                                                 && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                 && (p.ClaimDate >= dt_ClaimDate.DateFrEdit.DateTime
                                                                    && p.ClaimDate <= dt_ClaimDate.DateToEdit.DateTime)
                                                                )
                                                                .OrderBy(p => p.ClaimDate)
                                                                .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            if (removeFileList.Count > 0)
            {
                //파일 Delete
                foreach (var v in removeFileList.Where(p => !p.FileUrl.IsNullOrEmpty()).ToList())
                {
                    try
                    {
                        FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, v.FileUrl);
                    }
                    catch { }
                }
            }

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_QCT1400;
            GridBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
            removeFileList.Add(obj);
        }
        
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT1400, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }
    }
}