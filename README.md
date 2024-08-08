# SnackTech ![ ](LogoSnackTech.png)

## Descrição

O SnackTech é um projeto voltado para a estrutura backend de uma lanchonete. Ele foi projetado para gerenciar os principais aspectos de um negócio de lanchonete, incluindo a gestão de clientes, pedidos e produtos.

A API do projeto é dividida em várias camadas, cada uma focada em uma área específica:

- **Clientes**: Gerencia os clientes da lanchonete, permitindo o cadastro de novos clientes e a consulta de informações dos clientes existentes.
- **Pedidos**: Lida com todos os aspectos relacionados aos pedidos dos clientes, incluindo a criação de novos pedidos, a atualização de pedidos existentes e a consulta de detalhes dos pedidos.
- **Produtos**: Dedica-se ao gerenciamento dos produtos oferecidos pela lanchonete, permitindo o cadastro de novos produtos, a atualização e exclusão de produtos existentes e a consulta de informações dos produtos.

O projeto utiliza tecnologias modernas e práticas de desenvolvimento ágil para garantir um alto nível de qualidade e eficiência.

## Tecnologias Utilizadas

- <img src="https://raw.githubusercontent.com/github/explore/main/topics/csharp/csharp.png" alt="C#" width="20"/> **C#**: Linguagem de programação utilizada para desenvolver o projeto.
- <img src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg" alt="SQL Server" width="20"/> **SQL Server**: Sistema de gerenciamento de banco de dados utilizado.
- <img src="https://www.docker.com/wp-content/uploads/2022/03/Moby-logo.png" alt="Docker" width="20"/> **Docker**: Plataforma utilizada para contêinerizar a aplicação.
- <img src="https://static1.smartbear.co/swagger/media/assets/swagger_fav.png" alt="Swagger" width="20"/> **Swagger**: Plataforma utilizada para documentação da API.

## Como Utilizar

### Pré-requisitos

Antes de rodar o projeto SnackTech, certifique-se de que você possui os seguintes pré-requisitos:

- **.NET SDK**: O projeto foi desenvolvido com o .NET SDK. Instale a versão necessária para garantir a compatibilidade com o código.
- **Docker**: O projeto utiliza Docker para contêinerizar a aplicação e o banco de dados. Instale o Docker Desktop para Windows ou Mac, ou configure o Docker Engine para Linux. O Docker Compose também é necessário para orquestrar os containers.
- **SQL Server (Opcional)**: O projeto configura e gerencia uma instância do SQL Server dentro de um container Docker. Sendo assim, a instalação do SQL Server é opcional.

### Preparando o ambiente

Siga os passos abaixo para instalar e configurar o projeto SnackTech:

**1 - Clone o repositório** 
- Clone o repositório do projeto para sua máquina local usando o Git: git clone https://github.com/seu-usuario/SnackTech.git

**2 - Configure o ambiente Docker** 
- Certifique-se de que o Docker Desktop (Windows ou Mac) ou o Docker Engine (Linux) esteja instalado e em execução.

**3 - Inicialize os containers Docker**
- Na raiz da pasta do projeto (dentro da pasta "src" onde está o arquivo docker-compose.yml), abra um prompt de comando e execute o seguinte comando para construir e iniciar os containers: docker-compose up --build. Esse comando configura e inicia a aplicação e o banco de dados SQL Server dentro de containers Docker.

**4 - Verifique a execução**
- A aplicação estará disponível nas portas configuradas no docker-compose.yml, e o banco de dados SQL Server estará disponível para conexões conforme definido na string de conexão do arquivo de configuração da aplicação.

### Uso

Este é um projeto desenvolvido em .NET, utilizando arquitetura Hexagonal. A aplicação é um monolito que se comunica com um banco de dados SQL Server. O projeto está configurado para ser executado em contêineres Docker, facilitando a implantação e escalabilidade.

## Desenvolvimento

### Estrutura do Código

#### Core
No core temos os seguintes projetos:
- **SnackTech.Application**: Este projeto é responsável por implementar as UseCases da aplicação. Ele atua como uma ponte entre a interface do usuário e a lógica de negócios.
- **SnackTech.Domain**: Este é o coração do nosso sistema, onde definimos nossas entidades de domínio, regras de negócios e lógica de domínio. Aqui guardamos nossas Dtos, Models, entre outras estruturas fundamentais.

#### Driven
No Driven temos a camada de banco de dados:
- **SnackTech.Adapter.Database**: Este projeto é responsável por todas as operações relacionadas ao banco de dados. Ele contém nosso DbContext, repositories, e outras classes que nos ajudam a criar e manipular o banco de dados.

#### Driving
No Driving, temos a camada de API:
- **SnackTech.API**: Este projeto é a interface do nosso sistema. Ele contém nossos Controllers, que recebem requisições do usuário e retornam respostas. Além disso, ele contém arquivos de configuração como o appsettings, Dockerfile e docker-compose.

#### Tests
Em tests, temos projetos voltados para as outras camadas, sendo os projetos:

- **SnackTech.Adapter.Database.Tests**: Este projeto contém testes para a camada de banco de dados. Ele nos ajuda a garantir que nossas operações de banco de dados estão funcionando corretamente.
- **SnackTech.API.Tests**: Este projeto contém testes para a camada de API. Ele nos ajuda a garantir que nossos endpoints estão retornando as respostas corretas.
- **SnackTech.Application.Tests**: Este projeto contém testes para as UseCases. Ele nos ajuda a garantir que nossa lógica de negócios está funcionando corretamente.
- **SnackTech.Domain.Tests**: Este projeto contém testes para a camada de domínio. Ele nos ajuda a garantir que nossas regras de negócios e lógica de domínio estão corretas.

### Modificabilidade

O projeto foi estruturado para facilitar a modificação e a expansão:

- **Adicionar Novas Funcionalidades:** Novos casos de uso e funcionalidades podem ser adicionados na camada SnackTech.Application. Atualize também a camada SnackTech.API para expor novos endpoints, se necessário.
- **Modificar Funcionalidades Existentes:** Alterações na lógica de negócios devem ser feitas na camada SnackTech.Application e SnackTech.Domain. As alterações na interação com o banco de dados são feitas na camada SnackTech.Adapter.Database.
- **Manutenção:** A arquitetura modular permite que as alterações em uma parte do sistema (como o banco de dados ou a API) sejam feitas com impacto mínimo nas outras partes do sistema.
