using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>자재재고관리 디테일</summary>	
    public class TEMP_PUR_STOCK_DETAIL
    {
        public TEMP_PUR_STOCK_DETAIL()
        {

        }
        /// <summary>구분</summary>                 
        public string Division { get; set; }
        /// <summary>생성시간</summary>                 
        public DateTime? CreateTime { get; set; }
        /// <summary>입/출고일</summary>                 
        public DateTime? InOutDate { get; set; }
        /// <summary>품목코드</summary>           
        public string ItemCode { get; set; }
        /// <summary>입/출고 LOT NO</summary>             
        public string InOutLotNo { get; set; }
        /// <summary>입고량</summary>                 
        public decimal? InQty { get; set; }

        /// <summary>출고량</summary>                 
        public decimal? OutQty { get; set; }

        /// <summary>입/출고자</summary>             
        public string InOutId { get; set; }
        /// <summary>수입검사상태</summary>             
        public string InInspectionState { get; set; }
        /// <summary>입고창고</summary>               
        public string InWhCode { get; set; }
        /// <summary>입고위치</summary>               
        public string InWhPosition { get; set; }
        /// <summary>수정시간</summary>     
        public DateTime? UpdateTime { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}