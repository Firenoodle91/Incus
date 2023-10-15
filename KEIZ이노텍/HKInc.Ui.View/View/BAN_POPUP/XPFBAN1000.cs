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
using DevExpress.XtraGrid.Views.Base;

namespace HKInc.Ui.View.View.BAN_POPUP
{
    /// <summary>
    /// 반제품 재입고 팝업
    /// </summary>
    public partial class XPFBAN1000 : Service.Base.PopupCallbackFormTemplate
    {
        private IService<TN_BAN1102> ModelService = (IService<TN_BAN1102>)ProductionFactory.GetDomainService("TN_BAN1102");

        public XPFBAN1000()
        {
            InitializeComponent();
        }

        public XPFBAN1000(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ReturnInfo");

            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(UserRight.HasEdit);
            GridExControl.SetToolbarButtonVisible(false);
            //GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, UserRight.HasEdit);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("ReturnDate", LabelConvert.GetLabelText("ReturnDate2"));
            GridExControl.MainGrid.AddColumn("ReturnId", LabelConvert.GetLabelText("ReturnId"));
            GridExControl.MainGrid.AddColumn("TN_BAN1101.TN_BAN1001.InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("ReturnPossibleQty", LabelConvert.GetLabelText("ReturnPossibleQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("ReturnQty", LabelConvert.GetLabelText("ReturnQty"));
            GridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            GridExControl.MainGrid.AddColumn("OutLotNo", LabelConvert.GetLabelText("OutLotNo"));
            GridExControl.MainGrid.AddColumn("ReturnWhCode", LabelConvert.GetLabelText("ReturnWhCode"));
            GridExControl.MainGrid.AddColumn("ReturnWhPosition", LabelConvert.GetLabelText("ReturnWhPosition"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReturnDate", "ReturnId", "ReturnQty", "Memo", "ReturnWhCode", "ReturnWhPosition");

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
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ReturnDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("ReturnQty", DefaultBoolean.Default, "n2");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_BAN || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);

            var ReturnWhPositionEdit = GridExControl.MainGrid.Columns["ReturnWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            ReturnWhPositionEdit.Popup += ReturnWhPositionEdit_Popup;
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

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();

            var list = ModelBindingSource.List as List<TN_BAN1102>;
            var qtyCheckList = list.Where(p => p.ReturnQty > p.ReturnPossibleQty).ToList();
            if (qtyCheckList.Count > 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_61));
                SetSaveMessageCheck = false;
                return;
            }
            ModelService.Save();

            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_81));
            IsFormControlChanged = false;
            ActClose();
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var textEdit = sender as TextEdit;
                if (textEdit == null) return;

                var outLotNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                if (outLotNo.IsNullOrEmpty())
                {

                }
                else
                {
                    IService<TN_BAN1100> LocalService = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");

                    var TN_BAN1101 = ModelService.GetChildList<TN_BAN1101>(p => p.OutLotNo == outLotNo).FirstOrDefault();
                    if (TN_BAN1101 == null)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("OutLotNo")));
                        
                    }
                    else
                    {
                        var VI_RETURN_OBJECT = LocalService.GetChildList<VI_RETURN_OBJECT>(p => p.OutLotNo == outLotNo).FirstOrDefault();
                        if (VI_RETURN_OBJECT.ReturnPossibleQty <= 0)
                        {
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("ReturnInPossibleQty")));
                            
                        }
                        else
                        {
                            var newObj = (TN_BAN1102)ModelBindingSource.AddNew();
                            newObj.OutNo = TN_BAN1101.OutNo;
                            newObj.OutSeq = TN_BAN1101.OutSeq;
                            newObj.TN_BAN1101 = TN_BAN1101;
                            newObj.ItemCode = TN_BAN1101.ItemCode;
                            newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).FirstOrDefault();
                            newObj.OutLotNo = TN_BAN1101.OutLotNo;
                            newObj.InLotNo = TN_BAN1101.InLotNo;
                            newObj.ReturnQty = 0;
                            newObj.ReturnId = GlobalVariable.LoginId;
                            newObj.ReturnDate = DateTime.Today;
                            newObj.ReturnWhCode = TN_BAN1101.TN_BAN1001.InWhCode;
                            newObj.ReturnWhPosition = TN_BAN1101.TN_BAN1001.InWhPosition;
                            newObj.ReturnPossibleQty = VI_RETURN_OBJECT.ReturnPossibleQty;
                            newObj.NewRowFlag = "Y";
                            ModelService.Insert(newObj);
                            GridExControl.BestFitColumns();
                        }
                    }
                    LocalService.Dispose();
                }

                textEdit.EditValue = "";
                e.Handled = true;
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = ModelBindingSource.Current as TN_BAN1102;
            if (detailObj == null) return;

            if (e.Column.FieldName == "ReturnWhCode")
            {
                detailObj.ReturnWhPosition = null;
            }
        }

        private void ReturnWhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = ModelBindingSource.Current as TN_BAN1102;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.ReturnWhCode + "'";
        }
    }
}