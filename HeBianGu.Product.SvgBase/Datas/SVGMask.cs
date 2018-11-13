using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 蒙版
    /// </summary>
    public class SVGMask : SVGData, ISVGMask
    {
        /// <summary>
        /// 值
        /// </summary>
        public DrawingBrush Value { get; set; }
    }
}
