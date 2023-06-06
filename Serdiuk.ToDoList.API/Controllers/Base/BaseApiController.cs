using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Serdiuk.ToDoList.API.Controllers.Base
{
    public class BaseApiController : ControllerBase
    {
        internal string UserId => !User.Identity.IsAuthenticated
            ? string.Empty
            : User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
