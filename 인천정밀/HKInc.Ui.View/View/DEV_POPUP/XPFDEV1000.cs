using System;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using DevExpress.XtraEditors.Mask;
using HKInc.Service.Helper;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.DEV_POPUP
{
    /// <summary>
    /// 검사의뢰서 팝업
    /// </summary>
    public partial class XPFDEV1000 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_DEV1000> ModelService;

        public XPFDEV1000(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;

            ModelBindingSource = bindingSource1; // BindingSource설정

            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
        }

        protected override void AddControlList()
        {
            ControlEnableList.Add("ReqNo", tx_ReqNo);
            ControlEnableList.Add("ReqDate", dt_ReqDate);
            ControlEnableList.Add("ItemCode", lup_ItemCode);
            ControlEnableList.Add("CustomerCode", lup_CustomerCode);
            ControlEnableList.Add("ReqId", lup_ReqId);
            ControlEnableList.Add("ReqQty", spin_ReqQty);
            ControlEnableList.Add("ReturnDate", dt_ReturnDate);
            ControlEnableList.Add("ReqFileName", btn_ReqFileName);
            ControlEnableList.Add("CheckDate", lup_CheckDate);
            ControlEnableList.Add("CheckId", lup_CheckId);
            ControlEnableList.Add("CheckFileName", btn_CheckFileName);
            ControlEnableList.Add("Memo", tx_Memo);

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_DEV1000>(new TN_DEV1000(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            ModelService = (IService<TN_DEV1000>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            spin_ReqQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ReqQty.Properties.Mask.EditMask = "n0";
            spin_ReqQty.Properties.Mask.UseMaskAsDisplayFormat = true;

            lup_ItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_ReqId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lup_CheckId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());

            btn_ReqFileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_ReqFileName
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_ReqFile).ButtonClick;
            btn_CheckFileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_CheckFileName
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_ReqFile).ButtonClick;
        }

        protected override void DataLoad()
        {
            tx_ReqNo.ReadOnly = true;

            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var newObj = new TN_DEV1000()
                {
                    ReqNo = DbRequestHandler.GetSeqStandard("DREQ"),
                    ReqDate = DateTime.Today,
                    ReturnDate = DateTime.Today
                };

                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
            }
            else
            {
                // Update
                var obj = (TN_DEV1000)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            var obj = ModelBindingSource.Current as TN_DEV1000;

            if (EditMode == PopupEditMode.New)
            {
                //파일 CHECK
                if (btn_ReqFileName.EditValue.IsNullOrEmpty()) obj.ReqFileUrl = null;
                else
                {
                    string[] filename = btn_ReqFileName.EditValue.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ReqNo + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ReqFile + "/" + realFileName;

                        FileHandler.UploadFTP(btn_ReqFileName.EditValue.ToString(), realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ReqFile + "/");

                        btn_ReqFileName.EditValue = realFileName;
                        btn_ReqFileName.Tag = null;
                        obj.ReqFileUrl = ftpFileUrl;
                    }
                }

                if (btn_CheckFileName.EditValue.IsNullOrEmpty()) obj.CheckFileUrl = null;
                else
                {
                    string[] filename = btn_CheckFileName.EditValue.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ReqNo + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ReqFile + "/" + realFileName;

                        FileHandler.UploadFTP(btn_CheckFileName.EditValue.ToString(), realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ReqFile + "/");

                        btn_CheckFileName.EditValue = realFileName;
                        btn_CheckFileName.Tag = null;
                        obj.CheckFileUrl = ftpFileUrl;
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelBindingSource.DataSource = ModelService.Insert(obj);
            }
            else
            {
                //파일 CHECK
                if (btn_ReqFileName.EditValue.IsNullOrEmpty()) obj.ReqFileUrl = null;

                if (!obj.ReqFileUrl.IsNullOrEmpty())
                {
                    string[] filename = obj.ReqFileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ReqNo + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ReqFile + "/" + realFileName;

                        FileHandler.UploadFTP(obj.ReqFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ReqFile + "/");

                        btn_ReqFileName.EditValue = realFileName;
                        btn_ReqFileName.Tag = null;
                        obj.ReqFileUrl = ftpFileUrl;
                    }
                }

                if (btn_CheckFileName.EditValue.IsNullOrEmpty()) obj.CheckFileUrl = null;

                if (!obj.CheckFileUrl.IsNullOrEmpty())
                {
                    string[] filename = obj.CheckFileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ReqNo + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ReqFile + "/" + realFileName;

                        FileHandler.UploadFTP(obj.CheckFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ReqFile + "/");

                        btn_CheckFileName.EditValue = realFileName;
                        btn_CheckFileName.Tag = null;
                        obj.CheckFileUrl = ftpFileUrl;
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelService.Update(obj);
            }

            ModelService.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.ItemCode);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            tx_ReqNo.ReadOnly = true;
            SetSaveMessageCheck = true;
        }
    }
}