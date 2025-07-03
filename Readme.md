
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

#dotnet #csharp #dotnetdeveloper #developers #microsoft  #dotnetcore 