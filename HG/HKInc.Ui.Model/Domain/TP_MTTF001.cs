using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    public class TP_MTTF001
    {

        public TP_MTTF001()
        { }


        public string MachineCode { get; set; }

        public string SEQ { get; set; }

        public string PType { get; set; }

        public Nullable<decimal> M01 { get; set; }
        public Nullable<decimal> M02 { get; set; }
        public Nullable<decimal> M03 { get; set; }
        public Nullable<decimal> M04 { get; set; }
        public Nullable<decimal> M05 { get; set; }
        public Nullable<decimal> M06 { get; set; }
        public Nullable<decimal> M07 { get; set; }
        public Nullable<decimal> M08 { get; set; }
        public Nullable<decimal> M09 { get; set; }
        public Nullable<decimal> M10 { get; set; }
        public Nullable<decimal> M11 { get; set; }
        public Nullable<decimal> M12 { get; set; }
        public Nullable<decimal> MTOTAL { get; set; }


    }
}
