using StockLib;

namespace CandleStock
{
    /// <summary>
    /// The data sheet.
    /// </summary>
    partial class DataSheet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainDataSheet = new System.Windows.Forms.DataGridView();
            this.chartBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mainDataSheet
            // 
            this.mainDataSheet.AllowUserToAddRows = false;
            this.mainDataSheet.AllowUserToDeleteRows = false;
            this.mainDataSheet.AllowUserToResizeRows = false;
            this.mainDataSheet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.mainDataSheet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.mainDataSheet.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(24)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(96)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDataSheet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.mainDataSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(24)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mainDataSheet.DefaultCellStyle = dataGridViewCellStyle2;
            this.mainDataSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataSheet.EnableHeadersVisualStyles = false;
            this.mainDataSheet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.mainDataSheet.Location = new System.Drawing.Point(0, 0);
            this.mainDataSheet.Name = "mainDataSheet";
            this.mainDataSheet.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(96)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDataSheet.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.mainDataSheet.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.mainDataSheet.RowTemplate.Height = 24;
            this.mainDataSheet.RowTemplate.ReadOnly = true;
            this.mainDataSheet.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mainDataSheet.Size = new System.Drawing.Size(1107, 703);
            this.mainDataSheet.TabIndex = 0;
            // 
            // chartBindingSource
            // 
            this.chartBindingSource.DataSource = typeof(CandleStock.Chart);
            // 
            // DataSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(1107, 703);
            this.Controls.Add(this.mainDataSheet);
            this.Name = "DataSheet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DataSheet";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataSheet_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.DataSheet_VisibleChanged);
            this.Enter += new System.EventHandler(this.DataSheet_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Initalizes the custom components.
        /// </summary>
        private void InitalizeCustomComponents()
        {
            mainDataSheet.Columns.Add("Date","Date");
            mainDataSheet.Columns.Add("Open","Open");
            mainDataSheet.Columns.Add("Close","Close");
            mainDataSheet.Columns.Add("High","High");
            mainDataSheet.Columns.Add("Low","Low");
            mainDataSheet.Columns.Add("priceRange","Price Range");
            mainDataSheet.Columns.Add("bodyRange","Body Range");
            mainDataSheet.Columns.Add("upperShadow","Upper shadow");
            mainDataSheet.Columns.Add("lowerShadow","Lower Shadow");
            mainDataSheet.Columns.Add("dojiType","Doji");
            mainDataSheet.Columns.Add("dojiTypeRaw","DojiRawData");
        }

        /// <summary>
        /// The main data sheet.
        /// </summary>
        private System.Windows.Forms.DataGridView mainDataSheet;
        /// <summary>
        /// The selected stock.
        /// </summary>
        public Stock selectedStock;
        /// <summary>
        /// The chart binding source.
        /// </summary>
        private System.Windows.Forms.BindingSource chartBindingSource;
    }
}