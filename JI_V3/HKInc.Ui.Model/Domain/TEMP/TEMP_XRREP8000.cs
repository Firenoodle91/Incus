using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XRREP8000
    {
        /// <summary>설비코드</summary>
        public string MachineCode { get; set; }

        /// <summary>설비명</summary>
        public string MachineName { get; set; }

        /// <summary>품목코드</summary>
        public string ItemCode { get; set; }

        /// <summary>품명</summary>
        public string ItemName { get; set; }

        /// <summary>비가동시간(분)</summary>
        public int? StopMiniute { get; set; }

        /// <summary>비가동유형</summary>
        public string StopCode { get; set; }
       
    }
}
