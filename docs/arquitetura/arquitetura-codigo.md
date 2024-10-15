# Desenvolvimento

## Estrutura do Código

### Core
No core temos os seguintes projetos:
- **SnackTech.Core**: Este projeto é o responsável por implementar o "coração" da aplicação. Foi construído seguindo o Clean Architecture, atuando como uma ponte entre a as rotas da API com as camadas de persistência (Banco de Dados e integração com processadora de pagamento). 

### Infra.Persistence
Aqui temos os projetos com o código necessário para atuar com a camada de dados e outras persistências

- **SnackTech.Driver.DataBase**: Este projeto é responsável por todas as operações relacionadas ao banco de dados. Ele contém nosso DbContext, DataSources e outras classes que nos ajudam a criar e manipular o banco de dados.

- **SnackTech.Driver.MercadoPago**: O Mercado Pago está auxiliando como processador de pagamentos, por onde a aplicação envia o pedido, recebe uma forma válida para realizar o pagamento, que uma vez feito é notificado ao projeto para que faça o processamento interno do pedido

### Infra.web-api
Aqui ficam os projetos relacionado a interface externa, ao que o projeto fornece de comunicação para executar seus procedimentos.

- **SnackTech.Driver.API**: Este projeto é a interface do nosso sistema. Ele contém nossos Controllers, que recebem requisições do usuário e retornam respostas. Além disso, ele contém arquivos de configuração como o appsettings.

### Common
Aqui ficam os projetos que possuem estruturas de dados, interfaces e operações que possuem relação comum a qualquer um dos projetos envolvidos no desenho atual

- **SnackTech.Common**: Este projeto possui todas as estruturas de dados (DTOs) e interfaces que são de comum conhecimento entre os outros projetos da solução.

### Tests
Em tests, temos projetos voltados para as outras camadas, sendo os projetos:

- **SnackTech.Driver.DataBase.Tests**: Este projeto contém testes para a camada de banco de dados. Ele nos ajuda a garantir que nossas operações de banco de dados estão funcionando corretamente.
- **SnackTech.Driver.API.Tests**: Este projeto contém testes para a camada de API. Ele nos ajuda a garantir que nossos endpoints estão retornando as respostas corretas.
- **SnackTech.Core.Tests**: Este projeto contém os testes para o código do Core. Ele nos ajuda a garantir que toda a implementação feita seguindo o Clean Architecture alem da nossa lógica de negócios estejam funcionando corretamente.


### Modificabilidade

O projeto foi estruturado para facilitar a modificação e a expansão:

- **Adicionar Novas Funcionalidades:** Novos casos de uso e funcionalidades podem ser adicionados na camada SnackTech.Core. Atualize também a camada SnackTech.Driver.API para expor novos endpoints, se necessário.
- **Modificar Funcionalidades Existentes:** Alterações na lógica de negócios devem ser feitas na camada SnackTech.Core. As alterações na interação com o banco de dados são feitas na camada SnackTech.Driver.DataBase.
- **Manutenção:** A arquitetura modular permite que as alterações em uma parte do sistema (como o banco de dados ou a API) sejam feitas com impacto mínimo nas outras partes do sistema.

