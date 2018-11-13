using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 数据转化错误
    /// </summary>
    public class SVGDataParseException : Exception
    {
        /// <summary>
        /// 数据转化错误
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public SVGDataParseException(XName name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public XName Name { get; private set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Value { get; private set; }
    }
}
