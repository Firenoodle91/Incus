using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("SystemMenuLog")]
    public class MenuEventLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RowId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public int SearchCnt { get; set; }
        public int AddCnt { get; set; }
        public int UpdateCnt { get; set; }
        public int DeleteCnt { get; set; }
    }
}
