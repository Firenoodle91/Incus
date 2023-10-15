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
    /// 부품이동표관리 디테일 TEMP
    /// </summary>
    public class TP_MPS1900_Detail
    {
        public string ProcessName { get; set; }
        public decimal? OkQty { get; set; }
        public string WorkId { get; set; }
        public DateTime? WorkDate { get; set; }
    }
}
