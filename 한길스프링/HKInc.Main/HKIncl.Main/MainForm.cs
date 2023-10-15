using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;  
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Forms;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using System.Drawing;

namespace HKInc.Main
{
    public partial class MainForm : XtraForm, IMainFormMessage, IReloadDashboard, IToolBar
    {
        private NotificationHandler CodeCacheHandler;        
        private readonly Dictionary<ToolbarButton, BarButtonItem> ButtonList = new Dictionary<ToolbarButton, BarButtonItem>();
        private NavBarItemLink SelectedVavBarLink; // when click remove link
        private HKInc.Utils.Images.IconImageCollection TabImageCollectionList;
        private IService<Notice> NoticeService = (IService<Notice>)ServiceFactory.GetDomainService("Notice");

        //버전체크
        //////delegate void TimerEventFiredDelegate();
        //////System.Threading.Timer VersionCheckTimer;
        //////ApplicationVersionChecking versionChecking = new ApplicationVersionChecking();

        public MainForm()
        {
            InitializeComponent();

            InitForm();
        }

        void InitForm()
        {
            SetIconImageCollection();
            SetCodeCache();            
            SetDashboard();
            SetDocPanelCaption();
            SetToolbar();
            SetMidPage();
            SetMainMenu();
            SetBookmarkAndFavorite();            
            SetPopupMenu();
            
        }

        #region SetVersionCheck
        //private void SetVersionCheck()
        //{
        //    VersionCheckTimer = new System.Threading.Timer(Callback);
        //    VersionCheckTimer.Change(0, (1000 * 600));    // dueTime 은 Timer 가 시작되기 전 대기 시간 (ms)
        //}

        //void Callback(object status)
        //{
        //    // UI 에서 사용할 경우는 Cross-Thread 문제가 발생하므로 Invoke 또는 BeginInvoke 를 사용해서 마샬링을 통한 호출을 처리하여야 한다.
        //    BeginInvoke(new TimerEventFiredDelegate(Work));
        //}

        //private void Work()
        //{
        //    if (!versionChecking.CheckFlag)
        //        versionChecking.InstallUpdateSyncWithInfo();
        //    else
        //        VersionCheckTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        //}
        #endregion

