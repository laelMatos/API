using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.API.App_start
{
    /// <summary>
    /// Define o objeto de retorno para identificar os erros.
    /// </summary>
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (!context.ModelState.IsValid)
                {
                    List<object> errors = new List<object>();
                    foreach (var modelStateKey in context.ModelState.Keys)
                    {
                        var modelStateVal = context.ModelState[modelStateKey];
                        if (modelStateVal.Errors.Count > 0)
                        {
                            var response = new
                            {
                                ErrorField = modelStateKey,
                                Message = modelStateVal.Errors.Select(c => c.ErrorMessage).First(),
                                Success = false
                            };
                            errors.Add(response);
                        }
                    }

                    context.Result = new JsonResult(errors)
                    {
                        StatusCode = 400
                    };
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
