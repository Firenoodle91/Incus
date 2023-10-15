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
    public partial class XPFANDON_RUS : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_QCT1600> ANDONModel = (IService<TN_QCT1600>)ProductionFactory.GetDomainService("TN_QCT1600");
        TEMP_XFPOP1000 TEMP_XFPOP1000_Obj;

        BindingSource Bindings = new BindingSource();
        #endregion


        public XPFANDON_RUS()
        {
            InitializeComponent();

            Btn_PROD.Tag = "DEPT-00002"; //생산
            Btn_QCT.Tag = "DEPT-00003"; //품질

            btn_Exit.Click += btn_Exit_Click;

            Btn_PROD.Click += Btn_Click;
            Btn_QCT.Click += Btn_Click;
        }
        public XPFANDON_RUS(string Workno,string item,string proc,string mc)
        {
            InitializeComponent();
            TEMP_XFPOP1000_Obj = new TEMP_XFPOP1000();
            TEMP_XFPOP1000_Obj.WorkNo = Workno;
            TEMP_XFPOP1000_Obj.ItemCode = item;
            TEMP_XFPOP1000_Obj.ProcessCode = proc;
            TEMP_XFPOP1000_Obj.MachineCode = mc;
            Btn_PROD.Tag = "DEPT-00002"; //생산
            Btn_QCT.Tag = "DEPT-00003"; //품질

            btn_Exit.Click += btn_Exit_Click;

            Btn_PROD.Click += Btn_Click;
            Btn_QCT.Click += Btn_Click;
        }
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        public XPFANDON_RUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = "Андон";        // 안돈
            TEMP_XFPOP1000_Obj = (TEMP_XFPOP1000)PopupParam.GetValue(PopupParameter.KeyValue);
        }

        protected override void InitControls()
        {
        
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            Button BTN = sender as Button;

            ANDONModel.ReLoad();

            Bindings.DataSource = ANDONModel.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                && p.ProcessCode == TEMP_XFPOP1000_Obj.ProcessCode
                                                && p.MachineCode == TEMP_XFPOP1000_Obj.MachineCode
                                                && p.DepDivision == BTN.Tag.ToString()
                                                ).ToList();

            List<TN_QCT1600> List = Bindings.DataSource as List<TN_QCT1600>;

            try
            {
                var Obj = new TN_QCT1600()
                {
                    NewRowFlag = "Y",
                    WorkNo = TEMP_XFPOP1000_Obj.WorkNo,
                    ItemCode = TEMP_XFPOP1000_Obj.ItemCode,
                    ProcessCode = TEMP_XFPOP1000_Obj.ProcessCode,
                    MachineCode = TEMP_XFPOP1000_Obj.MachineCode,
                    DepDivision = BTN.Tag.ToString(),
                    Seq = List.Count == 0 ? 1 : List.Max(p => p.Seq) + 1,
                    CallDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    ConfirmFlag = "N",
                    CreateId = GlobalVariable.LoginId,
                    CreateTime = DateTime.Today
                };

                Bindings.EndEdit();
                ANDONModel.Insert(Obj);
                ANDONModel.Save();

                //생산, 품질 호출 완료 메세지

                string MSG = "'" + BTN.Text + "'  вызов";       // 호출
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.ReturnObject, MSG);
                ReturnPopupArgument = new PopupArgument(param);

                MessageBoxHandler.Show(MSG, "");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.Message + ex.StackTrace, "");
            }


        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
