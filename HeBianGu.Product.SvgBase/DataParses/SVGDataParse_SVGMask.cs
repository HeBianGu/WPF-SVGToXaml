using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGMask
    /// </summary>
    public class SVGDataParse_SVGMask : SVGDataParse<SVGMask>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGMask result = new SVGMask();

            if (attribute.Value.StartsWith("url"))
            {
                result.Value = this.Get_URL(attribute.SVG, attribute.Value);
            }

            attribute.Data = result;

            return true;
        }

        /// <summary>
        /// 尝试从url中获取画刷
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="value">颜色</param>
        /// <returns>画刷</returns>
        private DrawingBrush Get_URL(SVG svg, string value)
        {
            value = value.Replace(" ", "").Replace("\t", "");
            value = value.Replace("url(#", "").Replace(")", "");

            if (!svg.Resource.ContainsKey(value))
                return null;

            ISVGMask mask = svg.Resource[value] as ISVGMask;

            if (mask == null || mask.Value == null)
                return null;

            return mask.Value.Clone();
        }
    }
}
