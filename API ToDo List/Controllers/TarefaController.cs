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
        private readonly ITarefaRepositorio _tarefaRepository; // Mude de List<Tarefa> para a interface do repositório

        // Construtor que recebe o repositório via injeção de dependência
        public TarefaController(ITarefaRepositorio tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        // GET: api/tarefa
        [HttpGet]
        public async Task<IActionResult> GetTarefas()
        {
            var tarefas = await _tarefaRepository.GetAllAsync(); // Chame o método do repositório
            return Ok(tarefas);
        }

        // GET: api/tarefa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarefa(int id)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id); // Chame o método do repositório
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        // POST: api/tarefa
        [HttpPost]
        public async Task<IActionResult> AdicionarTarefa([FromBody] Tarefa novaTarefa)
        {
            // Validação básica (opcional, mas boa prática)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tarefaRepository.AddAsync(novaTarefa); // Chame o método do repositório
            // Retorna 201 Created e o link para a nova tarefa
            return CreatedAtAction(nameof(GetTarefa), new { id = novaTarefa.Id }, novaTarefa);
        }

        // PUT: api/tarefa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] Tarefa tarefaAtualizada)
        {
            if (id != tarefaAtualizada.Id)
            {
                return BadRequest();
            }

            // Opcional: Verificar se a tarefa existe antes de tentar atualizar
            var existingTarefa = await _tarefaRepository.GetByIdAsync(id);
            if (existingTarefa == null)
            {
                return NotFound();
            }

            await _tarefaRepository.UpdateAsync(tarefaAtualizada); // Chame o método do repositório
            return NoContent(); // 204 No Content para atualização bem-sucedida sem retorno de conteúdo
        }

        // DELETE: api/tarefa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id); // Chame o método do repositório
            if (tarefa == null)
            {
                return NotFound();
            }

            await _tarefaRepository.DeleteAsync(id); // Chame o método do repositório
            return NoContent(); // 204 No Content para exclusão bem-sucedida
        }
    }
}