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
    /// SVGBaseLineShift
    /// </summary>
    public class SVGDataParse_SVGBaseLineShift : SVGDataParse<SVGBaseLineShift>
    {
        private SVGDataParse_SVGDouble Parser = new SVGDataParse_SVGDouble();

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "baseline": attribute.Data = SVGBaseLineShift.Baseline; break;
                case "sub": attribute.Data = SVGBaseLineShift.Sub; break;
                case "super": attribute.Data = SVGBaseLineShift.Super; break;
                default: Parser.Parse(attribute); break;
            }

            return true;
        }
    }
}
