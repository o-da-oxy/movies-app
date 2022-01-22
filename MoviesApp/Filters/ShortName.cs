using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Filters
{
    //Кастомная валидация, отсеивающая созд и ред актера с именем и фамилией менее 4 символов
    public class ShortName: ValidationAttribute
    {
        public ShortName(int nameLength)
        {
            NameLength = nameLength;
        }
        
        public int NameLength { get; }
        
        public string GetErrorMessage() => $"Must be no shorter than {NameLength}.";
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = ((string) value).Length;

            if (name < NameLength)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}