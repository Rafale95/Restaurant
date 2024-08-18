using System.ComponentModel;

namespace AppRestaurantBOL
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }

        [DisplayName("Restaurant")]
        public string RestaurantName { get; set; }

        [DisplayName("Location")]
        public string RestaurantLoc { get; set; }

        [DisplayName("Serveurs")]
        public List<Serveur> serveurs { get; set; }

        [DisplayName("Tables")]
        public List<Table> tables { get; set; }

        [DisplayName("Commandes")]
        public List<Commande> commandes { get; set; }
    }
}
