using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVG可渲染元素
    /// </summary>
    public abstract class SVGDrawingElement : SVGElement
    {
        /// <summary>
        /// 元素
        /// </summary>
        /// <param name="svg">根元素</param>
        /// <param name="parent">父级元素</param>
        /// <param name="element">当前元素</param>
        public SVGDrawingElement(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 获取基础几何图形
        /// </summary>
        /// <returns>基础几何图形</returns>
        protected abstract Geometry GetBaseGeometry();

        /// <summary>
        /// 获取几何图形
        /// </summary>
        /// <returns>几何图形</returns>
        public virtual Geometry GetGeometry()
        {
            SVGDisplay display = this.GetAttributeValue<SVGDisplay>("display");
            if (display == SVGDisplay.None)
            {
                return null;
            }

            Geometry geometry = this.GetBaseGeometry();

            if (geometry == null)
                return null;

            SVGTransform transform = this.GetAttributeValue<SVGTransform>("transform");

            if (transform != null && transform.Value != null && transform.Value.Count > 0)
            {
                TransformGroup group = this.GetTransformGroup(geometry);

                foreach (Transform item in transform.Value)
                {
                    group.Children.Add(item);
                }

                geometry.Transform = group;
            }

            SVGClipPath clip_path = this.GetAttributeValue<SVGClipPath>("clip-path");

            if (clip_path != null && clip_path.Value != null)
            {
                geometry = Geometry.Combine(geometry, clip_path.Value, GeometryCombineMode.Intersect, null);
            }

            return geometry;
        }

        /// <summary>
        /// 获取基础渲染
        /// </summary>
        /// <returns>基础渲染</returns>
        protected virtual Drawing GetBaseDrawing()
        {
            Geometry geometry = this.GetGeometry();

            if (geometry == null || geometry.IsEmpty())
                return null;

            if (geometry.IsEmpty())
                return null;

            Brush fill_brush = this.GetFillBrush();
            Pen pen = this.GetPen();

            if (fill_brush == null && pen == null)
                return null;

            if (fill_brush != null && geometry.GetArea() > 0)
            {
                SVGFillRule fill_rule = this.GetAttributeValue<SVGFillRule>("fill-rule", true, SVGFillRule.Nonzero);

                PathGeometry path_geometry = Geometry.Combine(geometry, Geometry.Empty, GeometryCombineMode.Exclude, null);
                path_geometry.FillRule = fill_rule.Value;
                geometry = path_geometry;
            }

            GeometryDrawing result = new GeometryDrawing(fill_brush, pen, geometry);

            return result;
        }

        /// <summary>
        /// 获取渲染
        /// </summary>
        /// <returns>渲染</returns>
        public virtual Drawing GetDrawing()
        {
            Drawing drawing = this.GetBaseDrawing();

            if (drawing == null)
                return null;

            SVGMask mask = this.GetAttributeValue<SVGMask>("mask");

            if (mask != null && mask.Value != null)
            {
                SVGTransform transform = this.GetAttributeValue<SVGTransform>("transform");

                DrawingGroup group = new DrawingGroup();
                group.OpacityMask = mask.Value;

                if (transform != null && transform.Value != null && transform.Value.Count > 0)
                {
                    TransformGroup transform_group = this.GetTransformGroup(group.OpacityMask);

                    foreach (Transform item in transform.Value)
                    {
                        transform_group.Children.Add(item);
                    }
                }

                group.Children.Add(drawing);
                drawing = group;
            }

            return drawing;
        }

        /// <summary>
        /// 获取填充画刷
        /// </summary>
        /// <returns></returns>
        protected Brush GetFillBrush()
        {
            Brush fill_brush = null;

            if (this is Text || this is Tspan)
            {
                fill_brush = this.GetAttributeValue<SVGBrush>("fill", true, SVGBrush.Black).GetValue();
            }
            else
            {
                fill_brush = this.GetAttributeValue<SVGBrush>("fill", false, null).GetValue();
            }

            if (fill_brush == null)
                return null;

            double fill_opacity = this.GetAttributeValue<SVGDouble>("fill-opacity", false, SVGDouble.One).GetValue(1);
            double opacity = this.GetAttributeValue<SVGDouble>("opacity", false, SVGDouble.One).GetValue(1);

            fill_brush.Opacity = fill_opacity * opacity;

            return fill_brush;
        }

        /// <summary>
        /// 获取边框线
        /// </summary>
        /// <returns>边框线</returns>
        protected Pen GetPen()
        {
            Brush stroke_brush = null;
            double stroke_width = 0d;

            if (this is Text || this is Tspan)
            {
                stroke_brush = this.GetAttributeValue<SVGBrush>("stroke", true, SVGBrush.Black).GetValue();
                stroke_width = this.GetAttributeValue<SVGDouble>("stroke-width", true, new SVGDouble(0.1)).GetValue();
            }
            else
            {
                stroke_brush = this.GetAttributeValue<SVGBrush>("stroke", false, null).GetValue();
                stroke_width = this.GetAttributeValue<SVGDouble>("stroke-width", false, SVGDouble.One).GetValue();
            }

            if (stroke_brush == null || stroke_width == 0d)
                return null;

            double stroke_opacity = this.GetAttributeValue<SVGDouble>("stroke-opacity", true, SVGDouble.One).GetValue(1);
            double opacity = this.GetAttributeValue<SVGDouble>("opacity", false, SVGDouble.One).GetValue(1);

            double stroke_miterlimit = this.GetAttributeValue<SVGDouble>("stroke-miterlimit", true, new SVGDouble(4)).GetValue();
            SVGDashArray stroke_dasharray = this.GetAttributeValue<SVGDashArray>("stroke-dasharray", true, null);
            SVGLineCap stroke_linecap = this.GetAttributeValue<SVGLineCap>("stroke-linecap", true, null);
            SVGLineJoin stroke_linejoin = this.GetAttributeValue<SVGLineJoin>("stroke-linejoin", true, null);

            stroke_brush.Opacity = stroke_opacity * opacity;

            Pen pen = new Pen(stroke_brush, stroke_width);

            pen.MiterLimit = stroke_miterlimit;

            if (stroke_dasharray != null && stroke_dasharray.Value != null && stroke_dasharray.Value.Count > 0)
            {
                double scale = 1d / stroke_width;
                DashStyle dash_style = new DashStyle(stroke_dasharray.Value.Select(p => p * scale), 0);
                pen.DashStyle = dash_style;
            }
            if (stroke_linecap != null)
            {
                pen.StartLineCap = stroke_linecap.Value;
                pen.EndLineCap = stroke_linecap.Value;
            }
            if (stroke_linejoin != null)
            {
                pen.LineJoin = stroke_linejoin.Value;
            }

            return pen;
        }

        /// <summary>
        /// 获取变换组
        /// </summary>
        /// <param name="geometry">路径组</param>
        /// <returns>变换组</returns>
        protected TransformGroup GetTransformGroup(Geometry geometry)
        {
            TransformGroup result = null;

            if (geometry.Transform is TransformGroup)
            {
                result = geometry.Transform as TransformGroup;
            }
            else
            {
                result = new TransformGroup();
                geometry.Transform = result;
            }

            return result;
        }

        /// <summary>
        /// 获取变换组
        /// </summary>
        /// <param name="group">绘制组</param>
        /// <returns>变换组</returns>
        protected TransformGroup GetTransformGroup(DrawingGroup group)
        {
            TransformGroup result = null;

            if (group.Transform is TransformGroup)
            {
                result = group.Transform as TransformGroup;
            }
            else
            {
                result = new TransformGroup();
                group.Transform = result;
            }

            return result;
        }

        /// <summary>
        /// 获取变换组
        /// </summary>
        /// <param name="brush">画刷</param>
        /// <returns>变换组</returns>
        protected TransformGroup GetTransformGroup(Brush brush)
        {
            TransformGroup result = null;

            if (brush.Transform is TransformGroup)
            {
                result = brush.Transform as TransformGroup;
            }
            else
            {
                result = new TransformGroup();
                brush.Transform = result;
            }

            return result;
        }


    }
}
