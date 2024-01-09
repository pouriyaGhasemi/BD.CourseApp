using BD.CourseApp.Core.Domain.Categories;
using System.Text.Json;

namespace BD.CourseApp.Infrastructures.Services.Outbound
{
    public class GetCategoryService
    {
        private readonly HttpClient _httpClient;
        //private readonly string _apiUrl;

        //use system.text.json over newtonsoft in .net 8
        public GetCategoryService(HttpClient httpClient, string apiUrl)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            //_apiUrl = apiUrl ?? throw new ArgumentNullException(nameof(apiUrl)); 
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
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
            //ToDo:Cache this in our local databse and call it just in create course.
            var categories = await GetCategories();
            var category= categories.Where(w => w.CategoryId == id).SingleOrDefault();
            if (category is null)
                throw new CategoryNotExistException($"id is {id}");
            return category;
        }


    }
}
