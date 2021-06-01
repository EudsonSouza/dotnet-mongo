using Api.Data.Collections;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        InfectadosService _infectadosService;

        public InfectadoController(InfectadosService infectadosService)
        {
            _infectadosService = infectadosService;
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InsertInfectadoInputModel infectadoInputModel)
        {
            var infectado = new Infectado(infectadoInputModel.DataNascimento, infectadoInputModel.Sexo, infectadoInputModel.Latitude, infectadoInputModel.Longitude);

            _infectadosService.Add(infectado);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]

        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosService.GetAll();
            List<InfectadoOutputModel> outputModel = infectados.Select(i => new InfectadoOutputModel()
            {
                Id = i.Id.ToString(),
                DataNascimento = i.DataNascimento,
                Sexo = i.Sexo,
                Latitude = i.Localizacao.Latitude,
                Longitude = i.Localizacao.Longitude
            }).ToList();

            return Ok(outputModel);
        }

        [HttpGet("{id}")]
        public ActionResult ObterInfectado(string id)
        {
            var infectado = _infectadosService.GetById(new ObjectId(id));
            if(infectado != null)
            {
                InfectadoOutputModel outputModel = new InfectadoOutputModel()
                {
                    Id = infectado.Id.ToString(),
                    DataNascimento = infectado.DataNascimento,
                    Sexo = infectado.Sexo,
                    Latitude = infectado.Localizacao.Latitude,
                    Longitude = infectado.Localizacao.Longitude
                };
                return Ok(outputModel);
            }
            else
            {
                return NotFound();
            }



        }

        [HttpPut("{id}")]
        public ActionResult AtualizarInfectado(string id, [FromBody] InsertInfectadoInputModel infectadoInputModel)
        {
            var infectado = new Infectado(infectadoInputModel.DataNascimento, infectadoInputModel.Sexo, infectadoInputModel.Latitude, infectadoInputModel.Longitude);
            infectado.Id = new ObjectId(id);
            _infectadosService.Update(infectado);


            return Ok("Infectado atualizado");
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirInfectado(string id)
        {
            _infectadosService.Delete(new ObjectId(id));
            return Ok("Infectado excluido");
        }
    }
}
