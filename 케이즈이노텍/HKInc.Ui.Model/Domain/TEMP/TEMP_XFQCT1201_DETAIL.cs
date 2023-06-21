using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFQCT1201_DETAIL
    {
        public string WorkNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemName1 { get; set; }
        public string ProcessCode { get; set; }
        public int ProcessSeq { get; set; }
        public string ProductLotNo { get; set; }
        public DateTime? ResultDate { get; set; }
        public decimal? ResultQty { get; set; }
        public decimal? OkQty { get; set; }
        public decimal? BadQty { get; set; }
        public decimal? Heat { get; set; }
        public decimal? Rpm { get; set; }
        public string WorkId { get; set; }
        public string MachineCode { get; set; }
        public string SrcItemCode { get; set; }
        public string SrcItemName { get; set; }
        public string SrcItemName1 { get; set; }
        public string SrcInLotNo { get; set; }
        public string OutProcFlag { get; set; }
        public string CustomerLotNo { get; set; }

    }
}
