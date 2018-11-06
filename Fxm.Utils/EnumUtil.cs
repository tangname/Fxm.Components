using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Fxm.Utils
{
    public class EnumItem
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }
    }

    public class EnumUtil
    {
        /// <summary>
        /// 枚举描述的集合  
        /// 参数：第一个字典存储 枚举类的完全限定名
        /// 第二个字段存储 描述的字段和描述值
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> EnumDescriptionCollections = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 获取枚举的描述，需要为枚举添加 DescriptionAttribute<see cref="System.ComponentModel.DescriptionAttribute"/> 特性
        /// 若是不添加，则获取描述为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">枚举对象值</param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(T item)
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("传入参数必须是枚举类型");
            }

            var refName = typeof(T).FullName;
            //该枚举尚未缓存，先缓存数据
            if (!EnumDescriptionCollections.ContainsKey(refName))
            {
                var dic = new Dictionary<string, string>();

                var fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (var field in fields)
                {
                    var attr = field.GetCustomAttribute<DescriptionAttribute>(false);

                    if (attr != null)
                    {
                        dic.Add(field.Name, attr.Description);
                    }
                    else
                    {
                        dic.Add(field.Name, "");
                    }
                }

                EnumDescriptionCollections.Add(refName, dic);
            }

            return EnumDescriptionCollections[refName][item.ToString()];
        }

        /// <summary>
        /// 枚举描述的集合  
        /// 参数：第一个字典存储 枚举类的完全限定名
        /// 第二个字段存储 描述的字段和描述值
        /// </summary>
        private static Dictionary<string, List<EnumItem>> EnumsCollections = new Dictionary<string, List<EnumItem>>();
        /// <summary>
        /// 获取枚举的文本-值对，
        /// 文本通过Description特性标注，若是没有该特性，则忽略该枚举字段
        /// Int值为Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<EnumItem> GetEnums<T>()
        {
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new Exception("传入参数必须是枚举类型");
            }

            List<EnumItem> list = new List<EnumItem>();

            var refName = type.FullName;
            //该枚举尚未缓存，先缓存数据
            if (!EnumsCollections.ContainsKey(refName))
            {
                var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (var field in fields)
                {
                    var description = "";
                    var attr = field.GetCustomAttribute<DescriptionAttribute>(false);
                    if (attr != null)
                    {
                        description = attr.Description;
                    }

                    var value = field.GetValue(null);

                    var enumItem = new EnumItem()
                    {
                        Name = field.Name,
                        Value = (int)value,
                        Description = description
                    };

                    list.Add(enumItem);
                }

                EnumsCollections.Add(refName, list);
            }

            return EnumsCollections[refName];
        }
    }
}
