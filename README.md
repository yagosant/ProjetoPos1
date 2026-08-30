# ProjetoPos1 — API REST de Pedidos (Desafio Final - Arquiteto(a) de Software)

## 📋 Sobre o projeto

Este projeto é a entrega do **Desafio Final do Bootcamp Arquiteto(a) de Software**. Consiste em uma **API REST** desenvolvida em **ASP.NET Core Web API**, seguindo o padrão arquitetural **MVC**, que expõe operações de **CRUD** sobre o domínio **Pedido** de uma empresa de vendas on-line.

## 🎯 Objetivo do desafio

Projetar, documentar e implantar uma API REST que disponibilize dados de Pedido publicamente para os parceiros da empresa, exercitando conceitos de arquitetura de software, requisitos arquiteturais, design patterns e o padrão MVC.

## 🛠️ Tecnologias utilizadas

- **.NET 9** / ASP.NET Core Web API
- **C#**
- **Entity Framework Core** (ORM)
- **SQLite** (banco de dados, para persistência)
- **Swagger / Swashbuckle.AspNetCore** (documentação interativa da API)

## 🏗️ Arquitetura (padrão MVC)

O projeto segue o padrão **MVC (Model-View-Controller)**, adaptado para uma API REST (sem a camada de View tradicional, substituída pelas respostas JSON), com a adição de uma camada de **Service** e **Repository** para melhor separação de responsabilidades:

```
ProjetoPos1/
├── Controllers/
│   └── PedidoController.cs        # Recebe requisições HTTP e retorna respostas
├── Models/
│   ├── Pedido.cs                  # Entidade de domínio
│   └── StatusPedido.cs            # Enum com os status possíveis de um pedido
├── Services/
│   ├── IPedidoService.cs          # Contrato do service
│   └── PedidoService.cs           # Lógica de negócio / regras
├── Repositories/
│   ├── IPedidoRepository.cs       # Contrato do repository
│   └── PedidoRepository.cs        # Acesso aos dados (via EF Core)
├── Data/
│   └── AppDbContext.cs            # Contexto do Entity Framework Core
├── Migrations/                    # Histórico de migrations do banco (gerado pelo EF Core)
├── appsettings.json               # Configurações da aplicação (connection string etc.)
├── Program.cs                     # Ponto de entrada; registra serviços e configura o pipeline HTTP
└── ProjetoPos1.csproj             # Arquivo do projeto
```

**Papel de cada camada:**

| Camada | Responsabilidade |
|---|---|
| **Controller** | Recebe a requisição HTTP, delega para o Service e devolve a resposta (status code + dados). Não contém regra de negócio. |
| **Model** | Representa a entidade de domínio `Pedido` e o enum `StatusPedido`. |
| **Service** | Contém a lógica de negócio e validações, orquestrando o acesso aos dados via Repository. |
| **Repository** | Abstrai o acesso aos dados, usando o `AppDbContext` (EF Core) para consultar/persistir no banco SQLite. |
| **Data (DbContext)** | Configura o mapeamento entre a entidade `Pedido` e a tabela do banco de dados. |

## 📦 Modelo de dados — Pedido

| Campo | Tipo | Descrição |
|---|---|---|
| `Id` | `int` | Identificador único (gerado automaticamente) |
| `ClienteNome` | `string` | Nome do cliente que fez o pedido |
| `Produtos` | `List<string>` | Lista de produtos do pedido |
| `DataPedido` | `DateTime` | Data em que o pedido foi realizado |
| `Status` | `StatusPedido` (enum) | Pendente, Pago, Enviado, Entregue ou Cancelado |
| `ValorTotal` | `decimal` | Valor total do pedido |

