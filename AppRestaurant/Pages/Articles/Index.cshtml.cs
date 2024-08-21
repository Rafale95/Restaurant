using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppRestaurantBLL;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppRestaurantBOL;

namespace AppRestaurant.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Article> articles { get; set; }
        //Search
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string currentFilter { get; set; }

        //Sort
        public string articleNameSort { get; set; }
        public string articleTypeSort { get; set; }
        public string articlePriceSort { get; set; }
        public string currentSort { get; set; }

        public void OnGet(string searchString, string sortOrder)
        {
            ArticleBLL bll = new ArticleBLL(_configuration.GetConnectionString(Program.CONNECTION_STRING));

            // recherche par catégorie liste deroulante
            //Categories = new SelectList(bll.GetArticleTypes().ToList());

            ////trier par ordre asc ou desc la catégorie et le nom du produit
            //articleNameSort = String.IsNullOrEmpty(sortOrder) ? "articleName_desc" : "";
            //articleTypeSort = sortOrder == "articleType_asc" ? "articleType_desc" : "articleType_asc";
            //articlePriceSort = sortOrder == "articlePrice_asc" ? "articlePrice_desc" : "articlePrice_asc";

            //searchString recherche
            articles = bll.GetAllArticles().ToList();


        }
    }
}
