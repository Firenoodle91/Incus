using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using HKInc.Service.Handler;
using HKInc.Service.Handler.EventHandler;
using HKInc.Utils.Class;
using HKInc.Utils.Common;

namespace HKInc.Service.Handler.EventHandler
{
    public class FtpFileButtonEditClickHandler
    {
        private ButtonEdit Editor;
        private PictureEdit PictureEdit;
        private bool imageFlag = false;
        public string ftpFolderName;
        public string key1;
        public string key2;

        /// <summary>
        /// Ftp ButtonEdit Click 핸들러
        /// </summary>
        /// <param name="isSearch">읽기가능여부</param>
        /// <param name="isEdit">수정가능여부</param>
        /// <param name="buttonEdit">buttonEdit</param>
        /// <param name="imageFlag">이미지확장자체크여부</param>
        public FtpFileButtonEditClickHandler(bool isSearch, bool isEdit, ButtonEdit buttonEdit, bool imageFlag, string ftpFolderName, PictureEdit pictureEdit = null)
        {
            this.Editor = buttonEdit;
            this.Editor.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.imageFlag = imageFlag;
            this.ftpFolderName = ftpFolderName;

            this.PictureEdit = pictureEdit;

            if (isEdit)
                this.Editor.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            else
                this.Editor.Properties.Buttons.Clear();

            if (isSearch)
                this.Editor.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Search));
        }

        public void ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Search)
            {
                string fileName = this.Editor.EditValue.GetNullToEmpty();
                if (!string.IsNullOrEmpty(fileName))
                {
                    byte[] localData = this.Editor.Tag as byte[];
                    if (localData != null)
                    {
                        string[] filename = fileName.Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = filename[filename.Length - 1];
                            HKInc.Service.Handler.FileHandler.SaveFile(new FileHolder
                            {
                                FileName = realFileName,
                                FileData = localData
                            });
                        }
                    }
                    else
                    {
                        string keyPath = string.Empty;
                        if (!key2.IsNullOrEmpty())
                        {
                            keyPath = string.Format("{0}/{1}/", key1, key2);
                        }
                        else if (!key1.IsNullOrEmpty())
                        {
                            keyPath = string.Format("{0}/", key1);
                        }

                        if (keyPath.IsNullOrEmpty())
                        {
                            FileHandler.SaveFile(new FileHolder
                            {
                                FileName = fileName,
                                FileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + ftpFolderName + "/" + fileName)
                            });
                        }
                        else
                        {
                            FileHandler.SaveFile(new FileHolder
                            {
                                FileName = fileName,
                                FileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + ftpFolderName + "/" + keyPath + fileName)
                            });
                        }
                    }
                }
            }
            else if (e.Button.Kind == ButtonPredefines.Delete)
            {
                try
                {
                    this.Editor.Tag = null;
                    this.Editor.EditValue = null;

                    if (this.PictureEdit != null)
                        this.PictureEdit.EditValue = null;
                }
                catch { }
            }
            else
            {
                FileHolder fileHolder = FileHandler.OpenFile();

                if (fileHolder != null)
                {
                    if (imageFlag)
                    {
                        var CheckList = new string[] { ".bmp", ".jpg", ".gif", ".png",".pdf" };

                        var Extension = System.IO.Path.GetExtension(fileHolder.FULLFileName);
                        if (!CheckList.Contains(Extension.ToLower()))
                        {
                            //Handler.MessageBoxHandler.Show(Helper.MessageHelper.GetStandardMessageList((int)Factory.StandardMessageEnum.M_85));
                            Handler.MessageBoxHandler.Show("파일형태(BMP,JPG,GIF,PNG,PDF)만 가능합니다.", "경고");
                        }
                        else
                        {
                            this.Editor.Tag = fileHolder.FileData;
                            this.Editor.EditValue = fileHolder.FULLFileName;

                            if (this.PictureEdit != null)
                                this.PictureEdit.EditValue = fileHolder.FileData;
                        }
                    }
                    else
                    {
                        this.Editor.Tag = fileHolder.FileData;
                        this.Editor.EditValue = fileHolder.FULLFileName;

                        if (this.PictureEdit != null)
                            this.PictureEdit.EditValue = fileHolder.FileData;
                    }
                }
            }
        }

        public void ClearDeleteButton()
        {
            var d = this.Editor.Properties.Buttons.Where(p => p.Kind == ButtonPredefines.Delete).FirstOrDefault();
            d.Visible = false;
        }
    }
}
