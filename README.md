# SnackTech

## Descrição

O SnackTech é um projeto voltado para a estrutura backend de uma lanchonete. Ele foi projetado para gerenciar os principais aspectos de um negócio de lanchonete, incluindo a gestão de clientes, pedidos e produtos.

A API do projeto é dividida em várias camadas, cada uma focada em uma área específica:

- **Clientes**: Gerencia os clientes da lanchonete, permitindo o cadastro de novos clientes e a consulta de informações dos clientes existentes.
- **Pedidos**: Lida com todos os aspectos relacionados aos pedidos dos clientes, incluindo a criação de novos pedidos, a atualização de pedidos existentes e a consulta de detalhes dos pedidos.
- **Produtos**: Dedica-se ao gerenciamento dos produtos oferecidos pela lanchonete, permitindo o cadastro de novos produtos, a atualização e exclusão de produtos existentes e a consulta de informações dos produtos.

O projeto utiliza tecnologias modernas e práticas de desenvolvimento ágil para garantir um alto nível de qualidade e eficiência.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação utilizada para desenvolver o projeto.
- **SQL Server**: Sistema de gerenciamento de banco de dados utilizado.
- **Docker**: Plataforma utilizada para contêinerizar a aplicação.
- **Swagger**: Plataforma utilizada para documentação da API.

## Como Utilizar

### Pré-requisitos

Antes de rodar o projeto SnackTech, certifique-se de que você possui os seguintes pré-requisitos:

- **.NET SDK**: O projeto foi desenvolvido com o .NET SDK. Instale a versão necessária para garantir a compatibilidade com o código.
- **SQL Server**: O projeto utiliza o SQL Server como sistema de gerenciamento de banco de dados. Certifique-se de ter o SQL Server instalado e configurado, ou utilize uma instância SQL Server em contêiner.
- **Docker**: O projeto é contêinerizado utilizando Docker. Instale o Docker Desktop para Windows ou Mac, ou configure o Docker Engine para Linux.
- **Arquivo de Configuração do Banco de Dados**: Verifique o arquivo appsettings.json (ou outro arquivo de configuração utilizado) para garantir que a string de conexão com o banco de dados SQL Server está corretamente configurada.

### Instalação

Siga os passos abaixo para instalar e configurar o projeto SnackTech:

**1 - Clone o repositório** 
- Clone o repositório do projeto para sua máquina local usando o Git: git clone https://github.com/seu-usuario/SnackTech.git

**2 - Configure o Banco de Dados** 
- Configure o banco de dados SQL Server. Verifique o arquivo appsettings.json ou o arquivo de configuração correspondente e ajuste a string de conexão com as informações da sua instância SQL Server.

**3 - Configuração do Docker** 
- Certifique-se de ter o Docker Desktop instalado e em execução. Através do prompt de comando, navegue até o projeto SnackTech.API e execute o seguinte comando: docker-compose up --build

- **4 - Executar Migrations** 
- Aplique as migrations ao banco de dados. Utilize o seguinte comando para executar as migrations: dotnet ef database update

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

### Tests
Em tests, temos projetos voltados para as outras camadas, sendo os projetos:

- **SnackTech.Adapter.Database.Tests**: Este projeto contém testes para a camada de banco de dados. Ele nos ajuda a garantir que nossas operações de banco de dados estão funcionando corretamente.
- **SnackTech.API.Tests**: Este projeto contém testes para a camada de API. Ele nos ajuda a garantir que nossos endpoints estão retornando as respostas corretas.
- **SnackTech.Application.Tests**: Este projeto contém testes para as UseCases. Ele nos ajuda a garantir que nossa lógica de negócios está funcionando corretamente.
- **SnackTech.Domain.Tests**: Este projeto contém testes para a camada de domínio. Ele nos ajuda a garantir que nossas regras de negócios e lógica de domínio estão corretas.
