// Repositorios/ITarefaDiariaRepositorio.cs
using API_ToDo_List.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_ToDo_List.Repositorios
{
    public interface ITarefaDiariaRepositorio
    {
        // Métodos CRUD para Tarefas Diárias
        Task<IEnumerable<TarefaDiaria>> GetAllDailyAsync();
        Task<TarefaDiaria> GetDailyByIdAsync(int id);
        Task AddDailyAsync(TarefaDiaria novaTarefaDiaria);
        Task UpdateDailyAsync(TarefaDiaria tarefaDiariaAtualizada);
        Task DeleteDailyAsync(int id);

        // Um método para "resetar" o status de conclusão de todas as tarefas diárias,
        // ou para gerenciar a conclusão por dia (vamos pensar na melhor abordagem para isso depois)
        // Por enquanto, o GetAllDailyAsync vai retornar o status atual.
    }
}