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
using HKInc.Service.Helper;
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

namespace HKInc.Ui.View.POP_Popup
{
    /// <summary>
    /// 안돈 POP 화면
    /// 2022-07-15 김진우
    /// </summary>
    public partial class XPFANDON : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_QCT1600> ANDONModel = (IService<TN_QCT1600>)ProductionFactory.GetDomainService("TN_QCT1600");
        TP_XFPOP1000_V2_LIST TEMP_XFPOP1000_Obj;

        BindingSource Bindings = new BindingSource();
        #endregion


        public XPFANDON()
        {
            InitializeComponent();

            //Btn_PROD.Tag = "DEPT02"; //생산
            //Btn_QCT.Tag = "DEPT01"; //품질

            //부서 사용x 공통코드 사용
            Btn_PROD.Tag = "01"; //생산
            Btn_QCT.Tag = "02"; //품질

            btn_Exit.Click += btn_Exit_Click;

            Btn_PROD.Click += Btn_Click;
            Btn_QCT.Click += Btn_Click;
        }
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        public XPFANDON(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = "안돈";
        }

        protected override void InitControls()
        {
            TEMP_XFPOP1000_Obj = (TP_XFPOP1000_V2_LIST)PopupParam.GetValue(PopupParameter.KeyValue);
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            Button BTN = sender as Button;

            ANDONModel.ReLoad();

            Bindings.DataSource = ANDONModel.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo
                                                && p.ProcessCode == TEMP_XFPOP1000_Obj.Process
                                                //&& p.MachineCode == TEMP_XFPOP1000_Obj.MachineCode
                                                && p.DepDivision == BTN.Tag.ToString()
                                                ).ToList();

            List<TN_QCT1600> List = Bindings.DataSource as List<TN_QCT1600>;

            try
            {
                var Obj = new TN_QCT1600();

                Obj.NewRowFlag = "Y";
                Obj.WorkNo = TEMP_XFPOP1000_Obj.WorkNo.GetNullToEmpty();
                Obj.ItemCode = TEMP_XFPOP1000_Obj.ItemCode.GetNullToEmpty();
                Obj.ProcessCode = TEMP_XFPOP1000_Obj.Process.GetNullToEmpty();
                Obj.MachineCode = TEMP_XFPOP1000_Obj.MachineCode.GetNullToEmpty();
                Obj.DepDivision = BTN.Tag.ToString().GetNullToEmpty();
                Obj.Seq = List.Count == 0 ? 1 : List.Max(p => p.Seq) + 1;
                Obj.CallDate = DateTime.Now.ToString("yyyy-MM-dd");
                Obj.ConfirmFlag = "N";
                Obj.CreateId = GlobalVariable.LoginId;
                Obj.CreateTime = DateTime.Today;

                Bindings.EndEdit();
                ANDONModel.Insert(Obj);
                ANDONModel.Save();

                //생산, 품질 호출 완료 메세지

                string MSG = "'" + BTN.Text + "'  호출";
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
