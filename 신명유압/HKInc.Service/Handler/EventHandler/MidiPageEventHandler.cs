using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Drawing;

using DevExpress.XtraTabbedMdi;

using HKInc.Utils.Interface.Forms;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;

namespace HKInc.Service.Handler.EventHandler
{
    public class MidiPageEventHandler
    {
        private XtraTabbedMdiManager MdiManager;

        public MidiPageEventHandler(XtraTabbedMdiManager mdiManager)
        {
            MdiManager = mdiManager;
        }

        public void SelectedPageChanged(object sender, EventArgs e)
        {
            if (MdiManager.SelectedPage != null && MdiManager.SelectedPage.MdiChild != null)
            {
                GlobalVariable.CurrentInstance = MdiManager.SelectedPage.MdiChild.Name;

                var ibaseForm = MdiManager.SelectedPage.MdiChild as IBaseForm;
                var itoolBar = MdiManager.MdiParent as IToolBar;
                var baseForm = ibaseForm as HKInc.Service.Base.BaseForm;

                ibaseForm.SetToolbarVisible(false);
                if (MdiManager.SelectedPage.MdiChild.Text == "부적합관리" || MdiManager.SelectedPage.MdiChild.Text == "포장관리")
                    itoolBar.SetToolbarPermission(ibaseForm.UserRight, MdiManager.SelectedPage.MdiChild.Text);  // child의 UserRight이용해서 설정
                else
                    itoolBar.SetToolbarPermission(ibaseForm.UserRight);  // child의 UserRight이용해서 설정
                itoolBar.SetToolbarButtonVisible(ToolbarButton.Close, true); // Main의 close활성화  

                if (ibaseForm.UserRight.HasReload && !ibaseForm.isFormOpen)
                {
                    if (baseForm != null)
                    {
                        WaitHandler waitHandler = new WaitHandler();
                        waitHandler.ShowWait();
                        baseForm.ReloadRefresh();
                        waitHandler.CloseWait();
                    }
                }

                //((IBaseForm)MdiManager.SelectedPage.MdiChild).SetToolbarVisible(false);
                //((IToolBar)MdiManager.MdiParent).SetToolbarPermission(((IBaseForm)MdiManager.SelectedPage.MdiChild).UserRight);  // child의 UserRight이용해서 설정
                //((IToolBar)MdiManager.MdiParent).SetToolbarButtonVisible(ToolbarButton.Close, true); // Main의 close활성화          
            }
        }

        public void PageAdded(object sender, MdiTabPageEventArgs e)
        {            
            if (e.Page.MdiChild != null)
            {
                e.Page.Image = ((IBaseForm)e.Page.MdiChild).MdiTabImage;

                //소스 추가 (UnDocking 시 아이콘이미지)
                Bitmap a = new Bitmap(((IBaseForm)e.Page.MdiChild).MdiTabImage);
                IntPtr Hicon = a.GetHicon();
                Icon newIcon = Icon.FromHandle(Hicon);
                ((DevExpress.XtraEditors.XtraForm)e.Page.MdiChild).Icon = newIcon;
            }
            ((IToolBar)MdiManager.MdiParent).SetToolbarButtonVisible(ToolbarButton.Dashboard, false); // Dashboard 숨기기
        }

        public void PageRemoved(object sender, MdiTabPageEventArgs e)
        {
            if (MdiManager.Pages.Count == 0)
            {
                ((IToolBar)MdiManager.MdiParent).SetToolbarButtonVisible(false);
                ((IToolBar)MdiManager.MdiParent).SetToolbarButtonVisible(ToolbarButton.Dashboard, true); // Dashboard 활성화
            }            
        }

        public void FloatMdiChildDragging(object sender, FloatMDIChildDraggingEventArgs e)
        {
            var info = MdiManager.GetType().GetMethod("PointToClient", BindingFlags.Instance | BindingFlags.NonPublic);
            var point = (Point)info.Invoke(MdiManager, new object[] { e.ScreenPoint });
            var rect = MdiManager.Bounds;
            rect.Height = 20;
            if (MdiManager.Pages.Count == 0 && rect.Contains(point))
            {
                MdiManager.FloatForms.Remove(e.ChildForm);
                e.ChildForm.MdiParent = MdiManager.MdiParent;
            }            
        }

        public void BeginFloating(object sender, FloatingCancelEventArgs e)
        {
            (MdiManager.MdiParent as IToolBar).SetToolbarButtonVisible(false);

            if (e.ChildForm != null)
            {                
                ((IBaseForm)e.ChildForm).SetToolbarVisible(true);
                ((IBaseForm)e.ChildForm).SetToolbarPermission(((IBaseForm)e.ChildForm).UserRight);
            }
            e.Cancel = false;            
        }

        public void EndFloating(object sender, FloatingEventArgs e)
        {
            if (MdiManager.SelectedPage != null && MdiManager.SelectedPage.MdiChild != null)
            {
                ((IBaseForm)MdiManager.SelectedPage.MdiChild).SetToolbarVisible(false);
                ((IToolBar)MdiManager.MdiParent).SetToolbarPermission(((IBaseForm)MdiManager.SelectedPage.MdiChild).UserRight);
            }            
        }        

        public void EndDocking(object sender, FloatingEventArgs e)
        {
            if (e.ChildForm != null)
            {
                ((IBaseForm)e.ChildForm).SetToolbarVisible(false);
                if (e.ChildForm.Text == "부적합관리" || e.ChildForm.Text == "포장관리")
                    ((IToolBar)MdiManager.MdiParent).SetToolbarPermission(((IBaseForm)MdiManager.SelectedPage.MdiChild).UserRight, e.ChildForm.Text);
                else
                    ((IToolBar)MdiManager.MdiParent).SetToolbarPermission(((IBaseForm)MdiManager.SelectedPage.MdiChild).UserRight);
            }            
        }
    }
}
