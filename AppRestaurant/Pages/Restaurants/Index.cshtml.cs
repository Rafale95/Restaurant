using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppRestaurantBOL;
using AppRestaurantBLL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppRestaurant.Pages.Restaurants
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Restaurant> restaurants { get; set; }

        //Search
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        public SelectList restaurantLoc { get; set; }
        [BindProperty(SupportsGet = true)]
        public string currentFilter { get; set; }

        //Sort
        public string restaurantNameSort { get; set; }
        public string restaurantLocSort { get; set; }
        public string currentSort { get; set; }

        public void OnGet(string searchString, string sortOrder)
        {
            RestaurantBLL bll = new RestaurantBLL(_configuration.GetConnectionString(Program.CONNECTION_STRING));

            // recherche par catégorie liste deroulante
            IEnumerable<string> restaurantLocQuery = bll.GetRestaurantLocs();
            restaurantLoc = new SelectList(restaurantLocQuery.ToList());

            //trier par ordre asc ou desc la catégorie et le nom du produit
            restaurantNameSort = String.IsNullOrEmpty(sortOrder) ? "restaurantName_desc" : "";
            restaurantLocSort = sortOrder == "restaurantLoc_asc" ? "restaurantLoc_desc" : "restaurantLoc_asc";

            //searchString recherche
            restaurants = bll.GetRestaurant(searchString, sortOrder).ToList();


        }
    }
}