        // IconImageCollections
        void SetIconImageCollection()
        {
            try
            {                
                GlobalVariable.IconImageCollection = new HKInc.Utils.Images.IconImageCollection();
                TabImageCollectionList = GlobalVariable.IconImageCollection;
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }
        #region SetCodeCache
        void SetCodeCache()
        {
            HKInc.Utils.Interface.Helper.IMasterCode masterCode = HKInc.Service.Factory.HelperFactory.GetMasterCode();
            HKInc.Utils.Interface.Helper.ILabelConvert labelConvert = HKInc.Service.Factory.HelperFactory.GetLabelConvert();
            HKInc.Utils.Interface.Helper.IStandardMessage messageHelper = HKInc.Service.Factory.HelperFactory.GetStandardMessage();
            masterCode.Reset();
            labelConvert.Reset();
            messageHelper.Reset();

            //Font
            var FontList = masterCode.GetMasterCode((int)MasterCodeEnum.Font).ToList();
            if (FontList.Count > 0)
            {
                string FontName = AppearanceObject.DefaultFont.Name;
                float FontSize = AppearanceObject.DefaultFont.Size;
                var FontNameObj = FontList.Where(p => p.Property2 == "FontName").FirstOrDefault();
                if (FontNameObj != null && !string.IsNullOrEmpty(FontNameObj.Property1.GetNullToEmpty()))
                    FontName = FontNameObj.Property1;

                var FontSizeObj = FontList.Where(p => p.Property2 == "FontSize").FirstOrDefault();
                if (FontSizeObj != null && !string.IsNullOrEmpty(FontSizeObj.Property1.GetNullToEmpty()))
                    FontSize = (float)FontSizeObj.Property1.GetDoubleNullToZero();
                if(FontName != AppearanceObject.DefaultFont.Name || FontSize != AppearanceObject.DefaultFont.Size)
                    AppearanceObject.DefaultFont = new Font(FontName, FontSize);
            }
        }
        #endregion

        #region SetDashboard() Dashboard background 
        void SetDashboard()
        {
            dsview.ConfigureDataConnection += dsview_ConfigureDataConnection;
            try
            {
                using (Stream s = new MemoryStream(Encoding.Default.GetBytes(Properties.Resources.Dashboard)))
                {
                    s.Position = 0;
                    dsview.LoadDashboard(s);
                    ReloadNotice();
                }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }        

        void dsview_ConfigureDataConnection(object sender, DashboardConfigureDataConnectionEventArgs e)
        {
            MsSqlConnectionParameters parameters = (MsSqlConnectionParameters)e.ConnectionParameters;

            parameters.DatabaseName = ServerInfo.Database;
            parameters.ServerName = ServerInfo.Server;
            parameters.UserName = ServerInfo.UserId;
            parameters.Password = ServerInfo.Password;
        }
        #endregion

        #region SetButtonCaption()
        void SetDocPanelCaption()
        {
            HKInc.Utils.Interface.Helper.ILabelConvert labelConvert = HelperFactory.GetLabelConvert();
            dockManager.DockingOptions.ShowCloseButton = false;

            dockPanelMainMenu.Text = labelConvert.GetLabelText("MainMenu");
            docPanelBookmark.Text = labelConvert.GetLabelText("FavoriteMenu");
            navGroupBookmark.Caption = labelConvert.GetLabelText("MyMenu");
            navGroupFavorite.Caption = labelConvert.GetLabelText("FavoriteMenu");
            
            popButtonAddMyMenu.Caption = labelConvert.GetLabelText("AddToMyMenu");
            popButtonMainMenuRefresh.Caption = labelConvert.GetLabelText("RefreshMeanu");
            popButtonExpandMenu.Caption = labelConvert.GetLabelText("ExpandAll");
            popButtonCollapseMenu.Caption = labelConvert.GetLabelText("CollapseAll");
            popButtonRemove.Caption = labelConvert.GetLabelText("RemoveBookmark");
            popButtonRefeshBookmark.Caption = labelConvert.GetLabelText("RefreshBookmark");
        }
        #endregion

        #region SetToolbar
        void SetToolbar()
        {
            HKInc.Utils.Interface.Helper.ILabelConvert labelConvert = HelperFactory.GetLabelConvert();

            ButtonList.Add(ToolbarButton.Domain, barButtonDomain);
            ButtonList.Add(ToolbarButton.Setting, barButtonChangePassword);
            ButtonList.Add(ToolbarButton.Home, barButtonHome);
            ButtonList.Add(ToolbarButton.Logout, barButtonLogout);
            ButtonList.Add(ToolbarButton.Refresh, barButtonRefresh);
            ButtonList.Add(ToolbarButton.Save, barButtonSave);
            ButtonList.Add(ToolbarButton.Export, barButtonExport);
            ButtonList.Add(ToolbarButton.Print, barButtonPrint);
            ButtonList.Add(ToolbarButton.Close, barButtonClose);
            ButtonList.Add(ToolbarButton.Pop, barButtonPop);

            foreach (ToolbarButton toolbarButton in ButtonList.Keys)
            {
                ButtonList[toolbarButton].Tag = (int)toolbarButton;
                ButtonList[toolbarButton].ItemClick += ToolbarButtonClick;
                ButtonList[toolbarButton].Caption = string.Format("{0}[{1}]", labelConvert.GetLabelText(ButtonList[toolbarButton].Caption), ButtonList[toolbarButton].ShortcutKeyDisplayString);
            }            
        }

        void ToolbarButtonClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Tag.GetIntNullToZero() == (int)ToolbarButton.Domain) ResetDomain();
            else if (e.Item.Tag.GetIntNullToZero() == (int)ToolbarButton.Setting) OpenSetting();
            else if (e.Item.Tag.GetIntNullToZero() == (int)ToolbarButton.Home) GoHome();
            else if (e.Item.Tag.GetIntNullToZero() == (int)ToolbarButton.Logout) RestartApplication();
            else if (e.Item.Tag.GetIntNullToZero() == (int)ToolbarButton.Close) CloseForm();
            else if (e.Item.Tag.GetIntNullToZero() == (int)ToolbarButton.Pop) OpenPop();
            else MidChildToolBarClicked((ToolbarButton)(e.Item.Tag.GetIntNullToZero()));
        }

        void MidChildToolBarClicked(ToolbarButton button)
        {
            if (mdiManager.SelectedPage != null && mdiManager.SelectedPage.MdiChild != null)
            {
                ((IBaseForm)(mdiManager.SelectedPage.MdiChild)).ToolbarButtonClicked(button);
            }
        }

