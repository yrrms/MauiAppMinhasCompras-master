using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views; // Define o namespace

//classe
public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()//construtor
    {
        InitializeComponent();//inicializador

        lst_produtos.ItemsSource = lista;//fonte dos itens
    }

    protected async override void OnAppearing()//metodo ao aparecer a pagina
    {
        try
        {
           // Limpa a lista atual
            lista.Clear();

            // pega os produtos do banco de dados
            List<Produto> tmp = await App.Db.GetAll();

            // Adiciona cada produto na lista 
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)//se der erro
        {
            await DisplayAlert("Ops", ex.Message, "OK");//mensagem insucesso
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    //metodo chamado no clique
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());//vai pra pag adicionar

        }
        catch (Exception ex)//se der erro
        {
            DisplayAlert("Ops", ex.Message, "OK");//mensagem insucesso
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)//metodo ao mudar o texto
    {
        try
        {
            // pega o novo texto da pesquisa
            string q = e.NewTextValue;

            // Limpa a lista atual
            lista.Clear();

            // faz a busca no banco de dados com o texto da pesquisa
            List<Produto> tmp = await App.Db.Search(q);

            // Adiciona os produtos encontrados na lista observável
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)//se der erro
        {
            await DisplayAlert("Ops", ex.Message, "OK");//mensagem insucesso
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)//metodo chamado no clique
    {
        // Calcula a soma total dos produtos na lista
        double soma = lista.Sum(i => i.Total);

        // Cria uma mensagem do total
        string msg = $"O total é {soma:C}";

        // Exibe um alerta com o total 
        DisplayAlert("Total dos Produtos", msg, "OK"); // Mensagem de insucesso
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)//metodo chamado no clique
    {
        try
        {
            // Obtém o item de menu selecionado
            MenuItem selecinado = sender as MenuItem;

            // Obtém o produto associado ao item de menu
            Produto p = selecinado.BindingContext as Produto;

            // Confirma a remoção do produto
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if(confirm)//se confirmar
            {
                await App.Db.Delete(p.Id);//tira do banco
                lista.Remove(p);//tira da lista
            }
        }
        catch (Exception ex)//se der erro
        {
            await DisplayAlert("Ops", ex.Message, "OK");//mensagem insucesso
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)//metodo chamado no clique
    {
        try
        {
            // Obtém o produto selecionado
            Produto p = e.SelectedItem as Produto;

            // Navega para a página de edição do produto, passando o produto selecionado como contexto de dados
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)//se der erro
        {
            DisplayAlert("Ops", ex.Message, "OK");//mensagem insucesso
        }
    }
}