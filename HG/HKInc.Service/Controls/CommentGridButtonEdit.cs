using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;

using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Forms;

namespace HKInc.Service.Controls
{
    public class CommentGridButtonEdit : GridButtonEdit
    {
        private bool hasEdit;

        public CommentGridButtonEdit(GridView gv, bool hasEdit = false, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor, int BestFitWidth = 200)
        {
            SetCommentButtonEdit(gv, hasEdit, textEditStyle, BestFitWidth);
        }

        public CommentGridButtonEdit(GridControlEx gridControl, bool hasEdit = false, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            SetCommentButtonEdit((GridView)gridControl.MainView, hasEdit, textEditStyle);
        }
        
        public CommentGridButtonEdit(GridEx gridEx, bool hasEdit = false, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            SetCommentButtonEdit((GridView)gridEx.MainGrid.MainView, hasEdit, textEditStyle);
        }

        protected override void ButtonClickHandler(object sender, EventArgs e)
        {            
            FocusedRow = ViewControl.FocusedRowHandle;
            FocusedColumn = ViewControl.FocusedColumn;            

            PopupDataParam popupParam = new PopupDataParam();

            popupParam.SetValue(PopupParameter.EditMode, this.hasEdit ? PopupEditMode.New : PopupEditMode.ReadOnly);
            popupParam.SetValue(PopupParameter.FocusedRow, FocusedRow);
            popupParam.SetValue(PopupParameter.FocusedGridColumn, FocusedColumn);

            DataParam map = new DataParam();

            map.SetValue(FocusedColumn.FieldName, ViewControl.GetRowCellValue(FocusedRow, FocusedColumn));
            map.SetValue("MAX_LENGTH", 1000);

            popupParam.SetValue(PopupParameter.DataParam, map);

            SetPopupCallbackForm(PopupScreen.Comment, popupParam);
        }

        void CommentCallback(object sender, PopupArgument args)
        {
            DataParam map = args.Map.GetValue(PopupParameter.DataParam) as DataParam;

            if (map != null)
            {
                ViewControl.SetFocusedRowCellValue(FocusedColumn, map.GetValue(FocusedColumn.FieldName));
                ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
            }
        }

        private void SetCommentButtonEdit(GridView gv, bool hasEdit, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor, int BestFitWidth = 200)
        {
            SetButtonEdit("Commentbutton", CommentCallback, hasEdit ? textEditStyle : TextEditStyles.DisableTextEditor);
            ViewControl = gv;            
            this.hasEdit = hasEdit;
            this.BestFitWidth = BestFitWidth;            
        }
    }
}
