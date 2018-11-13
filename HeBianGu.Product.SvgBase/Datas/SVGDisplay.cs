using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 显示属性
    /// </summary>
    public class SVGDisplay : SVGData
    {
        public static readonly SVGDisplay Inline = new SVGDisplay();
        public static readonly SVGDisplay Block = new SVGDisplay();
        public static readonly SVGDisplay ListItem = new SVGDisplay();
        public static readonly SVGDisplay RunIn = new SVGDisplay();
        public static readonly SVGDisplay Compact = new SVGDisplay();
        public static readonly SVGDisplay Marker = new SVGDisplay();
        public static readonly SVGDisplay Table = new SVGDisplay();
        public static readonly SVGDisplay InlineTable = new SVGDisplay();
        public static readonly SVGDisplay TableRowGroup = new SVGDisplay();
        public static readonly SVGDisplay TableHeaderGroup = new SVGDisplay();
        public static readonly SVGDisplay TableFooterGroup = new SVGDisplay();
        public static readonly SVGDisplay TableRow = new SVGDisplay();
        public static readonly SVGDisplay TableColumnGroup = new SVGDisplay();
        public static readonly SVGDisplay TableColumn = new SVGDisplay();
        public static readonly SVGDisplay TableCell = new SVGDisplay();
        public static readonly SVGDisplay TableCaption = new SVGDisplay();
    }
}
