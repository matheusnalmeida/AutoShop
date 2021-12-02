using AutoShop.Application.DTO.Usuario;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IApplicationServiceUsuario _applicationServiceUsuario;

        public UsuarioController(IApplicationServiceUsuario applicationServiceUsuario)
        {
            _applicationServiceUsuario = applicationServiceUsuario;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioGetDTO>> Get()
        {
            return Ok(_applicationServiceUsuario.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioGetDTO> Get(string id)
        {
            return Ok(_applicationServiceUsuario.GetById(id));
        }

        [HttpPost]
        public ActionResult<ApplicationCreateResult> Post([FromBody] UsuarioCreateDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                return BadRequest();

            var result = _applicationServiceUsuario.Add(usuarioDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public ActionResult<ApplicationResult> Put([FromQuery] string id, [FromBody] UsuarioUpdateDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                return BadRequest();

            var result = _applicationServiceUsuario.Update(id, usuarioDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ApplicationResult> Delete(string id)
        {
            var result = _applicationServiceUsuario.Remove(id);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
