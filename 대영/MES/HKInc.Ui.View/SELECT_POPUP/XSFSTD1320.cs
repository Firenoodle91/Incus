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
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Utils.Class;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using DevExpress.Xpf;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 20211102 오세완 차장
    /// bom type 팝업
    /// </summary>
    public partial class XSFSTD1320 : HKInc.Service.Base.PopupCallbackTreeFormTemplate
    {
        #region 전역변수
        IService<TN_STD1320> ModelService = (IService<TN_STD1320>)ProductionFactory.GetDomainService("TN_STD1320");
        #endregion

        public XSFSTD1320()
        {
            InitializeComponent();
        }

        public XSFSTD1320(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            TreeListExControl = treeListEx1;
            lup_Typecode.EditValueChanged += Lup_Typecode_EditValueChanged;
        }

        private void Lup_Typecode_EditValueChanged(object sender, EventArgs e)
        {
            ActRefresh();
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);

            SetToolbarButtonCaption(ToolbarButton.Confirm, "추가[F4]");
        }

        protected override void InitControls()
        {
            base.InitControls();
        }



        protected override void InitCombo()
        {
            lup_Typecode.SetDefault(true, "TypeCode", DataConvert.GetCultureDataFieldName("TypeName"), ModelService.GetList(p => true).OrderBy(o=>o.TypeName).ToList());
        }

        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.SetToolbarButtonVisible(false);

            TreeListExControl.AddColumn("TYPE_CODE", false);
            TreeListExControl.AddColumn("SEQ", LabelConvert.GetLabelText("Seq"));
            TreeListExControl.AddColumn("LEVEL", false);
            TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            TreeListExControl.AddColumn("ITEM_NAME1", LabelConvert.GetLabelText("ItemName1"));

            TreeListExControl.AddColumn("ITEM_NAME", LabelConvert.GetLabelText("ItemName"));
            TreeListExControl.AddColumn("TOP_CATEGORY_NAME", LabelConvert.GetLabelText("TopCategory"));
            TreeListExControl.AddColumn("MIDDLE_CATEGORY_NAME", LabelConvert.GetLabelText("MiddleCategory"));
            TreeListExControl.AddColumn("BOTTOM_CATEGORY_NAME", LabelConvert.GetLabelText("BottomCategory"));
            TreeListExControl.AddColumn("SPEC_1", LabelConvert.GetLabelText("Spec1"));

            TreeListExControl.AddColumn("SPEC_2", LabelConvert.GetLabelText("Spec2"));
            TreeListExControl.AddColumn("SPEC_3", LabelConvert.GetLabelText("Spec3"));
            TreeListExControl.AddColumn("SPEC_4", LabelConvert.GetLabelText("Spec4"));
            TreeListExControl.AddColumn("UNIT_NAME", LabelConvert.GetLabelText("Unit"));
            TreeListExControl.AddColumn("WEIGHT", LabelConvert.GetLabelText("Weight"));

            TreeListExControl.AddColumn("PROCESS_CODE_NAME", LabelConvert.GetLabelText("ProcessName"));
            TreeListExControl.AddColumn("USE_QTY", LabelConvert.GetLabelText("UseQty"));
            TreeListExControl.AddColumn("MEMO", LabelConvert.GetLabelText("Memo"));
            TreeListExControl.AddColumn("DISPLAY_ORDER", LabelConvert.GetLabelText("DisplayOrder"));
            TreeListExControl.AddColumn("USE_FLAG", LabelConvert.GetLabelText("UseFlag"));

            TreeListExControl.AddColumn("MG_FLAG", LabelConvert.GetLabelText("MgFlag"));

            TreeListExControl.KeyFieldName = "SEQ";
            TreeListExControl.ParentFieldName = "LEVEL";

            TreeListExControl.TreeList.OptionsView.AutoWidth = false;

            TreeListExControl.TreeList.Columns[0].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[1].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[2].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[3].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[4].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[5].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[6].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;

        }

        protected override void InitRepository()
        {
            TreeListExControl.SetRepositoryItemSpinEdit("USE_QTY", true, DefaultBoolean.Default, "n3", true);
            TreeListExControl.SetRepositoryItemCheckEdit("USE_FLAG", "N");
            TreeListExControl.SetRepositoryItemCheckEdit("MG_FLAG", "N");

            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
        }

        protected override void DataLoad()
        {
            TreeListExControl.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Typecode = new SqlParameter("@TYPE_CODE", lup_Typecode.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XFSTD1320_DETAIL>("USP_GET_XFSTD1320_DETAIL @TYPE_CODE", sp_Typecode).ToList();

                if(result != null)
                    TreeListBindingSource.DataSource = result.ToList();
            }

            TreeListExControl.DataSource = TreeListBindingSource;

            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.OptionsView.AutoWidth = false;
            TreeListExControl.TreeList.BestFitColumns();

        }

        protected override void TreeList_MouseDoubleClick(object sender, MouseEventArgs e) { }

        protected override void Confirm()
        {
            PopupDataParam param = new PopupDataParam();

            string typeCode = lup_Typecode.EditValue.GetNullToEmpty();
            if (typeCode.IsNullOrEmpty())
            {
                string sMessage = MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50);
                MessageBoxHandler.Show(sMessage, LabelConvert.GetLabelText("Warning"));
                return;
            }

            param.SetValue(PopupParameter.ReturnObject, typeCode);

            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }
    }
}