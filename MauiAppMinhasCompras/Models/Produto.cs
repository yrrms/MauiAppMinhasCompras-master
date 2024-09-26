using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto //criacao classe produto
    {
        string _descricao;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }//cria id que vai ser auto increment
        public string Descricao { //cria atributo descricao
            get => _descricao; 
            set
            {
                if(value == null) //validar se houve descricao
                {
                    throw new Exception("Por favor, preencha a descrição");//mensagem insucesso
                }

                _descricao = value;
            }
        }
        public double Quantidade {  get; set; }//cria atributo quantidade
        public double Preco {  get; set; }//cria atributo preco
        public double Total { get => Quantidade * Preco; }//cria o total
    }
}
