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
using System.Collections.Specialized;
using System.IO;
using System.Drawing;

namespace HKInc.Ui.View.View.MEA_POPUP
{
    /// <summary>
    /// 설비관리 팝업
    /// </summary>
    public partial class XPFMEA1000 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_MEA1000> Mea1000Service;

        public XPFMEA1000(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            this.Text = "설비등록";

            ModelBindingSource = bindingSource1; // BindingSource설정

            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
        }

        protected override void AddControlList()
        {
            ControlEnableList.Add("MachineMCode", tx_MachineMCode);
            ControlEnableList.Add("MachineCode", tx_MachineCode);
            ControlEnableList.Add("MachineGroupCode", lup_MachineGroup);
            ControlEnableList.Add("MachineName", tx_MachineName);
            ControlEnableList.Add("MachineNameENG", tx_MachineNameENG);
            ControlEnableList.Add("MachineNameCHN", tx_MachineNameCHN);
            ControlEnableList.Add("Model", lup_Model);
            ControlEnableList.Add("Maker", lup_Maker);
            ControlEnableList.Add("InstallDate", dt_InstallDate);
            ControlEnableList.Add("SerialNo", tx_SerialNo);
            ControlEnableList.Add("CheckTurn", lup_CheckTurn);
            ControlEnableList.Add("FileName", btn_FileName);
            ControlEnableList.Add("FileName2", btn_FileName2);
            ControlEnableList.Add("FileName3", btn_FileName3);
            ControlEnableList.Add("NextCheckDate", dt_NextCheckDate);
            ControlEnableList.Add("DailyCheckFlag", chk_DailyCheckFlag);
            ControlEnableList.Add("UseFlag", chk_UseFlag);
            ControlEnableList.Add("ProcTeamCode", lup_ProcTeamCode);
            ControlEnableList.Add("Memo", tx_Memo);
            ControlEnableList.Add("Class", lup_Class);
            ControlEnableList.Add("ClassDate", dt_ClassDate);
            ControlEnableList.Add("Score", spin_Score);


            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_MEA1000>(new TN_MEA1000(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            Mea1000Service = (IService<TN_MEA1000>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            lup_Maker.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMaker,1));
            lup_CheckTurn.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MachineCheckCycle,1));
            lup_MachineGroup.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MachineGroup, 1));
            lup_Model.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MachineModel, 1));
            lup_Class.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MachineClass, 1));

            btn_FileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_FileName
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_MachineImage).ButtonClick;
            btn_FileName.EditValueChanged += Btn_FileName_EditValueChanged;
            btn_FileName.KeyDown += Btn_FileName_KeyDown;


            btn_FileName2.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_FileName2
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_MachineCheckPoint).ButtonClick;
            btn_FileName2.EditValueChanged += Btn_FileName2_EditValueChanged;
            btn_FileName2.KeyDown += Btn_FileName2_KeyDown;


            btn_FileName3.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_FileName3
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_MachineMaintenance).ButtonClick;
            btn_FileName3.EditValueChanged += Btn_FileName3_EditValueChanged;
            btn_FileName3.KeyDown += Btn_FileName3_KeyDown;

            lup_ProcTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));

            spin_Score.Properties.Mask.MaskType = MaskType.Numeric;
            spin_Score.Properties.Mask.EditMask = "n0";
            spin_Score.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_Score.Properties.Buttons[0].Visible = false;
        }

        private void Btn_FileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                StringCollection list = Clipboard.GetFileDropList();
                if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
                {
                    using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        btn_FileName.Tag = fileData;
                        btn_FileName.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_FileName.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_FileName.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }

        private void Btn_FileName2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                StringCollection list = Clipboard.GetFileDropList();
                if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
                {
                    using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        btn_FileName2.Tag = fileData;
                        btn_FileName2.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_FileName2.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_FileName2.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }

        private void Btn_FileName3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                StringCollection list = Clipboard.GetFileDropList();
                if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
                {
                    using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        btn_FileName3.Tag = fileData;
                        btn_FileName3.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_FileName3.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_FileName3.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }

        protected override void DataLoad()
        {
            tx_MachineMCode.ReadOnly = true;
            //tx_MachineCode.ReadOnly = true;

            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var newObj = new TN_MEA1000()
                {
                    UseFlag = "Y",
                    DailyCheckFlag = "N",
                    MachineMCode = DbRequestHandler.GetSeqStandard("MC")
                };

                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
                //btn_FileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler_BAK(UserRight.HasSelect
                //                                                                                                        , UserRight.HasEdit
                //                                                                                                        , btn_FileName
                //                                                                                                        , true
                //                                                                                                        , MasterCodeSTR.FtpFolder_MachineImage
                //                                                                                                        , newObj.MachineCode).ButtonClick;
            }
            else
            {
                // Update
                //tx_MachineCode.ReadOnly = true;
                dt_NextCheckDate.ReadOnly = true;
                var obj = (TN_MEA1000)PopupParam.GetValue(PopupParameter.KeyValue);
                //btn_FileName.Tag = obj.FileUrl;
                ModelBindingSource.DataSource = obj;
                //btn_FileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler_BAK(UserRight.HasSelect
                //                                                                                                        , UserRight.HasEdit
                //                                                                                                        , btn_FileName
                //                                                                                                        , true
                //                                                                                                        , MasterCodeSTR.FtpFolder_MachineImage
                //                                                                                                        , obj.MachineCode).ButtonClick;
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            var obj = ModelBindingSource.Current as TN_MEA1000;

            if (EditMode == PopupEditMode.New)
            {
                //설비코드가 없을 경우 예외
                if (obj.MachineCode.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("MachineCode")));
                    return;
                }

                //등록한 설비코드 데이터가 있을경우
                if (Mea1000Service.GetList(p => p.MachineCode == obj.MachineCode).FirstOrDefault() != null)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), string.Format("{0}:{1}", LabelConvert.GetLabelText("MachineCode"), obj.MachineCode)));
                    return;
                }

                //파일 CHECK
                if (btn_FileName.EditValue.IsNullOrEmpty()) obj.FileUrl = null;

                if (!obj.FileUrl.IsNullOrEmpty())
                {
                    string[] filename = obj.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.MachineCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineImage + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                    else if (obj.FileUrl == "Clipboard_Image")
                    {
                        var realFileName = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineImage + "/" + realFileName;
                        var localImage = btn_FileName.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                }

                if (btn_FileName2.EditValue.IsNullOrEmpty()) obj.FileUrl2 = null;

                if (!obj.FileUrl2.IsNullOrEmpty())
                {
                    string[] filename = obj.FileUrl2.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.MachineCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineCheckPoint + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl2, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineCheckPoint + "/");

                        btn_FileName2.EditValue = realFileName;
                        btn_FileName2.Tag = null;
                        obj.FileUrl2 = ftpFileUrl;
                    }
                    else if (obj.FileUrl2 == "Clipboard_Image")
                    {
                        var realFileName = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineCheckPoint + "/" + realFileName;
                        var localImage = btn_FileName2.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineCheckPoint + "/");

                        btn_FileName2.EditValue = realFileName;
                        btn_FileName2.Tag = null;
                        obj.FileUrl2 = ftpFileUrl;
                    }
                }

                if (btn_FileName3.EditValue.IsNullOrEmpty()) obj.FileUrl3 = null;

                if (!obj.FileUrl3.IsNullOrEmpty())
                {
                    string[] filename = obj.FileUrl3.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.MachineCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineMaintenance + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl3, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineMaintenance + "/");

                        btn_FileName3.EditValue = realFileName;
                        btn_FileName3.Tag = null;
                        obj.FileUrl3 = ftpFileUrl;
                    }
                    else if (obj.FileUrl3 == "Clipboard_Image")
                    {
                        var realFileName = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineMaintenance + "/" + realFileName;
                        var localImage = btn_FileName3.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineMaintenance + "/");

                        btn_FileName3.EditValue = realFileName;
                        btn_FileName3.Tag = null;
                        obj.FileUrl3 = ftpFileUrl;
                    }
                }

                //점검예정일 CHECK
                if (!obj.CheckTurn.IsNullOrEmpty())
                {
                    if (obj.NextCheckDate.IsNullOrEmpty())
                    {
                        if (!obj.InstallDate.IsNullOrEmpty())
                        {
                            obj.NextCheckDate = ((DateTime)obj.InstallDate).AddMonths(obj.CheckTurn.GetIntNullToZero());
                        }
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelBindingSource.DataSource = Mea1000Service.Insert(obj);
            }
            else
            {
                //파일 CHECK
                if (btn_FileName.EditValue.IsNullOrEmpty()) obj.FileUrl = null;

                if (!obj.FileUrl.IsNullOrEmpty())
                {
                    string[] filename = obj.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.MachineCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineImage + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                    else if (obj.FileUrl == "Clipboard_Image")
                    {
                        var realFileName = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineImage + "/" + realFileName;
                        var localImage = btn_FileName.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                }

                if (btn_FileName2.EditValue.IsNullOrEmpty()) obj.FileUrl2 = null;

                if (!obj.FileUrl2.IsNullOrEmpty())
                {
                    string[] filename = obj.FileUrl2.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.MachineCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineCheckPoint + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl2, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineCheckPoint + "/");

                        btn_FileName2.EditValue = realFileName;
                        btn_FileName2.Tag = null;
                        obj.FileUrl2 = ftpFileUrl;
                    }
                    else if (obj.FileUrl2 == "Clipboard_Image")
                    {
                        var realFileName = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineCheckPoint + "/" + realFileName;
                        var localImage = btn_FileName2.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineCheckPoint + "/");

                        btn_FileName2.EditValue = realFileName;
                        btn_FileName2.Tag = null;
                        obj.FileUrl2 = ftpFileUrl;
                    }
                }

                if (btn_FileName3.EditValue.IsNullOrEmpty()) obj.FileUrl3 = null;

                if (!obj.FileUrl3.IsNullOrEmpty())
                {
                    string[] filename = obj.FileUrl3.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.MachineCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineMaintenance + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl3, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineMaintenance + "/");

                        btn_FileName3.EditValue = realFileName;
                        btn_FileName3.Tag = null;
                        obj.FileUrl3 = ftpFileUrl;
                    }
                    else if (obj.FileUrl3 == "Clipboard_Image")
                    {
                        var realFileName = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineMaintenance + "/" + realFileName;
                        var localImage = btn_FileName3.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineMaintenance + "/");

                        btn_FileName3.EditValue = realFileName;
                        btn_FileName3.Tag = null;
                        obj.FileUrl3 = ftpFileUrl;
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                Mea1000Service.Update(obj);
            }

            Mea1000Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.MachineCode);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            //tx_MachineCode.ReadOnly = true;
            SetSaveMessageCheck = true;
        }

        private void Btn_FileName_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MEA1000;
            if (obj == null) return;
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_FileImage.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_FileImage.EditValue = localfileData;
                    obj.FileUrl = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_FileImage.EditValue = localImage;
                        obj.FileUrl = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.FileUrl);
                        pic_FileImage.EditValue = fileData;
                    }
                }
            }

            //var fileName = button.EditValue.GetNullToEmpty();
            //if (fileName.IsNullOrEmpty())
            //{
            //    pic_FileImage.EditValue = null;
            //}
            //else
            //{
            //    var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + button.Tag.GetNullToEmpty());
            //    pic_FileImage.EditValue = fileData;
            //}
        }


        private void Btn_FileName2_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MEA1000;
            if (obj == null) return;
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_CheckPointImage.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_CheckPointImage.EditValue = localfileData;
                    obj.FileUrl2 = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_CheckPointImage.EditValue = localImage;
                        obj.FileUrl2 = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.FileUrl2);
                        pic_CheckPointImage.EditValue = fileData;
                    }
                }
            }
        }

        private void Btn_FileName3_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MEA1000;
            if (obj == null) return;
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_CheckMaintenanceImage.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_CheckMaintenanceImage.EditValue = localfileData;
                    obj.FileUrl3 = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_CheckMaintenanceImage.EditValue = localImage;
                        obj.FileUrl3 = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.FileUrl3);
                        pic_CheckMaintenanceImage.EditValue = fileData;
                    }
                }
            }
        }
    }
}