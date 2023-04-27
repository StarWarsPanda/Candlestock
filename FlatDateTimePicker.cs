using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CandleStock
{
    class FlatDateTimePicker : DateTimePicker
    {
        private Color fillColor = Color.White;
        [DefaultValue(typeof(Color), "White")]
        public Color FillColor
        {
            get { return fillColor; }
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    Invalidate();
                }
            }
        }
        private Color textColor = Color.Black;
        [DefaultValue(typeof(Color), "Black")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    Invalidate();
                }
            }
        }
        private Color borderColor = Color.Gray;
        [DefaultValue(typeof(Color), "Gray")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    Invalidate();
                }
            }
        }
        private int borderWidth = 1;
        [DefaultValue(typeof(int), "1")]
        public int BorderWidth
        {
            get { return borderWidth; }
        }
        private bool dateDropDown = false;
        private Image calendarImage = null;
        public Image CalendarImage
        {
            get { return calendarImage; }
            set 
            { 
                if(calendarImage != value)
                {
                    calendarImage = value;
                    Invalidate();
                }
            }
        }
    }

}
