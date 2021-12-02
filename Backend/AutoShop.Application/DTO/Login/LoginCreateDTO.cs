using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoShop.Domain.ValueObjects;

namespace AutoShop.Application.DTO.Login
{
	public class LoginCreateDTO
	{
		public string Email { get; set; }
		public string Senha { get; set; }
	}
}
