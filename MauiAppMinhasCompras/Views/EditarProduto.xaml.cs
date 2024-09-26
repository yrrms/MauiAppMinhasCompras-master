using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;//define o namespace

public partial class EditarProduto : ContentPage // Classe da edicao
{
	public EditarProduto() //construtor
	{
		InitializeComponent();//inicializador
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)//metodo ao clicar
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto; //pega o conteudo 

            Produto p = new Produto
            {
                Id = produto_anexado.Id, //pega id
                Descricao = txt_descricao.Text,//pega descricao
                Quantidade = Convert.ToDouble(txt_quantidade.Text),//pega e converte quantidade
                Preco = Convert.ToDouble(txt_preco.Text)//pega e converte
            };

            await App.Db.Update(p);//atualiza no bd
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");//msg de sucesso
            await Navigation.PopAsync();//navega pra pagina
        }
        catch (Exception ex)//pega erros
        {
            await DisplayAlert("Ops", ex.Message, "OK");//msg de erro
        }
    }
}