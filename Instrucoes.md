Lab 12 - Conexão com SQL Server

**Disciplina:** C# Software Development
**Turma:** 3ESPR
**Data:** 13/10/2025
**Professor:** Charles

---

## 📦 Conteúdo do Pacote

```
LAB10_ConexaoSQLServer/
│
├── README.md # Este arquivo
├── INSTRUÇÕES_LAB10.md # Guia detalhado do laboratório
├── slides_aula12.md # Slides da aula teórica
├── setup.sql # Script de criação do banco
├── LAB10-ConexaoSQLServer.cs # Código base do laboratório
│
└── Solucoes/ # (Apenas para o professor)
└── LAB10-ConexaoSQLServer-Completo.cs
```

---

## 🎯 Objetivos da Aula

Ao final desta aula, o aluno será capaz de:

✅ Configurar SQL Server em Docker localmente
✅ Estabelecer conexões com banco de dados usando ADO.NET
✅ Executar comandos SELECT, INSERT, UPDATE e DELETE
✅ Utilizar parâmetros para prevenir SQL Injection
✅ Implementar transações para garantir consistência
✅ Gerenciar recursos de conexão adequadamente

---

## ⏱️ Cronograma da Aula (2 horas)

### Parte 1: Teoria (40 min)
- Introdução ao ADO.NET (10 min)
- SQL Server em Docker (5 min)
- Connection Strings e Conexões (5 min)
- Executando Comandos SQL (10 min)
- Boas Práticas e Segurança (10 min)

### Parte 2: Laboratório (80 min)
- Setup do Ambiente (15 min)
- Exercícios Básicos 1-6 (30 min)
- Exercícios Avançados 7-10 (35 min)

---

## 🚀 Quick Start

### 1. Iniciar SQL Server
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SqlServer2024!" \
-p 1433:1433 --name sqlserver2022 -d \
mcr.microsoft.com/mssql/server:2022-latest
```

### 2. Executar Setup do Banco
- Conecte ao SQL Server (localhost,1433 / sa / SqlServer2024!)
- Execute o arquivo `setup.sql`

### 3. Abrir o Projeto base enviado C#


### 6. Completar Connection String
```csharp
private static string connectionString =
"Server=localhost,1433;" +
"Database=LojaDB;" +
"User Id=sa;" +
"Password=SqlServer2024!;" +
"TrustServerCertificate=True;";
```

### 7. Executar
```bash
dotnet run
```

---

## 📚 Conceitos Abordados

### ADO.NET Classes
- `SqlConnection` - Gerencia conexão
- `SqlCommand` - Executa comandos SQL
- `SqlDataReader` - Lê resultados
- `SqlParameter` - Parâmetros seguros
- `SqlTransaction` - Controle de transações

### Padrões de Código
- Using statement para recursos
- Parametrização de queries
- Tratamento de exceções
- Gerenciamento de transações

### SQL Commands
- SELECT - Leitura de dados
- INSERT - Inserção de dados
- UPDATE - Atualização de dados
- DELETE - Exclusão de dados
- JOIN - Relacionamento de tabelas

---

## 🗄️ Modelo de Dados

```
┌─────────────┐
│ Clientes │
└──────┬──────┘
│
│ 1:N
▼
┌─────────────┐ ┌──────────────┐
│ Pedidos │◄───────►│ PedidoItens │
└──────┬──────┘ 1:N └──────┬───────┘
│ │
│ │
▼ ▼
┌──────────────┐ ┌─────────────┐
│ Categorias │◄────┐ │ Produtos │
└──────────────┘ 1:N└───┴─────────────┘
```

---

## ✅ Exercícios

### Básicos (Obrigatórios)
1. ⭐ Connection String
2. ⭐ Listar Produtos
3. ⭐ Inserir Produto
4. ⭐ Atualizar Produto
5. ⭐ Deletar Produto
6. ⭐ Buscar por ID

### Intermediários (Obrigatórios)
7. ⭐⭐ Listar por Categoria (JOIN)
8. ⭐⭐ Listar Pedidos de Cliente
9. ⭐⭐ Detalhes do Pedido

### Avançados (Obrigatórios)
10. ⭐⭐⭐ Criar Pedido com Transação

### Desafios (Opcionais - Pontos Extra)
- 🌟 Estoque Baixo
- 🌟 Busca por Nome (LIKE)
- 🌟 Total de Vendas por Período
- 🌟 Métodos Auxiliares Completos

---

## 🛠️ Ferramentas Necessárias

### Obrigatórias
- **[Docker Desktop](https://www.docker.com/products/docker-desktop)** - Container SQL Server
- Windows: WSL 2 habilitado
- macOS: Versão compatível com seu processador (Intel/Apple Silicon)
- Linux: Docker Engine
- **[Guia completo de instalação](https://docs.docker.com/engine/install/)** no arquivo de instruções
- **[.NET SDK 6.0+](https://dotnet.microsoft.com/download)** - Desenvolvimento C#
- **[Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download)** ou SSMS - Cliente SQL

### Recomendadas
- Visual Studio 2022 ou Rider - IDE completa
- VS Code + C# Extension - Alternativa leve
- DBeaver - Cliente SQL multiplataforma

---

## 🐳 Guia Rápido do Docker

### Instalação
Veja o **[guia completo de instalação do Docker](INSTRUÇÕES_LAB10.md#-instalação-do-docker)** no arquivo `INSTRUÇÕES_LAB10.md`, com instruções detalhadas para:
- ✅ Windows (10/11 Home, Pro, Enterprise)
- ✅ macOS (Intel e Apple Silicon)
- ✅ Linux (Ubuntu/Debian/Fedora)

### Comandos Essenciais para o Lab

```bash
# Iniciar SQL Server
docker run -e "ACCEPT_EULA=Y" \
-e "MSSQL_SA_PASSWORD=SqlServer2024!" \
-p 1433:1433 --name sqlserver2022 -d \
mcr.microsoft.com/mssql/server:2022-latest

