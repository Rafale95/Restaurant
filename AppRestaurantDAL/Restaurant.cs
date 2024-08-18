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
        public void InsertRestaurantDB(Restaurant restaurant)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand storedProc = new SqlCommand("USP_InsertRestaurant", connection);

                storedProc.CommandType = CommandType.StoredProcedure;
                storedProc.Parameters.Add(new SqlParameter("@RestaurantName", restaurant.RestaurantName));
                storedProc.Parameters.Add(new SqlParameter("@RestaurantLoc", restaurant.RestaurantLoc));

                connection.Open();
                storedProc.ExecuteNonQuery();
            }
        }

                /*
      * ********************************************************
      * Récupération des restaurants avec ou sans filtre
      * ********************************************************
      * */
        public IEnumerable<Restaurant> FindRestaurantByNameDB(string restaurantName, string sortOrder)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME) +
                    QueryHelper.GetStringFilteredSelectQuery("NomRestaurant", restaurantName) +
                    QueryHelper.GetPartialOrderedSelectQuery("LocRestaurant", sortOrder == "RestaurantLoc_desc");

                connection.Open();
                SqlCommand cmd = new SqlCommand(selectSQL, connection);

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<Restaurant> restaurantList = new List<Restaurant>();
                    if (dr == null) return restaurantList;

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

            }
        }

        /*
        * ********************************************************
        * Edition d'un produit de la page Products/edit
        * ********************************************************
        * */

        public void UpdateRestaurantDB(Restaurant restaurant)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_UpdateRestaurant", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RestaurantId", restaurant.RestaurantID));
                cmd.Parameters.Add(new SqlParameter("@RestaurantName", restaurant.RestaurantName));
                cmd.Parameters.Add(new SqlParameter("@RestaurantLoc", restaurant.RestaurantLoc));
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

         /*
         * ********************************************************
         * Affiche les localistations
         * ********************************************************
         * */

        public IEnumerable<String> FindRestaurantLocationsDB()
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME, "LocRestaurant");
                connection.Open();
                SqlCommand cmd = new SqlCommand(selectSQL, connection);

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<String> restaurantLocations = new List<string>();
                    if (dr == null) return restaurantLocations;

                    while (dr.Read())
                    {
                        restaurantLocations.Add(dr["LocRestaurant"].ToString());
                    }

                    return restaurantLocations;
                }
            }
        }

        /*
         * ********************************************************
         * Affiche un seul restaurant
         * ********************************************************
         * */

        public Restaurant FindRestaurantByIdDB (int restaurantId)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME, restaurantId.ToString());
                connection.Open();

                SqlCommand cmd = new SqlCommand(selectSQL, connection);

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    Restaurant restaurant = new Restaurant();

                    if (dr == null) { return restaurant; }

                    while (dr.Read())
                    {
                        restaurant.RestaurantID = Convert.ToInt32(dr["IdRestaurant"]);
                        restaurant.RestaurantName = dr["NomRestaurant"].ToString();
                        restaurant.RestaurantLoc = dr["LocRestaurant"].ToString();
                    }
                    return restaurant;
                }
            }
        }

        /*
         * ********************************************************
         * Méthode GetRestaurant pour Windows Form API
         * ********************************************************
         * */

        public IEnumerable<Restaurant> FindRestaurantsWFapiDB()
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME);
                connection.Open();
                SqlCommand cmd = new SqlCommand(selectSQL, connection);

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<Restaurant> restaurantList = new List<Restaurant>();
                    if (dr == null) return restaurantList;

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
            }
        }

        /*
         * ********************************************************
         * Supprime un restaurant
         * ********************************************************
         * */
        public void DeleteProductDB(int restaurantid)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_DeleteRestaurant", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@restaurantId", restaurantid));

                connection.Open();
                cmd.ExecuteNonQuery();
            }   
        }
    }
}
