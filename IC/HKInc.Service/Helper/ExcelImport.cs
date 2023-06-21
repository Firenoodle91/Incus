using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.DataAccess.Excel;
using HKInc.Utils.Class;
using DevExpress.SpreadsheetSource;
namespace HKInc.Service.Helper
{
    public class ExcelImport
    {
        public static DataTable GetGridDataSourceFromExcel(GridColumnCollection columns)
        {
            DataTable dtResult = GetDataTableFromExcel();
            if (dtResult.Rows.Count < 1) return dtResult;

            foreach (GridColumn column in columns)
            {
                if (column.Visible)
                {
                    bool isLookup = column.RealColumnEdit.GetType() == typeof(RepositoryItemLookUpEdit) ? true : false;
                    bool isDate = column.RealColumnEdit.GetType() == typeof(RepositoryItemDateEdit) ? true : false;

                    dtResult = DataTableColumnNameChange(dtResult, column.GetTextCaption(), column.FieldName, isLookup, isDate);
                    if (isLookup)
                        ChangeValueFromLookup(column, dtResult, column.FieldName);
                }
            }
            return dtResult;
        }
        public static DataTable GetDataTableFromExcel()
        {
            string fileName = string.Empty;
            string sheetName = string.Empty; // Excel file의 sheet name

            if (!GetExcelFileName(out fileName)) return new DataTable();

            string connectionString = string.Empty;
            string fileExt = Path.GetExtension(fileName);
            if (fileExt == ".xlsx")
            {
                connectionString = GetOleDbConnectionStringXlsx(fileName);
            }
            else
            {
                connectionString = GetOleDbConnectionString(fileName);
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                HKInc.Service.Handler.MessageBoxHandler.Show("Error OLEDB Provider does not exists ");
                return new DataTable();
            }

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            sheetName = GetSheetName(connection);
            OleDbCommand cmdSelect = new OleDbCommand(string.Format(@"SELECT * FROM [{0}]", sheetName), connection);

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdSelect);
            DataTable dtSource = new DataTable();
            dataAdapter.Fill(dtSource);

            connection.Close();
            dataAdapter = null;

            return dtSource;
        }

        private static string GetSheetName(OleDbConnection connection)
        {
            string sheetName;

            DataTable dtSheetList = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dtSheetList.Rows.Count > 1) // 여러 Sheet file
            {
                IEnumerable<DataRow> idr = dtSheetList.AsEnumerable().Where(p => p["TABLE_NAME"].GetNullToEmpty().ToUpper().StartsWith("PRODUCTION")); // Production으로 시작하나느 Sheet
                if (idr.Count() > 0)
                    sheetName = idr.First()["TABLE_NAME"].GetNullToEmpty();  // 여러개면 첫번째
                else
                    sheetName = dtSheetList.Rows[0]["TABLE_NAME"].GetNullToEmpty();
            }
            else
            {
                sheetName = dtSheetList.Rows[0]["TABLE_NAME"].GetNullToEmpty();
            }

