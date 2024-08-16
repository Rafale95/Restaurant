using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AppRestaurantBOL
{
    public class Table
    {
        public int TableId { get; set; }

        [DisplayName("Numéro de table")]
        public int? tableNum { get; set; }

        [DisplayName("Occupé")]
        public bool isOccupied { get; set; }

        [DisplayName("Mode de paiement")]
        public string paymentMethod { get; set; }
    }
}
