using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_HOLIDAY")]
    public class Holiday
    {
        [Key]
        public DateTime Date { get; set; }

        [Column("HOLIDAY_FLAG")]
        public string HolidayFlag { get; set; }

        [Column("OVERTIME_FLAG")]
        public string OvertimeFlag { get; set; }

        [Column("WORK_TIME")]
        public int? WorkTime { get; set; }

        [Column("MEMO")]
        public string Memo { get; set; }

        [NotMapped]
        public DateTime DayOfWeek { get { return Date; } }
    }
}
