using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 使用坐标系
    /// </summary>
    public class SVGGradientUnits : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public BrushMappingMode Value { get; set; }

        public static readonly SVGGradientUnits ObjectBoundingBox = new SVGGradientUnits { Value = BrushMappingMode.RelativeToBoundingBox };

        public static readonly SVGGradientUnits UserSpaceOnUse = new SVGGradientUnits { Value = BrushMappingMode.Absolute };
    }
}
