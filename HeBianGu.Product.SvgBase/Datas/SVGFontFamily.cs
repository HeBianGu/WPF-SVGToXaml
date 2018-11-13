using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 字体
    /// </summary>
    public class SVGFontFamily : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public FontFamily Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly SVGFontFamily Default = new SVGFontFamily { Value = new FontFamily("Microsoft YaHei") };
    }
}
