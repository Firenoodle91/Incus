using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

using HKInc.Service.Factory;
using HKInc.Utils.Interface.Helper;

namespace HKInc.Service.Handler
{
    public class TreeListOptionHandler
    {
        DevExpress.XtraTreeList.TreeList treeList;

        public TreeListOptionHandler(DevExpress.XtraTreeList.TreeList treeList)
        {
            this.treeList = treeList;
        }

        public void SetTreeListOption(bool checkBoxVisible)
        {
            treeList.OptionsView.ShowCheckBoxes = checkBoxVisible;
            treeList.OptionsView.EnableAppearanceEvenRow = true;
            treeList.OptionsView.EnableAppearanceOddRow = true;

            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsBehavior.ReadOnly = true;
            treeList.OptionsBehavior.PopulateServiceColumns = true;
            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;

            treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeList.OptionsSelection.EnableAppearanceFocusedRow = false;
        }

        public void SetTreeListEditable(bool flag, params string[] column)
        {
            treeList.OptionsBehavior.Editable = flag;
            treeList.OptionsBehavior.ReadOnly = !flag;

            if (column.Length > 0)
            {
                foreach (var v in column)
                {
                    treeList.Columns[v].OptionsColumn.ReadOnly = !flag;
                    treeList.Columns[v].OptionsColumn.AllowEdit = flag;
                    treeList.Columns[v].OptionsColumn.AllowFocus = flag;

                    treeList.Columns[v].AppearanceCell.Options.UseBackColor = flag;
                    treeList.Columns[v].AppearanceCell.BackColor = HKInc.Utils.Common.GlobalVariable.GridEditableColumnColor;
                }
            }
        }

        public TreeListColumn AddColumn(string fieldName, string caption, bool visible = true, HorzAlignment horzAlignment = HorzAlignment.Near)
        {
            TreeListColumn treeListColumn = treeList.Columns.Add();
            treeListColumn.Name = fieldName;
            treeListColumn.FieldName = fieldName;
            treeListColumn.Caption = caption;
            treeListColumn.Visible = visible;
            treeListColumn.OptionsColumn.ReadOnly = true;
            treeListColumn.OptionsColumn.AllowEdit = false;
            treeListColumn.OptionsColumn.AllowFocus = false;
            treeListColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            treeListColumn.AppearanceCell.TextOptions.HAlignment = horzAlignment;
            return treeListColumn;
        }
        public TreeListColumn AddColumn(string fieldName, string caption, HorzAlignment horzAlignment, FormatType formatType, string formatString, bool visible = true)
        {
            TreeListColumn treeListColumn = treeList.Columns.Add();
            treeListColumn.Name = fieldName;
            treeListColumn.FieldName = fieldName;
            treeListColumn.Caption = caption;
            treeListColumn.Visible = visible;
            treeListColumn.OptionsColumn.ReadOnly = true;
            treeListColumn.OptionsColumn.AllowEdit = false;
            treeListColumn.OptionsColumn.AllowFocus = false;
            treeListColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            treeListColumn.AppearanceCell.TextOptions.HAlignment = horzAlignment;
            treeListColumn.Format.FormatType = formatType;
            treeListColumn.Format.FormatString = formatString;

            return treeListColumn;
        }

        public TreeListColumn AddColumn(string fieldName, bool visible = true)
        {
            ILabelConvert labelConvert = HelperFactory.GetLabelConvert();

            return AddColumn(fieldName, labelConvert.GetLabelText(fieldName), visible);
        }

        public TreeListColumn AddColumn(string fieldName, HorzAlignment horzAlignment, bool visible = true)
        {
            ILabelConvert labelConvert = HelperFactory.GetLabelConvert();

            return AddColumn(fieldName, labelConvert.GetLabelText(fieldName), visible, horzAlignment);
        }
        
        public TreeListColumn AddColumn(string fieldName, string caption, HorzAlignment horzAlignment, bool visible = true)
        {
            return AddColumn(fieldName, caption, visible, horzAlignment);
        }
    }
}
