using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// line 元素
    /// </summary>
    public class Line : SVGDrawingElement
    {
        /// <summary>
        /// line 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Line(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new Line(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry GetBaseGeometry()
        {
            double x1 = this.GetAttributeValue<SVGDouble>("x1", false, SVGDouble.Zero).GetValue(this.SVG.Width);
            double y1 = this.GetAttributeValue<SVGDouble>("y1", false, SVGDouble.Zero).GetValue(this.SVG.Height);
            double x2 = this.GetAttributeValue<SVGDouble>("x2", false, SVGDouble.Zero).GetValue(this.SVG.Width);
            double y2 = this.GetAttributeValue<SVGDouble>("y2", false, SVGDouble.Zero).GetValue(this.SVG.Height);

            LineGeometry geometry = new LineGeometry(new Point(x1, y1), new Point(x2, y2));

            return geometry;
        }
    }
}
