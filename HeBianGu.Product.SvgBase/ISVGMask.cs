using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 滤镜
    /// </summary>
    public interface ISVGMask
    {
        /// <summary>
        /// 值
        /// </summary>
        DrawingBrush Value { get; }
    }
}
