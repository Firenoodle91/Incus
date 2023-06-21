using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
    [Table("CodeMaster")]
    public class CodeMaster :BaseDomain.BaseDomain3
    {
        public CodeMaster()
        {
            CodeMasterDetailList = new List<CodeMaster>();            
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal CodeId { get; set; }

        public Nullable<decimal> CodeGroup { get; set; }
        public string GroupDescription { get; set; }

        [Required(ErrorMessage = "Code name is required.")]
        public string CodeName { get; set; }

        public string CodeName2 { get; set; }
        public string CodeName3 { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
        public string Property6 { get; set; }
        public string Property7 { get; set; }
        public string Property8 { get; set; }
        public string Property9 { get; set; }
        public string Property10 { get; set; }
        public string Active { get; set; }
        public Nullable<System.DateTime> DeActiveDate { get; set; }
                       
        public virtual ICollection<CodeMaster> CodeMasterDetailList { get; set; }

        [ForeignKey("CodeGroup")]
        public virtual CodeMaster GroupCodeMaster { get; set; }
    }
}
