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

namespace HKInc.Service.Controls
{
    public class FileGridButtonEdit : GridButtonEdit
    {
        private string FileField;
        private string FileNameField;

        public FileGridButtonEdit(GridView gv, string fileField = "File", string fileNameField = "FileName")
        {
            SetFileButtonEdit(gv, fileField, fileNameField);
        }

        public FileGridButtonEdit(GridControlEx gridControl, string fileField = "File", string fileNameField = "FileName")
        {
            SetFileButtonEdit((GridView)gridControl.MainView, fileField, fileNameField);
        }
        
        public FileGridButtonEdit(GridEx gridEx, string fileField = "File", string fileNameField = "FileName")
        {
            SetFileButtonEdit((GridView)gridEx.MainGrid.MainView, fileField, fileNameField);
        }

        public FileGridButtonEdit(GridView gv, string isDraw, string fileField = "File", string fileNameField = "FileName")
        {
            SetFileButtonEdit(gv, fileField, fileNameField);
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
                        HKInc.Service.Handler.FileHandler.SaveFile(new HKInc.Service.Handler.EventHandler.FileHolder
                        {
                            FileName = fileName,
                            FileData = (byte[])ViewControl.GetFocusedRowCellValue(FileField)
                        });
                    }
                }
                else if (args.Button.Kind == ButtonPredefines.Delete)
                {
                    ViewControl.SetFocusedRowCellValue(FileField, null);
                    ViewControl.SetFocusedRowCellValue(FileNameField, null);
                }
                else
                {
                    HKInc.Service.Handler.EventHandler.FileHolder fileHolder = HKInc.Service.Handler.FileHandler.OpenFile();

                    if (fileHolder != null)
                    {
                        ViewControl.SetFocusedRowCellValue(FileField, fileHolder.FileData);
                        ViewControl.SetFocusedRowCellValue(FileNameField, fileHolder.FileName);
                        ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
                    }
                }
           
        }

        void DummyCallback(object sender, PopupArgument args) { }

        private void SetFileButtonEdit(GridView gv, string fileField, string fileNameField)
        {
            SetButtonEdit("FileUploadButton", DummyCallback, TextEditStyles.DisableTextEditor);
            this.FileField = fileField;
            this.FileNameField = fileNameField;

            Buttons.Add(new EditorButton(ButtonPredefines.Search));
            Buttons.Add(new EditorButton(ButtonPredefines.Delete));

            ViewControl = gv;
            ViewControl.Columns[FileNameField].AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            ViewControl.Columns[FileNameField].AppearanceCell.Font = new System.Drawing.Font(ViewControl.Columns[FileNameField].AppearanceCell.Font, System.Drawing.FontStyle.Underline);
        }
    }
}
