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
using System.Data.SqlClient;
using DevExpress.XtraEditors.Mask;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 20210819 오세완 차장 
    /// 이동표 새 출력시 박스내 수량, 프레스 공정에서는 공정이동표를 10*6 형태로 출력하기를 원해서 선택을 할 수 있게 하는 버전
    /// </summary>
    public partial class XPFITEMMOVEPRINT_BOX_V2 : Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        /// <summary>
        /// 20210819 오세완 차장 
        /// 부품이동표 양식 결정
        /// </summary>
        private string gs_DocType = "";

        /// <summary>
        /// 20210829 오세완 차장 
        /// 박스내 상품수량이 이동표를 발행한 총 수량을 넘는지 확인용
        /// </summary>
        private decimal gd_MaxQty = 0.0m;
        #endregion
        public XPFITEMMOVEPRINT_BOX_V2()
        {
            InitializeComponent();
        }

        public XPFITEMMOVEPRINT_BOX_V2(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("BoxInQty");

            this.MinimumSize = new Size(this.Size.Width, this.Size.Height - 40);
            //this.MaximumSize = new Size(this.Size.Width, this.Size.Height - 40);
            this.Size = this.MinimumSize;

            spin_BoxInQty.Click += Spin_Click;
            btn_Print.Click += Btn_Print_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_A4.Click += Btn_Choose_Click;
            btn_Bar.Click += Btn_Choose_Click;

            // 20210829 오세완 차장 박스수량 입력 추가
            spin_PerBoxQty.Click += Spin_Click;

            // 20210829 오세완 차장 입력을 하기 위해서는 더블클릭 이벤트 받기 위함
            //spin_BoxInQty.Enabled = false;

            // 20210829 오세완 차장 공정박스 수량 선택
            //lcBoxInQty.DoubleClick += LcBoxInQty_DoubleClick;

            btn_End_Without_Qty.Click += Btn_End_Without_Qty_Click;
        }

        /// <summary>
        /// 20211006 오세완 차장 
        /// 신부장님 요청으로 취소해야할 작업을 시작한 경우 무실적으로 처리할 수 있게 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_End_Without_Qty_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxHandler.Show("무실적으로 작업을 종료하시겠습니까?", "작업 종료", MessageBoxButtons.OKCancel);
            if(dr == DialogResult.OK)
            {
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.KeyValue, "NoQty");
                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }
        }

        /// <summary>
        /// 20210829 오세완 차장
        /// 공정이동박스 수량이 변경되는 것을 신부장님이 우려해서 기능 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LcBoxInQty_DoubleClick(object sender, EventArgs e)
        {
            string sMessage = "공정이동박스 수량을 변경하시겠습니까?";
            DialogResult dr = MessageBoxHandler.Show("수량변경", sMessage, MessageBoxButtons.YesNo);
            if(dr == DialogResult.OK)
            {
                spin_BoxInQty.Enabled = true;
            }
        }

        /// <summary>
        /// 20210820 오세완 차장 부품이동표 형태 선택 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Choose_Click(object sender, EventArgs e)
        {
            SimpleButton sb = sender as SimpleButton;
            if(sb.Name == "btn_Bar")
            {
                gs_DocType = "Barcode";
                btn_A4.Enabled = true;
                // 20211028 오세완 차장 button 색상을 넣어달라는 신부장님 요청으로 수정 designer에서 btn_a4 & bar를 참조
            }
            else
            {
                gs_DocType = "A4";
                btn_Bar.Enabled = true;
            }

            sb.Enabled = false;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            spin_BoxInQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BoxInQty.Properties.Mask.EditMask = "n0"; // n0로 변경
            spin_BoxInQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BoxInQty.Properties.Buttons[0].Visible = false;

            // 20210830 오세완 차장 공정박스수량 입력과 형식 동일하게 설정
            spin_PerBoxQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_PerBoxQty.Properties.Mask.EditMask = "n0"; // n0로 변경
            spin_PerBoxQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_PerBoxQty.Properties.Buttons[0].Visible = false;

            // 20210829 오세완 차장 신부장님 요청(사용자가 입력 잘못할 수 있다)으로 그냥 수량입력을 최초 1로 고정하여 조절하지 못하게 설정
            //spin_BoxInQty.EditValue = this.PopupParam.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();
            decimal dBoxInQty = this.PopupParam.GetValue(PopupParameter.Value_1).GetDecimalNullToZero();
            if (dBoxInQty == 0)
                spin_BoxInQty.EditValue = 1;
            else
                spin_BoxInQty.EditValue = dBoxInQty;

            btn_Print.Text = LabelConvert.GetLabelText("NewPrint");
            btn_Cancel.Text = LabelConvert.GetLabelText("Cancel");

            gd_MaxQty = this.PopupParam.GetValue(PopupParameter.Value_2).GetDecimalNullToZero();
            tx_CurrentQty.EditValue = gd_MaxQty.ToString();

            // 20211006 오세완 차장 무실적 완료 처리할 수 있게 버튼 출력 확인
            string sNoQty = this.PopupParam.GetValue(PopupParameter.Value_3).GetNullToEmpty();
            if (sNoQty == "")
                this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            if(gs_DocType.GetNullToEmpty() == "")
            {
                MessageBoxHandler.Show("출력 유형을 선택해 주세요.");
            }
            else
            {
                bool bResult = Check_PerBoxQty();
                if(bResult)
                {
                    PopupDataParam param = new PopupDataParam();
                    param.SetValue(PopupParameter.KeyValue, "Print"); // 20210915 오세완 차장 여기서 취소 여부를 선택해야 한다고 해서 상태를 구분짓기 위해 추가
                    param.SetValue(PopupParameter.Value_1, spin_BoxInQty.EditValue.GetDecimalNullToZero());
                    param.SetValue(PopupParameter.Value_2, gs_DocType); // 20210820 오세완 차장 부품이동표 양식 전달
                    param.SetValue(PopupParameter.Value_3, spin_PerBoxQty.EditValue.GetDecimalNullToZero()); // 20210829 오세완 차장 박스 수량 전달 추가
                    ReturnPopupArgument = new PopupArgument(param);

                    ActClose();
                }
            }
        }

        /// <summary>
        /// 20210830 오세완 차장
        /// 입력 수량 실적대비 확인 로직
        /// </summary>
        /// <returns></returns>
        private bool Check_PerBoxQty()
        {
            bool bOverQty = false;
            decimal dBoxQty = spin_BoxInQty.EditValue.GetDecimalNullToZero();
            decimal dPerBoxQty = spin_PerBoxQty.EditValue.GetDecimalNullToZero();
            decimal dTotal = dBoxQty * dPerBoxQty;
            string sMessage = "";
            if (dTotal == 0)
            {
                sMessage = "입력 수량 중 0이 있습니다.";
            }
            else if (gd_MaxQty < dTotal)
            {
                sMessage = "실적수량을 넘었습니다.";
            }
            else
                bOverQty = true;

            if(!bOverQty)
                MessageBoxHandler.Show(sMessage);

            return bOverQty;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            // 20210915 오세완 차장 여기서 취소한 경우 작업을 종료시키지 않게 하기 위해 구분
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.KeyValue, "Cancel");
            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void Spin_Click(object sender, EventArgs e)
        {
            var spinEdit = sender as SpinEdit;
            if (spinEdit == null) return;
            if (!GlobalVariable.KeyPad) return;

            var keyPad = new XFCNUMPAD(); // 20210830 오세완 차장 키패드는 필요 없어 보여서 숫자패드로 변경
            if (keyPad.ShowDialog() != DialogResult.Cancel)
            {
                // 20210906 오세완 차장 음수입력이 되어서 안되게 설정
                int iReturn;
                bool bResult = int.TryParse(keyPad.returnval, out iReturn);
                if (bResult)
                    spinEdit.EditValue = keyPad.returnval;
                else
                    spinEdit.EditValue = 0;
            }
        }
    }
}