        void ResetDomain()
        {
           
            SetCodeCache();

            ReloadNotice();

            CreateMainMenu();
        }

        void OpenSetting()
        {
            try
            {
                ChangeSettingKor form = new ChangeSettingKor();
                form.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }
         
        void GoHome()
        {
            //MessageBoxHandler.Show("Home Buttion clicked");

            WaitHandler waitHandler = new WaitHandler();
            try
            {
                waitHandler.ShowWait();
                for (int i = mdiManager.Pages.Count - 1; i >= 0; i--)
                {
                    mdiManager.Pages[i].MdiChild.Close();
                }
                SetToolbarButtonVisible(ToolbarButton.Close, false);    // When ChildForm close all, 'barButtonClose' visible option is false.
            }
            finally
            {
                waitHandler.CloseWait();
                waitHandler = null;
            }
        }

        void OpenPop()
        {
            //DialogResult rt = MessageBox.Show("프레스 공정입니까?", "", MessageBoxButtons.YesNo);
            //GlobalVariable.KeyPad = true;
            //if (rt == DialogResult.Yes)
            //{
            //    HKInc.Ui.View.POP.XFPOP001 form = new HKInc.Ui.View.POP.XFPOP001();
            //    form.Show();
            //}
            //else
            //{
            //    HKInc.Ui.View.POP.XFPOP002 form = new HKInc.Ui.View.POP.XFPOP002();
            //    form.Show();
            //}

            GlobalVariable.KeyPad = true;
            HKInc.Ui.View.POP.XFPOP004 form = new HKInc.Ui.View.POP.XFPOP004();
            form.Show();
        }

        void RestartApplication()
        {
            HKInc.Utils.Interface.Helper.ILabelConvert labelConvert = HelperFactory.GetLabelConvert();
            HKInc.Utils.Interface.Helper.IStandardMessage messageHelper = HelperFactory.GetStandardMessage();

            if (MessageBoxHandler.Show(messageHelper.GetStandardMessage(4), labelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {                
                Application.ExitThread();
                Application.Restart();
            }
        }

        void CloseForm()
        {
            if (mdiManager.Pages.Count > 0)
            {
                mdiManager.SelectedPage.MdiChild.Close();
                if (mdiManager.Pages.Count == 0)
                    SetToolbarButtonVisible(ToolbarButton.Close, false);
            }            
        }
        #endregion

        #region SetMidiPageEvent
        void SetMidPage()
        {
            HKInc.Service.Handler.EventHandler.MidiPageEventHandler mdiHandler = new Service.Handler.EventHandler.MidiPageEventHandler(mdiManager);

            this.mdiManager.PageAdded += mdiHandler.PageAdded;            
            this.mdiManager.BeginFloating += mdiHandler.BeginFloating;
            this.mdiManager.EndDocking += mdiHandler.EndDocking;
            this.mdiManager.EndFloating += mdiHandler.EndFloating;
            this.mdiManager.FloatMDIChildDragging += mdiHandler.FloatMdiChildDragging;
            this.mdiManager.PageRemoved += mdiHandler.PageRemoved;
            this.mdiManager.SelectedPageChanged += mdiHandler.SelectedPageChanged;
        }
        #endregion

        #region SetMainMenu
        void SetMainMenu()
        {
            mainMenu.BeginUpdate();

            TreeListColumn column = mainMenu.Columns.Add();
            column.Visible = true;
            column.FieldName = HKInc.Service.Helper.LookUpFieldHelper.GetCultureFieldName("Menu");
            column.Name = "MenuName";

            mainMenu.StateImageList = TabImageCollectionList;

            // Culture확인해서 MenuName을 스위칭 한다.

            mainMenu.GetStateImage += mainMenu_GetStateImage;
            mainMenu.MouseClick += mainMenu_MouseClick;
            mainMenu.MouseDoubleClick += mainMenu_MouseDoubleClick;

            CreateMainMenu();

            mainMenu.EndUpdate();
        }

        void CreateMainMenu()
        {            
            BindingSource source = new BindingSource();
            source.DataSource = MenuFactory.GetMenuService().GetMainMenuList(GlobalVariable.UserId);            
            mainMenu.DataSource = source;

            mainMenu.ExpandAll();

            MenuExpandCheck(mainMenu.Nodes);
        }
        
        void mainMenu_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree == null) return;

            e.NodeImageIndex = e.Node["IconIndex"].GetIntNullToZero();            
        }

        void mainMenu_MouseClick(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree == null) return;

            TreeListHitInfo info = tree.CalcHitInfo(tree.PointToClient(MousePosition));

            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && tree.State == TreeListState.Regular)
            {
                if (info.HitInfoType == HitInfoType.Cell && info.Node.HasChildren == false)
                    popButtonAddMyMenu.Visibility = BarItemVisibility.Always;
                else
                    popButtonAddMyMenu.Visibility = BarItemVisibility.Never;

                popupMenuOfMainMenu.ShowPopup(MousePosition);
            }

