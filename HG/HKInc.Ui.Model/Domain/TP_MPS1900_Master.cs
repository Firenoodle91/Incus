using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 부품이동표관리 마스터 TEMP
    /// </summary>
    public  class TP_MPS1900_Master
    {
        public string MoveNo { get; set; }
        public string WorkNo { get; set; }
        public string LotNo { get; set; }
        public string ItemNm1 { get; set; }
        public string ItemNm { get; set; }
        public string CustomerName { get; set; }
        public string OrderNo { get; set; }
        public string Memo { get; set; }
    }
}
