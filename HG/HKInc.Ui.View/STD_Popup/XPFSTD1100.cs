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
    public partial class XPFSTD1100 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD1100> Std1100Service;
        public XPFSTD1100(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정

            lupTopCategory.EditValueChanged += lupTopCategory_EditValueChanged;
            lupMiddleCategory.EditValueChanged += lupMiddleCategory_EditValueChanged;

            lupSpec1.EditValueChanged += textMaterial_EditValueChanged;
            //lupSpec2.EditValueChanged += textMaterial_EditValueChanged;

            buttonEdit1.ButtonPressed += new HKInc.Service.Handler.EventHandler.FileButtonEditClickHandler(buttonEdit1).ButtonClick;
            buttonEdit1.EditValueChanged += BtnFile_EditValueChanged;
        }
        
        protected override void InitBindingSource()
        {
            base.InitBindingSource();

            // Service설정 부모에게서 넘어온다
            Std1100Service = (IService<TN_STD1100>)PopupParam.GetValue(PopupParameter.Service);
        }
        
        protected override void InitCombo()
        {
            base.InitCombo();
            lupTopCategory.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
            //lupBottomCategory.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE));
            lup_ltype.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.lctype));
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", Std1100Service.GetChildList<TN_STD1400>(p=>p.UseFlag=="Y").OrderBy(o=>o.CustomerName).ToList());
            lupmc.SetDefault(true, "MachineCode", "MachineName", Std1100Service.GetChildList<VI_MEA1000_NOT_FILE_LIST>(p => p.UseYn == "Y").ToList());
            lupsrccode.SetDefault(true, "ItemCode", "ItemNm1", Std1100Service.GetChildList<TN_STD1100>(p => p.UseYn == "Y"&&(p.TopCategory=="P03"|| p.TopCategory == "P02")).OrderBy(o => o.ItemNm1).ToList());
            lupMoldCode.SetDefault(true, "MoldCode", "MoldName", Std1100Service.GetChildList<TN_MOLD001>(p => p.UseYN == "Y").ToList().OrderBy(o => o.MoldCode).ToList());
            lupKnifeCode.SetDefault(true, "KnifeCode", "KnifeName", Std1100Service.GetChildList<TN_KNIFE001>(p => p.UseYN == "Y").ToList().OrderBy(o => o.KnifeCode).ToList());
            gridLookUpEditEx4.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Unit));
            lupSpec1.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.ITEM_MATERIAL));
            //lupSpec2.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.ITEM_COLOR));

            textEdit1.Properties.Mask.EditMask = "n2";
            textEdit7.Properties.Mask.EditMask = "n2";
            textEdit8.Properties.Mask.EditMask = "n2";
            textSrcQty.Properties.Mask.EditMask = "n3";
            textStdPackQty.Properties.Mask.EditMask = "n0";

            textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textEdit7.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textEdit8.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textSrcQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textStdPackQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            textEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            textEdit7.Properties.Mask.UseMaskAsDisplayFormat = true;
            textEdit8.Properties.Mask.UseMaskAsDisplayFormat = true;
            textSrcQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            textStdPackQty.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        protected override void DataLoad()
        {
            tx_itemcode.ReadOnly = true;
            textItemNm1.ReadOnly = true;
            textItemNm.ReadOnly = true;
            textSrcQty.ReadOnly = true;

            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var ItemCode = DbRequesHandler.GetRequestNumberNew("ITEM");
                ModelBindingSource.Add(new TN_STD1100 {
                    UseYn = "Y"
                    ,Temp3 = "N"
                    ,ItemCode = ItemCode
                    ,ItemNm1 = ItemCode
                });
                ModelBindingSource.DataSource = Std1100Service.Insert((TN_STD1100)ModelBindingSource.Current);
            }
            else
            {  // Update
                TN_STD1100 obj = (TN_STD1100)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
                if (obj.TopCategory == MasterCodeSTR.TopCategory_Production) ProductControlInit();
                else if (obj.TopCategory == MasterCodeSTR.TopCategory_Material) MaterialControlInit();
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;

            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_STD1100 obj = ModelBindingSource.Current as TN_STD1100;

            var TopCategory = lupTopCategory.EditValue.GetNullToEmpty();
            if (TopCategory == MasterCodeSTR.TopCategory_Production)
            {
                var check = textSrcQty.EditValue.GetDecimalNullToZero();
                if(check == 0)
                {
                    MessageBoxHandler.Show("소재사용량을 입력해 주시기 바랍니다.", "경고");
                    return;
                }
                if (lupSpec1.EditValue.GetNullToEmpty().IsNullOrEmpty())
                {
                    MessageBoxHandler.Show("원재료를 입력해 주시기 바랍니다.", "경고");
                    return;
                }
            }
            else if (TopCategory == MasterCodeSTR.TopCategory_Material)
            {
                if (lupSpec1.EditValue.GetNullToEmpty().IsNullOrEmpty())
                {
                    MessageBoxHandler.Show("원재료를 입력해 주시기 바랍니다.", "경고");
                    return;
                }
                else if(textColor.EditValue.GetNullToEmpty().IsNullOrEmpty())
                {
                    MessageBoxHandler.Show("색상을 입력해 주시기 바랍니다.", "경고");
                    return;
                }
                else if (textMaterialSpec.EditValue.GetNullToEmpty().IsNullOrEmpty())
                {
                    MessageBoxHandler.Show("재료규격을 입력해 주시기 바랍니다.", "경고");
                    return;
                }
            }

            if (textItemNm.EditValue.GetNullToEmpty() == "품명 없음")
            {
                MessageBoxHandler.Show("품명을 입력해 주시기 바랍니다.", "경고");
                return;
            }

            if (buttonEdit1.EditValue.GetNullToEmpty() == "") obj.Temp6 = "";

            if (obj.Temp6.GetNullToEmpty() != "")
            {
                string[] filename = obj.Temp6.ToString().Split('\\');
                if (filename.Length != 1)
                {
                    FileHandler.UploadFile1(obj.Temp6, GlobalVariable.FTP_SERVER + "ITEM/");

                    obj.Temp6 = "ITEM/" + filename[filename.Length - 1];
                    buttonEdit1.EditValue = "ITEM/" + filename[filename.Length - 1];
                }
            }

            SetSaveMessageCheck = true;
            if (EditMode == PopupEditMode.New)
            {
                ModelBindingSource.DataSource = Std1100Service.Insert(obj);
            }
            else
            {
                Std1100Service.Update(obj);
            }
            Std1100Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.RowId);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }

        private void lupTopCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTopCategory.EditValue.GetNullToEmpty() == "")
            {
                return;
            }
            ////obj.ItemNm = null;
            var TopCategory = lupTopCategory.EditValue.ToString();
            if (TopCategory == MasterCodeSTR.TopCategory_Production)
            {
                layoutControlItem7.AppearanceItemCaption.ForeColor = Color.Red; //원재료
                layoutControlItem8.AppearanceItemCaption.ForeColor = Color.Black; //색상
                layoutControlItem9.AppearanceItemCaption.ForeColor = Color.Black; //규격
                ////layoutControlItem27.AppearanceItemCaption.ForeColor = Color.Red; //실리콘농도
                layoutControlItem18.AppearanceItemCaption.ForeColor = Color.Red; //소재사용량
                textItemNm.ReadOnly = false;
                textSrcQty.ReadOnly = false;
            }
            else if (TopCategory == MasterCodeSTR.TopCategory_Material)
            {
                layoutControlItem7.AppearanceItemCaption.ForeColor = Color.Red; //원재료
                layoutControlItem8.AppearanceItemCaption.ForeColor = Color.Red; //색상
                layoutControlItem9.AppearanceItemCaption.ForeColor = Color.Red; //규격
                //layoutControlItem27.AppearanceItemCaption.ForeColor = Color.Red; //실리콘농도
                layoutControlItem18.AppearanceItemCaption.ForeColor = Color.Black; //소재사용량
                textItemNm.ReadOnly = true;
                textSrcQty.ReadOnly = true;
            }
            lupMiddleCategory.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, lupTopCategory.EditValue.GetNullToEmpty(), 2));
            //textItemNm.Focus();
        }

        private void lupMiddleCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMiddleCategory.EditValue.GetNullToEmpty() == "") return;
            lupBottomCategory.SetDefault(false, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, lupMiddleCategory.EditValue.ToString(), 3));
        }

        private void textMaterial_EditValueChanged(object sender, EventArgs e)
        {
            TN_STD1100 obj = ModelBindingSource.Current as TN_STD1100;
            if (obj.TopCategory == MasterCodeSTR.TopCategory_Material)
            {
                var Material = lupSpec1.Text.GetNullToEmpty();
                var MaterialColor = textColor.EditValue.GetNullToEmpty();
                var MaterialSpec = textMaterialSpec.EditValue.GetNullToEmpty();
                var Silicon = textSilicon.EditValue.GetNullToEmpty();                
                if (Silicon.IsNullOrEmpty())
                    obj.ItemNm = string.Format("{0}_{1}_{2}", Material, MaterialColor, MaterialSpec);
                else
                    obj.ItemNm = string.Format("{0}_{1}_{2}_{3}", Material, MaterialColor, MaterialSpec, Silicon);
            }
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

        private void MaterialControlInit()
        {
            layoutControlItem7.AppearanceItemCaption.ForeColor = Color.Red; //원재료
            layoutControlItem8.AppearanceItemCaption.ForeColor = Color.Red; //색상
            layoutControlItem9.AppearanceItemCaption.ForeColor = Color.Red; //규격
                                                                            //layoutControlItem27.AppearanceItemCaption.ForeColor = Color.Red; //실리콘농도
            layoutControlItem18.AppearanceItemCaption.ForeColor = Color.Black; //소재사용량
            textItemNm.ReadOnly = true;
            textSrcQty.ReadOnly = true;
        }

        private void ProductControlInit()
        {
            layoutControlItem7.AppearanceItemCaption.ForeColor = Color.Black; //원재료
            layoutControlItem8.AppearanceItemCaption.ForeColor = Color.Black; //색상
            layoutControlItem9.AppearanceItemCaption.ForeColor = Color.Black; //규격
                                                                              ////layoutControlItem27.AppearanceItemCaption.ForeColor = Color.Red; //실리콘농도
            layoutControlItem18.AppearanceItemCaption.ForeColor = Color.Red; //소재사용량
            textItemNm.ReadOnly = false;
            textSrcQty.ReadOnly = false;
        }

    }
}