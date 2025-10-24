using MySql.Data.MySqlClient;
using ProjMercado.Models;

namespace ProjMercado.Repository
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        IConfiguration configuration;

        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void AdicionarUsuario(Usuario usuario)
        {
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = conexao.CreateCommand();
                cmd.CommandText = "INSERT INTO tbUsuario (Email, Senha) VALUES (@Email, @Senha)";

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = usuario.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        public Usuario ObterUsuario(string Email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("SELECT * FROM Usuario WHERE Email = @Email", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;

                using(MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;

                    if(dr.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Email = dr["Email"].ToString(),
                            Senha = dr["Senha"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }
    }
}
