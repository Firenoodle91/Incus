using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

using DevExpress.XtraLayout;
using DevExpress.Utils;
using DevExpress.XtraBars;

using HKInc.Utils.Class;
using HKInc.Utils.Enum;

namespace HKInc.Service.Handler
{
    public class LayoutControlHandler
    {
        HKInc.Utils.Interface.Helper.ILabelConvert labelConvert = HKInc.Service.Factory.HelperFactory.GetLabelConvert();
        System.Windows.Forms.Control.ControlCollection _layoutControls;

        public LayoutControlHandler(System.Windows.Forms.Control.ControlCollection layoutControls)
        {
            this._layoutControls = layoutControls;    
        }

        public void SetLayoutControlPadding()
        {
            if (this._layoutControls.Count > 0)
            {
                foreach (Control control in this._layoutControls)
                {                    
                    if (control.GetType() == typeof(LayoutControl))
                    {
                        ((LayoutControl)control).Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 8, 2);
                        break;
                    }
                }
            }
        }
        public void SetRequiredLabelText(Type type, Dictionary<string, Control> controlEnableList, Control.ControlCollection layoutControls)
        {
            foreach (var controlName in controlEnableList.Keys)
            {
                RequiredAttribute attribute = type.GetAttributeFrom<RequiredAttribute>(controlName);
                if (attribute != null)
                {
                    Control editControl = controlEnableList[controlName];
                    foreach (Control control in layoutControls)
                    {
                        LayoutControl lc = control as LayoutControl;
                        if (lc != null)
                        {
                            foreach (var item in lc.Items.Where(p => p.GetType() == typeof(LayoutControlItem) && p.Name.ToLower().Contains("lcitem") && ((LayoutControlItem)p).Control == editControl))
                            {
                                lc.BeginUpdate();
                                ((LayoutControlItem)item).AppearanceItemCaption.ForeColor = HKInc.Utils.Common.GlobalVariable.MandatoryFieldColor;
                                ((LayoutControlItem)item).Text = "*" + ((LayoutControlItem)item).Text;
                                lc.EndUpdate();
                            }
                        }
                    }
                }
            }
        }

        public void SetLabelText()
        {
            try
            {
                if (this._layoutControls.Count > 0)
                {
                    foreach (Control control in this._layoutControls)
                    {
                        if (control.GetType() == typeof(LayoutControl))
                        {
                            LayoutControl lc = (LayoutControl)control;
                            lc.BeginUpdate();
                            SetLabelText(lc);
                            lc.EndUpdate();
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }
        
        public void SetRequiredLabelText<TEntity>(object instance, Dictionary<string, Control> controlEnableList, Control.ControlCollection layoutControls) where TEntity : class
        {
            foreach (var controlName in controlEnableList.Keys)
            {
                RequiredAttribute attribute = ((TEntity)instance).GetAttributeFrom<RequiredAttribute>(controlName);
                if(attribute != null)
                {
                    Control editControl = controlEnableList[controlName];
                    if (layoutControls.Count > 0)
                    {                        
                        foreach (Control control in layoutControls)
                        {
                            if (control.GetType() == typeof(LayoutControl))
                            {
                                LayoutControl lc = (LayoutControl)control;
                                if (lc.Items.Count > 0)
                                {
                                    foreach (var item in lc.Items)
                                    {
                                        if (item.GetType() == typeof(LayoutControlItem) && (((LayoutControlItem)item).Name.ToLower().Contains("lcitem")))
                                        {
                                            if(((LayoutControlItem)item).Control == editControl)
                                            {
                                                lc.BeginUpdate();
                                                ((LayoutControlItem)item).AppearanceItemCaption.ForeColor = HKInc.Utils.Common.GlobalVariable.MandatoryFieldColor;
                                                lc.EndUpdate();
                                            }
                                                
                                        }
                                    }                                    
                                }
                            }
                        }
                    }
                }
            }
        }
    
        private void SetLabelText(LayoutControl control)
        {            
            if (control.Items.Count > 0)
            {
                foreach (var item in control.Items)
                {
                    if (item.GetType() == typeof(LayoutControlItem) && (((LayoutControlItem)item).Name.ToLower().Contains("lcitem") || ((LayoutControlItem)item).Name.ToLower().Contains("lcfield")|| ((LayoutControlItem)item).Name.ToLower().Contains("lc")))
                    {
                        ((LayoutControlItem)item).AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                        ((LayoutControlItem)item).AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;

                        string itemName = ((LayoutControlItem)item).Name.Replace("lcItem", "").Replace("lcField", "").Replace("lcGroup", "").Replace("lc", "");
                        ((LayoutControlItem)item).Text = labelConvert.GetLabelText(itemName);
                        if (((LayoutControlItem)item).Tag != null && ((LayoutControlItem)item).Tag.GetType() == typeof(String) && (String)((LayoutControlItem)item).Tag == "True")
                        {
                            ((LayoutControlItem)item).AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                            ((LayoutControlItem)item).AppearanceItemCaption.Options.UseForeColor = true;
                        }
                    }
                    else if (item.GetType() == typeof(LayoutControlGroup) && ((LayoutControlGroup)item).Name.ToLower().Contains("lcgroup"))
                    {
                        ((LayoutControlGroup)item).AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                        ((LayoutControlGroup)item).AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Center;

                        string itemName = ((LayoutControlGroup)item).Name.Replace("lcGroup", "");
                        ((LayoutControlGroup)item).Text = labelConvert.GetLabelText(itemName);
                        ((LayoutControlGroup)item).AppearanceGroup.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }        
    }
}
