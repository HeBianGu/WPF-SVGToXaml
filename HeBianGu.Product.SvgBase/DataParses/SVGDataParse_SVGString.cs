using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGString
    /// </summary>
    public class SVGDataParse_SVGString : SVGDataParse<SVGString>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGString value = new SVGString();
            value.Value = attribute.Value;

            attribute.Data = value;

            return true;
        }
    }
}
