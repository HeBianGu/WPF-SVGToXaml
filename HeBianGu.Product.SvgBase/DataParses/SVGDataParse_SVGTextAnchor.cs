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
    /// SVGTextAnchor
    /// </summary>
    public class SVGDataParse_SVGTextAnchor : SVGDataParse<SVGTextAnchor>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGTextAnchor result = new SVGTextAnchor();

            switch (attribute.Value)
            {
                case "start": result.Value = TextAlignment.Left; break;
                case "middle": result.Value = TextAlignment.Center; break;
                case "end": result.Value = TextAlignment.Right; break;
            }

            attribute.Data = result;

            return true;
        }
    }
}
