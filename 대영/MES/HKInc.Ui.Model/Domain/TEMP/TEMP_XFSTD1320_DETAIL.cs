using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20211101 오세완 차장 
    /// 프로시저 USP_GET_XFSTD1302_DETAIL 반환 객체
    /// </summary>
    public class TEMP_XFSTD1320_DETAIL
    {
        public TEMP_XFSTD1320_DETAIL()
        {

        }

        public string TYPE_CODE { get; set; }

        public int SEQ { get; set; }

        public int LEVEL { get; set; }

        public string ItemCode { get; set; }

        public string ITEM_NAME { get; set; }

        public string ITEM_NAME1 { get; set; }

        public string TOP_CATEGORY { get; set; }

        public string TOP_CATEGORY_NAME { get; set; }

        public string MIDDLE_CATEGORY { get; set; }

        public string MIDDLE_CATEGORY_NAME { get; set; }

        public string BOTTOM_CATEGORY { get; set; }

        public string BOTTOM_CATEGORY_NAME { get; set; }

        public string SPEC_1 { get; set; }

        public string SPEC_2 { get; set; }

        public string SPEC_3 { get; set; }

        public string SPEC_4 { get; set; }

        public string UNIT { get; set; }

        public string UNIT_NAME { get; set; }

        public decimal? WEIGHT { get; set; }

        public decimal? USE_QTY { get; set; }

        public string PROCESS_CODE { get; set; }

        public string MG_FLAG { get; set; }

        public string USE_FLAG { get; set; }

        public string MEMO { get; set; }

        public int DISPLAY_ORDER { get; set; }

        public string Type { get; set; }

        public string PARENT_ITEM_CODE { get; set; }
    }
}
