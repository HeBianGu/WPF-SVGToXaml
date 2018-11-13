using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGSpreadMethod
    /// </summary>
    public class SVGDataParse_SVGSpreadMethod : SVGDataParse<SVGSpreadMethod>
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
                case "pad": attribute.Data = SVGSpreadMethod.Pad; break;
                case "repeat": attribute.Data = SVGSpreadMethod.Repeat; break;
                case "reflect": attribute.Data = SVGSpreadMethod.Reflect; break;
            }

            return true;
        }
    }
}
