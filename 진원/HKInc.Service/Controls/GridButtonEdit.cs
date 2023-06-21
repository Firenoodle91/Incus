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

namespace HKInc.Service.Controls
{
    public abstract class GridButtonEdit : RepositoryItemButtonEdit
    {        
        private PopupCallback callback;

        public GridView ViewControl { get; set; }
        public GridColumn FocusedColumn { get; set; }
        public int FocusedRow { get; set; }
        protected string CodeColumnName { get; set; }
        protected string ReturnCodeColumnName { get; set; }
        protected string ReturnEditColumnName { get; set; }
        protected Dictionary<string, string> ColumnMatch { get; set; }

        protected void SetButtonEdit(string name, ButtonPressedEventHandler handler, PopupCallback callback, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            this.Name = name;
            this.TextEditStyle = textEditStyle;
            this.ButtonClick += handler;
            this.callback = callback;           
        }

        protected void SetButtonEdit(string name, PopupCallback callback, TextEditStyles textEditStyle = TextEditStyles.DisableTextEditor)
        {
            SetButtonEdit(name, ButtonClickHandler, callback, textEditStyle);
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

    }
}
