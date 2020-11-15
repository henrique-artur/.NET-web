using MySqlConnector;
using System.Collections.Generic;

namespace AlagoasTour.Models
{
    public class PacoteRepository : Repository
    {
        public void Cadastrar(Pacotes p)
        {
            conexao.Open();
            string sql = "INSERT INTO pacote (nomePacote, valor, descricao) VALUES (@nomePacote, @valor, @descricao);"; 
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nomePacote", p.nomePacote);
            comando.Parameters.AddWithValue("@valor", p.valor);
            comando.Parameters.AddWithValue("@descricao", p.descricao);
            
            comando.ExecuteNonQuery();
            
            conexao.Close();
        }

        public List<Pacotes> Query()
        {
            conexao.Open();
            string sql = "SELECT * FROM pacote ORDER BY nomePacote;";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            List<Pacotes> lista = new List<Pacotes>();
            while(reader.Read())
            {
                Pacotes pacote = new Pacotes();
                pacote.id = reader.GetInt32("id");

                if(!reader.IsDBNull(reader.GetOrdinal("nomePacote")))
                    pacote.nomePacote = reader.GetString("nomePacote");
                if(!reader.IsDBNull(reader.GetOrdinal("valor")))
                    pacote.valor = reader.GetDouble("valor");
                if(!reader.IsDBNull(reader.GetOrdinal("descricao")))
                    pacote.descricao = reader.GetString("descricao");
                lista.Add(pacote);
            }
            conexao.Clone();
            return lista;
        }

        public void Excluir(int id)
        {
            conexao.Open();
            string sql = "DELETE FROM pacote where id = @id;";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", id);
            comandoQuery.ExecuteNonQuery();
            conexao.Close();
        }
    }
}