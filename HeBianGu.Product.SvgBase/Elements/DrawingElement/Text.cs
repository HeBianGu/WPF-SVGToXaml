using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// text 元素
    /// </summary>
    public class Text : SVGDrawingElement
    {
        /// <summary>
        /// text 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Text(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {
            foreach (XNode node in element.Nodes())
            {
                this.Read(node, this);
            }
        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new Text(this.SVG, this.Parent, this.Element);
        }

        /// <summary>
        /// 读取节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="parent">父级元素</param>
        private void Read(XNode node, SVGElement parent)
        {
            if (node.NodeType == XmlNodeType.Text)
            {
                Tspan span = new Tspan(this.SVG, parent, null);
                span.Node = node;

                this.Children.Add(span);
            }
            else if (node.NodeType == XmlNodeType.Element)
            {
                XElement element = node as XElement;

                Tspan span = new Tspan(this.SVG, parent, element);
                span.Node = node;

                this.Children.Add(span);

                foreach (XNode n in element.Nodes())
                {
                    this.Read(n, span);
                }
            }
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected override Geometry GetBaseGeometry()
        {
            GeometryGroup result = new GeometryGroup();

            double x = this.GetAttributeValue<SVGDouble>("x").GetValue(this.SVG.Width);
            double y = this.GetAttributeValue<SVGDouble>("y").GetValue(this.SVG.Height);
            double totalwidth = 0;

            foreach (Tspan item in this.Children)
            {
                if (item == null)
                    continue;

                item.BuildTextSpan(result, ref x, ref y, ref totalwidth);
            }

            return result;
        }
    }
}
