using AutoShop.Application.DTO.Login;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoShop.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LoginController : Controller
	{
		private readonly IApplicationServiceLogin _applicationServiceLogin;

		public LoginController(IApplicationServiceLogin applicationServiceLogin)
		{
			_applicationServiceLogin = applicationServiceLogin;
		}

		[Route("GerarToken")]
		[HttpPost]
		public ActionResult<LoginGetDTO> GerarToken([FromBody] LoginCreateDTO loginDTO)
		{
			if (loginDTO == null)
				return BadRequest();

			var result = _applicationServiceLogin.Login(loginDTO);

			if (result == null)
				return Unauthorized();

			return Ok(result);
		}
	}
}
