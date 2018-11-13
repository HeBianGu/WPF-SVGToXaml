using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGPathData
    /// </summary>
    public class SVGDataParse_SVGPathData : SVGDataParse<SVGPathData>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGPathData result = new SVGPathData();
            result.Value = Geometry.Parse(attribute.Value).Clone();

            attribute.Data = result;

            return true;
        }
    }
}
