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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.QCT_POPUP
{
    /// <summary>
    /// 20210924 오세완 차장
    /// 출하검사품목 추가 
    /// </summary>
    public partial class XPFQCT1900_MASTER : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_QCT1900> ModelService = (IService<TN_QCT1900>)ProductionFactory.GetDomainService("TN_QCT1900");
        private bool IsmultiSelect = true;
        #endregion

        public XPFQCT1900_MASTER()
        {
            InitializeComponent();
        }

        public XPFQCT1900_MASTER(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("PopupShipMaster");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitBindingSource() { }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            dt_OutDate.SetTodayIsMonth();

            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" &&
                                                                                                                                              p.TopCategory == MasterCodeSTR.TopCategory_WAN).ToList());
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OutNo", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            GridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            GridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));

            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("OrderQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");

            GridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("UserName", LabelConvert.GetLabelText("OutId"));
            GridExControl.MainGrid.AddColumn("Memo");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
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

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Datefrom = new SqlParameter("@DATE_FROM", dt_OutDate.DateFrEdit.DateTime.ToShortDateString());
                SqlParameter sp_Dateto = new SqlParameter("@DATE_TO", dt_OutDate.DateToEdit.DateTime.ToShortDateString());
                SqlParameter sp_Itemcode = new SqlParameter("@ITEM_CODE", lup_Item.EditValue.GetNullToEmpty());
                SqlParameter sp_Outno = new SqlParameter("@OUT_NO", textEdit1.EditValue.GetNullToEmpty());

                var vResult = context.Database.SqlQuery<TEMP_XPFQCT1900_LIST>("USP_GET_XPFQCT1900_LIST @DATE_FROM, @DATE_TO, @ITEM_CODE, @OUT_NO", sp_Datefrom, sp_Dateto, sp_Itemcode, sp_Outno).ToList();
                if (vResult != null)
                    if (vResult.Count > 0)
                        ModelBindingSource.DataSource = vResult.OrderBy(p=>p.OutDate).ToList();
            }

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            
            if (IsmultiSelect)
            {
                var returnList = new List<TEMP_XPFQCT1900_LIST>();
                List<TEMP_XPFQCT1900_LIST> tempList = ModelBindingSource.List as List<TEMP_XPFQCT1900_LIST>;
                foreach(TEMP_XPFQCT1900_LIST each in tempList)
                {
                    if (each._Check.GetNullToEmpty() == "Y")
                        returnList.Add(each);
                }

                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                var obj = (TEMP_XPFQCT1900_LIST)ModelBindingSource.Current;
                param.SetValue(PopupParameter.ReturnObject, obj);
            }

            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                if (IsmultiSelect)
                {
                    var returnList = new List<TEMP_XPFQCT1900_LIST>();
                    List<TEMP_XPFQCT1900_LIST> tempList = ModelBindingSource.List as List<TEMP_XPFQCT1900_LIST>;
                    foreach (TEMP_XPFQCT1900_LIST each in tempList)
                    {
                        if (each._Check.GetNullToEmpty() == "Y")
                            returnList.Add(each);
                    }

                    // 20211029 오세완 차장 유미대리 요청으로 단건 더블클릭 추가 기능 구현
                    if(returnList.Count == 0)
                    {
                        TEMP_XPFQCT1900_LIST selectObj = (TEMP_XPFQCT1900_LIST)ModelBindingSource.Current;
                        if(selectObj != null)
                            returnList.Add(selectObj);
                    }

                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    // 20211029 오세완 차장 1개도 list형태로 던지게 수정처리
                    var obj = (TEMP_XPFQCT1900_LIST)ModelBindingSource.Current;
                    List<TEMP_XPFQCT1900_LIST> returnList = new List<TEMP_XPFQCT1900_LIST>();
                    returnList.Add(obj);
                    //param.SetValue(PopupParameter.ReturnObject, obj);
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}