using System;
using System.Collections.Generic;
using System.Windows.Forms;

using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Handler;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;

using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Service.Handler;

namespace HKInc.Service.Base
{
    public partial class PopupCallbackFormMasterDetailTemplate : BaseForm, IPopupCallbackForm
    {
        protected PopupCallback Callback;            // Callback함수 부모윈도우에서 넘어 온다
        protected PopupDataParam PopupParam;         // Parameter
        protected PopupArgument ReturnPopupArgument; // Callback에 같이 보낼 parameter
        protected PopupEditMode EditMode;            // ReadOnly, new        
        protected Dictionary<string, Control> ControlEnableList = new Dictionary<string, Control>(); // 권한에 따라 Enable, Disable할 Control list 
        protected string ActiveFilter = string.Empty;
        protected BindingSource MasterGridBindingSource = new BindingSource();
        protected BindingSource DetailGridBindingSource = new BindingSource();
        protected HKInc.Service.Controls.GridEx MasterGridExControl;
        protected HKInc.Service.Controls.GridEx DetailGridExControl;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator GridRowLocator;
        protected HKInc.Utils.Interface.Helper.IGridRowLocator DetailGridRowLocator;


        protected bool IsMasterGridFocusedRowChangedEnabled { get; set; } = true;


        public PopupCallbackFormMasterDetailTemplate()
        {
            InitializeComponent();
        }

        public PopupCallbackFormMasterDetailTemplate(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
        }

        protected override void InitControls()
        {
            // UserRight에 값이 들어갈대 Toolbar가 설정된다.

            if (PopupParam != null)
            {
                if (PopupParam.ContainsKey(PopupParameter.UserRight))
                    UserRight = (IUserRight)PopupParam.GetValue(PopupParameter.UserRight);
                if (PopupParam.ContainsKey(PopupParameter.EditMode))
                    EditMode = (PopupEditMode)PopupParam.GetValue(PopupParameter.EditMode);
            }
            if (UserRight != null)
            {
                // UserRight권한에 따라 Control 설정
                foreach (var controlName in ControlEnableList.Keys)
                    ControlEnableList[controlName].Enabled = UserRight.HasEdit;
            }
        }

        protected override void InitDataLoad()
        {
            ActRefresh();
            ApplyFilterString();
        }


        protected virtual void ApplyFilterString()
        {
            if (MasterGridExControl != null)
                MasterGridExControl.MainGrid.MainView.ActiveFilter.NonColumnFilter = ActiveFilter;
            if (DetailGridExControl != null)
                DetailGridExControl.MainGrid.MainView.ActiveFilter.NonColumnFilter = ActiveFilter;
        }

        protected override void InitGridLayoutRestore()
        {
            if (MasterGridExControl != null)
                MasterGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
            if (DetailGridExControl != null)
                DetailGridExControl.MainGrid.GridLayoutRestore(); //GridLayout 저장기능을 위해선 꼭 필요 Add컬럼 맨 뒤에 위치해야함 ★
        }

        protected override void InitBindingSource()
        {
            // 변경시 IsControlChanged에 True설정 종료시 CloseQuery에 사용한다.
            if (MasterGridBindingSource != null)
                MasterGridBindingSource.CurrentItemChanged += ModelBindingSource_CurrentItemChanged;
            if (DetailGridBindingSource != null)
                DetailGridBindingSource.CurrentItemChanged += ModelBindingSource_CurrentItemChanged;
        }

        // Callback실행한다. Form_Closed Event에서  Call한다. ReturnPopupArgument를 인수로 넘긴다.
        protected virtual void ExecuteCallback(PopupArgument arg) { if (Callback != null) Callback(this, arg); }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            ExecuteCallback(ReturnPopupArgument); // Callback함수 실행 Close() Call전에 ReturnPopupArgument에 값을 생성하면 Return된다.
        }

        protected virtual void ModelBindingSource_CurrentItemChanged(object sender, System.EventArgs e)
        {
            IsFormControlChanged = true;
        }

        #region IPopupCallbackForm 구현
        public virtual void ShowPopup(bool isDialog = true)
        {
            if (isDialog)
                ShowDialog();
            else
                Show();
        }

        public virtual void SetGridFilter(string filterString)
        {
            ActiveFilter = filterString;
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                bool tempIsFormControlChanged = IsFormControlChanged;

                MasterFocusedRowChanged();

                IsFormControlChanged = tempIsFormControlChanged ? true : false;
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }

        //Callback설정
        public virtual void SetPopupCallback(PopupCallback callback) { this.Callback = callback; }
        #endregion

        // Userright권한에 따라 Enable, Disable할 Control List를 설정한다.
        protected virtual void AddControlList() { }

        protected virtual void MasterFocusedRowChanged() { }
    }
}