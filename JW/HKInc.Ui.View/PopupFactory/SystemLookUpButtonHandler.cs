using System;
using System.Collections.Generic;
using System.Linq;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Service.Controls;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Interface.Helper;
using HKInc.Ui.View.ProductionService;

namespace HKInc.Ui.View.PopupFactory
{
    class SystemLookUpButtonHandler<TEntity> where TEntity : class
    {
        private GridLookUpEditEx lookupEditor;
        private GridLookUpEditEx CascadingLookupEditor;        
        private string GridFilter = string.Empty;
        private ProductionPopupView PopupView;        
        private bool IsMultiSelect;        
        private bool IsObjectContraint;
        private PopupCallback PopupCallback;
        private IUserRight UserRight;
        private PopupEditMode EditMode;

        public SystemLookUpButtonHandler(GridLookUpEditEx edit, ProductionPopupView popupView, PopupCallback callback = null, string constraint = null)
        {
            lookupEditor = edit;
            PopupView = popupView;
            PopupCallback = callback;
            lookupEditor.Constraint = constraint;                     
            IsMultiSelect = false;
        }

        public SystemLookUpButtonHandler(GridLookUpEditEx edit, ProductionPopupView popupView, PopupCallback callback, IUserRight userRight, PopupEditMode editMode, string constraint = null) : this(edit, popupView, callback, constraint)
        {
            this.UserRight = userRight;
            this.EditMode = editMode;
        }

        public SystemLookUpButtonHandler(GridLookUpEditEx edit, GridLookUpEditEx cascadingLookUpEditor, string gridFilter, string constraint = null)
        {
            lookupEditor = edit;
            lookupEditor.Constraint = constraint;
            IsMultiSelect = false;
            CascadingLookupEditor = cascadingLookUpEditor;
            GridFilter = gridFilter;

            lookupEditor.EditValueChanged += EditValueChanged;
        }

        public SystemLookUpButtonHandler(GridLookUpEditEx edit, ProductionPopupView popupView, GridLookUpEditEx cascadingLookUpEditor) : this(edit, popupView)
        {
            CascadingLookupEditor = cascadingLookUpEditor;
            IsObjectContraint = true;

            lookupEditor.EditValueChanged += EditValueChanged;
        }
        
        
        public SystemLookUpButtonHandler(GridLookUpEditEx edit, ProductionPopupView popupView, GridLookUpEditEx cascadingLookUpEditor, string gridFilter, string constraint = null) : this(edit, popupView, null, constraint)
        {
            CascadingLookupEditor = cascadingLookUpEditor;
            GridFilter = gridFilter;

            lookupEditor.EditValueChanged += EditValueChanged;
        }

                
        public void ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                PopupDataParam param = new PopupDataParam();
                IPopupForm form;
                
                param.SetValue(PopupParameter.Constraint, lookupEditor.Constraint); //제약조건    
                param.SetValue(PopupParameter.IsMultiSelect, IsMultiSelect);
                param.SetValue(PopupParameter.UserRight, this.UserRight);
                param.SetValue(PopupParameter.EditMode, this.EditMode);

                form = ProductionPopupFactory.GetPopupForm(PopupView, param, SetLookUpCode);

                if (!string.IsNullOrEmpty(lookupEditor.GetGridFilter()))
                    form.SetGridFilter(lookupEditor.GetGridFilter());
                else
                    form.SetGridFilter(string.Empty);
                
                form.ShowPopup(true);
            }
        }

        public void EditValueChanged(object sender, EventArgs e)
        {
            HKInc.Service.Controls.GridLookUpEditEx editor = sender as HKInc.Service.Controls.GridLookUpEditEx;
            if (editor == null) return;

            if (IsObjectContraint)
            {
                CascadingLookupEditor.Constraint = ((List<TEntity>)editor.DataSource)[editor.SelectedIndex];
            }
            else
            {
                if (editor.EditValue == null)
                    CascadingLookupEditor.SetGridFilter(string.Empty);
                else
                    CascadingLookupEditor.SetGridFilter(string.Format(GridFilter, editor.EditValue));
            }
        }

        private void SetLookUpCode(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e != null)
            {
                PopupDataParam param = (PopupDataParam)e.Map;
                if (param.Keys.Contains(PopupParameter.ReturnObject))
                {
                    TEntity returnObject = (TEntity)param.GetValue(PopupParameter.ReturnObject);
                    lookupEditor.EditValue = returnObject.GetType().GetProperty(lookupEditor.ValueMember).GetValue(returnObject, null);
                }

                if (PopupCallback != null)
                    PopupCallback(this, e);
            }
        }
    }
}
