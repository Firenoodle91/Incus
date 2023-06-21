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
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Helper;
using DevExpress.XtraBars;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.PUR_POPUP
{
    /// <summary>
    /// 반품 팝업
    /// </summary>
    public partial class XPFPUR1203 : Service.Base.PopupCallbackFormTemplate
    {
        private IService<TN_PUR1303> ModelService = (IService<TN_PUR1303>)ProductionFactory.GetDomainService("TN_PUR1303");

        public XPFPUR1203()
        {
            InitializeComponent();
        }

        public XPFPUR1203(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("DisposalInfo");

            GridExControl = gridEx1;
            GridExControl.ActDeleteRowClicked += GridExControl_ActDeleteRowClicked;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(UserRight.HasEdit);
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            //GridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            //GridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("InInspectionState", LabelConvert.GetLabelText("InInspectionState"), HorzAlignment.Center, true);
            //GridExControl.MainGrid.AddColumn("InConfirmFlag", LabelConvert.GetLabelText("InConfirmFlag"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("DisposalDate", LabelConvert.GetLabelText("DisposalDate"));
            GridExControl.MainGrid.AddColumn("DisposalId", LabelConvert.GetLabelText("DisposalId"));
            GridExControl.MainGrid.AddColumn("TN_PUR1301.TN_PUR1201.InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ReturnPossibleQty", LabelConvert.GetLabelText("DisposalPossibleQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("DisposalQty", LabelConvert.GetLabelText("DisposalQty"));
            //GridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("InCost"));
            //GridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "#,###,###,###.##");
            GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            GridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            //GridExControl.MainGrid.AddColumn("PrintQty", LabelConvert.GetLabelText("PrintQty2"));
            GridExControl.MainGrid.AddColumn("TN_PUR1301.TN_PUR1201.InCustomerLotNo", LabelConvert.GetLabelText("InCustomerLotNo"));
            //GridExControl.MainGrid.AddColumn("DisposalWhCode", LabelConvert.GetLabelText("DisposalWhCode"));
            //GridExControl.MainGrid.AddColumn("DisposalWhPosition", LabelConvert.GetLabelText("DisposalnWhPosition"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DisposalDate", "DisposalId", "DisposalQty", "Memo");

            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(GridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 5;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 130;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);

            var barTextEditBarCodeStaticItem = new DevExpress.XtraBars.BarEditItem(GridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem.Id = 6;
            barTextEditBarCodeStaticItem.Name = "barTextEditBarCodeStaticItem";
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("OutLotNo") + ":";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 9;
            //barTextEditBarCodeStaticItem.EditWidth = 120;
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;

            GridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            GridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("DisposalDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DisposalId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("DisposalQty", DefaultBoolean.Default, "n2");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);

        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            ModelBindingSource.DataSource = ModelService.GetList(p => false).ToList();

            GridExControl.DataSource = ModelBindingSource;

            GridExControl.BestFitColumns();
        }

        private void GridExControl_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            var obj = ModelBindingSource.Current as TN_PUR1303;
            if (obj == null) return;

            ModelService.Delete(obj);
            ModelBindingSource.RemoveCurrent();

            GridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            ModelBindingSource.EndEdit();

            //var list = ModelBindingSource.List as List<TN_PUR1302>;
            //var qtyCheckList = list.Where(p => p.ReturnQty > p.ReturnPossibleQty).ToList();
            //if (qtyCheckList.Count > 0)
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_61));
            //    SetSaveMessageCheck = false;
            //    return;
            //}
            ModelService.Save();

            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_62));
            IsFormControlChanged = false;
            ActClose();
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                var outLotNo = textEdit.EditValue.GetNullToNull().ToUpper();
                if (outLotNo.IsNullOrEmpty())
                {
                    textEdit.EditValue = "";
                    textEdit.Focus();
                }
                else
                {
                    IService<TN_PUR1300> LocalService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");

                    var TN_PUR1301 = ModelService.GetChildList<TN_PUR1301>(p => p.OutLotNo == outLotNo).FirstOrDefault();
                    if (TN_PUR1301 == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("OutLotNo")));
                        textEdit.EditValue = "";
                        textEdit.Focus();
                    }
                    else
                    {
                        var VI_RETURN_OBJECT = LocalService.GetChildList<VI_DISPOSAL_OBJECT>(p => p.OutLotNo == outLotNo).FirstOrDefault();
                        //if (VI_RETURN_OBJECT.ReturnPossibleQty <= 0)
                        //{
                        //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("ReturnInPossibleQty")));
                        //    textEdit.EditValue = "";
                        //    textEdit.Focus();
                        //}
                        //else
                        //{
                        var newObj = (TN_PUR1303)ModelBindingSource.AddNew();
                        newObj.OutNo = TN_PUR1301.OutNo;
                        newObj.OutSeq = TN_PUR1301.OutSeq;
                        newObj.TN_PUR1301 = TN_PUR1301;
                        newObj.ItemCode = TN_PUR1301.ItemCode;
                        newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).FirstOrDefault();
                        newObj.OutLotNo = TN_PUR1301.OutLotNo;
                        newObj.InLotNo = TN_PUR1301.InLotNo;
                        newObj.DisposalQty = 0;
                        newObj.DisposalId = GlobalVariable.LoginId;
                        newObj.DisposalDate = DateTime.Today;
                        newObj.DisposalWhCode = TN_PUR1301.TN_PUR1201.InWhCode;
                        newObj.DisposalWhPosition = TN_PUR1301.TN_PUR1201.InWhPosition;
                        newObj.ReturnPossibleQty = VI_RETURN_OBJECT.ReturnPossibleQty;
                        newObj.NewRowFlag = "Y";
                        ModelService.Insert(newObj);

                        textEdit.EditValue = "";
                        textEdit.Focus();
                        e.Handled = true;
                        //}
                    }
                    GridExControl.BestFitColumns();

                    LocalService.Dispose();
                }
            }
        }
    }
}