using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// g 元素
    /// </summary>
    public class G : SVGDrawingContainerElement
    {
        /// <summary>
        /// g 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public G(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new G(this.SVG, this.Parent, this.Element);
        }
    }
}
