# API ToDo List

API REST para gerenciamento de tarefas e tarefas diárias.

## Conexão do Frontend

### URL Base da API
```
http://localhost:5000/api
```

### CORS Configurado
A API aceita requisições de:
- `http://127.0.0.1:5500`
- `http://localhost:5500`
- `http://127.0.0.1:5501`

### Exemplo de Conexão Básica (JavaScript/Fetch)
```javascript
const API_URL = 'http://localhost:5000/api';

// Exemplo: GET todas as tarefas
fetch(`${API_URL}/tarefa`)
  .then(response => response.json())
  .then(data => console.log(data))
  .catch(error => console.error('Erro:', error));
```

---

## Endpoints de Tarefas

### 1. GET - Listar Todas as Tarefas
**Endpoint:** `GET /api/tarefa`

**Parâmetros:** Nenhum

**Resposta (200 OK):**
```json
[
  {
    "id": 1,
    "descricao": "Comprar leite",
    "concluida": false
  },
  {
    "id": 2,
    "descricao": "Estudar C#",
    "concluida": true
  }
]
```

**Exemplo Frontend:**
```javascript
async function getTarefas() {
  const response = await fetch(`${API_URL}/tarefa`);
  const tarefas = await response.json();
  console.log(tarefas);
}
```

---

### 2. GET - Obter Tarefa por ID
**Endpoint:** `GET /api/tarefa/{id}`

**Parâmetros:**
- `id` (integer, obrigatório) - ID da tarefa

**Resposta (200 OK):**
```json
{
  "id": 1,
  "descricao": "Comprar leite",
  "concluida": false
}
```

**Resposta (404 Not Found):**
```json
null
```

**Exemplo Frontend:**
```javascript
async function getTarefaById(id) {
  const response = await fetch(`${API_URL}/tarefa/${id}`);
  const tarefa = await response.json();
  console.log(tarefa);
}
```

---

### 3. POST - Criar Nova Tarefa
**Endpoint:** `POST /api/tarefa`

**Parâmetros (Body JSON):**
```json
{
  "descricao": "Fazer exercícios",
  "concluida": false
}
```

**Campos:**
- `descricao` (string, obrigatório) - Descrição da tarefa
- `concluida` (boolean, obrigatório) - Status inicial da tarefa

**Resposta (201 Created):**
```json
{
  "id": 3,
  "descricao": "Fazer exercícios",
  "concluida": false
}
```

**Resposta (400 Bad Request):**
```
Bad Request se ModelState for inválido (dados ausentes ou tipo incorreto)
```

**Exemplo Frontend:**
```javascript
async function criarTarefa(descricao) {
  const response = await fetch(`${API_URL}/tarefa`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      descricao: descricao,
      concluida: false
    })
  });
  const novaTarefa = await response.json();
  console.log('Tarefa criada:', novaTarefa);
}
```

---

### 4. PUT - Atualizar Tarefa
**Endpoint:** `PUT /api/tarefa/{id}`

**Parâmetros URL:**
- `id` (integer, obrigatório) - ID da tarefa

**Parâmetros (Body JSON):**
```json
{
  "id": 1,
  "descricao": "Comprar leite integral",
  "concluida": true
}
```

**Campos:**
- `id` (integer, obrigatório) - Deve corresponder ao ID da URL
- `descricao` (string, obrigatório) - Descrição atualizada
- `concluida` (boolean, obrigatório) - Status atualizado

**Resposta (204 No Content):**
Sem corpo (sucesso)

**Resposta (400 Bad Request):**
Se o ID do body não corresponder ao ID da URL

**Resposta (404 Not Found):**
Se a tarefa não existir

