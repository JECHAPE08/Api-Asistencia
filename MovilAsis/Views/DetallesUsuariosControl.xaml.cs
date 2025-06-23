using MovilAsis.Models;
using MovilAsis.Services;
namespace MovilAsis.Views;

public partial class DetallesUsuariosControl : ContentPage
{
    private List<AsistenciasMes> asistencias = new();
    private readonly Service _apiService;
    private int idUsuario;
    public DetallesUsuariosControl(Usuario usuario)
    {
        _apiService = new Service();
        InitializeComponent();
        idUsuario = usuario.Id;
        BindingContext = usuario;
        _ = ObtenerAsistenciasMes(usuario.Id);
    }

    public async void OnRegresar(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosControl());
    }

    public async Task ObtenerAsistenciasMes(int idUsuario)
    {
        try
        {

            asistencias = await _apiService.GetAsync<AsistenciasMes>($"Asistencias/AsistenciasUsuario?idUsuario={idUsuario}");

            asistenciasCollectionView.ItemsSource = asistencias;
        }
        catch (Exception)
        {
            return;
        }
    }

    public async void OnMapa(object sender, EventArgs e)
    {
        var boton = sender as Button;
        var asistencia = boton?.CommandParameter as AsistenciasMes;

        if (asistencia != null)
        {
            try
            {
                Global.AbrirMapa(asistencia.LatitudEntrada, asistencia.LongitudEntrada);
            }
            catch
            {
                await DisplayAlert("Error", "No se pudo abrir el mapa para esta asistencia.", "OK");
            }

        }

    }
    public async void OnEliminarUsuario(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert($"Confirmar",
                                         "¿Eliminar este usuario?",
                                         "Sí, eliminar",
                                         "Cancelar");

        if (confirmar)
        {
            try
            {
                var (success, message) = await _apiService.DeleteAsync($"Usuarios/Eliminar/{idUsuario}");

                if (success)
                {
                    await Navigation.PopAsync();
                    var nuevaPaginaUsuarios = new UsuariosControl();
                    await Navigation.PushAsync(nuevaPaginaUsuarios);

                    var existingPage = Navigation.NavigationStack
                        .FirstOrDefault(p => p is UsuariosControl && p != nuevaPaginaUsuarios);

                }
                else
                {
                    await DisplayAlert("Error", message, "OK");
                }
            }
            catch (HttpRequestException httpEx)
            {
                await DisplayAlert("Error", $"Error de conexión", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo eliminar", "OK");
            }       
        }
    }
}

