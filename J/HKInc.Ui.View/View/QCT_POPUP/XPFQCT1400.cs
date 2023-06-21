using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using DevExpress.XtraEditors.Mask;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using System.Collections.Specialized;
using System.IO;

namespace HKInc.Ui.View.View.QCT_POPUP
{
    /// <summary>
    /// 클레임관리 팝업
    /// </summary>
    public partial class XPFQCT1400 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_QCT1400> ModelService;

        public XPFQCT1400()
        {
            InitializeComponent();
        }

        public XPFQCT1400(PopupDataParam param, PopupCallback callback) : this()
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;

            ModelBindingSource = bindingSource1; // BindingSource설정

            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;

            lup_ItemCode.EditValueChanged += Lup_ItemCode_EditValueChanged;
            btn_FileName.EditValueChanged += Btn_FileName_EditValueChanged;
        }
        
        protected override void AddControlList()
        {
            ControlEnableList.Add("ClaimNo", tx_ClaimNo);
            ControlEnableList.Add("ClaimDate", dt_ClaimDate);
            ControlEnableList.Add("ItemCode", lup_ItemCode);
            ControlEnableList.Add("ClaimCustomerCode", lup_ClaimCustomerCode);
            ControlEnableList.Add("ProductLotNo", tx_ProductLotNo);
            ControlEnableList.Add("OutLotNo", tx_OutLotNo);
            ControlEnableList.Add("ClaimType", lup_ClaimType);
            ControlEnableList.Add("ClaimQty", spin_ClaimQty);
            ControlEnableList.Add("ClaimId", lup_ClaimId);
            ControlEnableList.Add("FileName", btn_FileName);
            ControlEnableList.Add("Memo", memoEdit1);

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_QCT1400>(new TN_QCT1400(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            ModelService = (IService<TN_QCT1400>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            tx_ClaimNo.ReadOnly = true;
            tx_ItemCode.ReadOnly = true;
            tx_ItemName1.ReadOnly = true;

            pic_AttachImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;

            spin_ClaimQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ClaimQty.Properties.Mask.EditMask = "n2";
            spin_ClaimQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ClaimQty.Properties.Buttons[0].Visible = false;

            lup_ItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 반제품(타사)에 대한 클레임을 기록할 수 있게 하기 위해서 추가처리
            lup_ClaimCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_ClaimId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lup_ClaimType.SetDefault(true, "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ClaimType));

            //btn_FileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
            //                                                                                                        , UserRight.HasEdit
            //                                                                                                        , btn_FileName
            //                                                                                                        , false
            //                                                                                                        , MasterCodeSTR.FtpFolder_ClaimFile).ButtonClick;

            btn_FileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_FileName, !UserRight.HasEdit, false, false).ButtonClick;

            btn_FileName.EditValueChanged += Btn_FileName_EditValueChanged;
            btn_FileName.KeyDown += Btn_FileName_KeyDown;

            lup_ClaimId.EditValue = GlobalVariable.LoginId;
            lup_ClaimId.ReadOnly = true;
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
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var newObj = new TN_QCT1400()
                {
                    ClaimNo = DbRequestHandler.GetSeqMonth("CQC"),
                    ClaimDate = DateTime.Today,
                    ClaimId = GlobalVariable.LoginId
                };

                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
            }
            else
            {
                // Update
                var obj = (TN_QCT1400)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            var obj = ModelBindingSource.Current as TN_QCT1400;

            if (EditMode == PopupEditMode.New)
            {
                ////파일 CHECK
                //if (btn_FileName.EditValue.IsNullOrEmpty()) obj.FileUrl = null;

                //if (!obj.FileUrl.IsNullOrEmpty())
                //{
                //    string[] filename = obj.FileUrl.ToString().Split('\\');
                //    if (filename.Length != 1)
                //    {
                //        var realFileName = obj.ClaimNo + "_" + filename[filename.Length - 1];
                //        var ftpFileUrl = MasterCodeSTR.FtpFolder_ClaimFile + "/" + realFileName;

                //        FileHandler.UploadFTP(obj.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ClaimFile + "/");

                //        btn_FileName.EditValue = realFileName;
                //        btn_FileName.Tag = null;
                //        obj.FileUrl = ftpFileUrl;
                //    }
                //    else if (obj.FileUrl == "Clipboard_Image")
                //    {
                //        var realFileName = obj.ClaimNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                //        var ftpFileUrl = MasterCodeSTR.FtpFolder_ClaimFile + "/" + realFileName;
                //        var localImage = btn_FileName.Tag as Image;
                //        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ClaimFile + "/");

                //        btn_FileName.EditValue = realFileName;
                //        btn_FileName.Tag = null;
                //        obj.FileUrl = ftpFileUrl;
                //    }
                //}

                if (btn_FileName.EditValue.IsNullOrEmpty())
                {
                    obj.FileUrl = null;
                    obj.FileData = null;
                }
                else
                {
                    string[] filename = obj.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ClaimNo + "_" + filename[filename.Length - 1];
                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelBindingSource.DataSource = ModelService.Insert(obj);
            }
            else
            {
                if (btn_FileName.EditValue.IsNullOrEmpty())
                {
                    obj.FileUrl = null;
                    obj.FileData = null;
                }
                else
                {
                    string[] filename = obj.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ClaimNo + "_" + filename[filename.Length - 1];
                        btn_FileName.EditValue = realFileName;
                        btn_FileName.Tag = null;
                    }
                }

                

                ////파일 CHECK
                //if (btn_FileName.EditValue.IsNullOrEmpty()) obj.FileUrl = null;

                //if (!obj.FileUrl.IsNullOrEmpty())
                //{
                //    string[] filename = obj.FileUrl.ToString().Split('\\');
                //    if (filename.Length != 1)
                //    {
                //        var realFileName = obj.ClaimNo + "_" + filename[filename.Length - 1];
                //        var ftpFileUrl = MasterCodeSTR.FtpFolder_ClaimFile + "/" + realFileName;

                //        FileHandler.UploadFTP(obj.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ClaimFile + "/");

                //        btn_FileName.EditValue = realFileName;
                //        btn_FileName.Tag = null;
                //        obj.FileUrl = ftpFileUrl;
                //    }
                //    else if (obj.FileUrl == "Clipboard_Image")
                //    {
                //        var realFileName = obj.ClaimNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                //        var ftpFileUrl = MasterCodeSTR.FtpFolder_ClaimFile + "/" + realFileName;
                //        var localImage = btn_FileName.Tag as Image;
                //        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ClaimFile + "/");

                //        btn_FileName.EditValue = realFileName;
                //        btn_FileName.Tag = null;
                //        obj.FileUrl = ftpFileUrl;
                //    }
                //}
                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelService.Update(obj);
            }

            ModelService.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.ClaimNo);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            SetSaveMessageCheck = true;
        }

        private void Lup_ItemCode_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var obj = lookup.GetSelectedDataRow() as TN_STD1100;

            if (obj == null)
            {
                tx_ItemCode.EditValue = null;
                tx_ItemName1.EditValue = null;
            }
            else
            {
                //tx_ItemCode.EditValue = lookup.EditValue;
                tx_ItemCode.EditValue = obj.ItemName;
                //tx_ItemName1.EditValue = obj.ItemName1;
            }
        }

        private void Btn_FileName_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_QCT1400;
            if (obj == null) return;
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_AttachImage.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_AttachImage.EditValue = localfileData;
                    obj.FileUrl = fileName;
                    obj.FileData = localfileData;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_AttachImage.EditValue = localImage;
                        obj.FileUrl = fileName;
                    }
                    else
                    {
                        //var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.FileUrl);
                        var fileData = obj.FileData;
                        pic_AttachImage.EditValue = fileData;
                    }
                }
            }

            //var fileName = button.EditValue.GetNullToEmpty();
            //if (fileName.IsNullOrEmpty())
            //{
            //    pic_AttachImage.EditValue = null;
            //}
            //else
            //{
            //    var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + button.Tag.GetNullToEmpty());
            //    pic_AttachImage.EditValue = fileData;
            //}
        }
    }
}