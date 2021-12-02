using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoShop.Application.DTO.Login;
using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.Interfaces;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.ValueObjects;
using AutoShop.Shared.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AutoShop.Application.Services
{
	public class ApplicationServiceLogin : IApplicationServiceLogin
	{
		private readonly IConfiguration _configuration;
		private readonly IServiceUsuario _serviceUsuario;

		public ApplicationServiceLogin(IConfiguration configuration, IServiceUsuario serviceUsuario)
		{
			_configuration = configuration;
			_serviceUsuario = serviceUsuario;
		}

		public LoginGetDTO Login(LoginCreateDTO obj)
		{
			if (obj == null)
				return null;

			LoginGetDTO loginGetDTO = new LoginGetDTO();
			var senha = new Senha(obj.Senha);
			var usuario = _serviceUsuario.GetAll()?.Where(u => u.Email.Endereco == obj.Email && u.Senha.Valor == senha.Valor).FirstOrDefault();

			if (usuario != null)
			{
				var token = GerarToken(usuario.Email?.Endereco, Convert.ToInt32(usuario.Tipo));
				var listaFuncoes = ListarFuncoes(Convert.ToInt32(usuario.Tipo));
				
				loginGetDTO.Email = usuario.Email.Endereco;
				loginGetDTO.Token = token;
				loginGetDTO.Funcoes = listaFuncoes;
			}

			return String.IsNullOrEmpty(loginGetDTO.Token) ? null : loginGetDTO;
		}

		private string GerarToken(string email, int tipoUsuario)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var chave = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT:Secret").Value);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, email)
			};
			claims.AddRange(new List<Claim>() {
				new Claim(ClaimTypes.Role, "Administrador"),
				new Claim(ClaimTypes.Role, "Vendedor"),
				new Claim(ClaimTypes.Role, "Cliente")
			}); ;
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Audience = _configuration.GetSection("JWT:Audience").Value,
				Issuer = _configuration.GetSection("JWT:Issuer").Value,
				Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("JWT:ExpiraEmMinutos").Value)),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha512Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		private List<string> ListarFuncoes(int tipoUsuario)
		{
			List<string> listaFuncoes = null;

			switch (tipoUsuario)
			{
				// Administrador
				case 3:
					listaFuncoes = new List<string> {
						"Home",
						"Editar Perfil",
						"Clientes",
						"Vendedores",
						"Veículos",
						"Produtos",
						"Operações"
					};
					break;
				// Vendedor
				case 2:
					listaFuncoes = new List<string> {
						"Home",
						"Operações"
					};
					break;
				// Cliente
				case 1:
					listaFuncoes = new List<string> {
						"Home",
						"Operações"
					};
					break;
			}

			return listaFuncoes;
		}
	}
}
