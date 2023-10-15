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
    /// 재입고라벨출력
    /// </summary>
    public class ReturnInLabelTemp
    {
        public int? Qty { get; set; }
        public DateTime? Date { get; set; }
        public string WhCode { get; set; }
        public string WhPosition { get; set; }
    }
}