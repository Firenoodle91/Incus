using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Utils.Common;
using DevExpress.XtraEditors;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;

namespace HKInc.Service.Handler
{
    public static class FileHandler
    {
        private static HKInc.Utils.Interface.Helper.IStandardMessage MessageHelper = HKInc.Service.Factory.HelperFactory.GetStandardMessage();

        /// <summary>
        /// 20220318 오세완 차장 
        /// ftp파일명에 특수문자가 있으면 오류가 나서 해당 문자를 제거한 파일명을 반환처리
        /// </summary>
        /// <param name="sFilename">변환하고자 하는 파일명</param>
        /// <returns>특수문자제거된 파일명</returns>
        public static string CheckFileName(string sFilename)
        {
            string sReturn_name = "";

            string sTemp = "";
            if (sFilename != null)
                sTemp = sFilename;

            if (sTemp != "")
            {
                Regex rFind = new Regex(@"[\/:*?""<>|#]");
                Match mFind = rFind.Match(sFilename);
                if (mFind.Success)
                    sReturn_name = rFind.Replace(sFilename, "");
                else
                    sReturn_name = sFilename;
            }

            return sReturn_name;
        }

        public static EventHandler.FileHolder OpenFile(string filter = "")
        {
            if (String.IsNullOrEmpty(filter))
                filter = "All files|*.*|Excel files|*.xlsx;*.xls";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(@ofd.FileName, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    byte[] fileData = new byte[fs.Length];
                    fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();

                    return new EventHandler.FileHolder { FileName = ofd.SafeFileName,FULLFileName=ofd.FileName, FileData = fileData };
                }
            }
            else
            {
                return null;
            }
        }

