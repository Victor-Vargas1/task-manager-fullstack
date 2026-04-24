using Microsoft.EntityFrameworkCore;
using ApiTarefas.Models;

namespace ApiTarefas.Database
{
    public class TarefasContext : DbContext
    {
        public TarefasContext(DbContextOptions<TarefasContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}