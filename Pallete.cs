using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleStock
{
    public class Pallete
    {
        /* Background, Foreground (Fonts), Accent Color(s)...*/
        private Color[] darkPallete = new Color[] { Color.FromArgb(12, 12, 24), Color.FromArgb(255, 255, 255), Color.FromArgb(25, 240, 50) };
        private Color[] lightPallete = new Color[] { Color.FromArgb(232, 232, 232), Color.FromArgb(0, 0, 0), Color.FromArgb(25, 100, 180) };
        public enum ApplicationShade
        {
            light,
            dark
        }

        ApplicationShade shade;

        public Pallete()
        {
            shade = ApplicationShade.dark;
        }
        public void Switch()
        {
            shade = 1 - shade;
        }
        public void setShade(ApplicationShade Shade)
        {
            shade = Shade;
        }
        public Color getBackgroundColor()
        {
            return (shade == ApplicationShade.light) ? lightPallete[0] : darkPallete[0];
        }
        public Color getForegroundColor()
        {
            return (shade == ApplicationShade.light) ? lightPallete[1] : darkPallete[1];
        }
        public Color getAccentColor()
        {
            return (shade == ApplicationShade.light) ? lightPallete[2] : darkPallete[2];
        }
        public Color getAccentColor(uint offset)
        {
            if (offset > 0) { return new Color(); }
            offset += 2;
            return (shade == ApplicationShade.light) ? lightPallete[offset] : darkPallete[offset];
        }
        public override String ToString()
        {
            switch (shade)
            {
                case ApplicationShade.light:
                    return "light";
                case ApplicationShade.dark:
                    return "dark";
                default:
                    return "unknown";
            }
        }
    }
}