        public static List<EventHandler.FileHolder> OpenFiles(string filter = "", bool multiselect = true)
        {
            List<EventHandler.FileHolder> fileHolderList = new List<EventHandler.FileHolder>();

            if (String.IsNullOrEmpty(filter))            
                filter = "All files|*.*|Excel files|*.xlsx;*.xls";            

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            ofd.RestoreDirectory = false;
            ofd.Multiselect = multiselect;
            ofd.CheckFileExists = false;            

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in ofd.FileNames)
                {
                    using (FileStream fs = new FileStream(@fileName, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        fileHolderList.Add(new EventHandler.FileHolder { FileName = @fileName, FileData = fileData });
                    }                    
                }

                return fileHolderList;
            }
            else
            {
                return null;
            }
        }

        public static string SaveFile(EventHandler.FileHolder fileHolder, string filter = "")
        {
            if (String.IsNullOrEmpty(filter))            
                filter = "Excel(Xlsx) files|*.xlsx|Excel(Xls) files|*.xls|All files|*.*";
            
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filter;
            sfd.FileName = fileHolder.FileName;

            if (MessageBoxHandler.Show(MessageHelper.GetStandardMessage(22), "File Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {   
                if (fileHolder.FileData != null)
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GlobalVariable.TempFolder))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GlobalVariable.TempFolder);
                    }

                    sfd.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GlobalVariable.TempFolder + "\\" + sfd.FileName;

                    byte[] data = fileHolder.FileData;                    
                    FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write);

                    fs.Write(data, 0, data.Length);
                    fs.Close();

                    StartProcess(sfd.FileName);
                }
            }
            else
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (fileHolder.FileData != null)
                    {
                        using (FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            byte[] data = fileHolder.FileData;

                            fs.Write(data, 0, data.Length);
                            fs.Close();
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            return sfd.FileName;
        }



        public static void StartProcess(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {                   
                    System.Diagnostics.Process.Start(fileName);
                }
            }
            catch { }
        }
        public static Image FtpImageLoad(string url)
        {
            try
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    
                    return Bitmap.FromStream(stream);
                }
            }
            catch {
                return null;
            }
        }
        public static byte[] FtpImageToByte(string url)
        {
            try
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    return ImageToByteArray(Bitmap.FromStream(stream));
                }
            }
            catch
            {
                return null;
            }
        }
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public static byte[] FtpToByte(string url)
        {
            try
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    return ToByteArray(stream);
                }
            }
            catch
            {
                return null;
            }
        }
        public static byte[] ToByteArray(Stream imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static byte[] FileToByte(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch
            {
                return null;
            }
        }
        public static void UploadFile1(string _FileName)
        {
            FileInfo fileInf = new FileInfo(_FileName);          
            string uri = GlobalVariable.FTP_SERVER + fileInf.Name;
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));          
            reqFTP.Credentials = new NetworkCredential();           
            reqFTP.KeepAlive = true;        
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;         
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                MessageBox.Show("FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                return;
            }
        }

        public static void UploadFile1(string _FileName,string _ip)
        {
            FileInfo fileInf = new FileInfo(_FileName);
            string uri = _ip + fileInf.Name;
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential();
            reqFTP.KeepAlive = true;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();

            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                MessageBox.Show("FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                return;
            }
        }

        /// <summary>
        /// 2021-11-15 김진우 주임 기존 대영로직 추가
        /// </summary>
        /// <param name="localImage"></param>
        /// <param name="fileName"></param>
        /// <param name="_ip"></param>
        public static void UploadFTP(Image localImage, string fileName, string _ip)
        {
            //[0]: "ftp:"
            //[1]: ""
            //[2]: "192.168.0.103:1901"
            //[3]: "MES_FILE_LIST"
            //[4]: "JOBSTD"
            //[5]: "ITEM-00002"
            //[6]: "P01"
            //[7]: ""

            //디렉토리 유무 및 생성
            var CheckArray = _ip.Split('/');
            for (int i = 3; i < CheckArray.Count(); i++)
            {
                if (i > 3)
                {
                    string DirUrl = string.Format("{0}//{1}/{2}/", CheckArray[0], CheckArray[2], CheckArray[3]);

                    switch (i)
                    {
                        case 4:
                            DirUrl += CheckArray[i] + "/";
                            break;
                        case 5:
                            DirUrl += CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 6:
                            DirUrl += CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 7:
                            DirUrl += CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 8:
                            DirUrl += CheckArray[i - 4] + "/" + CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 9:
                            DirUrl += CheckArray[i - 5] + "/" + CheckArray[i - 4] + "/" + CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 10:
                            DirUrl += CheckArray[i - 6] + "/" + CheckArray[i - 5] + "/" + CheckArray[i - 4] + "/" + CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                    }
                    CheckDirectoryExistsToUpload(DirUrl);
                }
                else
                {
                    var DirUrl = string.Format("{0}//{1}/{2}/", CheckArray[0], CheckArray[2], CheckArray[i]);
                    CheckDirectoryExistsToUpload(DirUrl);
                }
            }

            //FileInfo fileInf = new FileInfo(localFileInfo);
            string uri = _ip + fileName;
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            //아이디,패스워드
            //reqFTP.Credentials = new NetworkCredential(GlobalVariable.FTP_USER_ID, GlobalVariable.FTP_USER_PWD);
            //서버에 대한 연결이 소멸되지 않아야 하면 true, 소멸되어야 하면 false KeepAlive의 기본값은 원래 true임.
            reqFTP.KeepAlive = false;
            //업로드 명령어
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            //전송 타입
            reqFTP.UseBinary = true;

            try
            {
                using (Stream strm = reqFTP.GetRequestStream())
                {
                    localImage.Save(strm, System.Drawing.Imaging.ImageFormat.Jpeg);
                    strm.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                MessageBoxHandler.Show("FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                return;
            }
        }

        /// <summary>
        /// 2021-11-15 김진우 주임 기존 대영로직 추가
        /// </summary>
        /// <param name="localFileInfo"></param>
        /// <param name="fileName"></param>
        /// <param name="_ip"></param>
        public static void UploadFTP(string localFileInfo, string fileName, string _ip)
        {
            //[0]: "ftp:"
            //[1]: ""
            //[2]: "192.168.0.103:1901"
            //[3]: "MES_FILE_LIST"
            //[4]: "JOBSTD"
            //[5]: "ITEM-00002"
            //[6]: "P01"
            //[7]: ""

            //디렉토리 유무 및 생성
            var CheckArray = _ip.Split('/');
            for (int i = 3; i < CheckArray.Count(); i++)
            {
                if (i > 3)
                {
                    string DirUrl = string.Format("{0}//{1}/{2}/", CheckArray[0], CheckArray[2], CheckArray[3]);

                    switch (i)
                    {
                        case 4:
                            DirUrl += CheckArray[i] + "/";
                            break;
                        case 5:
                            DirUrl += CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 6:
                            DirUrl += CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 7:
                            DirUrl += CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 8:
                            DirUrl += CheckArray[i - 4] + "/" + CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 9:
                            DirUrl += CheckArray[i - 5] + "/" + CheckArray[i - 4] + "/" + CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                        case 10:
                            DirUrl += CheckArray[i - 6] + "/" + CheckArray[i - 5] + "/" + CheckArray[i - 4] + "/" + CheckArray[i - 3] + "/" + CheckArray[i - 2] + "/" + CheckArray[i - 1] + "/" + CheckArray[i] + "/";
                            break;
                    }
                    CheckDirectoryExistsToUpload(DirUrl);
                }
                else
                {
                    var DirUrl = string.Format("{0}//{1}/{2}/", CheckArray[0], CheckArray[2], CheckArray[i]);
                    CheckDirectoryExistsToUpload(DirUrl);
                }
            }


            FileInfo fileInf = new FileInfo(localFileInfo);
            string uri = _ip + fileName;
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            //아이디,패스워드
            //reqFTP.Credentials = new NetworkCredential(GlobalVariable.FTP_USER_ID, GlobalVariable.FTP_USER_PWD);
            //서버에 대한 연결이 소멸되지 않아야 하면 true, 소멸되어야 하면 false KeepAlive의 기본값은 원래 true임.
            reqFTP.KeepAlive = false;
            //업로드 명령어
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            //전송 타입
            reqFTP.UseBinary = true;
            //FTP에 사이즈 전송
            reqFTP.ContentLength = fileInf.Length;
            //버퍼사이즈 
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                MessageBoxHandler.Show("FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                return;
            }
        }

        private static void CheckDirectoryExistsToUpload(string ftpDirPath)
        {
            FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(ftpDirPath);
            //requestFTPUploader.Credentials = new NetworkCredential(GlobalVariable.FTP_USER_ID, GlobalVariable.FTP_USER_PWD);
            var request = requestFTPUploader; request.Method = WebRequestMethods.Ftp.ListDirectory; try
            {
                using (var result = (FtpWebResponse)request.GetResponse())
                {
                    result.Close(); //정상 종료 
                }
            }
            catch (WebException e)
            {
                FtpWebResponse response = (FtpWebResponse)e.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    MakeDir(ftpDirPath);
                    return;
                }
                else if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    //Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    //Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                    MessageBoxHandler.Show("FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                    return;
                }
                else
                {
                    MessageBoxHandler.Show("FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                    return;
                }
            }
        }

        /// <summary>
        /// 디렉토리 생성
        /// </summary>
        /// <param name="dirName">디렉토리명</param>
        private static void MakeDir(string ftpDirPath)
        {
            FtpWebRequest reqFTP;
            try
            {
                //string uri = string.Format("ftp://{0}/{1}", _ip, dirName);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpDirPath));
                //디렉토리 생성 명령 실행
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                //reqFTP.UsePassive = usePassive;
                //reqFTP.Credentials = new NetworkCredential(GlobalVariable.FTP_USER_ID, GlobalVariable.FTP_USER_PWD);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                MessageBoxHandler.Show("FTP 전송중 문제가 발생하였습니다.네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                return;
            }
        }
    }
}
