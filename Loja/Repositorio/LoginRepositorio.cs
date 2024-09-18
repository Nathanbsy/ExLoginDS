using Loja.Models;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System.Data;

namespace Loja.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        //declarando a varivel de strig de conexão com o banco de dados
        private readonly string? _conexaoMsql;

        //METODO DA CONEXÃO COM O BANCO DE DADOS
        public LoginRepositorio(IConfiguration conf) => _conexaoMsql = conf.GetConnectionString("ConexaoMsql");

        //metodo login
        public Usuario Login(string usuario, string senha)
        {
            //INSTANCIANDO A VARIAVEL CONEXÃO 
            using (var conexao = new MySqlConnection(_conexaoMsql))
            {
                // ABRINDO A CONEXÃO COM O BANCO DE DADOS 
                conexao.Open();

                //VARIAVEL CMD QUE RECEBE O SELECT DO BANCO DE DADOS TRANZENDO O USUARIO E A SENHA 
                MySqlCommand cmd = new MySqlCommand("select * from tbLogin where usuario =@user and senha = @senha", conexao);

                //pegando os parametros do usuario e senha do banco
                cmd.Parameters.Add("@user", MySqlDbType.VarChar).Value = usuario;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;

                //ler os dados que foi pego do usuario e da senha
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                //guarda os dados lidos
                MySqlDataReader dr;

                //instanciando a model usuario
                Usuario user = new Usuario();

                //executando os comandos do MySQL para a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //verifica todos os usuarios que foi pego no banco
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
