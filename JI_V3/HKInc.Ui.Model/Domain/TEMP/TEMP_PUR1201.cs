using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_PUR1201
    {
        /// <summary>입고번호</summary>               
        public string InNo { get; set; }
        /// <summary>입고순번</summary>               
        public int InSeq { get; set; }
        /// <summary>발주번호</summary>               
        public string PoNo { get; set; }
        /// <summary>발주순번</summary>               
        public int? PoSeq { get; set; }
        /// <summary>품번(도번)</summary>             
        public string ItemCode { get; set; }
        /// <summary>입고량</summary>                 
        public decimal InQty { get; set; }
        /// <summary>입고단가</summary>               
        public decimal? InCost { get; set; }
        /// <summary>입고 LOT NO</summary>            
        public string InLotNo { get; set; }
        /// <summary>납품처 LOT NO</summary>          
         public string InCustomerLotNo { get; set; }
        /// <summary>입고창고</summary>               
        public string InWhCode { get; set; }
        /// <summary>입고위치</summary>               
         public string InWhPosition { get; set; }
        /// <summary>라벨수</summary>                 
         public int? PrintQty { get; set; }
        /// <summary>입고확정여부</summary>                   
        public string InConfirmFlag { get; set; }
        /// <summary>메모</summary>                   
        public string Memo { get; set; }
        /// <summary>협력사메모</summary>                
        public string Memo1 { get; set; }
        /// <summary>입고 LOT NO(모)</summary>                   
        public string Temp { get; set; }
        /// <summary>임시1</summary>                  
       public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
         public string Temp2 { get; set; }
        /// <summary>미입고확정여부</summary>                   
         public string NotInConfirmFlag { get; set; }
        /// <summary>파일명</summary>                    
         public string FileName { get; set; }
        /// <summary>파일URL</summary>                   
         public string FileUrl { get; set; }
        /// <summary>수입검사여부</summary>                  
       public string InInspectionState { get; set; }
        /// <summary>재출고 여부(자동차감)</summary>                  
       public string ReOutYn { get; set; }
    }
}