            return sheetName;
        }
        // Grid의 Filed Name이 없음 source table에 추가
        // Lookup이면 fieldName_DSP_VALUE 컬럼 추가하고 headerName컬럼의 Display value로 채운다
        // Date이면 DateTime으로 Parse
        // FieldName의 컬럼에 값을 HeaderName 컬럼 값으로 채운다
        private static DataTable DataTableColumnNameChange(DataTable dt, string headerName, string fieldName, bool isLookup, bool isDate = false)
        {
            if (!dt.Columns.Contains(fieldName) || !dt.Columns[fieldName].ColumnName.Equals(fieldName))  // Field Name이 없으면 추가          
                dt.Columns.Add(fieldName, new object().GetType());

            if (isLookup)
            {
                dt.Columns.Add(fieldName + "_DSP_VALUE", dt.Columns[headerName].DataType); // fieldName_DSP_VALUE 컬럼으로 추가   

                foreach (DataRow row in dt.Rows)
                    row[fieldName + "_DSP_VALUE"] = row[headerName];  // Display value 채움        
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (isDate)
                    {
                        if (row[headerName].GetNullToEmpty().Equals(string.Empty))
                            row[fieldName] = null;
                        else
                            row[fieldName] = DateTime.Parse(row[headerName].GetNullToEmpty());  // 날짜 DataTime type으로 Parsing
                    }
                    else
                    {
                        row[fieldName] = row[headerName].GetNullToEmpty().Equals(string.Empty) ? DBNull.Value : row[headerName];  // 값채움
                    }
                }
            }
            return dt;
        }

        private static void ChangeValueFromLookup(GridColumn column, DataTable dt, string fieldName)
        {
            RepositoryItemLookUpEdit editor = column.ColumnEdit as RepositoryItemLookUpEdit;
            DataTable lupDt = (DataTable)editor.DataSource;

            foreach (DataRow dr in dt.AsEnumerable())
            {
                IEnumerable<DataRow> rows = lupDt.AsEnumerable().Where(p => p[editor.DisplayMember].ToString().Equals(dr[fieldName + "_DSP_VALUE"].GetNullToEmpty().ToUpper()) ||
                                                                            p[editor.DisplayMember].ToString().Equals(dr[fieldName + "_DSP_VALUE"].GetNullToEmpty()));
                if (rows.Count() > 0)
                    dr[fieldName] = rows.First()[editor.ValueMember];
            }
        }

        private static bool GetExcelFileName(out string fileName)
        {
            fileName = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "Select Excel File";
            ofd.DefaultExt = "xlsx";
            ofd.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileNames[0];

                if (!File.Exists(fileName))
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("File not found");
                    return false;
                }
                return true;
            }
            return false;
        }

        private static string GetOleDbConnectionString(string fileName)
        {
            OleDbEnumerator oleEnum = new OleDbEnumerator();

            IEnumerable<DataRow> idr = oleEnum.GetElements().AsEnumerable().Where(p => p["SOURCES_NAME"].GetNullToEmpty().StartsWith("Microsoft.ACE.OLEDB") ||
                                                                                       p["SOURCES_NAME"].GetNullToEmpty().StartsWith("Microsoft.Jet.OLEDB"));
            if (idr.Count() < 1) return string.Empty;

            return string.Format("Provider={0}; Data Source={1}; Jet OLEDB:Engine Type=5;Extended Properties=Excel 8.0;", idr.First()["SOURCES_NAME"], fileName);
        }

        private static string GetOleDbConnectionStringXlsx(string fileName)
        {
            string ConnectStrFrm_Excel =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=\"{0}\";" +
            "Mode=ReadWrite|Share Deny None;" +
            "Extended Properties='Excel 12.0; HDR={1}; IMEX={2}';" +
            "Persist Security Info=False";

            //return string.Format("Provider={0}; Data Source={1}; Jet OLEDB:Engine Type=5;Extended Properties=Excel 8.0;", idr.First()["SOURCES_NAME"], fileName);
            return string.Format(ConnectStrFrm_Excel, fileName, "YES", 1);
        }

        public static DataTable GetDataTableFromExcelDataSource(GridColumnCollection columns, int workSheetIndex = 0, bool isTagType = false)
        {
            DataTable dt = new DataTable();
            string fileName;

            if (!GetExcelFileName(out fileName)) return dt;

            try
            {
                ExcelDataSource excelDataSource = GetExcelDataSource(fileName, workSheetIndex);

                foreach (GridColumn column in columns.Where(p => p.Visible).OrderBy(p => p.VisibleIndex))
                {
                    if (isTagType)
                    {
                        excelDataSource.Schema.Add(new FieldInfo { Name = column.FieldName, Type = (Type)column.Tag });
                    }
                    else
                    {
                        if (column.ColumnEdit is RepositoryItemGridLookUpEdit)
                        {
                            excelDataSource.Schema.Add(new FieldInfo { Name = column.FieldName, Type = typeof(string) });
                        }
                        else
                        {
                            System.Type type;
                            if (column.DisplayFormat.FormatType == DevExpress.Utils.FormatType.DateTime)
                            {
                                type = typeof(DateTime);
                            }
                            else if (column.DisplayFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
                            {
                                if (column.DisplayFormat.FormatString.StartsWith("n"))
                                    type = column.DisplayFormat.FormatString == "n0" ? typeof(int) : typeof(decimal);
                                else
                                    type = Type.GetType(column.ColumnType.AssemblyQualifiedName);
                            }
                            else
                            {
                                type = column.ColumnType;
                            }
                            excelDataSource.Schema.Add(new FieldInfo { Name = column.FieldName, Type = type });
                        }
                    }
                }

                excelDataSource.Fill();

                dt = excelDataSource.ToDataTable();
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }

            return dt;
        }

        public static DataTable GetDataTableFromExcelDataSource(List<FieldInfo> fieldInfoList, int workSheetIndex = 0)
        {
            DataTable dt = new DataTable();
            string fileName;

            if (!GetExcelFileName(out fileName)) return dt;

            try
            {
                ExcelDataSource excelDataSource = GetExcelDataSource(fileName, workSheetIndex);

                foreach (var obj in fieldInfoList)
                    excelDataSource.Schema.Add(obj);

                excelDataSource.Fill();

                dt = excelDataSource.ToDataTable();
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }

            return dt;
        }

        private static ExcelDataSource GetExcelDataSource(string fileName, int workSheetIndex)
        {
            ExcelDataSource excelDataSource = new ExcelDataSource();
            excelDataSource.FileName = @fileName;

            ExcelWorksheetSettings worksheetSettings = new ExcelWorksheetSettings(GetWorkSheetNameByIndex(@fileName, workSheetIndex));
            ExcelSourceOptions excelSourceOptions = new ExcelSourceOptions();
            excelSourceOptions.ImportSettings = worksheetSettings;
            excelSourceOptions.SkipHiddenRows = true;
            excelSourceOptions.SkipHiddenColumns = true;
            excelSourceOptions.SkipEmptyRows = true;
            excelSourceOptions.UseFirstRowAsHeader = true;
            excelDataSource.SourceOptions = excelSourceOptions;

            return excelDataSource;
        }

        private static string GetWorkSheetNameByIndex(string fileName, int workSheetIndex = 0)
        {
            string worksheetName = "";
            using (ISpreadsheetSource spreadsheetSource = SpreadsheetSourceFactory.CreateSource(fileName))
            {
                IWorksheetCollection worksheets = spreadsheetSource.Worksheets;
                worksheetName = worksheets[workSheetIndex].Name;
            }
            return worksheetName;
        }


        public static DataTable GetTableFromExcel(string fileName, int workSheetIndex = 0)
        {
            DataTable dt = new DataTable();
            //string fileName;

          //  if (!GetExcelFileName(out fileName)) return dt;

            try
            {
                ExcelDataSource excelDataSource = GetExcelDataSource(fileName, workSheetIndex);

                //foreach (var obj in fieldInfoList)
                //    excelDataSource.Schema.Add(obj);

                excelDataSource.Fill();

                dt = excelDataSource.ToDataTable();
            }
            catch (Exception ex)
            {
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }

            return dt;
        }

    }
}
