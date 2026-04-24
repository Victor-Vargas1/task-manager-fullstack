namespace ApiTarefas.ModelViews;

public struct HomeView
{
// Para mostra o valor padrão ou nulo "= default!;" posso utilizar "?" informando que pode ser nulo ou "Required" informa que é requidiro.
  public required string Mensagem { get; set; } 
  public required string Documentacao { get; set; } 
}
 