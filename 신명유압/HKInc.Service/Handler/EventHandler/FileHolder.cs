using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Handler.EventHandler
{
    public class FileHolder
    {
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string EntityName { get; set; }
        public string FirstKey { get; set; }
        public string SecondKey { get; set; }
        public string ThirdKey { get; set; }
        public string FULLFileName { get; set; }
    }
}
