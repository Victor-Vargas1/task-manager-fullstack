
using System.ComponentModel.DataAnnotations;

namespace ApiTarefas.Dto
{
    public record TarefaDto // tranferir os dadtos de um lugar para outro, nesse caso do controller para o banco de dados, ou seja, do TarefaDto para o Tarefa.
    {
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;
        public DateTime? Prazo { get; set; }
        public bool Concluida { get; set; }
    }
}