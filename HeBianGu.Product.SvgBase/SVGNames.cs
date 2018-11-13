using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 名称集合
    /// </summary>
    public static class SVGNames
    {
        // =============================================================================================
        // ================== Element ==================

        public static XName use { get; } = XName.Get("use");

        // =============================================================================================
        // ================== Attribute ==================

        public static XName id { get; } = XName.Get("id");

        public static XName href { get; } = XName.Get("href", SVGNamespaces.xlink);

        public static XName style { get; } = XName.Get("style");
    }
}
