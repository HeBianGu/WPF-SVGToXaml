using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 数据转换器
    /// </summary>
    public interface ISVGDataParse
    {
        /// <summary>
        /// 能够处理的数据类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        bool Parse(SVGAttribute attribute);
    }
}
