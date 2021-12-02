using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoShop.Application.DTO.Login;

namespace AutoShop.Application.Interfaces
{
	public interface IApplicationServiceLogin
	{
		LoginGetDTO Login(LoginCreateDTO obj);
	}
}
