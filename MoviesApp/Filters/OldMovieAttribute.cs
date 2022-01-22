using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Models;

namespace MoviesApp.Filters
{
    public class OldMovieAttribute : ValidationAttribute //продерка,когда bind данные(после отправки формы)
    //не выдаем сразу страницу с ошибкой
    {
        public OldMovieAttribute(int year)
        {
            Year = year;
        }

        public int Year { get; }

        public string GetErrorMessage() =>
            $"Movies must have a release year no later than {Year}.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var releaseYear = ((DateTime) value).Year;

            if (releaseYear < Year)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}