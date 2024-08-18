using AppRestaurantBOL;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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

        public IEnumerable<Article> InsertArticleDB(Article article)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand storedProc = new SqlCommand("USP_InsertArticle", connection);

                storedProc.CommandType = CommandType.StoredProcedure;
                storedProc.Parameters.Add(new SqlParameter("@NomArticle", article.ArticleName));
                storedProc.Parameters.Add(new SqlParameter("@TypeArticle", article.ArticleType));
                storedProc.Parameters.Add(new SqlParameter("@PrixArticle", article.ArticlePrice));

                connection.Open();
                storedProc.ExecuteNonQuery();
            }
        }

        private string GetFilteredWhereClause(string first, string second)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(first))
            {
                sb.Append(" WHERE NomArticle LIKE '%" + first + "%'");
                if (!string.IsNullOrEmpty(second))
                    sb.Append(" AND TypeArticle LIKE '%" + second + "%'");
            }
            else
            {
                sb.Append(" WHERE TypeArticle LIKE '%" + second + "%'");
            }
            return sb.ToString();
        }

        /*
      * ********************************************************
      * Récupération des articles avec ou sans filtre
      * ********************************************************
      * */
        public IEnumerable<Article> FindFilteredRestaurant(string articletName, string articleType, float articlePrice, string sortOrder)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);

                cmd.CommandText = QueryHelper.GetSelectQuery(TABLE_NAME);
                if (!string.IsNullOrEmpty(articletName))
                {
                    cmd.CommandText += " WHERE NomArticle LIKE @name";
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = "%" + articletName + "%";
                    if (!string.IsNullOrEmpty(articleType))
                    {
                        cmd.CommandText += (" AND TypeArticle = @type");
                        cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = location;
                    }
                }
                else if (!string.IsNullOrEmpty(articleType))
                {
                    cmd.CommandText += (" WHERE TypeArticle = @type");
                    cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = location;
                }

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<Article> articleList= new List<Article>();
                    if (dr == null) return articleList;

                    while (dr.Read())
                    {
                        Article article= new Article();

                        article.ArticleID = Convert.ToInt32(dr["IdArticle"]);
                        article.ArticleName = dr["NomArticle"].ToString();
                        article.ArticleType = dr["TypeArticle"].ToString();
                        article.ArticlePrice = (float)Convert.ToDouble(dr["PrixArticle"]);

                        articleList.Add(article);
                    }

                    return articleList;
                }

            }
        }

    }
}
