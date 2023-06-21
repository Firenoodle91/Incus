﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    public class TP_MTTFCHART
    {

        public TP_MTTFCHART()
        { }


        public string MachineCode { get; set; }

        public string MM { get; set; }
        public Nullable<int> Runtime { get; set; }
        public Nullable<int> StopTime { get; set; }
        public Nullable<int> StopCnt { get; set; }
        public Nullable<decimal> FailRat { get; set; }
        public Nullable<decimal> MTBF { get; set; }
        public Nullable<decimal> MTTR { get; set; }



    }
}
