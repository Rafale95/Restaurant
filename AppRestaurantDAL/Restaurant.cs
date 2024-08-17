using AppRestaurantBOL;
using System.Data.SqlClient;
using System.Data;

namespace AppRestaurantDAL
{
    public class RestaurantDB
    {
        private readonly string connectionString;
        private static readonly string TABLE_NAME = "Restaurant";
        public RestaurantDB(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        /*
       * ********************************************************
       * Création d'un Restaurant de la page Restaurants/create
       * ********************************************************
       * */
        public void CreateRestaurantDB(Restaurant restaurant)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertRestaurant", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@RestaurantName", restaurant.RestaurantName));
                    cmd.Parameters.Add(new SqlParameter("@RestaurantLoc", restaurant.RestaurantLoc));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

                /*
      * ********************************************************
      * Récupération des restaurants avec ou sans filtre
      * ********************************************************
      * */
        public IEnumerable<Restaurant> GetRestaurantDB(string searchString, string sortOrder)
        {
            SqlConnection con = new SqlConnection(connectionString);

            string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME) +
                QueryHelper.GetStringFilteredSelectQuery("NomRestaurant", searchString) +
                QueryHelper.GetPartialOrderedSelectQuery("LocRestaurant", sortOrder == "RestaurantLoc_desc");

            con.Open();
            SqlCommand cmd = new SqlCommand (selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();

            List<Restaurant> restaurantList = new List<Restaurant>();

            if (dr != null)
                while (dr.Read())
                {
                    Restaurant restaurant = new Restaurant();

                    restaurant.RestaurantID = Convert.ToInt32(dr["IdRestaurant"]);
                    restaurant.RestaurantName = dr["NomRestaurant"].ToString();
                    restaurant.RestaurantLoc = dr["LocRestaurant"].ToString();

                    restaurantList.Add(restaurant);
                }

            return restaurantList;

        }

        /*
        * ********************************************************
        * Edition d'un produit de la page Products/edit
        * ********************************************************
        * */

        public void EditRestaurantDB(Restaurant restaurant)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("USP_UpdateRestaurant", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@RestaurantId", restaurant.RestaurantID));
                    cmd.Parameters.Add(new SqlParameter("@RestaurantName", restaurant.RestaurantName));
                    cmd.Parameters.Add(new SqlParameter("@RestaurantLoc", restaurant.RestaurantLoc));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                {
                    throw ex;
                }
            }
        }

         /*
         * ********************************************************
         * Affiche les localistations
         * ********************************************************
         * */

        public IEnumerable<String> GetRestaurantLocsDB()
        {

            SqlConnection con = new SqlConnection(connectionString);
            string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME, "LocRestaurant");
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();

            List<String> restaurantLocList = new List<string>();

            if (dr == null) return restaurantLocList;
            
            while (dr.Read())
            {
                restaurantLocList.Add(dr["LocRestaurant"].ToString());
            }
          
            con.Close();
            return restaurantLocList;
        }

        /*
         * ********************************************************
         * Affiche un seul restaurant
         * ********************************************************
         * */

        public Restaurant GetRestaurantDB (int restaurantId)
        {
            SqlConnection con = new SqlConnection(connectionString);

            string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME);
            con.Open();

            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();
            Restaurant restaurant = new Restaurant();

            if(dr == null) { return restaurant; }

            while(dr.Read())
            {
                restaurant.RestaurantID = Convert.ToInt32(dr["IdRestaurant"]);
                restaurant.RestaurantName = dr["NomRestaurant"].ToString();
                restaurant.RestaurantLoc = dr["LocRestaurant"].ToString();
            }

            con.Close();
            return restaurant;
        }

        /*
         * ********************************************************
         * Méthode GetRestaurant pour Windows Form API
         * ********************************************************
         * */

        public IEnumerable<Restaurant> GetRestaurantsWFapiDB()
        {
            List<Restaurant> restaurantList = new List<Restaurant>(); 
            SqlConnection con = new SqlConnection(connectionString);

            string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME);
            con.Open();
            SqlCommand cmd = new SqlCommand( selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();

            if(dr == null ) return restaurantList;

            while(dr.Read())
            {
                Restaurant restaurant = new Restaurant();
                restaurant.RestaurantID = Convert.ToInt32(dr["IdRestaurant"]);
                restaurant.RestaurantName = dr["NomRestaurant"].ToString();
                restaurant.RestaurantLoc = dr["LocRestaurant"].ToString();

                restaurantList.Add(restaurant);
            }
            con.Close();
            return restaurantList;
        }

        /*
         * ********************************************************
         * Supprime un restaurant
         * ********************************************************
         * */
        public void DeleteProductDB(int restaurantid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("USP_DeleteRestaurant", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@restaurantId", restaurantid));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
