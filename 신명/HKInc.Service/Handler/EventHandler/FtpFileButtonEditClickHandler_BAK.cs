using System;
using System.Collections.Generic;
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
    public class FtpFileButtonEditClickHandler_BAK
    {
        private ButtonEdit Editor;
        private PictureEdit PictureEdit;
        private bool imageFlag = false;
        public string ftpFolderName;
        public string key1;
        public string key2;
        public string key3;
        public string key4;
        public string key5;

        /// <summary>
        /// Ftp ButtonEdit Click 핸들러
        /// </summary>
        /// <param name="isSearch">읽기가능여부</param>
        /// <param name="isEdit">수정가능여부</param>
        /// <param name="buttonEdit">buttonEdit</param>
        /// <param name="imageFlag">이미지확장자체크여부</param>
        /// <param name="ftpFolderName">부모폴더명</param>
        /// <param name="key1">세부폴더명1</param>
        /// <param name="key2">세부폴더명2</param>
        /// <param name="key3">세부폴더명3</param>
        /// <param name="key4">세부폴더명4</param>
        /// <param name="key5">세부폴더명5</param>
        public FtpFileButtonEditClickHandler_BAK(bool isSearch, bool isEdit, ButtonEdit buttonEdit, bool imageFlag, string ftpFolderName, string key1 = null, string key2 = null, string key3 = null, string key4 = null, string key5 = null)
        {
            this.Editor = buttonEdit;
            this.Editor.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.ftpFolderName = ftpFolderName;
            this.key1 = key1;
            this.key2 = key2;
            this.key3 = key3;
            this.key4 = key4;
            this.key5 = key5;
            this.imageFlag = imageFlag;

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
                    HKInc.Service.Handler.FileHandler.SaveFile(new FileHolder
                    {
                        FileName = fileName,
                        FileData = (byte[])FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + this.Editor.Tag.GetNullToEmpty())
                    });
                }
            }
            else if (e.Button.Kind == ButtonPredefines.Delete)
            {
                try
                {
                    var DeleteFileName = this.Editor.EditValue.GetNullToEmpty();
                    if (DeleteFileName.IsNullOrEmpty()) return;
                    SetDelete(DeleteFileName);
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
                        var CheckList = new string[] { ".bmp", ".jpg", ".gif", ".png" };

                        var Extension = System.IO.Path.GetExtension(fileHolder.FULLFileName);
                        if (!CheckList.Contains(Extension.ToLower()))
                        {
                            //Handler.MessageBoxHandler.Show(Helper.MessageHelper.GetStandardMessageList((int)Factory.StandardMessageEnum.M85));
                            Handler.MessageBoxHandler.Show("이미지 파일(BMP,JPG,GIF,PNG)만 가능합니다.", "경고");
                        }
                        else
                        {
                            SetUpload(fileHolder.FULLFileName, fileHolder.FileName);
                        }
                    }
                    else
                    {
                        SetUpload(fileHolder.FULLFileName, fileHolder.FileName);
                    }
                }
            }
        }

        private void SetUpload(string fileUrl, string fileName)
        {

            //if (!key5.IsNullOrEmpty())
            //{
            //    FileHandler.UploadFTP(fileUrl, string.Format("{0}{1}/{2}/{3}/{4}/{5}/{6}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2, key3, key4, key5));
            //    this.Editor.Tag = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", ftpFolderName, key1, key2, key3, key4, key5, fileName);
            //}
            //else if (!key4.IsNullOrEmpty())
            //{
            //    FileHandler.UploadFTP(fileUrl, string.Format("{0}{1}/{2}/{3}/{4}/{5}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2, key3, key4));
            //    this.Editor.Tag = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", ftpFolderName, key1, key2, key3, key4, fileName);
            //}
            //else if (!key3.IsNullOrEmpty())
            //{
            //    FileHandler.UploadFTP(fileUrl, string.Format("{0}{1}/{2}/{3}/{4}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2, key3));
            //    this.Editor.Tag = string.Format("{0}/{1}/{2}/{3}/{4}", ftpFolderName, key1, key2, key3, fileName);
            //}
            //else if (!key2.IsNullOrEmpty())
            //{
            //    FileHandler.UploadFTP(fileUrl, string.Format("{0}{1}/{2}/{3}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2));
            //    this.Editor.Tag = string.Format("{0}/{1}/{2}/{3}", ftpFolderName, key1, key2, fileName);
            //}
            //else if (!key1.IsNullOrEmpty())
            //{
            //    FileHandler.UploadFTP(fileUrl, string.Format("{0}{1}/{2}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1));
            //    this.Editor.Tag = string.Format("{0}/{1}/{2}", ftpFolderName, key1, fileName);
            //}
            //else
            //{
            //    FileHandler.UploadFTP(fileUrl, string.Format("{0}{1}/", GlobalVariable.FTP_SERVER, ftpFolderName));
            //    this.Editor.Tag = string.Format("{0}/{1}", ftpFolderName, fileName);
            //}

            this.Editor.EditValue = fileName;
        }

        private void SetDelete(string fileName)
        {
            if (!key5.IsNullOrEmpty())
                FileHandler.DeleteFTP(fileName, string.Format("{0}{1}/{2}/{3}/{4}/{5}/{6}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2, key3, key4, key5));
            else if (!key4.IsNullOrEmpty())
                FileHandler.DeleteFTP(fileName, string.Format("{0}{1}/{2}/{3}/{4}/{5}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2, key3, key4));
            else if (!key3.IsNullOrEmpty())
                FileHandler.DeleteFTP(fileName, string.Format("{0}{1}/{2}/{3}/{4}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2, key3));
            else if (!key2.IsNullOrEmpty())
                FileHandler.DeleteFTP(fileName, string.Format("{0}{1}/{2}/{3}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1, key2));
            else if (!key1.IsNullOrEmpty())
                FileHandler.DeleteFTP(fileName, string.Format("{0}{1}/{2}/", GlobalVariable.FTP_SERVER, ftpFolderName, key1));
            else
                FileHandler.DeleteFTP(fileName, string.Format("{0}{1}/", GlobalVariable.FTP_SERVER, ftpFolderName));

            this.Editor.Tag = null;
            this.Editor.EditValue = null;

            if (this.PictureEdit != null)
                this.PictureEdit.EditValue = null;
        }

        public void ClearDeleteButton()
        {
            var d = this.Editor.Properties.Buttons.Where(p => p.Kind == ButtonPredefines.Delete).FirstOrDefault();
            d.Visible = false;
        }
    }
}
