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
            tableDB.InsertTableDB(table);
        }

        public void EditTable(Table table)
        {
            tableDB.UpdateTableDB(table);
        }

        public void DeleteTable(Table table)
        {
            tableDB.DeleteTableDB(table);
        }
    }
}
