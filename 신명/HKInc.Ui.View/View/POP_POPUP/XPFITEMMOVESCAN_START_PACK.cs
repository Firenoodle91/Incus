﻿using DevExpress.XtraEditors;
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

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 작업 시작 시 이동표 스캔 (포장)
    /// </summary>
    public partial class XPFITEMMOVESCAN_START_PACK : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_MPS1201> ModelService = (IService<TN_MPS1201>)ProductionFactory.GetDomainService("TN_MPS1201");

        TEMP_XFPOP_PACK TEMP_XFPOP_PACK;

        private string ItemMoveNo = null;

        public XPFITEMMOVESCAN_START_PACK()
        {
            InitializeComponent();
        }

        public XPFITEMMOVESCAN_START_PACK(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("WorkStart");

            TEMP_XFPOP_PACK = (TEMP_XFPOP_PACK)parameter.GetValue(PopupParameter.KeyValue);

            tx_ItemMoveNo.KeyDown += Tx_ItemMoveNo_KeyDown;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
            SetToolbarButtonCaption(ToolbarButton.Save, LabelConvert.GetLabelText("Start") + "[F5]", IconImageList.GetIconImage("arrows/play"));
        }

        protected override void InitCombo()
        {
            tx_ItemMoveNo.ImeMode = ImeMode.Disable;

            var processList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process);
            lup_PriviousProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NowProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            lup_NextProcessName.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), processList);
            
            lup_PriviousProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NowProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lup_NextProcessName.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ItemMoveNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_WorkNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            tx_ProductLotNo.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void Tx_ItemMoveNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var itemMoveNo = tx_ItemMoveNo.EditValue.GetNullToEmpty().ToUpper();

                if (itemMoveNo.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }

                DataSet ds;

                if (TEMP_XFPOP_PACK.ProcessSeq == 1)
                {
                    ds = DbRequestHandler.GetDataQury("EXEC USP_GET_ITEM_MOVE_NO_SCAN_START_PACK '" + TEMP_XFPOP_PACK.ItemCode + "', '" + itemMoveNo + "'");
                }
                else
                {
                    ds = DbRequestHandler.GetDataQury("EXEC USP_GET_ITEM_MOVE_NO_SCAN_START '" + TEMP_XFPOP_PACK.WorkNo + "', '" + itemMoveNo + "'," + TEMP_XFPOP_PACK.ProcessSeq + "");
                }

                if (ds == null || ds.Tables[1].Rows.Count < 1)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "F")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }
                else if (TEMP_XFPOP_PACK.ProcessSeq > 1 && ds.Tables[1].Rows[0][0].ToString() != TEMP_XFPOP_PACK.WorkNo)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_64));
                    ItemMoveNo = null;
                    tx_ItemMoveNo.EditValue = null;
                    tx_WorkNo.EditValue = null;
                    tx_ProductLotNo.EditValue = null;
                    lup_PriviousProcessName.EditValue = null;
                    lup_NowProcessName.EditValue = null;
                    lup_NextProcessName.EditValue = null;
                    tx_ItemMoveNo.Focus();
                    return;
                }

                ItemMoveNo = itemMoveNo;
                tx_WorkNo.EditValue = ds.Tables[1].Rows[0][0].GetNullToNull();
                tx_ProductLotNo.EditValue = ds.Tables[1].Rows[0][1].GetNullToNull();
                lup_PriviousProcessName.EditValue = ds.Tables[1].Rows[0][2].GetNullToNull();
                lup_NowProcessName.EditValue = ds.Tables[1].Rows[0][3].GetNullToNull();
                lup_NextProcessName.EditValue = ds.Tables[1].Rows[0][4].GetNullToNull();
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            //ItemMoveNo = tx_ItemMoveNo.EditValue.GetNullToEmpty();
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToNull();
            if (ItemMoveNo.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("ItemMoveNo")));
                return;
            }

            DialogResult = DialogResult.OK;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, ItemMoveNo);
            param.SetValue(PopupParameter.Value_2, productLotNo);
            if (TEMP_XFPOP_PACK.ProcessSeq == 1)
            {
                param.SetValue(PopupParameter.Value_3, productLotNo);
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
    }
}
