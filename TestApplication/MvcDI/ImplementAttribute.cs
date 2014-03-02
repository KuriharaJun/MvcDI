using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcDI
{
    /// <summary>
    /// Serviceクラス注入対象識別用属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ImplementAttribute : Attribute
    {
        /// <summary>
        /// 実装型
        /// </summary>
        readonly Type implementType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="implementType">本番環境実装型</param>
        public ImplementAttribute(Type implementType)
        {
            this.implementType = implementType;
        }

        /// <summary>
        /// 本番環境実装型
        /// </summary>
        public Type ImplementType { get { return this.implementType; } private set; }

        /// <summary>
        /// デバッグ環境実装型
        /// </summary>
        public Type DebugImplementType { get; set; }
    }
}
