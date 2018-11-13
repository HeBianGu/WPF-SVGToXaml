using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 填充方式
    /// </summary>
    public class SVGFillRule : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FillRule Value;

        public static readonly SVGFillRule Nonzero = new SVGFillRule { Value = FillRule.Nonzero };

        public static readonly SVGFillRule Evenodd = new SVGFillRule { Value = FillRule.EvenOdd };

    }
}
