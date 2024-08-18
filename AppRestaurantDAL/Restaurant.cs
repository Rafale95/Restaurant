using AppRestaurantBOL;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Generic;

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
                storedProc.Parameters.Add(new SqlParameter("@NomRestaurant", restaurant.RestaurantName));
                storedProc.Parameters.Add(new SqlParameter("@RestaurantLoc", restaurant.RestaurantLoc));

                connection.Open();
                storedProc.ExecuteNonQuery();
            }
        }

        private string GetFilteredWhereClause(string first, string second)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(first)) {
                sb.Append(" WHERE NomRestaurant LIKE '%" + first + "%'");
                if (!string.IsNullOrEmpty(second))
                    sb.Append(" AND LocRestaurant LIKE '%" + second + "%'");
            }
            else
            {
                sb.Append(" WHERE LocRestaurant LIKE '%" + second + "%'");
            }
            return sb.ToString();
        }


        /*
      * ********************************************************
      * Récupération des restaurants avec ou sans filtre
      * ********************************************************
      * */
        public IEnumerable<Restaurant> FindFilteredRestaurant(string restaurantName, string location, string sortOrder)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(null, connection);

                cmd.CommandText = QueryHelper.GetSelectQuery(TABLE_NAME);
                if (!string.IsNullOrEmpty(restaurantName))
                {
                    cmd.CommandText += " WHERE NomRestaurant LIKE @name";
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = "%"+restaurantName+"%";
                    if (!string.IsNullOrEmpty(location))
                    {
                        cmd.CommandText += (" AND LocRestaurant = @loc");
                        cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = location;
                    }
                }
                else if (!string.IsNullOrEmpty(location))
                {
                    cmd.CommandText += (" WHERE LocRestaurant = @loc");
                    cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = location;
                }

                cmd.CommandText += GetRestaurantSortOrder(sortOrder);

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

        private string GetRestaurantSortOrder(string sortOrder)
        {
            switch (sortOrder)
            {
                case "nameDesc": return " ORDER BY NomRestaurant DESC";
                case "locDesc": return " ORDER BY NomRestaurant DESC";
                case "locAsc": return " ORDER BY NomRestaurant ASC";
                default : return " ORDER BY NomRestaurant";
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
                cmd.Parameters.Add(new SqlParameter("@IdRestaurant", restaurant.RestaurantID));
                cmd.Parameters.Add(new SqlParameter("@NomRestaurant", restaurant.RestaurantName));
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
                string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME) + QueryHelper.GetIntegerFilteredSelectQuery("IdRestaurant", restaurantId);
                connection.Open();

                SqlCommand cmd = new SqlCommand(selectSQL, connection);
                Restaurant restaurant = new Restaurant();

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null) { return restaurant; }

                    while (dr.Read())
                    {
                        restaurant.RestaurantID = Convert.ToInt32(dr["IdRestaurant"]);
                        restaurant.RestaurantName = dr["NomRestaurant"].ToString();
                        restaurant.RestaurantLoc = dr["LocRestaurant"].ToString();
                    }
                }

                ServeurDB serveurDB = new ServeurDB(connectionString);
                restaurant.serveurs= serveurDB.GetServeursByRestaurant(restaurantId);
                foreach (Serveur serveur in restaurant.serveurs) 
                    serveur.employeur = restaurant;

                return restaurant;
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
        public void DeleteRestaurantDB(int restaurantid)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_DeleteRestaurant", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@IdRestaurant", restaurantid));

                connection.Open();
                cmd.ExecuteNonQuery();
            }   
        }
    }
}
