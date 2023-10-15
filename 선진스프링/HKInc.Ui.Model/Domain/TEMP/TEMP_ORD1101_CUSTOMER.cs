using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_ORD1101_CUSTOMER
    {
        public TEMP_ORD1101_CUSTOMER()
        {
        }
    
        public string OutRepNo { get; set; }
        public string CustomerName { get; set; }
    }
}