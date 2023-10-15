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

namespace HKInc.Ui.View.QC_Popup
{
    public partial class XPFQCT2200 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_QCT2200> MEA1000Service;
        public XPFQCT2200(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
            this.Text = "검사의뢰관리";
        }

        protected override void InitControls()
        {
            base.InitControls();
            buttonEdit1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit1,"Y").ButtonClick;
            buttonEdit1.EditValueChanged += BtnFile_EditValueChanged;
            buttonEdit2.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit2,"Y").ButtonClick;
            buttonEdit2.EditValueChanged += BtnFile2_EditValueChanged;

        }
        protected override void InitCombo()
        {
            lup_cust.SetDefault(false, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_item.SetDefault(false, "ItemCode", "ItemNm", MEA1000Service.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
            lup_qcuser.SetDefault(false, "LoginId", "UserName", MEA1000Service.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lup_requser.SetDefault(false, "LoginId", "UserName", MEA1000Service.GetChildList<UserView>(p => p.Active == "Y").ToList());
        }
        private void BtnFile_EditValueChanged(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            byte[] fileData = obj.Tag as byte[];
            

        }
        private void BtnFile2_EditValueChanged(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            byte[] fileData = obj.Tag as byte[];


        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            MEA1000Service = (IService<TN_QCT2200>)PopupParam.GetValue(PopupParameter.Service);
        }
      
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {

                ModelBindingSource.Add(new TN_QCT2200 { No = DbRequestHandler.GetRequestNumberNew("RQC")});


                ModelBindingSource.DataSource = MEA1000Service.Insert((TN_QCT2200)ModelBindingSource.Current);



                this.Refresh();

            }
            else
            {  // Update
                TN_QCT2200 obj = (TN_QCT2200)PopupParam.GetValue(PopupParameter.KeyValue);

                ModelBindingSource.DataSource = obj;

            }
            textEdit1.ReadOnly = true;
      
        }

        protected override void DataSave()
        {
         

            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_QCT2200 obj = ModelBindingSource.Current as TN_QCT2200;

            if (obj.ReqFile.GetNullToEmpty() != "")
            {
                string[] ChkQcfile1cha = obj.ReqFile.ToString().Split('\\');
                if (ChkQcfile1cha.Length > 2)
                {
                    FileHandler.UploadFile1(obj.ReqFile, GlobalVariable.FTP_SERVER + "RQC/");
                    obj.ReqFile = "RQC/" + ChkQcfile1cha[ChkQcfile1cha.Length - 1];
                }
            }
            if (obj.CheckFile.GetNullToEmpty() != "")
            {
                string[] ChkQcfile2cha = obj.CheckFile.ToString().Split('\\');
                if (ChkQcfile2cha.Length > 2)
                {
                    FileHandler.UploadFile1(obj.CheckFile, GlobalVariable.FTP_SERVER + "RQC/");
                    // string[] ChkQcfile2cha = obj.ChkQcfile2cha.ToString().Split('\\');
                    obj.CheckFile = "RQC/" + ChkQcfile2cha[ChkQcfile2cha.Length - 1];
                }
                if (EditMode == PopupEditMode.New)
                {


                    ModelBindingSource.DataSource = MEA1000Service.Insert(obj);
                }
                else
                {
                    MEA1000Service.Update(obj);
                }
            }
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