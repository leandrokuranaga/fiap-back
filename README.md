# 🕹️ FIAP Cloud Games – Projeto .NET 8

Repositório do projeto **FIAP Cloud Games (FCG)** desenvolvido para o **Tech Challenge da FIAP - Fase 1**.

> 📍 Acesse a [📘 Wiki do projeto](https://github.com/leandrokuranaga/fiap-back/wiki)

---

## 📦 Sobre o Projeto

O **FCG** é uma aplicação backend em **.NET 8**, com o objetivo de simular uma plataforma de venda e gerenciamento de jogos digitais. Esta é a **primeira entrega**, focada em:

- Autenticação de usuários  
- Registro de jogos  
- Promoções  
- Controle da biblioteca pessoal

---

## 📈 Status do Projeto

- 🚧 Em desenvolvimento

---

## 🔧 Tecnologias Utilizadas

- **.NET 8 / C#**
- **PostgreSQL**
- **EF Core + Migrations**
- **Swagger** (documentação)
- **Domain-Driven Design (DDD)**
- **Test-Driven Development (TDD)** com camadas de teste separadas
- **Repository Pattern + Unit of Work**
- **Camadas organizadas**:
  - API Layer
  - Application Layer
  - Domain Layer
  - Infra Layer
  - Tests Layer

### 🧱 Camadas da Arquitetura

```plaintext
─ Fiap.Api - Exposição da API (Controllers, Swagger)
─ Fiap.Application - Casos de uso, DTOs, validadores
─ Fiap.Domain - Entidades, agregados, interfaces de repositório
─ Fiap.Infra.Data - EF Core (DbContext, Seeds, Migrations)
─ Fiap.Infra.CrossCutting.IoC - Injeção de dependência
─ Fiap.Infra.CrossCutting.Http - Integrações externas (HttpClient)
─ Fiap.Tests - Testes organizados por camada
```

---

## 🛠️ Pré-requisitos

- Docker + Docker Compose  
- .NET SDK 8.0  
- PostgreSQL (caso não use o Docker)

---

## 📚 Links Úteis

- [📘 .NET 8](https://learn.microsoft.com/en-us/dotnet/)
- [📘 EF Core](https://learn.microsoft.com/en-us/ef/core/)
- [📘 Docker](https://docs.docker.com/)

---

## 🗃️ Estrutura de Pastas

```plaintext
├── Fiap.Api                     // Camada de exposição (Controllers, Swagger)
├── Fiap.Application             // Casos de uso, DTOs, validadores
├── Fiap.Domain                  // Entidades, agregados, repositórios
├── Fiap.Infra.Data              // Mapeamentos EF, contexto, seeds, migrations
├── Fiap.Infra.CrossCutting.IoC  // Injeção de dependência, integrações, helpers
├── Fiap.Infra.CrossCutting.Http // BaseHttpClient, integrações externas (HttpClient)
├── Fiap.Tests                   // Testes organizados por camada
```

---

## 🧠 Banco de Dados

### Tabelas principais

- `Users`
- `Games`
- `Promotions`
- `LibraryGames`

### 🔗 Relacionamentos

- `Users` → `LibraryGames`: 1:N (um usuário tem uma biblioteca) 
- `LibraryGames` → `Games`: N:1 (uma biblioteca contém vários jogos, com informações como data e preço de compra) 
- `Games` → `Promotions`: N:1 (um jogo pode estar em uma promoção) - (opcional)

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

### 📌 Semente Inicial (EF Core `HasData()`)

O projeto utiliza **`HasData()` com EF Core** para inserir dados iniciais automaticamente ao aplicar as migrations. Isso facilita testes e demonstrações, evitando a necessidade de popular o banco manualmente.

Inclui:

- Usuários padrão (Admin, User)
- Jogos (8 títulos)
- Promoções (3)
- Bibliotecas de jogos por usuário

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

---

## 🗺️ Diagrama Relacional

![Diagrama Relacional](https://github.com/leandrokuranaga/fiap-back/blob/3e65794e05bfc659739fcb9538e0f1f90ed79517/Diagrama%20Relacional%20FCG.png)

---

## 🚀 Deploy Final

A entrega será feita via **GitHub Releases**, publicada a partir da **branch `dev`**.

---

## 👥 Equipe

| Nome                      | E-mail                               |
|---------------------------|---------------------------------------|
| Vinicius Brito Chantres   | viniciuschantres@gmail.com           |
| Leandro da Silva Kuranaga | le.s.kuranaga@hotmail.com            |
| Bruno dos Santos Moura    | brunobsm88@gmail.com                 |
| Brayan Fernandes Julio    | brayan.fernandesjulio@gmail.com      |
| Rafael Nunes dos Santos   | devrafaelnunes@gmail.com             |

---

> Para mais detalhes sobre PRs e contribuições, acesse [📄 Guia de Contribuição](../../wiki/Guia-de-Contribuição)

---

# 🧪 Guia para rodar o projeto com Docker e SonarQube

Este projeto depende de **Docker** para executar seus serviços (API, banco de dados e SonarQube).

---

## 🐳 Subindo os serviços com Docker

```bash
docker compose up -d --build
```

Acesse a API: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

## 🎯 Subir serviços específicos:

```bash
docker compose up -d db sonarqube
docker compose up -d app
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
2. Login:  
  - **Usuário:** `admin`
   - **Senha:** `admin`
3. Altere a senha, gere um token em **My Account > Security**
4. No arquivo `.env-dev`, adicione:

```env
SONAR_TOKEN=seu_token_gerado
```

5. Renomeie o arquivo:

```bash
mv .env-dev .env
```

## ▶️ Análise com SonarQube

1. Execute `sonar-analyze.bat`
2. Veja os resultados em [http://localhost:9000](http://localhost:9000)
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

# 🖥️ Execução Manual (sem Docker)

### 1. Clonar o projeto

```bash
git clone https://github.com/leandrokuranaga/fiap-back.git
cd fiap-back
```

### 2. Restaurar pacotes

```bash
dotnet restore
```

### 3. Configurar o banco (`appsettings.json`)

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Meu_Banco_;User Id=seu_banco;Password=sua_senha;"
}
```

### 4. Aplicar migrations

```bash
dotnet ef database update
```

### 5. Rodar a aplicação

```bash
dotnet run --project ./src/Fiap.Api
```

---

## 📡 Exemplos de Requisições

### 🔐 Login

**POST** `/api/v1/auth/login`

```json
{
  "username": "john.doe@hotmail.com",
  "password": "Password123!"
}
```

---

### 🎮 Criar Jogo

**POST** `/api/v1/games`

```json
{
  "name": "Elden Ring",
  "genre": "Adventure",
  "price": 299.90,
  "promotionId": null
}
```

### 🎮 Listar Jogos

**GET** `/api/v1/games`

---

### 🎮 Obter Jogo por ID

**GET** `/api/v1/games/{id}`

---

### 🏷️ Criar Promoção

**POST** `/api/v1/promotions`

```json
{
  "discount": 25,
  "expirationDate": "2025-05-22T03:47:12.1123632Z",
  "gameId": [1, 2, 3]
}
```

### 🏷️ Atualizar Promoção

**PATCH** `/api/v1/promotions/{id}`

```json
{
  "discount": 10,
  "expirationDate": "2025-05-22T03:47:12.1213794Z",
  "gameId": [1, 2, 3]
}
```

### 🏷️ Obter Promoção por ID

**GET** `/api/v1/promotions/{id}`

---

### 👤 Criar Usuário

**POST** `/api/v1/users/create`

```json
{
  "name": "John Doe",
  "email": "john.doe@hotmail.com",
  "password": "Password123!"
}
```

### 👤 Criar Admin

**POST** `/api/v1/users/create-admin`

```json
{
  "name": "John Doe",
  "email": "john.doe@hotmail.com",
  "password": "Password123!",
  "typeUser": 1,
  "active": true
}
```

### 👤 Atualizar Usuário

**PATCH** `/api/v1/users/{id}`

```json
{
  "name": "Maria Carie",
  "email": "maria.carie@hotmail.com",
  "password": "Password456!",
  "type": 1,
  "active": false
}
```

### 👤 Deletar Usuário

**DELETE** `/api/v1/users/{id}`

---

### 👤 Obter Usuário por ID

**GET** `/api/v1/users/{id}`

---

### 👥 Listar Todos Usuários

**GET** `/api/v1/users`

---

### 🎮📚 Biblioteca de Jogos do Usuário Logado

**GET** `/api/v1/users/users-games`
