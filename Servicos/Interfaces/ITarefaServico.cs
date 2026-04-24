using Microsoft.EntityFrameworkCore;
using ApiTarefas.Models;
using ApiTarefas.Database;
using ApiTarefas.Model.Erros;
using ApiTarefas.Dto;

namespace ApiTarefas.Servicos;

public interface ITarefaServico //contratos sobrescritas de codigo, assinaturas de metodos, sem implementação
{
    List<Tarefa> Listar(int page);
    Tarefa Incluir(TarefaDto tarefaDto);
    Tarefa Update(int id, TarefaDto tarefaDto);
    Tarefa BuscaPorId(int id);
    void Delete(int id);
    Tarefa AtualizarStatus(int id, bool concluida);

}
