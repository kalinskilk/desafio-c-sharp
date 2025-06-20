# 📌 Api Desafio - C#

Este repositório contém a implementação da API backend, desenvolvida em C#, que atende aos requisitos propostos no teste. A API segue os princípios da arquitetura DDD (Domain-Driven Design), utiliza JWT para autenticação e inclui testes unitários. Entre as funcionalidades, destaca-se o gerenciamento de FeatureToggles, Ambientes e suas configurações específicas por ambiente.

---

## 🛠️ Tecnologias Utilizadas

- **.NET 9**
- **Angular**
- **PrimeNG**
- **Bootstrap**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLLite**
- **JWT Authentication**
- **Swagger (Swashbuckle)**
- **xUnit** para testes unitários

---

## ▶️ Como Executar o Projeto

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/kalinskilk/desafio-c-sharp.git
   cd desafio-c-sharp
   ```

2. **Instale as dependências do backend**:
   Certifique-se de ter o SDK do .NET 9 instalado. Depois, execute:

   ```bash
    cd ApiDesafio
    dotnet restore
   ```

3. **Ferramenta CLI do Entity Framework Core instalada**

   Para verificar se você já possui a ferramenta instalada, execute:

   ```bash
   dotnet ef
   ```

   Se aparecer uma lista de comandos como migrations add, database update, etc., está tudo certo ✅.

   Se **não** aparecer, instale a ferramenta com o comando:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Configure o banco de dados** :

   ```bash
   dotnet ef database update
   ```

5. **Execute a aplicação**:

   ```bash
   dotnet run
   ```

6. Acesse o Swagger:

   ```
   http://localhost:5275/index.html
   ```

7. **Instale as dependências do Front end**:
   Certifique-se de ter o node.js instalado. Em um novo terminal execute:

   ```bash
   cd FrontDesafio && npm install
   ```

   Com yarn execute:

   ```bash
   cd FrontDesafio && yarn install
   ```

8. **Execute a aplicação**:
   Execute:

   ```bash
   npm start
   ```

   Com yarn execute:

   ```bash
    yarn start
   ```

9. **Acesse a aplicação**:

   ```bash
   http://localhost:4200/
   ```

---

## 🔐 Autenticação JWT

- Não é necessário usuário e senha para login.
- Para obter um token de teste, basta chamar o endpoint:

```

POST /api/auth/token

```

Esse endpoint gera um token JWT com **claims fixas**, que pode ser utilizado para autenticar endpoints protegidos (POST e PUT).

- Para usar o token no Swagger:

1. Clique em **Authorize** no topo direito da interface do Swagger.
2. Insira o token no formato:
   ```
   Bearer {seu_token_aqui}
   ```

---

## 🧠 DDD - Domain Driven Design

A estrutura do projeto segue o padrão DDD, com separação clara de responsabilidades entre camadas:

### 📂 Pastas e Responsabilidades

- `Domain/`

- Contém **Entidades** e **Interfaces** que representam o modelo de negócio.
- Exemplo: `FeatureToggle.cs`, `IAmbienteRepository.cs`.

- `Application/`

- Contém os **DTOs**, **Services** e **Interfaces de Aplicação**.
- Exemplo: `FeatureToggleService.cs`, `AmbienteDto.cs`.

- `Infrastructure/`

- Implementação dos repositórios com **Entity Framework Core**.
- Exemplo: `FeatureToggleRepository.cs`.

- `API/`
- Camada de **exposição** (controllers e configuração do Swagger, autenticação, etc).
- Exemplo: `FeatureToggleController.cs`, `AuthController.cs`.

Essa organização promove **baixa dependência entre camadas**, facilitando a manutenção e testes.

---

## 🧪 Testes

- Os testes utilizam o **xUnit**.
- Estão localizados na pasta:

```

Tests/
└── Application/
├── ConfiguracaoAmbienteFeatureServiceUnitTests.cs
└── AmbienteControllerUnitTests.cs

```

- Para rodar os testes:

```bash
dotnet test
```

---

## ✅ Funcionalidades Implementadas

- ✅ **POST /api/featuretoggles**: Criação de nova FeatureToggle
- ✅ **GET /api/featuretoggles**: Listagem de todas as FeatureToggles
- ✅ **PUT /api/featuretoggles/{id}**: Atualização da FeatureToggle
- ✅ **POST /api/ambientes**: Criação de um novo Ambiente
- ✅ **GET /api/ambientes**: Listagem de Ambientes
- ✅ **POST /api/featuretoggles/{featureToggleId}/ambientes/{ambienteId}/config**:  
   Define ou atualiza a configuração de uma FeatureToggle para um Ambiente
- ✅ **GET /api/featuretoggles/status?featureName=...&environmentName=...**:  
   Retorna o estado de uma FeatureToggle para determinado ambiente (regra de negócio principal)
- ✅ **POST /api/auth/token**: Geração de token JWT de teste
- ✅ **Autenticação JWT aplicada aos endpoints de escrita (POST e PUT)**
- ✅ **Testes unitários com xUnit**, incluindo:
  - Verificação da regra de negócio principal
  - Testes de controllers

# Created By

```
  _                       _         _ _           _    _
 | |                     | |       | (_)         | |  (_)
 | |_   _  ___ __ _ ___  | | ____ _| |_ _ __  ___| | ___
 | | | | |/ __/ _` / __| | |/ / _` | | | '_ \/ __| |/ / |
 | | |_| | (_| (_| \__ \ |   < (_| | | | | | \__ \   <| |
 |_|\__,_|\___\__,_|___/ |_|\_\__,_|_|_|_| |_|___/_|\_\_|

```
