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
- **Kubernetes**: A aplicação é implantada em um cluster Kubernetes. Certifique-se de ter o Kubernetes instalado e configurado. Se estiver utilizando o Docker Desktop, verifique se a opção de Kubernetes está habilitada.
- **Minikube**: Para rodar o Kubernetes localmente, é necessário instalar o Minikube, que cria um ambiente de desenvolvimento Kubernetes em sua máquina.

# 🚀 **Passo a Passo para Configuração e Deploy da Aplicação**


## 1. 📥 **Clonar o repositório**
Clone o repositório do projeto para sua máquina local e posteriormente abra um terminal na raiz do projeto em `SnackTech/src/`. : 
```bash
git clone https://github.com/seu-usuario/SnackTech.git
```

## 2. 🟢 **Iniciar o Minikube**
Com o terminal aberto na raiz do projeto, inicie um cluster Minikube para criar um ambiente Kubernetes local:

```bash
minikube start
```

## 3. 🐳 **Configurar o Ambiente Docker com Minikube**
Configure o Docker para utilizar o ambiente Minikube:

```bash
eval $(minikube docker-env)
```

## 4. 🔨 **Construir a Imagem Docker**
Construa a imagem Docker da aplicação utilizando o Dockerfile especificado:

```bash
docker buildx build -t snack-tech-api:dev -f infra.web-api/SnackTech.Driver.API/Dockerfile .
```

## 5. 📂 **Navegar para o Diretório e Iniciar o Minikube**
Navegue até o diretório `infra/local` e inicie o Minikube se ele ainda não estiver em execução:

```bash
cd infra/local && minikube start
```

## 6. 💾 **Criar o Pod do Banco de Dados**
Crie o pod do banco de dados aplicando os arquivos de configuração no diretório `database`:

```bash
kubectl apply -f database --validate=false
```

## 7. 👀 **Verificar os Pods**
Verifique se os pods foram criados com sucesso:

```bash
kubectl get pod
```

## 8. 📦 **Criar os Pods da Aplicação**
Crie os pods da aplicação aplicando os arquivos de configuração no diretório `api`:

```bash
kubectl apply -f api
```

## 9. 📝 **Listar os Serviços**
Mostre todos os serviços rodando no cluster Kubernetes:

```bash
kubectl get services
```

## 10. 🌐 **Expor o Serviço Externamente**
Exponha o serviço `snack-tech-api-service` externamente:

```bash
minikube service snack-tech-api-service
```

**Feito todo o passo a passo, o navegador já deve apresentar toda a API. Você também pode usar o Postman e importar o arquivo [SnackTech.postman_collection.json](SnackTech.postman_collection.json), isso vai te dar acesso a exemplos de uso de todos os endpoints.**

### Uso

Este é um projeto desenvolvido em .NET, utilizando arquitetura Hexagonal. A aplicação é um monolito que se comunica com um banco de dados SQL Server. O projeto está configurado para ser executado em contêineres Docker, facilitando a implantação e escalabilidade.

## Para mais detalhes 

#### [Arquitetura da aplicação](docs/arquitetura/arquitetura-aplicacao.md)
#### [Arquitetura da infraestrutura](docs/arquitetura/arquitetura-infra.md)
#### [Arquitetura de Código](docs/arquitetura/arquitetura-codigo.md)

## Equipe

* Adriano de Melo Costa
Email: adriano.dmcosta@gmail.com
Discord: adrianocosta0591
* Rafael Duarte Gervásio da Silva
Email: rafael.dgs.1993@gmail.com
Discord: rafadu4984
* Guilherme Felipe de Souza
Email: gui240799@outlook.com
Discord: gui3504
* Dayvid Ribeiro Correia
Email: dayvidrc@gmail.com
Discord: dayvid4015


