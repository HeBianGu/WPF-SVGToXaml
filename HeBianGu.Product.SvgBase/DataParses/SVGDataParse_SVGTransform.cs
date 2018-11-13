using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVGDashArray
    /// </summary>
    public class SVGDataParse_SVGTransform : SVGDataParse<SVGTransform>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public override bool Parse(SVGAttribute attribute)
        {
            SVGTransform result = new SVGTransform();

            string[] components = attribute.Value.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < components.Length; ++i)
            {
                components[i] = components[i].Trim();
            }

            foreach (string item in components)
            {
                if (item.StartsWith("translate"))
                {
                    this.Excute_translate(result, item);
                }
                else if (item.StartsWith("scale"))
                {
                    this.Excute_scale(result, item);
                }
                else if (item.StartsWith("rotate"))
                {
                    this.Excute_rotate(result, item);
                }
                else if (item.StartsWith("skewX"))
                {
                    this.Excute_skewX(result, item);
                }
                else if (item.StartsWith("skewY"))
                {
                    this.Excute_skewY(result, item);
                }
                else if (item.StartsWith("matrix"))
                {
                    this.Excute_matrix(result, item);
                }
            }

            result.Value.Reverse();

            attribute.Data = result;

            return true;
        }

        /// <summary>
        /// 执行translate变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private void Excute_translate(SVGTransform transform, string item)
        {
            item = item.Replace("translate", "").Trim();
            item = item.Substring(1, item.Length - 1);
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double x = 0;
            double y = 0;

            if (components.Length == 1)
            {
                x = double.Parse(components[0].Trim());
            }
            else if (components.Length == 2)
            {
                x = double.Parse(components[0].Trim());
                y = double.Parse(components[1].Trim());
            }

            TranslateTransform t = new TranslateTransform();
            t.X = x;
            t.Y = y;

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行scale变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private void Excute_scale(SVGTransform transform, string item)
        {
            item = item.Replace("scale", "").Trim();
            item = item.Substring(1, item.Length - 1);
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double x = 0d;
            double y = 0d;

            if (components.Length == 1)
            {
                x = double.Parse(components[0].Trim());
                y = x;
            }
            else
            {
                x = double.Parse(components[0].Trim());
                y = double.Parse(components[1].Trim());
            }

            ScaleTransform t = new ScaleTransform();
            t.CenterX = 0.5d;
            t.CenterY = 0.5d;
            t.ScaleX = x;
            t.ScaleY = y;

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行rotate变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private void Excute_rotate(SVGTransform transform, string item)
        {
            item = item.Replace("rotate", "").Trim();
            item = item.Substring(1, item.Length - 1);
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double centerX = 0.5d;
            double centerY = 0.5d;
            double angle = 0d;

            if (components.Length == 1)
            {
                angle = double.Parse(components[0].Trim());
            }
            else if (components.Length == 3)
            {
                angle = double.Parse(components[0].Trim());
                centerX = double.Parse(components[1].Trim());
                centerY = double.Parse(components[2].Trim());
            }

            RotateTransform t = new RotateTransform();
            t.CenterX = centerX;
            t.CenterY = centerY;
            t.Angle = angle;

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行skewX变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private void Excute_skewX(SVGTransform transform, string item)
        {
            item = item.Replace("skewX", "").Trim();
            item = item.Substring(1, item.Length - 1);
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double angle = 0d;

            if (components.Length == 1)
            {
                angle = double.Parse(components[0].Trim());
            }

            SkewTransform t = new SkewTransform();
            t.AngleX = angle;

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行skewY变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private void Excute_skewY(SVGTransform transform, string item)
        {
            item = item.Replace("skewY", "").Trim();
            item = item.Substring(1, item.Length - 1);
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double angle = 0d;

            if (components.Length == 1)
            {
                angle = double.Parse(components[0].Trim());
            }

            SkewTransform t = new SkewTransform();
            t.AngleY = angle;

            transform.Value.Add(t);
        }

        /// <summary>
        /// 执行matrix变换
        /// </summary>
        /// <param name="transform">变换</param>
        /// <param name="item">项</param>
        private void Excute_matrix(SVGTransform transform, string item)
        {
            item = item.Replace("matrix", "").Trim();
            item = item.Substring(1, item.Length - 1);
            string[] components = item.Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double m11 = 0d;
            double m12 = 0d;
            double m21 = 0d;
            double m22 = 0d;
            double offsetX = 0d;
            double offsetY = 0d;

            if (components.Length == 6)
            {
                m11 = double.Parse(components[0].Trim());
                m12 = double.Parse(components[1].Trim());
                m21 = double.Parse(components[2].Trim());
                m22 = double.Parse(components[3].Trim());
                offsetX = double.Parse(components[4].Trim());
                offsetY = double.Parse(components[5].Trim());
            }

            MatrixTransform t = new MatrixTransform(m11, m12, m21, m22, offsetX, offsetY);

            transform.Value.Add(t);
        }
    }
}
