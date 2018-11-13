using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// marsk 元素
    /// </summary>
    public class Mask : SVGElement, ISVGMask
    {
        /// <summary>
        /// marsk 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Mask(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            return new Mask(this.SVG, this.Parent, this.Element);
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

        /// <summary>
        /// 获取蒙版
        /// </summary>
        /// <returns>蒙版</returns>
        private DrawingBrush GetOpacityMask()
        {
            if (this.Children.Count == 0)
                return null;

            DrawingGroup group = new DrawingGroup();

            foreach (SVGElement item in this.Children)
            {
                Drawing drawing = null;

                if (item is SVGDrawingContainerElement)
                {
                    drawing = (item as SVGDrawingContainerElement).GetDrawing();
                }
                else if (item is SVGDrawingElement)
                {
                    drawing = (item as SVGDrawingElement).GetDrawing();
                }

                if (drawing != null)
                {
                    group.Children.Add(drawing);
                }
            }

            if (group.Children.Count == 0)
                return null;

            foreach (Drawing drawing in group.Children)
            {
                ConvertColors(drawing);
            }

            DrawingBrush result = new DrawingBrush(group);

            SVGGradientUnits mask_units = this.GetAttributeValue<SVGGradientUnits>("maskUnits");

            if (mask_units != null && mask_units == SVGGradientUnits.UserSpaceOnUse)
            {
                result.ViewportUnits = mask_units.Value;
                result.Viewport = group.Bounds;
            }

            return result;
        }

        private DrawingBrush _value;

        /// <summary>
        /// 剪切路径
        /// </summary>
        public DrawingBrush Value
        {
            get
            {
                if (this._value == null)
                {
                    this._value = this.GetOpacityMask();
                }

                return this._value;
            }
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="color">颜色</param>
        private static Color ConvertColor(Color color)
        {
            float a = (float)(0.2125 * color.ScR + 0.7154 * color.ScG + 0.0721 * color.ScB) * color.ScA;

            return Color.FromScRgb(a, 0, 0, 0);
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="brush">画刷</param>
        private static void ConvertColors(Brush brush)
        {
            if (brush == null)
                return;

            if (brush is SolidColorBrush)
            {
                SolidColorBrush temp = brush as SolidColorBrush;
                temp.Color = ConvertColor(temp.Color);
            }
            else if (brush is GradientBrush)
            {
                GradientBrush temp = brush as GradientBrush;
                foreach (GradientStop stop in temp.GradientStops)
                {
                    stop.Color = ConvertColor(stop.Color);
                }
            }
            else if (brush is DrawingBrush)
            {
                ConvertColors((brush as DrawingBrush).Drawing);
            }
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="pen">画笔</param>
        private static void ConvertColors(Pen pen)
        {
            if (pen == null)
                return;

            ConvertColors(pen.Brush);
        }

        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="drawing">画刷</param>
        private static void ConvertColors(Drawing drawing)
        {
            if (drawing is DrawingGroup)
            {
                DrawingGroup drawing_group = drawing as DrawingGroup;

                foreach (Drawing item in drawing_group.Children)
                {
                    ConvertColors(item);
                }
            }
            else if (drawing is GeometryDrawing)
            {
                GeometryDrawing geometry_drawing = drawing as GeometryDrawing;

                ConvertColors(geometry_drawing.Brush);
                ConvertColors(geometry_drawing.Pen);
            }
        }
    }
}
