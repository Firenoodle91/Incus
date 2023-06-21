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

namespace HKInc.Service.Handler
{
    public static class FileHandler
    {
        private static HKInc.Utils.Interface.Helper.IStandardMessage MessageHelper = HKInc.Service.Factory.HelperFactory.GetStandardMessage();
 
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
    }
}
