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
    public interface ISVGBrush
    {
        /// <summary>
        /// 画刷
        /// </summary>
        Brush Value { get; }
    }
}
