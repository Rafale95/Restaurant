using AppRestaurantBOL;
using AppRestaurantDAL;

namespace AppRestaurantBLL
{
    public class ServeurBLL
    {

        private ServeurDB serveurDB;

        public ServeurBLL(ServeurDB serveurDB)
        {
            this.serveurDB = serveurDB;
        }

        public void CreateServeur(Serveur serveur)
        {
            serveurDB.CreateServeurDB(serveur);
        }

        public void EditServeur(Serveur serveur)
        {
            serveurDB.EditServeurDB(serveur);
        }

        public void DeleteServeur(Serveur serveur)
        {
            serveurDB.DeleteServeurDB(serveur);
        }
    }
}
