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
    /// ellipse 元素
    /// </summary>
    public class Ellipse : SVGDrawingElement
    {
        /// <summary>
        /// ellipse 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Ellipse(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new Ellipse(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry GetBaseGeometry()
        {
            double cx = this.GetAttributeValue<SVGDouble>("cx", false, SVGDouble.Zero).GetValue(this.SVG.Width);
            double cy = this.GetAttributeValue<SVGDouble>("cy", false, SVGDouble.Zero).GetValue(this.SVG.Height);
            double rx = this.GetAttributeValue<SVGDouble>("rx", false, SVGDouble.Zero).GetValue(this.SVG.Width);
            double ry = this.GetAttributeValue<SVGDouble>("ry", false, SVGDouble.Zero).GetValue(this.SVG.Height);

            EllipseGeometry geometry = new EllipseGeometry(new Point(cx, cy), rx, ry);

            return geometry;
        }
    }
}
