using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockLib;

namespace CandleStock
{
    /// <summary>
    /// The data sheet.
    /// </summary>
    public partial class DataSheet : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSheet"/> class.
        /// </summary>
        public DataSheet()
        {
            InitializeComponent();
            InitalizeCustomComponents();
        }
        /// <summary>
        /// Datasheet enter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The E.</param>
        private void DataSheet_Enter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Datasheet visible changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The E.</param>
        private void DataSheet_VisibleChanged(object sender, EventArgs e)
        {
            if(selectedStock != null)
            {
                mainDataSheet.Rows.Clear();
                for (int i = 0; i < selectedStock.Open.Count(); i++)
                {
                    char[] chars = Convert.ToString((int)selectedStock.dojiType[i], 2).ToCharArray();
                    Array.Reverse(chars);
                    string rawDojiData = new string(chars);
                    while (rawDojiData.Length < 16) { rawDojiData += "0"; }
                    rawDojiData = "0b" + rawDojiData;

                    mainDataSheet.Rows.Add(selectedStock.Date[i], selectedStock.Open[i], selectedStock.Close[i], 
                                           selectedStock.High[i], selectedStock.Low[i],  selectedStock.priceRange[i],
                                           selectedStock.bodyRange[i], selectedStock.upperShadow[i], selectedStock.lowerShadow[i], RecognizerUtils.TypeToString(selectedStock.dojiType[i], true), rawDojiData);
                }
            }
        }
        /// <summary>
        /// Datasheet form closing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The E.</param>
        private void DataSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                ShowInTaskbar = false;
            }
        }
    }
}
