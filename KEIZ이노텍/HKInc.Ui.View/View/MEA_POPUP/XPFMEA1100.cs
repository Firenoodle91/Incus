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
using System.Collections.Specialized;
using System.IO;
using System.Drawing;

namespace HKInc.Ui.View.View.MEA_POPUP
{
    /// <summary>
    /// 계측기관리 팝업
    /// </summary>
    public partial class XPFMEA1100 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_MEA1100> Mea1100Service;

        public XPFMEA1100(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            this.Text = "계측기등록";

            ModelBindingSource = bindingSource1; // BindingSource설정

            tx_InstrCode.KeyUp += Tx_InstrCode_KeyUp;

            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
        }

        protected override void AddControlList()
        {
            ControlEnableList.Add("InstrCode", tx_InstrCode);
            ControlEnableList.Add("InstrName", tx_InstrName);
            ControlEnableList.Add("InstrNameENG", tx_InstrNameENG);
            ControlEnableList.Add("InstrNameCHN", tx_InstrNameCHN);
            ControlEnableList.Add("InstrKind", tx_InstrKind);
            ControlEnableList.Add("Spec", tx_Spec);
            ControlEnableList.Add("Maker", lup_Maker);
            ControlEnableList.Add("CorDate", dt_CorDate);
            ControlEnableList.Add("IntroductionDate", dt_IntroductionDate);
            ControlEnableList.Add("SerialNo", tx_SerialNo);
            ControlEnableList.Add("CorTurn", lup_CorTurn);
            ControlEnableList.Add("FileName", btn_FileName);
            ControlEnableList.Add("PredictionCorDate", dt_PredictionCorDate);
            ControlEnableList.Add("WorkId", lup_WorkId);
            ControlEnableList.Add("UseFlag", chk_UseFlag);
            ControlEnableList.Add("Memo", tx_Memo);


            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_MEA1100>(new TN_MEA1100(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            Mea1100Service = (IService<TN_MEA1100>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            lup_Maker.SetDefault(false, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.InstrMaker,1));
            lup_CorTurn.SetDefault(false, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.InstrCorTurn,1));
            lup_WorkId.SetDefault(true, "LoginId", "UserName", Mea1100Service.GetChildList<User>(p => p.Active == "Y").ToList());

            btn_FileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_FileName
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_InstrImage).ButtonClick;
            btn_FileName.EditValueChanged += Btn_FileName_EditValueChanged;
            btn_FileName.KeyDown += Btn_FileName_KeyDown;
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

        protected override void DataLoad()
        {
            //tx_InstrCode.ReadOnly = true;

            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var newObj = new TN_MEA1100()
                {
                    UseFlag = "Y",
                    //InstrCode = DbRequestHandler.GetSeqStandard("INSTR")
                };

                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
            }
            else
            {
                dt_PredictionCorDate.ReadOnly = true;
                // Update
                var obj = (TN_MEA1100)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            var obj = ModelBindingSource.Current as TN_MEA1100;

            if (EditMode == PopupEditMode.New)
            {
                //계측기코드 체크
                if (obj.InstrCode.GetNullToNull() == null)
                {
                    obj.InstrCode = DbRequestHandler.GetSeqStandard("INSTR");
                }
                else
                {
                    
                    //string codeChk = obj.InstrCode.Substring(0, 6);
                    int itemseq = 0;

                    //if (codeChk == "INSTR-")
                    if(obj.InstrCode.Contains("INSTR-"))
                    {
                        if (obj.InstrCode.Length != 11)
                        {
                            MessageBox.Show("자릿수 11자리를 지켜주시기 바랍니다.");
                            return;
                        }

                        bool intchk = int.TryParse(obj.InstrCode.Substring(6, 5), out itemseq);

                        if (intchk)
                        {
                            //return;

                            string sSql = "USP_INS_ITEM_SEQ 'INSTR'," + itemseq + "";
                            DbRequestHandler.GetCellValue(sSql, 0);
                        }
                    }
                }


                //파일 CHECK
                if (btn_FileName.EditValue.IsNullOrEmpty()) obj.FileUrl = null;

                if (!obj.FileUrl.IsNullOrEmpty())
                {
                    string[] filename = obj.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.InstrCode + "_" + filename[filename.Length - 1];
                        realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_InstrImage + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_InstrImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                    else if (obj.FileUrl == "Clipboard_Image")
                    {
                        var realFileName = obj.InstrCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 특수문자 있는 경우 오류가 있어서 그걸 제거처리
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_InstrImage + "/" + realFileName;
                        var localImage = btn_FileName.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_InstrImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                }

                //교정예정일 CHECK
                if (!obj.CorTurn.IsNullOrEmpty())
                {
                    if (obj.PredictionCorDate.IsNullOrEmpty())
                    {
                        if (!obj.CorDate.IsNullOrEmpty())
                        {
                            obj.PredictionCorDate = ((DateTime)obj.CorDate).AddMonths(obj.CorTurn.GetIntNullToZero());
                        }
                        else if (!obj.IntroductionDate.IsNullOrEmpty())
                        {
                            obj.PredictionCorDate = ((DateTime)obj.IntroductionDate).AddMonths(obj.CorTurn.GetIntNullToZero());
                        }
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelBindingSource.DataSource = Mea1100Service.Insert(obj);
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
                        var realFileName = obj.InstrCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_InstrImage + "/" + realFileName;

                        FileHandler.UploadFTP(obj.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_InstrImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                    else if (obj.FileUrl == "Clipboard_Image")
                    {
                        var realFileName = obj.InstrCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_InstrImage + "/" + realFileName;
                        var localImage = btn_FileName.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_InstrImage + "/");

                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                        obj.FileUrl = ftpFileUrl;
                    }
                }
                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                Mea1100Service.Update(obj);
            }

            Mea1100Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.InstrCode);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            tx_InstrCode.ReadOnly = true;
            SetSaveMessageCheck = true;
        }

        private void Btn_FileName_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MEA1100;
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

        private void Tx_InstrCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (EditMode == PopupEditMode.New)
            {
                if (tx_InstrCode.Text.GetNullToEmpty() != "")
                {
                    var instrCodeCheck = Mea1100Service.GetList(x => x.InstrCode == tx_InstrCode.Text.ToString()).FirstOrDefault();

                    if (instrCodeCheck != null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("InstrCode"), LabelConvert.GetLabelText("ItemCode")));
                        tx_InstrCode.Text = string.Empty;
                    }
                }
            }
        }


    }
}