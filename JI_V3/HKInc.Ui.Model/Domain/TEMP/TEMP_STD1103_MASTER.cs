using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_STD1103_MASTER
    {

        public TEMP_STD1103_MASTER()
        { 
            //TN_STD1103List = new List<TN_STD1103>();
        }


        public string NewRowFlag { get; set; }

        public string ItemCode { get; set; }

        public string CustomerCode { get; set; }

        //public string CostManageNo { get; set; }

        //public virtual TN_STD1100 TN_STD1100 { get; set; }
        //public virtual ICollection<TN_STD1103> TN_STD1103List { get; set; }
    }
}
