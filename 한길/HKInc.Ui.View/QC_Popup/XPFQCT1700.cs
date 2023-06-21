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
    public partial class XPFQCT1700 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_QCT1700> ORD1000Service;

        public XPFQCT1700(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
            buttonEdit1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit1, "Y").ButtonClick;
            buttonEdit1.EditValueChanged += BtnFile_EditValueChanged;
            lupitem.EditValueChanged += Lupitem_EditValueChanged;
            this.Text = "클레임등록";
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            ORD1000Service = (IService<TN_QCT1700>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            lupcustcode.SetDefault(false, "CustomerCode", "CustomerName", ORD1000Service.GetChildList<TN_STD1400>(p =>p.UseFlag=="Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lupempid.SetDefault(false, "LoginId", "UserName", ORD1000Service.GetChildList<UserView>(p=>p.Active=="Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lupitem.SetDefault(false, "ItemCode", "ItemCode", ORD1000Service.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lupftype.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
        }

        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                ModelBindingSource.Add(new TN_QCT1700
                {
                    ClaimNo = DbRequesHandler.GetRequestNumber("CQC")
                    ,Seq=1
                    ,ResultDate=DateTime.Today
                });
                ModelBindingSource.DataSource = ORD1000Service.Insert((TN_QCT1700)ModelBindingSource.Current);
            }
            else
            {  // Update
                TN_QCT1700 obj = (TN_QCT1700)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_QCT1700 obj = ModelBindingSource.Current as TN_QCT1700;

            if (EditMode == PopupEditMode.New)
            {
                if (obj.ClaimFile.GetNullToEmpty() != "")
                {
                    string[] ChkQcfile1cha = obj.ClaimFile.ToString().Split('\\');
                    if (ChkQcfile1cha.Length > 2)
                    {
                        FileHandler.UploadFile1(obj.ClaimFile, GlobalVariable.FTP_SERVER + "QCFAIL/");
                        obj.ClaimFile = "QCFAIL/" + ChkQcfile1cha[ChkQcfile1cha.Length - 1];
                    }
                }
                ModelBindingSource.DataSource = ORD1000Service.Insert(obj);
            }
            else
            {
                if (obj.ClaimFile.GetNullToEmpty() != "")
                {
                    string[] ChkQcfile1cha = obj.ClaimFile.ToString().Split('\\');
                    if (ChkQcfile1cha.Length > 2)
                    {
                        FileHandler.UploadFile1(obj.ClaimFile, GlobalVariable.FTP_SERVER + "QCFAIL/");
                        obj.ClaimFile = "QCFAIL/" + ChkQcfile1cha[ChkQcfile1cha.Length - 1];
                    }
                }
                ORD1000Service.Update(obj);// (SaleNote)ModelBindingSource.Current);
            }
            ORD1000Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.ClaimNo);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }

        private void Lupitem_EditValueChanged(object sender, EventArgs e)
        {
            var data = lupitem.GetSelectedDataRow() as TN_STD1100;
            if (data != null)
            {
                textItemNm1.EditValue = data.ItemNm1;
                textItemNm.EditValue = data.ItemNm;
            }
        }

        private void BtnFile_EditValueChanged(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try { fname = obj.EditValue.ToString(); }
            catch { fname = ""; }
            byte[] fileData = obj.Tag as byte[];
            pictureEdit1.EditValue = null;
            if (fileData != null)
            {
                TN_QCT1700 tn = ModelBindingSource.Current as TN_QCT1700;
                pictureEdit1.EditValue = fileData;
                tn.ClaimFile = buttonEdit1.EditValue.ToString();

            }
            else
            {
                if (fname != "")
                {
                    byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                    pictureEdit1.EditValue = img;
                }
                else
                {
                    obj.EditValue = "";
                }
            }
        }

    }
}