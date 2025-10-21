using Microsoft.Data.SqlClient;

namespace SistemaLoja.Lab12_ConexaoSQLServer;

public class ProdutoRepository
{
    public void ListarTodosProdutos()
    {
        var sql = "SELECT Id, Nome, Preco, Estoque, CategoriaId FROM Produtos ORDER BY Id";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("\nID | NOME | PREÇO | ESTOQUE | CATEGORIA");
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetDecimal(2):F2} | {reader.GetInt32(3)} | {reader.GetInt32(4)}");
        }
    }

    public void InserirProduto(Produto p)
    {
        var sql = @"INSERT INTO Produtos (Nome, Preco, Estoque, CategoriaId)
                    VALUES (@Nome, @Preco, @Estoque, @CategoriaId);";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Nome", p.Nome);
        cmd.Parameters.AddWithValue("@Preco", p.Preco);
        cmd.Parameters.AddWithValue("@Estoque", p.Estoque);
        cmd.Parameters.AddWithValue("@CategoriaId", p.CategoriaId);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Produto inserido.");
    }

    public void AtualizarProduto(Produto p)
    {
        var sql = @"UPDATE Produtos SET Nome=@Nome, Preco=@Preco, Estoque=@Estoque, CategoriaId=@CategoriaId
                    WHERE Id=@Id;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Nome", p.Nome);
        cmd.Parameters.AddWithValue("@Preco", p.Preco);
        cmd.Parameters.AddWithValue("@Estoque", p.Estoque);
        cmd.Parameters.AddWithValue("@CategoriaId", p.CategoriaId);
        cmd.Parameters.AddWithValue("@Id", p.Id);
        var rows = cmd.ExecuteNonQuery();
        Console.WriteLine(rows > 0 ? "Produto atualizado." : "Produto não encontrado.");
    }

    public void DeletarProduto(int id)
    {
        var sql = "DELETE FROM Produtos WHERE Id=@Id;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        var rows = cmd.ExecuteNonQuery();
        Console.WriteLine(rows > 0 ? "Produto deletado." : "Produto não encontrado.");
    }

    public Produto? BuscarPorId(int id)
    {
        var sql = "SELECT Id, Nome, Preco, Estoque, CategoriaId FROM Produtos WHERE Id=@Id;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;
        return new Produto
        {
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1),
            Preco = reader.GetDecimal(2),
            Estoque = reader.GetInt32(3),
            CategoriaId = reader.GetInt32(4)
        };
    }

    public void ListarProdutosPorCategoria(int categoriaId)
    {
        var sql = @"SELECT p.Id, p.Nome, p.Preco, p.Estoque, c.Nome as Categoria
                    FROM Produtos p
                    INNER JOIN Categorias c ON p.CategoriaId = c.Id
                    WHERE p.CategoriaId = @CategoriaId
                    ORDER BY p.Id;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("\nID | NOME | PREÇO | ESTOQUE | CATEGORIA");
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetDecimal(2):F2} | {reader.GetInt32(3)} | {reader.GetString(4)}");
        }
    }

    public void ListarProdutosEstoqueBaixo(int quantidadeMinima)
    {
        var sql = @"SELECT Id, Nome, Preco, Estoque FROM Produtos
                    WHERE Estoque < @q ORDER BY Estoque ASC;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@q", quantidadeMinima);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("\nID | NOME | PREÇO | ESTOQUE");
        while (reader.Read())
        {
            var estoque = reader.GetInt32(3);
            var alert = estoque < quantidadeMinima ? " **ESTOQUE BAIXO**" : "";
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetDecimal(2):F2} | {estoque}{alert}");
        }
    }

    public void BuscarProdutosPorNome(string termoBusca)
    {
        var sql = @"SELECT Id, Nome, Preco, Estoque, CategoriaId
                    FROM Produtos
                    WHERE Nome LIKE @Busca
                    ORDER BY Nome;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Busca", "%" + termoBusca + "%");
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("\nID | NOME | PREÇO | ESTOQUE | CATEGORIA");
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetDecimal(2):F2} | {reader.GetInt32(3)} | {reader.GetInt32(4)}");
        }
    }
}
