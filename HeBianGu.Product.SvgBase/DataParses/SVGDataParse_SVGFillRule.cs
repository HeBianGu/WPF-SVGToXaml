using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGFillRule
    /// </summary>
    public class SVGDataParse_SVGFillRule : SVGDataParse<SVGFillRule>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "nonzero": attribute.Data = SVGFillRule.Nonzero; break;
                case "evenodd": attribute.Data = SVGFillRule.Evenodd; break;
            }

            return true;
        }
    }
}
