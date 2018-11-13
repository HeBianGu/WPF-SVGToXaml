using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 字体
    /// </summary>
    public class SVGFontWeight : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FontWeight Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly SVGFontWeight Default = new SVGFontWeight { Value = FontWeights.Normal };
    }
}
