using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Reflection;
using System.Collections.Generic;
using StockLib;

namespace CandleStock
{
    /// <summary>
    /// The candlestock.
    /// </summary>
    partial class Candlestock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// sets the image.
        /// </summary>
        /// <param name="m_control">The M control.</param>
        /// <param name="filePath">The file path.</param>
        private void setImage(Button m_control, String filePath)
        {
            m_control.Image = Image.FromFile(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + filePath);
        }

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Candlestock));
            this.firstChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Stock_Width = new System.Windows.Forms.ListBox();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.csvLoadFile = new System.Windows.Forms.OpenFileDialog();
            this.Stock_Ticker = new FlatComboBox();
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
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
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(12)))));
            chartArea1.Name = "candlesticks";
            this.firstChart.ChartAreas.Add(chartArea1);
            this.firstChart.Location = new System.Drawing.Point(377, 192);
            this.firstChart.Name = "firstChart";
            this.firstChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            series1.ChartArea = "candlesticks";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.WhiteSmoke;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.LabelForeColor = System.Drawing.Color.Brown;
            series1.Name = "mainSeries";
            series1.ShadowColor = System.Drawing.Color.White;
            series1.SmartLabelStyle.Enabled = false;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValuesPerPoint = 4;
            this.firstChart.Series.Add(series1);
            this.firstChart.Size = new System.Drawing.Size(1200, 675);
            this.firstChart.TabIndex = 1;
            // 
            // Stock_Width
            // 
            this.Stock_Width.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.Stock_Width.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Stock_Width.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Stock_Width.ForeColor = System.Drawing.Color.White;
            this.Stock_Width.FormattingEnabled = true;
            this.Stock_Width.ItemHeight = 16;
            this.Stock_Width.Items.AddRange(new object[] {
            "Day",
            "Week",
            "Month"});
            this.Stock_Width.Location = new System.Drawing.Point(1583, 192);
            this.Stock_Width.Name = "Stock_Width";
            this.Stock_Width.Size = new System.Drawing.Size(53, 48);
            this.Stock_Width.TabIndex = 4;
            this.Stock_Width.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Stock_Width_DrawItem);
            this.Stock_Width.SelectedIndexChanged += new System.EventHandler(this.Stock_Width_SelectedIndexChanged);
            // 
            // dtp_Start
            // 
            this.dtp_Start.CalendarForeColor = System.Drawing.Color.White;
            this.dtp_Start.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.dtp_Start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Start.Location = new System.Drawing.Point(377, 873);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Size = new System.Drawing.Size(99, 22);
            this.dtp_Start.TabIndex = 5;
            this.dtp_Start.CloseUp += new System.EventHandler(this.dtp_Start_CloseUp);
            // 
            // csvLoadFile
            // 
            this.csvLoadFile.Filter = "CSV files|*.csv|All Files|*.*";
            this.csvLoadFile.Title = "Load Stock File";
            this.csvLoadFile.FileOk += new System.ComponentModel.CancelEventHandler(this.csvLoadFile_FileOk);
            // 
            // Stock_Ticker
            // 
            this.Stock_Ticker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Stock_Ticker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Stock_Ticker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.Stock_Ticker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(96)))));
            this.Stock_Ticker.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(96)))));
            this.Stock_Ticker.ForeColor = System.Drawing.Color.White;
            this.Stock_Ticker.Location = new System.Drawing.Point(377, 162);
            this.Stock_Ticker.Name = "Stock_Ticker";
            this.Stock_Ticker.Size = new System.Drawing.Size(121, 24);
            this.Stock_Ticker.TabIndex = 3;
            this.Stock_Ticker.SelectedIndexChanged += new System.EventHandler(this.Stock_Ticker_SelectedIndexChanged);
            // 
            // dtp_End
            // 
            this.dtp_End.CalendarForeColor = System.Drawing.Color.White;
            this.dtp_End.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.dtp_End.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_End.Location = new System.Drawing.Point(1478, 873);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Size = new System.Drawing.Size(99, 22);
            this.dtp_End.TabIndex = 6;
            this.dtp_End.CloseUp += new System.EventHandler(this.dtp_End_CloseUp);
            // 
            // Candlestock
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.dtp_End);
            this.Controls.Add(this.dtp_Start);
            this.Controls.Add(this.Stock_Width);
            this.Controls.Add(this.Stock_Ticker);
            this.Controls.Add(this.firstChart);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Candlestock";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Candlestock_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Candlestock_Paint);
            this.Leave += new System.EventHandler(this.Candlestock_Leave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Candlestock_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Candlestock_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Candlestock_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Candlestock_MouseUp);
            this.Resize += new System.EventHandler(this.Candlestock_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.firstChart)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Initalizes the custom components.
        /// </summary>
        private void InitalizeCustom()
        {
            
            BackColor = mainPallete.getBackgroundColor();
            ForeColor = mainPallete.getForegroundColor();

            DefaultLocation = new Point(Screen.PrimaryScreen.Bounds.Size.Width / 2 - 450, Screen.PrimaryScreen.Bounds.Size.Height / 2 - 300);
            DefaultSizeF = new Size(900, 600);

            DefaultChartSize = firstChart.Size;
            DefaultChartLocation = firstChart.Location;
            DefaultClientSize = ClientSize;
            DefaultStock_TickerLocation = Stock_Ticker.Location;
            DefaultDateStartLocation = dtp_Start.Location;
            DefaultDateEndLocation = dtp_End.Location;

            Stock_Ticker.BackColor = mainPallete.getBackgroundColor();
            Stock_Ticker.ForeColor = mainPallete.getForegroundColor();
            Stock_Ticker.BorderColor = Utils.mix(0.25, mainPallete.getBackgroundColor(), mainPallete.getForegroundColor());
            Stock_Ticker.ButtonColor = Utils.mix(0.25, mainPallete.getBackgroundColor(), mainPallete.getForegroundColor());

            Stock_Width.BackColor = Utils.mix(0.25, mainPallete.getBackgroundColor(), mainPallete.getForegroundColor());
            Stock_Width.ForeColor = mainPallete.getForegroundColor();


            string[] allFiles = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Assets\\Stock_Data");

            List<string> csvFiles = new List<string>();

            for (int i = 0; i < allFiles.Length; i++) 
            {
                if (allFiles[i].Split('.').Last() == "csv")
                {
                    csvFiles.Add(allFiles[i].Split('\\').Last().Split('-').First());
                    stocks.Add(new Stock(allFiles[i],DateType.StringToDateWidth(Stock_Width.Text)));
                    Console.WriteLine(stocks[i]);
                }
            }

            csvFiles.Sort();
            csvFiles = csvFiles.Distinct().ToList();
            this.Stock_Ticker.AutoCompleteCustomSource.Clear();
            this.Stock_Ticker.AutoCompleteCustomSource.AddRange(csvFiles.ToArray());
            this.Stock_Ticker.Items.Clear();
            this.Stock_Ticker.Items.AddRange(csvFiles.ToArray());

            Stock_Ticker.SelectedIndex = 0;
            Stock_Width.SelectedIndex = 0;

            chartForm.stock = stocks.First();
            chartForm.currentDateWidth = new DateType((byte)(Stock_Width.SelectedIndex + 1));

            dtp_Start.MinDate = stocks.First().Date.First();
            dtp_Start.MaxDate = stocks.First().Date.Last();
            dtp_Start.MinDate = stocks.First().Date.First();
            dtp_Start.MaxDate = stocks.First().Date.Last();

            dtp_Start.Value = stocks.First().Date.First();
            dtp_End.Value = stocks.First().Date.Last();
        }

        /// <summary>
        /// The main pallete.
        /// </summary>
        private Pallete mainPallete = new Pallete();

        /// <summary>
        /// if the mouse is down.
        /// </summary>
        private bool m_MouseDown = false;
        /// <summary>
        /// if the mouse is able to drag.
        /// </summary>
        private bool m_AbleToDrag = false;
        /// <summary>
        /// the mouse location down.
        /// </summary>
        private Point m_MouseLocationDown = new Point(0, 0);
        /// <summary>
        /// if the window menu is open.
        /// </summary>
        private bool WindowMenuOpen = false;
        /// <summary>
        /// The selected list box.
        /// </summary>
        private byte selectedListBox = 255;
        /// <summary>
        /// The selected box.
        /// </summary>
        private int selectedBox = 0;

        /// <summary>
        /// The default location.
        /// </summary>
        private Point DefaultLocation;
        /// <summary>
        /// The default size.
        /// </summary>
        private Size DefaultSizeF;

        /// <summary>
        /// The default stock ticker location.
        /// </summary>
        private Point DefaultStock_TickerLocation;

        /// <summary>
        /// The default client size.
        /// </summary>
        private Size DefaultClientSize;
        /// <summary>
        /// The default chart size.
        /// </summary>
        private Size DefaultChartSize;
        /// <summary>
        /// The default chart location.
        /// </summary>
        private Point DefaultChartLocation;
        /// <summary>
        /// The default date start location.
        /// </summary>
        private Point DefaultDateStartLocation;
        /// <summary>
        /// The default date end location.
        /// </summary>
        private Point DefaultDateEndLocation;

        /// <summary>
        /// The cursor size handle.
        /// </summary>
        private UInt16 cursorSizeHandle = 0;
        /// <summary>
        /// The first chart.
        /// </summary>
        private System.Windows.Forms.DataVisualization.Charting.Chart firstChart;
        /// <summary>
        /// The stock ticker.
        /// </summary>
        private FlatComboBox Stock_Ticker;
        /// <summary>
        /// The stock width.
        /// </summary>
        private ListBox Stock_Width;
        /// <summary>
        /// The dateTimePicker start.
        /// </summary>
        private DateTimePicker dtp_Start;

        /// <summary>
        /// The stocks.
        /// </summary>
        private List<Stock> stocks = new List<Stock>();
        /// <summary>
        /// The csv load file dialog.
        /// </summary>
        public OpenFileDialog csvLoadFile;

        /// <summary>
        /// The chart form.
        /// </summary>
        CandleStock.Chart chartForm = new Chart();
        /// <summary>
        /// The datasheet form.
        /// </summary>
        DataSheet dataSheetForm = new DataSheet();
        /// <summary>
        /// The dateTimePicker end.
        /// </summary>
        private DateTimePicker dtp_End;
    }
}

