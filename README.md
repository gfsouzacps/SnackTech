# SnackTech ![ ](LogoSnackTech.png)

## O Problema

Com o sucesso crescente da lanchonete de bairro e a expansão inevitável, a falta de um sistema de controle de pedidos começa a criar um caos. Imagine um cliente fazendo um pedido bem específico—tipo um hambúrguer com uma combinação única de ingredientes, acompanhando batatas fritas e uma bebida especial. O atendente anota no papel e entrega à cozinha, mas as chances de erro são altas. Sem um sistema eficiente, pedidos se perdem ou são interpretados erradamente, causando atrasos e insatisfação geral. Um sistema de controle de pedidos automatizado e eficiente é crucial para manter a ordem, melhorar o atendimento e assegurar a satisfação dos clientes à medida que o negócio cresce.

## Com isso, vamos aos requisitos do negócio!

**1-Gestão de Pedidos**:

- Clientes devem ter uma interface para selecionar e personalizar seus pedidos (CPF, nome e e-mail opcionais), montando combos de Lanche, Acompanhamento e Bebida, com exibição de nome, descrição e preço de cada produto.

**2 - Pagamento**:

- Integração com pagamento via QRCode do Mercado Pago para o MVP.

**3 - Acompanhamento de Pedido**:

- Após a confirmação e pagamento, o pedido deve ser enviado à cozinha, e o cliente deve poder acompanhar o progresso através de um monitor com status: Recebido, Em preparação, Pronto e Finalizado.

**4 - Entrega**:

- Notificação ao cliente quando o pedido estiver pronto para retirada e atualização do status para finalizado após a retirada.

**5 - Acesso Administrativo**:

- Gerenciamento de clientes para campanhas promocionais.

- Gerenciamento de produtos e categorias (Lanche, Acompanhamento, Bebida, Sobremesa) com definição de nome, categoria, preço, descrição e imagens.

- Acompanhamento de pedidos em andamento e tempo de espera.

- Painel administrativo para gerenciar todas as informações de pedidos.


O projeto utiliza tecnologias modernas e práticas de desenvolvimento ágil para garantir um alto nível de qualidade e eficiência.

## Tecnologias Utilizadas

- <img src="https://raw.githubusercontent.com/github/explore/main/topics/csharp/csharp.png" alt="C#" width="20"/> **C#**: Linguagem de programação utilizada para desenvolver o projeto.
- <img src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg" alt="SQL Server" width="20"/> **SQL Server**: Sistema de gerenciamento de banco de dados utilizado.
- <img src="https://static1.smartbear.co/swagger/media/assets/swagger_fav.png" alt="Swagger" width="20"/> **Swagger**: Plataforma utilizada para documentação da API.
- <img src="https://www.docker.com/wp-content/uploads/2022/03/Moby-logo.png" alt="Docker" width="20"/> **Docker**: Plataforma utilizada para contêinerizar a aplicação.
- <img src="https://cdn2.iconfinder.com/data/icons/mixd/512/20_kubernetes-512.png" alt="Kubernetes" width="20"/> Kubernetes: Plataforma de orquestração de containers para escalabilidade.

## Como Utilizar

### Pré-requisitos

Antes de rodar o projeto SnackTech, certifique-se de que você possui os seguintes pré-requisitos:

- **.NET SDK**: O projeto foi desenvolvido com o .NET SDK 8. Instale a versão necessária para garantir a compatibilidade com o código.
- **Docker**: O projeto utiliza Docker para contêinerizar a aplicação e o banco de dados. Instale o Docker Desktop para Windows ou Mac, ou configure o Docker Engine para Linux. O Docker Compose também é necessário para orquestrar os containers.
- **SQL Server (Opcional)**: O projeto configura e gerencia uma instância do SQL Server dentro de um container Docker. Sendo assim, a instalação do SQL Server é opcional.

### Preparando o ambiente

Siga os passos abaixo para instalar e configurar o projeto SnackTech:

**1 - Clone o repositório** 
- Clone o repositório do projeto para sua máquina local usando o Git: git clone https://github.com/seu-usuario/SnackTech.git

**2 - Configure o ambiente Docker** 
- Certifique-se de que o Docker Desktop (Windows ou Mac) ou o Docker Engine (Linux) esteja instalado e em execução.

