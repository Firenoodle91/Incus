using HKInc.Service.Handler;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.View.ProductionService
{
    public class MailSendService
    {
        private string SMTP_SERVER = GlobalVariable.SMTP_SERVER; // SMTP 서버 주소
        private int SMTP_PORT = GlobalVariable.SMTP_PORT; // SMTP 포트
        private string Credentials_ID;
        private string Credentials_PWD;
        private bool EnableSsl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Credentials_ID">Id</param>
        /// <param name="Credentials_PWD">Password</param>
        /// <param name="EnableSsl">SSL 여부</param>
        public MailSendService(string Credentials_ID, string Credentials_PWD, bool EnableSsl = false)
        {
            this.Credentials_ID = Credentials_ID;
            this.Credentials_PWD = Credentials_PWD;
            this.EnableSsl = EnableSsl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SMTP_SERVER">SMTP 서버 주소</param>
        /// <param name="SMTP_PORT">SMTP 포트</param>
        /// <param name="Credentials_ID">Id</param>
        /// <param name="Credentials_PWD">Password</param>
        /// <param name="EnableSsl">SSL 여부</param>
        public MailSendService(string SMTP_SERVER, int SMTP_PORT, string Credentials_ID, string Credentials_PWD, bool EnableSsl = false)
        {
            this.SMTP_SERVER = SMTP_SERVER;
            this.SMTP_PORT = SMTP_PORT;
            this.Credentials_ID = Credentials_ID;
            this.Credentials_PWD = Credentials_PWD;
            this.EnableSsl = EnableSsl;
        }



        /// <summary>
        /// 메일 전송
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns>1 : 성공 , -1 : 실패</returns>
        public int MailSend(MailMessage mailMessage)
        {
            try
            {
                SmtpClient client = new SmtpClient(SMTP_SERVER, SMTP_PORT); // smtp 서버 정보를 생성
                client.EnableSsl = EnableSsl; // SSL 사용 유무 (네이버는 SSL을 사용합니다. )
                client.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(Credentials_ID, Credentials_PWD); // 보안인증 ( 로그인 )
                client.Send(mailMessage);  //메일 전송 

                return 1; //성공
            }
            catch (ArgumentException e)
            {
                MessageBoxHandler.Show(string.Format("Error: {0}", e.Message));
                return -1; //실패
            }
            catch (SmtpFailedRecipientException e)
            {
                MessageBoxHandler.Show(string.Format("Error: {0}", e.Message));
                return -1; //실패
            }
            catch (SmtpException e)
            {
                MessageBoxHandler.Show(string.Format("Error: {0}", e.Message));
                return -1; //실패
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
                return -1; //실패
            }
        }
    }
}
