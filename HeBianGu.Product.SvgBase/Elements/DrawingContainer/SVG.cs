using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// svg 元素
    /// </summary>
    public class SVG : SVGDrawingContainerElement
    {

        // =============================================================================================
        // ================== Property ==================

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// 资源
        /// </summary>
        internal Dictionary<string, SVGElement> Resource { get; set; } = new Dictionary<string, SVGElement>();

        // =============================================================================================

        /// <summary>
        /// svg 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public SVG(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {
            this.Width = this.GetAttributeValue<SVGDouble>("width").GetValue();
            this.Height = this.GetAttributeValue<SVGDouble>("height").GetValue();
        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new SVG(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 根据ID获取SVG元素
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SVG元素</returns>
        public SVGElement GetSVGElementByID(string id)
        {
            if (!this.Resource.ContainsKey(id))
                return null;

            return this.Resource[id];
        }
    }
}
