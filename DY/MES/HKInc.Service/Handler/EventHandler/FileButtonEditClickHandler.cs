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

namespace HKInc.Service.Handler.EventHandler
{
    public class FileButtonEditClickHandler
    {
        private ButtonEdit Editor;
        private PictureEdit PictureEdit;
        private bool IsNeedFullPath;
        public FileButtonEditClickHandler(ButtonEdit buttonEdit,string isDefault)
        {
            this.Editor = buttonEdit;
            this.Editor.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));

        }

        public FileButtonEditClickHandler(ButtonEdit buttonEdit, bool isReadonly = false, bool isTextEditable = false, bool isNeedFullPath = false)
        {
            this.Editor = buttonEdit;
            this.Editor.Properties.TextEditStyle = isTextEditable ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor;
            this.IsNeedFullPath = isNeedFullPath;

            if (isReadonly)
                this.Editor.Properties.Buttons.Clear();
            else
                this.Editor.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));

            this.Editor.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Search));
        }

        public FileButtonEditClickHandler(bool isSearch, ButtonEdit buttonEdit, bool isReadonly = false, bool isTextEditable = false, bool isNeedFullPath = false)
        {
            this.Editor = buttonEdit;
            this.Editor.Properties.TextEditStyle = isTextEditable ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor;
            this.IsNeedFullPath = isNeedFullPath;

            if (isReadonly)
                this.Editor.Properties.Buttons.Clear();
            else
                this.Editor.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));

            if (isSearch)
                this.Editor.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Search));
        }

        public FileButtonEditClickHandler(ButtonEdit buttonEdit, PictureEdit pictureEdit, bool isReadonly = false, bool isTextEditable = false) : this(buttonEdit, isReadonly, isTextEditable)
        {
            this.PictureEdit = pictureEdit;
        }
        public FileButtonEditClickHandler(bool isSearch, ButtonEdit buttonEdit, PictureEdit pictureEdit, bool isReadonly = false, bool isTextEditable = false) : this(isSearch, buttonEdit, isReadonly, isTextEditable)
        {
            this.PictureEdit = pictureEdit;
        }

        public void ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Search)
            {
                if (!string.IsNullOrEmpty(this.Editor.EditValue.GetNullToEmpty()))
                {
                    FileHandler.SaveFile(new FileHolder
                    {
                        FileName = this.Editor.EditValue.GetNullToEmpty(),
                        FileData = (byte[])this.Editor.Tag
                    });
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
                FileHolder fileHolder = FileHandler.OpenFile("");

                if (fileHolder != null)
                {
                    this.Editor.Tag = fileHolder.FileData;
                    this.Editor.EditValue = fileHolder.FULLFileName;

                    if (this.PictureEdit != null)
                        this.PictureEdit.EditValue = this.Editor.Tag;
                }
            }
        }
    }

}
