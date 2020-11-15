using MySqlConnector;
using System.Collections.Generic;

namespace AlagoasTour.Models
{
    public class LoginRepository : Repository
    {
        public void Cadastra(Login l)
        {
            conexao.Open();
            string sql = "INSERT INTO Login (nome, email, senha, perfil) VALUES (@nome, @email, @senha, @perfil);";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", l.nome);
            comando.Parameters.AddWithValue("@email", l.email);
            comando.Parameters.AddWithValue("@senha", l.senha);
            comando.Parameters.AddWithValue("@perfil", l.perfil);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public Login ValidaLogin(Login login)
        {
            conexao.Open();

            string sql = "SELECT * FROM login WHERE email = @email AND senha = @senha";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@email", login.email);
            comandoQuery.Parameters.AddWithValue("@senha", login.senha);

            MySqlDataReader reader = comandoQuery.ExecuteReader();

            Login l = null;
            if(reader.Read())
            {
                l = new Login();
                l.id = reader.GetInt32("id");
                l.nome = reader.GetString("nome");
                l.email = reader.GetString("email");
                l.senha = reader.GetString("senha");
                l.perfil = reader.GetString("perfil");
            }
            
            conexao.Close();
            return l;
        }
    }
}