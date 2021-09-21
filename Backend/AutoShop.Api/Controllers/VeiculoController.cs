using AutoShop.Application.DTO.Veiculo;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculoController : Controller
    {
        private readonly IApplicationServiceVeiculo _applicationServiceVeiculo;

        public VeiculoController(IApplicationServiceVeiculo applicationServiceVeiculo)
        {
            _applicationServiceVeiculo = applicationServiceVeiculo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VeiculoGetDTO>> Get()
        {
            return Ok(_applicationServiceVeiculo.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<VeiculoGetDTO> Get(string id)
        {
            return Ok(_applicationServiceVeiculo.GetById(id));
        }

        [HttpPost]
        public ActionResult<ApplicationResult> Post([FromBody] VeiculoCreateDTO veiculoDTO)
        {
            if (veiculoDTO == null)
                return BadRequest();

            var result = _applicationServiceVeiculo.Add(veiculoDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public ActionResult<ApplicationResult> Put([FromQuery] string id, [FromBody] VeiculoUpdateDTO veiculoDTO)
        {
            if (veiculoDTO == null)
                return BadRequest();

            var result = _applicationServiceVeiculo.Update(id, veiculoDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ApplicationResult> Delete(string id)
        {
            var result = _applicationServiceVeiculo.Remove(id);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
