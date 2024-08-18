using AppRestaurantBOL;
using System.Data.SqlClient;

namespace AppRestaurantDAL
{
    public class TableDB
    {
        private readonly string connectionString;

        public TableDB(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public void InsertTableDB(Table table)
        {
            throw new NotImplementedException();
        }

        public void DeleteTableDB(Table table)
        {
            throw new NotImplementedException();
        }

        public void UpdateTableDB(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
