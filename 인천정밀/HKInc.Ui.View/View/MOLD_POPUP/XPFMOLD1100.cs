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
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using DevExpress.XtraEditors.Mask;
using HKInc.Service.Helper;
using HKInc.Service.Handler.EventHandler;
using System.Collections.Specialized;
using System.IO;



namespace HKInc.Ui.View.View.MOLD_POPUP
{
    public partial class XPFMOLD1100 : HKInc.Service.Base.ListEditFormTemplate
    {
        IService<TN_MOLD1100> ModelService = (IService<TN_MOLD1100>)ProductionFactory.GetDomainService("TN_MOLD1100");

        private IService<TN_MOLD1100> Mold1100Service;        
        

        public XPFMOLD1100(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정

            btn_MoldFileName.KeyDown += Btn_MoldFileName_KeyDown;
            btn_MoldCheckFileName.KeyDown += Btn_MoldCheckFileName_KeyDown;

            lup_MoldWhCode.EditValueChanged += Lup_MoldWhCode_EditValueChanged;
            lup_MoldWhPosition.Popup += Lup_MoldWhPosition_Popup;

            btn_MoldFileName.EditValueChanged += Btn_MoldFileName_EditValueChanged;
            btn_MoldCheckFileName.EditValueChanged += Btn_MoldCheckFileName_EditValueChanged;

            dt_TransferDate.EditValueChanged += Dt_TransferDate_EditValueChanged;
            lup_CheckCycle.EditValueChanged += Lup_CheckCycle_EditValueChanged;

            this.Text = "금형관리";
        }

        

        private void Lup_MoldWhPosition_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var InWhCode = lup_MoldWhCode.EditValue.GetNullToEmpty();

