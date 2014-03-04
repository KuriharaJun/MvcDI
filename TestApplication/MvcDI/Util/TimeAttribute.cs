using System.ComponentModel.DataAnnotations;

namespace MvcDI
{
    /// <summary>
    /// 時間入力形式チェック属性
    /// </summary>
    public class TimeAttribute : RegularExpressionAttribute
    {
        public TimeAttribute()
            : base("^([0-1][0-9]|[2][0-3]):[0-5][0-9]$") { }
    }
}
