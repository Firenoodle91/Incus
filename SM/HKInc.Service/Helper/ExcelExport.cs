using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using HKInc.Service.Factory;

namespace HKInc.Service.Helper
{
    public class ExcelExport
    {        
        public static void ExportToExcel(GridView gridView)
        {
            if (gridView.RowCount == 0)
            {
                Handler.MessageBoxHandler.Show(Factory.HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_5));
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

        public static void ExportToExcel(TreeList treeList)
        {
            if (treeList.AllNodesCount == 0)
            {
                Handler.MessageBoxHandler.Show(Factory.HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_5));
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
            DialogResult result = Handler.MessageBoxHandler.Show(Factory.HelperFactory.GetStandardMessage().GetStandardMessage((int)Factory.StandardMessageEnum.M_46)
                                                                , HKInc.Service.Factory.HelperFactory.GetLabelConvert().GetLabelText("Export")
                                                                , MessageBoxButtons.YesNo
                                                                , MessageBoxIcon.Question);
            
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
