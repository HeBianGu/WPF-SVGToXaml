using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 字体样式
    /// </summary>
    public class SVGFontStyle : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FontStyle Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly SVGFontStyle Default = new SVGFontStyle { Value = FontStyles.Normal };
    }
}
