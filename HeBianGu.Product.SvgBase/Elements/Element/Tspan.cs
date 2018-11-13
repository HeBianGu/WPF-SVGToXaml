using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// tspan 元素
    /// </summary>
    public class Tspan : SVGElement
    {
        /// <summary>
        /// path 元素
        /// </summary>
        /// <param name="svg">SVG</param>
        /// <param name="parent">父级</param>
        /// <param name="element">当前元素</param>
        public Tspan(SVG svg, SVGElement parent, XElement element) : base(svg, parent, element)
        {

        }

        /// <summary>
        /// 节点
        /// </summary>
        public XNode Node { get; set; }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected override SVGElement GetCloneObject()
        {
            Tspan result = new Tspan(this.SVG, this.Parent, this.Element);

            result.Node = this.Node;

            return result;
        }
        
        /// <summary>
        /// 构建文本形状
        /// </summary>
        /// <param name="group">文本形状组</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="totalwidth">总宽度</param>
        public void BuildTextSpan(GeometryGroup group, ref double x, ref double y, ref double totalwidth)
        {
            if (this.Node.NodeType == XmlNodeType.Element)
            {
                foreach (Tspan item in this.Children)
                {
                    if (item == null)
                        continue;

                    item.BuildTextSpan(group, ref x, ref y, ref totalwidth);
                }

                return;
            }

            XText xtext = this.Node as XText;
            if (xtext == null)
                return;

            string text = xtext.Value;
            if (string.IsNullOrWhiteSpace(text))
                return;

            double baseline = y;

            SVGBaseLineShift baseline_shift = this.GetAttributeValue<SVGBaseLineShift>("baseline-shift", true, null);
            double font_size = this.GetAttributeValue<SVGDouble>("font-size", true, new SVGDouble(12)).GetValue(12);

            if (baseline_shift != null)
            {
                if (baseline_shift == SVGBaseLineShift.Sub)
                {
                    baseline += font_size * 0.5;
                }
                else if (baseline_shift == SVGBaseLineShift.Super)
                {
                    baseline -= font_size * 1.25;
                }
                else if (baseline_shift != null)
                {
                    baseline += font_size * baseline_shift.Value.GetValue(font_size);
                }
            }

            Geometry geometry = this.BuildGlyphRun(text, x, baseline, ref totalwidth);

            group.Children.Add(geometry);

            x += totalwidth;
        }

        /// <summary>
        /// 构建文本形状
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="totalwidth">总宽度</param>
        /// <returns>形状</returns>
        private Geometry BuildGlyphRun(string text, double x, double y, ref double totalwidth)
        {
            double baseline = y;

            SVGBaseLineShift baseline_shift = this.GetAttributeValue<SVGBaseLineShift>("baseline-shift", true, null);
            double font_size = this.GetAttributeValue<SVGDouble>("font-size", true, new SVGDouble(12)).GetValue(12);
            SVGFontFamily font_family = this.GetAttributeValue<SVGFontFamily>("font-family", true, SVGFontFamily.Default);
            SVGFontWeight font_weight = this.GetAttributeValue<SVGFontWeight>("font-weight", true, SVGFontWeight.Default);
            SVGFontStyle font_style = this.GetAttributeValue<SVGFontStyle>("font-style", true, SVGFontStyle.Default);
            SVGTextAnchor text_anchor = this.GetAttributeValue<SVGTextAnchor>("text-anchor", true, SVGTextAnchor.Default);
            SVGTextDecoration text_decoration = this.GetAttributeValue<SVGTextDecoration>("text-decoration", true, null);
            double letter_spacing = this.GetAttributeValue<SVGDouble>("letter-spacing", true, SVGDouble.Zero).GetValue(12);
            double word_spacing = this.GetAttributeValue<SVGDouble>("word-spacing", true, SVGDouble.Zero).GetValue(12);

            GlyphRun glyphs = null;

            Typeface type_face = new Typeface(font_family.Value, font_style.Value, font_weight.Value, FontStretch.FromOpenTypeStretch(9), SVGFontFamily.Default.Value);
            GlyphTypeface glyph_type_face;

            if (!type_face.TryGetGlyphTypeface(out glyph_type_face))
            {
                return null;
            }

            glyphs = new GlyphRun();

            ((System.ComponentModel.ISupportInitialize)glyphs).BeginInit();

            glyphs.GlyphTypeface = glyph_type_face;
            glyphs.FontRenderingEmSize = font_size;
            List<char> text_chars = new List<char>();
            List<ushort> glyph_indices = new List<ushort>();
            List<double> advance_widths = new List<double>();
            totalwidth = 0;
            for (int i = 0; i < text.Length; ++i)
            {
                char c = text[i];
                int code_point = c;
                ushort glyph_index;
                if (!glyph_type_face.CharacterToGlyphMap.TryGetValue(code_point, out glyph_index))
                    continue;

                text_chars.Add(c);
                double glyph_width = glyph_type_face.AdvanceWidths[glyph_index];
                glyph_indices.Add(glyph_index);
                advance_widths.Add(glyph_width * font_size + letter_spacing);
                if (char.IsWhiteSpace(c))
                {
                    advance_widths[advance_widths.Count - 1] += word_spacing;
                }
                totalwidth += advance_widths[advance_widths.Count - 1];
            }
            glyphs.Characters = text_chars.ToArray();
            glyphs.GlyphIndices = glyph_indices.ToArray();
            glyphs.AdvanceWidths = advance_widths.ToArray();

            double alignmentoffset = 0;
            if (text_anchor.Value == TextAlignment.Center)
            {
                alignmentoffset = totalwidth / 2;
            }
            else if (text_anchor.Value == TextAlignment.Right)
            {
                alignmentoffset = totalwidth;
            }

            glyphs.BaselineOrigin = new Point(x - alignmentoffset, baseline);

            ((System.ComponentModel.ISupportInitialize)glyphs).EndInit();

            GeometryGroup result = new GeometryGroup();

            result.Children.Add(glyphs.BuildGeometry());

            if (text_decoration != null)
            {
                double decoration_pos = 0;
                double decoration_thinkess = 0;

                if (text_decoration.Value == TextDecorationLocation.Strikethrough)
                {
                    decoration_pos = baseline - (type_face.StrikethroughPosition * font_size);
                    decoration_thinkess = type_face.StrikethroughThickness * font_size;
                }
                else if (text_decoration.Value == TextDecorationLocation.Underline)
                {
                    decoration_pos = baseline - (type_face.UnderlinePosition * font_size);
                    decoration_thinkess = type_face.UnderlineThickness * font_size;
                }
                else if (text_decoration.Value == TextDecorationLocation.OverLine)
                {
                    decoration_pos = baseline - font_size;
                    decoration_thinkess = type_face.StrikethroughThickness * font_size;
                }

                System.Windows.Rect bounds = new System.Windows.Rect(result.Bounds.Left, decoration_pos, result.Bounds.Width, decoration_thinkess);

                result.Children.Add(new RectangleGeometry(bounds));
            }

            return result;
        }
    }
}