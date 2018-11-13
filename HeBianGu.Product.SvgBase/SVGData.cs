using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVG数据
    /// </summary>
    public class SVGData : SVGObject
    {
        /// <summary>
        /// 空
        /// </summary>
        public static SVGData Null { get; } = new SVGData();

        /// <summary>
        /// 未设置
        /// </summary>
        public static SVGData None { get; } = new SVGData();

        /// <summary>
        /// 继承
        /// </summary>
        public static SVGData Inherit { get; } = new SVGData();
    }
}
