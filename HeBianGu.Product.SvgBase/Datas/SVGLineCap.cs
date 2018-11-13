using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 线冒
    /// </summary>
    public class SVGLineCap : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public PenLineCap Value { get; set; }

        public static readonly SVGLineCap Butt = new SVGLineCap { Value = PenLineCap.Flat };

        public static readonly SVGLineCap Round = new SVGLineCap { Value = PenLineCap.Round };

        public static readonly SVGLineCap Square = new SVGLineCap { Value = PenLineCap.Square };
    }
}
