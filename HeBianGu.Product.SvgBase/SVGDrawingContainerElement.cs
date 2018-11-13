using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVG可渲染容器元素
    /// </summary>
    public abstract class SVGDrawingContainerElement : SVGDrawingElement
    {
        /// <summary>
        /// 元素
        /// </summary>
        /// <param name="svg">根元素</param>
        /// <param name="parent">父级元素</param>
        /// <param name="element">当前元素</param>
        public SVGDrawingContainerElement(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {
        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected override Geometry GetBaseGeometry()
        {
            double opacity = this.GetAttributeValue<SVGDouble>("opacity", false, SVGDouble.One).GetValue(1);
            SVGTransform transform = this.GetAttributeValue<SVGTransform>("transform");

            GeometryGroup result = new GeometryGroup();

            if (transform != null && transform.Value != null && transform.Value.Count > 0)
            {
                TransformGroup group = new TransformGroup();

                foreach (Transform item in transform.Value)
                {
                    group.Children.Add(item);
                }

                result.Transform = group;
            }

            foreach (SVGElement element in this.Children)
            {
                Geometry drawing = null;

                if (element is SVGDrawingContainerElement)
                {
                    drawing = (element as SVGDrawingContainerElement).GetGeometry();
                }
                else if (element is SVGDrawingElement)
                {
                    drawing = (element as SVGDrawingElement).GetGeometry();
                }

                if (drawing != null)
                {
                    result.Children.Add(drawing);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected override Drawing GetBaseDrawing()
        {
            double opacity = this.GetAttributeValue<SVGDouble>("opacity", false, SVGDouble.One).GetValue(1);
            SVGTransform transform = this.GetAttributeValue<SVGTransform>("transform");

            DrawingGroup result = new DrawingGroup();

            result.Opacity = opacity;

            if (transform != null && transform.Value != null && transform.Value.Count > 0)
            {
                TransformGroup group = new TransformGroup();

                foreach (Transform item in transform.Value)
                {
                    group.Children.Add(item);
                }

                result.Transform = group;
            }

            foreach (SVGElement element in this.Children)
            {
                Drawing drawing = null;

                if (element is SVGDrawingContainerElement)
                {
                    drawing = (element as SVGDrawingContainerElement).GetDrawing();
                }
                else if (element is SVGDrawingElement)
                {
                    drawing = (element as SVGDrawingElement).GetDrawing();
                }

                if (drawing != null)
                {
                    result.Children.Add(drawing);
                }
            }

            return result;
        }
    }
}
