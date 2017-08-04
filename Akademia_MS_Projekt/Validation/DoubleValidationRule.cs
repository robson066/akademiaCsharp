using System.Globalization;
using System.Windows.Controls;

namespace Akademia_MS_Projekt.Validation
{
    public class DoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is double && (double)value >= 0)
            {
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, "Wartość musi być liczbą większą/równą 0.");
        }
    }
}