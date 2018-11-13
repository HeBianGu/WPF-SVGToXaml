using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 剪切路径
    /// </summary>
    public interface ISVGClipPath
    {
        /// <summary>
        /// 剪切路径
        /// </summary>
        Geometry Value { get; }
    }
}
