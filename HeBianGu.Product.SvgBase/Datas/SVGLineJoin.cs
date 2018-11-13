using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 连接线样式
    /// </summary>
    public class SVGLineJoin : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public PenLineJoin Value;

        public static readonly SVGLineJoin Miter = new SVGLineJoin { Value = PenLineJoin.Miter };

        public static readonly SVGLineJoin Round = new SVGLineJoin { Value = PenLineJoin.Round };

        public static readonly SVGLineJoin Bevel = new SVGLineJoin { Value = PenLineJoin.Bevel };
    }
}
