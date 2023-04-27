using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
using System.CodeDom;
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Reflection;
using System.Text.RegularExpressions;

namespace StockLib
{
    /// <summary>
    /// The recognizer utils.
    /// </summary>
    public class RecognizerUtils
    {
        /// <summary>
        /// The type.
        /// </summary>
        public enum Type
        {
            none           = 0b0000000000000000,
            spinningTop    = 0b0000000000000001, 
            standard       = 0b0000000000000010,
            longLegged     = 0b0000000000000100,
            dragonfly      = 0b0000000000001000,
            gravestone     = 0b0000000000010000,
            whiteMarubozu  = 0b0000000000100000,
            blackMarubozu  = 0b0000000001000000,
            hammer         = 0b0000000010000000,
            hangingMan     = 0b0000000100000000,
            invertedHammer = 0b0000001000000000,
            shootingStar   = 0b0000010000000000,
            bearish        = 0b0000100000000000,
            bullish        = 0b0001000000000000,
            uptrend        = 0b0010000000000000,
            downtrend      = 0b0100000000000000,
            engulfing      = 0b1000000000000000
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">The A.</param>
        /// <param name="b">The B.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>A bool.</returns>
        public static bool Compare(double a, double b, double epsilon)
        {
            return a * (1 - epsilon) < b && b < a * (1 + epsilon);
        }

        /// <summary>
        /// Add type.
        /// </summary>
        /// <param name="currentType">The current type.</param>
        /// <param name="newType">The new type.</param>
        /// <returns>A Type.</returns>
        public static Type AddType(Type currentType, Type newType)
        {
            return currentType | newType;
        }

        /// <summary>
        /// Type has.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="tester">The tester.</param>
        /// <returns>A bool.</returns>
        public static bool TypeHas(Type types, Type tester)
        {
            return ((int)types & (int)tester) > 0;
        }

