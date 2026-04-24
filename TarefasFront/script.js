document.addEventListener("DOMContentLoaded", () => {
  const API = "http://localhost:5001/tarefas";

  const lista = document.getElementById("lista");
  const form = document.getElementById("form");
  const input = document.getElementById("titulo");
  const descricao = document.getElementById("descricao");
  const pesquisa = document.getElementById("pesquisa");
  const prazoInput = document.getElementById("prazo");

  let tarefasGerais = [];

  //FUNÇÃO DE URGÊNCIA
  function verificarUrgencia(dataPrazo) {
    if (!dataPrazo) return false;

    const hoje = new Date();
    const prazo = new Date(dataPrazo);

    const diferenca = prazo - hoje;
    const diasRestantes = Math.ceil(diferenca / (1000 * 60 * 60 * 24)); // Converte para dias

    return diasRestantes <= 3;
  }

  //Carregar tarefas
  async function carregarTarefas() {
    const res = await fetch(API);
    const tarefas = await res.json();

    tarefasGerais = tarefas;
    renderizarTarefas(tarefas);
  }

  //Criar ou Editar
  form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const id = form.dataset.id;

    const dados = {
      titulo: input.value,
      descricao: descricao.value,
      prazo: prazoInput.value,
    };

    if (id) {
      await fetch(`${API}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(dados),
      });

      form.dataset.id = "";
    } else {
      await fetch(API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(dados),
      });
    }

    //Limpar campos
    input.value = "";
    descricao.value = "";
    prazoInput.value = "";

    carregarTarefas();
  });

  //Deletar
  window.deletar = async function (id) {
    await fetch(`${API}/${id}`, { method: "DELETE" });
    carregarTarefas();
  };

  //Limpar campos
  window.limparCampos = function () {
    if (confirm("Deseja limpar os campos?")) {
      document.getElementById("titulo").value = "";
      document.getElementById("descricao").value = "";
      document.getElementById("prazo").value = "";
    }
  };

  //Editar
  window.editar = function (id, titulo, desc) {
    input.value = titulo;
    descricao.value = desc;
    form.dataset.id = id;
  };

  //Concluir / Desmarcar
  window.toggleStatus = async function (id, concluida) {
    await fetch(`${API}/${id}/status`, {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(concluida),
    });

    carregarTarefas();
  };

  //Pesquisa
  pesquisa.addEventListener("input", () => {
    const termo = pesquisa.value.toLowerCase();

    const filtradas = tarefasGerais.filter(
      (t) =>
        t.titulo.toLowerCase().includes(termo) ||
        (t.descricao ?? "").toLowerCase().includes(termo),
    );

    renderizarTarefas(filtradas);
  });

  //Inicial
  carregarTarefas();

  // Renderiza tarefas
  function renderizarTarefas(tarefas) {
    tarefas.sort((a, b) => a.concluida - b.concluida);

    lista.innerHTML = tarefas
      .map((t) => {
        const urgente = verificarUrgencia(t.prazo) && !t.concluida;

        return `
          <div class="tarefa ${t.concluida ? "concluida" : ""} ${urgente ? "urgente" : ""}">
            
            <strong>${t.titulo}</strong>
            <p>${t.descricao ?? ""}</p>

         <small>
              Prazo: ${
                t.prazo
                  ? new Date(t.prazo).toLocaleDateString("pt-BR")
                  : "Não definido"
              }
          </small>

            ${urgente ? "<p class='alerta'>⚠️ URGENTE</p>" : ""}

            <label class="check">
              <input type="checkbox"
                ${t.concluida ? "checked" : ""}
                onchange="toggleStatus(${t.id}, this.checked)">
                <span>Concluído</span>
            </label>
            

            <div class="acoes">
              <button onclick='editar(${t.id}, ${JSON.stringify(t.titulo)}, ${JSON.stringify(t.descricao ?? "")})'>
                Editar
              </button>
              <button onclick="deletar(${t.id})">
                Excluir
              </button>
            </div>
          </div>
        `;
      })
      .join("");
  }
});
