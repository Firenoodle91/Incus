using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{    
    [Table("ParameterInfo")]
    public class ParameterInfo
    {
        [Key]
        public int ParameterId { get; set; }

        public string ProcedureName { get; set; }
        public string ParameterName { get; set; }
        public string DataType { get; set; }
        public short MaxLength { get; set; }
        public Nullable<int> Precision { get; set; }
        public Nullable<int> Scale { get; set; }        
        public string Collation { get; set; }
        public int ObjectId { get; set; }
    }
}
