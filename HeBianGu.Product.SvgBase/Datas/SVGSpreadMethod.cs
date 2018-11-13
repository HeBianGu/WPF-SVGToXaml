using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 过渡方向
    /// </summary>
    public class SVGSpreadMethod : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public GradientSpreadMethod Value { get; set; }

        public static readonly SVGSpreadMethod Pad = new SVGSpreadMethod { Value = GradientSpreadMethod.Pad };

        public static readonly SVGSpreadMethod Repeat = new SVGSpreadMethod { Value = GradientSpreadMethod.Repeat };

        public static readonly SVGSpreadMethod Reflect = new SVGSpreadMethod { Value = GradientSpreadMethod.Reflect };
    }
}
