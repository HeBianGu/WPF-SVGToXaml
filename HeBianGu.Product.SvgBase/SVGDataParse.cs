using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// 数据转化基类
    /// </summary>
    public abstract class SVGDataParse<T> : ISVGDataParse where T : SVGData
    {
        private Type type;

        /// <summary>
        /// 能够处理的数据类型
        /// </summary>
        public Type Type
        {
            get
            {
                if (this.type == null)
                {
                    this.type = typeof(T);
                }

                return this.type;
            }
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public abstract bool Parse(SVGAttribute attribute);
    }
}
