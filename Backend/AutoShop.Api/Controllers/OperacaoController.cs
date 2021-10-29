using AutoShop.Application.DTO.Operacao;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacaoController : Controller
    {
        private readonly IApplicationServiceOperacao _applicationServiceOperacao;

        public OperacaoController(IApplicationServiceOperacao applicationServiceOperacao)
        {
            _applicationServiceOperacao = applicationServiceOperacao;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OperacaoGetDTO>> Get()
        {
            return Ok(_applicationServiceOperacao.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<OperacaoGetDTO> Get(string id)
        {
            return Ok(_applicationServiceOperacao.GetById(id));
        }

        [HttpPost]
        public ActionResult<ApplicationCreateResult> Post([FromBody] OperacaoCreateDTO operacaoDTO)
        {
            if (operacaoDTO == null)
                return BadRequest();

            var result = _applicationServiceOperacao.Add(operacaoDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public ActionResult<ApplicationResult> Put([FromQuery] string id, [FromBody] OperacaoUpdateDTO operacaoDTO)
        {
            if (operacaoDTO == null)
                return BadRequest();

            var result = _applicationServiceOperacao.Update(id, operacaoDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
