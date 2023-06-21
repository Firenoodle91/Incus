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
using HKInc.Service.Handler.EventHandler;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;

namespace HKInc.Ui.View.View.STD_POPUP
{
    /// <summary>
    /// 품목기준정보 팝업
    /// </summary>
    public partial class XPFSTD1100 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD1100> Std1100Service;

        public XPFSTD1100(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;

            ModelBindingSource = bindingSource1; // BindingSource설정

            lup_TopCategory.EditValueChanged += Lup_TopCategory_EditValueChanged;
            lup_MiddleCategory.Popup += Lup_MiddleCategory_Popup;
            lup_BottomCategory.Popup += Lup_BottomCategory_Popup;

            lup_SrcCode.EditValueChanged += Lup_SrcCode_EditValueChanged;
            lup_OutBox.EditValueChanged += LupOutBox_EditValueChanged;
            lup_PackCode.EditValueChanged += LupPackCode_EditValueChanged;
            lup_ToolCode.EditValueChanged += Lup_ToolCode_EditValueChanged;
            lup_ToolCode2.EditValueChanged += Lup_ToolCode2_EditValueChanged;

            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;

            btn_ProdFileName.KeyDown += Btn_ProdFileName_KeyDown;
        }

        private void Btn_ProdFileName_KeyDown(object sender, KeyEventArgs e)
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

                        btn_ProdFileName.Tag = fileData;
                        btn_ProdFileName.EditValue = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        ImageConverter converter = new ImageConverter();
                        btn_ProdFileName.Tag = GetImage;//(byte[])converter.ConvertTo(GetImage, typeof(byte[]));
                        btn_ProdFileName.EditValue = "Clipboard_Image"; //this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }

