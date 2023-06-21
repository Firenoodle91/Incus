using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Animation;
using HKInc.Utils.Interface.Forms;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 슬라이드 현황판 테스트
    /// </summary>
    public partial class XFR6000 : HKInc.Service.Base.BaseForm, IToolBar
    {
        Control animatedControl;

        public XFR6000()
        {
            InitializeComponent();
            transitionManager1.CustomTransition += transitionManager1_CustomTransition;
        }

        private void XFR6000_Load(object sender, EventArgs e)
        {
            animatedControl = panelControl1;

            if (transitionManager1.Transitions[animatedControl] == null)
            {
                // Add a transition, associated with the xtraTabControl1, to the TransitionManager.
                Transition transition1 = new Transition();
                transition1.Control = animatedControl;
                transitionManager1.Transitions.Add(transition1);
            }
            // Specify the transition type.
            transitionManager1.Transitions[animatedControl].TransitionType = new SlideFadeTransition();
            ((PushTransition)transitionManager1.Transitions[animatedControl].TransitionType).Parameters.EffectOptions = DevExpress.Utils.Animation.PushEffectOptions.FromRight;

            var TimeObj = DbRequesHandler.GetCommCode("TIME").FirstOrDefault();

            timer1.Interval = 1000 * (TimeObj == null ? 10 : TimeObj.Codename.GetIntNullToZero());
        }

        protected override void InitBindingSource()
        {
            SetToolbarVisible(false);
            SetStaticBarVisible(false);
        }

        protected override void InitDataLoad()
        {
            ShowLogic();
            timer1.Start();
        }

        //BaseTransition CreateTransitionInstance(Transitions transitionType)
        //{
        //    switch (transitionType)
        //    {
        //        case Transitions.Dissolve: return new DissolveTransition();
        //        case Transitions.Fade: return new FadeTransition();
        //        case Transitions.Shape: return new ShapeTransition();
        //        case Transitions.Clock: return new ClockTransition();
        //        case Transitions.SlideFade: return new SlideFadeTransition();
        //        case Transitions.Cover: return new CoverTransition();
        //        case Transitions.Comb: return new CombTransition();
        //        default: return new PushTransition();
        //    }
        //}

        void ShowForm(Type type)
        {
            transitionManager1.StartTransition(animatedControl);

            if (panelControl1.Controls.Count > 0)
                panelControl1.Controls[0].Dispose();

            if (animatedControl == null) return;

            Form f = Activator.CreateInstance(type) as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            panelControl1.Controls.Add(f);
            f.Show();

            transitionManager1.EndTransition();
        }

        // A custom easing function.
        DevExpress.Data.Utils.IEasingFunction myEasingFunc = new DevExpress.Data.Utils.BackEase();

        private void transitionManager1_CustomTransition(DevExpress.Utils.Animation.ITransition transition, DevExpress.Utils.Animation.CustomTransitionEventArgs e)
        {
            // Set a clip region for the state transition.
            e.Regions = new Rectangle[] { this.Bounds };
            // Specify a custom easing function.
            e.EasingFunction = myEasingFunc;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowLogic();
        }

        private void ShowLogic()
        {
            if (panelControl1.Controls.Count > 0)
            {
                var f = panelControl1.Controls[0] as Form;
                if (f.Name == "XFR5000")
                {
                    ShowForm(typeof(XFR5001));
                }
                else
                {
                    ShowForm(typeof(XFR5000));
                }
            }
            else
            {
                ShowForm(typeof(XFR5000));
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ActClose();
        }
    }
}