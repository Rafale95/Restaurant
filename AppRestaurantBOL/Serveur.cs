using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurantBOL
{
    public class Serveur
    {
        public int serveurId {  get; set; }

        [DisplayName("Nom")]
        public string serveurName { get; set; }

        [DisplayName("Prénom")]
        public string serveurFirstname { get; set; }

        [DisplayName("Téléphone")]
        public int serveurPhone { get; set; }

        [DisplayName("Email")]
        public string serveurEmail { get; set; }

        [DisplayName("Employeur")]
        public Restaurant employeur { get; set; }
    }
}