        protected override void AddControlList()
        {
            ControlEnableList.Add("ItemCode", tx_ItemCode);
            ControlEnableList.Add("ItemName", tx_ItemName);
            ControlEnableList.Add("ItemNameENG", tx_ItemNameENG);
            ControlEnableList.Add("ItemNameCHN", tx_ItemNameCHN);
            ControlEnableList.Add("CustomerItemCode", tx_CustomerItemCode);
            ControlEnableList.Add("CustomerItemName", tx_CustomerItemName);
            ControlEnableList.Add("MainCustomerCode", lup_MainCustomerCode);
            ControlEnableList.Add("TopCategory", lup_TopCategory);
            ControlEnableList.Add("MiddleCategory", lup_MiddleCategory);
            ControlEnableList.Add("BottomCategory", lup_BottomCategory);
            ControlEnableList.Add("CarType", tx_CarType);
            ControlEnableList.Add("Unit", lup_Unit);
            ControlEnableList.Add("OutUnit", lup_OutUnit);
            ControlEnableList.Add("Weight", spin_Weight);
            ControlEnableList.Add("Spec1", tx_Spec1);
            ControlEnableList.Add("Spec2", tx_Spec2);
            ControlEnableList.Add("Spec3", tx_Spec3);
            ControlEnableList.Add("Spec4", tx_Spec4);
            ControlEnableList.Add("SafeQty", spin_SafeQty);
            ControlEnableList.Add("ProdQty", spin_ProdQty);
            ControlEnableList.Add("Cost", spin_Cost);
            ControlEnableList.Add("ProcTeamCode", lup_ProcTeamCode);
            ControlEnableList.Add("StockPosition", lup_StockPosition);
            ControlEnableList.Add("SrcCode", lup_SrcCode);
            ControlEnableList.Add("SrcWeight", spin_SrcWeight);
            ControlEnableList.Add("GrindingFlag", chk_GrindingFlag);
            ControlEnableList.Add("SurfaceList", lup_SurfaceList);
            ControlEnableList.Add("SelfInspFlag", chk_SelfInspFlag);
            ControlEnableList.Add("StockInspFlag", chk_StockInspFlag);
            ControlEnableList.Add("ShipmentInspFlag", chk_ShipmentInspFlag);
            ControlEnableList.Add("ProcInspFlag", chk_ProcInspFlag);
            ControlEnableList.Add("AQL", lup_AQL);
            ControlEnableList.Add("SetTime", spin_SetTime);
            ControlEnableList.Add("ProcTime", spin_ProcTime);
            ControlEnableList.Add("PackQty", spin_PackQty);
            ControlEnableList.Add("Memo", memoEdit1);
            ControlEnableList.Add("ProdFileName", btn_ProdFileName);
            ControlEnableList.Add("OutBox", lup_OutBox);
            ControlEnableList.Add("PackCode", lup_PackCode);
            ControlEnableList.Add("Heat", tx_HeatTemperature);
            ControlEnableList.Add("Rpm", tx_HeatRpm);
            ControlEnableList.Add("ToolCode", lup_ToolCode);
            ControlEnableList.Add("ToolLifeQty", spin_ToolLifeQty);
            ControlEnableList.Add("ToolCode2", lup_ToolCode2);
            ControlEnableList.Add("ToolLifeQty2", spin_ToolLifeQty2);
            ControlEnableList.Add("ProcessPackQty", spin_ProcessPackQty);

            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_STD1100>(new TN_STD1100(), ControlEnableList, this.Controls);
        }

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            Std1100Service = (IService<TN_STD1100>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitCombo()
        {
            tx_SrcItemCode.ReadOnly = true;
            tx_ToolCode.ReadOnly = true;
            tx_ToolCode2.ReadOnly = true;
            tx_PackPlasticItemCode.ReadOnly = true;
            tx_OutBoxItemCode.ReadOnly = true;

            spin_Weight.Properties.Mask.MaskType = MaskType.Numeric;
            spin_Weight.Properties.Mask.EditMask = "n5";
            spin_Weight.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_Weight.Properties.Buttons[0].Visible = false;

            spin_SafeQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SafeQty.Properties.Mask.EditMask = "n0";
            spin_SafeQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SafeQty.Properties.Buttons[0].Visible = false;

            spin_ProdQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ProdQty.Properties.Mask.EditMask = "n2";
            spin_ProdQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ProdQty.Properties.Buttons[0].Visible = false;

            spin_Cost.Properties.Mask.MaskType = MaskType.Numeric;
            spin_Cost.Properties.Mask.EditMask = "n2";
            spin_Cost.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_Cost.Properties.Buttons[0].Visible = false;

            spin_SrcWeight.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SrcWeight.Properties.Mask.EditMask = "n5";
            spin_SrcWeight.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SrcWeight.Properties.Buttons[0].Visible = false;

            spin_SetTime.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SetTime.Properties.Mask.EditMask = "n0";
            spin_SetTime.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SetTime.Properties.Buttons[0].Visible = false;

            spin_ProcTime.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ProcTime.Properties.Mask.EditMask = "n0";
            spin_ProcTime.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ProcTime.Properties.Buttons[0].Visible = false;

            spin_PackQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_PackQty.Properties.Mask.EditMask = "n2";
            spin_PackQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_PackQty.Properties.Buttons[0].Visible = false;

            spin_ToolLifeQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ToolLifeQty.Properties.Mask.EditMask = "n0";
            spin_ToolLifeQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ToolLifeQty.Properties.Buttons[0].Visible = false;

            spin_ToolLifeQty2.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ToolLifeQty2.Properties.Mask.EditMask = "n0";
            spin_ToolLifeQty2.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ToolLifeQty2.Properties.Buttons[0].Visible = false;

            spin_ProcessPackQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ProcessPackQty.Properties.Mask.EditMask = "n0";
            spin_ProcessPackQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ProcessPackQty.Properties.Buttons[0].Visible = false;

            lup_MainCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), Std1100Service.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());

