using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 点集合
    /// </summary>
    public class SVGPointArray : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public List<Point> Value { get; set; } = new List<Point>();
    }
}
