# WizCoPedidos

API simples para gestão de pedidos, desenvolvida para teste técnico backend.

## Tecnologias utilizadas

- **.NET**: `8` (Target Framework `net8.0`)
- **SDK**: `8.0.0` com `rollForward: latestMinor` (arquivo `global.json`)
- **ORM**: Entity Framework Core `8.0.26`
- **Banco de dados**: SQLite (`app.db`)
- **Validação**: FluentValidation (`11.11.0` + integração ASP.NET Core)
- **Mapeamento de objetos**: AutoMapper (`12.0.1`)
- **Logs**: Serilog (`Serilog.AspNetCore`, output JSON no console)
- **Documentação da API**: Swagger / OpenAPI (Swashbuckle)
- **Testes unitários**: xUnit + Moq

## Estrutura do projeto

- `WizCoPedidos.WebApi`: API principal
- `WizCoPedidos.WebApi.Tests`: testes unitários
- `WizCoPedidos.sln`: solution principal

### Estrutura de pastas (WebApi)

- `Controllers/`: endpoints HTTP da API.
- `Services/`: regras de negócio e orquestração dos casos de uso.
- `Services/Interfaces/`: contratos das camadas de serviço.
- `Repositories/`: acesso a dados via EF Core.
- `Repositories/Interfaces/`: contratos da camada de repositório.
- `Data/`: `AppDbContext` e configurações de persistência.
- `Entidades/`: modelos de domínio persistidos no banco.
- `Models/Dto/`: contratos de entrada e saída da API.
- `Models/Enum/`: enums de domínio (ex.: status do pedido).
- `Mappings/`: perfis do AutoMapper para conversão entre entidades e DTOs.
- `Validators/`: validações de entrada com FluentValidation.
- `Middlewares/`: middleware global de tratamento de exceções.
- `Exceptions/`: exceções de negócio/customizadas.
- `Migrations/`: histórico de migrations do EF Core.

## Como executar a aplicação

### Pré-requisitos

- .NET SDK 8 instalado

### Passos

1. Restaurar pacotes:

```bash
dotnet restore
```

2. Compilar a solution:

```bash
dotnet build WizCoPedidos.sln
```

3. Executar a API:

```bash
dotnet run --project WizCoPedidos.WebApi
```

4. Acessar documentação Swagger:

- `http://localhost:5250/swagger`

> A porta padrão vem do `launchSettings.json` do projeto WebApi.

## Como executar os testes

```bash
dotnet test WizCoPedidos.sln
```

## Banco de dados

- Provedor: SQLite
- Connection string padrão (`appsettings.json`):

```json
"DefaultConnection": "Data Source=app.db"
```

Ao iniciar a API, as migrations do EF Core são aplicadas automaticamente (`Database.Migrate()` no startup).

O arquivo `app.db` é criado/atualizado no projeto `WizCoPedidos.WebApi`.

## Logs

- Logger: Serilog
- Formato: JSON no console (bom para observabilidade e parse por ferramentas)
- Níveis padrão:
  - `Information` para aplicação
  - `Warning` para logs de infraestrutura (`Microsoft`, `System`)

## Status de API e tratamento de erros

- Erros de validação: `400 Bad Request`
- Regras de negócio inválidas: `400 Bad Request`
- Recurso não encontrado: `404 Not Found`
- Erros não tratados: `500 Internal Server Error`

As respostas de erro são padronizadas via `ProblemDetails` em middleware global.

## Observações

- Swagger com comentários XML habilitados.
