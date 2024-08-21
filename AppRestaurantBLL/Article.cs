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

        public void CreateArticle(Article article)
        {
            articleDB.InsertArticleDB(article);
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return articleDB.GetAllArticles();
        }

        public IEnumerable<string> GetArticleTypes()
        {
            return articleDB.FindArticleCategoryDB();
        }
    }
}
