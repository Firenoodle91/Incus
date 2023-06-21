using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFMACHINE_STATUS
    {
        public Int64 RowId { get; set; }

        public string MachineState { get; set; }
        public string MachineStateColor { get; set; }
        public string MachineGroup { get; set; }
        public string MachineGroupName { get; set; }
        public string MachineMCode { get; set; }
        public string MachineName { get; set; }

        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CarType { get; set; }


        public string WorkNo { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }

        public string ProductLotNo  { get; set; }
        public string CustomerCode  { get; set; }
        public string CustomerName { get; set; }

        public Int32 OperationTime { get; set; }
        public Int32 StopTime { get; set; }
        public string ResultStartDate { get; set; }


        public decimal OkQty { get; set; }
        public decimal BadQty { get; set; }

    }
}
