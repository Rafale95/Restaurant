using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppRestaurantBOL;
using AppRestaurantBLL;
using Microsoft.Extensions.Configuration;
using AppRestaurantDAL;
using System.Configuration;

namespace AppRestaurant.Pages.Restaurants
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Restaurant restaurant { get; set; }

        public ActionResult OnPost()
        {
            RestaurantBLL objRestaurant = new RestaurantBLL(_configuration.GetConnectionString("RestaurantContext"));
            objRestaurant.CreateRestaurant(restaurant);
            return RedirectToPage("./index");
        }
    }
}
