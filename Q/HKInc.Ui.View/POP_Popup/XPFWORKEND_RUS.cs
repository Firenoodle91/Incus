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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using System.Data.SqlClient;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.POP_Popup
{
    /// <summary>
    /// 작업종료 러시아
    /// 2022-08-16 김진우 추가
    /// </summary>
    public partial class XPFWORKEND_RUS : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_MPS1400> ModelService = (IService<TN_MPS1400>)ProductionFactory.GetDomainService("TN_MPS1400");

        TP_XFPOP1000_V2_LIST TEMP_XFPOP1000_Obj;
        private string productLotNo;
        #endregion

        public XPFWORKEND_RUS()
        {
            InitializeComponent();
        }

        public XPFWORKEND_RUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            //this.Text = LabelConvert.GetLabelText("WorkEnd");
            this.Text = "конец работы";             // 작업종료

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);

            TEMP_XFPOP1000_Obj = (TP_XFPOP1000_V2_LIST)parameter.GetValue(PopupParameter.KeyValue);
            productLotNo = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            btn_ResultAdd.Click += Btn_ResultAdd_Click;
            btn_WorkEnd.Click += Btn_WorkEnd_Click; 
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_QualityAdd.Click += Btn_QualityAdd_Click;
            btn_Pause.Click += Btn_Pause_Click;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        /// <summary>
        /// 실적 등록
        /// </summary>
        private void Btn_ResultAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null)
                return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.Value_1, productLotNo);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFRESULT_DEFAULT_RUS, param, ResultAddCallback);
            form.ShowPopup(true);
        }

        private void ResultAddCallback(object sender, PopupArgument e)
        {
            if (e == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, "SAVE");
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void Btn_WorkEnd_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();
            if (TEMP_XFPOP1000_Obj == null)
                return;

            string sMessage = "";

            // 20220317 오세완 차장 거의 외주를 한 적이 없어서 일단 외주제외 로직은 넣었으나 확인은 해봐야 함
            TN_MPS1400 preProcessObj = ModelService.GetList(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo && 
                                                                 p.PSeq == TEMP_XFPOP1000_Obj.PSeq - 1 && 
                                                                 p.OutProc != "Y").FirstOrDefault();


            if (preProcessObj != null)
            {
                if ((Convert.ToInt32(preProcessObj.JobStates) != (int)MasterCodeEnum.POP_Status_End))
                {
                    // 이전 공정에 대하여 작업이 완료되어 있지 않습니다. 확인 부탁드립니다.
                    MessageBoxHandler.Show("Предыдущий процесс не завершен. Прошу вас проверить.");
                    return;
                }
            }

            List<TN_MPS1401> sumObj = ModelService.GetChildList<TN_MPS1401>(p => p.WorkNo == TEMP_XFPOP1000_Obj.WorkNo &&
                                                                                 p.ProcessTurn == TEMP_XFPOP1000_Obj.PSeq &&
                                                                                 p.ProcessCode == TEMP_XFPOP1000_Obj.Process ).ToList();

            if (TEMP_XFPOP1000_Obj.PlanQty > sumObj.Sum(p => p.ResultQty).GetDecimalNullToZero())
            {
                // 지시수량보다 총 생산량이 부족합니다. 무시하고 종료하시겠습니까?
                if (MessageBoxHandler.Show("Общий объем производства меньше, чем указано. Игнорировать и закрыть?",
                    "확인", MessageBoxButtons.YesNo) == DialogResult.No) return;
            }

            // 정말로 종료하시겠습니까?
            if (MessageBoxHandler.Show("Вы действительно хотите выйти?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sSql = "exec SP_JOBSTATUS_UP '" + TEMP_XFPOP1000_Obj.WorkNo + "', '" + TEMP_XFPOP1000_Obj.PSeq.ToString() + "', '" + TEMP_XFPOP1000_Obj.Process + "', '" + TEMP_XFPOP1000_Obj.ItemCode 
                    + "', '" + (int)MasterCodeEnum.POP_Status_End + "' ";
                string sResult = DbRequestHandler.GetCellValue(sSql, 0);

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, "SAVE");
                param.SetValue(PopupParameter.Value_2, TEMP_XFPOP1000_Obj);
                ReturnPopupArgument = new PopupArgument(param);
                ActClose();
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ActClose();
        }

        private void Btn_QualityAdd_Click(object sender, EventArgs e)
        {
            var obj = TEMP_XFPOP1000_Obj;
            if (obj == null) return;

            var inspectionForm = new POP_Popup.XPFINSPECTION_RUS(obj, productLotNo);
            inspectionForm.ShowDialog();
        }

        private void Btn_Pause_Click(object sender, EventArgs e)
        {
            ModelService.ReLoad();

            if(TEMP_XFPOP1000_Obj != null)
            {
                string sSql = "exec SP_JOBSTATUS_UP '" + TEMP_XFPOP1000_Obj.WorkNo + "', " + TEMP_XFPOP1000_Obj.PSeq.ToString() + ", '" + TEMP_XFPOP1000_Obj.Process + "', '" + TEMP_XFPOP1000_Obj.ItemCode
                    + "', '" + Convert.ToInt16(MasterCodeEnum.POP_Status_StopWait).ToString() + "' ";
                string sResult = DbRequestHandler.GetCellValue(sSql, 0);

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, "SAVE");
                param.SetValue(PopupParameter.Value_2, "STOP_WAIT"); // 20220503 오세완 차장 이게 없으면 새로고침이 안되서 추가
                ReturnPopupArgument = new PopupArgument(param);
            }
            
            ActClose();
        }
    }
}
