using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVG属性
    /// </summary>
    public class SVGAttribute : SVGObject
    {
        /// <summary>
        /// 值解释器集合
        /// </summary>
        static List<ISVGDataParse> Providers = new List<ISVGDataParse>();

        static SVGAttribute()
        {
            Providers.Add(new SVGDataParse_SVGDouble());
            Providers.Add(new SVGDataParse_SVGString());
            Providers.Add(new SVGDataParse_SVGBrush());
            Providers.Add(new SVGDataParse_SVGColor());
            Providers.Add(new SVGDataParse_SVGDashArray());
            Providers.Add(new SVGDataParse_SVGLineCap());
            Providers.Add(new SVGDataParse_SVGLineJoin());
            Providers.Add(new SVGDataParse_SVGFillRule());
            Providers.Add(new SVGDataParse_SVGPathData());
            Providers.Add(new SVGDataParse_SVGPointArray());
            Providers.Add(new SVGDataParse_SVGTransform());
            Providers.Add(new SVGDataParse_SVGPatternUnits());
            Providers.Add(new SVGDataParse_SVGGradientUnits());
            Providers.Add(new SVGDataParse_SVGDisplay());
            Providers.Add(new SVGDataParse_SVGClipPath());
            Providers.Add(new SVGDataParse_SVGMask());
            Providers.Add(new SVGDataParse_SVGSpreadMethod());
            Providers.Add(new SVGDataParse_SVGFontFamily());
            Providers.Add(new SVGDataParse_SVGFontWeight());
            Providers.Add(new SVGDataParse_SVGFontStyle());
            Providers.Add(new SVGDataParse_SVGTextDecoration());
            Providers.Add(new SVGDataParse_SVGTextAnchor());
            Providers.Add(new SVGDataParse_SVGBaseLineShift());
        }

        /// <summary>
        /// SVG属性
        /// </summary>
        /// <param name="svg">根元素</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        public SVGAttribute(SVG svg, XName name, string value)
        {
            this.SVG = svg;
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static void Parse<T>(SVGAttribute attribute) where T : SVGData
        {
            if (string.IsNullOrWhiteSpace(attribute.Value) || attribute.Value.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                attribute.Data = SVGData.Null;
                attribute.IsParsed = true;

                return;
            }

            if (attribute.Value.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                attribute.Data = SVGData.None;
                attribute.IsParsed = true;

                return;
            }

            Type type = typeof(T);

            foreach (ISVGDataParse provider in Providers)
            {
                if (provider.Type != type)
                    continue;

                if (!provider.Parse(attribute))
                {
                    throw new SVGDataParseException(attribute.Name, attribute.Value);
                }

                attribute.IsParsed = true;

                return;
            }

            attribute.IsParsed = true;

            throw new SVGDataParseException(attribute.Name, attribute.Value);
        }

        /// <summary>
        /// 根元素
        /// </summary>
        public SVG SVG { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public XName Name { get; private set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public SVGData Data { get; set; }

        /// <summary>
        /// 是否已经转化过
        /// </summary>
        public bool IsParsed { get; set; }

        public override string ToString()
        {
            return $"{{{this.Name}, {this.Value}}}";
        }
    }
}
