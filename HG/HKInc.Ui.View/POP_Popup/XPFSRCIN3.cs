using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using System.Reflection;
using DevExpress.XtraEditors.Popup;

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFSRCIN3 : XtraForm
    {
        IService<VI_PURSTOCK_LOT> ModelService = (IService<VI_PURSTOCK_LOT>)ProductionFactory.GetDomainService("VI_PURSTOCK_LOT");
        public string[] returnval  ;
        public TP_POPJOBLIST TP_POPJOB;

        string lotno1 = null;
        string item1 = null;

        public XPFSRCIN3()
        {
            InitializeComponent();
            this.Text = "원소재 및 칼 투입처리";
            lupKnifeCode1.ButtonPressed += LupKnifeCode_ButtonPressed;
            //lupKnifeCode2.ButtonPressed += LupKnifeCode_ButtonPressed;
            //lupKnifeCode3.ButtonPressed += LupKnifeCode_ButtonPressed;
            //lupKnifeCode4.ButtonPressed += LupKnifeCode_ButtonPressed;

            lupKnifeCode1.Popup += LupKnifeCode_Popup;
            //lupKnifeCode2.Popup += LupKnifeCode_Popup;
            //lupKnifeCode3.Popup += LupKnifeCode_Popup;
            //lupKnifeCode4.Popup += LupKnifeCode_Popup;

            lupKnifeCode1.EditValueChanged += LupKnifeCode1_EditValueChanged;

            var KnifeList = ModelService.GetChildList<TN_KNIFE001>(p => p.UseYN == "Y").ToList();
            //lupKnifeCode1.SetDefault(true, "KnifeCode", "KnifeName", KnifeList);            
            //lupKnifeCode2.SetDefault(true, "KnifeCode", "KnifeName", KnifeList);
            //lupKnifeCode3.SetDefault(true, "KnifeCode", "KnifeName", KnifeList);
            //lupKnifeCode4.SetDefault(true, "KnifeCode", "KnifeName", KnifeList);
            searchLookupEditSetDefault(lupKnifeCode1, true, "KnifeCode", "KnifeName", KnifeList);
            //searchLookupEditSetDefault(lupKnifeCode2, true, "KnifeCode", "KnifeName", KnifeList);
            //searchLookupEditSetDefault(lupKnifeCode3, true, "KnifeCode", "KnifeName", KnifeList);
            //searchLookupEditSetDefault(lupKnifeCode4, true, "KnifeCode", "KnifeName", KnifeList);

            //Service.Handler.ImeHandler.SetAlpahNumeric(this.Handle);
            //IntPtr context = ImeGetContext(this.Handle);
            //Int32 dwConversion = 0;
            //dwConversion = IME_CMODE_ALPHANUMERIC;//영문자판설정
            //ImeSetConversionStatus(context, dwConversion, 0);

            tx_srcin1.ImeMode = ImeMode.Disable;
        }
        
        private void XPFSRCIN3_Load(object sender, EventArgs e)
        {
            var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TP_POPJOB.ItemCode).First();
            lupKnifeCode1.EditValue = ItemObj.KnifeCode.IsNullOrEmpty() ? null : ItemObj.KnifeCode;
        }

        private void tx_srcin_KeyDown(object sender, KeyEventArgs e)
       {
            TextEdit tx = sender as TextEdit;
            if (e.KeyCode != Keys.Enter) return;
            if (tx.Text.IsNullOrEmpty()) return;

            string idx = tx.Name.ToString().Substring(tx.Name.ToString().Length-1,1);            
            SrcInfo(tx.Text,idx);
        }

        private void SrcInfo(string tx,string idx)
        {
            //VI_PURSTOCK_LOT obj = ModelService.GetList(p => p.Temp2 == tx_srcin.Text).OrderBy(o => o.Temp2).FirstOrDefault();
            DataTable dt = DbRequesHandler.GetDTselect("exec sp_srcin '" + tx + "'");
            
            if (dt == null)
            {
                MessageBox.Show("사용 가능한 원소재가 없습니다.", "경고");          
            }
            else
            {                
                switch (idx)
                {
                    case "1":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname1.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty1.Text = dt.Rows[0]["qty"].ToString();
                            //lbitem1.Text = dt.Rows[0]["ItemCode"].ToString();
                            //lb_lotno1.Text = dt.Rows[0]["OutLot"].ToString();
                            item1 = dt.Rows[0]["ItemCode"].ToString();
                            lotno1 = dt.Rows[0]["OutLot"].ToString();
                            //tx_srcin2.Focus();
                        }
                        else
                        {
                            MessageBox.Show("사용 가능한 원소재가 없습니다.", "경고");
                            tx_srcname1.Text = "";
                            tx_srcqty1.Text = "";
                            //lbitem1.Text = "";
                            //lb_lotno1.Text = "";
                            item1 = null;
                            lotno1 = null;
                            tx_srcin1.Focus();
                        }
                    break;
                }            
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tx_srcname1.Text == "") {
                MessageBox.Show("원소재를 확인하세요");
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            returnval = new string[16];
            returnval[0] = item1;
            returnval[1] = lotno1;
            returnval[8] = lupKnifeCode1.EditValue.GetNullToEmpty() == string.Empty ? null : lupKnifeCode1.EditValue.GetNullToEmpty();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void tx_srcin_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            tx_srcin1.Text = keypad.returnval;
            tx_srcin_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        private void LupKnifeCode_Popup(object sender, EventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            var popupForm = edit.GetPopupEditForm();
            popupForm.KeyPreview = true;
            popupForm.KeyUp -= popupForm_KeyUp;
            popupForm.KeyUp += popupForm_KeyUp;

            DevExpress.Utils.Win.IPopupControl popupControl = sender as DevExpress.Utils.Win.IPopupControl;
            DevExpress.XtraEditors.Popup.PopupSearchLookUpEditForm window = popupControl.PopupWindow as DevExpress.XtraEditors.Popup.PopupSearchLookUpEditForm;
            DevExpress.XtraGrid.Editors.SearchEditLookUpPopup popup = window.Controls.OfType<DevExpress.XtraGrid.Editors.SearchEditLookUpPopup>().FirstOrDefault();
            TextEdit find = popup.FindTextBox;
            if (find != null)
            {
                find.ImeMode = ImeMode.Disable;
            }
        }

        void popupForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            var popupForm = sender as PopupSearchLookUpEditForm;
            if (e.KeyData == System.Windows.Forms.Keys.Enter)
            {
                GridView view = popupForm.OwnerEdit.Properties.View;
                view.FocusedRowHandle = 0;
                popupForm.OwnerEdit.ClosePopup();
            }
        }

        private void LupKnifeCode_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var searchLookup = sender as SearchLookUpEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                searchLookup.EditValue = null;

                //Change the field via reflection
                FieldInfo field = typeof(SearchLookUpEdit).GetField("fOldEditValue", BindingFlags.Instance | BindingFlags.NonPublic);

                if (field != null)
                    field.SetValue(searchLookup, searchLookup.EditValue);
            }
        }

        private void searchLookupEditSetDefault(SearchLookUpEdit searchLookUp, bool bDeleteButtonVisible = true, string valueMember = "CodeId", string displayMember = "CodeName", object dataSource = null, TextEditStyles textEditStyles = TextEditStyles.Standard, HorzAlignment horz = HorzAlignment.Near, EventHandler PopupEvent = null)
        {
            if (dataSource != null) searchLookUp.Properties.DataSource = dataSource;
            searchLookUp.Properties.DisplayMember = displayMember;
            searchLookUp.Properties.ValueMember = valueMember;
            searchLookUp.Properties.PopulateViewColumns();

            foreach(var v in searchLookUp.Properties.View.Columns)
            {
                var c = v as DevExpress.XtraGrid.Columns.GridColumn;
                if (c.FieldName != valueMember && c.FieldName != displayMember)
                {
                    searchLookUp.Properties.View.Columns[c.FieldName].Visible = false;                    
                }
            }

            searchLookUp.Properties.View.Appearance.Row.Font = lupKnifeCode1.Font;
            searchLookUp.Properties.View.OptionsView.ShowColumnHeaders = false;

            //#region SetHAlignment
            //GridView gridView = new GridView();
            //gridView.Appearance.Row.Options.UseTextOptions = true;
            //gridView.Appearance.Row.TextOptions.HAlignment = horz;
            //gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            //gridView.Appearance.HeaderPanel.TextOptions.HAlignment = horz;
            //gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            //gridView.Name = "gridView";
            //gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            //gridView.OptionsView.ShowGroupPanel = false;
            //gridView.OptionsView.ShowIndicator = false;
            //searchLookUp.Properties.PopupView = gridView;
            //searchLookUp.Properties.Appearance.TextOptions.HAlignment = horz;
            //searchLookUp.Properties.AppearanceDropDown.TextOptions.HAlignment = horz;
            //#endregion

            //searchLookUp.Properties.BestFitMode = BestFitMode.BestFit;
            searchLookUp.Properties.View.OptionsBehavior.AllowIncrementalSearch = true;
            searchLookUp.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            //searchLookUp.Properties.PopupFormSize = new Size(this.Width, 150);
            searchLookUp.Properties.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            searchLookUp.Properties.ValueMember = valueMember;
            searchLookUp.Properties.DisplayMember = displayMember;
            searchLookUp.Properties.View.OptionsView.ShowColumnHeaders = false;
            //searchLookUp.Properties.TextEditStyle = textEditStyles;

            //searchLookUp.Properties.PopupFilterMode = PopupFilterMode.Contains;
            searchLookUp.Properties.ImmediatePopup = true;

            foreach (var item in searchLookUp.Properties.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == ButtonPredefines.Delete))
            {
                searchLookUp.Properties.Buttons[item.Index].Visible = false;
            }
            if (!searchLookUp.Enabled)
            {
                foreach (var item in searchLookUp.Properties.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == ButtonPredefines.Combo))
                {
                    searchLookUp.Properties.Buttons[item.Index].Visible = false;
                }
            }

            if (bDeleteButtonVisible)
            {
                if (searchLookUp.Enabled)
                {
                    if (!searchLookUp.ReadOnly)
                    {
                        foreach (var item in searchLookUp.Properties.Buttons.OfType<EditorButton>().ToList().Where(button => button.Kind == ButtonPredefines.Delete))
                        {
                            searchLookUp.Properties.Buttons.RemoveAt(item.Index);
                        }

                        searchLookUp.Properties.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Delete });
                    }
                }
            }

        }

        private void LupKnifeCode1_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            var value = lookup.EditValue.GetNullToEmpty();
            var obj = ModelService.GetChildList<TN_KNIFE001>(p => p.KnifeCode == value).FirstOrDefault();
            if (obj != null)
            {
                textEdit1.EditValue = obj.RealShotcnt.GetDecimalNullToZero();
            }
        }
    }
}
