using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGDashArray
    /// </summary>
    public class SVGDataParse_SVGDashArray : SVGDataParse<SVGDashArray>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGDashArray result = new SVGDashArray();

            string[] components = attribute.Value.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in components)
            {
                double v = double.Parse(item.Trim());

                result.Value.Add(v);
            }

            attribute.Data = result;

            return true;
        }
    }
}
