using MauiAppMinhasCompras.Models;
using SQLite; //biblioteca sql

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper //definicao da classe
    {
        readonly SQLiteAsyncConnection _conn; //conexao assincrona com o banco

        public SQLiteDatabaseHelper(string path) //passando o caminho do banco de dados por parametro
        { 
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();//criando a tabela produto
        }

        public Task<int> Insert(Produto p) //inserir produto, passando o objeto p da classe Produto
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p) //atualizar produto, passando o objeto p da classe Produto
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        public Task<int> Delete(int id) //deletando produto a partir do id
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll() //lista todos os produtos
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q) //busca de produtos a partir de descricao
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
