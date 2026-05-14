using System.ComponentModel.DataAnnotations;

namespace API_ToDo_List.Models
{
    public class TarefaDiaria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição da tarefa diária é obrigatória.")]
        [StringLength(255, ErrorMessage = "A descrição não pode exceder 255 caracteres.")]
        public string Descricao { get; set; }
        public bool ConcluidaHoje { get; set; }
        public DateTime? DataUltimaConclusao { get; set; }
    }
}