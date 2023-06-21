using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_GRID_LAYOUT")]
    public class GridLayout
    {
        [Key, Column("LOGIN_ID", Order = 0), Required(ErrorMessage = "LoginId")]
        public string LoginId { get; set; }

        [Key, Column("NAME_SPACE", Order = 1), Required(ErrorMessage = "NameSpace")]
        public string NameSpace { get; set; }

        [Key, Column("FORM_NAME", Order = 2), Required(ErrorMessage = "FormName")]
        public string FormName { get; set; }

        [Column("GRID_NAME", Order = 3), Required(ErrorMessage = "GridName")]
        public string GridName { get; set; }

        [Column("LAYOUT_DATA"), Required(ErrorMessage = "LayoutData")]
        public string LayoutData { get; set; }

        [Column("MEMO")]
        public string Memo { get; set; }

        [Column("INS_DATE"), Required(ErrorMessage = "CreateTime")]
        public DateTime CreateTime { get; set; }

        [Column("INS_ID"), Required(ErrorMessage = "CreateId")]
        public string CreateId { get; set; }

        [Column("UPD_DATE")]
        public DateTime? UpdatetTime { get; set; }

        [Column("UPD_ID")]
        public string UpdateId { get; set; }
    }
}
