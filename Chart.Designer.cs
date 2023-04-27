using StockLib;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace CandleStock
{
    /// <summary>
    /// The chart.
    /// </summary>
    partial class Chart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing">If true, disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.LegendCellColumn legendCellColumn1 = new System.Windows.Forms.DataVisualization.Charting.LegendCellColumn();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem1 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendCell legendCell1 = new System.Windows.Forms.DataVisualization.Charting.LegendCell();
            System.Windows.Forms.DataVisualization.Charting.LegendCell legendCell2 = new System.Windows.Forms.DataVisualization.Charting.LegendCell();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem2 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendCell legendCell3 = new System.Windows.Forms.DataVisualization.Charting.LegendCell();
            System.Windows.Forms.DataVisualization.Charting.LegendCell legendCell4 = new System.Windows.Forms.DataVisualization.Charting.LegendCell();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.firstChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Doji_Type = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.firstChart)).BeginInit();
            this.SuspendLayout();
            // 
            // firstChart
            // 
            this.firstChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.firstChart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(96)))));
            this.firstChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.firstChart.BorderlineWidth = 5;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.SystemColors.GrayText;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(96)))));
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(192)))));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(96)))));
            chartArea1.AxisY.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(192)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(12)))));
            chartArea1.CursorX.IntervalOffset = 1D;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IntervalOffset = 1D;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "candlesticks";
            this.firstChart.ChartAreas.Add(chartArea1);
            this.firstChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(24)))));
            legendCellColumn1.Name = "Column1";
            legendCellColumn1.SeriesSymbolSize = new System.Drawing.Size(200, 0);
            legendCellColumn1.Text = "";
            legend1.CellColumns.Add(legendCellColumn1);
            legendItem1.BorderColor = System.Drawing.Color.White;
            legendItem1.BorderWidth = 2;
            legendCell1.CellType = System.Windows.Forms.DataVisualization.Charting.LegendCellType.SeriesSymbol;
            legendCell1.Name = "Cell1";
            legendCell1.Text = "Bullish";
            legendCell2.Name = "Cell2";
            legendCell2.Text = "Bullish";
            legendItem1.Cells.Add(legendCell1);
            legendItem1.Cells.Add(legendCell2);
            legendItem1.Color = System.Drawing.Color.LawnGreen;
            legendItem1.ImageStyle = System.Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line;
            legendItem1.SeparatorColor = System.Drawing.Color.White;
            legendItem2.BorderColor = System.Drawing.Color.White;
            legendItem2.BorderWidth = 2;
            legendCell3.CellType = System.Windows.Forms.DataVisualization.Charting.LegendCellType.SeriesSymbol;
            legendCell3.Name = "Cell1";
            legendCell4.Name = "Cell2";
            legendCell4.Text = "Barish";
            legendItem2.Cells.Add(legendCell3);
            legendItem2.Cells.Add(legendCell4);
            legendItem2.Color = System.Drawing.Color.IndianRed;
            legendItem2.ImageStyle = System.Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line;
            legendItem2.SeparatorColor = System.Drawing.Color.White;
            legend1.CustomItems.Add(legendItem1);
            legend1.CustomItems.Add(legendItem2);
            legend1.ForeColor = System.Drawing.Color.White;
            legend1.HeaderSeparatorColor = System.Drawing.Color.White;
            legend1.ItemColumnSeparatorColor = System.Drawing.Color.White;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend1.Name = "mainLegend";
            legend1.Title = "Engulfing Patterns";
            legend1.TitleForeColor = System.Drawing.Color.White;
            legend1.TitleSeparatorColor = System.Drawing.Color.White;
            this.firstChart.Legends.Add(legend1);
            this.firstChart.Location = new System.Drawing.Point(0, 0);
            this.firstChart.Name = "firstChart";
            this.firstChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            series1.ChartArea = "candlesticks";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.WhiteSmoke;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.LabelForeColor = System.Drawing.Color.Brown;
            series1.Legend = "mainLegend";
            series1.Name = "mainSeries";
            series1.ShadowColor = System.Drawing.Color.White;
            series1.SmartLabelStyle.Enabled = false;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValuesPerPoint = 4;
            this.firstChart.Series.Add(series1);
            this.firstChart.Size = new System.Drawing.Size(882, 553);
            this.firstChart.TabIndex = 3;
            this.firstChart.Paint += new System.Windows.Forms.PaintEventHandler(this.firstChart_Paint);
            this.firstChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.firstChart_MouseMove);
            // 
            // Doji_Type
            // 
            this.Doji_Type.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.Doji_Type.ForeColor = System.Drawing.Color.White;
            this.Doji_Type.Popup += new System.Windows.Forms.PopupEventHandler(this.Doji_Type_Popup);
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.firstChart);
            this.Name = "Chart";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chart";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chart_FormClosing);
            this.Resize += new System.EventHandler(this.Chart_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.firstChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>
        /// extension for extra custom initalization.
        /// </summary>
        private void CustomInitalizeComponent()
        {
            selectedPoint = firstChart.HitTest(0, 0, false, ChartElementType.DataPoint).First().Object as DataPoint;
        }

        /// <summary>
        /// The main pallete.
        /// </summary>
        Pallete mainPallete = new Pallete();
        /// <summary>
        /// The first chart.
        /// </summary>
        public System.Windows.Forms.DataVisualization.Charting.Chart firstChart;
        /// <summary>
        /// The selected point.
        /// </summary>
        private DataPoint selectedPoint;
        /// <summary>
        /// The doji type.
        /// </summary>
        private System.Windows.Forms.ToolTip Doji_Type;
        /// <summary>
        /// The stock.
        /// </summary>
        public Stock stock;
        /// <summary>
        /// The current date width.
        /// </summary>
        public DateType currentDateWidth;
    }
}