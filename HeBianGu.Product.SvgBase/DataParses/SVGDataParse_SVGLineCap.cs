using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGLineCap
    /// </summary>
    public class SVGDataParse_SVGLineCap : SVGDataParse<SVGLineCap>
    {

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="element">当前元素</param>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "butt": attribute.Data = SVGLineCap.Butt; break;
                case "round": attribute.Data = SVGLineCap.Round; break;
                case "square": attribute.Data = SVGLineCap.Square; break;
            }

            return true;
        }
    }
}
