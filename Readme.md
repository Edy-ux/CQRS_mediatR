# Para rodar essa aplicação execute

```cmd
 > dotnet restore
```
```cmd
 > dotnet run
```

## Possíveis Erros EF Core


Se esse erro ocorreu: Unable to create 'DcContext' of type 'RuntimeType.
O dotnet ef não consegue criar a instância do DbContext em tempo de desing.


O pacote **Microsoft.EntintyFrameworkCore.Desing** é obrigatório para gerar as migrações.

#### Como corrigir:

**1 - Instale o pacote**

```sh
dotnet add package Microsoft.EntintyFrameworkCore.Desing
```

**2 - Rode o comando novamente**

```sh
dotnet ef migrations add InitialCreate
```

### Solução 2

### IDesignTimeDbContextFactory quando o EF não consegui criar a instancia do DBContext em tempo de Deging time. 

A interface IDesignTimeDbContextFactory<T> é específica para o design-time do EF Core. Ela diz ao EF:

> "Quando você precisar criar um DbContext durante o design-time, use esta factory"

```cs
public class GamePlayerDbContextFactory : IDesignTimeDbContextFactory<GamePlayerDbContext>
{
    public GamePlayerDbContext CreateDbContext(string[] args)
    {
        // Aqui você fornece as configurações que o EF precisa
        var **optionsBuilder** = new DbContextOptionsBuilder<GamePlayerDbContext>();
        optionsBuilder.UseSqlServer("Server=PCDOJOHN;Database=GamePlayer;...");

        return new GamePlayerDbContext(optionsBuilder.Options);
    }
}
```
**Resumo**

A IDesignTimeDbContextFactory é a ponte entre o EF Core (que precisa criar DbContext no design-time) e sua aplicação (que tem configurações específicas). Ela diz ao EF exatamente como criar o DbContext quando não há container de DI disponível.
É uma solução elegante que mantém a separação entre design-time e runtime, permitindo que o EF trabalhe independentemente da sua aplicação.

##  Conhecimentos

### Uso de class x records

**Records** 

1. **Imutabilidade por padrão**: Records são imutáveis por padrão, o que é ideal para Value Objects com o `PlayerStatus`
2. **Implementação automática**: O `record` já implementa automaticamente:

    - `Equals()`
    - `GetHashCode()`
    -  Operadores `==` e `!=`
    - `ToString()`


🏆 **Conclusão:**

Nesse Projeto usamos Duas abordagem para tratamento de erros X `Exeption`: 

- OneOf para business logic
- Middleware para exceções técnicas
- Separação clara de responsabilidades
- É considerada uma best practice moderna em .NET, especialmente para APIs robustas e   manutenível
- 
✅ Domínio: Exceções para regras de negócio
✅ Application: OneOf para controle de fluxo
✅ Infrastructure: Middleware para exceções técnicas


Domain (Exception) →  Application (OneOf) → Controller (Match) → Response
        ↓                    ↓                       ↓
   Business Rules       Error Handling            HTTP Response

#dotnet #csharp #dotnetdeveloper #developers #microsoft  #dotnetcore 



/src
 ├── Domain
 │   ├── Entities
 │   │   └── GamePlayer.cs
 │   ├── Exceptions
 │   │   ├── GamePlayerValidationException.cs
 │   │   ├── GamePlayerRepositoryException.cs
 │   │   └── DatabaseException.cs
 │   ├── ValueObjects
 │   └── Enums
 │       └── TypeError.cs
 │   └── Interfaces
 │       └── IGamePlayerRepository.cs
 │
 ├── Application
 │   ├── Requests
 │   │   └── CreateGamePlayerRequest.cs
 │   ├── Commands
 │   │   └── CreatePlayer
 │   │        ├── CreatePlayerCommand.cs
 │   │        ├── CreatePlayerCommandHandler.cs
 │   │        └── CreatePlayerNotification.cs
 │   ├── Responses
 │   │   └── CreationPlayerError.cs
 │   ├── Validators
 │   │   └── CreateGamePlayerRequestValidator.cs
 │   └── Common
 │       └── Maybe, Result, etc (se criar tipos funcionais personalizados)
 │
 ├── Infrastructure
 │   ├── Persistence
 │   │   ├── GamePlayerDbContext.cs
 │   │   └── Repositories
 │   │       └── GamePlayerRepository.cs
 │   ├── Middleware
 │   │   └── ExceptionHandlingMiddleware.cs
 │   └── Configurations
 │       └── GamePlayerEntityTypeConfiguration.cs (se usar FluentAPI separada)
 │
 ├── API
 │   ├── Controllers
 │   │   └── GamePlayerController.cs
 │   ├── Extensions
 │   │   └── DependencyInjection.cs
 │   └── Program.cs / Startup.cs