            if (e.Button == MouseButtons.Left)
            {
                if (info.HitInfoType == HitInfoType.Cell && !info.Node.HasChildren)
                    OpenForm((((HKInc.Ui.Model.Domain.MenuUserList)((BindingSource)tree.DataSource).Current)).Menu);
            }
        }

        void mainMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //TreeList tree = sender as TreeList;
            //if (tree == null) return;

            //TreeListHitInfo info = tree.CalcHitInfo(tree.PointToClient(MousePosition));

            //if (e.Button == MouseButtons.Left && e.Clicks == 2)
            //{
            //    if (info.HitInfoType == HitInfoType.Cell && !info.Node.HasChildren)                                 
            //        OpenForm((((HKInc.Ui.Model.Domain.MenuUserList)((BindingSource)tree.DataSource).Current)).Menu);                
            //}
        }

        //  ( 중분류로 메뉴 축소 , 재귀함수 사용 )
        void MenuExpandCheck(DevExpress.XtraTreeList.Nodes.TreeListNodes treeListNode)
        {
            for (int a = 0; a < treeListNode.Count; a++)
            {
                if (treeListNode[a].Nodes.Count == 0)
                {
                    if(treeListNode[a].ParentNode != null)
                        treeListNode[a].ParentNode.Collapse();
                }
                else
                {
                    MenuExpandCheck(treeListNode[a].Nodes);
                }
            }            
        }

        #endregion

        #region SetBookmark
        void SetBookmarkAndFavorite()
        {
            SetBookmark();
            SetFavorite();
            SetBookmarkAndFavoriteEvent();
        }

        void SetBookmark()
        {
            navGroupBookmark.ItemLinks.Clear();

            IEnumerable<HKInc.Ui.Model.Domain.MenuBookmark> bookmarkList = MenuFactory.GetMenuService().GetBookmarkMenuList(GlobalVariable.UserId);

            foreach(var bookmark in bookmarkList)
            {
                NavBarItem bookmarkItem = navBarBookmark.Items.Add();
                bookmarkItem.Caption = GlobalVariable.IsDefaultCulture ? bookmark.Menu.MenuName : GlobalVariable.IsSecondCulture ? bookmark.Menu.MenuName2 : bookmark.Menu.MenuName3;
                bookmarkItem.SmallImage = TabImageCollectionList.GetIconImage(bookmark.Menu.IconIndex.GetIntNullToZero());
                bookmarkItem.Tag = bookmark;
                
                navGroupBookmark.ItemLinks.Add(bookmarkItem);
            }
        }

        void SetFavorite()
        {
            navGroupFavorite.ItemLinks.Clear();            

            IEnumerable<HKInc.Ui.Model.Domain.MenuFavorite> favoriteList = MenuFactory.GetMenuService().GetFavoriteMenuList(GlobalVariable.UserId);

            foreach (var bookmark in favoriteList)
            {
                NavBarItem bookmarkItem = navBarBookmark.Items.Add();
                bookmarkItem.Caption = GlobalVariable.IsDefaultCulture ? bookmark.Menu.MenuName : GlobalVariable.IsSecondCulture ? bookmark.Menu.MenuName2 : bookmark.Menu.MenuName3;
                bookmarkItem.SmallImage = TabImageCollectionList.GetIconImage(bookmark.Menu.IconIndex.GetIntNullToZero());
                bookmarkItem.Tag = bookmark;

                navGroupFavorite.ItemLinks.Add(bookmarkItem);
            }
        }

        void SetBookmarkAndFavoriteEvent()
        {            
            this.navBarBookmark.LinkClicked += navBarBookmarkAndFavorite_LinkClicked;
            this.navBarBookmark.MouseClick += navBarBookmarkAndFavorite_MouseClick;
        }

        void navBarBookmarkAndFavorite_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            if (e.Link.Item.Tag != null)
            {
                if(e.Link.Group == navGroupFavorite)
                    OpenForm(((HKInc.Ui.Model.Domain.MenuFavorite)e.Link.Item.Tag).Menu);
                else
                    OpenForm(((HKInc.Ui.Model.Domain.MenuBookmark)e.Link.Item.Tag).Menu);
            }
        }
        
        void navBarBookmarkAndFavorite_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NavBarControl navBar = sender as NavBarControl;
                NavBarHitInfo hitInfo = navBar.CalcHitInfo(navBar.PointToClient(MousePosition));
                if (hitInfo.InLink && hitInfo.Group.Name.Equals("navGroupBookmark"))
                {
                    SelectedVavBarLink = hitInfo.Link;
                    popupMenuOfBookmark.ShowPopup(MousePosition);
                }
            }
        }
        #endregion

        #region SetPopupMenu
        void SetPopupMenu()
        {
            popButtonAddMyMenu.ItemClick += popButtonAddMyMenu_ItemClick;
            popButtonMainMenuRefresh.ItemClick += popButtonMainMenuRefresh_ItemClick;
            popButtonExpandMenu.ItemClick += popButtonExpandMenu_ItemClick;
            popButtonCollapseMenu.ItemClick += popButtonCollapseMenu_ItemClick;
            popButtonRemove.ItemClick += popButtonRemove_ItemClick;
            popButtonRefeshBookmark.ItemClick += popButtonRefeshBookmark_ItemClick;
        }

        void popButtonAddMyMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            MenuFactory.GetMenuService().AddMenuToBookmarkList((HKInc.Ui.Model.Domain.MenuUserList)((BindingSource)mainMenu.DataSource).Current);
            SetBookmark();
        }

        void popButtonCollapseMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainMenu.CollapseAll();
        }

        void popButtonExpandMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainMenu.ExpandAll();
        }

        void popButtonMainMenuRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateMainMenu();
        }

        void popButtonRefeshBookmark_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetBookmark();
            SetFavorite();
        }

        void popButtonRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(SelectedVavBarLink != null)
            {
                MenuFactory.GetMenuService().RemoveFromBookmarkList((HKInc.Ui.Model.Domain.MenuBookmark)SelectedVavBarLink.Item.Tag);
                SetBookmark();
            }                
        }
        #endregion

        #region Form OnLoad        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //SetNotification(); // Form Handle이 생성되어야 된다.

            barStatusBarUserInfo.Caption = string.Format("[{0}] {1} {2}", GlobalVariable.LoginId, GlobalVariable.UserName, GlobalVariable.EmployeeNo);
            barStatusBarCulture.Caption = GlobalVariable.Culture;
            //this.Text = string.Format("{0} [Version {1}], {2}, {3}", GlobalVariable.ApplicationName, GlobalVariable.ApplicationVersion, GlobalVariable.ServerIp, GlobalVariable.ProductionDataBaseName);
            this.Text = string.Format("{0} [Version {1}], {2}, {3}", GlobalVariable.ApplicationName, GlobalVariable.ApplicationVersion, GlobalVariable.DatabaseIP, GlobalVariable.ProductionDataBaseName);

            GlobalVariable.CurrentInstance = string.Format("{0}.{1}", this.GetType().Namespace, this.GetType().Name);
            LogFactory.GetLoginLogService().SetLoginLog(DateTime.Now);

            LookAndFeel.StyleChanged += LookAndFeel_StyleChanged;

            //SetVersionCheck();// Form Handle이 생성되어야 된다.
        }

        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            string skinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;            
            HKInc.Service.Handler.RegistryHandler.SetValue(HKInc.Utils.Common.GlobalVariable.SkinPath, "Skin", skinName);
        }
        #endregion

        #region FormClosing        
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                deleteTempDirectory();
                base.OnFormClosed(e);

                GlobalVariable.CurrentInstance = string.Format("{0}.{1}", this.GetType().Namespace, this.GetType().Name);
                LogFactory.GetLoginLogService().SetLogoutLog(DateTime.Now);
            }
            catch(Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }            
        }

        protected void deleteTempDirectory()
        {            
            if (Directory.Exists(string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GlobalVariable.TempFolder)))
            {
                string[] files = Directory.GetFiles(string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GlobalVariable.TempFolder));
                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }
                Directory.Delete(string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GlobalVariable.TempFolder));
            }            
        }
        #endregion

        #region Open Form
        void OpenForm(HKInc.Ui.Model.Domain.Menu menu)
        {

            try
            {
                if (FormHelper.IsLoadedForm(string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId)))
                {
                    FormHelper.GetParentForm(string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId)).Focus();
                    return;
                }

                WaitHandler waitHandler = new WaitHandler();
                waitHandler.ShowWait();
                XtraForm form = FormHelper.CreateForm(menu.Screen.Module.Assembly, menu.Screen.NameSpace, menu.Screen.ClassName);
                if (form == null)
                {
                    MessageBoxHandler.ErrorShow(new Exception(string.Format("Form creation error, null returned from FormHelper.CreateForm{0}{1}Assembly : {2}{3}NameSpace: {4}{5}Class : {6}",
                                                                            Environment.NewLine, Environment.NewLine,
                                                                            menu.Screen.Module.Assembly, Environment.NewLine,
                                                                            menu.Screen.NameSpace, Environment.NewLine,
                                                                            menu.Screen.ClassName)));
                    return;
                }

                form.Name = string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId);
                form.Text = GlobalVariable.Culture.Equals(GlobalVariable.DefaultCulture) ? menu.MenuName : menu.MenuName2;
                ((IBaseForm)form).UserRight = MenuFactory.GetUserRight(menu.MenuId, GlobalVariable.UserId);
                ((IBaseForm)form).FormMenu = menu;
                ((IBaseForm)form).MdiTabImage = TabImageCollectionList.GetIconImage(menu.IconIndex.GetIntNullToZero());
                form.MdiParent = this;
                form.Show();
                waitHandler.CloseWait();
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }
        #endregion

        #region Nofication Hanlder
        void SetNotification()
        {
            try
            {
                //CodeCacheHandler = new NotificationHandler(GlobalVariable.LogInDataBase, QueName.CodeCache, this);
                flyMsg.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.flyMsg_ButtonClick);
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }

        void flyMsg_ButtonClick(object sender, FlyoutPanelButtonClickEventArgs e)
        {
            FlyoutPanel fly = sender as FlyoutPanel;
            if (fly == null) return;

            fly.HidePopup();
        }
        #endregion

        #region IReloadDashboar Interface구현
        public void ReloadDashboard()
        {
            dsview.ReloadData();
        }

        public void ReloadNotice()
        {
            IService<Notice> NoticeService = (IService<Notice>)ServiceFactory.GetDomainService("Notice");
            Notice obj = NoticeService.GetList(p => 1==1).First();
            if (obj != null)            
                ((DevExpress.DashboardCommon.TextBoxDashboardItem)dsview.Dashboard.Items["textBoxDashboardItem1"]).Rtf = obj.Contents;            
        }
        #endregion

        #region IMainFormMessage Interface구현
        public void SetMessage(string message)
        {
            barStaticMessage.Caption = message;
        }

        public Form FlyoutMainForm { get { return this; } }
        public FlyoutPanel FlyMsg { get { return flyMsg; } }
        public LabelControl LabelTitle { get { return labelTitle; } }
        public LabelControl LabelText { get { return labelText; } }
        #endregion

        #region IToolBar Interface구현        
        public void SetToolbarVisible(bool visible)
        {
            this.barTools.Visible = visible;
        }

        public void SetToolbarButtonVisible(bool visible)
        {
            foreach (ToolbarButton button in ButtonList.Keys)
            {
                if (button != ToolbarButton.Domain && button != ToolbarButton.Setting && 
                    button != ToolbarButton.Home && button != ToolbarButton.Logout &&
                    button != ToolbarButton.Dashboard &&
                    button != ToolbarButton.Pop)
                    SetToolbarButtonVisible(button, visible);                
            }
                
        }

        public void SetToolbarButtonVisible(ToolbarButton button, bool visible)
        {
            if (button == ToolbarButton.Dashboard)
                dsview.Visible = visible;
            else
                ButtonList[button].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }        

        public void SetToolbarPermission(HKInc.Utils.Interface.Helper.IUserRight userRight)
        {
            ButtonList[ToolbarButton.Refresh].Visibility = userRight.HasSelect ? BarItemVisibility.Always : BarItemVisibility.Never;
            ButtonList[ToolbarButton.Save].Visibility = userRight.HasEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
            ButtonList[ToolbarButton.Export].Visibility = userRight.HasExport ? BarItemVisibility.Always : BarItemVisibility.Never;
            ButtonList[ToolbarButton.Print].Visibility = userRight.HasPrint ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        #endregion       
    }
}