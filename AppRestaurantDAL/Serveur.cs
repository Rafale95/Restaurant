using AppRestaurantBOL;

namespace AppRestaurantDAL
{
    public class ServeurDB
    {
        private readonly string connectionString;

        public ServeurDB(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public void CreateServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public void DeleteServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public void EditServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }
    }
}
