using AppRestaurantBOL;
using System.Data;
using System.Data.SqlClient;

namespace AppRestaurantDAL
{
    public class ArticleDB
    {
        private string connectionString;
        private static readonly string TABLE_NAME = "Article";

        public ArticleDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertArticleDB(Article article)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand storedProc = new SqlCommand("USP_InsertArticle", connection);

                storedProc.CommandType = CommandType.StoredProcedure;
                storedProc.Parameters.Add(new SqlParameter("@Nom", article.Name));
                storedProc.Parameters.Add(new SqlParameter("@Categorie", article.Category));
                storedProc.Parameters.Add(new SqlParameter("@Prix", article.Price));

                connection.Open();
                storedProc.ExecuteNonQuery();
            }
        }

        /*
      * ********************************************************
      * Récupération des articles avec ou sans filtre
      * ********************************************************
      * */
        public IEnumerable<Article> GetAllArticles()
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);

                cmd.CommandText = QueryHelper.GetSelectQuery(TABLE_NAME);

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<Article> articleList= new List<Article>();
                    if (dr == null) return articleList;

                    while (dr.Read())
                    {
                        Article article= new Article();

                        article.Id = Convert.ToInt32(dr["IdArticle"]);
                        article.Name = dr["NomArticle"].ToString();
                        article.Category = dr["TypeArticle"].ToString();
                        article.Price = (float)Convert.ToDouble(dr["PrixArticle"]);

                        articleList.Add(article);
                    }

                    return articleList;
                }

            }
        }

        public IEnumerable<string> FindArticleCategoryDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME, "TypeArticle");
                connection.Open();
                SqlCommand cmd = new SqlCommand(selectSQL, connection);

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<String> restaurantLocations = new List<string>();
                    if (dr == null) return restaurantLocations;

                    while (dr.Read())
                    {
                        restaurantLocations.Add(dr["TypeArticle"].ToString());
                    }

                    return restaurantLocations;
                }
            }
        }
    }
}
