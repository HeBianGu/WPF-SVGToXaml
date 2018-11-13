using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGDisplay
    /// </summary>
    public class SVGDataParse_SVGDisplay : SVGDataParse<SVGDisplay>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            switch (attribute.Value)
            {
                case "inline": attribute.Data = SVGDisplay.Inline; break;
                case "block": attribute.Data = SVGDisplay.Block; break;
                case "list-item": attribute.Data = SVGDisplay.ListItem; break;
                case "run-in": attribute.Data = SVGDisplay.RunIn; break;
                case "compact": attribute.Data = SVGDisplay.Compact; break;
                case "marker": attribute.Data = SVGDisplay.Marker; break;
                case "table": attribute.Data = SVGDisplay.Table; break;
                case "inline-table": attribute.Data = SVGDisplay.InlineTable; break;
                case "table-row-group": attribute.Data = SVGDisplay.TableRowGroup; break;
                case "table-header-group": attribute.Data = SVGDisplay.TableHeaderGroup; break;
                case "table-row": attribute.Data = SVGDisplay.TableRow; break;
                case "table-column-group": attribute.Data = SVGDisplay.TableColumnGroup; break;
                case "table-column": attribute.Data = SVGDisplay.TableColumn; break;
                case "table-cell": attribute.Data = SVGDisplay.TableCell; break;
                case "table-caption": attribute.Data = SVGDisplay.TableCaption; break;
                case "none": attribute.Data = SVGDisplay.None; break;
            }

            return true;
        }
    }
}
