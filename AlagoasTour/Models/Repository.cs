using MySqlConnector;

namespace AlagoasTour.Models
{
    public class Repository
    {
        protected const string _strConexao = "Database = alagoasTour; Data Source = localhost; Port = 3306; User Id = root;";
        protected MySqlConnection conexao = new MySqlConnection(_strConexao); 
    }
}