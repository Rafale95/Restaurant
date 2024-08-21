using System.ComponentModel;

namespace AppRestaurantBOL
{
    public class Article
    {
        public int Id { get; set; }

        [DisplayName("Type l'article")]
        public string Category { get; set; }

        [DisplayName("Nom de l'article")]
        public string Name { get; set; }

        [DisplayName("Prix de l'article")]
        public float Price { get; set; }
    }
}
