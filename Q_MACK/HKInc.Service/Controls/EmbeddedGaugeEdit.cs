using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraGauges.Win;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
namespace HKInc.Service.Controls
{
    public class EmbeddedGaugeEdit
    {
        public static void CreateGaugeEdit(GridColumn column, GaugeControl gauge)
        {
            RepositoryItemAnyControl ri = new RepositoryItemAnyControl();
            ri.Control = gauge;
            column.View.GridControl.RepositoryItems.Add(ri);
            column.ColumnEdit = ri;
        }
    }
}