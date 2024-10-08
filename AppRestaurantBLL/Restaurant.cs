﻿using AppRestaurantBOL;
using AppRestaurantDAL;
namespace AppRestaurantBLL
{
    public class RestaurantBLL
    {
        private RestaurantDB restaurantDB;

        public RestaurantBLL(string connectionString)
        {
            restaurantDB = new RestaurantDB(connectionString);
        }

        public IEnumerable<Restaurant> GetAllRestaurants(string SearchString, string Location, string sortOrder)
        {
            return restaurantDB.FindFilteredRestaurant(SearchString, Location, sortOrder);
        }

        public IEnumerable<String> GetRestaurantLocs()
        {
            return restaurantDB.FindRestaurantLocationsDB();
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            restaurantDB.UpdateRestaurantDB(restaurant);
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            restaurantDB.InsertRestaurantDB(restaurant);
        }

        public void DeleteRestaurant(int restaurantID)
        {
            restaurantDB.DeleteRestaurantDB(restaurantID);
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            return restaurantDB.FindRestaurantByIdDB(restaurantId);
        }

        public IEnumerable<Restaurant> GetRestaurantWFapiDB()
        {
            return restaurantDB.FindRestaurantsWFapiDB();
        }

    }
}
