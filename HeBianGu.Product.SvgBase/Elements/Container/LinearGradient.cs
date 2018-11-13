using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// linearGradient 元素
    /// </summary>
    public class LinearGradient : SVGElement, ISVGBrush
    {
        /// <summary>
        /// linearGradient 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public LinearGradient(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new LinearGradient(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取画刷
        /// </summary>
        /// <returns>画刷</returns>
        private Brush GetBrush()
        {
            double x1 = this.GetAttributeValue<SVGDouble>("x1", false, SVGDouble.Zero).GetValue(1);
            double y1 = this.GetAttributeValue<SVGDouble>("y1", false, SVGDouble.Zero).GetValue(1);
            double x2 = this.GetAttributeValue<SVGDouble>("x2", false, SVGDouble.One).GetValue(1);
            double y2 = this.GetAttributeValue<SVGDouble>("y2", false, SVGDouble.Zero).GetValue(1);
            SVGGradientUnits gradientUnits = this.GetAttributeValue<SVGGradientUnits>("gradientUnits");
            SVGTransform transform = this.GetAttributeValue<SVGTransform>("gradientTransform");
            SVGSpreadMethod spreadMethod = this.GetAttributeValue<SVGSpreadMethod>("spreadMethod");

            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new System.Windows.Point(x1, y1);
            brush.EndPoint = new System.Windows.Point(x2, y2);

            foreach (SVGElement item in this.Children)
            {
                Stop stop = item as Stop;
                if (stop == null)
                    continue;

                double offset = stop.GetAttributeValue<SVGDouble>("offset").GetValue(1);
                double opacity = stop.GetAttributeValue<SVGDouble>("stop-opacity", false, SVGDouble.One).GetValue(1);
                SVGColor color = stop.GetAttributeValue<SVGColor>("stop-color");

                GradientStop gs = new GradientStop();
                gs.Offset = offset;

                if (color != null)
                {
                    Color c = color.Value;
                    c.A = (byte)(opacity * 255);

                    gs.Color = c;
                }

                brush.GradientStops.Add(gs);
            }

            if (transform != null && transform.Value != null && transform.Value.Count > 0)
            {
                TransformGroup group = new TransformGroup();
                foreach (Transform item in transform.Value)
                {
                    group.Children.Add(item);
                }

                brush.Transform = group;
            }

            if (gradientUnits != null)
            {
                brush.MappingMode = gradientUnits.Value;
            }

            if (spreadMethod != null)
            {
                brush.SpreadMethod = spreadMethod.Value;
            }

            return brush;
        }

        private Brush _value;

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush Value
        {
            get
            {
                if (this._value == null)
                {
                    this._value = this.GetBrush();
                }

                return this._value;
            }
        }
    }
}
