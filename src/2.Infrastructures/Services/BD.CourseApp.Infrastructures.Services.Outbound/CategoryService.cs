using BD.CourseApp.Core.Domain.Categories.Entities;
using System.Text.Json;

namespace BD.CourseApp.Infrastructures.Services.Outbound
{
    public class CategoryService: ICategoryService
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<Category>? _categories;

        //use system.text.json over newtonsoft in .net 8
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            if(_categories!=null)
                return _categories;
            //ToDo:Cache this in our local databse and call it just in create course.
            HttpResponseMessage response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                // Deserialize the JSON response into a Category object
                IEnumerable<Category>? categories = JsonSerializer.Deserialize<IEnumerable<Category>>(responseData);

                ArgumentNullException.ThrowIfNull(categories);
                return categories;
            }

            throw new HttpRequestException($"Error calling API. Status code: {response.StatusCode}");
        }
        public async Task<Category> GetCategoryById(int id)
        {
            _categories??=await GetCategories();
            var category= _categories.Where(w => w.CategoryId == id).SingleOrDefault();
            if (category is null)
                throw new CategoryNotExistException($"id is {id}");
            return category;
        }


    }
}
