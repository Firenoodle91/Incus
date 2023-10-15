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

namespace HKInc.Ui.View.STD_Popup
{
    public partial class XPFSTD4M001 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD4M001> MEA1000Service;
        public XPFSTD4M001(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
            this.Text = "4M등록";
        }

        protected override void InitControls()
        {
            base.InitControls();
           // buttonEdit1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit1,"Y").ButtonClick;
            buttonEdit2.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit2,"Y").ButtonClick;
            buttonEdit3.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit3,"Y").ButtonClick;
            buttonEdit4.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit4,"Y").ButtonClick;
            buttonEdit5.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit5,"Y").ButtonClick;
            buttonEdit6.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit6,"Y").ButtonClick;
            buttonEdit7.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit7,"Y").ButtonClick;
            buttonEdit8.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit8,"Y").ButtonClick;
            buttonEdit9.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit9, "Y").ButtonClick;
            buttonEdit2.EditValueChanged += BtnFile_EditValueChanged;
            buttonEdit3.EditValueChanged += BtnFile_EditValueChanged1;
            buttonEdit4.EditValueChanged += BtnFile_EditValueChanged2;
            buttonEdit5.EditValueChanged += BtnFile_EditValueChanged3;
            buttonEdit6.EditValueChanged += BtnFile_EditValueChanged4;
            buttonEdit7.EditValueChanged += BtnFile_EditValueChanged5;
            buttonEdit8.EditValueChanged += BtnFile_EditValueChanged6;
            buttonEdit9.EditValueChanged += BtnFile_EditValueChanged7;

        }
        protected override void InitCombo()
        {
            lupmakecust.SetDefault(true, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupChkCust1cha.SetDefault(true, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupChkCust2cha.SetDefault(true, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupChkCust3cha.SetDefault(true, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            luprqcust.SetDefault(true, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupchkcust.SetDefault(true, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lupfinaluser.SetDefault(true, "LoginId", "UserName", MEA1000Service.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupqcuser1cha.SetDefault(true, "LoginId", "UserName", MEA1000Service.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupqcuser2cha.SetDefault(true, "LoginId", "UserName", MEA1000Service.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupqcuser3cha.SetDefault(true, "LoginId", "UserName", MEA1000Service.GetChildList<UserView>(p => p.Active == "Y").ToList());
            luprequser.SetDefault(true, "LoginId", "UserName", MEA1000Service.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lupcartype.SetDefault(true,  "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE));
            lcitemcode.SetDefault(true, "ItemCode", "ItemNm", MEA1000Service.GetChildList<TN_STD1100>(p => p.TopCategory == "P01" && p.UseYn == "Y").ToList()); ;
      
        }
        private void BtnFile_EditValueChanged(object sender, EventArgs e)
        {           
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try   { fname = obj.EditValue.ToString();}
            catch { fname = ""; }           
                byte[] fileData = obj.Tag as byte[];
                pictureEdit2.EditValue = null;
                if (fileData != null){ pictureEdit2.EditValue = fileData;}
               else {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit2.EditValue = img;
                }
                else {
                    obj.EditValue = "";
                }
                     }
        }
        private void BtnFile_EditValueChanged1(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit4.EditValue = null;
            if (fileData != null) { pictureEdit4.EditValue = fileData; }
          
                else {
                    if (fname != "")
                    {
                        byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                        pictureEdit4.EditValue = img;
                    }
                    else
                    {
                        obj.EditValue = "";
                    }
                }
            }
        private void BtnFile_EditValueChanged2(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit5.EditValue = null;
            if (fileData != null) { pictureEdit5.EditValue = fileData; }
            else
            {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit5.EditValue = img;
                }
                else
                {
                    obj.EditValue = "";
                }
            }
        }
        private void BtnFile_EditValueChanged3(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit6.EditValue = null;
            if (fileData != null) { pictureEdit6.EditValue = fileData; }
            else
            {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit6.EditValue = img;
                }
                else
                {
                    obj.EditValue = "";
                }
            }
        }
        private void BtnFile_EditValueChanged4(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit7.EditValue = null;
            if (fileData != null) { pictureEdit7.EditValue = fileData; }
            else
            {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit7.EditValue = img;
                }
                else
                {
                    obj.EditValue = "";
                }
            }
        }
        private void BtnFile_EditValueChanged5(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit8.EditValue = null;
            if (fileData != null) { pictureEdit8.EditValue = fileData; }
            else
            {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit8.EditValue = img;
                }
                else
                {
                    obj.EditValue = "";
                }
            }
        }
        private void BtnFile_EditValueChanged6(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit10.EditValue = null;
            if (fileData != null) { pictureEdit10.EditValue = fileData; }
            else
            {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit10.EditValue = img;
                }
                else
                {
                    obj.EditValue = "";
                }
            }
        }
        private void BtnFile_EditValueChanged7(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit9.EditValue = null;
            if (fileData != null) { pictureEdit9.EditValue = fileData; }
            else
            {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit9.EditValue = img;
                }
                else
                {
                    obj.EditValue = "";
                }
            }
        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            MEA1000Service = (IService<TN_STD4M001>)PopupParam.GetValue(PopupParameter.Service);
        }
      
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {

                ModelBindingSource.Add(new TN_STD4M001 { L4mno = DbRequesHandler.GetRequestNumber("4M") });


                ModelBindingSource.DataSource = MEA1000Service.Insert((TN_STD4M001)ModelBindingSource.Current);



                this.Refresh();

            }
            else
            {  // Update
                TN_STD4M001 obj = (TN_STD4M001)PopupParam.GetValue(PopupParameter.KeyValue);

                ModelBindingSource.DataSource = obj;

            }
            textEdit1.ReadOnly = true;
      
        }

        protected override void DataSave()
        {


            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_STD4M001 obj = ModelBindingSource.Current as TN_STD4M001;

            if (obj.ChkQcfile1cha.GetNullToEmpty() != "")
            {
                string[] ChkQcfile1cha = obj.ChkQcfile1cha.ToString().Split('\\');
                if (ChkQcfile1cha.Length > 2)
                {
                    FileHandler.UploadFile1(obj.ChkQcfile1cha, GlobalVariable.FTP_SERVER + "4M/");
                    obj.ChkQcfile1cha = "4M/" + ChkQcfile1cha[ChkQcfile1cha.Length - 1];
                }
            }
            if (obj.ChkQcfile2cha.GetNullToEmpty() != "")
            {
                string[] ChkQcfile2cha = obj.ChkQcfile2cha.ToString().Split('\\');
                if (ChkQcfile2cha.Length > 2)
                {
                    FileHandler.UploadFile1(obj.ChkQcfile2cha, GlobalVariable.FTP_SERVER + "4M/");
                    // string[] ChkQcfile2cha = obj.ChkQcfile2cha.ToString().Split('\\');
                    obj.ChkQcfile2cha = "4M/" + ChkQcfile2cha[ChkQcfile2cha.Length - 1];
                }
            }

            if (obj.ChkQcfile3cha.GetNullToEmpty() != "")
            {
                string[] ChkQcfile3cha = obj.ChkQcfile3cha.ToString().Split('\\');
                if (ChkQcfile3cha.Length > 2)
                {
                    FileHandler.UploadFile1(obj.ChkQcfile3cha, GlobalVariable.FTP_SERVER + "4M/");
                    //  string[] ChkQcfile3cha = obj.ChkQcfile3cha.ToString().Split('\\');
                    obj.ChkQcfile3cha = "4M/" + ChkQcfile3cha[ChkQcfile3cha.Length - 1];
                }
            }

            if (obj.ReqDoc.GetNullToEmpty() != "")
            {
                string[] ReqDoc = obj.ReqDoc.ToString().Split('\\');
                if (ReqDoc.Length > 2)
                {
                    FileHandler.UploadFile1(obj.ReqDoc, GlobalVariable.FTP_SERVER + "4M/");
                  //  string[] ReqDoc = obj.ReqDoc.ToString().Split('\\');
                    obj.ReqDoc = "4M/" + ReqDoc[ReqDoc.Length - 1];
                }
            }

            if (obj.EtcFile1.GetNullToEmpty() != "")
            {
                string[] EtcFile1 = obj.EtcFile1.ToString().Split('\\');
                if (EtcFile1.Length > 2)
                {
                    FileHandler.UploadFile1(obj.EtcFile1, GlobalVariable.FTP_SERVER + "4M/");
                    // string[] EtcFile1 = obj.EtcFile1.ToString().Split('\\');
                    obj.EtcFile1 = "4M/" + EtcFile1[EtcFile1.Length - 1];
                }
            }

            if (obj.EtcFile2.GetNullToEmpty() != "")
            {
                string[] EtcFile2 = obj.EtcFile2.ToString().Split('\\');
                if (EtcFile2.Length > 2)
                {
                    FileHandler.UploadFile1(obj.EtcFile2, GlobalVariable.FTP_SERVER + "4M/");
                    //  string[] EtcFile2 = obj.EtcFile2.ToString().Split('\\');
                    obj.EtcFile2 = "4M/" + EtcFile2[EtcFile2.Length - 1];
                }
            }

            if (obj.EtcFile3.GetNullToEmpty() != "")
            {
                string[] EtcFile3 = obj.EtcFile3.ToString().Split('\\');
                if (EtcFile3.Length > 2)
                {
                    FileHandler.UploadFile1(obj.EtcFile3, GlobalVariable.FTP_SERVER + "4M/");
                    // string[] EtcFile3 = obj.EtcFile3.ToString().Split('\\');
                    obj.EtcFile3 = "4M/" + EtcFile3[EtcFile3.Length - 1];
                }
            }

            if (obj.EtcFile4.GetNullToEmpty() != "")
            {
                string[] EtcFile4 = obj.EtcFile4.ToString().Split('\\');
                if (EtcFile4.Length > 2)
                {
                    FileHandler.UploadFile1(obj.EtcFile4, GlobalVariable.FTP_SERVER + "4M/");
                    //string[] EtcFile4 = obj.EtcFile4.ToString().Split('\\');
                    obj.EtcFile4 = "4M/" + EtcFile4[EtcFile4.Length - 1];
                }
            }

            if (obj.EtcFile5.GetNullToEmpty() != "")
            {
                string[] EtcFile5 = obj.EtcFile5.ToString().Split('\\');
                if (EtcFile5.Length > 2)
                {
                    FileHandler.UploadFile1(obj.EtcFile5, GlobalVariable.FTP_SERVER + "4M/");
                    //   string[] EtcFile5 = obj.EtcFile5.ToString().Split('\\');
                    obj.EtcFile5 = "4M/" + EtcFile5[EtcFile5.Length - 1];
                }
            }
        



            if (EditMode == PopupEditMode.New)
            {

                obj.Seq = Convert.ToInt32(DbRequesHandler.GetCellValue("SELECT  isnull(max([SEQ]),0)+1 FROM TN_STD4M001T where ITEM_CODE='" + obj.ItemCode + "'", 0));
                ModelBindingSource.DataSource = MEA1000Service.Insert(obj);
            }
            else
            {
                MEA1000Service.Update(obj);
            }
            MEA1000Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.RowId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            this.Close();
        }

      
    }
}