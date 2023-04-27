using Microsoft.VisualBasic.Devices;
using StockLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CandleStock
{
    /// <summary>
    /// The chart.
    /// </summary>
    public partial class Chart : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chart"/> class.
        /// </summary>
        public Chart()
        {
            InitializeComponent();
            CustomInitalizeComponent();
        }
        /// <summary>
        /// if the chart mouse moves.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The E.</param>
        private void firstChart_MouseMove(object sender, MouseEventArgs e)
        {
            selectedPoint = firstChart.HitTest(e.X, e.Y, false, ChartElementType.DataPoint).First().Object as DataPoint;
            if (selectedPoint != null)
            {
                int stickIndex = stock.Date.ToList().FindIndex(x => x == DateTime.FromOADate(selectedPoint.XValue));

                if (stickIndex >= 0 && RecognizerUtils.TypeToString(stock.dojiType[stickIndex]).Length > 4)
                {
                    Doji_Type.Show(RecognizerUtils.TypeToString(stock.dojiType[stickIndex]), this, e.X, e.Y);
                }
            }
            else
            {
                Doji_Type.Hide(this);
            }
            firstChart.Invalidate();
        }
        /// <summary>
        /// if the chart paints.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The E.</param>
        private void firstChart_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if(selectedPoint == null)
            {
                return;
            }

            Axis xAxis = firstChart.ChartAreas.First().AxisX;
            Axis yAxis = firstChart.ChartAreas.First().AxisY;

            int stickIndex = stock.Date.ToList().FindIndex(x => x == DateTime.FromOADate(selectedPoint.XValue));
            int candleWidth = (int)Math.Abs(xAxis.ValueToPixelPosition(selectedPoint.XValue - currentDateWidth.DateTypeToWidth()) - xAxis.ValueToPixelPosition(selectedPoint.XValue));
            
            Point rectSelected = new Point((int)xAxis.ValueToPixelPosition(selectedPoint.XValue) - (candleWidth >> 1),
                                           (int)yAxis.ValueToPixelPosition(selectedPoint.YValues.Max()) - 5);

            Rectangle selectionRect = new Rectangle(rectSelected, new Size(candleWidth, (int)Math.Abs(yAxis.ValueToPixelPosition(selectedPoint.YValues.Max()) - yAxis.ValueToPixelPosition(selectedPoint.YValues.Min())) + 10));

            g.DrawRectangle(new Pen((RecognizerUtils.TypeHas(stock.dojiType[stickIndex], RecognizerUtils.Type.bullish) && RecognizerUtils.TypeHas(stock.dojiType[stickIndex], RecognizerUtils.Type.engulfing)) ? Color.LawnGreen :
                                    (RecognizerUtils.TypeHas(stock.dojiType[stickIndex], RecognizerUtils.Type.bearish) && RecognizerUtils.TypeHas(stock.dojiType[stickIndex], RecognizerUtils.Type.engulfing)) ? Color.IndianRed :
                                     Color.GhostWhite),
                                     selectionRect);
        }
        /// <summary>
        /// if the chart form is closing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The E.</param>
        private void Chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                ShowInTaskbar = false;
            }
        }
        /// <summary>
        /// if the chart resizes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The E.</param>
        private void Chart_Resize(object sender, EventArgs e)
        {
            this.ShowInTaskbar = (this.WindowState == FormWindowState.Minimized);
        }

        private void Doji_Type_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
