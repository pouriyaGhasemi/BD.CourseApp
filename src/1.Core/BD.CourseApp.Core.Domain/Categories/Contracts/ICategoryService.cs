using BD.CourseApp.Core.Domain.Categories.Entities;
using System.Collections.Immutable;

namespace BD.CourseApp.Core.Domain.Categories.Contracts
{
    public interface ICategoryService
    {
        Task<ImmutableSortedDictionary<int, Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
    }
}