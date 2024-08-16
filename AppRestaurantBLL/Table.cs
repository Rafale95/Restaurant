using AppRestaurantBOL;
using AppRestaurantDAL;

namespace AppRestaurantBLL
{
    public class TableBLL
    {
        private TableDB tableDB;

        public TableBLL(TableDB tableDB)
        {
            this.tableDB = tableDB;
        }

        public void CreateTable(Table table)
        {
            tableDB.CreateTableDB(table);
        }

        public void EditTable(Table table)
        {
            tableDB.EditTableDB(table);
        }

        public void DeleteTable(Table table)
        {
            tableDB.DeleteTableDB(table);
        }
    }
}
