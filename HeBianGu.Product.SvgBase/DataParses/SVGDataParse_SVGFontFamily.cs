using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGFontFamily
    /// </summary>
    public class SVGDataParse_SVGFontFamily : SVGDataParse<SVGFontFamily>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGFontFamily result = new SVGFontFamily();
            
            result.Value = new FontFamily(attribute.Value);

            attribute.Data = result;

            return true;
        }
    }
}
