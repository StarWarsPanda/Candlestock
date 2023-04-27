using Microsoft.VisualBasic;
using StockLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CandleStock
{
    /// <summary>
    /// The candlestock.
    /// </summary>
    public partial class Candlestock : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Candlestock"/> class.
        /// </summary>
        public Candlestock()
        {
            InitializeComponent();
            InitalizeCustom();
        }
        /// <summary>
        /// if the Candlestock resizes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Candlestock_Resize(object sender, EventArgs e)
        {
            firstChart.Location = new Point((ClientSize.Width * DefaultChartLocation.X) / DefaultClientSize.Width,
                                        (ClientSize.Height * DefaultChartLocation.Y) / DefaultClientSize.Height);
            firstChart.Size = new Size((ClientSize.Width * DefaultChartSize.Width) / DefaultClientSize.Width,
                                   (ClientSize.Height * DefaultChartSize.Height) / DefaultClientSize.Height);
            Stock_Ticker.Location = new Point(firstChart.Location.X, firstChart.Location.Y - Stock_Ticker.Height - 3);
            Stock_Width.Location = new Point(firstChart.Location.X + firstChart.Width + 5, firstChart.Location.Y);
            dtp_Start.Location = new Point((ClientSize.Width * DefaultDateStartLocation.X) / DefaultClientSize.Width,
                                           (ClientSize.Height * DefaultDateStartLocation.Y) / DefaultClientSize.Height);
            dtp_End.Location = new Point((ClientSize.Width * DefaultDateEndLocation.X) / DefaultClientSize.Width,
                                           (ClientSize.Height * DefaultDateEndLocation.Y) / DefaultClientSize.Height);
            Invalidate();
        }
        /// <summary>
        /// if the mouse is clicked over the Minimize button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void MinimizeButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// if the mouse is clicked over the Maximize button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void MaximizeButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Size = DefaultSizeF;
                Location = DefaultLocation;
                Console.WriteLine(Location.ToString());
                return;
            }

            DefaultLocation = Location;
            DefaultSizeF = Size;
            WindowState = FormWindowState.Maximized;
            Console.WriteLine(Location.ToString());
        }
        /// <summary>
        /// if the mouse is clicked over the Exit button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void ExitButton_MouseClick(object sender, MouseEventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// if the mouse moves over the Candlestock form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Candlestock_MouseMove(object sender, MouseEventArgs e)
        {
            Invalidate();

            if (WindowState == FormWindowState.Maximized) return;

            if (m_MouseDown)
            {
                if (m_AbleToDrag)
                {
                    Console.WriteLine("Cursor: " + Cursor.Position + " | Location: " + Location + "| m_MouseLocationDown: " + m_MouseLocationDown);
                    Location = new Point(Location.X + Cursor.Position.X - m_MouseLocationDown.X, Location.Y + Cursor.Position.Y - m_MouseLocationDown.Y);
                    Console.WriteLine(Location);
                    m_MouseLocationDown = Cursor.Position;
                }

                switch (cursorSizeHandle)
                {
                    case 0:
                        return;
                    case 1:
                        {
                            Point OldLocation = Location;
                            Location = new Point(Cursor.Position.X, Location.Y);
                            Size = new Size(Size.Width + OldLocation.X - Cursor.Position.X, Size.Height);
                        }
                        return;
                    case 2:
                        Size = new Size(Cursor.Position.X - Location.X, Size.Height);
                        return;
                    case 3:
                        Size = new Size(Size.Width, Cursor.Position.Y - Location.Y);
                        return;
                    case 4:
                        {
                            Point OldLocation = Location;
                            Location = new Point(Location.X, Cursor.Position.Y);
                            Size = new Size(Size.Width, Size.Height + OldLocation.Y - Cursor.Position.Y);
                        }
                        return;
                    default:
                        return;
                }
            }
            else
            {
                Console.WriteLine(Cursor.Current.ToString());

                if (Cursor.Position.X - Location.X < 5)
                {
                    Cursor = Cursors.SizeWE;
                    cursorSizeHandle = 1;
                    return;
                }

                if (Cursor.Position.X - Location.X > Size.Width - 5)
                {
                    Cursor = Cursors.SizeWE;
                    cursorSizeHandle = 2;
                    return;
                }

                if (Cursor.Position.Y - Location.Y > Size.Height - 5)
                {
                    Cursor = Cursors.SizeNS;
                    cursorSizeHandle = 3;
                    return;
                }

                if (Cursor.Position.Y - Location.Y < 5)
                {
                    Cursor = Cursors.SizeNS;
                    cursorSizeHandle = 4;
                    return;
                }

                Cursor = Cursors.Default;
                cursorSizeHandle = 0;
            }
        }
        /// <summary>
        /// if the mouse is down over the Candlestock form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Candlestock_MouseDown(object sender, MouseEventArgs e)
        {
            Image ExitImage = (Image)Properties.Resources.ResourceManager.GetObject("b_Exit_" + mainPallete.ToString());
            Image MaximizeImage = (Image)Properties.Resources.ResourceManager.GetObject((WindowState == FormWindowState.Maximized ? "b_Full_" : "b_Win_") + mainPallete.ToString());
            Image MinimizeImage = (Image)Properties.Resources.ResourceManager.GetObject("b_Minimize_" + mainPallete.ToString());

            m_MouseDown = true;
            m_AbleToDrag = Cursor.Position.X - Location.X > 75 && Cursor.Position.X - Location.X < Width - ExitImage.Width - MaximizeImage.Width - MinimizeImage.Width && Cursor.Position.Y - Location.Y > 5 && Cursor.Position.Y - Location.Y < ExitImage.Height;
            m_MouseLocationDown = Cursor.Position;
        }
        /// <summary>
        /// if the mouse is up over the Candlestock form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Candlestock_MouseUp(object sender, MouseEventArgs e)
        {
            m_AbleToDrag = false;
            m_MouseDown = false;
        }
        /// <summary>
        /// if the Candlestock paints.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Candlestock_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Console.WriteLine("WindowMenuOpen: " + WindowMenuOpen);

            if (WindowMenuOpen)
            {
                selectedListBox = DrawListBox(g, new String[] { "Chart", "Data Sheet","<br>", "Exit" }, new Font(DefaultFont.FontFamily, 10), new SolidBrush(mainPallete.getForegroundColor()), new SolidBrush(Utils.mix(0.3, mainPallete.getBackgroundColor(), mainPallete.getForegroundColor())), new SolidBrush(Color.FromArgb(127, mainPallete.getForegroundColor())), 5, new Font(DefaultFont.FontFamily, 10).Height + 5, 100, 5, 3, selectedBox == 1);
                WindowMenuOpen = (selectedListBox != 255);
            }

            selectedBox = 0;
            selectedBox += /*1 * */ Convert.ToByte(DrawTextBox(g, "Analyze", new Font(DefaultFont.FontFamily, 10), new SolidBrush(mainPallete.getForegroundColor()), new SolidBrush(Color.FromArgb(127, 127, 127, 127)), 5, 5, 10));

            Image ExitImage = (Image)Properties.Resources.ResourceManager.GetObject("b_Exit_" + mainPallete.ToString());
            Image MaximizeImage = (Image)Properties.Resources.ResourceManager.GetObject((WindowState == FormWindowState.Maximized ? "b_Full_" : "b_Win_") + mainPallete.ToString());
            Image MinimizeImage = (Image)Properties.Resources.ResourceManager.GetObject("b_Minimize_" + mainPallete.ToString());

            selectedBox += 2 * Convert.ToByte(DrawImageBox(g, ExitImage, new SolidBrush(Color.FromArgb(127, mainPallete.getForegroundColor())), Size.Width - ExitImage.Width, 0));
            selectedBox += 3 * Convert.ToByte(DrawImageBox(g, MaximizeImage, new SolidBrush(Color.FromArgb(127, mainPallete.getForegroundColor())), Size.Width - ExitImage.Width - MaximizeImage.Width, 0));
            selectedBox += 4 * Convert.ToByte(DrawImageBox(g, MinimizeImage, new SolidBrush(Color.FromArgb(127, mainPallete.getForegroundColor())), Size.Width - ExitImage.Width - MaximizeImage.Width - MinimizeImage.Width, 0));
        }
        /// <summary>
        /// Draws a text box.
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="str">The string.</param>
        /// <param name="font">The font.</param>
        /// <param name="fontBrush">The font brush.</param>
        /// <param name="rectBrush">The rect brush.</param>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <param name="stringOffset">The string offset.</param>
        /// <param name="width">The width.</param>
        /// <returns>A bool of success.</returns>
        private bool DrawTextBox(Graphics g, String str, Font font, Brush fontBrush, Brush rectBrush, int x, int y, int stringOffset, int width = 0)
        {
            bool final = false;

            width = width == 0 ? (int)(str.Length * font.Size) : width;

            // break line 
            if (str == "<br>")
            {
                g.DrawLine(new Pen(rectBrush), x + stringOffset, y + font.Height / 2, x + width - stringOffset, y + font.Height / 2);
                return final;
            }

            if (Cursor.Position.X - Location.X > x && Cursor.Position.X - Location.X < x + width && Cursor.Position.Y - Location.Y > y && Cursor.Position.Y - Location.Y < y + font.Height)
            {
                g.FillRectangle(rectBrush, x, y, width, font.Height);
                final = true;
            }

            g.DrawString(str, font, fontBrush, x + stringOffset, y);

            return final;
        }
        /// <summary>
        /// Draw a list box.
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="strings">The strings.</param>
        /// <param name="font">The font.</param>
        /// <param name="fontBrush">The font brush.</param>
        /// <param name="rectBrush">The rect brush.</param>
        /// <param name="rectTextBrush">The rect text brush.</param>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <param name="width">The width.</param>
        /// <param name="stringOffsetX">The string offset X.</param>
        /// <param name="stringOffsetY">The string offset Y.</param>
        /// <param name="DropDown">If true, drop down.</param>
        /// <returns>A Byte of success and selected box.</returns>
        private Byte DrawListBox(Graphics g, String[] strings, Font font, Brush fontBrush, Brush rectBrush, Brush rectTextBrush, int x, int y, int width, int stringOffsetX, int stringOffsetY, bool DropDown = false)
        {
            byte final = 255; // Not in ListBox

            if (DropDown || (Cursor.Position.X - Location.X > x && Cursor.Position.X - Location.X < x + width && Cursor.Position.Y - Location.Y > y - 5 && Cursor.Position.Y - Location.Y < y + font.Height * strings.Length + stringOffsetY * (strings.Length - 1)))
            {
                g.FillRectangle(rectBrush, x, y, width, font.Height * strings.Length + stringOffsetY * (strings.Length - 1));

                final = 0; // In the ListBox but not hovering over any items

                for (byte i = 0; i < strings.Length; i++)
                {
                    final += (byte)((i + 1) * Convert.ToByte(DrawTextBox(g, strings[i], font, fontBrush, rectTextBrush, x, y + font.Height * i + stringOffsetY * i, stringOffsetX, width))); // Finds if any items are selected
                }

                if (final > 0) Console.WriteLine(strings[final - 1]);
            }


            return final;
        }
        /// <summary>
        /// Draws am image box.
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="image">The image.</param>
        /// <param name="rectBrush">The rect brush.</param>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <returns>A bool of success.</returns>
        private bool DrawImageBox(Graphics g, Image image, Brush rectBrush, int x, int y)
        {
            bool final = false;

            if (Cursor.Position.X - Location.X > x && Cursor.Position.X - Location.X < x + image.Width && Cursor.Position.Y - Location.Y > y && Cursor.Position.Y - Location.Y < y + image.Height)
            {
                g.FillRectangle(rectBrush, x, y, image.Width, image.Height);
                final = true;
            }

            g.DrawImage(image, x, y);

            return final;
        }
        /// <summary>
        /// if the mouse leaves the Candlestock form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Candlestock_Leave(object sender, EventArgs e)
        {
            Invalidate();
        }
        /// <summary>
        /// if the mouse clicks on the Candlestock form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Candlestock_MouseClick(object sender, MouseEventArgs e)
        {
            switch (selectedListBox)
            {

                case 255:
                    Console.WriteLine("selectedListBox: Not in bounds");
                    break;
                case 0:
                    Console.WriteLine("selectedListBox: No items selected");
                    selectedListBox = 255;
                    return;
                case 1:
                    // Chart
                    selectedListBox = 255;
                    Console.WriteLine(Stock_Ticker.SelectedIndex);
                    if (chartForm.firstChart.Series.Count > 0)
                    {
                        chartForm.stock = RefreshChartData(chartForm.firstChart.Series.First(),dtp_Start.Value,dtp_End.Value);
                    }
                    chartForm.Show();
                    return;
                case 2:
                    // Data sheet
                    selectedListBox = 255;
                    dataSheetForm.selectedStock = RefreshChartData(chartForm.firstChart.Series.First(), dtp_Start.Value, dtp_End.Value);
                    dataSheetForm.Visible = true;
                    dataSheetForm.Show();
                    return;
                /*case 3:
                    // Add data...
                    selectedListBox = 255;
                    csvLoadFile.ShowDialog();
                    return;
                */
                // case 3: // <br>
                case 4:
                    //Exit
                    ExitButton_MouseClick(sender, e);
                    return;

            }

            switch (selectedBox)
            {
                case 0:
                    return;
                case 1:
                    WindowMenuOpen = !WindowMenuOpen;
                    return;
                case 2:
                    ExitButton_MouseClick(sender, e);
                    return;
                case 3:
                    MaximizeButton_MouseClick(sender, e);
                    return;
                case 4:
                    MinimizeButton_MouseClick(sender, e);
                    return;

            }
        }
        /// <summary>
        /// draw each item for the Stock width.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Stock_Width_DrawItem(object sender, DrawItemEventArgs e)
        {
            Console.WriteLine("Stock_Width_DrawItem");

            if (e.Index < 0)
            {
                Console.WriteLine("e.Index < 0");
                return;
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                Console.WriteLine("(e.State & DrawItemState.Selected) == DrawItemState.Selected");
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State ^ DrawItemState.Selected, Stock_Width.ForeColor, Utils.mix(0.25, Stock_Width.BackColor, Stock_Width.ForeColor));
            }

            e.DrawBackground();
            e.Graphics.DrawString(Stock_Width.GetItemText(Stock_Width.Items[e.Index]), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
        /// <summary>
        /// if the  selected index is changed for the Stock width.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments.</param>
        private void Stock_Width_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Stock_Width_SelectedIndexChanged");
            if (Stock_Ticker.SelectedIndex.Equals(null))
            {
                Stock_Ticker.SelectedIndex = 0;
            }

            chartForm.currentDateWidth = new DateType((byte)(Stock_Width.SelectedIndex + 1));

            string[] allFiles = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Assets\\Stock_Data");

            List<string> csvFiles = new List<string>();

            for (int i = 0; i < allFiles.Length; i++)
            {
                Console.WriteLine(allFiles[i].Split('\\').Last().Split('-').First() + ": " + allFiles[i].Split('\\').Last().Split('-').Last().Split('.').First() + ", " + Stock_Width.Text);
                if (allFiles[i].Split('.').Last() == "csv" && allFiles[i].Split('\\').Last().Split('-').Last().Split('.').First() == Stock_Width.Text)
                {
                    csvFiles.Add(allFiles[i].Split('\\').Last().Split('-').First());
                    stocks.Add(new Stock(allFiles[i], DateType.StringToDateWidth(Stock_Width.Text)));
                    //Console.WriteLine(stocks[i]);
                }
            }

            csvFiles.Sort();
            csvFiles = csvFiles.Distinct().ToList();
            int ic = 0;
            foreach (string file in csvFiles)
            {
                if (ic < csvFiles.Count - 1) 
                { 
                    Console.Write(file + ", "); 
                }
                else 
                {
                    Console.Write(file);
                }
                ic++;
            }
            object selectedItem = Stock_Ticker.SelectedItem;

            Stock_Ticker.AutoCompleteCustomSource.Clear();
            Stock_Ticker.AutoCompleteCustomSource.AddRange(csvFiles.ToArray());
            Stock_Ticker.Items.Clear();
            Stock_Ticker.Items.AddRange(csvFiles.ToArray());
            Console.WriteLine(Stock_Ticker.Items.Cast<object>().ToList().Find(x => x.Equals(selectedItem)));
            object foundItem = Stock_Ticker.Items.Cast<object>().ToList().Find(x => x.Equals(selectedItem));
            Stock_Ticker.SelectedItem = (foundItem == null) ? Stock_Ticker.Items[0] : selectedItem;
            Stock_Ticker.SelectedIndex = (foundItem == null) ? 0 : Stock_Ticker.Items.IndexOf(foundItem);
            Stock_Ticker.Text = (string)Stock_Ticker.SelectedItem;

            RefreshChartData(firstChart.Series.First());

            Stock_Width.Invalidate();
        }
        /// <summary>
        /// when the Candlestock form loads.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void Candlestock_Load(object sender, EventArgs e)
        {
            firstChart.Series.First().Points.Clear();
            stocks.First().addStockData(firstChart.Series.First());
        }
        /// <summary>
        /// if the csv file is loaded ok.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void csvLoadFile_FileOk(object sender, CancelEventArgs e)
        {
            //Assume the file is safe and in the csv stock file format from Yahoo

            //Add the file to the local stock data SAETWsfolder for when the app is reopened
            File.Copy(csvLoadFile.FileName, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Assets\\Stock_Data\\" + csvLoadFile.SafeFileName);
            stocks.Add(new Stock(csvLoadFile.FileName, DateType.StringToDateWidth(csvLoadFile.SafeFileName.Split('-').Last().Split('.').First())));
        }
        /// <summary>
        /// if the selected index changed for the Stock ticker.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void Stock_Ticker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Stock_Width.SelectedIndex.Equals(null))
            {
                Stock_Width.SelectedIndex = 0;
            }

            RefreshChartData(firstChart.Series.First());
        }
        /// <summary>
        /// Refreshes the chart data using all the data points.
        /// </summary>
        /// <param name="chartSeries">The chart series.</param>
        /// <returns>A Stock.</returns>
        private Stock RefreshChartData(System.Windows.Forms.DataVisualization.Charting.Series chartSeries)
        {
            if (Stock_Ticker.Items.Count > 0 && Stock_Ticker.SelectedIndex >= 0)
            { 
                Stock foundStock = stocks.Find(x => x.name == (Stock_Ticker.Items[Stock_Ticker.SelectedIndex] + "-" +  (string)Stock_Width.SelectedItem));
                if(foundStock != null && chartSeries != null)
                {
                    chartSeries.Points.Clear();
                    foundStock.addStockData(chartSeries);
                }
                return foundStock;
            }

            return new Stock();
        }
        /// <summary>
        /// Refreshes the chart data.
        /// </summary>
        /// <param name="chartSeries">The chart series.</param>
        /// <param name="minDate">The min date.</param>
        /// <param name="maxDate">The max date.</param>
        /// <returns>A Stock.</returns>
        private Stock RefreshChartData(System.Windows.Forms.DataVisualization.Charting.Series chartSeries, DateTime minDate, DateTime maxDate)
        {
            if (Stock_Ticker.Items.Count > 0)
            {
                Stock foundStock = stocks.Find(x => x.name == (Stock_Ticker.Items[Stock_Ticker.SelectedIndex] + "-" + (string)Stock_Width.SelectedItem));
                if (foundStock != null && chartSeries != null)
                {
                    chartSeries.Points.Clear();
                    foundStock.addStockData(chartSeries,minDate,maxDate);
                }
                return foundStock;
            }
            return new Stock();
        }
        /// <summary>
        /// if the dateTimePicker start closes up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void dtp_Start_CloseUp(object sender, EventArgs e)
        {
            dtp_End.MinDate = dtp_Start.Value;
            dtp_End.MaxDate = stocks.Find(x => x.name == (Stock_Ticker.Items[Stock_Ticker.SelectedIndex] + "-" + (string)Stock_Width.SelectedItem)).Date.Last();
        }
        /// <summary>
        /// if the dateTimePicker end closes up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArguments</param>
        private void dtp_End_CloseUp(object sender, EventArgs e)
        {
            dtp_Start.MaxDate = stocks.Find(x => x.name == (Stock_Ticker.Items[Stock_Ticker.SelectedIndex] + "-" + (string)Stock_Width.SelectedItem)).Date.First();
            dtp_Start.MaxDate = dtp_End.Value;
        }
    }
    /// <summary>
    /// The utils.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Mix between two <see cref="Color">Colors</see>.
        /// </summary>
        /// <param name="mixPercentage">The mix percentage.</param>
        /// <param name="A">The first color</param>
        /// <param name="B">The second color</param>
        /// <returns>A Color.</returns>
        public static Color mix(double mixPercentage, Color A, Color B)
        {
            return Color.FromArgb((byte)(A.A * (1.0 - mixPercentage) + mixPercentage * B.A),
                                  (byte)(A.R * (1.0 - mixPercentage) + mixPercentage * B.R),
                                  (byte)(A.G * (1.0 - mixPercentage) + mixPercentage * B.G),
                                  (byte)(A.B * (1.0 - mixPercentage) + mixPercentage * B.B));
        }
        /// <summary>
        /// Clamps x by a max value.
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="a">The max value</param>
        /// <returns>A double.</returns>
        public static double ClampMax(double x, double a)
        {
            return (-1.0 * Math.Abs(x - a) + x + a) * 0.5;
        }
        /// <summary>
        /// Clamps x by a max value.
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="a">The max value</param>
        /// <returns>A double.</returns>
        public static int ClampMax(int x, int a)
        {
            return (-Math.Abs(x - a) + x + a) >> 1;
        }
        /// <summary>
        /// Clamps x by a min value.
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="a">The min value</param>
        /// <returns>A double.</returns>
        public static double ClampMin(double x, double a)
        {
            return (Math.Abs(x - a) + x + a) * 0.5;
        }
        /// <summary>
        /// Clamps x by a min value.
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="a">The min value</param>
        /// <returns>A double.</returns>
        public static int ClampMin(int x, int a)
        {
            return (Math.Abs(x - a) + x + a) >> 1;
        }
        /// <summary>
        /// Clamps x between a min value and a max value.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="a">The min value</param>
        /// <param name="b">The max value</param>
        /// <returns>A double.</returns>
        public static double ClampMinMax(double x, double a, double b)
        {
            return -1.0 * Math.Abs((Math.Abs(x - a) + x + a) * 0.5 - b) * 0.5 + (Math.Abs(x - a) + x + a) * 0.25 + b * 0.5;
        }
        /// <summary>
        /// Clamps x between a min value and a max value.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="a">The min value</param>
        /// <param name="b">The max value</param>
        /// <returns>A double.</returns>
        public static int ClampMinMax(int x, int a, int b)
        {
            return (-Math.Abs(((Math.Abs(x - a) + x + a) >> 1) - b) >> 1) + ((Math.Abs(x - a) + x + a) >> 2) + (b >> 1);
        }
    }
}
