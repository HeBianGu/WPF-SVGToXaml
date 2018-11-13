using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// Double类型扩展
    /// </summary>
    public static class SVGDoubleExpand
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="v">SVGDouble</param>
        /// <param name="max">最大值</param>
        /// <returns>真实值</returns>
        public static double GetValue(this SVGDouble v, double max = 100d)
        {
            if (v == null)
                return 0d;

            double result = 0d;

            if (!string.IsNullOrWhiteSpace(v.Unit) && v.Unit.Equals("%"))
            {
                result = v.Value / 100d * max;
            }
            else
            {
                result = v.Value;
            }

            return result;
        }
    }
}