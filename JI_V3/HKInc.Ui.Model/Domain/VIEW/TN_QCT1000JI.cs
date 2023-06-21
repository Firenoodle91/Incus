using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_QCT1000T_JI")]
    public class TN_QCT1000JI : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1000JI()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }

        [ForeignKey("TN_STD1100"),Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Key,Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("CHECK_NAME")] public string CheckName { get; set; }//검사항목
        [Column("PROCESS_GU")] public string ProcessGu { get; set; }//검사종류
        [Column("CHECK_PROV")] public string CheckProv { get; set; }//검사방법
        [Column("CHECK_STAND")] public string CheckStand { get; set; }//규격
        [Column("UP_QUAD")] public Nullable<double> UpQuad { get; set; }//상한
        [Column("DOWN_QUAD")] public Nullable<double> DownQuad { get; set; }//하한
        [Column("MEMO")] public string Memo { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }//계측기종류
        [Column("TEMP2")] public string Temp2 { get; set; }//검사순서
        [Column("SPCYN")] public string SpcYn { get; set; }//특별항목
        [NotMapped]
        public string CheckVal { get; set; }

        public string ChaeckFlag
        {
            get {
                string val = "";
                try
                {
                    val = CheckVal.ToString();
                }
                catch { }
                if (val != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        return val;
                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { return "OK"; }
                        else { return "NG"; }
                    }
                }
                else { return ""; }




            }
        }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}