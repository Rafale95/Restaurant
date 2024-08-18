using AppRestaurantBLL;
using AppRestaurantBOL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppRestaurant.Pages.Restaurants
{
    public class DetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public DetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Restaurant restaurant { get; set; }


        public ActionResult OnGet(int id)
        {
            RestaurantBLL bll = new RestaurantBLL(_configuration.GetConnectionString(Program.CONNECTION_STRING));

            if (id == null) return NotFound(); 

            restaurant = bll.GetRestaurant(id);
            return Page();
        }
    }
}
