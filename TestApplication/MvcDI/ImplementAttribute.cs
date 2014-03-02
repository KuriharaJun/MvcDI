using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcDI
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ImplementAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236

        /// <summary>
        /// 実装型
        /// </summary>
        readonly Type implementType;

        // This is a positional argument
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
