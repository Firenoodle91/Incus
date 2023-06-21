using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 생산실적관리 TEMP
    /// </summary>
    public class TEMP_XRREP5005
    {

        public decimal RowId { get; set; }
        public string OrderNo { get; set; } //수주번호
        public string DelivNo { get; set; } //납품계획번호
        public string ItemCode { get; set; } //품목코드
        public string ItemName { get; set; } //품목명
        public string CarType { get; set; } //차종
        public string CustomerCode { get; set; } //거래처코드
        public string CustomerName { get; set; } //거래처명
        public DateTime DelivDate { get; set; } //납품예정일
        public Nullable<decimal> DelivQty { get; set; } //납품예정계획수량
        public Nullable<decimal> DueQty { get; set; } //실 납품수량
        public Nullable<decimal> DueMQty { get; set; } //미납수량
        public string OutYYMM { get; set; } //해당 월
        public Nullable<decimal> OutQtyYYMM { get; set; } //해당 월 납품 수량

        //public Nullable<decimal> OrderItemCost { get; set; } //수주제품단가
        //public Nullable<decimal> DuePlanCost { get; set; } //납품예정계획금액
        //public Nullable<decimal> DueCost { get; set; } //실 납품금액
        //public Nullable<decimal> DueMCost { get; set; } //미 달성금액
        public Nullable<decimal> DueRate { get; set; } //달성율



        public decimal A01 { get; set; }
        public decimal A02 { get; set; }
        public decimal A03 { get; set; }
        public decimal A04 { get; set; }
        public decimal A05 { get; set; }
        public decimal A06 { get; set; }
        public decimal A07 { get; set; }
        public decimal A08 { get; set; }
        public decimal A09 { get; set; }
        public decimal A10 { get; set; }
        public decimal A11 { get; set; }
        public decimal A12 { get; set; }
        public decimal A13 { get; set; }
        public decimal A14 { get; set; }
        public decimal A15 { get; set; }
        public decimal A16 { get; set; }
        public decimal A17 { get; set; }
        public decimal A18 { get; set; }
        public decimal A19 { get; set; }
        public decimal A20 { get; set; }
        public decimal A21 { get; set; }
        public decimal A22 { get; set; }
        public decimal A23 { get; set; }
        public decimal A24 { get; set; }
        public decimal A25 { get; set; }
        public decimal A26 { get; set; }
        public decimal A27 { get; set; }
        public decimal A28 { get; set; }
        public decimal A29 { get; set; }
        public decimal A30 { get; set; }
        public decimal A31 { get; set; }
        public decimal TOT { get; set; }
    }
}
