using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// Double类型
    /// </summary>
    public class SVGDouble : SVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public double Value;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit;

        /// <summary>
        /// Double类型
        /// </summary>
        public SVGDouble()
        {

        }

        /// <summary>
        /// Double类型
        /// </summary>
        /// <param name="value">值</param>
        public SVGDouble(double value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Double类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="unit">单位</param>
        public SVGDouble(double value, string unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        /// <summary>
        /// 零
        /// </summary>
        public static SVGDouble Zero { get; } = new SVGDouble(0);

        /// <summary>
        /// 一
        /// </summary>
        public static SVGDouble One { get; } = new SVGDouble(1);
    }
}
