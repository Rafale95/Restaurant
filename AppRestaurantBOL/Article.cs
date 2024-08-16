using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurantBOL
{
    public class Article
    {
        public int ArticleID { get; set; }

        [DisplayName("Type l'article")]
        public string ArticleType { get; set; }

        [DisplayName("Nom de l'article")]
        public string ArticleName { get; set; }

        [DisplayName("Prix de l'article")]
        public string ArticlePrice { get; set; }
    }
}
