using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// radialGradient 元素
    /// </summary>
    public class RadialGradient : SVGElement, ISVGBrush
    {
        /// <summary>
        /// radialGradient 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public RadialGradient(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new RadialGradient(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取画刷
        /// </summary>
        /// <returns>画刷</returns>
        private Brush GetBrush()
        {
            double cx = this.GetAttributeValue<SVGDouble>("cx", false, new SVGDouble(0.5)).GetValue(1);
            double cy = this.GetAttributeValue<SVGDouble>("cy", false, new SVGDouble(0.5)).GetValue(1);
            double fx = this.GetAttributeValue<SVGDouble>("fx", false, new SVGDouble(0.5)).GetValue(1);
            double fy = this.GetAttributeValue<SVGDouble>("fy", false, new SVGDouble(0.5)).GetValue(1);
            double r = this.GetAttributeValue<SVGDouble>("r", false, new SVGDouble(0.5)).GetValue(1);
            SVGGradientUnits gradientUnits = this.GetAttributeValue<SVGGradientUnits>("gradientUnits");
            SVGTransform transform = this.GetAttributeValue<SVGTransform>("gradientTransform");
            SVGSpreadMethod spreadMethod = this.GetAttributeValue<SVGSpreadMethod>("spreadMethod");

            RadialGradientBrush brush = new RadialGradientBrush();
            brush.Center = new System.Windows.Point(cx, cy);
            brush.RadiusX = r;
            brush.RadiusY = r;
            brush.GradientOrigin = new System.Windows.Point(fx, fy);

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
