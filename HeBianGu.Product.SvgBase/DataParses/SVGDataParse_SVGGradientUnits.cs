using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// DataGradientUnits
    /// </summary>
    public class SVGDataParse_SVGGradientUnits : SVGDataParse<SVGGradientUnits>
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
                case "userSpaceOnUse": attribute.Data = SVGGradientUnits.UserSpaceOnUse; break;
                case "objectBoundingBox": attribute.Data = SVGGradientUnits.ObjectBoundingBox; break;
            }

            return true;
        }
    }
}
