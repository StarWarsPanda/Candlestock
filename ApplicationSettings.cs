using System.Linq;
using System.Text;
using System.Configuration;
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
using static CandleStock.Pallete;
using StockLib;

namespace CandleStock
{
    public class ApplicationSettings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [DefaultSettingValue("dark")]
        public Pallete.ApplicationShade shade
        {
            get
            {
                return (Pallete.ApplicationShade)this["shade"];
            }
            set
            {
                this["shade"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("shadeDerived")]
        public string pallete
        {
            get 
            {
                return (string)this["pallete"];
            }
            set
            {
                this["pallete"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("{}")]
        public Color[] customPallete
        {
            get 
            {
                return (Color[])this["customPallete"];
            }
            set 
            {
                this["customPallete"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("None")]
        public string ticker
        {
            get 
            {
                return (string)this["ticker"];
            }
            set
            {
                this["ticker"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("StockLib.DateType.day")]
        public DateType stockWidth
        {
            get
            {
                return (DateType)this["stockWidth"];
            }
            set
            {
                this["stockWidth"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("2022-01-07")]
        public DateTime startDate
        {
            get
            {
                return (DateTime)this["startDate"];
            }
            set
            {
                this["startDate"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("2022-12-02")]
        public DateTime endDate
        {
            get
            {
                return (DateTime)this["endDate"];
            }
            set
            {
                this["endDate"] = value;
            }
        }

        //List of paths to extra Stock data
        [UserScopedSetting]
        [DefaultSettingValue("{}")]
        public string[] extraStockData
        {
            get
            {
                return (string[])this["extraStockData"];
            }
            set
            {
                this["extraStockData"] = value;
            }
        }

        private string ListToString(object[] strList)
        {
            string str = "{ ";
            foreach (object c in strList) 
            {
                str += c + ", ";
            }

            str = str.Substring(0, str.Length - 1) + " }";
            return str;
        }

        private object[] StringToList(string str)
        {
            if (!(str.StartsWith("{") && str.EndsWith("}"))) return new object[0];

            str.Trim('{','}');
            return str.Split(',');

        }

        public override string ToString()
        {
            return "[Appearance]\n" + "shade = " + shade + "\n" + "pallete = " + pallete + "\n" + "customPallete = ";
        }
    }
}
