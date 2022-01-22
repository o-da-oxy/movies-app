using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters
{
    public class EnsureReleaseDateBeforeNow: Attribute, IActionFilter 
    //экшн фильтр на уровне методов контроллера
    {
        public void OnActionExecuting(ActionExecutingContext context) //этап до вып тела метода контроллера
        {
            var formDate = DateTime.Parse(context.HttpContext.Request.Form["ReleaseDate"]);
            if (DateTime.Now < formDate)
            {
                context.Result = new BadRequestResult();
            }
        }
        
        public void OnActionExecuted(ActionExecutedContext context) //после вып кода контроллера
        {
            
        }
    }
}