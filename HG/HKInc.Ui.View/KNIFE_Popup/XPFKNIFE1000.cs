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

namespace HKInc.Ui.View.KNIFE_Popup
{
    public partial class XPFKNIFE1000 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_KNIFE001> ModelService;
        public XPFKNIFE1000(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정
            this.Text = "칼 정보관리";
        }

        protected override void InitControls()
        {
            base.InitControls();
            buttonEdit1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit1).ButtonClick;
            buttonEdit1.EditValueChanged += BtnFile_EditValueChanged;
            lupstpostion1.EditValueChanged += Lupstpostion1_EditValueChanged;
        }

        private void Lupstpostion1_EditValueChanged(object sender, EventArgs e)
        {
            var value = lupstpostion1.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty())
            {
                lupstpostion2.EditValue = null;
                lupstpostion2.SetDefault(true, "PosionCode", "PosionName", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == "NOT_DATA").ToList());
            }
            else
            {
                lupstpostion2.SetDefault(true, "PosionCode", "PosionName", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == value).ToList());
            }
        }

        protected override void InitCombo()
        {
            lupmakecust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lcitemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList()); ;
            lupMstMc.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList()); ;
            //lupstpostion1.SetDefault(true, "Mcode", "Codename",  DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 1));
            //lupstpostion2.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 2));
            //lupstpostion3.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 3));
            lupstpostion1.SetDefault(true, "WhCode", "WhName", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList());
            //lupstpostion2.SetDefault(true, "PosionCode", "PosionName", MEA1000Service.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList());
            lupcheckCycle.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.CHECKCYCLE));
            lupClass.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.MOLDCLASS));
        }
        private void BtnFile_EditValueChanged(object sender, EventArgs e)
        {
            ButtonEdit obj = sender as ButtonEdit;
            string fname = "";
            try
            {
                fname = obj.EditValue.ToString();
            }
            catch
            {
                fname = "";
            }
            
            byte[] fileData = obj.Tag as byte[];
            pictureEdit1.EditValue = null;
            if (fileData != null)
            {
                pictureEdit1.EditValue = fileData;             
            }
            else
            {
                if (obj.EditValue.IsNullOrEmpty()) return;
                byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.EditValue.ToString());
                pictureEdit1.EditValue = img;
            }
        }
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            ModelService = (IService<TN_KNIFE001>)PopupParam.GetValue(PopupParameter.Service);
        }
      
        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var KnifeCode = DbRequesHandler.GetRequestNumberNew("KNIFE");
                ModelBindingSource.Add(new TN_KNIFE001
                {
                    KnifeMcode = KnifeCode
                    ,KnifeCode = KnifeCode
                    ,UseYN ="Y"
                    ,StdShotcnt = 0
                    ,RealShotcnt = 0
                    ,BaseShotcnt = 0
                    ,SumShotcnt = 0
                    ,CheckPoint = 0
                    ,XCase = 0
                });
                ModelBindingSource.DataSource = ModelService.Insert((TN_KNIFE001)ModelBindingSource.Current);
            }
            else
            {  // Update
                TN_KNIFE001 obj = (TN_KNIFE001)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
            textEdit1.ReadOnly = true;
            textEdit2.ReadOnly = true;
        }

        protected override void DataSave()
        {
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_KNIFE001 obj = ModelBindingSource.Current as TN_KNIFE001;
            if (buttonEdit1.EditValue.GetNullToEmpty() == "") obj.Imgurl = "";
            if (obj.Imgurl.GetNullToEmpty() != "")
            {
                string[] filename = obj.Imgurl.ToString().Split('\\');
                if (filename.Length != 1)
                {
                    FileHandler.UploadFile1(obj.Imgurl, GlobalVariable.FTP_SERVER + "KNIFE/");

                    obj.Imgurl = "KNIFE/" + filename[filename.Length - 1];
                    if (EditMode == PopupEditMode.New)
                    {


                        ModelBindingSource.DataSource = ModelService.Insert(obj);
                    }
                    else
                    {
                        ModelService.Update(obj);
                    }
                    buttonEdit1.EditValue = "KNIFE/" + filename[filename.Length - 1];
                }
          
            }
            ModelService.Save();
            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }
    }
}