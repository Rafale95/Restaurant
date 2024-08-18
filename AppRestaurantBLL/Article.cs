using AppRestaurantBOL;
using AppRestaurantDAL;

namespace AppRestaurantBLL
{
    public class ArticleBLL
    {
        private ArticleDB articleDB;

        public ArticleBLL(string connectionString)
        {
            articleDB = new ArticleDB(connectionString);
        }

        public IEnumerable<Article> GetArticle(string searchString, string sortOrder)
        {
            return articleDB.FindArticleByNameDB(searchString, sortOrder);
        }

    }
}
