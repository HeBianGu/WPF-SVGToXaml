using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 读取器
    /// </summary>
    public static class SVGReader
    {
        /// <summary>
        /// 读取SVG文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>SVG元素</returns>
        public static SVG Read(string path)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                return Read(fs);
            }
        }

        /// <summary>
        /// 读取SVG文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns>SVG元素</returns>
        public static SVG Read(System.IO.Stream stream)
        {
            XElement element = XElement.Load(stream);

            SVG svg = new SVG(null, null, element);

            foreach (XElement item in element.Elements())
            {
                Read(svg, svg, item);
            }

            svg.InitHref();

            return svg;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="root">根</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        private static void Read(SVG root, SVGElement parent, XElement element)
        {
            string name = element.Name.LocalName.Trim();

            SVGElement svg_element = null;

            switch (name)
            {
                // container elements
                case "clipPath": svg_element = new ClipPath(root, parent, element); break;
                case "defs": svg_element = new Defs(root, parent, element); break;
                case "linearGradient": svg_element = new LinearGradient(root, parent, element); break;
                case "mask": svg_element = new Mask(root, parent, element); break;
                case "radialGradient": svg_element = new RadialGradient(root, parent, element); break;

                // drawing container elements
                case "g": svg_element = new G(root, parent, element); break;
                case "marker": svg_element = new Marker(root, parent, element); break;
                case "use": svg_element = new Use(root, parent, element); break;

                // drawing elements
                case "circle": svg_element = new Circle(root, parent, element); break;
                case "ellipse": svg_element = new Ellipse(root, parent, element); break;
                case "line": svg_element = new Line(root, parent, element); break;
                case "path": svg_element = new Path(root, parent, element); break;
                case "polygon": svg_element = new Polygon(root, parent, element); break;
                case "polyline": svg_element = new Polyline(root, parent, element); break;
                case "rect": svg_element = new Rect(root, parent, element); break;
                case "text": svg_element = new Text(root, parent, element); break;

                // elements
                case "stop": svg_element = new Stop(root, parent, element); break;
                case "symbol": svg_element = new Symbol(root, parent, element); break;

                // default
                default: return;
            }

            parent.Children.Add(svg_element);

            foreach (XElement item in element.Elements())
            {
                Read(root, svg_element, item);
            }
        }
    }
}
