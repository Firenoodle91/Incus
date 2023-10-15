using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
   // [Table("CodeSql")]
    public class CodeSql //: BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal CodeSqlId { get; set; }

        [Required(ErrorMessage = "Sql Code Name is required.")]
        public string SqlId { get; set; }

        [Required]
        public string CodeSqlName { get; set; }

        [Required(ErrorMessage = "Sql Query is required.")]
        public string Query { get; set; }

        public string Parameter { get; set; }

        [Required]
        public string FormTitle { get; set; }

        public Nullable<int> FormWith { get; set; }

        public Nullable<int> FormHeight { get; set; }

        public string DisplayField { get; set; }

        public string FindText { get; set; }

        public string KeyField { get; set; }

        public string Description { get; set; }

        public string Active { get; set; }        
    }
}
