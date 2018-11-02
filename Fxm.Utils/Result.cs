using System;

namespace Fxm.Utils
{
    /// <summary>
    /// 结果的基础类
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Result()
        {
            Message = string.Empty;
            Sucess = false;
        }

        /// <summary>
        /// 结果消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功，默认为false
        /// </summary>
        public bool Sucess { get; set; }

        /// <summary>
        /// 结果代码，根据实际应用自定义
        /// </summary>
        public int Code { get; set; }
    }

    /// <summary>
    /// 结果的基础类
    /// </summary>
    public class Result<T> : Result
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T ResultData { get; set; }
    }
}
