using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity.Validation;

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Helper;

namespace HKInc.Service.Base
{
    [ToolboxItem(false)]
    public partial class BaseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        protected LayoutControlHandler LayoutControlHandler;
        protected IStandardMessage MessageHelper = HelperFactory.GetStandardMessage();
        protected ILabelConvert LabelConvert = HelperFactory.GetLabelConvert();
        protected IMasterCode MasterCode = HelperFactory.GetMasterCode();
        protected WaitHandler WaitHandler = new WaitHandler();
        protected IGridRowLocator GridRowLocator;

        protected BindingSource GridBindingSource = new BindingSource();
        protected HKInc.Service.Controls.GridEx GridExControl;
        
        public BaseUserControl()
        {
            InitializeComponent();            
        }

        protected void Init()
        {            
            try
            {
                SetLayoutControl();

                InitControls();
                InitBindingSource(); // Event는 DataLoad후 처리한다.
                InitToolbarButton();
                InitCombo();
                InitGrid();
                InitRepository();
                InitDataLoad();
                InitBindingSourceEvent();                
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }

        private void SetLayoutControl()
        {
            LayoutControlHandler = new LayoutControlHandler(this.Controls);

            LayoutControlHandler.SetLayoutControlPadding();

            this.SuspendLayout();

            LayoutControlHandler.SetLabelText();

            this.ResumeLayout(true);
        }

        protected virtual void InitControls() { }
        protected virtual void InitBindingSource() { }
        protected virtual void InitToolbarButton() { }
        protected virtual void InitCombo() { }
        protected virtual void InitGrid() { }
        protected virtual void InitRepository() { }
        protected virtual void InitDataLoad() { }
        protected virtual void InitBindingSourceEvent() { }

        protected void ActRefresh()
        {
            try
            {                
                DataLoad();                
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }            
        }

        protected virtual void DataLoad() { }
        
        protected void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)
                ap.Font = font;
        }
        public void CloseControl()
        {
            ControlClosed();
        }
        protected virtual void ControlClosed()
        {
            Dispose();
        }
    }
}