**Exemplo Frontend:**
```javascript
async function atualizarTarefa(id, descricao, concluida) {
  const response = await fetch(`${API_URL}/tarefa/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      id: id,
      descricao: descricao,
      concluida: concluida
    })
  });
  
  if (response.status === 204) {
    console.log('Tarefa atualizada com sucesso');
  }
}
```

---

### 5. DELETE - Deletar Tarefa
**Endpoint:** `DELETE /api/tarefa/{id}`

**Parâmetros:**
- `id` (integer, obrigatório) - ID da tarefa

**Resposta (204 No Content):**
Sem corpo (sucesso)

**Resposta (404 Not Found):**
Se a tarefa não existir

**Exemplo Frontend:**
```javascript
async function deletarTarefa(id) {
  const response = await fetch(`${API_URL}/tarefa/${id}`, {
    method: 'DELETE'
  });
  
  if (response.status === 204) {
    console.log('Tarefa deletada com sucesso');
  }
}
```

---

## Endpoints de Tarefas Diárias

### 1. GET - Listar Todas as Tarefas Diárias
**Endpoint:** `GET /api/tarefadiaria`

**Parâmetros:** Nenhum

**Resposta (200 OK):**
```json
[
  {
    "id": 1,
    "descricao": "Exercício matinal",
    "concluidaHoje": true,
    "dataUltimaConclusao": "2026-05-13T10:30:00"
  },
  {
    "id": 2,
    "descricao": "Meditar",
    "concluidaHoje": false,
    "dataUltimaConclusao": null
  }
]
```

**Exemplo Frontend:**
```javascript
async function getTarefasDiarias() {
  const response = await fetch(`${API_URL}/tarefadiaria`);
  const tarefasDiarias = await response.json();
  console.log(tarefasDiarias);
}
```

---

### 2. GET - Obter Tarefa Diária por ID
**Endpoint:** `GET /api/tarefadiaria/{id}`

**Parâmetros:**
- `id` (integer, obrigatório) - ID da tarefa diária

**Resposta (200 OK):**
```json
{
  "id": 1,
  "descricao": "Exercício matinal",
  "concluidaHoje": true,
  "dataUltimaConclusao": "2026-05-13T10:30:00"
}
```

**Resposta (404 Not Found):**
```json
null
```

---

### 3. POST - Criar Nova Tarefa Diária
**Endpoint:** `POST /api/tarefadiaria`

**Parâmetros (Body JSON):**
```json
{
  "descricao": "Ler um livro",
  "concluidaHoje": false,
  "dataUltimaConclusao": null
}
```

**Campos:**
- `descricao` (string, obrigatório, máximo 255 caracteres) - Descrição da tarefa
- `concluidaHoje` (boolean, obrigatório) - Se foi concluída hoje
- `dataUltimaConclusao` (datetime, opcional) - Última data de conclusão

**Resposta (201 Created):**
```json
{
  "id": 3,
  "descricao": "Ler um livro",
  "concluidaHoje": false,
  "dataUltimaConclusao": null
}
```

**Resposta (400 Bad Request):**
Se descrição for vazia, null ou exceder 255 caracteres

**Exemplo Frontend:**
```javascript
async function criarTarefaDiaria(descricao) {
  const response = await fetch(`${API_URL}/tarefadiaria`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      descricao: descricao,
      concluidaHoje: false,
      dataUltimaConclusao: null
    })
  });
  const novaTarefa = await response.json();
  console.log('Tarefa diária criada:', novaTarefa);
}
```

---

### 4. PUT - Atualizar Tarefa Diária
**Endpoint:** `PUT /api/tarefadiaria/{id}`

**Parâmetros URL:**
- `id` (integer, obrigatório) - ID da tarefa diária

**Parâmetros (Body JSON):**
```json
{
  "id": 1,
  "descricao": "Exercício matinal - 30 minutos",
  "concluidaHoje": true,
  "dataUltimaConclusao": "2026-05-13T14:20:00"
}
```

**Campos:**
- `id` (integer, obrigatório) - Deve corresponder ao ID da URL
- `descricao` (string, obrigatório, máximo 255 caracteres) - Descrição atualizada
- `concluidaHoje` (boolean, obrigatório) - Status atualizado
- `dataUltimaConclusao` (datetime, opcional) - Data atualizada

**Resposta (204 No Content):**
Sem corpo (sucesso)

**Resposta (400 Bad Request):**
Se o ID do body não corresponder ao ID da URL ou descrição inválida

**Resposta (404 Not Found):**
Se a tarefa diária não existir

**Exemplo Frontend:**
```javascript
async function atualizarTarefaDiaria(id, descricao, concluidaHoje, dataUltimaConclusao) {
  const response = await fetch(`${API_URL}/tarefadiaria/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      id: id,
      descricao: descricao,
      concluidaHoje: concluidaHoje,
      dataUltimaConclusao: dataUltimaConclusao
    })
  });
  
  if (response.status === 204) {
    console.log('Tarefa diária atualizada com sucesso');
  }
}
```

---

### 5. DELETE - Deletar Tarefa Diária
**Endpoint:** `DELETE /api/tarefadiaria/{id}`

**Parâmetros:**
- `id` (integer, obrigatório) - ID da tarefa diária

**Resposta (204 No Content):**
Sem corpo (sucesso)

**Resposta (404 Not Found):**
Se a tarefa diária não existir

**Exemplo Frontend:**
```javascript
async function deletarTarefaDiaria(id) {
  const response = await fetch(`${API_URL}/tarefadiaria/${id}`, {
    method: 'DELETE'
  });
  
  if (response.status === 204) {
    console.log('Tarefa diária deletada com sucesso');
  }
}
```

---

## Tratamento de Erros no Frontend

Exemplo completo de tratamento de erros:

```javascript
async function fazerRequisicao(url, opcoes = {}) {
  try {
    const response = await fetch(url, opcoes);
    
    if (!response.ok) {
      switch(response.status) {
        case 400:
          console.error('Requisição inválida - verifique os parâmetros');
          break;
        case 404:
          console.error('Recurso não encontrado');
          break;
        case 500:
          console.error('Erro no servidor');
          break;
        default:
          console.error(`Erro HTTP: ${response.status}`);
      }
      return null;
    }
    
    // Para DELETE e PUT sem conteúdo, retorna sucesso
    if (response.status === 204) {
      return { sucesso: true };
    }
    
    return await response.json();
  } catch (error) {
    console.error('Erro na requisição:', error);
    return null;
  }
}
```

---

## Executar a API

### Pré-requisitos
- .NET 8.0
- MySQL configurado
- String de conexão em `appsettings.json`

### Rodar
```bash
dotnet run
```

A API estará disponível em `http://localhost:5000`

---

## Estrutura do Projeto

```
API ToDo List/
├── Controllers/
│   ├── TarefaController.cs
│   └── TarefaDiariaController.cs
├── Models/
│   ├── Tarefa.cs
│   └── TarefaDiaria.cs
├── Repositorios/
│   ├── ITarefaRepositorio.cs
│   ├── TarefaRepositorio.cs
│   ├── ITarefaDiariaRepositorio.cs
│   └── TarefaDiariaRepositorio.cs
├── Program.cs
└── appsettings.json
```
