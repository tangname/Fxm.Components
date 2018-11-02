using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fxm.Utils
{
    /// <summary>
    /// Json转换类，使用了第三方库：Newtonsoft.Json的 
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        ///  将对象转换为Json字符串
        /// </summary>
        /// <param name="item">传入的数据，不包括一些特殊对象：如XmlNode</param>
        /// <returns>返回转换后的Json字符串</returns>
        public static string ToJson<T>(T item) where T : class, new()
        {
            if (item == null) throw new ArgumentNullException("item");

            string json = JsonConvert.SerializeObject(item, Formatting.None);

            return json;
        }

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>返回对象</returns>
        public static T ToObject<T>(string json) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
