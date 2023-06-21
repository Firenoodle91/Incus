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
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Forms;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    public partial class NoticeEditor : HKInc.Service.Base.RibbonFormTemplate
    {                  
        IService<Notice> NoticeService = (IService<Notice>)ServiceFactory.GetDomainService("Notice");
        Notice NoticeObj;

        public NoticeEditor()
        {
            InitializeComponent();

            InitForm();
        }

        void InitForm()
        {
            this.FormClosing += NoticeEditor_FormClosing;                     
            this.Load += NoticeEditor_Load;

            richEditControl.Document.Sections[0].Page.PaperKind = System.Drawing.Printing.PaperKind.A4;
            richEditControl.Document.Sections[0].Page.Landscape = true;
        }

        private void NoticeEditor_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.FromHandle(new Bitmap(MdiTabImage).GetHicon());            

            DataLoad();
        }

        private void DataLoad()
        {
            // Load Data
            NoticeObj = NoticeService.GetList().FirstOrDefault();
            if(NoticeObj != null)
                richEditControl.RtfText = NoticeObj.Contents;
        }

        protected void fileSaveItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Save editor
            if (richEditControl.Modified)
            {
                if (NoticeObj == null)
                {
                    NoticeService.Insert(new Notice()
                    {
                        Contents = richEditControl.RtfText
                    });
                    NoticeService.Save();
                    richEditControl.Modified = false;
                }
                else
                {
                    NoticeObj.Contents = richEditControl.RtfText;
                    NoticeService.Update(NoticeObj);
                    NoticeService.Save();
                    richEditControl.Modified = false;
                }
            }
        }
        

        private void NoticeEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save editor
            if (richEditControl.Modified)
            {
                IStandardMessage MessageHelper = HelperFactory.GetStandardMessage();
                ILabelConvert LabelConvert = HelperFactory.GetLabelConvert();

                DialogResult result = HKInc.Service.Handler.MessageBoxHandler.Show(MessageHelper.GetStandardMessage(1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }                    
                else if (result == DialogResult.Yes)
                {
                    if (NoticeObj == null)
                    {
                        NoticeService.Insert(new Notice()
                        {
                            Contents = richEditControl.RtfText
                        });
                        NoticeService.Save();
                        richEditControl.Modified = false;
                    }
                    else
                    {
                        NoticeObj.Contents = richEditControl.RtfText;
                        NoticeService.Update(NoticeObj);
                        NoticeService.Save();
                        richEditControl.Modified = false;
                    }
                }                    
            }
        }        
    }
}