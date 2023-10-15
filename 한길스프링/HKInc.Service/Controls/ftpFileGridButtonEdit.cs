using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;

using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Forms;
using HKInc.Service.Handler;

using System.IO;

namespace HKInc.Service.Controls
{
    public class ftpFileGridButtonEdit : GridButtonEdit
    {
        private bool hasEdit;
        private string FileField;
        private string FileNameField;
        private bool lsreadonly;
 
        public ftpFileGridButtonEdit(GridView gv, bool isedit,string fileField = "File")
        {
            hasEdit = isedit;
           
            SetFileButtonEdit(gv, fileField);
        }
        public ftpFileGridButtonEdit(GridView gv,  string fileField = "File")
        {
           
            SetFileButtonEdit(gv, fileField);
        }
        public ftpFileGridButtonEdit(GridControlEx gridControl, bool isedit, string fileField = "File")
        {
            hasEdit = isedit;
            SetFileButtonEdit((GridView)gridControl.MainView, fileField);
        }

        public ftpFileGridButtonEdit(GridEx gridEx, bool isedit, string fileField = "File")
        {
            hasEdit = isedit;
            SetFileButtonEdit((GridView)gridEx.MainGrid.MainView, fileField);
        }

        public ftpFileGridButtonEdit(GridView gv, bool isedit, string isDraw, string fileField = "File")
        {
            hasEdit = isedit;
            SetFileButtonEdit(gv, fileField);
        }



        public ftpFileGridButtonEdit(GridControlEx gridControl, string fileField = "File")
        {
            SetFileButtonEdit((GridView)gridControl.MainView, fileField);
        }
     
        public ftpFileGridButtonEdit(GridEx gridEx, string fileField = "File")
        {
            SetFileButtonEdit((GridView)gridEx.MainGrid.MainView, fileField );
        }

        public ftpFileGridButtonEdit(GridView gv, string isDraw, string fileField = "File")
        {
            SetFileButtonEdit(gv, fileField);
        }

        protected override void ButtonClickHandler(object sender, EventArgs e)
        {
            ButtonPressedEventArgs args = e as ButtonPressedEventArgs;
            if (args == null) return;


                if (args.Button.Kind == ButtonPredefines.Search)
                {
                    string fileName = ViewControl.GetFocusedRowCellValue(FileNameField).GetNullToEmpty();
                    if (!string.IsNullOrEmpty(fileName))
                    {
                    string[] lfileName1 = fileName.Split(':');
                    if (lfileName1.Length < 2)
                    {
                        byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + fileName);
                        if (img == null) return;
                        string[] lfileName = fileName.Split('/');
                        if (lfileName.Length > 1)
                        {
                            File.WriteAllBytes(lfileName[lfileName.Length - 1], img);
                            HKInc.Service.Handler.FileHandler.StartProcess(lfileName[lfileName.Length - 1]);
                        }
                        else
                        {
                            File.WriteAllBytes(fileName, img);
                            HKInc.Service.Handler.FileHandler.StartProcess(fileName);
                        }
                    }
                    else {
                        byte[] imgfile = FileHandler.FileToByte(fileName);
                        string[] lfileName2 = fileName.Split('\\');
                        File.WriteAllBytes(lfileName2[lfileName2.Length-1], imgfile);
                        HKInc.Service.Handler.FileHandler.StartProcess(lfileName2[lfileName2.Length - 1]);
                    }
                    }
                }
                else if (args.Button.Kind == ButtonPredefines.Delete)
                {
                    ViewControl.SetFocusedRowCellValue(FileField, null);
               //     ViewControl.SetFocusedRowCellValue(FileNameField, null);
                }
                else
                {
                    HKInc.Service.Handler.EventHandler.FileHolder fileHolder = HKInc.Service.Handler.FileHandler.OpenFile();

                    if (fileHolder != null)
                    {
                 //       ViewControl.SetFocusedRowCellValue(FileField, fileHolder.FileData);
                        ViewControl.SetFocusedRowCellValue(FileNameField, fileHolder.FULLFileName);
                        ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
                    }
                }
           
        }

        void DummyCallback(object sender, PopupArgument args) { }

        private void SetFileButtonEdit(GridView gv, string fileField)
        {
           
            SetButtonEdit("FileUploadButton", DummyCallback, TextEditStyles.DisableTextEditor);
         // this.hasEdit = false;
            this.FileField = fileField;
            this.FileNameField = fileField;

            Buttons.Add(new EditorButton(ButtonPredefines.Search));
            Buttons.Add(new EditorButton(ButtonPredefines.Delete));

            ViewControl = gv;
            ViewControl.Columns[FileNameField].AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            ViewControl.Columns[FileNameField].AppearanceCell.Font = new System.Drawing.Font(ViewControl.Columns[FileNameField].AppearanceCell.Font, System.Drawing.FontStyle.Underline);
        }
      
    }
}
