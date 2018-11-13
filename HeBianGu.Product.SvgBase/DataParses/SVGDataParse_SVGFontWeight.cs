using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGFontWeight
    /// </summary>
    public class SVGDataParse_SVGFontWeight : SVGDataParse<SVGFontWeight>
    {
        private static FontWeightConverter Converter = new FontWeightConverter();

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGFontWeight result = new SVGFontWeight();

            result.Value = (FontWeight)Converter.ConvertFromString(attribute.Value);

            attribute.Data = result;

            return true;
        }
    }
}
