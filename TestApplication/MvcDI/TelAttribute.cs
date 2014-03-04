using System.ComponentModel.DataAnnotations;

namespace MvcDI.Util
{
    public class TelAttribute : RegularExpressionAttribute
    {
        public TelAttribute() : base("^\\d{2,4}-\\d{2,4}-\\d{4}$") { }
    }
}
