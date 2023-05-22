using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Serdiuk.ToDoList.API.Controllers.Base
{
    public class BaseApiController : ControllerBase
    {
        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
