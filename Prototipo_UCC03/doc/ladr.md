# Template

[Seguindo o padrão](https://github.com/peter-evans/lightweight-architecture-decision-records)

- Title
- Context
- Decision
- Status [Proposed, Accepted, Deprecated, Superseded]
- Consequences

---

# Arquitetura de Processo

- **Context**: Qual arquitetura utilizar em termos de processos de Sistema Operacional (Monolito ou Microserviço).
- **Decision**: Adotamos arquitetura de microsserviços.
- **Status**: Accepted.
- **Consequences**:
  - Exige integrações entre microsserviços.
  - Necessidade maior de gerenciamento, incluindo eventuais deploys, monitoramento e orquestração.
  - Maior facilidade de escalabilidade granular.

---

# Tecnologia Base Back-End

- **Context**: Qual linguagem/plataforma base utilizar para servir requisições.
- **Decision**: C#/ASP.NET Core.
- **Status**: Accepted.
- **Consequences**:
  - Maior facilidade de integração com Azure e Microsoft.
  - Suporte da Microsoft por vários anos.
  - Familiaridade do time com programação orientada a objetos (OO).

---

# Abordagem Transacional para Confiabilidade

- **Context**: Devido à natureza do sistema (compra de produtos), é necessária confiabilidade transacional entre operações, especialmente na parte de pedidos.
- **Decision**: Adotamos o padrão transacional distribuído SAGA, implementado com C# e MassTransit.
- **Status**: Accepted.
- **Consequences**:
  - Necessário entender o funcionamento do MassTransit, já que não temos tanto conhecimento atualmente.
  - A gestão de transacionalidade fica com o framework, o que pode dificultar a depuração, mas facilita a gestão geral.

---

# Arquitetura de Mensageria para Holder de Pedidos

- **Context**: Utilizando o padrão SAGA para transações, é necessário suporte tecnológico para as mensagens que carregarão as requisições de pedidos.
- **Decision**: Utilizaremos mensageria devido à vantagem de permitir que mensagens sejam consumidas conforme necessário, permitindo a escalabilidade dos microsserviços sem gerenciamento direto de tracking pelos desenvolvedores.
- **Status**: Accepted.
- **Consequences**:
  - Devemos decidir se usaremos algum framework para gerenciar o broker ou se faremos tudo manualmente.

---

# Gerenciar o Broker de Mensageria Diretamente ou Usar um Framework?

- **Context**: É necessário decidir se iremos gerenciar as filas de mensageria (criar, deletar, etc.) manualmente ou se delegaremos esse gerenciamento a um framework.
- **Decision**: Utilizaremos um framework. Escolhemos o MassTransit devido ao suporte ao padrão SAGA.
- **Status**: Accepted.
- **Consequences**:
  - Menos gerenciamento direto das filas.
  - Maior facilidade no uso de mensagens, com suporte à criação de tipos diretamente pelo framework, bastando seguir o padrão nos serviços que utilizam as mensagens.

---

# Arquitetura para Organização de Módulos nos Serviços

- **Context**: Para padronizar o projeto e facilitar a comunicação entre desenvolvedores, é necessária a escolha de uma arquitetura para organização dos módulos e o relacionamento entre componentes.
- **Decision**: Adotamos o Clean Architecture.
- **Status**: Accepted.
- **Consequences**:
  - Maior complexidade inicial devido à configuração das camadas.
  - Menor acoplamento entre componentes de diferentes camadas, facilitando mudanças futuras.
