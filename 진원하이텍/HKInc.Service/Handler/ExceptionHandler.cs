using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Handler
{
    public class ExceptionHandler
    {       
        //---------------------------------------------------------------------
        // Exception 개체를 전달받아서 InnerException의 메시지를 합한 다음
        // 그 내용으로 다시 오류를 발생시키는 메소드
        //---------------------------------------------------------------------
        public void RaiseException(Exception ex)
        {
            var exceptionMessage = new StringBuilder();
            do
            {
                exceptionMessage.Append(ex.Message);
                ex = ex.InnerException;
            }
            while (ex != null);
            throw new Exception(exceptionMessage.ToString());
        }

        //---------------------------------------------------------------------
        // Exception 개체를 전달받아서 InnerException의 메시지를 합한 다음
        // 그 내용을 반환하는 메소드
        //---------------------------------------------------------------------
        public string GetMessage(Exception ex)
        {
            var exceptionMessage = new StringBuilder();
            do
            {
                exceptionMessage.Append(ex.Message);
                ex = ex.InnerException;
            }
            while (ex != null);
            return exceptionMessage.ToString();
        }
    }
}