## 🔌 Endpoints da API

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/Pedido/ListarTodosPedidos` | Lista todos os pedidos |
| `GET` | `/api/Pedido/BuscarPedido/{id}` | Busca um pedido pelo ID |
| `GET` | `/api/Pedido/BuscaPedidoParcial/{nome}` | Busca pedidos pelo nome do cliente (busca parcial) |
| `GET` | `/api/Pedido/ContarPedidos` | Retorna o total de pedidos cadastrados |
| `POST` | `/api/Pedido/CriarPedido` | Cria um novo pedido |
| `PUT` | `/api/Pedido/AtualizarPedido/{id}` | Atualiza um pedido existente |
| `DELETE` | `/api/Pedido/RemoverPedido/{id}` | Remove um pedido |

> A documentação interativa completa (com exemplos de requisição/resposta) fica disponível via Swagger, conforme instruções abaixo.

### 📄 Exemplos de requisição/resposta

**Criar pedido — `POST /api/Pedido/CriarPedido`**

Requisição:
```json
{
  "clienteNome": "Maria Silva",
  "produtos": ["Notebook", "Mouse sem fio"],
  "valorTotal": 3499.90
}
```

> Não é necessário enviar `id`, `dataPedido` ou `status` — esses campos são preenchidos automaticamente pela API (`dataPedido` recebe a data/hora atual, e `status` nasce como `"Pendente"`).

Resposta (`201 Created`):
```json
{
  "id": 1,
  "clienteNome": "Maria Silva",
  "produtos": ["Notebook", "Mouse sem fio"],
  "dataPedido": "2026-08-30T14:32:00Z",
  "status": "Pendente",
  "valorTotal": 3499.90
}
```

**Atualizar pedido — `PUT /api/Pedido/AtualizarPedido/1`**

Requisição:
```json
{
  "clienteNome": "Maria Silva",
  "produtos": ["Notebook", "Mouse sem fio", "Mochila"],
  "dataPedido": "2026-08-30T10:00:00Z",
  "status": "Pago",
  "valorTotal": 3699.90
}
```

> O campo `status` aceita o nome do enum como texto (`"Pendente"`, `"Pago"`, `"Enviado"`, `"Entregue"` ou `"Cancelado"`), não o número.

Resposta (`200 OK`): o pedido atualizado, no mesmo formato acima.

**Buscar pedido por ID — `GET /api/Pedido/BuscarPedido/1`**

Resposta (`200 OK`): o pedido encontrado, ou `404 Not Found` com:
```json
{
  "mensagem": "Pedido com Id 1 não encontrado."
}
```

**Remover pedido — `DELETE /api/Pedido/RemoverPedido/1`**

Resposta: `204 No Content` em caso de sucesso, ou `404 Not Found` (mesmo formato de erro acima) se o Id não existir.

## ▶️ Como executar o projeto

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download) instalado
- Visual Studio Community 2022 (ou VS Code com C# Dev Kit)

### Passo a passo

1. Clone o repositório:
   ```bash
   git clone <url-do-repositorio>
   ```

2. Abra a solução `ProjetoPos1.sln` no Visual Studio (ou a pasta no VS Code).

3. Restaure os pacotes NuGet (o Visual Studio faz isso automaticamente ao abrir; se necessário, rode):
   ```bash
   dotnet restore
   ```

4. Aplique as migrations para criar o banco de dados SQLite:
   - Pelo **Package Manager Console** (Visual Studio):
     ```powershell
     Update-Database
     ```
   - Ou via terminal:
     ```bash
     dotnet ef database update
     ```

5. Execute o projeto (F5 no Visual Studio, ou):
   ```bash
   dotnet run
   ```

6. Acesse a documentação interativa do Swagger no navegador:
   ```
   https://localhost:<porta>/swagger
   ```
   (a porta exata aparece no terminal ao rodar o projeto)

## 🗄️ Banco de dados

O projeto utiliza **SQLite** como banco de dados, com o arquivo `pedidos.db` sendo criado automaticamente na raiz do projeto ao rodar as migrations. Isso elimina a necessidade de instalar ou configurar um servidor de banco de dados separado.

## 📐 Diagrama de arquitetura

O desenho da arquitetura (diagrama de componentes/C4) está disponível em [`docs/arquitetura.drawio`](./docs/arquitetura.drawio) *(ajustar caminho conforme onde o arquivo for salvo)*.

## ✍️ Autor

Yago dos Santos Ribeiro
