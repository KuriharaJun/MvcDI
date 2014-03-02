using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcDI
{
    /// <summary>
    /// Serviceクラス注入対象識別用属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class ImplementAttribute : Attribute
    {
        /// <summary>
        /// 実装型
        /// </summary>
        readonly Type implementType = null;

        readonly bool debug = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="implementType">本番環境実装型</param>
        public ImplementAttribute(Type implementType, bool debug)
        {
            this.implementType = implementType;
            this.debug = debug;
        }

        /// <summary>
        /// 本番環境実装型
        /// </summary>
        public Type ImplementType { get { return this.implementType; } private set; }

        /// <summary>
        /// デバッグ環境実装型
        /// </summary>
        public Type DebugImplementType { get; set; }

        /// <summary>
        /// デバッグフラグ
        /// </summary>
        public bool Debug { get; private set; }
    }
}
