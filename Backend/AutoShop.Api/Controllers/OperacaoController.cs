﻿using AutoShop.Application.DTO.Operacao;
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
        public ActionResult<ApplicationResult> Post([FromBody] OperacaoCreateDTO operacaoDTO)
        {
            if (operacaoDTO == null)
                return BadRequest();

            var result = _applicationServiceOperacao.Add(operacaoDTO);
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }
    }
}