        /// <summary>
        /// Type to string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        public static string TypeToString(Type type, bool detailed = false)
        {
            List<string> retStr = new List<string>();
            if (TypeHas(type, Type.shootingStar)) retStr.Add(ShootingStar.Name());
            if (TypeHas(type, Type.invertedHammer)) retStr.Add(InvertedHammer.Name());
            if (TypeHas(type, Type.hangingMan)) retStr.Add(HangingMan.Name());
            if (TypeHas(type, Type.hammer)) retStr.Add(Hammer.Name());
            if (TypeHas(type, Type.blackMarubozu)) retStr.Add(BlackMarubozu.Name());
            if (TypeHas(type, Type.whiteMarubozu)) retStr.Add(WhiteMarubozu.Name());
            if (TypeHas(type, Type.gravestone)) retStr.Add(Gravestone.Name());
            if (TypeHas(type, Type.dragonfly)) retStr.Add(Dragonfly.Name());
            if (TypeHas(type, Type.longLegged)) retStr.Add(DojiLongLegged.Name());
            if (TypeHas(type, Type.standard)) retStr.Add(DojiStandard.Name());
            if (TypeHas(type, Type.spinningTop)) retStr.Add(SpinningTop.Name());
            if (TypeHas(type, Type.bullish) && detailed) retStr.Add("Bullish");
            if (TypeHas(type, Type.bearish) && detailed) retStr.Add("Bearish");
            if (TypeHas(type, Type.uptrend) && detailed) retStr.Add(Uptrend.Name());
            if (TypeHas(type, Type.downtrend) && detailed) retStr.Add(Downtrend.Name());
            if (TypeHas(type, Type.engulfing) && detailed) retStr.Add(Engulfment.Name());
            return "{ " + string.Join(", ", retStr) + " }";
        }
    }
    /// <summary>
    /// The single stock recognizer.
    /// </summary>
    public abstract class SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleStockRecognizer"/> class.
        /// </summary>
        public SingleStockRecognizer()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleStockRecognizer"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public SingleStockRecognizer(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Init() 
        {
            epsilon = 0;
            Open = 0;
            High = 0;
            Close = 0;
            Low = 0;
            priceRange = 0;
            bodyRange = 0;
            upperShadow = 0;
            lowerShadow = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public void Init(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            this.epsilon = epsilon;
            Open = open;
            High = high;
            Low = low;
            Close = close;

            priceRange = high - low;
            bodyRange = Math.Abs(open - close);
            upperShadow = high - Math.Max(open, close);
            lowerShadow = Math.Min(open, close) - low;
        }

        /// <summary>
        /// gos the through each recognizer.
        /// </summary>
        /// <param name="dojiType">The doji type.</param>
        /// <param name="singleBasicStock">The single basic stock.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public static RecognizerUtils.Type goThroughEachRecognizer(RecognizerUtils.Type dojiType, SingleBasicStockRecognizer singleBasicStock)
        {
            dojiType |= new SpinningTop(singleBasicStock).IsType();
            dojiType |= new DojiStandard(singleBasicStock).IsType();
            dojiType |= new DojiLongLegged(singleBasicStock).IsType();
            dojiType |= new Dragonfly(singleBasicStock).IsType();
            dojiType |= new Gravestone(singleBasicStock).IsType();
            dojiType |= new WhiteMarubozu(singleBasicStock).IsType();
            dojiType |= new BlackMarubozu(singleBasicStock).IsType();
            dojiType |= new Hammer(singleBasicStock).IsType();
            dojiType |= new HangingMan(singleBasicStock).IsType();
            dojiType |= new InvertedHammer(singleBasicStock).IsType();
            dojiType |= new ShootingStar(singleBasicStock).IsType();
            dojiType |= singleBasicStock.Open > singleBasicStock.Close ? RecognizerUtils.Type.bearish : RecognizerUtils.Type.bullish;
            return dojiType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public string Name() { return "SingleStockRecognizer"; }
        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public abstract RecognizerUtils.Type IsType();

        /// <summary>
        /// The epsilon.
        /// </summary>
        public double epsilon;
        /// <summary>
        /// The open.
        /// </summary>
        public double Open, High, Low, Close, priceRange, bodyRange, upperShadow, lowerShadow;
    }
    /// <summary>
    /// The multiple stock recognizer.
    /// </summary>
    public abstract class MultipleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleStockRecognizer"/> class.
        /// </summary>
        public MultipleStockRecognizer() 
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleStockRecognizer"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="close">The close.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        public MultipleStockRecognizer(double epsilon, double[] open, double[] close, double[] high, double[] low)
        {
            Init(epsilon,open,close,high,low);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public string Name() { return "MultipleStockRecognizer"; }
        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public abstract RecognizerUtils.Type IsType(uint index);

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            epsilon = 0;
            Open = new double[0];
            High = new double[0];
            Low = new double[0];
            Close = new double[0];
            priceRange = new double[0];
            bodyRange = new double[0];
            upperShadow = new double[0];
            lowerShadow = new double[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="close">The close.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        public void Init(double epsilon, double[] open, double[] close, double[] high, double[] low)
        {
            this.epsilon = epsilon;
            Open = open;
            High = high; 
            Low = low;
            Close = close;
            priceRange = new double[open.Length];
            bodyRange = new double[open.Length];
            upperShadow = new double[open.Length];
            lowerShadow = new double[open.Length];

            for (int i = 0; i < open.Length; i++)
            {
                priceRange[i] = high[i] - low[i];
                bodyRange[i] = Math.Abs(open[i] - close[i]);
                upperShadow[i] = Math.Abs(high[i] - Math.Max(open[i], close[i]));
                lowerShadow[i] = Math.Abs(low[i] - Math.Min(open[i], close[i]));
            }
        }

        /// <summary>
        /// gos the through each recognizer.
        /// </summary>
        /// <param name="dojiType">The doji type.</param>
        /// <param name="multiBasicStock">The multi basic stock.</param>
        /// <param name="index">The index.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public static RecognizerUtils.Type goThroughEachRecognizer(RecognizerUtils.Type dojiType, MultipleBasicStockRecognizer multiBasicStock, uint index)
        {
            dojiType |= new Uptrend(multiBasicStock).IsType(index);
            dojiType |= new Downtrend(multiBasicStock).IsType(index);
            dojiType |= new Engulfment(multiBasicStock).IsType(index);
            return dojiType;
        }

        /// <summary>
        /// The epsilon.
        /// </summary>
        public double epsilon;
        /// <summary>
        /// The open.
        /// </summary>
        public double[] Open, High, Low, Close, priceRange, bodyRange, upperShadow, lowerShadow;
    }
    /// <summary>
    /// The single basic stock recognizer.
    /// </summary>
    public class SingleBasicStockRecognizer : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleBasicStockRecognizer"/> class.
        /// </summary>
        public SingleBasicStockRecognizer()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleBasicStockRecognizer"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public SingleBasicStockRecognizer(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "SingleBasicStockRecognizer";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The multiple basic stock recognizer.
    /// </summary>
    public class MultipleBasicStockRecognizer : MultipleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleBasicStockRecognizer"/> class.
        /// </summary>
        public MultipleBasicStockRecognizer()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleBasicStockRecognizer"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public MultipleBasicStockRecognizer(double epsilon, double[] open, double[] high, double[] low, double[] close)
        {
            Init(epsilon, open, high, low, close);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "MultipleBasicStockRecognizer";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType(uint index)
        {
            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The spinning top.
    /// </summary>
    public class SpinningTop : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpinningTop"/> class.
        /// </summary>
        public SpinningTop() 
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpinningTop"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public SpinningTop(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpinningTop"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public SpinningTop(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Spinning Top";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType() 
        {
            if ((Math.Max(Open,Close) < (High - 0.25 * priceRange)) && (Math.Min(Open,Close) > (Low + 0.25 * priceRange))  &&  (priceRange <= bodyRange))
            {
                return RecognizerUtils.Type.spinningTop;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The doji standard.
    /// </summary>
    public class DojiStandard : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DojiStandard"/> class.
        /// </summary>
        public DojiStandard()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DojiStandard"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public DojiStandard(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DojiStandard"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public DojiStandard(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Standard Doji";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {

            if ((epsilon * priceRange) > bodyRange)
            {
                return RecognizerUtils.Type.standard;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The doji long legged.
    /// </summary>
    public class DojiLongLegged : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DojiLongLegged"/> class.
        /// </summary>
        public DojiLongLegged()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DojiLongLegged"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public DojiLongLegged(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DojiLongLegged"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public DojiLongLegged(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Long-legged Doji";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            if ((epsilon * priceRange > bodyRange) && (0.5 * priceRange >= lowerShadow) && (0.5 * priceRange >= upperShadow))
            {
                return RecognizerUtils.Type.longLegged;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The dragonfly.
    /// </summary>
    public class Dragonfly : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dragonfly"/> class.
        /// </summary>
        public Dragonfly()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dragonfly"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public Dragonfly(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dragonfly"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public Dragonfly(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Dragonfly";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            if (((epsilon * priceRange) > bodyRange) && ((0.5 * priceRange) < lowerShadow))
            {
                return RecognizerUtils.Type.dragonfly;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The gravestone.
    /// </summary>
    public class Gravestone : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gravestone"/> class.
        /// </summary>
        public Gravestone()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Gravestone"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public Gravestone(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Gravestone"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public Gravestone(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Gravestone";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            if (((epsilon * priceRange) > bodyRange) && ((0.5 * priceRange) < upperShadow))
            {
                return RecognizerUtils.Type.gravestone;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The white marubozu.
    /// </summary>
    public class WhiteMarubozu : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteMarubozu"/> class.
        /// </summary>
        public WhiteMarubozu()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteMarubozu"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public WhiteMarubozu(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteMarubozu"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public WhiteMarubozu(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "White Marubozu";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            if (((epsilon * priceRange) > (upperShadow + lowerShadow)) && (Close > Open))
            {
                return RecognizerUtils.Type.whiteMarubozu;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The black marubozu.
    /// </summary>
    public class BlackMarubozu : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlackMarubozu"/> class.
        /// </summary>
        public BlackMarubozu()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackMarubozu"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public BlackMarubozu(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BlackMarubozu"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public BlackMarubozu(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Black Marubozu";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            if (((epsilon * priceRange) > (upperShadow + lowerShadow)) && (Open > Close))
            {
                return RecognizerUtils.Type.blackMarubozu;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The hammer.
    /// </summary>
    public class Hammer : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hammer"/> class.
        /// </summary>
        public Hammer()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hammer"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public Hammer(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Hammer"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public Hammer(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Hammer";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            if (((High - Math.Min(Open, Close)) < (0.25 * priceRange)) && ((epsilon * priceRange) <= bodyRange))
            {
                return RecognizerUtils.Type.hammer;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The hanging man.
    /// </summary>
    public class HangingMan : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HangingMan"/> class.
        /// </summary>
        public HangingMan()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HangingMan"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public HangingMan(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HangingMan"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public HangingMan(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Hanging Man";
        }
#warning Use the IsType(int recognizerTypes) instead
        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            return RecognizerUtils.Type.none;
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <param name="recognizerTypes">The recognizer types.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public RecognizerUtils.Type IsType(int recognizerTypes)
        {
            if (((recognizerTypes & (int)RecognizerUtils.Type.hammer) > 0) && ((recognizerTypes & (int)RecognizerUtils.Type.uptrend) > 0))
            {
                return RecognizerUtils.Type.hangingMan;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The inverted hammer.
    /// </summary>
    public class InvertedHammer : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedHammer"/> class.
        /// </summary>
        public InvertedHammer()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedHammer"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public InvertedHammer(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedHammer"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public InvertedHammer(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Inverted Hammer";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType()
        {
            if (((Math.Max(Open,Close) - Low) < (0.25 * priceRange)) && ((epsilon * priceRange) <= bodyRange))
            {
                return RecognizerUtils.Type.invertedHammer;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The shooting star.
    /// </summary>
    public class ShootingStar : SingleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShootingStar"/> class.
        /// </summary>
        public ShootingStar()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShootingStar"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        /// <param name="close">The close.</param>
        public ShootingStar(double epsilon = 0, double open = 0, double high = 0, double low = 0, double close = 0)
        {
            Init(epsilon, open, high, low, close);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShootingStar"/> class.
        /// </summary>
        /// <param name="basicRecognizer">The basic recognizer.</param>
        public ShootingStar(SingleBasicStockRecognizer basicRecognizer)
        {
            Init(basicRecognizer.epsilon, basicRecognizer.Open, basicRecognizer.High, basicRecognizer.Low, basicRecognizer.Close);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Shooting Star";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <returns>A RecognizerUtils.Type.</returns>
        [Obsolete("Add a Recognizer Type parameter")]
        public override RecognizerUtils.Type IsType()
        {
            return RecognizerUtils.Type.none;
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <param name="recognizerTypes">The recognizer types.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public RecognizerUtils.Type IsType(int recognizerTypes)
        {
            if (((recognizerTypes & (int)RecognizerUtils.Type.invertedHammer) > 0) && ((recognizerTypes & (int)RecognizerUtils.Type.downtrend) > 0))
            {
                return RecognizerUtils.Type.shootingStar;
            }

            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The uptrend.
    /// </summary>
    public class Uptrend : MultipleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Uptrend"/> class.
        /// </summary>
        public Uptrend()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Uptrend"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="close">The close.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        public Uptrend(double epsilon, double[] open, double[] close, double[] high, double[] low)
        {
            Init(epsilon,open,close,high,low);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Uptrend"/> class.
        /// </summary>
        /// <param name="multipleBasicStockRecognizer">The multiple basic stock recognizer.</param>
        public Uptrend(MultipleBasicStockRecognizer multipleBasicStockRecognizer)
        {
            Init(multipleBasicStockRecognizer.epsilon, multipleBasicStockRecognizer.Open, multipleBasicStockRecognizer.Close, multipleBasicStockRecognizer.High, multipleBasicStockRecognizer.Low);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Uptrend";
        }

        /// <summary>
        /// Checks if is type.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType(uint index)
        {
            if (index == 0) { return RecognizerUtils.Type.none; }
            if (High[index] > High[index - 1] || Low[index] >= Low[index - 1])
            {
                return RecognizerUtils.Type.uptrend;
            }
            return RecognizerUtils.Type.none;
        }
    }
    /// <summary>
    /// The downtrend.
    /// </summary>
    public class Downtrend : MultipleStockRecognizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Downtrend"/> class.
        /// </summary>
        public Downtrend()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Downtrend"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="open">The open.</param>
        /// <param name="close">The close.</param>
        /// <param name="high">The high.</param>
        /// <param name="low">The low.</param>
        public Downtrend(double epsilon, double[] open, double[] close, double[] high, double[] low)
        {
            Init(epsilon, open, close, high, low);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Downtrend"/> class.
        /// </summary>
        /// <param name="multipleBasicStockRecognizer">The multiple basic stock recognizer.</param>
        public Downtrend(MultipleBasicStockRecognizer multipleBasicStockRecognizer)
        {
            Init(multipleBasicStockRecognizer.epsilon, multipleBasicStockRecognizer.Open, multipleBasicStockRecognizer.Close, multipleBasicStockRecognizer.High, multipleBasicStockRecognizer.Low);
        }
        /// <summary>
        /// Gets the name of the class
        /// </summary>
        /// <returns>A string.</returns>
        public static new string Name()
        {
            return "Downtrend";
        }

        /// <summary>
        /// Checks if the candlstick is the current class type.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A RecognizerUtils.Type.</returns>
        public override RecognizerUtils.Type IsType(uint index)
        {
            if (index == 0) { return RecognizerUtils.Type.none; }
            if (High[index] <= High[index - 1] && Low[index] < Low[index - 1])
            {
                return RecognizerUtils.Type.downtrend;
            }
            return RecognizerUtils.Type.none;
        }
    }
    public class Engulfment : MultipleStockRecognizer
    {
        public Engulfment()
        {
            Init();
        }
        public Engulfment(double epsilon, double[] open, double[] close, double[] high, double[] low)
        {
            Init(epsilon, open, close, high, low);
        }
        public Engulfment(MultipleBasicStockRecognizer multipleBasicStockRecognizer)
        {
            Init(multipleBasicStockRecognizer.epsilon, multipleBasicStockRecognizer.Open, multipleBasicStockRecognizer.Close, multipleBasicStockRecognizer.High, multipleBasicStockRecognizer.Low);
        }
        public static new string Name()
        {
            return "Engulfment";
        }
        public override RecognizerUtils.Type IsType(uint index)
        {
            if (index < 2) { return RecognizerUtils.Type.none; }
            if (Math.Max(Open[index], Close[index]) > Math.Max(Open[index - 1], Close[index - 1]) &&
                Math.Min(Open[index], Close[index]) < Math.Min(Open[index - 1], Close[index - 1]) &&
                Math.Max(Open[index - 1], Close[index - 1]) > Math.Max(Open[index - 2], Close[index - 2]) &&
                Math.Min(Open[index - 1], Close[index - 1]) < Math.Min(Open[index - 2], Close[index - 2])) 
            {
                return RecognizerUtils.Type.engulfing;
            }

            return RecognizerUtils.Type.none;
        }
    }

    /// <summary>
    /// The date type.
    /// </summary>
    public class DateType
    {
        /// <summary>
        /// Unknown date
        /// </summary>
        public const System.Byte unknown = 0;
        /// <summary>
        /// The day date width
        /// </summary>
        public const System.Byte day = 1;
        /// <summary>
        /// The week date width
        /// </summary>
        public const System.Byte week = 2;
        /// <summary>
        /// The month date width
        /// </summary>
        public const System.Byte month = 3;

        /// <summary>
        /// The date.
        /// </summary>
        public System.Byte date;
        /// <summary>
        /// Initializes a new default instance of the <see cref="DateType"/> class.
        /// </summary>
        public DateType()
        {
            date = unknown;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DateType"/> class.
        /// </summary>
        /// <param name="datetype">The datetype.</param>
        public DateType(System.Byte datetype)
        {
            date = datetype;
        }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A String.</returns>
        override public String ToString()
        {
            switch (date)
            {
                case unknown:
                    return "Unknown";
                case day:
                    return "Day";
                case week:
                    return "Week";
                case month:
                    return "Month";
                default:
                    return "unknown";
            }
        }
        /// <summary>
        /// String the to date width.
        /// </summary>
        /// <param name="dateWidth">The date width.</param>
        /// <returns>A DateType.</returns>
        static public DateType StringToDateWidth(String dateWidth)
        {
            switch (dateWidth)
            {
                case "Day":
                    return new DateType(day);
                case "Week":
                    return new DateType(week);
                case "Month":
                    return new DateType(month);
            }

            return new DateType(unknown);
        }
        /// <summary>
        /// Date the type to width.
        /// </summary>
        /// <returns>An int.</returns>
        public int DateTypeToWidth()
        {
            switch(date)
            {
                case unknown:
                    return 0;
                case day:
                    return 1;
                case week:
                    return 7;
                case month:
                    return 30;
                default:
                    return 0;
            }
        }
    }
    /// <summary>
    /// The stock.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// The name.
        /// </summary>
        public String name;
        /// <summary>
        /// The date.
        /// </summary>
        public DateTime[] Date;
        /// <summary>
        /// The open.
        /// </summary>
        public Double[] Open, High, Low, Close,
            priceRange, bodyRange, upperShadow, lowerShadow;
        /// <summary>
        /// The doji type.
        /// </summary>
        public RecognizerUtils.Type[] dojiType;
        /// <summary>
        /// The volume.
        /// </summary>
        public UInt32[] Volume;
        /// <summary>
        /// The date type.
        /// </summary>
        public DateType dateType;
        /// <summary>
        /// The epsilon.
        /// </summary>
        private double epsilon;

        /// <summary>
        /// The min date default.
        /// </summary>
        readonly DateTime minDateDefault = DateTime.MinValue;
        /// <summary>
        /// The max date default.
        /// </summary>
        readonly DateTime maxDateDefault = DateTime.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stock"/> class.
        /// </summary>
        public Stock()
        {
            Date = new DateTime[0];
            Open = new Double[0];
            High = new Double[0];
            Low = new Double[0];
            Close = new Double[0];
            Volume = new UInt32[0];
            priceRange = new Double[0];
            bodyRange = new Double[0];
            upperShadow = new double[0];
            lowerShadow = new double[0];
            name = "unknown";
            dateType = new DateType(DateType.unknown);
            dojiType = new RecognizerUtils.Type[0];
            epsilon = 0.0;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Stock"/> class.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="dateType">The date type.</param>
        public Stock(String fileName, DateType dateType)
        {
            String[] stockData;

            try
            {
                stockData = File.ReadAllText(fileName).Split('\n');
            }
            catch
            {
                Date = new DateTime[0];
                Open = new Double[0];
                High = new Double[0];
                Low = new Double[0];
                Close = new Double[0];
                Volume = new UInt32[0];
                priceRange = new Double[0];
                bodyRange = new Double[0];
                upperShadow = new double[0];
                lowerShadow = new double[0];
                name = "unknown";
                dateType = new DateType(DateType.unknown);
                dojiType = new RecognizerUtils.Type[0];
                epsilon = 0.0;
                return;
            }
            Int16 i = -1;

            Date = new DateTime[stockData.Length - 2];
            Open = new Double[stockData.Length - 2];
            High = new Double[stockData.Length - 2];
            Low = new Double[stockData.Length - 2];
            Close = new Double[stockData.Length - 2];
            Volume = new UInt32[stockData.Length - 2];
            dojiType = new RecognizerUtils.Type[stockData.Length - 2];
            priceRange = new double[stockData.Length - 2];
            bodyRange = new double[stockData.Length - 2];
            upperShadow = new double[stockData.Length - 2];
            lowerShadow = new double[stockData.Length - 2];

            epsilon = 0.15; // 15%

            // Default stock data
            if (stockData[0].Equals("\"Date\",\"Open\",\"High\",\"Low\",\"Close\",\"Volume\"\r"))
            {
                foreach (String stock in stockData)
                {
                    if (i >= 0 && i < stockData.Length - 2)
                    {
                        String[] dataStock = stock.Split(',');

                        if (dataStock.Length < 6) break;

                        String date = dataStock[0];
                        String open = dataStock[1];
                        String high = dataStock[2];
                        String low = dataStock[3];
                        String close = dataStock[4];
                        String volume = dataStock[5];

                        date = date.Trim('"');
                        string[] dateSplit = date.Split('-');
                        Date[i] = new DateTime(Int16.Parse(dateSplit[0]),
                                               Int16.Parse(dateSplit[1]),
                                               Int16.Parse(dateSplit[2]));

                        Open[i] = double.Parse(open);
                        High[i] = double.Parse(high);
                        Low[i] = double.Parse(low);
                        Close[i] = double.Parse(close);

                        Volume[i] = UInt32.Parse(volume);

                        priceRange[i] = High[i] - Low[i];
                        bodyRange[i] = Math.Abs(Open[i] - Close[i]);
                        upperShadow[i] = Math.Abs(High[i] - Math.Max(Open[i], Close[i]));
                        lowerShadow[i] = Math.Abs(Low[i] - Math.Min(Open[i], Close[i]));

                        dojiType[i] = MultipleStockRecognizer.goThroughEachRecognizer(dojiType[i], new MultipleBasicStockRecognizer(epsilon, Open, High, Low, Close), (uint)i);
                        dojiType[i] = SingleStockRecognizer.goThroughEachRecognizer(dojiType[i], new SingleBasicStockRecognizer(epsilon, Open[i], High[i], Low[i], Close[i]));
                    }

                    i++;
                }
            }

            // Yahoo! Stock data
            if (stockData[0].Equals("Date,Open,High,Low,Close,Adj Close,Volume"))
            {
                foreach(string stock in stockData)
                {
                    if (i >= 0 && i < stockData.Length - 2)
                    {
                        stock.Trim();
                        string[] dataStock = stock.Split(',');

                        string date = dataStock[0];
                        string open = dataStock[1];
                        string high = dataStock[2];
                        string low = dataStock[3];
                        string close = dataStock[4];
                     // string adj_close = dataStock[5];
                        string volume = dataStock[6];

                        string[] dateSplit = date.Split('-');

                        Date[i] = new DateTime(Int16.Parse(dateSplit[0]),
                                               Int16.Parse(dateSplit[1]),
                                               Int16.Parse(dateSplit[2]));

                        Open[i] = double.Parse(open);
                        High[i] = double.Parse(high);
                        Low[i] = double.Parse(low);
                        Close[i] = double.Parse(close);
                     // Adj_Close[i] = double.Parse(adj_close);

                        Volume[i] = UInt32.Parse(volume);

                        priceRange[i] = High[i] - Low[i];
                        bodyRange[i] = Math.Abs(Open[i] - Close[i]);
                        upperShadow[i] = Math.Abs(High[i] - Math.Max(Open[i], Close[i]));
                        lowerShadow[i] = Math.Abs(Low[i] - Math.Min(Open[i], Close[i]));

                        dojiType[i] = MultipleStockRecognizer.goThroughEachRecognizer(dojiType[i], new MultipleBasicStockRecognizer(epsilon, Open, High, Low, Close), (uint)i);
                        dojiType[i] = SingleStockRecognizer.goThroughEachRecognizer(dojiType[i], new SingleBasicStockRecognizer(epsilon, Open[i], High[i], Low[i], Close[i]));
                    }

                    i++;
                }
            }

            this.dateType = dateType;

            this.name = fileName.Split('\\').Last().Split('.').First();

        }
        /// <summary>
        /// Alls distinct.
        /// </summary>
        /// <param name="doubleList">The double list.</param>
        /// <returns>A bool.</returns>
        public static bool AllDistinct(List<double> doubleList)
        {
            return doubleList.Distinct().Count() == doubleList.Count;
        }
        /// <summary>
        /// add stock data.
        /// </summary>
        /// <param name="chartList">The chart list.</param>
        /// <param name="minDate">The min date.</param>
        /// <param name="maxDate">The max date.</param>
        public void addStockData(System.Windows.Forms.DataVisualization.Charting.Series chartList, DateTime minDate, DateTime maxDate)
        {
            for (UInt16 i = 0; i < High.Length; i++)
            {
                if (Date[i] > minDate && Date[i] < maxDate)
                {
                    chartList.Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(Date[i].ToOADate(), Low[i].ToString() + "," + High[i].ToString() + "," + Open[i].ToString() + "," + Close[i].ToString()));
                }
            }
        }
        /// <summary>
        /// add stock data.
        /// </summary>
        /// <param name="chartList">The chart list.</param>
        public void addStockData(System.Windows.Forms.DataVisualization.Charting.Series chartList)
        {
            for (UInt16 i = 0; i < High.Length; i++)
            {
                chartList.Points.Add(new System.Windows.Forms.DataVisualization.Charting.DataPoint(Date[i].ToOADate(), Low[i].ToString() + "," + High[i].ToString() + "," + Open[i].ToString() + "," + Close[i].ToString()));
            }
        }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            String printedString = "________________________________________________\n" +
                                   "|Open     |Close     |High    |Low    |Close    |";

            for (int i = 0; i < Open.Length; i++)
            {
                printedString += "|" + Open[i] + "|" + Close[i] + "|" + High[i] + "|" + Low[i] + "|" + Volume[i] + "|\n";
            }

            printedString += "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";

            return printedString;
        }
    }
    
}
