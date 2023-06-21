using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

using HKInc.Utils.Interface.Forms;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.Base
{
    public partial class RibbonFormTemplate : DevExpress.XtraBars.Ribbon.RibbonForm, IBaseForm
    {
        #region IBaseForm variable
        protected IUserRight _UserRight;
        protected HKInc.Ui.Model.Domain.Menu _Menu;
        protected IMenuOpenLogService MenuOpenLogService = LogFactory.GetMenuOpenLogService();
        protected IStandardMessage MessageHelper = HelperFactory.GetStandardMessage();
        #endregion

        public RibbonFormTemplate()
        {
            InitializeComponent();
        }

        protected virtual void SetMessage(string message)
        {
            HKInc.Utils.Interface.Handler.IMainFormMessage mainForm = (HKInc.Utils.Interface.Handler.IMainFormMessage)this.MdiParent;

            if (mainForm != null)
                mainForm.SetMessage(message);
        }

        #region IBaseForm 구현 Infterface
        public void SetToolbarVisible(bool visible) { }

        public void SetToolbarButtonVisible(bool visible) { }

        public void SetToolbarButtonVisible(ToolbarButton button, bool visible) { }

        // void SetToolbarPermission();

        // MainForm에서 Child form의 UserRight로 설정할때 사용
        public void SetToolbarPermission(IUserRight userRight) { }

        // MainForm에서 Child form의 UserRight로 설정할때 사용
        public void SetToolbarPermission(IUserRight userRight, string formText) { }

        public IUserRight UserRight { get { return this._UserRight; } set { _UserRight = value; } }

        // Menu class to open this form
        public HKInc.Ui.Model.Domain.Menu FormMenu
        {
            get { return this._Menu; }
            set { this._Menu = value; MenuOpenLogService.SetOpenMenuLog(DateTime.Now, this._Menu.MenuId.GetIntNullToZero()); }
        }

        public bool isFormOpen { get; set; }

        public Image MdiTabImage { get; set; }

        // MainForm Toolbar button click event시 호출
        // Refresh, Save, Export, Print
        public void ToolbarButtonClicked(ToolbarButton toobarButton) { }

        #endregion        

        protected virtual void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Popup은 기록하지 않는다.
            if (_Menu != null)
                MenuOpenLogService.SetCloseMenuLog(DateTime.Now);
        }
    }
}