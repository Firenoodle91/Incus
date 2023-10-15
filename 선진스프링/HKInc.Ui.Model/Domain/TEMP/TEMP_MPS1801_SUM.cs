using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_MPS1801_SUM
    {
        public TEMP_MPS1801_SUM()
        {
        }
       
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CustomerCode { get; set; }          
        public decimal OkQty { get; set; }
    }
}