using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_QCT1201T_JI")]
    public class TN_QCT1201JI : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1201JI()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [ForeignKey("TN_QCT1200JI"), Key,Column("NO",Order =0)] public String No { get; set; }//검사번호
        [Key,Column("SEQ",Order =1)] public int Seq { get; set; }//순번
        [Column("FME_NO")] public string FmeNo { get; set; } //검사구분
        [Column("FME_DIVISION")] public string FmeDivision { get; set; }//일반 01 초 02 중 03 종 04
        [Column("ITEM_CODE")] public string ItemCode { get; set; }//품목코드
       
        [Column("READING1")] public string Reading1 { get; set; } //측정치
        [Column("READING2")] public string Reading2 { get; set; }
        [Column("READING3")] public string Reading3 { get; set; }
        [Column("READING4")] public string Reading4 { get; set; }
        [Column("READING5")] public string Reading5 { get; set; }
        [Column("JUDGE")] public string Judge { get; set; } //판정
        [Column("POOR_TYPE")] public string PoorType { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("CHECK_NAME")] public string CheckName { get; set; }//검사항목       
        [Column("CHECK_PROV")] public string CheckProv { get; set; }//검사방법
        [Column("CHECK_STAND")] public string CheckStand { get; set; }//규격
        [Column("UP_QUAD")] public Nullable<double> UpQuad { get; set; }//상한
        [Column("DOWN_QUAD")] public Nullable<double> DownQuad { get; set; }//하한
     //   public string CheckVal { get; set; }

        public string ChaeckFlag
        {
            get
            {
                string val = "";
                try
                {
                    val = Reading1.ToString();
                }
                catch { }
                if (val != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        return val.ToUpper();
                    }
                    else
                    {
                        Decimal dval = Convert.ToDecimal(val);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        //double dval = Convert.ToDouble(val);
                        //double std = Convert.ToDouble(CheckStand);
                        //double stdup = Convert.ToDouble(UpQuad);
                        //double stddown = Convert.ToDouble(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { return "OK"; }
                        else { return "NG"; }
                    }
                }
                else { return ""; }




            }
        }
        public virtual TN_QCT1200JI TN_QCT1200JI { get; set; }
    }
}