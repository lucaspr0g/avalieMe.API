using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace AvalieMe.API.Controllers
{
    public class BaseController : ApiController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        protected BadRequestErrorMessageResult CustomBadRequest(ModelStateDictionary modelState)
        {
            var erros = new List<string>();

            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                    erros.Add(error.ErrorMessage);
            }

            return BadRequest(string.Join(" ", erros));
        }
    }
}