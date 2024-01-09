using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace BD.CourseApp.Endpoint.Api.Controllers
{

    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/courses")]
    public class CourseController : Controller
    {
        [HttpGet("{id}/Studetns")]
        public IActionResult Studetns()
        {
            return View();
        }
    }
}
