namespace Loja.Models
{
    public class Usuario
    {
        //CRIANDO O ENCAPSULAMENTO DO OBJETO COM GET E SET
        public int CodLogin { get; set; }
        public string? usuario { get; set; }
        public string? senha { get; set; }
    }
}