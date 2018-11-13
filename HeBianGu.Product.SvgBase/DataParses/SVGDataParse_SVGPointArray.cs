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
    /// SVGDashArray
    /// </summary>
    public class SVGDataParse_SVGPointArray : SVGDataParse<SVGPointArray>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGPointArray result = new SVGPointArray();

            string[] components = attribute.Value.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < components.Length; i += 2)
            {
                double x = double.Parse(components[i].Trim());
                double y = double.Parse(components[i + 1].Trim());

                result.Value.Add(new Point(x, y));
            }

            attribute.Data = result;

            return true;
        }
    }
}
