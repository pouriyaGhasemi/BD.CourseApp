using BD.CourseApp.Core.Domain.Categories.Contracts;
using BD.CourseApp.Core.Domain.Categories.Entities;
using BD.CourseApp.Core.Domain.Categories.Exceptions;
using System.Collections.Immutable;
using System.Text.Json;
using Newtonsoft;
using Newtonsoft.Json;

namespace BD.CourseApp.Infrastructures.Services.Outbound
{
    public class CategoryService: ICategoryService
    {
        private readonly HttpClient _httpClient;
        private ImmutableSortedDictionary<int,Category>? _categories;

        //ToDo:Use system.text.json over newtonsoft in .net 8
        //ToDo:Cache this in our local databse and call it just in create course.
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<ImmutableSortedDictionary<int,Category>> GetCategories()
        {
            if(_categories!=null)
                return _categories;
            
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                IEnumerable<Category>? categories = JsonConvert
                    .DeserializeObject<IEnumerable<Category>>(responseData);
                ArgumentNullException.ThrowIfNull(categories);
                return categories.ToImmutableSortedDictionary(category => category.Id, category => category);
            }
            throw new HttpRequestException($"Error calling API. Status code: {response.StatusCode}");
        }
        public async Task<Category> GetCategoryById(int id)
        {
            _categories??=await GetCategories();
            var category= _categories.GetValueOrDefault(id);
            if (category is null)
                throw new CategoryNotExistException($"id is {id}");
            return category;
        }


    }
}
