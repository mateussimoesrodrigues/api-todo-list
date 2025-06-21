// Models/TarefaDiaria.cs
using System.ComponentModel.DataAnnotations; // Para validações, opcional por enquanto

namespace API_ToDo_List.Models
{
    public class TarefaDiaria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição da tarefa diária é obrigatória.")]
        [StringLength(255, ErrorMessage = "A descrição não pode exceder 255 caracteres.")]
        public string Descricao { get; set; }

        // Indica se a tarefa foi concluída para o dia atual.
        // A lógica de "resetar" isso diariamente estará na API ou na forma de uso.
        public bool ConcluidaHoje { get; set; }
    }
}