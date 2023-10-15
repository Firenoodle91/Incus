using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace HKInc.Service.Helper
{
    public class ExcelExport
    {        
        public static void ExportToExcel(GridView gridView)
        {
            if (gridView.RowCount == 0)
            {
                Handler.MessageBoxHandler.Show(Factory.HelperFactory.GetStandardMessage().GetStandardMessage(5));
                return;
            }

            try
            {
                string fileName = GetFileNameToSave(gridView.GridControl);
                if (!string.IsNullOrEmpty(fileName))
                {
                    gridView.ExportToXlsx(fileName);
                    StartProcess(fileName);
                }                
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }
        public static void ExportToExcel(GridView gridView, GridView gridView1)
        {
            if (gridView.RowCount == 0)
            {
                Handler.MessageBoxHandler.Show(Factory.HelperFactory.GetStandardMessage().GetStandardMessage(5));
                return;
            }

            try
            {

                var printingSystem = new PrintingSystemBase();
                var compositeLink = new CompositeLinkBase();
                compositeLink.PrintingSystemBase = printingSystem;

                var link1 = new PrintableComponentLinkBase();
                link1.Component = gridView.GridControl;
                var link2 = new PrintableComponentLinkBase();
                link2.Component = gridView1.GridControl;

                compositeLink.Links.Add(link1);
                compositeLink.Links.Add(link2);

                var options = new XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;

                compositeLink.CreatePageForEachLink();
                string fileName = GetFileNameToSave(gridView1.GridControl);
                compositeLink.ExportToXlsx(fileName, options);
                StartProcess(fileName);



                //string fileName = GetFileNameToSave(gridView.GridControl);
                //if (!string.IsNullOrEmpty(fileName))
                //{
                //    gridView.ExportToXlsx(fileName);
                //    StartProcess(fileName);
                //}
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }
        public static void ExportToExcel(GridView gridView, GridView gridView1, GridView gridView2)
        {
            if (gridView.RowCount == 0)
            {
                Handler.MessageBoxHandler.Show(Factory.HelperFactory.GetStandardMessage().GetStandardMessage(5));
                return;
            }

            try
            {

                var printingSystem = new PrintingSystemBase();
                var compositeLink = new CompositeLinkBase();
                compositeLink.PrintingSystemBase = printingSystem;

                var link1 = new PrintableComponentLinkBase();
                link1.Component = gridView.GridControl;
                var link2 = new PrintableComponentLinkBase();
                link2.Component = gridView1.GridControl;
                var link3 = new PrintableComponentLinkBase();
                link3.Component = gridView2.GridControl;
                compositeLink.Links.Add(link1);
                compositeLink.Links.Add(link2);
                compositeLink.Links.Add(link3);
                var options = new XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;

                compositeLink.CreatePageForEachLink();
                string fileName = GetFileNameToSave(gridView.GridControl);
                compositeLink.ExportToXlsx(fileName, options);
                StartProcess(fileName);



                //string fileName = GetFileNameToSave(gridView.GridControl);
                //if (!string.IsNullOrEmpty(fileName))
                //{
                //    gridView.ExportToXlsx(fileName);
                //    StartProcess(fileName);
                //}
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }
        public static void ExportToExcel(TreeList treeList)
        {
            if (treeList.AllNodesCount == 0)
            {
                Handler.MessageBoxHandler.Show(Factory.HelperFactory.GetStandardMessage().GetStandardMessage(5));
                return;
            }

            try
            {
                treeList.OptionsPrint.AutoWidth = true;
                treeList.OptionsPrint.PrintAllNodes = false;
                treeList.OptionsPrint.PrintTreeButtons = false;

                string fileName = GetFileNameToSave(treeList);
                if (!string.IsNullOrEmpty(fileName))
                {
                    treeList.ExportToXlsx(fileName);
                    StartProcess(fileName);
                }
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
        }
        static public void StartProcess(string path)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("이 파일을 여시겠습니까?", "내보내기", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process process = new Process();
                try
                {
                    process.StartInfo.FileName = path;
                    process.Start();
                    process.WaitForInputIdle();
                }
                catch { }
            }
        }

        private static string GetFileNameToSave(Control control)
        {
            string fileName = string.Format("{0}_{1:yyyyMMdd}", control.FindForm().Text, DateTime.Now);

            fileName = fileName
                .Replace(@"\", "")
                .Replace(@"/", "")
                .Replace(@":", "")
                .Replace(@"*", "")
                .Replace(@"?", "")
                .Replace("\"", "")
                .Replace(@"<", "")
                .Replace(@">", "")
                .Replace(@"|", "")
            ;

            using (SaveFileDialog savefile = new SaveFileDialog())
            {
                savefile.InitialDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                savefile.Title = "Save Grid Excel File";
                savefile.DefaultExt = "xlsx";
                savefile.Filter = "xlsx files (*.xlsx)|*.xlsx";
                savefile.FileName = fileName;

                if (savefile.ShowDialog() != DialogResult.OK)
                    return string.Empty;

                fileName = savefile.FileName;
                savefile.Dispose();
            }
            return fileName;
        }
    }
}