**3 - Inicialize os containers Docker**
- Na raiz da pasta do projeto/repositório, abra um prompt de comando e execute o seguinte comando para construir e iniciar os containers: docker-compose up --build. Esse comando configura e inicia a aplicação e o banco de dados SQL Server dentro de containers Docker.

**4 - Verifique a execução**
- A aplicação estará disponível nas portas configuradas no docker-compose.yml, e o banco de dados SQL Server estará disponível para conexões conforme definido na string de conexão do arquivo de configuração da aplicação.

**4 - Utilize a aplicação**
- Para uma execução local a página do Swagger pode ser acessada no endereço "http://localhost:porta_configurada/swagger/index.html". Ou ainda, você usar o Postman e importar o arquivo [SnackTech.postman_collection.json](SnackTech.postman_collection.json), isso vai te dar acesso a exemplos de uso de todos os endpoints.

### Uso

Este é um projeto desenvolvido em .NET, utilizando arquitetura Hexagonal. A aplicação é um monolito que se comunica com um banco de dados SQL Server. O projeto está configurado para ser executado em contêineres Docker, facilitando a implantação e escalabilidade.

## Desenvolvimento

### Estrutura do Código

#### Core
No core temos os seguintes projetos:
- **SnackTech.Core**: Este projeto é o responsável por implementar o "coração" da aplicação. Foi construído seguindo o Clean Architecture, atuando como uma ponte entre a as rotas da API com as camadas de persistência (Banco de Dados e integração com processadora de pagamento). 

#### Infra.Persistence
Aqui temos os projetos com o código necessário para atuar com a camada de dados e outras persistências

- **SnackTech.Driver.DataBase**: Este projeto é responsável por todas as operações relacionadas ao banco de dados. Ele contém nosso DbContext, repositories, e outras classes que nos ajudam a criar e manipular o banco de dados.

- **SnackTech.Driver.MercadoPago**: O Mercado Pago está auxiliando como processador de pagamentos, por onde a aplicação envia o pedido, recebe uma forma válida para realizar o pagamento, que uma vez feito é notificado ao projeto para que faça o processamento interno do pedido

#### Infra.web-api
Aqui ficam os projetos relacionado a interface externa, ao que o projeto fornece de comunicação para executar seus procedimentos.

- **SnackTech.Driver.API**: Este projeto é a interface do nosso sistema. Ele contém nossos Controllers, que recebem requisições do usuário e retornam respostas. Além disso, ele contém arquivos de configuração como o appsettings.

#### Common
Aqui ficam os projetos que possuem estruturas de dados, interfaces e operações que possuem relação comum a qualquer um dos projetos envolvidos no desenho atual

- **SnackTech.Common**: Este projeto possui todas as estruturas de dados e interfaces que são de comum conhecimento entre os outros projetos da solução.

#### Tests
Em tests, temos projetos voltados para as outras camadas, sendo os projetos:

- **SnackTech.Driver.DataBase.Tests**: Este projeto contém testes para a camada de banco de dados. Ele nos ajuda a garantir que nossas operações de banco de dados estão funcionando corretamente.
- **SnackTech.Driver.API.Tests**: Este projeto contém testes para a camada de API. Ele nos ajuda a garantir que nossos endpoints estão retornando as respostas corretas.
- **SnackTech.Core.Tests**: Este projeto contém os testes para o código do Core. Ele nos ajuda a garantir que toda a implementação feita seguindo o Clean Architecture alem da nossa lógica de negócios estejam funcionando corretamente.


### Modificabilidade

O projeto foi estruturado para facilitar a modificação e a expansão:

- **Adicionar Novas Funcionalidades:** Novos casos de uso e funcionalidades podem ser adicionados na camada SnackTech.Application. Atualize também a camada SnackTech.Driver.API para expor novos endpoints, se necessário.
- **Modificar Funcionalidades Existentes:** Alterações na lógica de negócios devem ser feitas na camada SnackTech.Application e SnackTech.Domain. As alterações na interação com o banco de dados são feitas na camada SnackTech.Driver.DataBase.
- **Manutenção:** A arquitetura modular permite que as alterações em uma parte do sistema (como o banco de dados ou a API) sejam feitas com impacto mínimo nas outras partes do sistema.
