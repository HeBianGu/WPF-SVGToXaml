using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// clipPath 元素
    /// </summary>
    public class ClipPath : SVGElement, ISVGClipPath
    {
        /// <summary>
        /// clipPath 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public ClipPath(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new ClipPath(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <returns></returns>
        private Geometry GetClipGeometry()
        {
            if (this.Children.Count == 0)
                return null;

            GeometryGroup group = new GeometryGroup();

            foreach (SVGElement item in this.Children)
            {
                Geometry geometry = null;

                if (item is SVGDrawingContainerElement)
                {
                    geometry = (item as SVGDrawingContainerElement).GetGeometry();
                }
                else if (item is SVGDrawingElement)
                {
                    geometry = (item as SVGDrawingElement).GetGeometry();
                }

                if (geometry != null)
                {
                    group.Children.Add(geometry);
                }
            }

            return group;
        }

        private Geometry _value;

        /// <summary>
        /// 剪切路径
        /// </summary>
        public Geometry Value
        {
            get
            {
                if (this._value == null)
                {
                    this._value = this.GetClipGeometry();
                }

                return this._value;
            }
        }
    }
}
