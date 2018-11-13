using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Product.SvgBase
{
    /// <summary>
    /// SVG元素
    /// </summary>
    public abstract class SVGElement : SVGObject
    {
        // =============================================================================================
        // ================== Property ==================

        /// <summary>
        /// 元素名
        /// </summary>
        public XName Name { get; private set; }

        /// <summary>
        /// 根元素
        /// </summary>
        public SVG SVG { get; private set; }

        /// <summary>
        /// 元素
        /// </summary>
        public XElement Element { get; private set; }

        /// <summary>
        /// 父级元素
        /// </summary>
        public SVGElement Parent { get; private set; }

        /// <summary>
        /// 子项
        /// </summary>
        public List<SVGElement> Children { get; } = new List<SVGElement>();

        /// <summary>
        /// 属性池
        /// </summary>
        protected Dictionary<XName, SVGAttribute> AttributePool { get; } = new Dictionary<XName, SVGAttribute>();

        /// <summary>
        /// 是否已经完成引用的初始化
        /// </summary>
        private bool IsInitedHref;

        // =============================================================================================
        // ================== Other ==================

        /// <summary>
        /// 元素
        /// </summary>
        /// <param name="svg">根元素</param>
        /// <param name="parent">父级元素</param>
        /// <param name="element">当前元素</param>
        public SVGElement(SVG svg, SVGElement parent, XElement element)
        {
            this.SVG = svg;
            this.Parent = parent;
            this.Name = element?.Name;
            this.Element = element;

            this.Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if (this.Element == null)
                return;

            foreach (XAttribute attribute in this.Element.Attributes())
            {
                if (attribute.Name == SVGNames.id && this.SVG != null)
                {
                    this.SVG.Resource[attribute.Value.Trim()] = this;
                }
                else if (attribute.Name == SVGNames.style)
                {
                    foreach (string property in attribute.Value.Split(';'))
                    {
                        string[] tokens = property.Split(':');
                        if (tokens.Length == 2)
                        {
                            string str_name = tokens[0].Trim();
                            if (str_name.Length == 0 || !(str_name[0] > 'a' && str_name[0] < 'z' || str_name[0] > 'A' && str_name[0] < 'Z'))
                                continue;
                            XName name = str_name;
                            string value = tokens[1].Trim();

                            this.AttributePool[name] = new SVGAttribute(this.SVG, name, value);
                        }
                    }

                    continue;
                }

                this.AttributePool[attribute.Name] = new SVGAttribute(this.SVG, attribute.Name, attribute.Value);
            }
        }

        /// <summary>
        /// 初始化引用
        /// </summary>
        public void InitHref()
        {
            if (this.IsInitedHref)
                return;

            SVGString href = this.GetAttributeValue<SVGString>(SVGNames.href);

            if (href != null && !string.IsNullOrWhiteSpace(href.Value))
            {
                string id = href.Value.Substring(1, href.Value.Length - 1);

                if (this.SVG != null && this.SVG.Resource.ContainsKey(id))
                {
                    SVGElement element = this.SVG.Resource[id];

                    element.InitHref();

                    if (this.Name == SVGNames.use)
                    {
                        SVGElement clone = element.Clone() as SVGElement;
                        clone.Parent = this;

                        this.Children.Add(clone);
                    }
                    else
                    {
                        foreach (var kv in element.AttributePool)
                        {
                            if (kv.Key == SVGNames.id || kv.Key == SVGNames.href)
                                continue;

                            if (this.AttributePool.ContainsKey(kv.Key))
                                continue;

                            this.AttributePool[kv.Key] = kv.Value;
                        }

                        foreach (SVGElement item in element.Children)
                        {
                            SVGElement clone = item.Clone() as SVGElement;
                            clone.Parent = this;

                            this.Children.Add(clone);
                        }
                    }
                }
            }

            foreach (SVGElement item in this.Children)
            {
                item.InitHref();
            }

            this.IsInitedHref = true;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="name">属性名</param>
        /// <param name="inherit">是否是继承属性</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性值</returns>
        public T GetAttributeValue<T>(XName name, bool inherit, T defaultValue) where T : SVGData
        {
            if (this.AttributePool.ContainsKey(name))
            {
                SVGAttribute attribute = this.AttributePool[name];

                if (!attribute.IsParsed)
                {
                    SVGAttribute.Parse<T>(attribute);
                }

                if (attribute.Data == SVGData.Null || attribute.Data == SVGData.None)
                {
                    return defaultValue;
                }

                if (attribute.Data == SVGData.Inherit)
                {
                    if (this.Parent == null)
                    {
                        return defaultValue;
                    }
                    else
                    {
                        return this.Parent.GetAttributeValue<T>(name, inherit, defaultValue);
                    }
                }

                return attribute.Data as T;
            }

            if (inherit)
            {
                if (this.Parent == null)
                {
                    return defaultValue;
                }
                else
                {
                    return this.Parent.GetAttributeValue<T>(name, inherit, defaultValue);
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public T GetAttributeValue<T>(XName name) where T : SVGData
        {
            return this.GetAttributeValue<T>(name, false, null);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">值</param>
        public void SetAttributeValue(XName name, string value)
        {
            this.AttributePool[name] = new SVGAttribute(this.SVG, name, value);
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <returns>拷贝值</returns>
        private SVGElement Clone()
        {
            SVGElement result = this.GetCloneObject();

            foreach (SVGElement item in this.Children)
            {
                result.Children.Add(item.Clone());
            }

            return result as SVGElement;
        }

        /// <summary>
        /// 获取克隆对象
        /// </summary>
        /// <returns>克隆对象</returns>
        protected abstract SVGElement GetCloneObject();

        public override string ToString()
        {
            if (this.Element == null)
                return null;
            else
                return this.Element.ToString();
        }

    }
}
