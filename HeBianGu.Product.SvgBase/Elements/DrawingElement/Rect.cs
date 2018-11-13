using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// rect 元素
    /// </summary>
    public class Rect : SVGDrawingElement
    {
        /// <summary>
        /// rect 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Rect(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new Rect(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry GetBaseGeometry()
        {
            double width = this.GetAttributeValue<SVGDouble>("width", false, SVGDouble.Zero).GetValue(this.SVG.Width);
            double height = this.GetAttributeValue<SVGDouble>("height", false, SVGDouble.Zero).GetValue(this.SVG.Height);

            if (width <= 0 || height <= 0)
                return null;

            double x = this.GetAttributeValue<SVGDouble>("x", false, SVGDouble.Zero).GetValue(this.SVG.Width);
            double y = this.GetAttributeValue<SVGDouble>("y", false, SVGDouble.Zero).GetValue(this.SVG.Height);
            SVGDouble rx = this.GetAttributeValue<SVGDouble>("rx");
            SVGDouble ry = this.GetAttributeValue<SVGDouble>("ry");

            RectangleGeometry geometry = new RectangleGeometry();
            geometry.Rect = new System.Windows.Rect(x, y, width, height);
            if (rx != null && ry != null)
            {
                geometry.RadiusX = rx.GetValue(width);
                geometry.RadiusY = ry.GetValue(height);
            }
            else if (rx != null && ry == null)
            {
                geometry.RadiusX = rx.GetValue(width);
                geometry.RadiusY = geometry.RadiusX;
            }
            else if (rx == null && ry != null)
            {
                geometry.RadiusX = ry.GetValue(height);
                geometry.RadiusY = geometry.RadiusX;
            }

            return geometry;
        }
    }
}
