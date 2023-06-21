using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain.VIEW
{
    public class TP_SPC_LIST
    {

        public TP_SPC_LIST()
        {

        }

        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Process { get; set; }
        public string FME_NO { get; set; }
        public Nullable<int> Seq { get; set; }
    }
}
