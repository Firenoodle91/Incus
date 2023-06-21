using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private static string Password = Properties.Settings.Default.Password;
        private static string UserId = Properties.Settings.Default.UserId;
        private static string Server = Properties.Settings.Default.DefaultServerName;

        public Form1()
        {
            InitializeComponent();

            memo_Rec.ReadOnly = true;
            memo_Send.ReadOnly = true;
            memo_Error.ReadOnly = true;

            label5.Text = "Log";

            this.Text = "큐맥 LOG API";
            Threadstart();
        }

        static string requestURL = "https://log.smart-factory.kr/apisvc/sendLogDataJSON.do?logData=";

        Thread t;
        bool checkThread = false;
        bool checkFunc = true;

        // JSON 파싱
        private JObject SetDataFormat(string apiKey, string typ, DateTime dt, string user, string ip, string dataLength)
        {
            string jsonStr = "{";
            jsonStr += "'crtfcKey' : '" + apiKey + "', ";
            jsonStr += "'logDt' : '" + dt.ToString("yyyy-MM-dd HH:mm:ss.sss") + "', ";
            jsonStr += "'useSe' : '" + typ + "', ";
            jsonStr += "'sysUser' : '" + user + "', ";
            jsonStr += "'conectIp' : '" + ip + "', ";
            jsonStr += "'dataUsgqty' : '" + dataLength + "'";
            jsonStr += "}";

            //Console.WriteLine(jsonStr);
            return JObject.Parse(jsonStr);
        }

        /// <summary>
        /// 당일 로그인아웃 접속 데이터 처리
        /// </summary>
        private void GetDataToday()
        {
            SetHistory("QMack", null, null);
            //string key = "$5$API$R93YF3MnFm2Bn9HwgkYVMzNrpuZ2Y2.cYrByPA859kD";      // 큐맥 키
            string key = "$5$API$gjd6d5eLLPbtJe2imSCPCCNeBzlQU.i6USfkCaW8.V8";      // 테스트 키

            DateTime now = DateTime.Now;
            string strtDt = now.ToString("yyyy-MM-dd");
            string endDt = now.ToString("yyyy-MM-dd");

            DataTable dt = GetProcData(SqlQuery.Proc2());

            int okCnt = 0;
            int badCnt = 0;
            foreach (DataRow row in dt.Rows)
            {
                JObject jObj = SetDataFormat(key, row["Typ"].ToString(), (DateTime)row["LoginTime"], row["UserId"].ToString(), row["IpAddress"].ToString(), row["Cnt"].ToString());
                int result = DataSend(jObj.ToString());

                if (result == 0)
                    badCnt++;
                else
                    okCnt++;
            }

            SetHistory("QMack", okCnt, badCnt);

            Console.WriteLine("{0} 데이터 처리 완료", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 데이터 전송
        /// </summary>
        /// <param name="url"></param>
        /// <returns>0 : 실패  1: 성공</returns>
        private int DataSend(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(requestURL + url);
                request.Method = "GET";
                request.ContentType = "application/json";
                using (WebResponse response = request.GetResponse())
                using (Stream dataStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    string str = reader.ReadToEnd();

                    //memo_Rec.Text += str + Environment.NewLine;

                    if (str.Contains("AP1002"))
                        return 1;       // 성공
                    else
                        return 0;       // 실패
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        // DataTable로 값 가져오기
        private DataTable GetProcData(string procName)
        {
            SqlDataAdapter da = null;
            DataSet ds = null;
            ds = new DataSet();
            da = new SqlDataAdapter();
            DataTable dt = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlQuery.ConnectionString(Server, "HKInc_Data_QMackM_V4", UserId, Password)))
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procName;     // "USP_GET_LOGAPI_HISTORY"

                    SqlParameter param1 = new SqlParameter("@WORK_DATE", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmd.Parameters.Add(param1);

                    conn.Open();
                    da.SelectCommand = cmd;
                    da.Fill(ds, "list");

                    if (ds.Tables[0] != null)
                    {
                        dt = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        // 테이블 저장 프로시저 호출
        private void SetHistory(string customer, int? okCnt = null, int? badCnt = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlQuery.ConnectionString(Server, "HKInc_Data_QMackM_V4", UserId, Password)))
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_SET_LOGAPI_HISTORY";

                    SqlParameter param1 = new SqlParameter("@WORK_DATE", DateTime.Now.ToString("yyyy-MM-dd"));
                    SqlParameter param2 = new SqlParameter("@CUSTOMER", customer);
                    SqlParameter param3 = new SqlParameter("@OK_CNT", DBNull.Value);
                    SqlParameter param4 = new SqlParameter("@BAD_CNT", DBNull.Value);

                    if (okCnt != null)
                        param3.Value = okCnt;

                    if (badCnt != null)
                        param4.Value = badCnt;

                    conn.Open();
                    cmd.Parameters.AddRange(new SqlParameter[] { param1, param2, param3, param4});
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ThreadFunc()
        {
            DateTime now;
            while (checkThread)
            {
                now = DateTime.Now;

                if (now.Hour == 23 && now.Minute == 59 && checkFunc)
                {
                    SetText(memo_Error, string.Format("[{0}] 스레드 함수 실행", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    Console.WriteLine("{0} 스레드 함수 실행", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    GetDataToday();
                    checkFunc = false;
                }
                else if (now.Hour != 23 && now.Minute != 59 && !checkFunc)
                    checkFunc = true;

                Thread.Sleep(1000 * 30);
            }
        }

        private void SetText(DevExpress.XtraEditors.MemoEdit memo, string txt)
        {
            if (memo.InvokeRequired)
            {
                this.Invoke(new EventHandler(delegate { memo.Text += txt + Environment.NewLine; }));
            }
            else
            {
                memo.Text += txt + Environment.NewLine;
            }
        }

        #region Btn
        
        // 스레드 시작
        private void button2_Click(object sender, EventArgs e)
        {
            Threadstart();
        }

        // 스레드 중지
        private void button3_Click(object sender, EventArgs e)
        {
            checkThread = false;

            if(t != null)
                t.Abort();      // 스레드 중지

            t = null;
            Console.WriteLine("스레드 중지");
        }

        private void Threadstart()
        {
            if (t == null)
            {
                Console.WriteLine("스레드 시작");
                t = new Thread(ThreadFunc);
                t.IsBackground = true;
                checkThread = true;
                t.Start();
            }
        }

        #endregion

        // 테스트 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            SetHistory("QMack", null, null);

            DataTable dt = GetProcData(SqlQuery.Proc2());
            string key = "$5$API$gjd6d5eLLPbtJe2imSCPCCNeBzlQU.i6USfkCaW8.V8";      // 테스트 키

            int okCnt = 0;
            int badCnt = 0;
            foreach (DataRow row in dt.Rows)
            {
                JObject jObj = SetDataFormat(key, row["Typ"].ToString(), (DateTime)row["LoginTime"], row["UserId"].ToString(), row["IpAddress"].ToString(), row["Cnt"].ToString());
                int result = DataSend(jObj.ToString());

                if (result == 0)
                    badCnt++;
                else
                    okCnt++;
            }

            SetHistory("QMack", okCnt, badCnt);

            Console.Write("테스트전송 완료");
        }

    }
}
