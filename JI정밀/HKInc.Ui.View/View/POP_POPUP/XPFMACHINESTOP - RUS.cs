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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.XtraReports.UI;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 비가동 팝업 창
    /// </summary>
    public partial class XPFMACHINESTOP_RUS : Service.Base.BaseForm
    {
        IService<TN_MEA1004> ModelService = (IService<TN_MEA1004>)ProductionFactory.GetDomainService("TN_MEA1004");
        //List<LayoutControlItem> layoutItem_StopTypeList = new List<LayoutControlItem>();
        //List<Control> btn_StopTypeList = new List<Control>();
        Control[] btn_StopTypeList;
        private TEMP_XFPOP1000 workObj;

        public XPFMACHINESTOP_RUS(TEMP_XFPOP1000 tempObj)
        {
            InitializeComponent();

            workObj = tempObj;
            lup_Machine.EditValue = workObj.MachineCode;

            lup_MachineGroup.EditValueChanged += Lup_MachineGroup_EditValueChanged;
            lup_Machine.Popup += Lup_Machine_Popup;
            lup_Machine.EditValueChanged += Lup_Machine_EditValueChanged;

            btn_Cancel.Click += Btn_Cancel_Click;

            lup_Machine.Properties.ReadOnly = true; //수정 못하게 픽스

            this.Text = LabelConvert.GetLabelText("StopAdd");

            //this.Size = new Size(517, 551);
            this.WindowState = FormWindowState.Maximized;



            
        }

        protected override void InitControls()
        {
            base.InitControls(); //없애면 안됨.

            var TN_STD1000_List = DbRequestHandler.GetCommTopCode(MasterCodeSTR.StopType);

            var cultureIndex = DataConvert.GetCultureIndex();

            btn_StopTypeList = new Control[TN_STD1000_List.Count];
            int j = TN_STD1000_List.Count / 4 + 1;
            int k = 280 / j > 70 ? 70 : 280 / j;

            for (int i = 0; i < TN_STD1000_List.Count; i++)
            {
                btn_StopTypeList[i] = new SimpleButton();
                btn_StopTypeList[i].Name = TN_STD1000_List[i].CodeVal;
                btn_StopTypeList[i].Parent = this.panel_Button;

                var value = i / 4;
                var height1 = ((i / 4 * k) + (value * 10) + 30);

                switch (i % 4)
                {
                    case 0:
                        btn_StopTypeList[i].Location = new Point(30, height1); //new Point(30, i / 4 * k + 30);
                        break;
                    case 1:
                        btn_StopTypeList[i].Location = new Point(200, height1);
                        break;
                    case 2:
                        btn_StopTypeList[i].Location = new Point(370, height1);
                        break;
                    case 3:
                        btn_StopTypeList[i].Location = new Point(540, height1);
                        break;
                }

                btn_StopTypeList[i].Size = new Size(150, k);
                btn_StopTypeList[i].Text = cultureIndex == 1 ? TN_STD1000_List[i].CodeName : (cultureIndex == 2 ? TN_STD1000_List[i].CodeNameENG : TN_STD1000_List[i].CodeNameCHN);
                btn_StopTypeList[i].Font = new Font("맑은 고딕", 15f, FontStyle.Bold);
                btn_StopTypeList[i].Click += new EventHandler(Btn_StopTypeClick);
            }

            SetBtnStopTypeEnable(false);
        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarVisible(false);
        }

        protected override void InitCombo()
        {
            lup_MachineGroup.SetDefaultPOP(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineGroup), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_MachineGroup.SetFontSize(new Font("맑은 고딕", 15f));
            //lup_MachineGroup.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            lup_Machine.SetDefaultPOP(false, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_Machine.SetFontSize(new Font("맑은 고딕", 12f));
            lup_Machine.Properties.View.Columns["MachineMCode"].Visible = false;
            //lup_Machine.ColumnVisibleIndexChange("MachineCode", 1);
            //lup_Machine.ColumnVisibleIndexChange(DataConvert.GetCultureDataFieldName("MachineName"), 2);
            lup_Machine.AddColumnDisplay("MachineStopStates", LabelConvert.GetLabelText("MachineStopStates"), 3, true);
            
            dt_StopStartDate.ReadOnly = true;
            dt_StopStartDate.SetFormat(Utils.Enum.DateFormat.DateAndTimeSecond);

            //lup_Machine.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            dt_StopStartDate.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        }

        /// <summary>
        /// 설비 변경 이벤트
        /// </summary>
        private void Lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            var edit = sender as SearchLookUpEdit;

            var value = edit.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty())
            {
                SetBtnStopTypeEnable(false);
                dt_StopStartDate.EditValue = null;
            }
            else
            {
                var stopObj = ModelService.GetList(p => p.MachineCode == value && p.StopStartTime != null && p.StopEndTime == null).FirstOrDefault();
                if (stopObj == null)
                {
                    SetBtnStopTypeEnable(true);
                    dt_StopStartDate.EditValue = null;
                }
                else
                {
                    SetBtnStopTypeEnable(false);
                    SetBtnStopTypeEnable(true, stopObj.StopCode);
                    dt_StopStartDate.EditValue = stopObj.StopStartTime;
                }
            }
        }

        /// <summary>
        /// 취소 클릭 이벤트
        /// </summary>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 비가동유형 선택
        /// </summary>
        private void Btn_StopTypeClick(object sender, EventArgs e)
        {
            var control = sender as Control;

            var machineCode = lup_Machine.EditValue.GetNullToEmpty();

            var stopList = ModelService.GetList(p => p.MachineCode == machineCode
                                                  && p.WorkNo == workObj.WorkNo
                                                  && p.ProcessCode == workObj.ProcessCode
                                               ).ToList();

            if (dt_StopStartDate.EditValue != null)
            {
                var updateObj = stopList.Where(p => p.StopStartTime != null && p.StopEndTime == null).First();
                updateObj.StopEndTime = DateTime.Now;
                ModelService.Update(updateObj);
            }
            else
            {
                var newObj = new TN_MEA1004()
                {
                    MachineCode = machineCode,
                    StopCode = control.Name,
                    StopStartTime = DateTime.Now,
                    WorkNo = workObj.WorkNo,
                    ProcessCode = workObj.ProcessCode
                };
                var TN_MPS1200 = ModelService.GetChildList<TN_MPS1200>(p => p.WorkNo == workObj.WorkNo
                                                        && p.ProcessCode == workObj.ProcessCode
                                                        && p.ProcessSeq == workObj.ProcessSeq).FirstOrDefault();
                if (TN_MPS1200 != null)
                {
                    //작업지시서 상태 변경
                    TN_MPS1200.JobStates = MasterCodeSTR.JobStates_Stop;
                    TN_MPS1200.UpdateTime = DateTime.Now;
                    ModelService.UpdateChild(TN_MPS1200);
                }
                ModelService.Insert(newObj);
            }

            ModelService.Save();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            ActClose();
        }

        private void SetBtnStopTypeEnable(bool enable)
        {
            //foreach (var v in btn_StopTypeList)
            //    v.Enabled = enable;
        }

        private void SetBtnStopTypeEnable(bool enable, string stopType)
        {
            try
            {
                var obj = btn_StopTypeList.Where(p => p.Name == stopType).FirstOrDefault();
                if (obj != null)
                    obj.Enabled = enable;
            }
            catch { }
        }

        private void Lup_MachineGroup_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            //var value = lookup.EditValue.GetNullToNull();
            lup_Machine.EditValue = null;
        }

        private void Lup_Machine_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            try
            {
                WaitHandler.ShowWait();
                var machineGroupCode = lup_MachineGroup.EditValue.GetNullToNull();

                if (machineGroupCode.IsNullOrEmpty())
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
                else
                    lookup.Properties.View.ActiveFilter.NonColumnFilter = "[MachineGroupCode] = '" + machineGroupCode + "'";
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }

    }
}
