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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.STD_POPUP
{
    public partial class XPFSTD4M001 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD4M001> ModelService;

        public XPFSTD4M001(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
            this.Text = "4M등록";
        }


        protected override void AddControlList()
        {
            ControlEnableList.Add("L4mno", tx_L4mno);
            ControlEnableList.Add("ItemCode", lupitemcode);
            ControlEnableList.Add("Seq", tx_Seq);
            ControlEnableList.Add("CustCode", lupmakecust);
            ControlEnableList.Add("CarType", tx_CarType);
            ControlEnableList.Add("ChgCust", lupchkcust);
            ControlEnableList.Add("ChgDate", tx_ChangeDate);
            ControlEnableList.Add("ReqCust", luprqcust);
            ControlEnableList.Add("ReqUser", luprequser);
            ControlEnableList.Add("ChgNote", tx_ChangeContent);
            ControlEnableList.Add("ChgMemo", tx_ChangeCause);
            ControlEnableList.Add("FinalUser", lupfinaluser);
            ControlEnableList.Add("ProdWorkDate", tx_MassProductDate);
            //ControlEnableList.Add("ReqDoc", btn_RequestDocument);
            ControlEnableList.Add("ReqDocFileName", btn_RequestDocument);
            ControlEnableList.Add("ChkCust1Cha", lupChkCust1Cha);
            ControlEnableList.Add("ChkDate1Cha", tx_CheckFirstDate);
            ControlEnableList.Add("ChkQc1Cha", tx_CheckFirstQC);
            ControlEnableList.Add("ChkQcUser1Cha", lupqcuser1Cha);
            //ControlEnableList.Add("ChkQcfile1Cha", btn_CheckFirstFile);
            ControlEnableList.Add("QcFileName1", btn_CheckFirstFile);
            //ControlEnableList.Add("QcFileData1", btn_CheckFirstFile);
            ControlEnableList.Add("ChkCust2Cha", lupChkCust2Cha);
            ControlEnableList.Add("ChkDate2Cha", tx_CheckSecondDate);
            ControlEnableList.Add("ChkQc2Cha", tx_CheckSecondQC);
            ControlEnableList.Add("ChkQcUser2Cha", lupqcuser2Cha);
            //ControlEnableList.Add("ChkQcfile2Cha", btn_CheckSecondFile);

            ControlEnableList.Add("QcFileName2", btn_CheckSecondFile);
            ControlEnableList.Add("ChkCust3Cha", lupChkCust3Cha);
            ControlEnableList.Add("ChkDate3Cha", tx_CheckThirdDate);
            ControlEnableList.Add("ChkQc3Cha", tx_CheckThirdQC);
            ControlEnableList.Add("ChkQcUser3Cha", lupqcuser3Cha);
            //ControlEnableList.Add("ChkQcfile3Cha", btn_CheckThirdFile);
            ControlEnableList.Add("QcFileName3", btn_CheckThirdFile);
            //ControlEnableList.Add("EtcFile1", btn_EtcFile1);
            //ControlEnableList.Add("EtcFile2", btn_EtcFile2);
            //ControlEnableList.Add("EtcFile3", btn_EtcFile3);
            //ControlEnableList.Add("EtcFile4", btn_EtcFile4);
            //ControlEnableList.Add("EtcFile5", btn_EtcFile5);
            ControlEnableList.Add("EtcFileName1", btn_EtcFile1);
            ControlEnableList.Add("EtcFileName2", btn_EtcFile2);
            ControlEnableList.Add("EtcFileName3", btn_EtcFile3);
            ControlEnableList.Add("EtcFileName4", btn_EtcFile4);
            ControlEnableList.Add("EtcFileName5", btn_EtcFile5);
            ControlEnableList.Add("Memo1", tx_Memo1);
            ControlEnableList.Add("Memo2", tx_Memo2);
            ControlEnableList.Add("Memo3", tx_Memo3);
            ControlEnableList.Add("Memo4", tx_Memo4);

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            btn_CheckFirstFile.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "QcFileData1", true));
            btn_CheckSecondFile.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "QcFileData2", true));
            btn_CheckThirdFile.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "QcFileData3", true));
            btn_EtcFile1.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "EtcFileData1", true));
            btn_EtcFile2.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "EtcFileData2", true));
            btn_EtcFile3.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "EtcFileData3", true));
            btn_EtcFile4.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "EtcFileData4", true));
            btn_EtcFile5.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "EtcFileData5", true));
            btn_RequestDocument.DataBindings.Add(new Binding("Tag", this.ModelBindingSource, "ReqDocFileData", true));

            LayoutControlHandler.SetRequiredLabelText<TN_STD4M001>(new TN_STD4M001(), ControlEnableList, this.Controls);
        }

        protected override void InitControls()
        {
            base.InitControls();
            //btn_RequestDocument.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_RequestDocument,"Y").ButtonClick;
            btn_RequestDocument.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_RequestDocument, false).ButtonClick;
            btn_CheckFirstFile.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_CheckFirstFile, "Y").ButtonClick;
            btn_CheckSecondFile.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_CheckSecondFile, "Y").ButtonClick;
            btn_CheckThirdFile.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_CheckThirdFile, "Y").ButtonClick;
            btn_EtcFile1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_EtcFile1, "Y").ButtonClick;
            btn_EtcFile2.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_EtcFile2, "Y").ButtonClick;
            btn_EtcFile3.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_EtcFile3, "Y").ButtonClick;
            btn_EtcFile4.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_EtcFile4, "Y").ButtonClick;
            btn_EtcFile5.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(btn_EtcFile5, "Y").ButtonClick;
            btn_RequestDocument.EditValueChanged += BtnFile_EditValueChanged8;
            btn_CheckFirstFile.EditValueChanged += BtnFile_EditValueChanged;
            btn_CheckSecondFile.EditValueChanged += BtnFile_EditValueChanged1;
            btn_CheckThirdFile.EditValueChanged += BtnFile_EditValueChanged2;
            btn_EtcFile1.EditValueChanged += BtnFile_EditValueChanged3;
            btn_EtcFile2.EditValueChanged += BtnFile_EditValueChanged4;
            btn_EtcFile3.EditValueChanged += BtnFile_EditValueChanged5;
            btn_EtcFile4.EditValueChanged += BtnFile_EditValueChanged6;
            btn_EtcFile5.EditValueChanged += BtnFile_EditValueChanged7;
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            ModelService = (IService<TN_STD4M001>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            lupmakecust.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupChkCust1Cha.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupChkCust2Cha.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupChkCust3Cha.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            luprqcust.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupchkcust.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupfinaluser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lupqcuser1Cha.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lupqcuser2Cha.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            lupqcuser3Cha.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            luprequser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList());
            //lupcartype.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.CarType));
            lupitemcode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.TopCategory == MasterCodeSTR.TopCategory_WAN && p.UseFlag == "Y").ToList());

            tx_L4mno.ReadOnly = true;
        }

        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                ModelBindingSource.Add(new TN_STD4M001 { L4mno = DbRequestHandler.GetSeqYear("4M") });
                ModelBindingSource.DataSource = ModelService.Insert((TN_STD4M001)ModelBindingSource.Current);
            }
            else
            {  
                // Update
                TN_STD4M001 obj = (TN_STD4M001)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_STD4M001 obj = ModelBindingSource.Current as TN_STD4M001;

            if (btn_CheckFirstFile.EditValue.GetNullToEmpty() != "")
            {
                string[] ChkQcfile1cha = obj.QcFileName1.ToString().Split('\\');
                if (ChkQcfile1cha.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "Qc1_" + ChkQcfile1cha[ChkQcfile1cha.Length - 1];
                    obj.QcFileName1 = realFileName;
                }
            }
            else
            {
                obj.QcFileName1 = null;
                obj.QcFileData1 = null;
            }

            if (btn_CheckSecondFile.EditValue.GetNullToEmpty() != "")
            {
                string[] ChkQcfile2cha = obj.QcFileName2.ToString().Split('\\');
                if (ChkQcfile2cha.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "Qc2_" + ChkQcfile2cha[ChkQcfile2cha.Length - 1];
                    obj.QcFileName2 = realFileName;
                }
            }
            else
            {
                obj.QcFileName2 = null;
                obj.QcFileData2 = null;
            }

            if (btn_CheckThirdFile.EditValue.GetNullToEmpty() != "")
            {
                string[] ChkQcfile3cha = obj.QcFileName3.ToString().Split('\\');
                if (ChkQcfile3cha.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "Qc3_" + ChkQcfile3cha[ChkQcfile3cha.Length - 1];
                    obj.QcFileName3 = realFileName;
                }
            }
            else
            {
                obj.QcFileName3 = null;
                obj.QcFileData3 = null;
            }

            if (btn_RequestDocument.EditValue.GetNullToEmpty() != "")
            {
                string[] Reqdoc = obj.ReqDoc.ToString().Split('\\');
                if (Reqdoc.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "Reqdoc_" + Reqdoc[Reqdoc.Length - 1];
                    obj.ReqDocFileName = realFileName;
                    //obj.ReqDocFileData = btn_RequestDocument.Tag;
                }
            }
            else
            {
                obj.ReqDocFileName = null;
                obj.ReqDocFileData = null;
            }

            if (btn_EtcFile1.EditValue.GetNullToEmpty() != "")
            {
                string[] EtcFile1 = obj.EtcFileName1.ToString().Split('\\');
                if (EtcFile1.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "EtcFile1_" + EtcFile1[EtcFile1.Length - 1];
                    obj.EtcFileName1 = realFileName;
                }
            }
            else
            {
                obj.EtcFileName1 = null;
                obj.EtcFileData1 = null;
            }

            if (btn_EtcFile2.EditValue.GetNullToEmpty() != "")
            {
                string[] EtcFile2 = obj.EtcFileName2.ToString().Split('\\');
                if (EtcFile2.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "EtcFile2_" + EtcFile2[EtcFile2.Length - 1];
                    obj.EtcFileName2 = realFileName;
                }
            }
            else
            {
                obj.EtcFileName2 = null;
                obj.EtcFileData2 = null;
            }

            if (btn_EtcFile3.EditValue.GetNullToEmpty() != "")
            {
                string[] EtcFile3 = obj.EtcFileName3.ToString().Split('\\');
                if (EtcFile3.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "EtcFile3_" + EtcFile3[EtcFile3.Length - 1];
                    obj.EtcFileName3 = realFileName;
                }
            }
            else
            {
                obj.EtcFileName3 = null;
                obj.EtcFileData3 = null;
            }

            if (btn_EtcFile4.EditValue.GetNullToEmpty() != "")
            {
                string[] EtcFile4 = obj.EtcFileName4.ToString().Split('\\');
                if (EtcFile4.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "EtcFile4_" + EtcFile4[EtcFile4.Length - 1];
                    obj.EtcFileName4 = realFileName;
                }
            }
            else
            {
                obj.EtcFileName4 = null;
                obj.EtcFileData4 = null;
            }

            if (btn_EtcFile5.EditValue.GetNullToEmpty() != "")
            {
                string[] EtcFile5 = obj.EtcFileName5.ToString().Split('\\');
                if (EtcFile5.Length > 2)
                {
                    var realFileName = obj.Seq.ToString() + "EtcFile5_" + EtcFile5[EtcFile5.Length - 1];
                    obj.EtcFileName5 = realFileName;
                }
            }
            else
            {
                obj.EtcFileName5 = null;
                obj.EtcFileData5 = null;
            }

            #region 주석
            //if (obj.ChkQcFile1Cha.GetNullToEmpty() != "")
            //{
            //string[] ChkQcfile1cha = obj.ChkQcFile1Cha.ToString().Split('\\');
            //if (ChkQcfile1cha.Length > 2)
            //{
            //    var realFileName = obj.Seq.ToString() + "Qc1_" + ChkQcfile1cha[ChkQcfile1cha.Length - 1];
            //    //var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //    //FileHandler.UploadFTP(obj.ChkQcFile1Cha, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");                                       
            //    //obj.ChkQcFile1Cha = ftpFileUrl;

            //    obj.QcFileName1 = realFileName;
            //    //obj.QcFileData1 = 
            //}
            //}

            //if (obj.ChkQcFile2Cha.GetNullToEmpty() != "")
            //{
            //    string[] ChkQcfile2cha = obj.ChkQcFile2Cha.ToString().Split('\\');
            //    if (ChkQcfile2cha.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "Qc2_" + ChkQcfile2cha[ChkQcfile2cha.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.ChkQcFile2Cha, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.ChkQcFile2Cha = ftpFileUrl;
            //    }
            //}

            //if (obj.ChkQcFile3Cha.GetNullToEmpty() != "")
            //{
            //    string[] ChkQcfile3cha = obj.ChkQcFile3Cha.ToString().Split('\\');
            //    if (ChkQcfile3cha.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "Qc3_" + ChkQcfile3cha[ChkQcfile3cha.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.ChkQcFile3Cha, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.ChkQcFile3Cha = ftpFileUrl;
            //    }
            //}

            //if (obj.ReqDoc.GetNullToEmpty() != "")
            //{
            //    string[] Reqdoc = obj.ReqDoc.ToString().Split('\\');
            //    if (Reqdoc.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "Reqdoc_" + Reqdoc[Reqdoc.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.ReqDoc, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.ReqDoc = ftpFileUrl;
            //    }
            //}


            //if (obj.EtcFile1.GetNullToEmpty() != "")
            //{
            //    string[] EtcFile1 = obj.EtcFile1.ToString().Split('\\');
            //    if (EtcFile1.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "EtcFile1_" + EtcFile1[EtcFile1.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.EtcFile1, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.EtcFile1 = ftpFileUrl;
            //    }
            //}

            //if (obj.EtcFile2.GetNullToEmpty() != "")
            //{
            //    string[] EtcFile2 = obj.EtcFile2.ToString().Split('\\');
            //    if (EtcFile2.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "EtcFile2_" + EtcFile2[EtcFile2.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.EtcFile2, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.EtcFile2 = ftpFileUrl;
            //    }
            //}

            //if (obj.EtcFile3.GetNullToEmpty() != "")
            //{
            //    string[] EtcFile3 = obj.EtcFile3.ToString().Split('\\');
            //    if (EtcFile3.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "EtcFile3_" + EtcFile3[EtcFile3.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.EtcFile3, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.EtcFile3 = ftpFileUrl;
            //    }
            //}

            //if (obj.EtcFile4.GetNullToEmpty() != "")
            //{
            //    string[] EtcFile4 = obj.EtcFile4.ToString().Split('\\');
            //    if (EtcFile4.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "EtcFile4_" + EtcFile4[EtcFile4.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.EtcFile4, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.EtcFile4 = ftpFileUrl;
            //    }
            //}

            //if (obj.EtcFile5.GetNullToEmpty() != "")
            //{
            //    string[] EtcFile5 = obj.EtcFile5.ToString().Split('\\');
            //    if (EtcFile5.Length > 2)
            //    {
            //        var realFileName = obj.Seq.ToString() + "EtcFile5_" + EtcFile5[EtcFile5.Length - 1];
            //        var ftpFileUrl = MasterCodeSTR.FtpFolder_4M + "/" + realFileName;
            //        FileHandler.UploadFTP(obj.EtcFile5, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_4M + "/");
            //        obj.EtcFile5 = ftpFileUrl;
            //    }
            //}
            #endregion


            if (EditMode == PopupEditMode.New)
            {
                obj.Seq = Convert.ToInt32(DbRequestHandler.GetCellValue("SELECT isnull(max([SEQ]),0)+1 FROM TN_STD4M001T where ITEM_CODE='" + obj.ItemCode + "'", 0));
                ModelBindingSource.DataSource = ModelService.Insert(obj);
            }
            else
            {
                ModelService.Update(obj);
            }
            ModelService.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.RowId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            this.Close();
        }

        private void BtnFile_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit2.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit2.EditValue = fileData;
                    obj.QcFileName1 = fileName;
                    obj.QcFileData1 = fileData;
                }
                else
                {
                    var data = obj.QcFileData1;
                    pictureEdit2.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged1(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit4.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit4.EditValue = fileData;
                    obj.QcFileName2 = fileName;
                    obj.QcFileData2 = fileData;
                }
                else
                {
                    var data = obj.QcFileData2;
                    pictureEdit4.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged2(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit5.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit5.EditValue = fileData;
                    obj.QcFileName3 = fileName;
                    obj.QcFileData3 = fileData;
                }
                else
                {
                    var data = obj.QcFileData3;
                    pictureEdit5.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged3(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit6.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit6.EditValue = fileData;
                    obj.EtcFileName1 = fileName;
                    obj.EtcFileData1 = fileData;
                }
                else
                {
                    var data = obj.EtcFileData1;
                    pictureEdit6.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged4(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit7.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit7.EditValue = fileData;
                    obj.EtcFileName2 = fileName;
                    obj.EtcFileData2 = fileData;
                }
                else
                {
                    var data = obj.EtcFileData2;
                    pictureEdit7.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged5(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit8.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit8.EditValue = fileData;
                    obj.EtcFileName3 = fileName;
                    obj.EtcFileData3 = fileData;
                }
                else
                {
                    var data = obj.EtcFileData3;
                    pictureEdit8.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged6(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit10.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit10.EditValue = fileData;
                    obj.EtcFileName4 = fileName;
                    obj.EtcFileData4 = fileData;
                }
                else
                {
                    var data = obj.EtcFileData4;
                    pictureEdit10.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged7(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                pictureEdit9.EditValue = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    pictureEdit9.EditValue = fileData;
                    obj.EtcFileName5 = fileName;
                    obj.EtcFileData5 = fileData;
                }
                else
                {
                    var data = obj.EtcFileData5;
                    pictureEdit9.EditValue = data;
                }
            }
        }
        private void BtnFile_EditValueChanged8(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD4M001;
            if (obj == null) return;

            ButtonEdit button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToNull();

            if (fileName.IsNullOrEmpty())
            {
                //pictureEdit6.EditValue = null;
                //obj.ReqDocFileData = null;
                button.Tag = null;
            }
            else
            {
                byte[] fileData = button.Tag as byte[];

                if (fileData != null)
                {
                    //pictureEdit6.EditValue = fileData;
                    //obj.EtcFileName1 = fileName;
                    //obj.EtcFileData1 = fileData;
                    obj.ReqDocFileName = fileName;
                    obj.ReqDocFileData = fileData;
                }
                else
                {
                    var data = obj.ReqDocFileData;
                    //pictureEdit6.EditValue = data;
                    button.Tag = data;
                }
            }
        }

        //private void BtnFile_EditValueChanged(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit2.EditValue = null;
        //    if (fileData != null) { pictureEdit2.EditValue = fileData; }
        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit2.EditValue = img;

        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
        //private void BtnFile_EditValueChanged1(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit4.EditValue = null;
        //    if (fileData != null) { pictureEdit4.EditValue = fileData; }

        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit4.EditValue = img;
        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
        //private void BtnFile_EditValueChanged2(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit5.EditValue = null;
        //    if (fileData != null) { pictureEdit5.EditValue = fileData; }
        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit5.EditValue = img;
        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
        //private void BtnFile_EditValueChanged3(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit6.EditValue = null;
        //    if (fileData != null) { pictureEdit6.EditValue = fileData; }
        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit6.EditValue = img;
        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
        //private void BtnFile_EditValueChanged4(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit7.EditValue = null;
        //    if (fileData != null) { pictureEdit7.EditValue = fileData; }
        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit7.EditValue = img;
        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
        //private void BtnFile_EditValueChanged5(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit8.EditValue = null;
        //    if (fileData != null) { pictureEdit8.EditValue = fileData; }
        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit8.EditValue = img;
        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
        //private void BtnFile_EditValueChanged6(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit10.EditValue = null;
        //    if (fileData != null) { pictureEdit10.EditValue = fileData; }
        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit10.EditValue = img;
        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
        //private void BtnFile_EditValueChanged7(object sender, EventArgs e)
        //{
        //    ButtonEdit obj = sender as ButtonEdit;
        //    string fname = "";
        //    try { fname = obj.EditValue.ToString(); }
        //    catch { fname = ""; }
        //    byte[] fileData = obj.Tag as byte[];
        //    pictureEdit9.EditValue = null;
        //    if (fileData != null) { pictureEdit9.EditValue = fileData; }
        //    else
        //    {
        //        if (fname != "")
        //        {
        //            byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
        //            pictureEdit9.EditValue = img;
        //        }
        //        else
        //        {
        //            obj.EditValue = "";
        //        }
        //    }
        //}
    }
}