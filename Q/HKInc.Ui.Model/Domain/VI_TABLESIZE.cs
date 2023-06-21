using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
    [Table("VI_TABLE_SIZE")]
    public class VI_TABLESIZE 
    {
        [Key]
        public decimal RowIndex { get; set; } //일련번호

      
        public string TableName { get; set; } //테이블명

      
        public string TableDescription { get; set; } //테이블설명

        public Nullable<decimal> RowCnt { get; set; } //데이터수
        public Nullable<decimal> DataSize { get; set; } //테이블크기

    }
}
