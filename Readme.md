# Referencias 

## Links
- https://github.com/dotnet/EntityFramework.Docs/blob/live/samples/core/Querying/ClientEvaluation/Program.cs
- https://learn.microsoft.com/pt-br/ef/core/querying/client-eval

## Possíveis Erros 

Se esse erro ocorreu: Unable to create 'DcContext' of type 'RuntimeType.

O dotnet ef não consegue criar a instância do DbContext em tempo de desing.

Microsoft.EntintyFrameworkCore.Desing é obrigatório para gerar as migrações.

Como corrigir:
1 - Instale o pacote
dotnet add package Microsoft.EntintyFrameworkCore.Desing

2 - Rode o comando novamente
dotnet ef migrations add InitialCreate

#dotnet #csharp #dotnetdeveloper #developers #microsoft  #dotnetcore 