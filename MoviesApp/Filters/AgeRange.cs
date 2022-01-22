using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters
{
    //Фильтр, отсеивающий создание и редактирование актера младше 7 и старше 99 лет
    //выкидывает страницу с ошибкой
    public class AgeRange: Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //сначала дает сохранить данные, а потом выкидывает 400
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //не дает сохранить данные и выкидывает 400
            var formDate = DateTime.Parse(context.HttpContext.Request.Form["BirthDate"]);
            var age = DateTime.Now.Year - formDate.Year;
            if (age <= 7 || age >= 99)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}