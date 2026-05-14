using Microsoft.AspNetCore.Mvc;
using API_ToDo_List.Models;
using API_ToDo_List.Repositorios;
using System.Collections.Generic;

namespace API_ToDo_List.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepository;

        public TarefaController(ITarefaRepositorio tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefas()
        {
            var tarefas = await _tarefaRepository.GetAllAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarefa(int id)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTarefa([FromBody] Tarefa novaTarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tarefaRepository.AddAsync(novaTarefa);
            return CreatedAtAction(nameof(GetTarefa), new { id = novaTarefa.Id }, novaTarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] Tarefa tarefaAtualizada)
        {
            if (id != tarefaAtualizada.Id)
            {
                return BadRequest();
            }

            var existingTarefa = await _tarefaRepository.GetByIdAsync(id);
            if (existingTarefa == null)
            {
                return NotFound();
            }

            await _tarefaRepository.UpdateAsync(tarefaAtualizada);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            await _tarefaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}