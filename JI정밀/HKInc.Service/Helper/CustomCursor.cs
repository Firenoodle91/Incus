using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Service.Helper
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    public class CustomCursor
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        public static Cursor GetCursor(Bitmap bmp, int width = 80, int height = 80)
        {
            if (bmp == null) return Cursors.Default;

            if (width > 0)
                bmp = (Bitmap)bmp.GetThumbnailImage(width, height, null, IntPtr.Zero);

            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            bmp.Dispose(); // bmp해제
            return new Cursor(ptr);
        }
    }
}
