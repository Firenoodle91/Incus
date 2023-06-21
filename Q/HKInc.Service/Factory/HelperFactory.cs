using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Service.Helper;
using HKInc.Utils.Interface.Helper;

namespace HKInc.Service.Factory
{
    public class HelperFactory
    {
        public static ILabelConvert GetLabelConvert()
        {
            return new LabelConvert();
        }

        public static IMasterCode GetMasterCode()
        {
            return new MasterCode();
        }

        public static IStandardMessage GetStandardMessage()
        {
            return new MessageHelper();
        }

        public static IDateEditMask GetDateEditMask()
        {
            return new DateEditMask();
        }

        public static IDateEditMask GetDateEditMask(Controls.DateEditEx editor)
        {
            return new DateEditMask(editor);
        }        

        public static ICodeSqlHelper GetCodeSqlHelper()
        {
            return new CodeSqlHelper();
        }

        public static IGridRowLocator GetGridRowLocator(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            return new GridRowLocator(gv);
        }
    }
}