# Verificar status
docker ps

# Ver logs
docker logs sqlserver2022

# Parar
docker stop sqlserver2022

# Iniciar novamente
docker start sqlserver2022

# Remover (para recomeçar do zero)
docker rm -f sqlserver2022
```

### Troubleshooting Docker
Problemas com Docker? Consulte a **[seção completa de troubleshooting](INSTRUÇÕES_LAB10.md#-troubleshooting-específico-do-docker)** que inclui:
- Docker não inicia (Windows/Mac/Linux)
- Container SQL Server não inicia
- Problemas de porta 1433
- Problemas de performance
- Comandos de emergência
- Alternativas ao Docker

---

## 📖 Material de Apoio

### Documentação Oficial
- [ADO.NET Documentation](https://docs.microsoft.com/dotnet/framework/data/adonet/)
- [SQL Server on Docker](https://docs.microsoft.com/sql/linux/quickstart-install-connect-docker)
- [T-SQL Reference](https://docs.microsoft.com/sql/t-sql/language-reference)

### Tutoriais Complementares
- [Connection String Reference](https://www.connectionstrings.com/sql-server/)
- [SQL Injection Prevention](https://cheatsheetseries.owasp.org/cheatsheets/SQL_Injection_Prevention_Cheat_Sheet.html)
- [Transaction Best Practices](https://docs.microsoft.com/sql/connect/ado-net/sql/transaction-and-bulk-copy-operations)

---

## ⚠️ Avisos Importantes

### Segurança
🔒 **NUNCA concatene SQL** - Sempre use parâmetros
🔒 **Não exponha erros** em produção
🔒 **Valide entrada** do usuário
🔒 **Use senhas fortes** em ambientes reais

### Performance
⚡ **Sempre feche conexões** - Use `using`
⚡ **Reutilize connection strings** - Connection pooling
⚡ **Use índices** no banco de dados
⚡ **Evite múltiplas queries** - Prefira JOINs

### Desenvolvimento
💡 **Teste queries** no Azure Data Studio primeiro
💡 **Use transações** para operações múltiplas
💡 **Leia comentários** no código - eles guiam!
💡 **Consulte correções** quando travar

---

## 🐛 Troubleshooting Rápido

| Erro | Solução Rápida |
|------|----------------|
| "Cannot open database" | Execute setup.sql novamente |
| "Login failed" | Verifique senha no Docker e connection string |
| "Network error" | Verifique se Docker está rodando: `docker ps` |
| "Timeout" | Adicione `Connection Timeout=30;` na string |
| "Invalid column" | Verifique nomes das colunas C# vs SQL |

**Mais detalhes:** Consulte seção Troubleshooting em `INSTRUÇÕES_LAB10.md`

---

## 📊 Critérios de Avaliação

| Item | Peso |
|------|------|
| CRUD básico (Ex. 1-4) | 20% |
| Busca e validações (Ex. 5-6) | 15% |
| JOIN e listagem (Ex. 7) | 15% |
| Transação completa (Ex. 8) | 20% |
| Listagens de pedidos (Ex. 9-10) | 10% |
| Código limpo e boas práticas | 20% |
| **TOTAL** | **100%** |

**Pontos Extra:** Desafios opcionais valem até +20%

---


## 📝 Notas do Professor

### Para a Turma
- Leia TODA a instrução antes de começar
- Os comentários no código são seus guias
- Não tenha medo de errar - é parte do aprendizado
- Colabore com colegas, mas faça seu próprio código

### Tempo Estimado
- Alunos rápidos: 60-70 minutos
- Maioria da turma: 80-90 minutos
- Com desafios: 100-120 minutos

### Pontos de Atenção
- Muitos esquecem o `using` statement
- SQL Injection é tentador para quem tem pressa
- Transações causam mais dúvidas
- Connection string com erro é comum no início

---


