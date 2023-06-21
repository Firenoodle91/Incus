using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>검사 디테일</summary>
    [Table("TN_QCT1001T")]
    public class TN_QCT1001 : BaseDomain.BaseDomain
    {
        public TN_QCT1001()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        /// <summary>순번</summary>
        [Key,Column("SEQ",Order =0)] public int Seq { get; set; }
        /// <summary>품번</summary>
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>검사명</summary>
        [Column("QCNAME")] public string Qcname { get; set; }
        /// <summary>검사메모</summary>
        [Column("QCNOTE")] public string Qcnote { get; set; }
        /// <summary>이미지 파일</summary>
        [Column("IMAGEFILE")] public string Imagefile { get; set; }
        /// <summary></summary>
        [Column("APPEND_DT")] public Nullable<DateTime> AppendDt { get; set; }
        /// <summary></summary>
        [Column("APPEND_USER")] public string AppendUser { get; set; }
    }
}