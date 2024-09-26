using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
//cria a classe e herda o content page
{
	public NovoProduto()//construtor da classe
	{
		InitializeComponent();//inicializa a classe
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)//manipulador do evento clique
    {
		try //se der certo criar o novo produto
		{
			Produto p = new Produto //cria o novo objeto produto
			{
				//propriedades
				Descricao = txt_descricao.Text,//pega a descricao do campo txt_descricao
				Quantidade = Convert.ToDouble(txt_quantidade.Text),//pega quantidade do campo quantidade e converte
				Preco = Convert.ToDouble(txt_preco.Text)//pega o preco do campo preco e converte
			};

			await App.Db.Insert(p);//insercao no banco
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");//mensagem de sucesso

		} catch(Exception ex)//se der errado 
		{
			await DisplayAlert("Ops", ex.Message, "OK");//mensagem de insucesso
		}
    }
}