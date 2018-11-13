using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 画刷
    /// </summary>
    public class SVGBrush : SVGData, ISVGBrush
    {
        /// <summary>
        /// 值
        /// </summary>
        public Brush Value { get; set; }

        /// <summary>
        /// 黑色
        /// </summary>
        public static readonly SVGBrush Black = new SVGBrush { Value = new SolidColorBrush(Colors.Black) };
    }
}
