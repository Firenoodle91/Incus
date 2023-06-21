using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using DevExpress.XtraEditors;
using DevExpress.Utils;

using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;


namespace HKInc.Ui.View.View.TOOL
{
    /// <summary>
    /// 툴 입출고관리
    /// </summary>
    public partial class XFTOOL1001 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_TOOL1001> ModelService = (IService<TN_TOOL1001>)ProductionFactory.GetDomainService("TN_TOOL1001");

        public XFTOOL1001()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lup_Tool.SetDefault(false, true, "ToolCode", DataConvert.GetCultureDataFieldName("ToolName"), ModelService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("ToolCode", LabelConvert.GetLabelText("ToolCode"));
            MasterGridExControl.MainGrid.AddColumn("ToolName", LabelConvert.GetLabelText("ToolName"));
            //MasterGridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("ToolNameENG"),false);
            //MasterGridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("ToolNameCHN"),false);
            MasterGridExControl.MainGrid.AddColumn("ToolStockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "n2");
            //MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            MasterGridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            MasterGridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            MasterGridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            MasterGridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), false);
            //MasterGridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"), false);
            //MasterGridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            //MasterGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            //MasterGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), false);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", LabelConvert.GetLabelText("Date"));
            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"));
            DetailGridExControl.MainGrid.AddColumn("Qty", LabelConvert.GetLabelText("Qty"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("InOutId", LabelConvert.GetLabelText("CreateId"));
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InOutDate", "Division", "Qty", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1201>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            //MasterGridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl", false);
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", false);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InOutDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InOutDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Qty");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ToolCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var toolCode = new SqlParameter("@ToolCode", lup_Tool.EditValue.GetNullToEmpty());
                var result = context.Database.SqlQuery<TEMP_XFTOOL1001_MASTER>
                    ("USP_GET_XFTOOL1001_MASTER @ToolCode", toolCode).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();

            }

            //MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(toolCode) ? true : p.ItemCode == toolCode)
            //                                                        && (p.UseFlag == "Y")
            //                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)
            //                                                   )
            //                                                   .OrderBy(p => p.ItemName)
            //                                                   .ToList();

            //MasterGridExControl.DataSource = MasterGridBindingSource;
            //MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TEMP_XFTOOL1001_MASTER;
            if (masterObj == null) return;
            

            DetailGridBindingSource.DataSource = ModelService.GetList(p => p.ItemCode == masterObj.ToolCode).OrderBy(p => p.CreateTime).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_XFTOOL1001_MASTER;
            if (masterObj == null) return;

            List<TN_TOOL1001> DetailList = DetailGridBindingSource.DataSource as List<TN_TOOL1001>;


            //if (!masterObj.TN_TOOL1001List.Any(p => p.NewRowFlag == "Y"))
            //{
            var newObj = new TN_TOOL1001()
            {
                ItemCode = masterObj.ToolCode,
                Seq = DetailList.Count == 0 ? 1 : DetailList.Max(p => p.Seq) + 1,
                InOutDate = DateTime.Today,
                Qty = 0,
                InOutId = GlobalVariable.LoginId
            };
            //masterObj.TN_TOOL1001List.Add(newObj);
            DetailGridBindingSource.Add(newObj);
            DetailGridExControl.BestFitColumns();
            //}
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TEMP_XFTOOL1001_MASTER;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_TOOL1001;
            if (detailObj == null) return;

            //masterObj.TN_TOOL1001List.Remove(detailObj);

            ModelService.Delete(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();


            List<TN_TOOL1001> List = DetailGridBindingSource.DataSource as List<TN_TOOL1001>;

            foreach (var v in List)
            {
                //추가
                if (v.NewRowFlag == "Y")
                {
                    ModelService.Insert(v);
                }
                //수정
                else if (v.NewRowFlag == "N" && v.EditRowFlag == "Y")
                {
                    ModelService.Update(v);
                }
            }

            ModelService.Save();
            DataLoad();
        }
    }
}