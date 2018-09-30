namespace Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public bool Ativo { get; set; }
    }
}