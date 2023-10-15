using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 금형등급관리평가 DETAIL
    /// </summary>
    public class TEMP_XFMOLD1700_DETAIL
    {
        public DateTime RevisionDate { get; set; }
        public string GradeManageNo { get; set; }
        public string EvaluationItem { get; set; }
        public Decimal MoldMin { get; set; }
        public Decimal MoldMax { get; set; }
    }
}
