using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Handler;
using System.Data.SqlClient;

namespace HKInc.Ui.View.MPS_Popup
{
    public partial class XPFMPS1301 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1400> ModelService = (IService<TN_MPS1400>)ProductionFactory.GetDomainService("TN_MPS1400");

        public XPFMPS1301()
        {
            InitializeComponent();
            this.Text = "작업지시순서관리";
            GridExControl = gridEx1;
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();
            SetToolbarButtonVisible(ToolbarButton.Print, false);
            SetToolbarButtonVisible(ToolbarButton.Export, false);
        }

        protected override void InitCombo()
        {
            
        }

        protected override void InitGrid()
        {
            //GridExControl.SetToolbarVisible(false);
            //GridExControl.MainGrid.AddColumn("JobStates", "상태");
            //GridExControl.MainGrid.AddColumn("DisplayOrder", "작업지시순서");
            //GridExControl.MainGrid.AddColumn("PlanNo", "생산계획번호", false);
            //GridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            //GridExControl.MainGrid.AddColumn("WorkDate", "작업지시일");
            //GridExControl.MainGrid.AddColumn("PSeq", "공정순서", HorzAlignment.Far, true);
            //GridExControl.MainGrid.AddColumn("Process", "공정");
            //GridExControl.MainGrid.AddColumn("MachineCode", "설비");
            //GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            //GridExControl.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("DelivDate", "납품계획일", false);
            //GridExControl.MainGrid.AddColumn("OrderNo", "수주번호", false);
            //GridExControl.MainGrid.AddColumn("WorkId", "작업자", false);
            //GridExControl.MainGrid.AddColumn("Memo", "비고");
            //GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DisplayOrder");

            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("JobStatus", "작업상태", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("DisplayOrder", "작업지시순서");
            GridExControl.MainGrid.AddColumn("WorkDate", "작업지시일", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호", HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("ProcessName", "공정");
            GridExControl.MainGrid.AddColumn("MachineName", "설비");
            GridExControl.MainGrid.AddColumn("ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("PlanQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("StdPackQty", "박스당수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DisplayOrder");
            GridExControl.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            //GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //GridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStates", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
            //GridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");

            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("JobStatus", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("DisplayOrder");
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var DateTime = dateWorkDate.DateTime;
            if (DateTime.GetDateTimeToNullCheck())
            {
                MessageBoxHandler.Show("작업지시일은 필수 조건입니다.", "경고");
                return;
            }
            else
            {
                GridExControl.MainGrid.Clear();

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    var process = new SqlParameter("@process", "");
                    var mccode = new SqlParameter("@mccode", "");
                    var itemcode = new SqlParameter("@itemcode", "");
                    var wkno = new SqlParameter("@wkpno", "");
                    var workDate = new SqlParameter("@WorkDate", DateTime);
                    var result = context.Database
                          .SqlQuery<TP_POPJOBLIST>("SP_POP_JOBLIST @process,@mccode ,@itemcode ,@wkpno, @WorkDate", process, mccode, itemcode, wkno, workDate).ToList();
                    GridBindingSource.DataSource = result.OrderBy(p => p.DisplayOrder).ThenBy(p => p.PSeq).ToList();

                }
                GridExControl.DataSource = GridBindingSource;
                GridExControl.MainGrid.BestFitColumns();
            }
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            var SaveList = GridBindingSource.List as List<TP_POPJOBLIST>;
            if (SaveList != null && SaveList.Count > 0)
            {
                //foreach (var v in SaveList)
                //{
                //    ModelService.Update()
                //}
                //using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                //{                    
                //    var _saveList = new SqlParameter("@SaveList", SqlDbType.Structured);
                //    _saveList.Value = ConvertToDataTable(SaveList);
                //    var result = context.Database
                //          .SqlQuery<int>("USP_UPD_XPFMPS1301 @SaveList", _saveList).SingleOrDefault();
                //}

                //using (var conn = new SqlConnection(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                //{
                //    using (var sproc = new SqlCommand("USP_UPD_XPFMPS1301", conn))
                //    {
                //        var param = new SqlParameter("@SaveList", SqlDbType.Structured);
                //        param.TypeName = "MPS1301_SAVE";
                //        param.SqlValue = ConvertToDataTable(SaveList);
                //        sproc.Parameters.Add(param);

                //        if (conn.State != ConnectionState.Open) conn.Open();
                //            sproc.();
                //    }
                //}

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    System.Data.SqlClient.SqlParameter _SaveList = new System.Data.SqlClient.SqlParameter("@SaveList", SqlDbType.Structured);

                    _SaveList.Value = ConvertToDataTable(SaveList);
                    _SaveList.TypeName = "dbo.MPS1301_SAVE";
                    var result = context.Database
                         .ExecuteSqlCommand("USP_UPD_XPFMPS1301 @SaveList", _SaveList);

                }
            }
            //ModelService.Save();
            ActRefresh();
        }

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.Name.Contains("Nullable"))
                {
                    if (prop.PropertyType.GenericTypeArguments[0].Name == "Int32")
                    {
                        var column = new DataColumn(prop.Name, Type.GetType("System.Int32"))
                        {
                            AllowDBNull = true
                        };
                        table.Columns.Add(column);
                    }
                    else if(prop.PropertyType.GenericTypeArguments[0].Name == "DateTime")
                    {
                        var column = new DataColumn(prop.Name, Type.GetType("System.DateTime"))
                        {
                            AllowDBNull = true
                        };
                        table.Columns.Add(column);
                    }
                }
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

    }
}