            if (InWhCode.IsNullOrEmpty())
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            }
            else 
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + InWhCode + "'";
            }

        }

        private void Lup_MoldWhCode_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        protected override void AddControlList()
        {
            ControlEnableList.Add("MoldMcode", tx_MoldMCode);
            ControlEnableList.Add("Moldcode", tx_MoldCode);
            ControlEnableList.Add("MoldName", tx_MoldName);
            ControlEnableList.Add("ItemCode", lup_ItemCode);
            ControlEnableList.Add("MoldMakercust", lup_MakerCust);
            ControlEnableList.Add("TransferDate", dt_TransferDate);
            ControlEnableList.Add("MainMachineCode", lup_MainMachineCode);
            ControlEnableList.Add("Cavity", Spin_Cavity);
            ControlEnableList.Add("MoldWhCode", lup_MoldWhCode);
            ControlEnableList.Add("MoldWhPosition", lup_MoldWhPosition);
            //ControlEnableList.Add("StPostion1", lup_MoldWhPosition);
            //ControlEnableList.Add("StPostion2", lup_StPostion2);
            //ControlEnableList.Add("StPostion3", lup_StPostion3);
            ControlEnableList.Add("CheckCycle", lup_CheckCycle);
            ControlEnableList.Add("MoldClass", lup_MoldClass);
            ControlEnableList.Add("StdShotcnt", Spin_StdShotcnt);
            ControlEnableList.Add("RealShotcnt", Spin_RealShotcnt);
            ControlEnableList.Add("BaseShotcnt", Spin_BaseShotcnt);
            ControlEnableList.Add("SumShotcnt", Spin_SumShotcnt);
            ControlEnableList.Add("CheckShotcnt", Spin_CheckShotcnt);
            ControlEnableList.Add("DisuseDate", dt_DisuseDate);
            ControlEnableList.Add("NextCheckDate", dt_NextCheckDate);
            ControlEnableList.Add("MoldFileName", btn_MoldFileName);
            ControlEnableList.Add("MoldCheckFileName", btn_MoldCheckFileName);
            ControlEnableList.Add("Memo", tx_Memo);
            ControlEnableList.Add("UseYN", chk_UseYn);
            ControlEnableList.Add("MoldDayInspFlag", chk_MoldDayInspFlag);
            

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_MOLD1100>(new TN_MOLD1100(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            Mold1100Service = (IService<TN_MOLD1100>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            tx_MoldMCode.ReadOnly = true;

            Spin_Cavity.Properties.Mask.MaskType = MaskType.Numeric;
            Spin_Cavity.Properties.Mask.EditMask = "n5";
            Spin_Cavity.Properties.Mask.UseMaskAsDisplayFormat = true;
            Spin_Cavity.Properties.Buttons[0].Visible = false;

            Spin_StdShotcnt.Properties.Mask.MaskType = MaskType.Numeric;
            Spin_StdShotcnt.Properties.Mask.EditMask = "n5";
            Spin_StdShotcnt.Properties.Mask.UseMaskAsDisplayFormat = true;
            Spin_StdShotcnt.Properties.Buttons[0].Visible = false;

            Spin_RealShotcnt.Properties.Mask.MaskType = MaskType.Numeric;
            Spin_RealShotcnt.Properties.Mask.EditMask = "n5";
            Spin_RealShotcnt.Properties.Mask.UseMaskAsDisplayFormat = true;
            Spin_RealShotcnt.Properties.Buttons[0].Visible = false;

            Spin_BaseShotcnt.Properties.Mask.MaskType = MaskType.Numeric;
            Spin_BaseShotcnt.Properties.Mask.EditMask = "n5";
            Spin_BaseShotcnt.Properties.Mask.UseMaskAsDisplayFormat = true;
            Spin_BaseShotcnt.Properties.Buttons[0].Visible = false;

            Spin_SumShotcnt.Properties.Mask.MaskType = MaskType.Numeric;
            Spin_SumShotcnt.Properties.Mask.EditMask = "n5";
            Spin_SumShotcnt.Properties.Mask.UseMaskAsDisplayFormat = true;
            Spin_SumShotcnt.Properties.Buttons[0].Visible = false;

            Spin_CheckShotcnt.Properties.Mask.MaskType = MaskType.Numeric;
            Spin_CheckShotcnt.Properties.Mask.EditMask = "n5";
            Spin_CheckShotcnt.Properties.Mask.UseMaskAsDisplayFormat = true;
            Spin_CheckShotcnt.Properties.Buttons[0].Visible = false;

            lup_ItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_MainMachineCode.SetDefault(true, "MachineMCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList()); ;
            
            lup_MakerCust.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust).ToList());

            lup_MoldWhCode.SetDefault(true, "WhCode", DataConvert.GetCultureDataFieldName("WhName"), ModelService.GetChildList<TN_WMS1000>(p => p.WhDivision == MasterCodeSTR.WhCodeDivision_MOLD &&  p.UseFlag == "Y").ToList()); ;            
            lup_MoldWhPosition.SetDefault(true, "PositionCode", DataConvert.GetCultureDataFieldName("PositionName"), ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList());

            lup_MoldWhCode.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "TN_WMS1000." + DataConvert.GetCultureDataFieldName("WhCode"),
                Caption = LabelConvert.GetLabelText(DataConvert.GetCultureDataFieldName("WhName")),
                Visible = true
            });


           

            //lup_MoldWhPosition.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MoldPosition, 1));
            //lup_StPostion2.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MoldPosition, 2));
            //lup_StPostion3.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.MoldPosition, 2));

            lup_CheckCycle.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle));
            lup_MoldClass.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass));

            btn_MoldFileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_MoldFileName
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_MoldImage).ButtonClick;

            btn_MoldCheckFileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_MoldCheckFileName
                                                                                                                    , true
                                                                                                                    , MasterCodeSTR.FtpFolder_MoldCheckImage).ButtonClick;





        }


        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var newObj = new TN_MOLD1100()
                {
                    UseYN = "Y",
                    MoldMCode = DbRequestHandler.GetSeqStandard("MOLD"),
                    Cavity = 1, // 20220207 오세완 차장 캐비티 0이상 입력 로직 추가 
                    StdShotcnt = 0,
                    BaseShotcnt = 0,
                    SumShotcnt = 0,
                    CheckShotcnt = 0,
                    RealShotcnt = 0,
                };

                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
            }
            else
            {
                var obj = (TN_MOLD1100)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }

        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            var obj = ModelBindingSource.Current as TN_MOLD1100;
            if (obj == null)
                return; // 20220207 오세완 차장 안정성을 위해 추가

            // 20220207 오세완 차장 캐비티를 0을 입력한 건이 있어서 로직 추가 
            int iCavity = Spin_Cavity.EditValue.GetIntNullToZero();
            if (iCavity <= 0)
            {
                //string sMessage = "캐비티는 0이하를 입력할 수 없습니다.";
                string sMessage = string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_178));
                MessageBoxHandler.Show(sMessage, LabelConvert.GetLabelText("Cavitity"));
                return;
            }

            if (EditMode == PopupEditMode.New)
            {

                if (tx_MoldName.EditValue.GetNullToEmpty() == "")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("MoldName")));
                }

                if (tx_MoldCode.EditValue.GetNullToEmpty() == "")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("MoldCode")));
                }


                //금형사진파일 CHECK
                if (btn_MoldFileName.EditValue.IsNullOrEmpty())
                {
                    obj.MoldFileUrl = null;
                }
                else
                {
                    if (!obj.MoldFileUrl.IsNullOrEmpty())
                    {
                        string[] filename = obj.MoldFileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = obj.MoldMCode + "_" + filename[filename.Length - 1];
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldImage + "/" + realFileName;

                            FileHandler.UploadFTP(obj.MoldFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldImage + "/");

                            btn_MoldFileName.EditValue = realFileName;
                            btn_MoldFileName.Tag = null;
                            obj.MoldFileUrl = ftpFileUrl;
                        }
                        else if (obj.MoldFileUrl == "Clipboard_Image")
                        {
                            var realFileName = obj.MoldMCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldImage + "/" + realFileName;
                            var localImage = btn_MoldFileName.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldImage + "/");

                            btn_MoldFileName.EditValue = realFileName;
                            btn_MoldFileName.Tag = null;
                            obj.MoldFileUrl = ftpFileUrl;
                        }
                    }
                }

                if (btn_MoldCheckFileName.EditValue.IsNullOrEmpty())
                {
                    obj.MoldCheckFileUrl = null;
                }
                else
                {
                    if (!obj.MoldCheckFileUrl.IsNullOrEmpty())
                    {
                        string[] filename = obj.MoldCheckFileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = obj.MoldMCode + "_" + filename[filename.Length - 1];
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldCheckImage + "/" + realFileName;

                            FileHandler.UploadFTP(obj.MoldCheckFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldCheckImage + "/");

                            btn_MoldCheckFileName.EditValue = realFileName;
                            btn_MoldCheckFileName.Tag = null;
                            obj.MoldCheckFileUrl = ftpFileUrl;
                        }
                        else if (obj.MoldCheckFileUrl == "Clipboard_Image")
                        {
                            var realFileName = obj.MoldMCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldCheckImage + "/" + realFileName;
                            var localImage = btn_MoldCheckFileName.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldCheckImage + "/");

                            btn_MoldCheckFileName.EditValue = realFileName;
                            btn_MoldCheckFileName.Tag = null;
                            obj.MoldCheckFileUrl = ftpFileUrl;
                        }
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelBindingSource.DataSource = Mold1100Service.Insert(obj);
            }
            else
            {
                //금형파일 CHECK
                if (btn_MoldFileName.EditValue.IsNullOrEmpty())
                {
                    obj.MoldFileUrl = null;
                }
                else
                {
                    if (!obj.MoldFileUrl.IsNullOrEmpty())
                    {
                        string[] filename = obj.MoldFileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = obj.MoldMCode + "_" + filename[filename.Length - 1];
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldImage + "/" + realFileName;

                            FileHandler.UploadFTP(obj.MoldFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldImage + "/");

                            btn_MoldFileName.EditValue = realFileName;
                            btn_MoldFileName.Tag = null;
                            obj.MoldFileUrl = ftpFileUrl;
                        }
                        else if (obj.MoldFileUrl == "Clipboard_Image")
                        {
                            var realFileName = obj.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldImage + "/" + realFileName;
                            var localImage = btn_MoldFileName.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldImage + "/");

                            btn_MoldFileName.EditValue = realFileName;
                            btn_MoldFileName.Tag = null;
                            obj.MoldFileUrl = ftpFileUrl;
                        }
                    }
                }

                //금형일상점검파일 CHECK
                if (btn_MoldCheckFileName.EditValue.IsNullOrEmpty())                    
                {
                    obj.MoldCheckFileUrl = null;
                }
                else
                {
                    if (!obj.MoldCheckFileUrl.IsNullOrEmpty())
                    {
                        string[] filename = obj.MoldCheckFileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = obj.MoldMCode + "_" + filename[filename.Length - 1];
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldCheckImage + "/" + realFileName;

                            FileHandler.UploadFTP(obj.MoldCheckFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldCheckImage + "/");

                            btn_MoldCheckFileName.EditValue = realFileName;
                            btn_MoldCheckFileName.Tag = null;
                            obj.MoldCheckFileUrl = ftpFileUrl;
                        }
                        else if (obj.MoldCheckFileUrl == "Clipboard_Image")
                        {
                            var realFileName = obj.MoldCheckFileUrl + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            realFileName = FileHandler.CheckFileName(realFileName); // 20220309 오세완 차장 ftp파일명에 특수문자가 있으면 오류가 생겨서 변환 처리
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldImage + "/" + realFileName;
                            var localImage = btn_MoldCheckFileName.Tag as Image;
                            FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldCheckImage + "/");

                            btn_MoldCheckFileName.EditValue = realFileName;
                            btn_MoldCheckFileName.Tag = null;
                            obj.MoldCheckFileUrl = ftpFileUrl;
                        }
                    }
                }


                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                Mold1100Service.Update(obj);
            }

            Mold1100Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.ItemCode);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            SetSaveMessageCheck = true;
        }


        private void Btn_MoldFileName_KeyDown(object sender, KeyEventArgs e)
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

                        btn_MoldFileName.Tag = fileData;
                        btn_MoldFileName.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_MoldFileName.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_MoldFileName.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }

        }

        private void Btn_MoldCheckFileName_KeyDown(object sender, KeyEventArgs e)
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

                        btn_MoldCheckFileName.Tag = fileData;
                        btn_MoldCheckFileName.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_MoldCheckFileName.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_MoldCheckFileName.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }


        private void Btn_MoldFileName_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MOLD1100;
            if (obj == null) return;
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_MoldImage.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_MoldImage.EditValue = localfileData;
                    obj.MoldFileUrl = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_MoldImage.EditValue = localImage;
                        obj.MoldFileUrl = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.MoldFileUrl);
                        pic_MoldImage.EditValue = fileData;
                    }
                }
            }
        }


        private void Btn_MoldCheckFileName_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_MOLD1100;
            if (obj == null) return;
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_MoldCheckImage.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_MoldCheckImage.EditValue = localfileData;
                    obj.MoldCheckFileUrl = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_MoldCheckImage.EditValue = localImage;
                        obj.MoldCheckFileUrl = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.MoldCheckFileUrl);
                        pic_MoldCheckImage.EditValue = fileData;
                    }
                }
            }
        }

        private void Dt_TransferDate_EditValueChanged(object sender, EventArgs e)
        {
            

            int checkcycle = Convert.ToInt32(lup_CheckCycle.EditValue.GetNullToZero());

            DateTime transferdate = Convert.ToDateTime(dt_TransferDate.EditValue.GetNullToNull());

            if (checkcycle != 0 && transferdate != null)
            {
                dt_NextCheckDate.EditValue = transferdate.AddMonths(checkcycle.GetIntNullToZero());
            }

        }

        private void Lup_CheckCycle_EditValueChanged(object sender, EventArgs e)
        {
            
            int checkcycle = Convert.ToInt32(lup_CheckCycle.EditValue.GetNullToZero());

            DateTime transferdate = Convert.ToDateTime(dt_TransferDate.EditValue.GetNullToNull());

            if (checkcycle != 0 && transferdate != null)
            {
                dt_NextCheckDate.EditValue = transferdate.AddMonths(checkcycle.GetIntNullToZero());
            }
        }


    }
}