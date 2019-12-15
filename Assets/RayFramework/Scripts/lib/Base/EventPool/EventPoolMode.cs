using System;
namespace RayFramework
{
    [Flags]
    internal enum EventPoolMode
    {
        /// <summary>
        /// 默認事件池，必須存在一個事件處理函式。
        /// </summary>
        Default = 0,

        /// <summary>
        /// 允許不存在的函式執行。
        /// </summary>
        AllowNoHandler = 1,

        /// <summary>
        /// 允許存在許多個事件處理函數。
        /// </summary>
        AllowMultiHandler = 2,

        /// <summary>
        /// 允許存在重複的事件處理函數。
        /// </summary>
        AllowDuplicateHandler = 4,
    }
}
