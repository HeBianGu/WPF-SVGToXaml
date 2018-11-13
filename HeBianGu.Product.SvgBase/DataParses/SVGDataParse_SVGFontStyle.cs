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
    /// SVGFontStyle
    /// </summary>
    public class SVGDataParse_SVGFontStyle : SVGDataParse<SVGFontStyle>
    {
        private static FontStyleConverter Converter = new FontStyleConverter();

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGFontStyle result = new SVGFontStyle();

            result.Value = (FontStyle)Converter.ConvertFromString(attribute.Value);

            attribute.Data = result;

            return true;
        }
    }
}
