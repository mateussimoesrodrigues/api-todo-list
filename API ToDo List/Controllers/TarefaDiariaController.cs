using Microsoft.AspNetCore.Mvc;
using API_ToDo_List.Models;
using API_ToDo_List.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_ToDo_List.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaDiariaController : ControllerBase
    {
        private readonly ITarefaDiariaRepositorio _tarefaDiariaRepositorio;

        public TarefaDiariaController(ITarefaDiariaRepositorio tarefaDiariaRepositorio)
        {
            _tarefaDiariaRepositorio = tarefaDiariaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefasDiarias()
        {
            var tarefas = await _tarefaDiariaRepositorio.GetAllDailyAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarefaDiaria(int id)
        {
            var tarefa = await _tarefaDiariaRepositorio.GetDailyByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTarefaDiaria([FromBody] TarefaDiaria novaTarefaDiaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tarefaDiariaRepositorio.AddDailyAsync(novaTarefaDiaria);
            return CreatedAtAction(nameof(GetTarefaDiaria), new { id = novaTarefaDiaria.Id }, novaTarefaDiaria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefaDiaria(int id, [FromBody] TarefaDiaria tarefaDiariaAtualizada)
        {
            if (id != tarefaDiariaAtualizada.Id)
            {
                return BadRequest();
            }

            var existingTarefa = await _tarefaDiariaRepositorio.GetDailyByIdAsync(id);
            if (existingTarefa == null)
            {
                return NotFound();
            }

            await _tarefaDiariaRepositorio.UpdateDailyAsync(tarefaDiariaAtualizada);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTarefaDiaria(int id)
        {
            var tarefa = await _tarefaDiariaRepositorio.GetDailyByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            await _tarefaDiariaRepositorio.DeleteDailyAsync(id);
            return NoContent();
        }
    }
}