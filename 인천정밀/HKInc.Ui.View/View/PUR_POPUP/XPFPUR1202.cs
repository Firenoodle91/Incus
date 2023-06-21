using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Class;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace HKInc.Ui.View.View.PUR_POPUP
{
    public partial class XPFPUR1202 : XtraForm
    {
        GridHitInfo down = null;
        Utils.Interface.Helper.ILabelConvert labelConvert = Service.Factory.HelperFactory.GetLabelConvert();
        BindingSource bindingSource = new BindingSource();
        List<XPFPUR1202_TEMP> tempList = new List<XPFPUR1202_TEMP>();
        public XPFPUR1202()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            this.Text = labelConvert.GetLabelText("CustomerLotNoScan");

            lcScanInfo.Text = labelConvert.GetLabelText("ScanInfo");
            lcCustomerLotNo.Text = labelConvert.GetLabelText("InCustomerLotNo");
            lcCustomerLotNoScanList.Text = labelConvert.GetLabelText("CustomerLotNoScanList");

            gridEx1.SetToolbarVisible(false);
            gridEx1.MainGrid.AddColumn("CustomerLotNo", labelConvert.GetLabelText("InCustomerLotNo"), HorzAlignment.Center, true);

            this.Load += XPFPUR1202_Load;
            tx_CustomerLotNo.KeyDown += Tx_CustomerLotNo_KeyDown;

            gridEx1.MainGrid.AllowDrop = true;
            gridEx1.MainGrid.DragOver += new System.Windows.Forms.DragEventHandler(grid_DragOver);
            gridEx1.MainGrid.DragDrop += new System.Windows.Forms.DragEventHandler(grid_DragDrop);
            gridEx1.MainGrid.MainView.MouseDown += MainView_MouseDown;
            gridEx1.MainGrid.MainView.MouseMove += MainView_MouseMove;
        }

        private void XPFPUR1202_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = tempList.ToList();
            gridEx1.DataSource = bindingSource;
            gridEx1.BestFitColumns();
            tx_CustomerLotNo.Focus();

        }

        private void Tx_CustomerLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var customerLotNo = tx_CustomerLotNo.EditValue.GetNullToEmpty().ToUpper();
                    if (customerLotNo.IsNullOrEmpty()) return;

                    var bindingList = bindingSource.List as List<XPFPUR1202_TEMP>;
                    if (bindingList.Any(p => p.CustomerLotNo == customerLotNo)) return;

                    bindingSource.Add(new XPFPUR1202_TEMP()
                    {
                        CustomerLotNo = customerLotNo
                    });
                    gridEx1.BestFitColumns();
                }
                finally
                {
                    tx_CustomerLotNo.EditValue = null;
                    tx_CustomerLotNo.Focus();
                }
            }
        }

        private void grid_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            //if (e.Data.GetDataPresent(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)))
            //{
            //    e.Effect = DragDropEffects.Move;

            //    // *********** Added Code ******** (start) ************************************
            //    //GridHitInfo srcHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
            //    //if (srcHitInfo == null)
            //    //{
            //    //    lblID_Value.Text = "Unable to obtain the row number.";
            //    //    return;
            //    //}
            //    //int sourceRow = srcHitInfo.RowHandle;
            //    //lblID_Value.Text = "It came from row number " + sourceRow.ToString();
            //    // *********** Added Code ******** (end) **************************************
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}
        }

        private void grid_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //GridControl grid = sender as GridControl;
            //DataTable table = grid.DataSource as DataTable;
            //DataRow row = e.Data.GetData(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)) as DataRow;
            //if (row != null && table != null && row.Table != table)
            //{
            //    table.ImportRow(row);
            //    //row.Delete();
            //}
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            var view = sender as GridView;
            down = null;

            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));

            if (Control.ModifierKeys != Keys.None)
                return;

            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                down = hitInfo;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Button == MouseButtons.Left && down != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(down.HitPoint.X - dragSize.Width / 2, down.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(view.GetFocusedRowCellValue("CustomerLotNo"), DragDropEffects.Move);
                    down = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

    }
}