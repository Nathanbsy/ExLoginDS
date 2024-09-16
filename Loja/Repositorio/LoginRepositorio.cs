using Loja.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Loja.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        //Declarando a variavel de string de conexão com o banco de dados
        private readonly string? _conexaoMySql;

        //Metodo de conexão com banco de dados
        public LoginRepositorio(IConfiguration conf) => _conexaoMySql = conf.GetConnectionString("ConexaoMysql");

        //Metodo login
        public Usuario Login(string usuario, string senha)
        {
            //Instranciando a variavel conexão
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                //Abrindo a conexão com o banco de dados
                conexao.Open();

                //Variavel cmd recebe o SELECT do banco de dados trazendo usuario e senha
                MySqlCommand cmd = new MySqlCommand("Select * from tbLogin where usuario = @user and senha = @senha", conexao);

                //Pegando os parametros do usuario e senha do banco
                cmd.Parameters.Add("@user", MySqlDbType.VarChar).Value = usuario;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;

                //Ler os dados que foram pegos do usuario e da senha
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                //Guarda os dados lidos
                MySqlDataReader dr;

                //Instanciando o modelo usuario
                Usuario user = new Usuario();

                //Executando os comandos do MySQL para a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //Verifica todos os usuarios que foram pegos no banco
                while (dr.Read())
                {
                    user.usuario = Convert.ToString(dr["usuario"]);
                    user.senha = Convert.ToString(dr["senha"]);
                }
                return user;
            }
        }
    }
}
