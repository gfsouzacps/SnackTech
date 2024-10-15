# Arquitetura Da Aplicação

A API como um todo, para ter um código de fácil manutenção e evolução das features precisa seguir algum modelo de organização, ou será difícil atingir esses objetivos.

# Clean Architecture

Seguindo a proposta do Tech Challenge, estamos implementando o Clean Architecture na aplicação, fazendo primeiro uma separação entre a camada de API, a camada de Persistência e seu Core.

Dentro da camada de API possuímos todo o código necessário para subir a aplicação como uma API, a definição de suas rotas, a configuração do Swagger e as injeções necessárias para fazer a Inversão de Dependência, proposta no SOLID.

A camada de Persistência possui o código necessário para lidar com o banco de dados ou outros serviços de integração, como o serviço de pagamento.

O Core segue uma organização atendendo a separação proposta no Clean Architecture para garantir a separação de responsabilidade, execução das operações e aplicação das regras de negócio.

## Controllers

A pasta de Controllers possui classes cujo objetivo é preparar e configurar o necessário para a execução das operações, de acordo com o tipo de operação a ser feita

## Gateways

Como o nome sugere, as classes dentro da pasta Gateways lidam como portões entre o core da aplicação e fontes externas de dados.

## Presenters

Ficam como responsáveis por mapear os resultados das operações, sejam resultados de buscas ou alterações, além de cenários entre sucesso, falha de lógica ou falha interna, para retornos comuns ao mundo externo do Core, ou seja, se isso chegar a camada de API, Banco de Dados, etc, a estrutura de dados é conhecida por essas camadas, sem necessariamente saberem como elas são dentro do Core.

## UseCases

Implementam todas as regras de negócio que foram definidas, fazendo uso dos gateways para acessar as fontes externas de dados, executar atualizações e gerar o resultado de acordo com o resultado dessas operações

## Domain

Possui todos os tipos de dados, classes e estruturas que representam as entidades do Core, como o negócio interpreta os dados que chegam até o core para que alguma operação seja feita.