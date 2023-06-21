using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Utils.Images
{
    [ToolboxItem(false)]
    public class IconImageCollection : ImageCollection
    {
        private Dictionary<int, string> ImageDescription = new Dictionary<int, string>();
        
        public IconImageCollection()
        {
            InitImageList();
        }

        private void InitImageList()
        {            
            AddImage("Window", 0, DevExpress.Images.ImageResourceCache.Default.GetImage("images/ui/window_16x16.png"));                        // 0 Window이미지
            AddImage("Folder", 1, DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/open_16x16.png"));                     // 1 폴더 이미지                        
            AddImage("Setup", 2, DevExpress.Images.ImageResourceCache.Default.GetImage("images/setup/savepagesetup_16x16.png"));              // 2 seting 이미지
            AddImage("Module", 3, DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/programming/operatingsystem_16x16.png"));  // 3 MOdule
            AddImage("Person", 4, DevExpress.Images.ImageResourceCache.Default.GetImage("images/business%20objects/boperson_16x16.png"));      // 4 User            
            AddImage("Screen", 5, DevExpress.Images.ImageResourceCache.Default.GetImage("images/ui/window_16x16.png"));                        // 5 Screen
            AddImage("Menu", 6, DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/group_16x16.png"));                    // 6 Menu
            AddImage("Team", 7, DevExpress.Images.ImageResourceCache.Default.GetImage("images/people/team_16x16.png"));                      // 7 User gRup
            AddImage("Label", 8, DevExpress.Images.ImageResourceCache.Default.GetImage("images/business%20objects/bosale_16x16.png"));        // 8 label text
            AddImage("Language", 9, DevExpress.Images.ImageResourceCache.Default.GetImage("images/maps/map_16x16.png"));                         // 9 Language
            AddImage("Message", 10, DevExpress.Images.ImageResourceCache.Default.GetImage("images/comments/editcomment_16x16.png"));                         // 10 Message
            
            // To add local image use this
            //AddImage(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("HKInc.Utils.Images.Icons.house.png")));

            // all Dev ImageGallery List
            string[] imageList = DevExpress.Images.ImageResourceCache.Default.GetAllResourceKeys();
            Array.Sort(imageList);
            int imageIndex = 11;
            foreach (var imageName in imageList)
            {
                if(imageName.StartsWith("images/") && imageName.EndsWith("_16x16.png"))
                    AddImage(GetDescription(@imageName), imageIndex++, DevExpress.Images.ImageResourceCache.Default.GetImage(imageName));
            }
        }

        private string GetDescription(string @imageName)
        {
            string returnValue = @imageName.Replace("_16x16.png", "").Replace(@"images/", "");
            return returnValue;
        }

        private void AddImage(string @description, int index, Image image)
        {
            AddImage(image);
            ImageDescription.Add(index, @description);
        }

        public Image GetIconImage(int index)
        {
            return Images[index];            
        }

        public Image GetIconImage(string ContainDescription)
        {
            var obj = ImageDescription.Where(p => p.Value.Contains(ContainDescription)).FirstOrDefault();
            if (obj.Equals(default(KeyValuePair<int, string>))) return null;
            else return Images[obj.Key];
        }

        public void SetImageComboBoxEdit(ImageComboBoxEdit comboBox)
        {
            foreach(var index in ImageDescription.Keys)            
                comboBox.Properties.Items.Add(new ImageComboBoxItem(ImageDescription[index], index, index));
                        
            comboBox.Properties.SmallImages = (ImageCollection)this;
        }
    }
}
