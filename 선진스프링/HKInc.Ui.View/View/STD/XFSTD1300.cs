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

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// BOM관리화면
    /// </summary>
    public partial class XFSTD1300 : HKInc.Service.Base.TreeListFormTemplate
    {
        IService<TN_STD1300> ModelService = (IService<TN_STD1300>)ProductionFactory.GetDomainService("TN_STD1300");

        public XFSTD1300()
        {
            InitializeComponent();
            TreeListExControl = treeListEx1;
            TreeListExControl.TreeList.CellValueChanged += TreeList_CellValueChanged;
            TreeListExControl.TreeList.NodeCellStyle += TreeList_NodeCellStyle;
            TreeListExControl.TreeList.ShowingEditor += TreeList_ShowingEditor;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = IconImageList;

            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("HighLevelAdd") + "[F3]"); // "최상위추가[F3]"
            IsGridButtonFileChooseEnabled = true;
            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("LowLevelAdd") + "[F10]" );// "하위추가[F10]"
            TreeListExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut(Keys.F10));

            TreeListExControl.AddColumn("BomCode", LabelConvert.GetLabelText("BomCode"), false);
            TreeListExControl.AddColumn("ParentBomCode", LabelConvert.GetLabelText("ParentBomCode"), false);
            TreeListExControl.AddColumn("Level", LabelConvert.GetLabelText("Level"), false);
            TreeListExControl.AddColumn("CustomItemCode", LabelConvert.GetLabelText("ItemCode"));
            TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemName"));
            TreeListExControl.AddColumn("CustomItemCombineSpec", LabelConvert.GetLabelText("Spec"));
            TreeListExControl.AddColumn("CustomItemUnit", LabelConvert.GetLabelText("Unit"));
            TreeListExControl.AddColumn("UseQty", LabelConvert.GetLabelText("UseQty"));
            TreeListExControl.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            TreeListExControl.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));

            TreeListExControl.KeyFieldName = "BomCode";
            TreeListExControl.ParentFieldName = "ParentBomCode";
            TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "ItemCode", "UseQty", "DisplayOrder", "Memo");
        }

        protected override void InitRepository()
        {
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            TreeListExControl.SetRepositoryItemSpinEdit("UseQty", true, DefaultBoolean.Default, "n3", true);
            TreeListExControl.SetRepositoryItemSpinEdit("DisplayOrder");
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("CustomItemUnit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            TreeListExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            TreeListExControl.Clear();
            ModelService.ReLoad();

            var Item = lup_Item.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            TreeListBindingSource.DataSource = ModelService.GetList(p => (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                      && (string.IsNullOrEmpty(Item) ? true : p.ItemCode == Item)
                                                                   )
                                                                   .ToList();
            TreeListExControl.DataSource = TreeListBindingSource;
            TreeListExControl.BestFitColumns();
            TreeListExControl.ExpandAll();
        }

        protected override void DataSave()
        {
            TreeListExControl.TreeList.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 0레벨 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TreeListEx_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (!IsFirstLoaded) ActRefresh();

            var newObj = new TN_STD1300()
            {
                BomCode = DbRequestHandler.GetSeqStandard("BOM"),
                UseQty = 0,
                Level = 0,
                UseFlag = "Y",
            };
            TreeListBindingSource.Add(newObj);
            ModelService.Insert(newObj);
        }

        /// <summary>
        /// 하위레벨 등록
        /// </summary>
        protected override void FileChooseClicked()
        {
            if (!IsFirstLoaded) ActRefresh();
            var obj = TreeListBindingSource.Current as TN_STD1300;
            if (obj == null) return;

            if (obj.Level > 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32));
                //MessageBoxHandler.Show("최상위에 대해서만 하위추가를 하실 수 있습니다.");
                return;
            }

            var newObj = new TN_STD1300()
            {
                BomCode = DbRequestHandler.GetSeqStandard("BOM"),
                ParentBomCode = obj.BomCode,
                UseQty = 0,
                Level = obj.Level + 1,
                UseFlag = "Y"
            };
            TreeListBindingSource.Add(newObj);
            ModelService.Insert(newObj);
            TreeListExControl.ExpandAll();
        }

        /// <summary>
        /// 삭제
        /// </summary>
        protected override void DeleteRow()
        {
            if (!IsFirstLoaded) ActRefresh();
            var obj = TreeListBindingSource.Current as TN_STD1300;
            if (obj == null) return;

            var objList = TreeListBindingSource.List as List<TN_STD1300>;
            if (objList.Any(p => p.ParentBomCode == obj.BomCode))
            {                
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ChildData")), LabelConvert.GetLabelText("Warning"));
                return;
            }

            TreeListBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
        }

        private void TreeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            var obj = TreeListBindingSource.Current as TN_STD1300;
            if (obj == null) return;

            if (e.Column.FieldName == "ItemCode")
            {
                var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
                obj.TN_STD1100 = itemObj;
            }
        }

        private void TreeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "UseQty")
            {
                var level = e.Node.GetValue("Level").GetIntNullToZero();

                if (level == 0)
                {
                    e.Appearance.BackColor = Color.Empty;
                }
                else
                {
                    var parentBomId = e.Node.GetValue("ParentBomCode").GetNullToEmpty();
                    var BomCode = e.Node.GetValue("BomCode").GetNullToEmpty();
                    var list = TreeListBindingSource.List as List<TN_STD1300>;
                    if (list.Any(p => p.ParentBomCode == parentBomId && p.BomCode != BomCode && p.UseQty > 0))
                    {
                        e.Appearance.BackColor = Color.Empty;
                    }
                    else
                        e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                }
            }
        }

        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            var obj = TreeListBindingSource.Current as TN_STD1300;
            if (obj == null) return;
            
            var fieldName = TreeListExControl.TreeList.FocusedColumn.FieldName;
            if (fieldName == "UseQty")
            {
                if (obj.Level == 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    var parentBomId = obj.ParentBomCode;
                    var list = TreeListBindingSource.List as List<TN_STD1300>;
                    if (list.Any(p => p.ParentBomCode == parentBomId && p.BomCode != obj.BomCode && p.UseQty > 0))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        protected override void TreeList_MouseDoubleClick(object sender, MouseEventArgs e) { }
    }
}