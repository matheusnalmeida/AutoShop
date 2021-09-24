namespace AutoShop.Application.DTO.Usuario
{
    public class UsuarioGetDTO
    {
        public string Cpf { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }

        public static UsuarioGetDTO MapEntityAsDTO(Domain.Entities.Usuario usuario)
        {
            return usuario == null ? null : new UsuarioGetDTO()
            {
                Cpf = usuario.Cpf.Numero,
                Idade = usuario.Idade,
                Telefone = usuario.Telefone.Numero,
                Email = usuario.Email.Endereco,
                Tipo = usuario.Tipo.ToString()
            };
        }
    }
}
