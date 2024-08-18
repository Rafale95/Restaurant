using AppRestaurantBOL;
using System.Data.SqlClient;

namespace AppRestaurantDAL
{
    public class ServeurDB
    {
        private readonly string connectionString;
        private static readonly string TABLE_NAME = "Serveur";

        public ServeurDB(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public void InsertServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public void DeleteServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public void UpdateServeurDB(Serveur serveur)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Serveur> GetAllServeur()
        {
            throw new NotImplementedException();
        }

        public List<Serveur> GetServeursByRestaurant(int serveurId)
        {
            // En utilisant USING, la ressource est close à la fin
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectSQL = QueryHelper.GetSelectQuery(TABLE_NAME) + QueryHelper.GetIntegerFilteredSelectQuery("IdServeur", serveurId);
                connection.Open();

                SqlCommand cmd = new SqlCommand(selectSQL, connection);

                // En utilisant USING, la ressource est close à la fin
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<Serveur> serveurs = new List<Serveur>();

                    if (dr == null) { return serveurs; }

                    while (dr.Read())
                    {
                        Serveur serveur = new Serveur();

                        serveur.serveurId = Convert.ToInt32(dr["IdServeur"].ToString);
                        serveur.serveurFirstname = dr["Prenom"].ToString();
                        serveur.serveurName = dr["Nom"].ToString();
                        serveur.serveurPhone = Convert.ToInt32(dr["Telephone"].ToString);
                        serveur.serveurEmail = dr["Email"].ToString();

                        serveurs.Add(serveur);
                    }
                    return serveurs;
                }
            }
        }
    }
}
