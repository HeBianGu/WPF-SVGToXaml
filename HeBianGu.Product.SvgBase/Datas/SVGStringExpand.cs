using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGString 扩展
    /// </summary>
    public static class SVGStringExpand
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>值</returns>
        public static string GetValue(this SVGString value)
        {
            if (value == null)
                return null;

            return value.Value;
        }
    }
}
