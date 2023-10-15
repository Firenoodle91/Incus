using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.BaseDomain
{
    public class GsValue
    {

        public static String UserId { get; set; }

        public static String MachineCode { get; set; }

        public static decimal loginId { get; set; }
        public static byte[] UriToByte(string url)
        {
            var request = WebRequest.Create(url);
            try
            {
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    Image img = Bitmap.FromStream(stream);


                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms, img.RawFormat);
                        return ms.ToArray();
                    }
                }
            }
            catch { return null; }
        }
    }
   
}