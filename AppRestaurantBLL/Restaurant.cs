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
            return restaurantDB.GetRestaurantDB(searchString, sortOrder);
        }

        public IEnumerable<String> GetRestaurantLocs()
        {
            return restaurantDB.GetRestaurantLocsDB();
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            restaurantDB.EditRestaurantDB(restaurant);
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            restaurantDB.CreateRestaurantDB(restaurant);
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
