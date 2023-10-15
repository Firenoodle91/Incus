using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XRREP5004_MASTER
    {
        public decimal RowId { get; set; }
        public string OrderNo { get; set; } //수주번호
        public int OrderSeq { get; set; } //수주순번
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
        public Nullable<decimal> OrderItemCost { get; set; } //수주제품단가
        public Nullable<decimal> DuePlanCost { get; set; } //납품예정계획금액
        public Nullable<decimal> DueCost { get; set; } //실 납품금액
        public Nullable<decimal> DueMCost { get; set; } //미 달성금액
        public Nullable<decimal> DueRate { get; set; } //달성율


    }
}
