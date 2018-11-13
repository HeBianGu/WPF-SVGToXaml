using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGDouble
    /// </summary>
    public class SVGDataParse_SVGDouble : SVGDataParse<SVGDouble>
    {
        /// <summary>
        /// 单位集合
        /// </summary>
        public static readonly string[] UNITS = new string[] { "px", "em", "pt", "ex", "pc", "in", "mm", "cm", "%" };

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGDouble result = new SVGDouble();

            foreach (string unit in UNITS)
            {
                if (attribute.Value.EndsWith(unit))
                {
                    result.Unit = unit;

                    result.Value = double.Parse(attribute.Value.Substring(0, attribute.Value.IndexOf(unit[0])));

                    attribute.Data = result;

                    return true;
                }
            }

            result.Value = double.Parse(attribute.Value);

            attribute.Data = result;

            return true;
        }
    }
}
