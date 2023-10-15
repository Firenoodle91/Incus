using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 외주재고관리 디테일
    /// </summary>
    public class TEMP_XFOUTPROC_STOCK_DETAIL
    {
        public string PoNo { get; set; }
        public int? PoSeq { get; set; }
        public string InNo { get; set; }
        public int? InSeq { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        /// <summary>
        /// 20210824 오세완 차장
        /// 품목명 추가
        /// </summary>
        public string ItemName1 { get; set; }
        public string ItemNameENG { get; set; }
        public string ItemNameCHN { get; set; }
        public string CombineSpec { get; set; }
        public decimal? PoQty { get; set; }
        public decimal? InQty { get; set; }
        public decimal? BadQty { get; set; }
        public string BadType { get; set; }
        public string Memo { get; set; }
        public decimal? RemainQty { get; set; }
        public string InLotNo { get; set; }
        public string ProductLotNo { get; set; }
        public string ItemMoveNo { get; set; }
    }
}