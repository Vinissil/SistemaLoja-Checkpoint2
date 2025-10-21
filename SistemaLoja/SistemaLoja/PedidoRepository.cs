using Microsoft.Data.SqlClient;

namespace SistemaLoja.Lab12_ConexaoSQLServer;

public class PedidoRepository
{
    public void CriarPedido(Pedido pedido, List<PedidoItem> itens)
    {
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();
        try
        {
            var insertPedido = @"INSERT INTO Pedidos (ClienteId, DataPedido, ValorTotal, Status)
                                 OUTPUT INSERTED.Id
                                 VALUES (@ClienteId, @DataPedido, @ValorTotal, @Status);";
            using var cmdPedido = new SqlCommand(insertPedido, conn, tx);
            cmdPedido.Parameters.AddWithValue("@ClienteId", pedido.ClienteId);
            cmdPedido.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
            cmdPedido.Parameters.AddWithValue("@ValorTotal", pedido.ValorTotal);
            cmdPedido.Parameters.AddWithValue("@Status", pedido.Status);
            var pedidoId = (int)cmdPedido.ExecuteScalar();

            foreach (var it in itens)
            {
                var insertItem = @"INSERT INTO PedidoItens (PedidoId, ProdutoId, Quantidade, PrecoUnitario)
                                   VALUES (@PedidoId, @ProdutoId, @Quantidade, @PrecoUnitario);";
                using var cmdItem = new SqlCommand(insertItem, conn, tx);
                cmdItem.Parameters.AddWithValue("@PedidoId", pedidoId);
                cmdItem.Parameters.AddWithValue("@ProdutoId", it.ProdutoId);
                cmdItem.Parameters.AddWithValue("@Quantidade", it.Quantidade);
                cmdItem.Parameters.AddWithValue("@PrecoUnitario", it.PrecoUnitario);
                cmdItem.ExecuteNonQuery();

                var atualizaEstoque = @"UPDATE Produtos SET Estoque = Estoque - @q WHERE Id=@id;";
                using var cmdEstoque = new SqlCommand(atualizaEstoque, conn, tx);
                cmdEstoque.Parameters.AddWithValue("@q", it.Quantidade);
                cmdEstoque.Parameters.AddWithValue("@id", it.ProdutoId);
                cmdEstoque.ExecuteNonQuery();
            }

            tx.Commit();
            Console.WriteLine($"Pedido {pedidoId} criado.");
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void ListarPedidosCliente(int clienteId)
    {
        var sql = @"SELECT Id, DataPedido, ValorTotal, Status
                    FROM Pedidos
                    WHERE ClienteId=@ClienteId
                    ORDER BY DataPedido DESC;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ClienteId", clienteId);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("\nID | DATA | TOTAL | STATUS");
        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetDateTime(1):yyyy-MM-dd} | {reader.GetDecimal(2):F2} | {reader.GetString(3)}");
        }
    }

    public void ObterDetalhesPedido(int pedidoId)
    {
        var cab = @"SELECT p.Id, c.Nome, p.DataPedido, p.ValorTotal, p.Status
                    FROM Pedidos p
                    INNER JOIN Clientes c ON p.ClienteId = c.Id
                    WHERE p.Id=@PedidoId;";
        var itens = @"SELECT pi.Id, pr.Nome, pi.Quantidade, pi.PrecoUnitario, (pi.Quantidade*pi.PrecoUnitario) as Subtotal
                      FROM PedidoItens pi
                      INNER JOIN Produtos pr ON pi.ProdutoId = pr.Id
                      WHERE pi.PedidoId=@PedidoId
                      ORDER BY pi.Id;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using (var cmd = new SqlCommand(cab, conn))
        {
            cmd.Parameters.AddWithValue("@PedidoId", pedidoId);
            using var r = cmd.ExecuteReader();
            if (r.Read())
            {
                Console.WriteLine($"Pedido: {r.GetInt32(0)}");
                Console.WriteLine($"Cliente: {r.GetString(1)}");
                Console.WriteLine($"Data: {r.GetDateTime(2):yyyy-MM-dd}");
                Console.WriteLine($"Total: {r.GetDecimal(3):F2}");
                Console.WriteLine($"Status: {r.GetString(4)}");
            }
            else
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }
        }
        Console.WriteLine("\nITENS");
        using (var cmd2 = new SqlCommand(itens, conn))
        {
            cmd2.Parameters.AddWithValue("@PedidoId", pedidoId);
            using var r2 = cmd2.ExecuteReader();
            Console.WriteLine("ID | PRODUTO | QTD | PREÇO | SUBTOTAL");
            while (r2.Read())
            {
                Console.WriteLine($"{r2.GetInt32(0)} | {r2.GetString(1)} | {r2.GetInt32(2)} | {r2.GetDecimal(3):F2} | {r2.GetDecimal(4):F2}");
            }
        }
    }

    public void TotalVendasPorPeriodo(DateTime dataInicio, DateTime dataFim)
    {
        var sql = @"SELECT COALESCE(SUM(ValorTotal),0) FROM Pedidos
                    WHERE DataPedido >= @ini AND DataPedido < @fim;";
        using var conn = DatabaseConnection.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ini", dataInicio);
        cmd.Parameters.AddWithValue("@fim", dataFim);
        var total = (decimal)cmd.ExecuteScalar();
        Console.WriteLine($"Total de vendas no período: {total:F2}");
    }
}
