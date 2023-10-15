using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비관리</summary>	
    [Table("TN_MEA1000T")]
    public class TN_MEA1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1000()
        {
            TN_MEA1001List = new List<TN_MEA1001>();
            TN_MEA1002List = new List<TN_MEA1002>();
            TN_MEA1004List = new List<TN_MEA1004>();
            TN_UPHList = new List<TN_UPH1000>();


        }
        /// <summary>설비고유코드</summary>        
        [Key, Column("MACHINE_MCODE"), Required(ErrorMessage = "MachineCode")] public string MachineMCode { get; set; }
        /// <summary>설비코드</summary>        
        [Column("MACHINE_CODE"), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }
        /// <summary>설비그룹코드</summary>          
        [Column("MACHINE_GROUP_CODE")] public string MachineGroupCode { get; set; }
        /// <summary>설비명</summary>          
        [Column("MACHINE_NAME"), Required(ErrorMessage = "MachineName")] public string MachineName { get; set; }
        /// <summary>설비명(영문)</summary>    
        [Column("MACHINE_NAME_ENG")] public string MachineNameENG { get; set; }
        /// <summary>설비명(중문)</summary>    
        [Column("MACHINE_NAME_CHN")] public string MachineNameCHN { get; set; }
        /// <summary>모델</summary>            
        [Column("MODEL")] public string Model { get; set; }
        /// <summary>제작사</summary>          
        [Column("MAKER")] public string Maker { get; set; }
        /// <summary>설치일자</summary>        
        [Column("INSTALL_DATE")] public DateTime? InstallDate { get; set; }
        /// <summary>S/N</summary>             
        [Column("SERIAL_NO")] public string SerialNo { get; set; }
        /// <summary>점검주기</summary>        
        [Column("CHECK_TURN")] public string CheckTurn { get; set; }
        /// <summary>파일명</summary>          
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>         
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>점검예정일</summary>      
        [Column("NEXT_CHECK_DATE")] public DateTime? NextCheckDate { get; set; }
        /// <summary>일상점검여부</summary>    
        [Column("DAILY_CHECK_FLAG"), Required(ErrorMessage = "DailyCheckFlag")] public string DailyCheckFlag { get; set; }
        /// <summary>사용여부</summary>        
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }
        /// <summary>점검포인트파일명</summary>          
        [Column("FILE_NAME2")] public string FileName2 { get; set; }
        /// <summary>점검포인트파일URL</summary>         
        [Column("FILE_URL2")] public string FileUrl2 { get; set; }
        /// <summary>예방보전점검파일명</summary>          
        [Column("FILE_NAME3")] public string FileName3 { get; set; }
        /// <summary>예방보전점검파일URL</summary>         
        [Column("FILE_URL3")] public string FileUrl3 { get; set; }
        /// <summary>제조팀코드</summary>            
        [Column("PROC_TEAM_CODE")] public string ProcTeamCode { get; set; }
        /// <summary>등급</summary>            
        [Column("CLASS")] public string Class { get; set; }
        /// <summary>등급평가일</summary>            
        [Column("CLASS_DATE")] public DateTime? ClassDate { get; set; }
        /// <summary>점수</summary>            
        [Column("SCORE")] public decimal? Score { get; set; }
        /// <summary>메모</summary>            
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>            
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary> 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>           
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual ICollection<TN_MEA1001> TN_MEA1001List { get; set; }
        public virtual ICollection<TN_MEA1002> TN_MEA1002List { get; set; }
        public virtual ICollection<TN_MEA1004> TN_MEA1004List { get; set; }
        public virtual ICollection<TN_UPH1000> TN_UPHList { get; set; }


        [NotMapped]
        public string MachineStopStates
        {
            get
            {
                if (TN_MEA1004List.Count > 0)
                {
                    if (TN_MEA1004List.Any(p => p.StopEndTime == null))
                        return "비가동";
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }
    }
}