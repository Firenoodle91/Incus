﻿using System;
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

namespace HKInc.Ui.View.MEA_Popup
{
    public partial class XPFMEA1000 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_MEA1000> MEA1000Service;
        public XPFMEA1000(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
        }

        protected override void InitControls()
        {
            base.InitControls();
            buttonEdit1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit1).ButtonClick;
            buttonEdit1.EditValueChanged += BtnFile_EditValueChanged;
            lcMachineMaker.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MCMAKER));
            lup_process.SetDefault(false, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            lupCheckTurn.SetDefault(false, "CodeId", "CodeName", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
        }

        private void BtnFile_EditValueChanged(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            byte[] fileData = obj.Tag as byte[];
            pictureEdit1.EditValue = null;
            if (fileData != null)
            {
                TN_MEA1000 tn = ModelBindingSource.Current as TN_MEA1000;
                pictureEdit1.EditValue = fileData;
                tn.FileName = buttonEdit1.EditValue.ToString();
                tn.FileData = fileData;
            }
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            MEA1000Service = (IService<TN_MEA1000>)PopupParam.GetValue(PopupParameter.Service);
        }
      
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                ModelBindingSource.Add(new TN_MEA1000 { MachineCode = DbRequestHandler.GetRequestNumberNew("MC") ,UseYn="Y", DailyCheckFlag = "N"}); // 2021-11-17 김진우 주임 DailyCheckFlag 추가
                ModelBindingSource.DataSource = MEA1000Service.Insert((TN_MEA1000)ModelBindingSource.Current);

                this.Refresh();
            }
            else
            {  // Update
                TN_MEA1000 obj = (TN_MEA1000)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
            textEdit1.ReadOnly = true;
        }

        protected override void DataSave()
        {
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_MEA1000 obj = ModelBindingSource.Current as TN_MEA1000;

            if (EditMode == PopupEditMode.New)
                ModelBindingSource.DataSource = MEA1000Service.Insert(obj);
            else
                MEA1000Service.Update(obj);

            MEA1000Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.RowId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }
    }
}