using System.ComponentModel;

namespace AppRestaurantBOL
{
    public class Restaurant
    {
        public int Id { get; set; }

        [DisplayName("Restaurant")]
        public string Nom { get; set; }

        [DisplayName("Location")]
        public string Localisation { get; set; }

        [DisplayName("Serveurs")]
        public List<Serveur> Serveurs { get; set; }

        [DisplayName("Tables")]
        public List<Table> Tables { get; set; }

        [DisplayName("Commandes")]
        public List<Commande> Commandes { get; set; }
    }
}
