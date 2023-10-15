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
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Helper;
using DevExpress.XtraBars;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.BAN_POPUP
{
    /// <summary>
    /// 반제품 재입고 수정 팝업
    /// </summary>
    public partial class XPFBAN1102 : Service.Base.PopupCallbackFormTemplate
    {
        private IService<TN_BAN1102> ModelService = (IService<TN_BAN1102>)ProductionFactory.GetDomainService("TN_BAN1102");
        private string inLotNo;
        private List<VI_RETURN_OBJECT> VI_RETURN_OBJECT_List;

        public XPFBAN1102()
        {
            InitializeComponent();
        }

        public XPFBAN1102(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ReturnInfo");

            if (parameter.ContainsKey(PopupParameter.KeyValue))
                inLotNo = parameter.GetValue(PopupParameter.KeyValue).GetNullToEmpty();

            tx_InLotNo.EditValue = inLotNo;

            GridExControl = gridEx1;
        }

        protected override void InitBindingSource() { }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(UserRight.HasEdit);
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("ReturnDate", LabelConvert.GetLabelText("ReturnDate2"));
            GridExControl.MainGrid.AddColumn("ReturnId", LabelConvert.GetLabelText("ReturnId"));
            GridExControl.MainGrid.AddColumn("TN_BAN1101.TN_BAN1001.InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("ReturnPossibleQty", LabelConvert.GetLabelText("ReturnPossibleQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("ReturnQty", LabelConvert.GetLabelText("ReturnQty"));
            GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            GridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            GridExControl.MainGrid.AddColumn("ReturnWhCode", LabelConvert.GetLabelText("ReturnWhCode"));
            GridExControl.MainGrid.AddColumn("ReturnWhPosition", LabelConvert.GetLabelText("ReturnWhPosition"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReturnDate", "ReturnId", "ReturnQty", "Memo", "ReturnWhCode", "ReturnWhPosition");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ReturnDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("ReturnQty", DefaultBoolean.Default, "n2");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_BAN || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);

            var ReturnWhPositionEdit = GridExControl.MainGrid.Columns["ReturnWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            ReturnWhPositionEdit.Popup += ReturnWhPositionEdit_Popup;
        }

        protected override void InitDataLoad()
        {
            VI_RETURN_OBJECT_List = ModelService.GetChildList<VI_RETURN_OBJECT>(p => p.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing).ToList(); // 20210524 오세완 차장 반제품(타사) 반품처리 할 수 있도록 추가
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            ModelBindingSource.DataSource = ModelService.GetList(p => p.InLotNo == inLotNo).ToList();
            //ModelBindingSource.DataSource = ModelService.GetList(p => false).ToList();

            GridExControl.DataSource = ModelBindingSource;

            var list = ModelBindingSource.List as List<TN_BAN1102>;
            if (list.Count > 0)
            {
                foreach (var v in list)
                {
                    var VI_RETURN_OBJECT = VI_RETURN_OBJECT_List.Where(p => p.OutLotNo == v.OutLotNo).FirstOrDefault();
                    if (VI_RETURN_OBJECT != null)
                    {
                        v.ReturnPossibleQty = v.ReturnQty + VI_RETURN_OBJECT.ReturnPossibleQty;
                    }
                }
            }

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            ModelBindingSource.EndEdit();

            var list = ModelBindingSource.List as List<TN_BAN1102>;
            if (list.Count > 0)
            {
                if (list.Any(p => p.ReturnQty > p.ReturnPossibleQty))
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_61));
                    SetSaveMessageCheck = false;
                    return;
                }
            }

            ModelService.Save();
            IsFormControlChanged = false;
            ActClose();
        }

        private void ReturnWhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = ModelBindingSource.Current as TN_BAN1102;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.ReturnWhCode + "'";
        }
    }
}