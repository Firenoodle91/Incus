using System;
using System.Collections.Generic;

using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Factory;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace HKInc.Service.Controls
{
    public abstract class GridButtonEdit : RepositoryItemButtonEdit
    {
        private PopupCallback callback;

        public GridView ViewControl { get; set; }
        public GridColumn FocusedColumn { get; set; }

        /// <summary>
        /// 20210118 오세완 차장
        /// TreeListEx에 텍스트를 입력하는 popup을 출력하기 위해 추가
        /// </summary>
        public TreeListEx ViewTree { get; set; }

        /// <summary>
        /// 20210118 오세완 차장
        /// TreeListEx에서 mouse로 click한 column저장용
        /// </summary>
        public TreeListColumn FocusedTreeColumn { get; set; }

        /// <summary>
        /// 20210118 오세완 차장
        /// TreeListEx에서 mouse로 click한 Tree Node 저장용
        /// </summary>
        public TreeListNode FocusedTreeNode { get; set; }
        public int FocusedRow { get; set; }
        protected string CodeColumnName { get; set; }
        protected string ReturnCodeColumnName { get; set; }
        protected string ReturnEditColumnName { get; set; }
        protected Dictionary<string, string> ColumnMatch { get; set; }
        /// <summary>
        /// 20210321 김태영 대리
        /// 출하성적서 레포트 풀 텍스트로 인해 기본 출력 데이터
        /// </summary>
        public string DefaultText { get; set; }

        protected void SetButtonEdit(bool ftpFlag, string name, ButtonPressedEventHandler handler, PopupCallback callback, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            this.Name = name;
            this.TextEditStyle = textEditStyle;
            this.ButtonClick += handler;
            this.callback = callback;
        }

        protected void SetButtonEdit(string name, ButtonPressedEventHandler handler, PopupCallback callback, TextEditStyles textEditStyle, int maxLength)
        {
            this.Name = name;
            this.TextEditStyle = textEditStyle;
            this.ButtonClick += handler;
            this.callback = callback;
            this.MaxLength = maxLength;
        }

        protected void SetButtonEdit(string name, ButtonPressedEventHandler handler, PopupCallback callback, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            this.Name = name;
            this.TextEditStyle = textEditStyle;
            this.ButtonClick += handler;
            this.callback = callback;
        }

        protected void SetButtonEdit(string name, PopupCallback callback, TextEditStyles textEditStyle, int maxLength)
        {
            SetButtonEdit(name, ButtonClickHandler, callback, textEditStyle, maxLength);
        }

        protected void SetButtonEdit(string name, PopupCallback callback, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            SetButtonEdit(name, ButtonClickHandler, callback, textEditStyle);
        }

        /// <summary>
        /// 20210119 오세완 차장
        /// CommentGirdButtonEdit에서 메모 팝업을 출력하기 위해서 이벤트 연결 함수 추가
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="callback"></param>
        /// <param name="textEditStyle"></param>
        protected void SetButtonEdit_Tree(string sName, PopupCallback callback, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            this.Name = sName;
            this.TextEditStyle = textEditStyle;
            this.ButtonClick += ButtonClickHandler_Tree;
            this.callback += callback;
        }


        protected void SetButtonEdit(string name, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            SetButtonEdit(name, ButtonClickHandler, DefaultEditButtonCallback, textEditStyle);
        }

        //Callback이 지정되지 않을때 사용하는 기본  Callback
        private void DefaultEditButtonCallback(object sender, PopupArgument args)
        {
            try
            {
                DataParam map = (DataParam)args.Map.GetValue(PopupParameter.DataParam);

                ViewControl.SetFocusedRowCellValue(FocusedColumn, map.GetValue(ReturnEditColumnName));

                if (string.IsNullOrEmpty(CodeColumnName))
                {
                    foreach (string columnName in ColumnMatch.Keys)
                        ViewControl.SetFocusedRowCellValue(columnName, map.GetValue(ColumnMatch[columnName]));
                }
                else
                {
                    ViewControl.SetFocusedRowCellValue(CodeColumnName, map.GetValue(ReturnCodeColumnName));
                }
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        // 상속클래스에서 구현 해야되는 Click Handler
        // SetPopupCallbackForm(PopupScreen popupEnum, DataParam map)을 
        // Call해서 화면 Open
        protected abstract void ButtonClickHandler(object sender, EventArgs e);

        /// <summary>
        /// 20210118 오세완 차장
        /// CommentGridButtonEdit에서 Tree용으로 따로 사용하기 위해서 선언
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void ButtonClickHandler_Tree(object sender, EventArgs e);

        // ButtonClickHandler에서 Call해서 Popup Open
        protected virtual void SetPopupCallbackForm(PopupScreen popupEnum, PopupDataParam map)
        {
            try
            {
                var form = PopupFactory.GetPopupCallbackForm(popupEnum, map, this.callback);
                form.ShowPopup(true);
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        /// <summary>
        /// 20210119 오세완 차장
        /// 메모팝업을 트리 컨트롤에도 사용하애 해서 구현
        /// </summary>
        /// <param name="popupEnum"></param>
        /// <param name="map"></param>
        protected void SetPopupCallbackForm_Tree(PopupScreen popupEnum, PopupDataParam map)
        {
            try
            {
                var form = PopupFactory.GetPopupCallbackForm(popupEnum, map, this.callback, true);
                form.ShowPopup(true);
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }

        //SetButtonEdit_Tree
        /// <summary>
        /// 20210321 김태영
        /// CommentGirdButtonEdit에서 메모 팝업을 출력하기 위해서 이벤트 연결 함수 추가
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="callback"></param>
        /// <param name="textEditStyle"></param>
        protected void SetButtonEdit_TY(string sName, PopupCallback callback, string setTxt, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            this.Name = sName;
            this.TextEditStyle = textEditStyle;
            this.ButtonClick += ButtonClickHandlerTY;
            this.callback += callback;
            this.DefaultText = setTxt;
        }

        protected abstract void ButtonClickHandlerTY(object sender, EventArgs e);

    }
}
