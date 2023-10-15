using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Enum
{
    public enum PopupParameter
    {
        EditMode,
        KeyValue,
        FocusedGridColumn,
        FocusedColumnName,
        FocusedColumnIndex,
        FocusedRow,
        DataParam,
        DbContext,
        UserRight,
        Repository,
        Service,
        FormMenu,
        Constraint,
        ReturnObject,
        IsMultiSelect,

        #region Grid Row Focus 를 위한 Param
        GridRowId_1,
        GridRowId_2,
        GridRowId_3,
        GridRowId_4,
        GridRowId_5,
        #endregion

        Value_1,
        Value_2,
        Value_3,
        Value_4,
        Value_5,

    }
}
