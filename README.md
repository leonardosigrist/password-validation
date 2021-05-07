# Password Validation

O projeto *Password Validation* tem como objetivo disponibilizar uma API para fazer a validação de senhas e demonstrar a utilização de boas práticas de codificação. 

Uma senha é considerada válida quando atingir todos os seguintes requisitos:

- Nove ou mais caracteres

- Ao menos 1 dígito

- Ao menos 1 letra minúscula

- Ao menos 1 letra maiúscula

- Ao menos 1 caractere especial

  - Considere como especial os seguintes caracteres: !@#$%^&*()-+

- Não possuir caracteres repetidos dentro do conjunto

  

# Linguagem e Bibliotecas

A API foi desenvolvida utilizando a linguagem **C#** em conjunto com o framework **.NET Core**.  Os testes unitários foram desenvolvidos utilizando a biblioteca **xUnit** e a interface do swagger foi provido com ajuda da biblioteca **Swashbuckle**.



# Como executar?

Para que seja possível executar o projeto é necessário que você tenha instalado na sua máquina a [SDK do .NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1). 

Após a instalação concluída, existem duas formas de executar a API na sua máquina local, a seguir vou mostrar cada uma delas em detalhes.



## Usando Visual Studio

Caso você tenha o Visual Studio instalado, basta abrir o arquivo `src/PasswordValidation.sln`. A IDE abrirá toda a estrutura do projeto. 

Selecione o menu `Build > Build Solution`. Isso fará com que os pacotes nuget sejam baixados e o projeto seja compilado. Aguarde até que o build seja bem sucedido e em seguida clique no botão com símbolo de play no menu superior para executar o aplicativo:

![image-20210505232152834](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210505232152834.png)

**Obs:** essa aplicação pode ser executada tanto utilizando a opção **IIS Express** quanto **PasswordValidation** (self-hosted).



## Usando .Net CLI

Abra o prompt de comandos do Windows, powershell ou outro terminal de sua escolha, navegue até a pasta `src/Password.API` e execute os seguintes comandos: 

```
dotnet build
dotnet run
```

Esses comandos vão servir para fazer download das dependências, compilar e rodar a aplicação.



## Testando com Swagger

Independente da forma escolhida para rodar o sistema, por padrão ele será executado na porta 3001. Quando executado pelo Visual Studio ele abrirá automaticamente o browser na página do Swagger, que pode também ser aberto manualmente entrando no browser e procurando pelo endereço http://localhost:3001/swagger 

![image-20210506185959166](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506185959166.png)

Para executar a action e validar se a sua senha atende aos requisitos mínimos para ser considerada válida você precisa clicar no botão "Try it out", preencher o campo **password** com a senha a ser validada, o **x-api-key** com o token de segurança (explicado no tópico "Segurança" a seguir) e clicar em "Execute".

O Swagger apresenta na interface as descrições contidas nos comentários do código, como descrição da action, das variáveis de parâmetro e os códigos de response previstos. Para isso foi necessário alterar o arquivo `.csproj` da API para gerar um XML com a documentação presente nos comentários:

![image-20210506194243093](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506194243093.png)

E então solicitar ao Swagger que inclua os comentários do XML em sua inicialização no Startup do projeto:

 ![image-20210506194741821](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506194741821.png)



# Segurança

Para garantir uma segurança mínima à API optei por validar um token que é passado pelo header do request HTTP. O nome do header **x-api-key** é baseado na nomenclatura padrão utilizado pelo AWS API Gateway. A chave passada no header de cada requisição é confrontada com o valor presente no arquivo de configuração, como mostrado na figura abaixo:

![image-20210506191543485](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506191543485.png)

Caso a chave não seja passada na requisição, será retornado um response com status **401 - Unauthorized** informando que você não está autorizado a obter a informação.

Para que uma action ou um controller possam receber essa validação de segurança basta anotar o método com o attribute `[ApiKeyAuthorize]`. Caso seja anotado o controller, todas as actions contidas naquela classe serão validadas, mas se optar por proteger uma única action basta anotá-la apenas na assinatura do método específico.

![image-20210506192314824](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506192314824.png)



# Testes

Foram utilizados testes unitários para garantir cada uma das regras de validação da senha. A nomenclatura dos métodos seguiram o seguinte padrão: 

`[Nome do método que está sendo validado]_[Critério que está sendo avaliado]_[Retorno esperado]` 

Abaixo um exemplo utilizando esse padrão de nomenclatura:

`IsValid_LessThanNineCharacters_ReturnsFalse`

Sempre que possível os métodos fazem a validação de mais de um valor para garantir que a determinada regra esteja válida com uma diversidade maior de inputs.

![image-20210506200057013](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506200057013.png)



## Como executar?

Para executar os testes no Visual Studio basta ir no menu superior `Test > Run All Tests` e acompanhar o resultado da execução no Test Explorer (caso não visualize a tab do Test Explorer acesse o menu `View > Test Explorer`).

![image-20210506200531102](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506200531102.png)

Também é possível executar os testes utilizando o .NET CLI. Basta abrir um prompt de comandos, navegar até a pasta `src/Password.Services.Test` e executar o comando abaixo:

```
dotnet test
```



# Padrões

## Open/Closed Principle

Para solucionar esse problema utilizei o conceito do princípio do aberto/fechado, onde as classes ficam fechadas para modificação mas abertas para extensão. A ideia foi criar um serviço base `IPasswordValidator` que define um contrato básico, definindo que toda validação de senha necessita uma senha para ser validada e retornará um resultado booleano informando se a senha é ou não válida.

![image-20210506203121150](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506203121150.png)

A partir dessa interface criei a classe `DefaultPasswordValidator`  que irá validar a senha de uma forma padrão com as regras já mencionadas acima, mas se no futuro surgir a necessidade de validar de forma mais branda uma senha, pode ser criada uma classe chamada `BasicPasswordValidator` que implementa a mesma interface `IPasswordValidator` mas que faz suas próprias regras de validação.



## Single Responsability Principle

Apliquei o princípio da responsabilidade única para que cada método tenha apenas um único motivo para ser alterado, ou seja, ele é responsável pela validação de uma única regra e não mais que isso.

Para isso, na classe `DefaultPasswordValidator` eu quebrei a regra de negócio em pequenos métodos, cada um validando um único quesito. Dessa forma se for preciso incluir um caractere especial na validação de caracteres especiais, fica mais fácil perceber que será necessário alterar apenas o método `HasAtLeastOneSpecialCharacter` e não será preciso entender todas as regras do método `IsValid` para saber onde alterar.

![image-20210506205014399](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506205014399.png)



## Singleton

Pensando que a aplicação não precisa ter mais de uma instância da classe `DefaultPasswordValidator` para fazer a validação de senhas, resolvi utilizar o padrão Singleton para injetar essa dependência e evitar o uso desnecessário de memória.

![image-20210506205658685](C:\Users\leona\AppData\Roaming\Typora\typora-user-images\image-20210506205658685.png)