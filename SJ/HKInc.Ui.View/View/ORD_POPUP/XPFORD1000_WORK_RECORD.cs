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
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;



namespace HKInc.Ui.View.View.ORD_POPUP
{
    /// <summary>
    /// 작업지시생성 시 이력보여주기
    /// </summary>
    public partial class XPFORD1000_WORK_RECORD : Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_XFORD1102_MASTER_VIEW> ModelService = (IService<VI_XFORD1102_MASTER_VIEW>)ProductionFactory.GetDomainService("VI_XFORD1102_MASTER_VIEW");

        private string itemCode;

        public XPFORD1000_WORK_RECORD()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            btn_Ok.Click += Btn_Ok_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
        }

        public XPFORD1000_WORK_RECORD(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = "작업지시이력";

            itemCode = parameter.GetValue(PopupParameter.KeyValue).GetNullToEmpty();
            tx_ItemCode.EditValue = itemCode;
            tx_ItemCode.ReadOnly = true;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitBindingSource(){}
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("WorkNoDate", LabelConvert.GetLabelText("PublishDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("PlanWorkQty", LabelConvert.GetLabelText("PlanWorkQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            GridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            GridExControl.MainGrid.AddColumn("PackQty", LabelConvert.GetLabelText("PackQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
        }

        protected override void InitRepository()
        {           
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var ItemCode = new SqlParameter("@ItemCode", itemCode);
                var result = context.Database.SqlQuery<TEMP_WORK_RECORD>("USP_GET_WORK_RECORD @ItemCode", ItemCode).ToList();
                ModelBindingSource.DataSource = result.ToList();
            }

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, "OK");
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ActClose();
        }

    }
}