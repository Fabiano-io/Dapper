using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoDapper.Entities;
using ProjetoDapper.Repository;

namespace ProjetoDapper.Controllers
{
    [Route("[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        [HttpGet]
        public ActionResult<Contato> Index()
        {
            var resultao = _contatoRepository.GetContatos();

            return Ok(resultao);
        }

        [HttpGet("{id}")]
        public ActionResult<Contato> Get(int id)
        {
            var resultao = _contatoRepository.Get(id);

            if (resultao == null)
                return NotFound();

            return Ok(resultao);
        }

        [HttpPost]
        public ActionResult<IEnumerable<string>> Post([FromBody] Contato contato)
        {
            var resultado = _contatoRepository.Add(contato);
            if (resultado == 0)
                return BadRequest();

            return Ok(new
            {
                Status = true,
                RegistrosInseridos = resultado
            });
        }

        [HttpPut]
        public ActionResult<IEnumerable<string>> Put([FromBody] Contato contato)
        {
            var resultado = _contatoRepository.Edit(contato);
            if (resultado == 0)
                return BadRequest();

            return Ok(new
            {
                Status = true,
                RegistrosAtualizados = resultado
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<string>> Delete(int id)
        {
            var resultado = _contatoRepository.Delete(id);
            if (resultado == 0)
                return BadRequest();

            return Ok(new
            {
                Status = true,
                RegistrosRemovidos = resultado
            });
        }
    }
}