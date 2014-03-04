using System.ComponentModel.DataAnnotations;

namespace MvcDI
{
    /// <summary>
    /// メール入力形式チェック属性
    /// </summary>
    public class MailAttribute : RegularExpressionAttribute
    {
        public MailAttribute()
            : base("^[a-z0-9~!$%^&*_=+}{\'?]+(\\.[a-z0-9~!$%^&*_=+}{\'?]+)*@"
            + "([a-z0-9_][a-z0-9_]*(\\.[a-z0-9_]+)*\\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])"
            + "|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))(:[0-9]{1,5})?")
        {
        }
    }
}
