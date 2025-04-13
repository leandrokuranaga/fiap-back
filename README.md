# 🕹️ FIAP Cloud Games – Projeto .NET 8

Repositório do projeto **FIAP Cloud Games (FCG)** desenvolvido para o **Tech Challenge da FIAP - Fase 1**.

> 📍 Acesse a Wiki do projeto: [https://github.com/leandrokuranaga/fiap-back/wiki](https://github.com/leandrokuranaga/fiap-back/wiki)

---

## 📦 Sobre o Projeto

O FCG é uma aplicação backend desenvolvida em **.NET 8**, com o objetivo de simular uma plataforma de venda e gerenciamento de jogos digitais. Essa versão representa a primeira entrega do projeto, com foco em autenticação de usuários, registro de jogos, promoções e controle de bibliotecas pessoais.

---

## 🔧 Tecnologias e Padrões Utilizados

- **.NET 8 / C#**
- **PostgreSQL**
- **EF Core + Migrations**
- **Swagger** para documentação
- **DDD (Domain-Driven Design)**
- **TDD (Test-Driven Development)** com camadas de teste separadas
- **Repository Pattern** e **Unit of Work**
- **Camadas organizadas**:
  - API Layer
  - Application Layer
  - Domain Layer
  - Infra Layer
  - Tests Layer

---

## 🗂️ Estrutura de Pastas

```
├── Fiap.Api                 // Camada de exposição (controllers, Swagger)
├── Fiap.Application         // Casos de uso, DTOs, validadores
├── Fiap.Domain              // Entidades, agregados, repositórios
├── Fiap.Infra.Data          // Mapeamentos EF, contexto, seeds, migrations
├── Fiap.Infra.CrossCutting  // IoC, integrações, helpers
├── Fiap.Tests               // Testes organizados por camada
```

---

## 🧠 Relacionamento das Tabelas

Além das tabelas principais utilizadas na lógica de negócio, o sistema também possui a tabela `__EFMigrationsHistory`, gerenciada automaticamente pelo Entity Framework Core.

### 📄 Tabela de Migrations

| Tabela                 | Descrição                                               |
|------------------------|-----------------------------------------------------------|
| `__EFMigrationsHistory` | Controla o histórico de migrations aplicadas no banco de dados |

#### Exemplo:
| MigrationId              | ProductVersion |
|--------------------------|----------------|
| 20250406185714_initial   | 9.0.3          |

O projeto possui as seguintes tabelas principais:

- `Users`
- `Games`
- `Promotion`
- `Library`
- `LibraryGames`

### 🔗 Relacionamentos:
- `Users` ↔️ `Library` → 1:1 (um usuário tem uma biblioteca)
- `Library` ↔️ `LibraryGames` ↔️ `Games` → N:N (uma biblioteca contém vários jogos, com informações como data e preço de compra)
- `Games` ↔️ `Promotion` → N:1 (um jogo pode estar em uma promoção)

### 📌 Semente Inicial:

O projeto utiliza **`HasData()` com EF Core** para inserir dados iniciais automaticamente ao aplicar as migrations. Isso facilita testes e demonstrações, evitando a necessidade de popular o banco manualmente.

#### Dados incluídos:
- **Usuários:**
  - `Admin` (admin@gmail.com)
  - `User` (user@gmail.com)
- **Bibliotecas:**
  - Uma biblioteca associada a cada usuário
- **Jogos:**
  - 8 títulos inseridos com nome, gênero e preço
- **Promoções:**
  - 3 promoções com datas de início e fim
- **LibraryGames:**
  - Jogos comprados por usuários com preço pago e data de compra

---

## 🗺️ Diagrama Relacional

![image](https://github.com/user-attachments/assets/5b06b48a-6b68-4ae9-a1e5-d1a5b24be042)

---

## 🚀 Deploy Final

A entrega final do projeto será feita via **Release no GitHub**, publicada a partir da **branch `dev`**.

---

## 👥 Grupo

| Nome                     | E-mail                                |
|--------------------------|----------------------------------------|
| Vinicius Brito Chantres  | viniciuschantres@gmail.com            |
| Leandro da Silva Kuranaga | le.s.kuranaga@hotmail.com           |
| Bruno dos Santos Moura   | brunobsm88@gmail.com                 |
| Brayan Fernandes Julio   | brayan.fernandesjulio@gmail.com      |
| Rafael Nunes dos Santos  | devrafaelnunes@gmail.com             |

---

> Para mais detalhes sobre PRs e contribuições, acesse o arquivo [📄 Guia de Contribuição](./Contributing%20Guide.md).

# 🧪 Guia para rodar o projeto com Docker e SonarQube

Este projeto depende de **Docker** para executar seus serviços (API, banco de dados e SonarQube).

---

## 🐳 Subindo os serviços com Docker

### 🔁 Subir todos os serviços:

```bash
docker compose up -d --build
```

http://localhost:8080/swagger/index.html endereço da api

### 🎯 Subir serviços específicos:

```bash
docker compose up -d db sonarqube
```

Você também pode iniciar um serviço por vez:

```bash
docker compose up -d db          # Banco de dados
docker compose up -d app         # API
docker compose up -d sonarqube   # SonarQube
```

---

## 🔐 Configuração do SonarQube

1. Acesse: [http://localhost:9000](http://localhost:9000)
2. Faça login com:
   - **Usuário:** `admin`
   - **Senha:** `admin`
3. Altere a senha quando solicitado.
4. Vá em **My Account > Security**
5. Gere um token de autenticação
6. No arquivo `.env-dev`, adicione:

```env
SONAR_TOKEN=seu_token_gerado
```

7. Após isso, renomeie o arquivo:

```bash
mv .env-dev .env
```

---

## ▶️ Rodando a análise com SonarQube

Para executar a análise de código:

1. Dê dois cliques no arquivo `sonar-analyze.bat`
2. Acesse o painel do Sonar em [http://localhost:9000](http://localhost:9000)
3. Vá até a seção "Projetos" para visualizar os resultados da análise

---

## 🛢️ Configuração do banco de dados

No arquivo `.env` (anteriormente `.env-dev`), configure as variáveis:

```env
POSTGRES_USER=postgres
POSTGRES_PASSWORD=sua_senha
POSTGRES_DB=FIAP
```

Essas variáveis são usadas pelo Docker para inicializar o banco e pela aplicação para se conectar a ele.

---


