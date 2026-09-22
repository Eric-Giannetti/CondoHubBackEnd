# CondoHub

## Arquitetura, Design Patterns e SOLID

O projeto segue Clean Architecture, separado em `CondoHub.Domain` (entidades/contratos), `CondoHub.DataBase` (persistência), `CondoHub.Services` (regras de negócio), `CondoHub.Security` (helpers de segurança) e `CondoHub.API` (apresentação/HTTP).

### Design Patterns aplicados
- **Repository Pattern** — acesso a dados (MongoDB/MySQL) isolado atrás de interfaces em `CondoHub.Domain/Interfaces/Repositorys` (ex.: `ICondominiumMongoRepository`, `ILogRepository`).
- **Factory Method** — `Result<T>.Success()/Failure()`, `ResultList<T>.Success()/Failure()` e `DbConnectionFactory` encapsulam a criação de resultados e conexões.
- **Dependency Injection** — cadastro de dependências centralizado em `ConnectionsConfiguration` e `UseCasesConfiguration`.
- **Middleware Pipeline** — `UserContextMiddleware` intercepta a requisição e popula `IUserContextService`.
- **Decorator/Filter (Action Filter)** — `IsAdminAttribute`, `IsUserAttribute`, `IsCondominiumManagerAttribute` para checagem de autorização.
- **Context Object** — `IUserContextService`/`UserContextService` guarda dados do usuário autenticado durante a requisição.
- **Result/Either Pattern** — `Result`, `Result<T>`, `ResultList<T>` substituem exceptions para fluxo de erro esperado, com `ResultLogger` plugado por DI para logging transversal.
- **API Versioning** — pastas `Controllers/V1`, `V2`, `V3` permitem múltiplas versões de endpoints coexistirem.

### Princípios SOLID
- **SRP** — camadas e classes com responsabilidade única (cada repositório cuida de uma fonte de dados, cada atributo cuida de uma regra de autorização).
- **OCP** — novas versões de API e novos tipos de erro podem ser adicionados sem alterar código existente.
- **LSP** — implementações de interfaces (`ICondominiumMongoRepository`, `ILogRepository`, `ILogService`) são substituíveis sem quebrar consumidores.
- **ISP** — interfaces pequenas e focadas por responsabilidade, evitando interfaces "gordas".
- **DIP** — camadas de negócio dependem de abstrações definidas no `Domain`, nunca de implementações concretas; a composição concreta acontece só na `API`.

## Visão geral

O CondoHub é um sistema de ERP voltado para a administração de condomínios, desenvolvido para centralizar e automatizar os processos administrativos, financeiros e operacionais de empreendimentos residenciais e comerciais.

A solução foi pensada para auxiliar síndicos, administradoras e funcionários na gestão de condomínios, permitindo o controle de moradores, unidades, ocorrências, pagamentos, reservas, documentos e outras rotinas do dia a dia.

## Objetivo do projeto

O objetivo principal do CondoHub é oferecer uma plataforma robusta e organizada para que a gestão condominial seja feita de forma mais eficiente, segura e transparente. O sistema busca reduzir a dependência de processos manuais, melhorar a comunicação entre colaboradores e moradores e facilitar a tomada de decisão por meio de dados e informações centralizadas.

## Problema resolvido

Administrar um condomínio envolve diversas atividades simultâneas, como:

- controle de moradores e unidades;
- gestão de ocorrências e solicitações;
- acompanhamento de pagamentos e cobranças;
- organização de reservas e áreas comuns;
- manutenção e chamados de serviços;
- comunicação e documentação interna.

Sem um sistema integrado, essas atividades tendem a ficar dispersas em planilhas, mensagens e processos manuais, gerando retrabalho, erros e falta de visibilidade.

## Funcionalidades esperadas

O ERP do CondoHub pode evoluir para cobrir os principais módulos de uma administradora de condomínio, incluindo:

### Gestão de moradores e unidades
- cadastro de moradores;
- vínculo com unidades e vagas;
- controle de dependentes e moradores autorizados;
- histórico de mudanças na unidade.

### Administração financeira
- controle de boletos e pagamentos;
- geração de vouchers e extratos;
- registro de despesas do condomínio;
- acompanhamento da situação financeira por unidade.

### Ocorrências e chamados
- abertura de chamados por moradores;
- acompanhamento de status e pendências;
- histórico de atendimento.

### Reservas e áreas comuns
- agendamento de salão de festas, churrasqueira e outros espaços;
- controle de disponibilidade;
- gestão de regras e notificações.

### Documentação e compliance
- arquivos e documentos administrativos;
- armazenamento de contratos e atas;
- organização de registros do condomínio.

### Comunicação e portal do morador
- envio de avisos e comunicados;
- notificações internas;
- acesso a informações relevantes por perfil de usuário.

## Arquitetura do sistema

O projeto foi estruturado em camadas, seguindo uma abordagem moderna para APIs e aplicações empresariais.

### Camadas principais

- CondoHub.API: responsável pela exposição dos endpoints da aplicação e integração com o cliente.
- CondoHub.Domain: contém as entidades, regras de negócio e contratos principais do domínio.
- CondoHub.DataBase: camada de acesso a dados e persistência.
- CondoHub.Services: serviços e regras de aplicação.
- CondoHub.Security: funcionalidades relacionadas à autenticação, autorização e segurança.
- CondoHub.Tests: testes automatizados para garantir a qualidade e estabilidade do sistema.

## Stack tecnológica

O backend do projeto está baseado em tecnologias .NET, com foco em APIs REST. A estrutura atual sugere uso de:

- ASP.NET Core
- C#
- Entity Framework / acesso a banco relacionais ou NoSQL conforme a implementação
- autenticação e autorização via JWT
- Swagger/OpenAPI para documentação de endpoints
- arquitetura em camadas e modularização por responsabilidade

## Fluxo de uso

O sistema é pensado para servir diferentes perfis de usuários, como:

- síndico;
- administradora;
- porteiro ou atendimento;
- morador;
- financeiro;
- manutenção.

Cada perfil pode ter acesso a funcionalidades específicas de acordo com permissões e regras de negócio.

## Benefícios esperados

Ao centralizar a administração condominial em uma plataforma ERP, o condomínio passa a contar com:

- maior eficiência operacional;
- redução de erros manuais;
- melhor rastreabilidade de processos;
- maior transparência financeira;
- melhor comunicação com moradores;
- melhor organização de documentos e pendências;
- suporte à tomada de decisão por dados.

## Estado atual

Este repositório representa a base backend do sistema CondoHub, com a estrutura inicial para uma solução de ERP de administração de condomínios. O projeto está em evolução e pode receber novos módulos e integrações conforme as demandas de negócio.

## Conclusão

O CondoHub é um projeto de ERP para gestão de condomínios, com foco em digitalizar e automatizar a administração do patrimônio e das relações entre condomínio, moradores e prestadores de serviços. A proposta é criar uma solução completa, escalável e organizada para atender as necessidades reais de administradoras e síndicos.

---

Este README foi criado para apresentar a visão geral do projeto e sua finalidade dentro do contexto de gestão condominial.
