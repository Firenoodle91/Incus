﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>제품재고관리 마스터(월별)</summary>	
    public class TEMP_PROD_STOCK_MASTER
    {
        public TEMP_PROD_STOCK_MASTER()
        {

        }
        /// <summary>고유번호(가상번호)</summary>           
        public long RowIndex { get; set; }
        /// <summary>품목코드</summary>           
        public string ItemCode { get; set; }
        /// <summary>품명</summary>           
        public string ItemName { get; set; }
        /// <summary>품명(영문)</summary>           
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>           
        public string ItemNameCHN { get; set; }
        /// <summary>단위</summary>           
        public string Unit { get; set; }
        /// <summary>단가</summary>           
        public decimal? Cost { get; set; }
        /// <summary>주거래처</summary>           
        public string CustomerName { get; set; }
        /// <summary>담당자</summary>           
        public string BusinessManagementId { get; set; }
        /// <summary>안전재고</summary>                 
        public decimal? SafeQty { get; set; }
        /// <summary>총 입고량</summary>                 
        public decimal? SumInQty { get; set; }
        /// <summary>총 출고량</summary>                 
        public decimal? SumOutQty { get; set; }
        /// <summary>총 이월재고량</summary>                 
        public decimal? SumCarryOverQty { get; set; }
        /// <summary>총 재고조정량</summary>                 
        public decimal? SumAdjustQty { get; set; }
        /// <summary>총 재고량</summary>                 
        public decimal? SumStockQty { get; set; }
    }
}