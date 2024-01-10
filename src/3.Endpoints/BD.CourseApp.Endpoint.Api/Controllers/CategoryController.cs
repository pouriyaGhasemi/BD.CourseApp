using Asp.Versioning;
using BD.CourseApp.Core.ApplicationService.Categories;
using BD.CourseApp.Core.Domain.Categories.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BD.CourseApp.Endpoint.Api.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController : Controller
    {
        [HttpGet]
        public async  Task<IEnumerable<Category>?> GetAll([FromServices] GetAllCategoriesHandler handler)
        {
            return await handler.Handle();
        }
    }
}
