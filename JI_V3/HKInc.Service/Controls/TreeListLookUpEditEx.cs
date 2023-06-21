using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;

using DevExpress.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;

using HKInc.Utils.Class;
using HKInc.Utils.Interface.Helper;
using HKInc.Service.Factory;

namespace HKInc.Service.Controls
{
	[ToolboxItem(true)]
    public partial class TreeListLookUpEditEx : TreeListLookUpEdit
    {		
        private string mNullText = String.Empty;
        private ILabelConvert FieldNameConverter = HelperFactory.GetLabelConvert();

        #region Constructor
        public TreeListLookUpEditEx()
        {
            InitializeComponent();

            Initialize();				
        }

        private void Initialize()
        {
            this.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.Properties.NullText = "";
            this.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.Properties.TreeList.OptionsView.ShowColumns = false;
            this.Properties.TreeList.OptionsView.ShowHorzLines = false;
            this.Properties.TreeList.OptionsView.ShowVertLines = false;           
        }

        #endregion        
        public string ParentFieldName
        {
            get { return this.Properties.TreeList.ParentFieldName; }
            set { this.Properties.TreeList.ParentFieldName = value; }
        }

        public string KeyFieldName
        {
            get { return this.Properties.TreeList.KeyFieldName; }
            set { this.Properties.TreeList.KeyFieldName = value; }
        }

        public string DisplayMember
        {
            get { return this.Properties.DisplayMember; }
            set { this.Properties.DisplayMember = value; }
        }

        public string ValueMember
        {
            get { return this.Properties.ValueMember; }
            set { this.Properties.ValueMember = value; }
        }

        public object DataSource
        {
            get { return this.Properties.TreeList.DataSource; }
            set
            {
                this.Properties.TreeList.DataSource = value;
                if (value != null)
                {
                    if (!String.IsNullOrEmpty(this.mNullText) && value.GetType() == typeof(DataTable))
                    {
                        DataTable dt = (DataTable)value;
                        if (dt.Columns.Contains(ValueMember) && dt.Columns.Contains(DisplayMember))
                        {
                            DataRow firstRow = dt.NewRow();
                            firstRow[DisplayMember] = this.mNullText;
                            dt.Rows.InsertAt(firstRow, 0);
                        }
                    }

                    BestFitColumns();
                    ExpandLevel(1);
                }
            }
        }

        public bool ShowColumns
        {
            get { return this.Properties.TreeList.OptionsView.ShowColumns; }
            set { this.Properties.TreeList.OptionsView.ShowColumns = value; }
        }

        public TreeListColumnCollection Columns
        {
            get { return this.Properties.TreeList.Columns; }
        }

        public void Clear()
		{
			this.DataSource = null;
		}

		protected override void OnLoaded()
		{
			base.OnLoaded();
			AddDeleteButton();
		}

		private void AddDeleteButton()
		{
			foreach (var item in this.Properties.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == ButtonPredefines.Delete))
			{
				this.Properties.Buttons.RemoveAt(item.Index);
			}

			this.Properties.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Delete });
			this.ButtonClick += LookUpEditEx_ButtonClick;
		}

        private void LookUpEditEx_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Kind == ButtonPredefines.Delete)
			{
				this.EditValue = null;
			}
		}

		public void SetEnable(bool bEnable = false)
		{			
			Properties.AllowFocused = bEnable;
			Properties.ReadOnly = !bEnable;            

			if (Properties.Buttons.Count > 0)
			{
				foreach (EditorButton btn in Properties.Buttons)
				{
					btn.Visible = bEnable;
				}
			}
		}

        public void AddColumn(string fieldName)
        {
            AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), true);
        }
        public void AddColumn(string fieldName, bool visible)
        {
            AddColumn(fieldName, FieldNameConverter.GetLabelText(fieldName), visible);
        }
        public void AddColumn(string fieldName, string caption)
        {
            AddColumn(fieldName, caption, true);
        }
        public void AddColumn(string fieldName, string caption, bool visible)
        {
            TreeListColumn col = this.Properties.TreeList.Columns.AddField(fieldName);
            col.Caption = caption;
            col.Visible = visible;
        }

        public void BestFitColumns()
        {
            this.Properties.TreeList.BestFitColumns();
        }

        public void ExpandAll()
        {
            this.Properties.TreeList.ExpandAll();
        }

        public void ExpandLevel(int level)
        {
            foreach (TreeListNode node in this.Properties.TreeList.Nodes)
            {
                if (node.Level < level)
                {
                    node.Expanded = true;
                    if (node.HasChildren)
                    {
                        ExpandLowLevel(node, level);
                    }
                }
                else
                {
                    node.Expanded = false;
                }
            }
        }
        private void ExpandLowLevel(TreeListNode uNode, int level)
        {
            foreach (TreeListNode node in uNode.Nodes)
            {
                if (node.Level < level)
                {
                    node.Expanded = true;
                    if (node.HasChildren)
                    {
                        ExpandLowLevel(node, level);
                    }
                }
                else
                {
                    node.Expanded = false;
                }
            }
        }        
    }
}
