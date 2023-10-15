using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XPFMACHINECHECK_LIST
    { 
        /// <summary>설비코드</summary>            
        public string MachineCode { get; set; }
        /// <summary>점검순번</summary>            
        public int CheckSeq { get; set; }
        /// <summary>점검위치</summary>            
        public string CheckPosition { get; set; }
        /// <summary>점검항목</summary>            
        public string CheckList { get; set; }
        /// <summary>점검방법</summary>            
        public string CheckWay { get; set; }
        /// <summary>점검주기</summary>            
        public string CheckCycle { get; set; }
        /// <summary>점검기준일</summary>          
        public string CheckStandardDate { get; set; }
        /// <summary>관리기준</summary>            
        public string ManagementStandard { get; set; }
        /// <summary>표시순서</summary>            
        public int? DisplayOrder { get; set; }
        /// <summary>메모</summary>                
        public string Memo { get; set; }
        /// <summary>임시</summary>                
        public string Temp { get; set; }
        /// <summary>임시1</summary>               
        public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        public string Temp2 { get; set; }

        public string CreateId { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateId { get; set; }

        public DateTime? UpdateTime { get; set; }

        public decimal RowId { get; set; }

        public DateTime? LastCheckDate { get; set; }

        public string CheckValue { get; set; }
    }
}
