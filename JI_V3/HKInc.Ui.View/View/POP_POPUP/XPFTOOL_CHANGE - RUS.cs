using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraEditors.Mask;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Data.SqlClient;


namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 설비별 공구 리스트 등록
    /// </summary>
    public partial class XPFTOOL_CHANGE_RUS : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_TOOL1000> ModelService = (IService<TN_TOOL1000>)ProductionFactory.GetDomainService("TN_TOOL1000");
        IService<TN_TOOL1101> MachineToolModel = (IService<TN_TOOL1101>)ProductionFactory.GetDomainService("TN_TOOL1101");
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        BindingSource GridBindings = new BindingSource();
        int cnt = 0;
        private bool IsmultiSelect = true;

        #endregion

        public XPFTOOL_CHANGE_RUS()
        {
            InitializeComponent();

            btn_ToolChange.Click += btn_ToolChange_Click;
            btn_Exit.Click += btn_Exit_Click;

            gridEx1.MainGrid.MainView.RowCellStyle += View_RowCellStyle;

            gridEx1.MainGrid.MainView.RowCellClick += View_RowCellClick;

        }
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        public XPFTOOL_CHANGE_RUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            Label_MachineInfo.Text = "";

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

        }

        protected override void InitGrid()
        {
            gridEx1.SetToolbarVisible(false);

            gridEx1.MainGrid.CheckBoxMultiSelect(true, "ToolCode", IsmultiSelect);

            gridEx1.MainGrid.AddColumn("_Check", " ", HorzAlignment.Center, true);
            gridEx1.MainGrid.AddColumn("ToolCode", "공구명");
            gridEx1.MainGrid.AddColumn("BaseCNT", "기본수명", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("LifeCNT", "남은수명", HorzAlignment.Far, FormatType.Numeric, "n0");
            gridEx1.MainGrid.AddColumn("ChangeCNT", "교체수명", HorzAlignment.Far, FormatType.Numeric, "n0");

            gridEx1.MainGrid.SetGridFont(gridEx1.MainGrid.MainView, new Font("맑은 고딕", 12f));
            gridEx1.MainGrid.MainView.ColumnPanelRowHeight = 25;
            gridEx1.MainGrid.MainView.RowHeight = 37;

            //gridEx1.MainGrid.SetEditable("_Check", "LifeCNT");

            //if (GlobalVariable.KeyPad)
            //    gridEx1.MainGrid.SetEditable("_Check");
            //else
                gridEx1.MainGrid.SetEditable("_Check", "ChangeCNT");
        }

        protected override void InitRepository()
        {
            gridEx1.MainGrid.MainView.Columns["_Check"].MinWidth = 60;
            gridEx1.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ToolCode", ModelService.GetChildList<TN_TOOL1000>(p => p.UseFlag == "Y").ToList(), "ToolCode", "ToolName");
        }

        protected override void InitControls()
        {
            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)PopupParam.GetValue(PopupParameter.KeyValue);
        }

        protected override void DataLoad()
        {
            //var ToolObj = MachineToolModel.GetChildList<TN_TOOL1101>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
            //                                               && p.MachineCode == TEMP_XFPOP1000_Obj.MachineCode
            //                                               && p.ItemCode == TEMP_XFPOP1000_Obj.ItemCode
            //                                               && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode)
            //                                               .OrderBy(p => p.ToolCode).ToList();
            try
            {
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);
                    var MachineCode = new SqlParameter("@MachineCode", TEMP_XFPOP1000_Obj.MachineCode);
                    var ItemCode = new SqlParameter("@ItemCode", TEMP_XFPOP1000_Obj.ItemCode);
                    var ProcessCode = new SqlParameter("@ProcessCode", TEMP_XFPOP1000_Obj.ProcessCode);

                    var result = context.Database.SqlQuery<TEMP_XPFTOOL_CHANGE>("USP_GET_XPFTOOL_CHANGE @WorkNo, @MachineCode, @ItemCode, @ProcessCode",
                                                                                          WorkNo, MachineCode, ItemCode, ProcessCode).ToList();

                    GridBindings.DataSource = result.ToList();
                    gridEx1.DataSource = GridBindings;
                }
            }
            catch(Exception ex)
            { }


        }


        private void btn_ToolChange_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gv = gridEx1.MainGrid.MainView as GridView;
                int rowcnt = 0;

                string MSG = string.Empty;

                //List<TN_TOOL1101> Grid = GridBindings.DataSource as List<TN_TOOL1101>;
                List<TEMP_XPFTOOL_CHANGE> Grid = GridBindings.DataSource as List<TEMP_XPFTOOL_CHANGE>;

                var List = new List<TEMP_XPFTOOL_CHANGE>();
                List = Grid.Where(p => p._Check == "Y").ToList();

                if(List.Count == 0)
                {
                    MessageBoxHandler.Show("교체 공구 미선택", "");
                    return;
                }

                foreach (var v in List)
                {
                    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                    {
                        var WorkNo = new SqlParameter("@WorkNo", TEMP_XFPOP1000_Obj.WorkNo);
                        var MachineCode = new SqlParameter("@MachineCode", TEMP_XFPOP1000_Obj.MachineCode);
                        var ItemCode = new SqlParameter("@ItemCode", TEMP_XFPOP1000_Obj.ItemCode);
                        var ProcessCode = new SqlParameter("@ProcessCode", TEMP_XFPOP1000_Obj.ProcessCode);
                        var ToolCode = new SqlParameter("@ToolCode", v.ToolCode);

                        var BaseCNT = new SqlParameter("@BaseCNT", v.BaseCNT);
                        var LifeCNT = new SqlParameter("@LifeCNT", v.LifeCNT);
                        var ChangeCNT = new SqlParameter("@ChangeCNT", v.ChangeCNT);

                        var ResultSumQty = new SqlParameter("@ResultSumQty", TEMP_XFPOP1000_Obj.ResultSumQty);


                        var LoginId = new SqlParameter("@LoginId", GlobalVariable.LoginId);

                        MSG = context.Database.SqlQuery<string>

                            ("USP_INS_TOOL_CHANGE @WorkNo, @MachineCode, @ItemCode, @ProcessCode, @ToolCode, @ResultSumQty, @BaseCNT, @LifeCNT, @ChangeCNT, @LoginId",
                                                   WorkNo,  MachineCode,  ItemCode,  ProcessCode,  ToolCode,  ResultSumQty,  BaseCNT,  LifeCNT,  ChangeCNT,  LoginId).SingleOrDefault();
                    }
                }

                if(MSG != "OK")
                {
                    MessageBoxHandler.Show(MSG, "");
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.Message, "");
            }

        }

        private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle < 0) return;

            int cnt = View.GetRowCellValue(e.RowHandle, View.Columns["LifeCNT"]).GetIntNullToZero();

            /*
            if (cnt >= 30)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.LimeGreen;

            else if (1 <= cnt && cnt <= 29)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Gold;

            else
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Red;
            */


            if (11 <= cnt && cnt <= 30)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Green;
            else if (1 <= cnt && cnt <= 10)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Gold;
            else if (cnt <= 0)
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Red;
            else
                View.Columns["LifeCNT"].AppearanceCell.ForeColor = Color.Black;

            e.Appearance.BackColor = Color.White;
            //View.Columns["ChangeCNT"].AppearanceCell.BackColor = Color.Yellow;

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            //Close();
        }

        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (!GlobalVariable.KeyPad)
                return;

            GridView gridview = sender as GridView;

            string fieldName = e.Column.FieldName;

            if (fieldName != "ChangeCNT") return;

            var keyPad = new XFCNUMPAD();

            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                gridview.SetFocusedRowCellValue(fieldName, keyPad.returnval);
            }
        }
    }
}
