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

            SetMainFormCultureChange();
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
            popButtonMainMenuRefresh.Caption = labelConvert.GetLabelText("RefreshMenu");
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

            if (GlobalVariable.LoginId.ToLower() == "admin" || GlobalVariable.LoginId.ToLower() == "jsi")
            {
                ButtonList.Add(ToolbarButton.Sql, barButtonSql);
            }

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
            else if (e.Item.Tag.GetIntNullToZero() == (int)ToolbarButton.Sql) CustomSqlQueryOpen();
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
            WaitHandler WaitHandler = new WaitHandler();
            WaitHandler.ShowWait();

            SetCodeCache();
            ReloadNotice();
            CreateMainMenu();

            WaitHandler.CloseWait();
        }

        public void SetMainFormCultureChange()
        {
            WaitHandler WaitHandler = new WaitHandler();
            WaitHandler.ShowWait();

            HKInc.Utils.Interface.Helper.ILabelConvert labelConvert = HelperFactory.GetLabelConvert();

            #region 오른쪽 상단 버튼 텍스트 변경
            foreach (ToolbarButton toolbarButton in ButtonList.Keys)
            {
                ButtonList[toolbarButton].Caption = string.Empty;
                ButtonList[toolbarButton].Caption = string.Format("{0}[{1}]", labelConvert.GetLabelText(ButtonList[toolbarButton].Name.Substring(9)), ButtonList[toolbarButton].ShortcutKeyDisplayString);
            }
            #endregion

            #region 왼쪽 상단 버튼 텍스트 변경
            var domainTitle = barButtonDomain.SuperTip.Items[0] as ToolTipTitleItem;
            var domainTooltip = barButtonDomain.SuperTip.Items[1] as ToolTipItem;
            domainTitle.Text = labelConvert.GetLabelText("CacheReload");
            domainTooltip.Text = labelConvert.GetLabelText("CacheReloadTooltip");

            var changePasswordTitle = barButtonChangePassword.SuperTip.Items[0] as ToolTipTitleItem;
            var changePasswordTooltip = barButtonChangePassword.SuperTip.Items[1] as ToolTipItem;
            changePasswordTitle.Text = labelConvert.GetLabelText("ChangeSetting");
            changePasswordTooltip.Text = labelConvert.GetLabelText("ChangeSettingTooltip");

            var homeTitle = barButtonHome.SuperTip.Items[0] as ToolTipTitleItem;
            var homeTooltip = barButtonHome.SuperTip.Items[1] as ToolTipItem;
            homeTitle.Text = labelConvert.GetLabelText("FormAllClose");
            homeTooltip.Text = labelConvert.GetLabelText("FormAllCloseTooltip");

            var logoutTitle = barButtonLogout.SuperTip.Items[0] as ToolTipTitleItem;
            var logoutTooltip = barButtonLogout.SuperTip.Items[1] as ToolTipItem;
            logoutTitle.Text = labelConvert.GetLabelText("Restart");
            logoutTooltip.Text = labelConvert.GetLabelText("RestartTooltip");

            var popTitle = barButtonPop.SuperTip.Items[0] as ToolTipTitleItem;
            var popTooltip = barButtonPop.SuperTip.Items[1] as ToolTipItem;
            popTitle.Text = labelConvert.GetLabelText("StratPOP");
            popTooltip.Text = labelConvert.GetLabelText("StratPOPTooltip");

            var sqlTitle = barButtonSql.SuperTip.Items[0] as ToolTipTitleItem;
            var sqlTooltip = barButtonSql.SuperTip.Items[1] as ToolTipItem;
            sqlTitle.Text = "Writing sql statements";
            sqlTooltip.Text = "You can get the result by writing sql.";
            #endregion

            #region 메뉴 텍스트 변경
            mainMenu.BeginUpdate();
            foreach (var v in mainMenu.Columns.ToList())
            {
                v.FieldName = DataConvert.GetCultureDataFieldName("MenuName", "MenuName2", "MenuName3");
            }
            mainMenu.EndUpdate();
            #endregion

            #region 메뉴 버튼 및 드롭다운 메뉴 텍스트 변경

            dockPanelMainMenu.Text = labelConvert.GetLabelText("MainMenu");
            docPanelBookmark.Text = labelConvert.GetLabelText("FavoriteMenu");
            navGroupBookmark.Caption = labelConvert.GetLabelText("MyMenu");
            navGroupFavorite.Caption = labelConvert.GetLabelText("FavoriteMenu");

            popButtonAddMyMenu.Caption = labelConvert.GetLabelText("AddToMyMenu");
            popButtonMainMenuRefresh.Caption = labelConvert.GetLabelText("RefreshMenu");
            popButtonExpandMenu.Caption = labelConvert.GetLabelText("ExpandAll");
            popButtonCollapseMenu.Caption = labelConvert.GetLabelText("CollapseAll");
            popButtonRemove.Caption = labelConvert.GetLabelText("RemoveBookmark");
            popButtonRefeshBookmark.Caption = labelConvert.GetLabelText("RefreshBookmark");

            #endregion

            WaitHandler.CloseWait();
        }

        void OpenSetting()
        {
            try
            {
                ChangeSetting form = new ChangeSetting();
                form.Owner = this;
                form.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
        }
         
        void GoHome()
        {
            var waitHandler = new WaitHandler();
            try
            {
                waitHandler.ShowWait();
                for (int i = mdiManager.Pages.Count - 1; i >= 0; i--)
                {
                    mdiManager.Pages[i].MdiChild.Close();
                }
                SetToolbarButtonVisible(ToolbarButton.Close, false);    // When ChildForm close all, 'barButtonClose' visible option is false.
                //SetToolbarButtonVisible(ToolbarButton.Pop, true);
            }
            finally
            {
                waitHandler.CloseWait();
            }

        }

        void OpenPop()
        {
            var popForm = new Ui.View.View.POP.XFPOP1000();
            popForm.Show();

            //DialogResult rt = MessageBox.Show("프레스 공정입니까?", "", MessageBoxButtons.YesNo);
            //GlobalVariable.KeyPad = true;
            //if (rt == DialogResult.Yes)
            //{
            //    HKInc.Ui.View.POP.XFPOP001 form = new HKInc.Ui.View.POP.XFPOP001();
            //    form.Show();
            //}
            //else
            //{
            //   HKInc.Ui.View.POP.XFPOP002 form = new HKInc.Ui.View.POP.XFPOP002();
            //FPOPOPEN form = new FPOPOPEN();
            //form.Show();
            //}
        }

        void CustomSqlQueryOpen()
        {
            var form = new CustomSqlQuery();
            form.Show();
        }

        void RestartApplication()
        {
            HKInc.Utils.Interface.Helper.ILabelConvert labelConvert = HelperFactory.GetLabelConvert();
            HKInc.Utils.Interface.Helper.IStandardMessage messageHelper = HelperFactory.GetStandardMessage();

            if (MessageBoxHandler.Show(messageHelper.GetStandardMessage((int)StandardMessageEnum.M_4), labelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
            column.FieldName = DataConvert.GetCultureDataFieldName("MenuName", "MenuName2", "MenuName3");
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
        }

        void mainMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (tree == null) return;

            TreeListHitInfo info = tree.CalcHitInfo(tree.PointToClient(MousePosition));

            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {                                                
                if (info.HitInfoType == HitInfoType.Cell && !info.Node.HasChildren)                                 
                    OpenForm((((HKInc.Ui.Model.Domain.MenuUserList)((BindingSource)tree.DataSource).Current)).Menu);                
            }
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
            WaitHandler waitHandler = new WaitHandler();
            try
            {
                if (FormHelper.IsLoadedForm(string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId)))
                {
                    FormHelper.GetParentForm(string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId)).Focus();
                    return;
                }

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
                ((IBaseForm)form).isFormOpen = true;
                form.MdiParent = this;
                form.Show();
                ((IBaseForm)form).isFormOpen = false;
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
            finally
            {
                waitHandler.CloseWait();
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
            {
                ButtonList[button].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
                ButtonList[button].Enabled = visible;
            }
        }        

        public void SetToolbarPermission(HKInc.Utils.Interface.Helper.IUserRight userRight)
        {
            SetToolbarButtonVisible(ToolbarButton.Refresh, userRight.HasSelect);
            SetToolbarButtonVisible(ToolbarButton.Save, userRight.HasEdit);
            SetToolbarButtonVisible(ToolbarButton.Export, userRight.HasExport);
            SetToolbarButtonVisible(ToolbarButton.Print, userRight.HasPrint);

            //ButtonList[ToolbarButton.Refresh].Visibility = userRight.HasSelect ? BarItemVisibility.Always : BarItemVisibility.Never;
            //ButtonList[ToolbarButton.Save].Visibility = userRight.HasEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
            //ButtonList[ToolbarButton.Export].Visibility = userRight.HasExport ? BarItemVisibility.Always : BarItemVisibility.Never;
            //ButtonList[ToolbarButton.Print].Visibility = userRight.HasPrint ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        public void SetToolbarPermission(HKInc.Utils.Interface.Helper.IUserRight userRight, string formText)
        {            
            SetToolbarButtonVisible(ToolbarButton.Refresh, userRight.HasSelect);
            if (formText == "부적합관리" || formText == "포장관리")
                SetToolbarButtonVisible(ToolbarButton.Save, false);
            SetToolbarButtonVisible(ToolbarButton.Export, userRight.HasExport);
            SetToolbarButtonVisible(ToolbarButton.Print, userRight.HasPrint);

            //ButtonList[ToolbarButton.Refresh].Visibility = userRight.HasSelect ? BarItemVisibility.Always : BarItemVisibility.Never;
            //ButtonList[ToolbarButton.Save].Visibility = userRight.HasEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
            //ButtonList[ToolbarButton.Export].Visibility = userRight.HasExport ? BarItemVisibility.Always : BarItemVisibility.Never;
            //ButtonList[ToolbarButton.Print].Visibility = userRight.HasPrint ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        #endregion       
    }
}