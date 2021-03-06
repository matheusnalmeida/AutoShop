namespace AutoShop.Application.DTO.Produto
{
    public class ProdutoGetDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; }

        public static ProdutoGetDTO MapEntityAsDTO(Domain.Entities.Produto produto) {
            return  new ProdutoGetDTO()
            {
                Id = produto.Id,
                Nome = produto.Nome.Valor,
                Preco = produto.Preco.Valor,
                Tipo = produto.Tipo.ToString()
            };
        }
    }
}
