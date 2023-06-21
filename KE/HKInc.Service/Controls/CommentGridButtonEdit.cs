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

        //public CommentGridButtonEdit(GridView gv, bool hasEdit = false, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor, int BestFitWidth = 200)
        //{
        //    SetCommentButtonEdit(gv, hasEdit, textEditStyle, BestFitWidth);
        //}

        //public CommentGridButtonEdit(GridControlEx gridControl, bool hasEdit = false, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        //{
        //    SetCommentButtonEdit((GridView)gridControl.MainView, hasEdit, textEditStyle);
        //}

        //public CommentGridButtonEdit(GridEx gridEx, bool hasEdit = false, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        //{
        //    SetCommentButtonEdit(gridEx.MainGrid.MainView, hasEdit);
        //}

        public CommentGridButtonEdit(GridEx gridEx, string fieldName, bool hasEdit = false)
        {
            gridEx.MainGrid.SetEditable(fieldName);
            if (!hasEdit)
            {
                gridEx.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridEx.MainGrid.MainView.Columns[fieldName].AppearanceCell.Options.UseBackColor = false;
            }

            SetCommentButtonEdit(gridEx.MainGrid.MainView, hasEdit);
        }

        public CommentGridButtonEdit(GridEx gridEx, string fieldName, bool hasEdit, int bestFitWidth, int maxLength)
        {
            gridEx.MainGrid.SetEditable(fieldName);
            if (!hasEdit)
            {
                gridEx.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridEx.MainGrid.MainView.Columns[fieldName].AppearanceCell.Options.UseBackColor = false;
            }

            SetCommentButtonEdit(gridEx.MainGrid.MainView, hasEdit, bestFitWidth, maxLength);
        }

        public CommentGridButtonEdit(GridEx_POP gridEx, string fieldName, bool hasEdit = false, int bestFitWidth = 100)
        {
            gridEx.MainGrid.SetEditable(fieldName);
            if (!hasEdit)
            {
                gridEx.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridEx.MainGrid.MainView.Columns[fieldName].AppearanceCell.Options.UseBackColor = false;
            }

            SetCommentButtonEdit(gridEx.MainGrid.MainView, hasEdit, bestFitWidth);
        }

        /// <summary>
        /// 20210119 오세완 차장 
        /// BOM관리에서 TreeListEx control에 메모 팝업을 띄우기 위한 함수
        /// </summary>
        /// <param name="treeEx"></param>
        /// <param name="fieldName"></param>
        /// <param name="hasEdit"></param>
        public CommentGridButtonEdit(TreeListEx treeEx, string fieldName, bool hasEdit = false)
        {
            SetCommentButtonEdit_Tree(treeEx, hasEdit, 100);
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

        /// <summary>
        /// 20210119 오세완 차장
        /// 트리구조 컨트롤에서 클릭한 경우의 이벤트 헨들러를 따로 구현
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonClickHandler_Tree(object sender, EventArgs e)
        {
            FocusedRow = ViewTree.TreeList.FocusedNode.Nodes.Count;
            FocusedTreeColumn = ViewTree.TreeList.FocusedNode.TreeList.FocusedColumn;
            FocusedTreeNode = ViewTree.TreeList.FocusedNode;

            PopupDataParam popupParam = new PopupDataParam();
            popupParam.SetValue(PopupParameter.EditMode, this.hasEdit ? PopupEditMode.New : PopupEditMode.ReadOnly);
            popupParam.SetValue(PopupParameter.FocusedRow, FocusedRow);
            popupParam.SetValue(PopupParameter.FocusedGridColumn, FocusedTreeColumn);

            DataParam map = new DataParam();
            map.SetValue(FocusedTreeColumn.FieldName, ViewTree.TreeList.GetRowCellValue(FocusedTreeNode, FocusedTreeColumn));
            map.SetValue("MAX_LENGTH", 1000);

            popupParam.SetValue(PopupParameter.DataParam, map);
            SetPopupCallbackForm_Tree(PopupScreen.Comment, popupParam);
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

        /// <summary>
        /// 20210119 오세완 차장
        /// 트리구조 컨트롤에서 메모팝업에 대한 콜백함수 구현
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CommentCallback_Tree(object sender, PopupArgument args)
        {
            DataParam map = args.Map.GetValue(PopupParameter.DataParam) as DataParam;
            if (map != null)
            {
                ViewTree.TreeList.SetFocusedRowCellValue(FocusedTreeColumn.FieldName, map.GetValue(FocusedTreeColumn.FieldName));
                ((IFormControlChanged)ViewTree.TreeList.FindForm()).SetIsFormControlChanged(true);
            }
        }

        private void SetCommentButtonEdit(GridView gv, bool hasEdit, int BestFitWidth, int maxLength)
        {
            SetButtonEdit("Commentbutton", CommentCallback, hasEdit ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor, maxLength);
            ViewControl = gv;
            this.hasEdit = hasEdit;
            this.BestFitWidth = BestFitWidth;
        }

        private void SetCommentButtonEdit(GridView gv, bool hasEdit, int BestFitWidth = 100)
        {
            SetButtonEdit("Commentbutton", CommentCallback, hasEdit ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor);
            ViewControl = gv;
            this.hasEdit = hasEdit;
            this.BestFitWidth = BestFitWidth;
        }

        /// <summary>
        /// 20210119 오세완 차장
        /// 트리컨트롤에서 메모팝업 출력을 위한 함수
        /// </summary>
        /// <param name="treeEx">트리 컨트롤</param>
        /// <param name="bHasedit">수정 권한</param>
        /// <param name="iBestfitwidth">컬럼 크기</param>
        private void SetCommentButtonEdit_Tree(TreeListEx treeEx, bool bHasedit, int iBestfitwidth = 100)
        {
            TextEditStyles temp_TES = hasEdit ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor;
            SetButtonEdit_Tree("Commentbutton", CommentCallback_Tree, temp_TES);
            ViewTree = treeEx;
            this.hasEdit = bHasedit;
            this.BestFitWidth = iBestfitwidth;
        }

        #region  텍스트 데이터 없을시 특정 텍스트 보여줄 경우
        /// <summary>
        /// 20210321 김태영 대리
        /// 출하성적서 풀텍스트 하드코딩 하기위해 추가
        /// 기존 데이터가 없을시 보여주고 싶은 텍스트를 가져옴
        /// </summary>
        /// <param name="gridEx"></param>
        /// <param name="fieldName"></param>
        /// <param name="hasEdit"></param>
        public CommentGridButtonEdit(GridEx gridEx, string fieldName, string setTxt, bool hasEdit = false)
        {
            gridEx.MainGrid.SetEditable(fieldName);
            if (!hasEdit)
            {
                gridEx.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridEx.MainGrid.MainView.Columns[fieldName].AppearanceCell.Options.UseBackColor = false;
            }

            SetCommentButtonEditTY(gridEx.MainGrid.MainView, setTxt, hasEdit);
        }

        private void SetCommentButtonEditTY(GridView gv, string setTxt, bool hasEdit, int BestFitWidth = 100)
        {
            SetButtonEdit_TY("Commentbutton", CommentCallbackTY, setTxt, hasEdit ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor);
            ViewControl = gv;
            this.hasEdit = hasEdit;
            this.BestFitWidth = BestFitWidth;
        }

        void CommentCallbackTY(object sender, PopupArgument args)
        {
            DataParam map = args.Map.GetValue(PopupParameter.DataParam) as DataParam;

            if (map != null)
            {
                ViewControl.SetFocusedRowCellValue(FocusedColumn, map.GetValue(FocusedColumn.FieldName));
                ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
            }
        }

        protected override void ButtonClickHandlerTY(object sender, EventArgs e)
        {
            FocusedRow = ViewControl.FocusedRowHandle;
            FocusedColumn = ViewControl.FocusedColumn;

            PopupDataParam popupParam = new PopupDataParam();

            popupParam.SetValue(PopupParameter.EditMode, this.hasEdit ? PopupEditMode.New : PopupEditMode.ReadOnly);
            popupParam.SetValue(PopupParameter.FocusedRow, FocusedRow);
            popupParam.SetValue(PopupParameter.FocusedGridColumn, FocusedColumn);

            DataParam map = new DataParam();

            if (ViewControl.GetRowCellValue(FocusedRow, FocusedColumn).GetNullToNull() == null)
            {
                map.SetValue(FocusedColumn.FieldName, DefaultText);
            }
            else
            {
                map.SetValue(FocusedColumn.FieldName, ViewControl.GetRowCellValue(FocusedRow, FocusedColumn));
            }

            popupParam.SetValue(PopupParameter.DataParam, map);

            SetPopupCallbackForm(PopupScreen.Comment, popupParam);
        }

        #endregion
    }
}
