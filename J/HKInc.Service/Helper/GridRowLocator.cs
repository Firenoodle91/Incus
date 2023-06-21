using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraGrid.Views.Grid;

using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Context;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Class;

namespace HKInc.Service.Helper
{
    public class GridRowLocator : IGridRowLocator
    {
        private readonly GridView View;
        private int currentRow;
        private string keyFieldName;
        object keyValue;

        public GridRowLocator(GridView gv)
        {
            View = gv;
        }

        public void GetCurrentRow()
        {
            if (View.RowCount > 0)
                currentRow = View.FocusedRowHandle > 0 ? View.FocusedRowHandle : 0;
            else
                currentRow = 0;
        }

        public void SetCurrentKeyValue(string fieldName, object value)
        {
            //this.keyFieldName = fieldName;
            //this.keyValue = value;

            keyFieldName = fieldName;
            var RowObj = value as HKInc.Ui.Model.BaseDomain.MES_BaseDomain;
            if (RowObj != null)
                keyValue = RowObj.RowId;
            else
                keyValue = value;

            currentRow = View.FocusedRowHandle > 0 ? View.FocusedRowHandle : 0;
        }

        public void GetCurrentRow(string fieldName, object value)
        {
            keyFieldName = fieldName;
            var RowObj = value as HKInc.Ui.Model.BaseDomain.MES_BaseDomain;
            if (RowObj != null)
                keyValue = RowObj.RowId;
            else
                keyValue = value;

            currentRow = View.FocusedRowHandle > 0 ? View.FocusedRowHandle : 0;
        }
        public void GetCurrentRow(string fieldName)
        {
            keyFieldName = fieldName;
            if (View.RowCount > 0)
            {
                currentRow = View.FocusedRowHandle > 0 ? View.FocusedRowHandle : 0;
                keyValue = View.GetRowCellValue(currentRow, keyFieldName);
            }            
        }

        public void SetCurrentRow()
        {
            if (string.IsNullOrEmpty(keyFieldName) || keyValue == null)            
                View.FocusedRowHandle = currentRow;                     
            else            
                View.FocusedRowHandle = View.LocateByValue(keyFieldName, keyValue);   
            
            View.MakeRowVisible(View.FocusedRowHandle);
        }        
    }
}
