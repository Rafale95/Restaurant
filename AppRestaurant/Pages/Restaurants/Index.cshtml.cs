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
        [BindProperty(SupportsGet = true)]
        public string location { get; set; }
        public SelectList Locations { get; set; }
        public string currentFilter { get; set; }

        //Sort
        public string nameSort { get; set; }
        public string locSort { get; set; }

        public string sortOrder { get; set; }


        public void OnGet(string searchString, string sortOrder)
        {
            RestaurantBLL bll = new RestaurantBLL(_configuration.GetConnectionString(Program.CONNECTION_STRING));

            // recherche par catégorie liste deroulante
            Locations = new SelectList(bll.GetRestaurantLocations().ToList());

            //trier par ordre asc ou desc la catégorie et le nom du produit
            nameSort = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            locSort = sortOrder == "locAsc" ? "locDesc" : "locAsc";

            //searchString recherche
            restaurants = bll.GetAllRestaurants(searchString, location, sortOrder).ToList();
        }
    }
}
