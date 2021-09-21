using AutoShop.Application.DTO.Produto;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IApplicationServiceProduto _applicationServiceProduto;

        public ProdutoController(IApplicationServiceProduto applicationServiceProduto)
        {
            _applicationServiceProduto = applicationServiceProduto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoGetDTO>> Get()
        {
            return Ok(_applicationServiceProduto.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ProdutoGetDTO> Get(string id)
        {
            return Ok(_applicationServiceProduto.GetById(id));
        }

        [HttpPost]
        public ActionResult<ApplicationResult> Post([FromBody] ProdutoCreateDTO produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest();

            var result = _applicationServiceProduto.Add(produtoDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public ActionResult<ApplicationResult> Put([FromQuery] string id,[FromBody] ProdutoUpdateDTO produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest();

            var result = _applicationServiceProduto.Update(id, produtoDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<ApplicationResult> Delete(string id)
        {
            var result = _applicationServiceProduto.Remove(id);
            if(result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
