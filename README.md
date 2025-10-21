# SistemaLoja — Checkpoint 2 (C# + SQL Server)

Aplicação de console em C# para CRUD de produtos e fluxo de pedidos com SQL Server.

## Requisitos
- .NET SDK 8+
- SQL Server (local) **ou** Docker
- Ferramenta para executar SQL (SSMS, Azure Data Studio ou `sqlcmd`)

## Banco de Dados
### Opção A — Docker (recomendado)
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SqlServer2024!"   -p 1433:1433 --name sqlserver2022 -d mcr.microsoft.com/mssql/server:2022-latest
```
Verifique o container com:
```bash
docker ps
```

### Opção B — Instância local
Habilite a porta 1433. Garanta usuário `sa` ativo (ou equivalente) e ajuste a connection string se necessário.

## Criar a base e dados de exemplo
Execute **todo** o script `Setup.sql` no servidor `localhost,1433`. Ele cria a base `LojaDB`, tabelas e dados exemplo.

## Connection string
Em `SistemaLoja/SistemaLoja/DatabaseConnection.cs`:
```
Server=localhost,1433; Database=LojaDB; User Id=sa; Password=SqlServer2024!; TrustServerCertificate=True;
```

## Executar a aplicação
```bash
cd SistemaLoja/SistemaLoja
dotnet run
```

## Menu (funcionalidades)
**Produtos**
1. Listar todos os produtos  
2. Inserir produto  
3. Atualizar produto  
4. Deletar produto  
5. Listar por categoria (JOIN)

**Pedidos**
6. Criar novo pedido (transação: pedido + itens + baixa de estoque)  
7. Listar pedidos de um cliente  
8. Detalhes de um pedido (cabeçalho + itens)

**Extras**
- Produtos com **estoque baixo** (parâmetro)  
- Busca por **nome (LIKE)**  
- Total de vendas por **período**

## Estrutura
```
SistemaLoja/
  SistemaLoja/
    DatabaseConnection.cs
    *.cs (entidades e repositórios)
    Program.cs
Setup.sql
Instrucoes.md
```

## Problemas comuns
- Conexão falhou: confirme se o SQL Server está ativo (Docker/local) e se porta/credenciais batem.  
- Permissão negada: use login com permissão para criar DB/objetos.  
- Timeout: aguarde o container iniciar e tente novamente.

## Entrega (conforme instrução)
- Criar **repositório GitHub** com **nome claro**.  
- **Carregar o projeto testado**, incluindo os **arquivos necessários** (`Setup.sql`, `Instrucoes.md`).  
- **Verificar organização e presença de arquivos** antes de enviar o **link do GitHub**.
