using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 对齐方式
    /// </summary>
    public class SVGTextAnchor : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public TextAlignment Value { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly SVGTextAnchor Default = new SVGTextAnchor { Value = TextAlignment.Left };
    }
}
