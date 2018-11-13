using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 虚线数组
    /// </summary>
    public class SVGDashArray : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public List<double> Value { get; set; } = new List<double>();
    }
}
