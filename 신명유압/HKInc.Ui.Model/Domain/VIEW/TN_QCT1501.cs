using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>성적서검사 디테일</summary>	
    [Table("TN_QCT1501T")]
    public class TN_QCT1501 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1501()
        {
        }
        /// <summary>검사번호</summary>            
        [ForeignKey("TN_QCT1500"), Key, Column("INSP_NO", Order = 0), Required(ErrorMessage = "InspNo")] public string InspNo { get; set; }
        /// <summary>검사순번</summary>            
        [Key, Column("INSP_SEQ", Order = 1), Required(ErrorMessage = "InspSeq")] public int InspSeq { get; set; }
        /// <summary>리비전번호</summary>              
        [ForeignKey("TN_QCT1001"), Column("REV_NO", Order = 2)] public string RevNo { get; set; }
        /// <summary>품번(도번)</summary>              
        [ForeignKey("TN_QCT1001"), Column("ITEM_CODE", Order = 3)] public string ItemCode { get; set; }
        /// <summary>규격순번</summary>                
        [ForeignKey("TN_QCT1001"), Column("SEQ", Order = 4)] public int? Seq { get; set; }
        /// <summary>검사방법</summary>            
        [Column("CHECK_WAY"), Required(ErrorMessage = "InspectionWay")] public string CheckWay { get; set; }
        /// <summary>검사항목</summary>            
        [Column("CHECK_LIST"), Required(ErrorMessage = "InspectionItem")] public string CheckList { get; set; }
        /// <summary>공차MAX</summary>            
        [Column("CHECK_MAX")] public string CheckMax { get; set; }
        /// <summary>공차MIN</summary>            
        [Column("CHECK_MIN")] public string CheckMin { get; set; }
        /// <summary>검사규격</summary>            
        [Column("CHECK_SPEC")] public string CheckSpec { get; set; }
        /// <summary>공차상한</summary>            
        [Column("CHECK_UP_QUAD")] public string CheckUpQuad { get; set; }
        /// <summary>공차하한</summary>            
        [Column("CHECK_DOWN_QUAD")] public string CheckDownQuad { get; set; }
        /// <summary>데이터타입</summary>            
        [Column("CHECK_DATA_TYPE")] public string CheckDataType { get; set; }
        /// <summary>측정값1</summary>             
        [Column("READING1")] public string Reading1 { get; set; }
        /// <summary>측정값2</summary>             
        [Column("READING2")] public string Reading2 { get; set; }
        /// <summary>측정값3</summary>             
        [Column("READING3")] public string Reading3 { get; set; }
        /// <summary>측정값4</summary>             
        [Column("READING4")] public string Reading4 { get; set; }
        /// <summary>측정값5</summary>             
        [Column("READING5")] public string Reading5 { get; set; }
        /// <summary>측정값6</summary>             
        [Column("READING6")] public string Reading6 { get; set; }
        /// <summary>측정값7</summary>             
        [Column("READING7")] public string Reading7 { get; set; }
        /// <summary>측정값8</summary>             
        [Column("READING8")] public string Reading8 { get; set; }
        /// <summary>측정값9</summary>             
        [Column("READING9")] public string Reading9 { get; set; }
        /// <summary>판정</summary>                
        [Column("JUDGE")] public string Judge { get; set; }
        /// <summary>메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_QCT1500 TN_QCT1500 { get; set; }
        public virtual TN_QCT1001 TN_QCT1001 { get; set; }
    }
}