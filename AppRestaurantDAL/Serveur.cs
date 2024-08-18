using AppRestaurantBOL;

namespace AppRestaurantDAL
{
    public class ServeurDB
    {
        private readonly string connectionString;
        private static readonly string SELECT_QUERY = "SELECT * FROM Serveur";

        public ServeurDB(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public void InsertServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public void DeleteServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public void UpdateServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Serveur> GetAllServeur()
        {
            throw new NotImplementedException();
        }
    }
}
