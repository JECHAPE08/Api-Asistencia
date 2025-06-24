using MovilAsis.Models;
using MovilAsis.Services;

namespace MovilAsis.Views;

public partial class UsuariosControl : ContentPage
{
    private List<Usuario> usuarios = new();
    private readonly Service _apiService;

    public UsuariosControl()
    {
        _apiService = new Service();
        InitializeComponent();
        _=CargarUsuariosAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarUsuariosAsync();
    }

    private async Task CargarUsuariosAsync()
    {

        try
        {

            var lista = await _apiService.GetAsync<Usuario>("ListaUsuarios");
            usuarios = lista?.ToList() ?? new List<Usuario>();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                usuariosCollection.ItemsSource = usuarios;
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error cargando usuarios: {ex.Message}");
        }
    }

    private async void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        var seleccionado = e.CurrentSelection.FirstOrDefault() as Usuario;
        if (seleccionado == null)
            return;

        await Navigation.PushAsync(new DetallesUsuariosControl(seleccionado));
        ((CollectionView)sender).SelectedItem = null;
    }

    private async void OnInicio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioControl());
    }
}
