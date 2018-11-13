using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGPatternUnits
    /// </summary>
    public class SVGDataParse_SVGPatternUnits : SVGDataParse<SVGPatternUnits>
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
                case "objectBoundingBox": attribute.Data = SVGPatternUnits.objectBoundingBox; break;
                case "userSpaceOnUse": attribute.Data = SVGPatternUnits.userSpaceOnUse; break;
            }

            return true;
        }
    }
}
