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
    public class FtpFileGridButtonEdit_BAK : GridButtonEdit
    {
        private bool hasEditCheckFlag = false;
        private bool hasEdit;
        private string FileNameField;
        private string FileUrlField;
        private bool ImageFlag;
        private string UploadFilePathFieldName = "UploadFilePath";
        private string DeleteFilePathFieldName = "DeleteFilePath";

        /// <summary>
        /// Grid 에서 파일을 상태값에 따라 수정 또는 읽기로 변경
        /// </summary>
        public FtpFileGridButtonEdit_BAK(bool isEdit, GridEx gridEx, string fileNameField = "FileName", string fileUrlField = "FileUrl", bool imageFlag = false)
        {
            hasEditCheckFlag = true;
            hasEdit = isEdit;
            gridEx.MainGrid.SetEditable(fileNameField);

            if (!hasEdit)
            {
                gridEx.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridEx.MainGrid.MainView.Columns[fileNameField].AppearanceCell.Options.UseBackColor = false;
            }

            SetFileButtonEdit((GridView)gridEx.MainGrid.MainView, fileNameField, fileUrlField, imageFlag);
        }

        /// <summary>
        /// Grid 에서 파일을 상태값에 따라 수정 또는 읽기로 변경
        /// </summary>
        public FtpFileGridButtonEdit_BAK(bool isEdit, string uploadFilePathFieldName, string deleteFilePathFieldName, GridEx gridEx, string fileNameField = "FileName", string fileUrlField = "FileUrl", bool imageFlag = false)
        {
            hasEditCheckFlag = true;
            hasEdit = isEdit;
            UploadFilePathFieldName = uploadFilePathFieldName;
            DeleteFilePathFieldName = deleteFilePathFieldName;
            gridEx.MainGrid.SetEditable(fileNameField);

            if (!hasEdit)
            {
                gridEx.MainGrid.MainView.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridEx.MainGrid.MainView.Columns[fileNameField].AppearanceCell.Options.UseBackColor = false;
            }

            SetFileButtonEdit((GridView)gridEx.MainGrid.MainView, fileNameField, fileUrlField, imageFlag);
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
                        FileData = (byte[])FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + ViewControl.GetFocusedRowCellValue(FileUrlField).GetNullToEmpty())
                    });
                }
            }
            else if (args.Button.Kind == ButtonPredefines.Delete)
            {
                if (ViewControl.Columns["DeleteFilePath"] != null)
                {
                    ViewControl.SetFocusedRowCellValue("DeleteFilePath", ViewControl.GetFocusedRowCellValue(FileUrlField));
                }

                ViewControl.SetFocusedRowCellValue(FileNameField, null);
                ViewControl.SetFocusedRowCellValue(FileUrlField, null);
            }
            else
            {
                HKInc.Service.Handler.EventHandler.FileHolder fileHolder = HKInc.Service.Handler.FileHandler.OpenFile();

                if (fileHolder != null)
                {
                    if (ImageFlag)
                    {
                        var CheckList = new string[] { ".bmp", ".jpg", ".gif", ".png" };

                        var Extension = System.IO.Path.GetExtension(fileHolder.FULLFileName);
                        if (!CheckList.Contains(Extension.ToLower()))
                        {
                            //Handler.MessageBoxHandler.Show(Helper.MessageHelper.GetStandardMessage((int)Factory.StandardMessageEnum.M_85));
                            Handler.MessageBoxHandler.Show("이미지 파일(BMP,JPG,GIF,PNG)만 가능합니다.", "경고");
                        }
                        else
                        {
                            var fileUrl = ViewControl.GetFocusedRowCellValue(FileUrlField).GetNullToEmpty();
                            if (!fileUrl.IsNullOrEmpty())
                            {
                                if (ViewControl.Columns[DeleteFilePathFieldName] != null)
                                {
                                    ViewControl.SetFocusedRowCellValue(DeleteFilePathFieldName, fileUrl);
                                }
                            }
                            else
                            {
                                if (ViewControl.Columns[DeleteFilePathFieldName] != null)
                                {
                                    ViewControl.SetFocusedRowCellValue(DeleteFilePathFieldName, null);
                                }
                            }
                            ViewControl.SetFocusedRowCellValue(FileNameField, fileHolder.FileName);
                            ViewControl.SetFocusedRowCellValue(FileUrlField, fileHolder.FULLFileName);
                            ViewControl.SetFocusedRowCellValue(UploadFilePathFieldName, fileHolder.FULLFileName);
                            ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
                        }
                    }
                    else
                    {
                        var fileUrl = ViewControl.GetFocusedRowCellValue(FileUrlField).GetNullToEmpty();
                        if (!fileUrl.IsNullOrEmpty())
                        {
                            if (ViewControl.Columns[DeleteFilePathFieldName] != null)
                            {
                                ViewControl.SetFocusedRowCellValue(DeleteFilePathFieldName, fileUrl);
                            }
                        }
                        else
                        {
                            if (ViewControl.Columns[DeleteFilePathFieldName] != null)
                            {
                                ViewControl.SetFocusedRowCellValue(DeleteFilePathFieldName, null);
                            }
                        }
                        ViewControl.SetFocusedRowCellValue(FileNameField, fileHolder.FileName);
                        ViewControl.SetFocusedRowCellValue(FileUrlField, fileHolder.FULLFileName);
                        ViewControl.SetFocusedRowCellValue(UploadFilePathFieldName, fileHolder.FULLFileName);
                        ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
                    }
                }
            }
        }

        void DummyCallback(object sender, PopupArgument args) { }

        private void SetFileButtonEdit(GridView gv, string fileNameField, string fileUrlField, bool imageFlag)
        {
            SetButtonEdit("FileUploadButton", DummyCallback, TextEditStyles.DisableTextEditor);
            this.FileNameField = fileNameField;
            this.FileUrlField = fileUrlField;
            this.ImageFlag = imageFlag;

            ViewControl = gv;
            ViewControl.Columns[FileNameField].AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            ViewControl.Columns[FileNameField].AppearanceCell.Font = new System.Drawing.Font(ViewControl.Columns[FileNameField].AppearanceCell.Font, System.Drawing.FontStyle.Underline);

            if (hasEditCheckFlag)
            {
                if (!hasEdit)
                {
                    Buttons.Clear();
                    Buttons.Add(new EditorButton(ButtonPredefines.Search));
                }
                else
                {
                    Buttons.Add(new EditorButton(ButtonPredefines.Search));
                    Buttons.Add(new EditorButton(ButtonPredefines.Delete));
                }
            }
            else
            {
                Buttons.Add(new EditorButton(ButtonPredefines.Search));
                Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            }
        }

    }
}
