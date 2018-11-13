using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// Double类型扩展
    /// </summary>
    public static class SVGBrushExpand
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="brush">画刷</param>
        /// <returns>画刷</returns>
        public static Brush GetValue(this ISVGBrush brush)
        {
            if (brush == null)
                return null;

            return brush.Value;
        }
    }
}
