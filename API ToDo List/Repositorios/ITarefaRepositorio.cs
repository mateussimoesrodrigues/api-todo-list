using API_ToDo_List.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_ToDo_List.Repositorios
{
    public interface ITarefaRepositorio
    {
        Task<IEnumerable<Tarefa>> GetAllAsync();
        Task<Tarefa> GetByIdAsync(int id);
        Task AddAsync(Tarefa tarefa);
        Task UpdateAsync(Tarefa tarefa);
        Task DeleteAsync(int id);
    }
}
