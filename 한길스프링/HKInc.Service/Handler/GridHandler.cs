using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Service.Handler
{
    public static class GridHandler
    {
        public static void PasteFromClipboard2D(GridView P_View)
        {
            IDataObject obj = Clipboard.GetDataObject();

            string[,] Ls_2DArray = null;
            string[] Ls_ColumnNames = GetColumns(P_View);

            int Li_StartRow = P_View.FocusedRowHandle;
            int Li_StartCol = GetColumnIndex(P_View.FocusedColumn.FieldName, Ls_ColumnNames);

            if (obj.GetDataPresent(DataFormats.Text))
            {
                string Ls_Clipboard = (string)obj.GetData(DataFormats.Text);
                string[] Ls_Line = Ls_Clipboard.Replace(Environment.NewLine, "\n").Split('\n');

                string[] Ls_OneLineSample = Ls_Line[0].Split('\t');

                Ls_2DArray = new string[Ls_Line.Length, Ls_OneLineSample.Length];

                int Li_EndRow = Math.Min(Li_StartRow + Ls_Line.Length - 1, P_View.RowCount);
                int Li_EndCol = Math.Min(Li_StartCol + Ls_OneLineSample.Length, P_View.Columns.Count);

                int Li_CntRow = Li_EndRow - Li_StartRow;
                int Li_CntCol = Li_EndCol - Li_StartCol;

                for (int Li_Y = 0; Li_Y < Li_CntRow; Li_Y++)
                {
                    string[] Ls_OneLine = Ls_Line[Li_Y].Split('\t');
                    if (Ls_OneLine.Length < Li_CntCol) break;

                    for (int Li_X = 0; Li_X < Li_CntCol; Li_X++)
                    {
                        try
                        {
                            P_View.SetRowCellValue(Li_Y + Li_StartRow, GetColumnName(Li_X +
                                                     Li_StartCol, Ls_ColumnNames), Ls_OneLine[Li_X].Trim());
                        }
                        catch
                        { }
                    }
                }
            }
        }
        public static string[] GetColumns(GridView P_View)
        {
            string[] Ls_ColumnNames = null;
            Ls_ColumnNames = new string[P_View.Columns.Count];
            for (int i = 0; i < P_View.Columns.Count; i++)
            {
                Ls_ColumnNames[i] = P_View.Columns[i].FieldName;
            }
            return Ls_ColumnNames;
        }

        public static int GetColumnIndex(string Ps_ColumnName, string[] Ps_ColumnArray)
        {
            int Li_RET = -1;
            for (int i = 0; i < Ps_ColumnArray.Length; i++)
            {
                if (Ps_ColumnArray[i] == Ps_ColumnName)
                {
                    Li_RET = i;
                    break;
                }
            }
            return Li_RET;
        }

        public static string GetColumnName(int Pi_ColumnIndex, string[] Ps_ColumnArray)
        {
            if (Pi_ColumnIndex > Ps_ColumnArray.Length - 1) return null;
            return Ps_ColumnArray[Pi_ColumnIndex];
        }
    }
}
