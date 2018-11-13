using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGClipPath
    /// </summary>
    public class SVGDataParse_SVGClipPath : SVGDataParse<SVGClipPath>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGClipPath result = new SVGClipPath();

            if (attribute.Value.StartsWith("url"))
            {
                result.Value = this.Get_URL(attribute.SVG, attribute.Value);
            }

            attribute.Data = result;

            return true;
        }

        /// <summary>
        /// 尝试从url中获取剪切路径
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="value">剪切路径值</param>
        /// <returns>画刷</returns>
        private Geometry Get_URL(SVG svg, string value)
        {
            value = value.Replace(" ", "").Replace("\t", "");
            value = value.Replace("url(#", "").Replace(")", "");

            if (!svg.Resource.ContainsKey(value))
                return null;

            ISVGClipPath clip_path = svg.Resource[value] as ISVGClipPath;

            if (clip_path == null || clip_path.Value == null)
                return null;

            return clip_path.Value.Clone();
        }
    }
}
