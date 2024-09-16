using Loja.Models;

namespace Loja.Repositorio
{
    public interface ILoginRepositorio
    {
        //  CRUD- CREATE, READ, UPDATE, DELETE
        Usuario Login(string usuario, string senha);
    }
}
