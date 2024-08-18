using AppRestaurantBOL;
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

        public IEnumerable<Restaurant> GetRestaurant(string searchString, string sortOrder)
        {
            return restaurantDB.GetRestaurantByNameDB(searchString, sortOrder);
        }

        public IEnumerable<String> GetRestaurantLocs()
        {
            return restaurantDB.FindRestaurantByLocationLocsDB();
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
            restaurantDB.DeleteProductDB(restaurantID);
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            return restaurantDB.GetRestaurantDB(restaurantId);
        }

        public IEnumerable<Restaurant> GetRestaurantWFapiDB()
        {
            return restaurantDB.GetRestaurantsWFapiDB();
        }

    }
}
