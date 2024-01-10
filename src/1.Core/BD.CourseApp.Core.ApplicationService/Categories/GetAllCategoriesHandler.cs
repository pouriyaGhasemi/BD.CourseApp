using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Core.Domain.Categories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.CourseApp.Core.ApplicationService.Categories
{
    public class GetAllCategoriesHandler
    {
        private readonly ICategoryService _categoryService;
        public GetAllCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IEnumerable<Category>?> Handle()
        {
            var data= await _categoryService.GetCategories();
            if (data == null)
                return null;
            return data.Select(s=>s.Value).ToList();
        }
    }
}
