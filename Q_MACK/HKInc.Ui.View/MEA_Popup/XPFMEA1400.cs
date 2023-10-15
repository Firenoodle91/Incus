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

namespace HKInc.Ui.View.MEA_Popup
{
    public partial class XPFMEA1400 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_MOLD001> MEA1000Service;
        public XPFMEA1400(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
            this.Text = "금형등록";
        }

        protected override void InitControls()
        {
            base.InitControls();
            buttonEdit1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit1).ButtonClick;
            buttonEdit1.EditValueChanged += BtnFile_EditValueChanged;
     
        }
        protected override void InitCombo()
        {
            lupmakecust.SetDefault(true, "CustomerCode", "CustomerName", MEA1000Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());

            lcitemcode.SetDefault(true, "ItemCode", "ItemNm", MEA1000Service.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList()); ;
            lupMstMc.SetDefault(true, "MachineCode", "MachineName", MEA1000Service.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList()); ;
            lupstpostion1.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 1));
            lupstpostion2.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 2));
            lupstpostion3.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSITION, 3));
            lupcheckCycle.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.CHECKCYCLE));
            lupClass.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDCLASS));
            //lupstpostion.SetDefault(false, "CodeId", "CodeName", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
        }
        private void BtnFile_EditValueChanged(object sender, EventArgs e)
        {
            //TN_MOLD001 obj1 = ModelBindingSource.Current as TN_MOLD001;
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try
            {
                fname = obj.EditValue.ToString();
            //    obj1.Imgurl = obj.EditValue.ToString();
            }
            catch { fname = "";// obj1.Imgurl = "";
            }

           
                byte[] fileData = obj.Tag as byte[];
                pictureEdit1.EditValue = null;
                if (fileData != null)
                {
               
                    pictureEdit1.EditValue = fileData;
                
                }
               else
        
            {

              
                byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                pictureEdit1.EditValue = img;


            }

            
        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            MEA1000Service = (IService<TN_MOLD001>)PopupParam.GetValue(PopupParameter.Service);
        }
      
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {

                ModelBindingSource.Add(new TN_MOLD001 { MoldMcode = DbRequestHandler.GetRequestNumberNew("MOLD") ,UseYN="Y"});


                ModelBindingSource.DataSource = MEA1000Service.Insert((TN_MOLD001)ModelBindingSource.Current);



                this.Refresh();

            }
            else
            {  // Update
                TN_MOLD001 obj = (TN_MOLD001)PopupParam.GetValue(PopupParameter.KeyValue);

                ModelBindingSource.DataSource = obj;

            }
            textEdit1.ReadOnly = true;
      
        }

        protected override void DataSave()
        {
         

            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_MOLD001 obj = ModelBindingSource.Current as TN_MOLD001;
            if (buttonEdit1.EditValue.GetNullToEmpty() == "") obj.Imgurl = "";
            if (obj.Imgurl.GetNullToEmpty() != "")
            {
                string[] filename = obj.Imgurl.ToString().Split('\\');
                if (filename.Length != 1)
                {
                    FileHandler.UploadFile1(obj.Imgurl, GlobalVariable.FTP_SERVER + "MOLD/");

                    obj.Imgurl = "MOLD/" + filename[filename.Length - 1];
                    if (EditMode == PopupEditMode.New)
                    {


                        ModelBindingSource.DataSource = MEA1000Service.Insert(obj);
                    }
                    else
                    {
                        MEA1000Service.Update(obj);
                    }
                    buttonEdit1.EditValue = "MOLD/" + filename[filename.Length - 1];
                }
          
            }
            //else { obj.Imgurl = "";
            //    MEA1000Service.Update(obj);
            //}
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