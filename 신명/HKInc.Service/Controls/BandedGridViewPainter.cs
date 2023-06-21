using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.Utils.Drawing;


namespace HKInc.Service.Controls
{
    public class BandedGridViewPainter : GridPainter
    {
        BandedGridView _View;
        GridBand[] _Bands;

        public BandedGridViewPainter(BandedGridView view, GridBand[] bands)
            : base(view)
        {
            _View = view;
            _Bands = bands;
            _View.GridControl.Paint += new PaintEventHandler(GridControl_Paint);
            _View.CustomDrawColumnHeader += new ColumnHeaderCustomDrawEventHandler(_View_CustomDrawColumnHeader);
            _View.CustomDrawBandHeader += new BandHeaderCustomDrawEventHandler(_View_CustomDrawBandHeader);           
        }
        
        public void _View_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            if (_Bands.Contains(e.Band))
                e.Handled = true;
        }

        public void _View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (_View.GroupCount > 0)
            {
                e.Painter.DrawObject(e.Info);
            }
            if (((e.Column is BandedGridColumn)) && (_Bands.Contains((e.Column as BandedGridColumn).OwnerBand)))
                e.Handled = true;
        }

        public void GridControl_Paint(object sender, PaintEventArgs e)
        {
            foreach (GridBand band in _Bands)
                foreach (BandedGridColumn column in band.Columns)
                    DrawColumnHeader(new GraphicsCache(e.Graphics), column);
        }

        public void DrawColumnHeader(GraphicsCache cache, GridColumn column)
        {
            BandedGridViewInfo viewInfo = _View.GetViewInfo() as BandedGridViewInfo;

            GridColumnInfoArgs colInfo = viewInfo.ColumnsInfo[column];
            GridBandInfoArgs bandInfo = getBandInfo(viewInfo.BandsInfo, (column as BandedGridColumn).OwnerBand);
            if (colInfo == null || bandInfo == null)
                return;
            colInfo.Cache = cache;

            int top = bandInfo.Bounds.Top;
            Rectangle rect = colInfo.Bounds;
            int delta = rect.Top - top;
            rect.Y = top;
            rect.Height += delta;
            colInfo.Bounds = rect;
            colInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            ElementsPainter.Column.CalcObjectBounds(colInfo);
            ElementsPainter.Column.DrawObject(colInfo);
        }

        public GridBandInfoArgs getBandInfo(GridBandInfoCollection bands, GridBand band)
        {
            GridBandInfoArgs info = bands[band];
            if (info != null)
                return info;
            else
                foreach (GridBandInfoArgs bandInfo in bands)
                {
                    if (bandInfo.Children != null)
                    {
                        info = getBandInfo(bandInfo.Children, band);
                        if (info != null)
                            return info;
                    }
                }
            return null;
        }
    }
}
