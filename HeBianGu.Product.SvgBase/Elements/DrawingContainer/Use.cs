using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// use 元素
    /// </summary>
    public class Use : SVGDrawingContainerElement
    {
        /// <summary>
        /// use 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Use(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new Use(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected override Drawing GetBaseDrawing()
        {
            Drawing drawing = base.GetBaseDrawing();
            if (drawing == null)
                return null;

            DrawingGroup drawing_group = drawing as DrawingGroup;
            if (drawing_group == null)
                return drawing;

            double x = this.GetAttributeValue<SVGDouble>("x").GetValue(this.SVG.Width);
            double y = this.GetAttributeValue<SVGDouble>("y").GetValue(this.SVG.Height);

            TransformGroup transform_group = this.GetTransformGroup(drawing_group);

            TranslateTransform tt = new TranslateTransform();
            tt.X = x;
            tt.Y = y;
            transform_group.Children.Add(tt);

            return drawing_group;
        }
    }
}
