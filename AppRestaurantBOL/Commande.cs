using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurantBOL
{
    public class Commande
    {
        public int CommandeId { get; set; }

        [DisplayName("Date de la commande")]
        public string CommandeDate { get; set; }
    }
}
