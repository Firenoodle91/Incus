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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 용접지그관리
    /// </summary>
    public partial class XFMEA1500 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1500> ModelService = (IService<TN_MEA1500>)ProductionFactory.GetDomainService("TN_MEA1500");

        public XFMEA1500()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("RBIDCode", LabelConvert.GetLabelText("RBIDCode"));
            GridExControl.MainGrid.AddColumn("RBIDKind", LabelConvert.GetLabelText("RBIDKind"));
            GridExControl.MainGrid.AddColumn("RBIDName", LabelConvert.GetLabelText("RBIDName"));        
            GridExControl.MainGrid.AddColumn("RBIDNameENG", LabelConvert.GetLabelText("RBIDNameENG"));
            GridExControl.MainGrid.AddColumn("RBIDNameCHN", LabelConvert.GetLabelText("RBIDNameCHN"));
            GridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("Maker", LabelConvert.GetLabelText("Maker"));
            GridExControl.MainGrid.AddColumn("IntroductionDate", LabelConvert.GetLabelText("IntroductionDate"));
            GridExControl.MainGrid.AddColumn("SerialNo", LabelConvert.GetLabelText("SerialNo"));
            GridExControl.MainGrid.AddColumn("CorTurn", LabelConvert.GetLabelText("CorTurn"));
            GridExControl.MainGrid.AddColumn("CorDate", LabelConvert.GetLabelText("CorDate"));
            GridExControl.MainGrid.AddColumn("PredictionCorDate", LabelConvert.GetLabelText("PredictionCorDate"));
            GridExControl.MainGrid.AddColumn("WorkId", LabelConvert.GetLabelText("WorkId"));
            GridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"), false);     // 2021-05-26 김진우 주임 false 추가
            GridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1500>(GridExControl);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.InstrMaker, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CorTurn", DbRequestHandler.GetCommCode(MasterCodeSTR.InstrCorTurn, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemDateEdit("IntroductionDate");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("CorDate");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("PredictionCorDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            //GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_InstrImage, "FileName", "FileUrl", true);     // 2021-05-26 김진우 주임 주석처리
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InstrCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var instrCCodeName = tx_InstrCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p =>    (p.RBIDCode.Contains(instrCCodeName) || (p.RBIDName == instrCCodeName) || p.RBIDNameENG.Contains(instrCCodeName) || p.RBIDNameCHN.Contains(instrCCodeName))
                                                                    &&  (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                               )
                                                               .OrderBy(p => p.RBIDName)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl);
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
            var obj = GridBindingSource.Current as TN_MEA1500;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("InstrInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMEA1500, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

    }
}