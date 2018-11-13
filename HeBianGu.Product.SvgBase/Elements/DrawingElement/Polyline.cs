using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// polyline 元素
    /// </summary>
    public class Polyline : SVGDrawingElement
    {
        /// <summary>
        /// polyline 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Polyline(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new Polyline(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry GetBaseGeometry()
        {
            SVGPointArray points = this.GetAttributeValue<SVGPointArray>("points");

            if (points == null || points.Value == null || points.Value.Count == 0)
                return null;

            PathFigure figure = new PathFigure();

            figure.StartPoint = points.Value[0];
            figure.IsClosed = false;
            figure.IsFilled = true;

            for (int i = 1; i < points.Value.Count; ++i)
            {
                figure.Segments.Add(new LineSegment(points.Value[i], true));
            }

            PathGeometry geometry = new PathGeometry();
            geometry.Figures.Add(figure);

            return geometry;
        }
    }
}
