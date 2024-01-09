using BD.CourseApp.Core.Domain.Categories.Entities;

namespace BD.CourseApp.Infrastructures.Services.Outbound
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
    }
}