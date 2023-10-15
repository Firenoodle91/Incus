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
using HKInc.Service.Handler.EventHandler;

namespace HKInc.Service.Controls
{
    public class FtpFileGridButtonEdit : GridButtonEdit
    {
        private bool hasEditCheckFlag = false;
        private bool hasEdit;
        private string FileNameField;
        private string FileUrlField;
        private bool ImageFlag;
        private string ftpFolderName;
        private byte[] localData = null;
        /// <summary>
        /// Grid 에서 파일을 상태값에 따라 수정 또는 읽기로 변경
        /// </summary>
        public FtpFileGridButtonEdit(bool isEdit, GridEx gridEx, string ftpFolderName, string fileNameField, string fileUrlField, bool imageFlag = false)
        {
            hasEditCheckFlag = true;
            hasEdit = isEdit;
            gridEx.MainGrid.SetEditable(fileNameField);
            this.ftpFolderName = ftpFolderName;

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
                    if (fileName.Contains("\\") && localData != null)
                    {
                        string[] filename = fileName.Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = filename[filename.Length - 1];
                            FileHandler.SaveFile(new FileHolder
                            {
                                FileName = realFileName,
                                FileData = localData
                            });
                        }
                    }
                    else
                    {
                        localData = null;
                        FileHandler.SaveFile(new FileHolder
                        {
                            FileName = fileName,
                            FileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + ftpFolderName + "/" + fileName)
                        });
                    }
                }
            }
            else if (args.Button.Kind == ButtonPredefines.Delete)
            {
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
                            //MessageBoxHandler.Show(Helper.MessageHelper.GetStandardMessage((int)Factory.StandardMessageEnum.M_85));
                            Handler.MessageBoxHandler.Show("이미지 파일(BMP,JPG,GIF,PNG)만 가능합니다.", "경고");
                        }
                        else
                        {
                            //ViewControl.SetFocusedRowCellValue(FileNameField, fileHolder.FileName);
                            ViewControl.SetFocusedRowCellValue(FileNameField, fileHolder.FULLFileName);
                            ViewControl.SetFocusedRowCellValue(FileUrlField, fileHolder.FULLFileName);
                            localData = fileHolder.FileData;
                            ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
                        }
                    }
                    else
                    {
                        ViewControl.SetFocusedRowCellValue(FileNameField, fileHolder.FULLFileName);
                        ViewControl.SetFocusedRowCellValue(FileUrlField, fileHolder.FULLFileName);
                        localData = fileHolder.FileData;
                        ((IFormControlChanged)ViewControl.GridControl.FindForm()).SetIsFormControlChanged(true);
                    }
                }
            }
        }

        /// <summary>
        /// 20210119 오세완 차장
        /// GridButtonEdit에 트리컨트롤에서 메모팝업을 출력하기 위하여 함수형을 추가하여 기본으로 구현처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonClickHandler_Tree(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// 20210321 김태영 대리
        /// 출하성적서용 풀텍스트 메모팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonClickHandlerTY(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
