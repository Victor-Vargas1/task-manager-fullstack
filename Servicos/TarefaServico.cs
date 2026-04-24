using Microsoft.EntityFrameworkCore;
using ApiTarefas.Models;
using ApiTarefas.Database;
using ApiTarefas.Model.Erros;
using ApiTarefas.Dto;

namespace ApiTarefas.Servicos;

public class TarefaServico : ITarefaServico// tranferir os dadtos de um lugar para outro, nesse caso do controller para o banco de dados, ou seja, do TarefaDto para o Tarefa.
{
    public TarefaServico(TarefasContext db)
    {
        _db = db;
    }
    private TarefasContext _db;

    public List<Tarefa> Listar(int page = 1)
    {
        if (page <= 1)
            page = 1;
        int limit = 10;
        int offset = (page - 1) * limit;
        return _db.Tarefas.Skip(offset).Take(limit).ToList();
    }

    public Tarefa Incluir(TarefaDto tarefaDto)
    {
        if (string.IsNullOrEmpty(tarefaDto.Titulo))
            throw new TarefaErro("O Titulo da tarefa não pode ser vazio.");

        var tarefa = new Tarefa
        {
            Titulo = tarefaDto.Titulo,
            Descricao = tarefaDto.Descricao,
            Prazo = tarefaDto.Prazo,
            Concluida = tarefaDto.Concluida,
        };

        _db.Tarefas.Add(tarefa);
        _db.SaveChanges();
        return tarefa;
    }

    public Tarefa Update(int id, TarefaDto tarefaDto)
    {
        if (string.IsNullOrEmpty(tarefaDto.Titulo))
            throw new TarefaErro("O Titulo da tarefa não pode ser vazio.");

        var tarefaDb = _db.Tarefas.Find(id); //busca a terafa no banco de dados pelo id, se não encontrar retorna null, caso contrário retorna a tarefa encontrada.
        if (tarefaDb == null)
            throw new TarefaErro("Tarefa não encontrada."); //404 error

        tarefaDb.Titulo = tarefaDto.Titulo;
        tarefaDb.Descricao = tarefaDto.Descricao;
        tarefaDb.Prazo = tarefaDto.Prazo;
        tarefaDb.Concluida = tarefaDto.Concluida;

        _db.Tarefas.Update(tarefaDb);
        _db.SaveChanges();
        return tarefaDb;
    }

    public Tarefa BuscaPorId(int id)
    {
        var tarefaDb = _db.Tarefas.Find(id); //busca a terafa no banco de dados pelo id, se não encontrar retorna null, caso contrário retorna a tarefa encontrada.
        if (tarefaDb == null)
            throw new TarefaErro("Tarefa não encontrada."); //404 error

        return tarefaDb;
    }

    public void Delete(int id)
    {
        var tarefaDb = _db.Tarefas.Find(id); //busca a terafa no banco de dados pelo id, se não encontrar retorna null, caso contrário retorna a tarefa encontrada.
        if (tarefaDb == null)
            throw new TarefaErro("Tarefa não encontrada."); //404 error

        _db.Tarefas.Remove(tarefaDb);
        _db.SaveChanges();
    }

    public Tarefa AtualizarStatus (int id, bool concluida)
    {
        var tarefaDb = _db.Tarefas.Find(id); //busca a terafa no banco de dados pelo id, se não encontrar retorna null, caso contrário retorna a tarefa encontrada.
        if (tarefaDb == null)
            throw new TarefaErro("Tarefa não encontrada."); 

        tarefaDb.Concluida = concluida;

        _db.Tarefas.Update(tarefaDb);
        _db.SaveChanges();
        return tarefaDb;
    }

}
