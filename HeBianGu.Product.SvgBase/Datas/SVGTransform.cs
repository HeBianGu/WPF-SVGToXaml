using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 变换
    /// </summary>
    public class SVGTransform : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public List<Transform> Value { get; set; } = new List<Transform>();
    }
}
