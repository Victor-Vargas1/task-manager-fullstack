using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ApiTarefas.ModelViews;

namespace ApiTarefas.Controllers;

//assinaturas  / mascaras
[ApiController]
[Route("/")] //mapeamento de rota
public class HomeController : ControllerBase
{
    [HttpGet] //rota
    public HomeView Index()
    {
        return new HomeView
        {
            Mensagem = "Bem Vindo a API de Tarefas",
            Documentacao = "http://localhost:5001/swagger"
        };

    }
}
