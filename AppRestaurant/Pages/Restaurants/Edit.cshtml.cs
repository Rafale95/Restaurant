using AppRestaurantBLL;
using AppRestaurantBOL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppRestaurant.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Restaurant restaurant { get; set; }


        public ActionResult OnGet(int id, int monid)
        {
            RestaurantBLL bll = new RestaurantBLL(_configuration.GetConnectionString(Program.CONNECTION_STRING));
            if (id == null) return NotFound();
            
            restaurant = bll.GetRestaurant(id);
            return Page();
        }
        public ActionResult OnPost(int id)
        {
            RestaurantBLL bll = new (_configuration.GetConnectionString(Program.CONNECTION_STRING));
            if (id == null) return NotFound();

            bll.EditRestaurant(restaurant); 
            return RedirectToPage("./Index");
        }
    }
}