            lup_TopCategory.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1).Where(p => p.CodeVal != MasterCodeSTR.TopCategory_SPARE && p.CodeVal != MasterCodeSTR.TopCategory_TOOL).ToList());
            lup_MiddleCategory.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2));
            lup_BottomCategory.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3));

            //lup_CarType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.CarType));
            lup_Unit.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit));
            lup_OutUnit.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit));
            lup_ProcTeamCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.ProductTeamCode));
            lup_StockPosition.SetDefault(true, "PositionCode", "PositionName", Std1100Service.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList());
            lup_StockPosition.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "TN_WMS1000." + DataConvert.GetCultureDataFieldName("WhName"),
                Caption = LabelConvert.GetLabelText(DataConvert.GetCultureDataFieldName("WhName")),
                Visible = true
            });
            lup_StockPosition.Popup += Lup_StockPosition_Popup;

            lup_SrcCode.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), Std1100Service.GetList(p => p.UseFlag == "Y" && p.TopCategory == MasterCodeSTR.TopCategory_MAT).OrderBy(p=>p.ItemCode).ToList());
            //lup_MainMachineCode.SetDefault(true, "MachineCode", DataConvert.GetCultureDataFieldName("MachineName"), Std1100Service.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            lup_AQL.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.AQL));
            lup_SurfaceList.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.SurfaceList));
            lup_PackCode.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), Std1100Service.GetList(p => p.UseFlag == "Y" && p.MiddleCategory == MasterCodeSTR.MiddleCategory_PackPlastic).ToList());
            lup_OutBox.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), Std1100Service.GetList(p => p.UseFlag == "Y" && p.MiddleCategory == MasterCodeSTR.MiddleCategory_OutBox).ToList());
            lup_ToolCode.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), Std1100Service.GetList(p => p.UseFlag == "Y" && p.TopCategory == MasterCodeSTR.TopCategory_TOOL).ToList());
            lup_ToolCode2.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), Std1100Service.GetList(p => p.UseFlag == "Y" && p.TopCategory == MasterCodeSTR.TopCategory_TOOL).ToList());

            btn_ProdFileName.ButtonPressed += new HKInc.Service.Handler.EventHandler.FtpFileButtonEditClickHandler(UserRight.HasSelect
                                                                                                                    , UserRight.HasEdit
                                                                                                                    , btn_ProdFileName
                                                                                                                    , true
                                                                                                                    ,MasterCodeSTR.FtpFolder_ProdImage).ButtonClick;
            btn_ProdFileName.EditValueChanged += Btn_ProdFileName_EditValueChanged;

            SetControlEnableToTopCategory();
        }

        protected override void DataLoad()
        {
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                var newObj = new TN_STD1100()
                {
                    UseFlag = "Y",
                    StockInspFlag = "Y",
                    GrindingFlag = "Y",
                    SelfInspFlag = "Y",
                    ProcInspFlag = "Y",
                    ShipmentInspFlag = "Y"
                    //FileFolderKeyName = DbRequestHandler.GetSeqDay("PROD_IMG")
                };

                ModelBindingSource.Add(newObj);
                ModelBindingSource.MoveLast();
            }
            else
            {
                // Update
                tx_ItemCode.ReadOnly = true;
                var obj = (TN_STD1100)PopupParam.GetValue(PopupParameter.KeyValue);
                //btn_ProdFileName.Tag = obj.ProdFileUrl;
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            SetSaveMessageCheck = false;
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting

            var obj = ModelBindingSource.Current as TN_STD1100;

            if (EditMode == PopupEditMode.New)
            {
                //품명과 고객사품명 둘다 없을 경우 예외
                if(obj.ItemName.IsNullOrEmpty() && obj.CustomerItemName.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_49), LabelConvert.GetLabelText("ItemName"), LabelConvert.GetLabelText("CustomerItemName")));
                    return;
                }

                //품명이 없고 고객사품명이 있을경우 품명을 고객사 품명으로 
                if (obj.ItemName.IsNullOrEmpty() && !obj.CustomerItemName.IsNullOrEmpty())
                    obj.ItemName = obj.CustomerItemName;

                //품번(도번)이 없고, 고객사품번이 있을경우
                if (obj.ItemCode.IsNullOrEmpty() && !obj.CustomerItemCode.IsNullOrEmpty())
                    obj.ItemCode = obj.CustomerItemCode;

                //대분류가 없을 경우
                if (obj.TopCategory.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("TopCategory"), LabelConvert.GetLabelText("CustomerItemName")));
                    return;
                }

                //품번(도번)이 없고, 고객사품번도 없을경우
                if (obj.ItemCode.IsNullOrEmpty() && obj.CustomerItemCode.IsNullOrEmpty())
                    obj.ItemCode = DbRequestHandler.GetSeqStandard("ITEM");

                //등록한 품번이 데이터가 있을경우
                if (Std1100Service.GetList(p => p.ItemCode == obj.ItemCode).FirstOrDefault() != null)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), string.Format("{0}:{1}", LabelConvert.GetLabelText("ItemCode"), obj.ItemCode)));
                    return;
                }

                //파일 CHECK
                if (btn_ProdFileName.EditValue.IsNullOrEmpty()) obj.ProdFileUrl = null;

                if (!obj.ProdFileUrl.IsNullOrEmpty())
                {
                    string[] filename = obj.ProdFileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ItemCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;

                        FileHandler.UploadFTP(obj.ProdFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                        btn_ProdFileName.EditValue = realFileName;
                        btn_ProdFileName.Tag = null;
                        obj.ProdFileUrl = ftpFileUrl;
                    }
                    else if (obj.ProdFileUrl == "Clipboard_Image")
                    {
                        var realFileName = obj.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;
                        var localImage = btn_ProdFileName.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                        btn_ProdFileName.EditValue = realFileName;
                        btn_ProdFileName.Tag = null;
                        obj.ProdFileUrl = ftpFileUrl;
                    }
                }

                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                ModelBindingSource.DataSource = Std1100Service.Insert(obj);
            }
            else
            {
                //파일 CHECK
                if (btn_ProdFileName.EditValue.IsNullOrEmpty()) obj.ProdFileUrl = null;

                if (!obj.ProdFileUrl.IsNullOrEmpty())
                {
                    string[] filename = obj.ProdFileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = obj.ItemCode + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;

                        FileHandler.UploadFTP(obj.ProdFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                        btn_ProdFileName.EditValue = realFileName;
                        btn_ProdFileName.Tag = null;
                        obj.ProdFileUrl = ftpFileUrl;
                    }
                    else if (obj.ProdFileUrl == "Clipboard_Image")
                    {
                        var realFileName = obj.ItemCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_ProdImage + "/" + realFileName;
                        var localImage = btn_ProdFileName.Tag as Image;
                        FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_ProdImage + "/");

                        btn_ProdFileName.EditValue = realFileName;
                        btn_ProdFileName.Tag = null;
                        obj.ProdFileUrl = ftpFileUrl;
                    }
                }
                ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
                Std1100Service.Update(obj);
            }

            Std1100Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.ItemCode);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
            tx_ItemCode.ReadOnly = true;
            SetSaveMessageCheck = true;
        }

        private void Lup_MiddleCategory_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lup_TopCategory.EditValue.GetNullToEmpty();

            if (value.IsNullOrEmpty())
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            }
            else
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[CodeTop] = '" + value + "'";
            }
        }

        private void Lup_BottomCategory_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lup_MiddleCategory.EditValue.GetNullToEmpty();

            if (value.IsNullOrEmpty())
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            }
            else
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[CodeMid] = '" + value + "'";
            }
        }

        private void Lup_TopCategory_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;


            SetControlEnableToTopCategory();
            //var value = lookup.EditValue.GetNullToEmpty();

            //if (value.IsNullOrEmpty())
            //{
            //    lup_SrcCode.ReadOnly = true;
            //    spin_SrcWeight.ReadOnly = true;
            //    lup_OutBox.ReadOnly = true;
            //    lup_PackCode.ReadOnly = true;
            //    lup_ToolCode.ReadOnly = true;
            //    spin_ToolLifeQty.ReadOnly = true;

            //    spin_PackQty.ReadOnly = true;
            //    spin_HeatTemperature.ReadOnly = true;
            //    spin_HeatRpm.ReadOnly = true;
            //}
            //else if (value == MasterCodeSTR.TopCategory_WAN || value == MasterCodeSTR.TopCategory_BAN)
            //{
            //    lup_SrcCode.ReadOnly = false;
            //    spin_SrcWeight.ReadOnly = false;
            //    lup_OutBox.ReadOnly = false;
            //    lup_PackCode.ReadOnly = false;
            //    lup_ToolCode.ReadOnly = false;
            //    spin_ToolLifeQty.ReadOnly = false;

            //    spin_PackQty.ReadOnly = false;
            //    spin_HeatTemperature.ReadOnly = false;
            //    spin_HeatRpm.ReadOnly = false;
            //}
            //else
            //{
            //    lup_SrcCode.ReadOnly = true;
            //    spin_SrcWeight.ReadOnly = true;
            //    lup_OutBox.ReadOnly = true;
            //    lup_PackCode.ReadOnly = true;
            //    lup_ToolCode.ReadOnly = true;
            //    spin_ToolLifeQty.ReadOnly = true;

            //    spin_PackQty.ReadOnly = true;
            //    spin_HeatTemperature.ReadOnly = true;
            //    spin_HeatRpm.ReadOnly = true;
            //}
        }

        private void Lup_SrcCode_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lookup.EditValue.GetNullToEmpty();
            if (value.IsNullOrEmpty())
            {
                tx_SrcItemCode.EditValue = null;
                spin_SrcWeight.EditValue = null;
            }
            else
            {
                tx_SrcItemCode.EditValue = lookup.EditValue;
                var SrcItemObj = lookup.GetSelectedDataRow() as TN_STD1100;
                spin_SrcWeight.EditValue = SrcItemObj.Weight.GetDecimalNullToZero();
            }
        }

        private void LupOutBox_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var obj = lookup.GetSelectedDataRow() as TN_STD1100;

            if (obj == null)
            {
                tx_OutBoxItemCode.EditValue = null;
                pic_OutBoxImage.EditValue = null;
            }
            else
            {
                tx_OutBoxItemCode.EditValue = lookup.EditValue;
                if (!obj.ProdFileUrl.IsNullOrEmpty())
                {
                    pic_OutBoxImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.ProdFileUrl);
                }
            }
        }

        private void LupPackCode_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var obj = lookup.GetSelectedDataRow() as TN_STD1100;

            if (obj == null)
            {
                tx_PackPlasticItemCode.EditValue = null;
                pic_PackPlasticImage.EditValue = null;
            }
            else
            {
                tx_PackPlasticItemCode.EditValue = lookup.EditValue;
                if (!obj.ProdFileUrl.IsNullOrEmpty())
                {
                    pic_PackPlasticImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.ProdFileUrl);
                }
            }
        }

        private void Lup_ToolCode_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lookup.EditValue.GetNullToEmpty();

            if (value.IsNullOrEmpty())
                tx_ToolCode.EditValue = null;
            else
                tx_ToolCode.EditValue = lookup.EditValue;
        }

        private void Lup_ToolCode2_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var value = lookup.EditValue.GetNullToEmpty();

            if (value.IsNullOrEmpty())
                tx_ToolCode2.EditValue = null;
            else
                tx_ToolCode2.EditValue = lookup.EditValue;
        }

        private void Btn_ProdFileName_EditValueChanged(object sender, EventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_STD1100;
            if (obj == null) return;
            var button = sender as ButtonEdit;

            var fileName = button.EditValue.GetNullToEmpty();
            if (fileName.IsNullOrEmpty())
            {
                pic_ProdImage.EditValue = null;
            }
            else
            {
                byte[] localfileData = button.Tag as byte[];
                if (localfileData != null)
                {
                    pic_ProdImage.EditValue = localfileData;
                    obj.ProdFileUrl = fileName;
                }
                else
                {
                    var localImage = button.Tag as Image;
                    if (localImage != null)
                    {
                        pic_ProdImage.EditValue = localImage;
                        obj.ProdFileUrl = fileName;
                    }
                    else
                    {
                        var fileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + obj.ProdFileUrl);
                        pic_ProdImage.EditValue = fileData;
                    }
                }
            }
        }

        /// <summary>
        /// 대분류에 따른 컨트롤 활성화
        /// </summary>
        private void SetControlEnableToTopCategory()
        {
            var topCategory = lup_TopCategory.EditValue.GetNullToEmpty();
            if (topCategory.IsNullOrEmpty())
            {
                //tx_SrcItemCode.ReadOnly = true;
                //tx_SrcItemCode.EditValue = null;

                lup_OutUnit.ReadOnly = true;
                lup_OutUnit.EditValue = null;

                lup_SrcCode.ReadOnly = true;
                lup_SrcCode.EditValue = null;

                spin_SrcWeight.ReadOnly = true;
                spin_SrcWeight.EditValue = null;

                tx_ToolCode.ReadOnly = true;
                tx_ToolCode.EditValue = null;

                tx_ToolCode2.ReadOnly = true;
                tx_ToolCode2.EditValue = null;

                lup_ToolCode.ReadOnly = true;
                lup_ToolCode.EditValue = null;

                lup_ToolCode2.ReadOnly = true;
                lup_ToolCode2.EditValue = null;

                spin_ToolLifeQty.ReadOnly = true;
                spin_ToolLifeQty.EditValue = null;

                spin_ToolLifeQty2.ReadOnly = true;
                spin_ToolLifeQty2.EditValue = null;

                chk_GrindingFlag.ReadOnly = true;
                chk_GrindingFlag.EditValue = "N";

                chk_SelfInspFlag.ReadOnly = true;
                chk_SelfInspFlag.EditValue = "N";

                chk_ProcInspFlag.ReadOnly = true;
                chk_ProcInspFlag.EditValue = "N";

                chk_StockInspFlag.ReadOnly = true;
                chk_StockInspFlag.EditValue = "N";

                chk_ShipmentInspFlag.ReadOnly = true;
                chk_ShipmentInspFlag.EditValue = "N";

                lup_SurfaceList.ReadOnly = true;
                lup_SurfaceList.EditValue = null;

                lup_AQL.ReadOnly = true;
                lup_AQL.EditValue = null;

                spin_SetTime.ReadOnly = true;
                spin_SetTime.EditValue = null;

                spin_ProcTime.ReadOnly = true;
                spin_ProcTime.EditValue = null;

                spin_PackQty.ReadOnly = true;
                spin_PackQty.EditValue = null;

                tx_HeatTemperature.ReadOnly = true;
                tx_HeatTemperature.EditValue = null;

                tx_HeatRpm.ReadOnly = true;
                tx_HeatRpm.EditValue = null;

                //tx_PackPlasticItemCode.ReadOnly = true;
                //tx_PackPlasticItemCode.EditValue = null;

                lup_PackCode.ReadOnly = true;
                lup_PackCode.EditValue = null;

                //tx_OutBoxItemCode.ReadOnly = true;
                //tx_OutBoxItemCode.EditValue = null;

                lup_OutBox.ReadOnly = true;
                lup_OutBox.EditValue = null;

                lup_StockPosition.EditValue = null;

                spin_ProcessPackQty.ReadOnly = true;
                spin_ProcessPackQty.EditValue = null;
            }
            else if (topCategory == MasterCodeSTR.TopCategory_WAN || topCategory == MasterCodeSTR.TopCategory_BAN)
            {
                //완제품,반제품 선택
                //tx_SrcItemCode.ReadOnly = false;
                //tx_SrcItemCode.EditValue = null;

                lup_OutUnit.ReadOnly = false;
                lup_OutUnit.EditValue = null;

                lup_SrcCode.ReadOnly = false;
                lup_SrcCode.EditValue = null;

                spin_SrcWeight.ReadOnly = false;
                spin_SrcWeight.EditValue = null;

                tx_ToolCode.ReadOnly = true;
                tx_ToolCode.EditValue = null;

                tx_ToolCode2.ReadOnly = true;
                tx_ToolCode2.EditValue = null;

                lup_ToolCode.ReadOnly = false;
                lup_ToolCode.EditValue = null;

                lup_ToolCode2.ReadOnly = false;
                lup_ToolCode2.EditValue = null;

                spin_ToolLifeQty.ReadOnly = false;
                spin_ToolLifeQty.EditValue = null;

                spin_ToolLifeQty2.ReadOnly = false;
                spin_ToolLifeQty2.EditValue = null;

                chk_GrindingFlag.ReadOnly = false;
                chk_GrindingFlag.EditValue = "Y";

                chk_SelfInspFlag.ReadOnly = false;
                chk_SelfInspFlag.EditValue = "Y";

                chk_ProcInspFlag.ReadOnly = false;
                chk_ProcInspFlag.EditValue = "Y";

                chk_StockInspFlag.ReadOnly = false;
                chk_StockInspFlag.EditValue = "Y";

                chk_ShipmentInspFlag.ReadOnly = true;
                chk_ShipmentInspFlag.EditValue = "Y";

                lup_SurfaceList.ReadOnly = false;
                lup_SurfaceList.EditValue = null;

                lup_AQL.ReadOnly = false;
                lup_AQL.EditValue = null;

                spin_SetTime.ReadOnly = false;
                spin_SetTime.EditValue = null;

                spin_ProcTime.ReadOnly = false;
                spin_ProcTime.EditValue = null;

                spin_PackQty.ReadOnly = false;
                spin_PackQty.EditValue = null;

                tx_HeatTemperature.ReadOnly = false;
                tx_HeatTemperature.EditValue = null;

                tx_HeatRpm.ReadOnly = false;
                tx_HeatRpm.EditValue = null;

                //tx_PackPlasticItemCode.ReadOnly = false;
                //tx_PackPlasticItemCode.EditValue = null;

                lup_PackCode.ReadOnly = false;
                lup_PackCode.EditValue = null;

                //tx_OutBoxItemCode.ReadOnly = false;
                //tx_OutBoxItemCode.EditValue = null;

                lup_OutBox.ReadOnly = false;
                lup_OutBox.EditValue = null;

                if (topCategory == MasterCodeSTR.TopCategory_WAN) 
                    lup_StockPosition.EditValue = MasterCodeSTR.WAN_PositionCode_DefaultCode;

                spin_ProcessPackQty.ReadOnly = false;
                spin_ProcessPackQty.EditValue = null;
            }
            else if (topCategory == MasterCodeSTR.TopCategory_MAT || topCategory == MasterCodeSTR.TopCategory_BU)
            {
                //원자재, 소모품 선택
                //tx_SrcItemCode.ReadOnly = true;
                //tx_SrcItemCode.EditValue = null;

                lup_OutUnit.ReadOnly = true;
                lup_OutUnit.EditValue = null;

                lup_SrcCode.ReadOnly = true;
                lup_SrcCode.EditValue = null;

                spin_SrcWeight.ReadOnly = true;
                spin_SrcWeight.EditValue = null;

                tx_ToolCode.ReadOnly = true;
                tx_ToolCode.EditValue = null;

                tx_ToolCode.ReadOnly = true;
                tx_ToolCode.EditValue = null;

                lup_ToolCode2.ReadOnly = true;
                lup_ToolCode2.EditValue = null;

                lup_ToolCode2.ReadOnly = true;
                lup_ToolCode2.EditValue = null;

                spin_ToolLifeQty.ReadOnly = true;
                spin_ToolLifeQty.EditValue = null;

                spin_ToolLifeQty2.ReadOnly = true;
                spin_ToolLifeQty2.EditValue = null;

                chk_GrindingFlag.ReadOnly = true;
                chk_GrindingFlag.EditValue = "N";

                chk_SelfInspFlag.ReadOnly = true;
                chk_SelfInspFlag.EditValue = "N";

                chk_ProcInspFlag.ReadOnly = true;
                chk_ProcInspFlag.EditValue = "N";

                chk_StockInspFlag.ReadOnly = false;
                chk_StockInspFlag.EditValue = "Y";

                chk_ShipmentInspFlag.ReadOnly = true;
                chk_ShipmentInspFlag.EditValue = "N";

                lup_SurfaceList.ReadOnly = true;
                lup_SurfaceList.EditValue = null;

                lup_AQL.ReadOnly = true;
                lup_AQL.EditValue = null;

                spin_SetTime.ReadOnly = true;
                spin_SetTime.EditValue = null;

                spin_ProcTime.ReadOnly = true;
                spin_ProcTime.EditValue = null;

                spin_PackQty.ReadOnly = true;
                spin_PackQty.EditValue = null;

                tx_HeatTemperature.ReadOnly = true;
                tx_HeatTemperature.EditValue = null;

                tx_HeatRpm.ReadOnly = true;
                tx_HeatRpm.EditValue = null;

                //tx_PackPlasticItemCode.ReadOnly = true;
                //tx_PackPlasticItemCode.EditValue = null;

                lup_PackCode.ReadOnly = true;
                lup_PackCode.EditValue = null;

                //tx_OutBoxItemCode.ReadOnly = true;
                //tx_OutBoxItemCode.EditValue = null;

                lup_OutBox.ReadOnly = true;
                lup_OutBox.EditValue = null;

                if (topCategory == MasterCodeSTR.TopCategory_MAT)
                    lup_StockPosition.EditValue = MasterCodeSTR.MAT_PositionCode_DefaultCode;

                spin_ProcessPackQty.ReadOnly = true;
                spin_ProcessPackQty.EditValue = null;
            }
        }

        private void Lup_StockPosition_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            var topCategory = lup_TopCategory.EditValue.GetNullToEmpty();

            if (topCategory.IsNullOrEmpty())
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            }
            else if (topCategory == MasterCodeSTR.TopCategory_WAN)
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + MasterCodeSTR.WAN_WhCode_DefaultCode + "'";
            }
            else if (topCategory == MasterCodeSTR.TopCategory_MAT)
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + MasterCodeSTR.MAT_WhCode_DefaultCode + "'";
            }
            else
            {
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            }
        }

    }
}