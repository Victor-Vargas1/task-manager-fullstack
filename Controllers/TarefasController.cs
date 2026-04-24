using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ApiTarefas.ModelViews;
using ApiTarefas.Database;
using ApiTarefas.Models;
using ApiTarefas.Dto;
using ApiTarefas.Model.Erros;
using ApiTarefas.Servicos;

namespace ApiTarefas.Controllers;

//assinaturas  / mascaras
[ApiController]
[Route("/tarefas")] //mapeamento de rota
public class TarefasController : ControllerBase
{

    public TarefasController(ITarefaServico servico)
    {
        _servico = servico;
    }

    private ITarefaServico _servico;

    [HttpGet] //rota
    public IActionResult Index(int page = 1)
    {
        var tarefas = _servico.Listar(page);
        return StatusCode(200, tarefas);

    }

    [HttpPost]
    public IActionResult Create([FromBody] TarefaDto tarefaDto)
    {
        try
        {
            var tarefa = _servico.Incluir(tarefaDto);
            return StatusCode(201, tarefa); //201 Created      
        }
        catch (TarefaErro erro)
        {
            return StatusCode(400, new ErroView { Mensagem = erro.Message }); //400 error
        }

    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] TarefaDto tarefaDto)
    {
        try
        {
            var tarefa = _servico.Update(id, tarefaDto);
            return StatusCode(200, tarefa);
        }
        catch (TarefaErro erro)
        {
            return StatusCode(400, new ErroView { Mensagem = erro.Message }); //400 error
        }

    }

    [HttpGet("{id}")]
    public IActionResult Show([FromRoute] int id)
    {
        try
        {
            var tarefaDb = _servico.BuscaPorId(id);
            return StatusCode(200, tarefaDb); //200 OK
        }
        catch (TarefaErro erro)
        {
            return StatusCode(404, new ErroView { Mensagem = erro.Message }); //404 error
        }

    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            _servico.Delete(id);
            return StatusCode(204); //204 No Content
        }
        catch (TarefaErro erro)
        {
            return StatusCode(404, new ErroView { Mensagem = erro.Message }); //404 error
        }

    }

    [HttpPatch("{id}/status")]
    public IActionResult AtualizarStatus(int id, [FromBody] bool concluida)
    {
        try
        {
            var tarefa = _servico.AtualizarStatus(id, concluida);
            return Ok(tarefa);
        }
        catch (TarefaErro erro)
        {
            return StatusCode(404, new ErroView { Mensagem = erro.Message });
        }
    }
}
