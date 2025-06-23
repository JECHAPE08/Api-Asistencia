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

            // Actualizar en el hilo UI
            MainThread.BeginInvokeOnMainThread(() =>
            {
                usuariosCollection.ItemsSource = usuarios;
            });
        }
        catch (Exception ex)
        {
            // Puedes usar un logger o mostrar un mensaje para depuración
            System.Diagnostics.Debug.WriteLine($"Error cargando usuarios: {ex.Message}");
        }
    }

    private async void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        var seleccionado = e.CurrentSelection.FirstOrDefault() as Usuario;
        if (seleccionado == null)
            return;

        // Navegar a detalles
        await Navigation.PushAsync(new DetallesUsuariosControl(seleccionado));

        // Limpiar selección para evitar problema visual
        ((CollectionView)sender).SelectedItem = null;
    }

    private async void OnInicio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioControl());
    }
}
