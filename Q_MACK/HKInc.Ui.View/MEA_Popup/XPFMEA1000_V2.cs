using System;
using System.Linq;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
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

namespace HKInc.Ui.View.MEA_Popup
{
    /// <summary>
    /// 설비관리 팝업
    /// </summary>
    public partial class XPFMEA1000_V2 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_MEA1000> Mea1000Service;

        public XPFMEA1000_V2(PopupDataParam param, PopupCallback callback)
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
            ControlEnableList.Add("MachineCode", tx_MachineMCode);
            ControlEnableList.Add("MachineCode2", tx_MachineCode);
            ControlEnableList.Add("MachineGroupCode", lup_MachineGroup);
            ControlEnableList.Add("MachineName", tx_MachineName);
            //ControlEnableList.Add("MachineNameENG", tx_MachineNameENG);
            //ControlEnableList.Add("MachineNameCHN", tx_MachineNameCHN);
            ControlEnableList.Add("ModelNo", tx_Model);
            ControlEnableList.Add("Maker", lup_Maker);
            ControlEnableList.Add("InstallDate", dt_InstallDate);
            ControlEnableList.Add("SerialNo", tx_SerialNo);
            ControlEnableList.Add("CheckTurn", lup_CheckTurn);
            ControlEnableList.Add("FileName", btn_FileName);
            ControlEnableList.Add("CheckPointFileName", btn_FileName2);
            ControlEnableList.Add("MainternanceFileName", btn_FileName3);
            ControlEnableList.Add("NextCheck", dt_NextCheckDate);
            ControlEnableList.Add("DailyCheckFlag", chk_DailyCheckFlag);
            ControlEnableList.Add("UseYn", chk_UseFlag);
            ControlEnableList.Add("MonitorLocation", lup_MonitoringLocation);
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
            lup_Maker.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MCMAKER));
            lup_CheckTurn.SetDefault(true, "CodeId", "CodeName", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            lup_MachineGroup.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineGroup));
            lup_Class.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineClass));

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

            spin_Score.Properties.Mask.MaskType = MaskType.Numeric;
            spin_Score.Properties.Mask.EditMask = "n0";
            spin_Score.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_Score.Properties.Buttons[0].Visible = false;

            chk_MonitoringLocation.EditValueChanged += Chk_MonitoringLocation_EditValueChanged;
            if (chk_MonitoringLocation.Checked)
                lup_MonitoringLocation.Enabled = true;
                
            else            
                lup_MonitoringLocation.Enabled = false;
        }

        private void Chk_MonitoringLocation_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit ce = sender as CheckEdit;
            if (ce == null)
                return;

            if (ce.Checked)
            {
                lup_MonitoringLocation.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMonotoringLocation));
                lup_MonitoringLocation.Enabled = true;
            }
            else
            {
                lup_MonitoringLocation.DataSourceClear();
                lup_MonitoringLocation.Enabled = false;
                lup_MonitoringLocation.Text = null;
            }
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

            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var newObj = new TN_MEA1000()
                {
                    UseYn = "Y",
                    DailyCheckFlag = "N",
                    MachineCode = DbRequestHandler.GetRequestNumberNew("MC")
                };

                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
            }
            else
            {
                // Update
                dt_NextCheckDate.ReadOnly = true;
                var obj = (TN_MEA1000)PopupParam.GetValue(PopupParameter.KeyValue);

                // 20220420 오세완 차장 모니터링 위치 설정되어 있는 경우 값을 출력해야 함
                if(obj != null)
                {
                    if(obj.MonitorLocation.GetNullToEmpty() != "")
                    {
                        chk_MonitoringLocation.Checked = true;
                        lup_MonitoringLocation.EditValue = obj.MonitorLocation;
                    }
                }
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            var obj = ModelBindingSource.Current as TN_MEA1000;
            if (obj == null) return;

            string sMessage = "";
            if (EditMode == PopupEditMode.New)
            {
                //설비코드가 없을 경우 예외
                if (obj.MachineCode.IsNullOrEmpty())
                {
                    sMessage = "설비코드는 필수입니다. ";
                    MessageBoxHandler.Show(sMessage);
                    return;
                }

                //등록한 설비코드 데이터가 있을경우
                if (Mea1000Service.GetList(p => p.MachineCode == obj.MachineCode).FirstOrDefault() != null)
                {
                    sMessage = "이미 등록된 설비코드가 있습니다. ";
                    MessageBoxHandler.Show(sMessage);
                    return;
                }
            }

            // 모니터링 위치 중복 체크        2022-08-24 김진우
            if (Mea1000Service.GetList(p => p.MonitorLocation != null && p.MachineCode != obj.MachineCode).Any(a => a.MonitorLocation == obj.MonitorLocation.GetNullToEmpty()))
            {
                MessageBoxHandler.Show("중복된 모니터링 위치입니다.");
                return;
            }

            //파일 CHECK
            if (btn_FileName.EditValue.IsNullOrEmpty())
                obj.FileUrl = null;

            if (!obj.FileUrl.IsNullOrEmpty())
            {
                string[] filename = obj.FileUrl.ToString().Split('\\');
                if (filename.Length != 1)
                {
                    var realFileName = obj.MachineCode + "_" + filename[filename.Length - 1];
                    realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
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

            if (btn_FileName2.EditValue.IsNullOrEmpty())
                obj.CheckPointFileNameUrl = null;

            if (!obj.CheckPointFileNameUrl.IsNullOrEmpty())
            {
                string[] filename = obj.CheckPointFileNameUrl.ToString().Split('\\');
                if (filename.Length != 1)
                {
                    var realFileName_CheckPoint = obj.MachineCode + "_" + filename[filename.Length - 1];
                    realFileName_CheckPoint = FileHandler.CheckFileName(realFileName_CheckPoint);
                    var ftpFileUrl_CheckPoint = MasterCodeSTR.FtpFolder_MachineCheckPoint + "/" + realFileName_CheckPoint;

                    FileHandler.UploadFTP(obj.CheckPointFileNameUrl, realFileName_CheckPoint, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineCheckPoint + "/");

                    btn_FileName2.EditValue = realFileName_CheckPoint;
                    btn_FileName2.Tag = null;
                    obj.CheckPointFileNameUrl = ftpFileUrl_CheckPoint;
                }
                else if (obj.CheckPointFileNameUrl == "Clipboard_Image")
                {
                    var realFileName_CheckPoint = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    var ftpFileUrl_CheckPoint = MasterCodeSTR.FtpFolder_MachineCheckPoint + "/" + realFileName_CheckPoint;
                    var localImage = btn_FileName2.Tag as Image;
                    FileHandler.UploadFTP(localImage, realFileName_CheckPoint, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineCheckPoint + "/");

                    btn_FileName2.EditValue = realFileName_CheckPoint;
                    btn_FileName2.Tag = null;
                    obj.CheckPointFileNameUrl = ftpFileUrl_CheckPoint;
                }
            }

            if (btn_FileName3.EditValue.IsNullOrEmpty())
                obj.MainternanceFileNameUrl = null;

            if (!obj.MainternanceFileNameUrl.IsNullOrEmpty())
            {
                string[] filename = obj.MainternanceFileNameUrl.ToString().Split('\\');
                if (filename.Length != 1)
                {
                    var realFileName_Mainternance = obj.MachineCode + "_" + filename[filename.Length - 1];
                    realFileName_Mainternance = FileHandler.CheckFileName(realFileName_Mainternance); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                    var ftpFileUrl_Mainternance = MasterCodeSTR.FtpFolder_MachineMaintenance + "/" + realFileName_Mainternance;

                    FileHandler.UploadFTP(obj.MainternanceFileNameUrl, realFileName_Mainternance, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineMaintenance + "/");

                    btn_FileName3.EditValue = realFileName_Mainternance;
                    btn_FileName3.Tag = null;
                    obj.MainternanceFileNameUrl = ftpFileUrl_Mainternance;
                }
                else if (obj.MainternanceFileNameUrl == "Clipboard_Image")
                {
                    var realFileName_Mainternance = obj.MachineCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    var ftpFileUrl_Mainternance = MasterCodeSTR.FtpFolder_MachineMaintenance + "/" + realFileName_Mainternance;
                    var localImage = btn_FileName3.Tag as Image;
                    FileHandler.UploadFTP(localImage, realFileName_Mainternance, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineMaintenance + "/");

                    btn_FileName3.EditValue = realFileName_Mainternance;
                    btn_FileName3.Tag = null;
                    obj.MainternanceFileNameUrl = ftpFileUrl_Mainternance;
                }
            }

            if (EditMode == PopupEditMode.New)
            {
                //점검예정일 CHECK
                if (!obj.CheckTurn.IsNullOrEmpty())
                {
                    if (obj.NextCheck.IsNullOrEmpty())
                    {
                        if (!obj.InstallDate.IsNullOrEmpty())
                        {
                            obj.NextCheck = ((DateTime)obj.InstallDate).AddMonths(obj.CheckTurn.GetIntNullToZero());
                        }
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelBindingSource.DataSource = Mea1000Service.Insert(obj);
            }
            else
            {
                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                Mea1000Service.Update(obj);
            }

            // 설비 I/F 위치 등록시 MEA1700 변경되도록 추가            2022-08-25 김진우
            if (obj.MonitorLocation != null)
            {
                TN_MEA1700 MEA1700 = Mea1000Service.GetChildList<TN_MEA1700>(p => p.Connection == obj.MonitorLocation).FirstOrDefault();
                MEA1700.MachineCode = obj.MachineCode;
                Mea1000Service.UpdateChild<TN_MEA1700>(MEA1700);
            }

            Mea1000Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.MachineCode);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            SetSaveMessageCheck = true;
        }

        private void Btn_FileName_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MEA1000;
            if (obj == null)
                return;

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
        }


        private void Btn_FileName2_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MEA1000;
            if (obj == null)
                return;

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
                    obj.CheckPointFileNameUrl = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_CheckPointImage.EditValue = localImage;
                        obj.CheckPointFileNameUrl = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.CheckPointFileNameUrl);
                        pic_CheckPointImage.EditValue = fileData;
                    }
                }
            }
        }

        private void Btn_FileName3_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MEA1000;
            if (obj == null)
                return;

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
                    obj.MainternanceFileNameUrl = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_CheckMaintenanceImage.EditValue = localImage;
                        obj.MainternanceFileNameUrl = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.MainternanceFileNameUrl);
                        pic_CheckMaintenanceImage.EditValue = fileData;
                    }
                }
            }
        }
    }
}