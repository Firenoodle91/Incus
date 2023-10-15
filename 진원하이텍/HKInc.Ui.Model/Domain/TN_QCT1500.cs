using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_QCT1500T")]
    public class TN_QCT1500 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1500()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("P_NO",Order =0)] public string PNo { get; set; }
        [Key,Column("P_TYPE",Order =1)] public string PType { get; set; }
        [Key,Column("RESULT_DATE",Order =2)] public Nullable<DateTime> ResultDate { get; set; }
        [Column("WORK_NO")] public string WorkNo{ get; set; }
        [Key,Column("SEQ",Order =3)] public int Seq { get; set; }
        [Key, Column("PROCESS_CODE", Order = 4)] public string ProcessCode { get; set; }       
        [Column("FALE_TYPE")] public string FaleType { get; set; }
        [Column("USE_FLAG")] public string UseFlag { get; set; }
        [Column("USE_QTY")] public Nullable<int> UseQty { get; set; }      
        [Column("FMEMO")] public string Fmemo { get; set; }
        [Column("FIMG1")] public byte[] FImg1 { get; set; }
        [Column("FIMG1_FILE")] public string Fimg1File { get; set; }
        [Column("FIMG2")] public byte[] FImg2 { get; set; }
        [Column("FIMG2_FILE")] public string Fimg2File { get; set; }
        [Column("FDATE")]       public Nullable<DateTime> Fdate { get; set; }
        [Column("FUSER")] public string Fuser { get; set; }
        [Column("RIMG1")] public byte[] RImg1 { get; set; }
        [Column("RIMG1_FILE")] public string Rimg1File { get; set; }     
        [Column("RIMG2")] public byte[] RImg2 { get; set; }
        [Column("RIMG2_FILE")] public string Rimg2File { get; set; }
        [Column("RMEMO")]       public string Rmemo { get; set; }
        [Column("RDATE")] public Nullable<DateTime> Rdate { get; set; }
        [Column("RUSER")] public string Ruser { get; set; }
        
    }
}


       
     
