using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 文本定位
    /// </summary>
    public class SVGBaseLineShift : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public SVGDouble Value { get; set; }

        public static readonly SVGBaseLineShift Baseline = new SVGBaseLineShift();

        public static readonly SVGBaseLineShift Sub = new SVGBaseLineShift();

        public static readonly SVGBaseLineShift Super = new SVGBaseLineShift();
    }
}
