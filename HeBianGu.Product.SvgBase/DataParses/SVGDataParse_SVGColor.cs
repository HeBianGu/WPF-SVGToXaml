using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGColor
    /// </summary>
    public class SVGDataParse_SVGColor : SVGDataParse<SVGColor>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGColor result = new SVGColor();

            if (attribute.Value.StartsWith("#"))
            {
                Color color = this.ParseColor_Well(attribute.Value);
                result.Value = color;
            }
            else if (attribute.Value.StartsWith("rgb"))
            {
                Color color = this.ParseColor_RGB(attribute.Value);
                result.Value = color;
            }
            else
            {
                Color color = (Color)ColorConverter.ConvertFromString(attribute.Value);
                result.Value = color;
            }

            attribute.Data = result;

            return true;
        }

        /// <summary>
        /// 解释颜色 -- 井号
        /// </summary>
        /// <param name="value">颜色</param>
        /// <returns>颜色</returns>
        private Color ParseColor_Well(string value)
        {
            Color result = new Color();
            result.A = 0xFF;

            string color = value.Substring(1).Trim();
            if (color.Length == 3)
            {
                result.R = Byte.Parse(String.Format("{0}{0}", color[0]), NumberStyles.HexNumber);
                result.G = Byte.Parse(String.Format("{0}{0}", color[1]), NumberStyles.HexNumber);
                result.B = Byte.Parse(String.Format("{0}{0}", color[2]), NumberStyles.HexNumber);
            }
            else if (color.Length == 6)
            {
                result.R = Byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber);
                result.G = Byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber);
                result.B = Byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber);
            }

            return result;
        }

        /// <summary>
        /// 解释颜色 -- RGB
        /// </summary>
        /// <param name="value">颜色</param>
        /// <returns>颜色</returns>
        private Color ParseColor_RGB(string value)
        {
            Color result = new Color();
            result.A = 0xFF;

            string color = value.Substring(3).Trim();

            color = color.Substring(1, color.Length - 2).Trim();

            string[] components = color.Split(',');
            for (int i = 0; i < components.Length; ++i)
            {
                components[i] = components[i].Trim();
            }

            if (components.Length == 3)
            {
                // R
                if (components[0].EndsWith("%"))
                {
                    result.R = (byte)(double.Parse(components[0].Substring(0, components[0].Length - 1).Trim()) / 255d);
                }
                else
                {
                    result.R = Byte.Parse(components[0]);
                }

                // G
                if (components[1].EndsWith("%"))
                {
                    result.G = (byte)(double.Parse(components[1].Substring(0, components[0].Length - 1).Trim()) / 255d);
                }
                else
                {
                    result.G = Byte.Parse(components[1]);
                }

                // B
                if (components[2].EndsWith("%"))
                {
                    result.B = (byte)(double.Parse(components[2].Substring(0, components[0].Length - 1).Trim()) / 255d);
                }
                else
                {
                    result.B = Byte.Parse(components[2]);
                }
            }

            return result;
        }
    }
}
