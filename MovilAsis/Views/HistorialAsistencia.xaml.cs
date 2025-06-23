using MovilAsis.Models;
using MovilAsis.Services;

namespace MovilAsis.Views;

public partial class HistorialAsistencia : ContentPage
{
    private readonly Service _apiService;
    private List<AsistenciasMes> asistencias = new();

    public HistorialAsistencia()
    {
        _apiService = new Service();
        InitializeComponent();
        _ = ObtenerAsistenciasMes(Preferences.Get("Id", 0));
    }

    public async Task ObtenerAsistenciasMes(int idUsuario)
    {
        try
        {
            asistencias = await _apiService.GetAsync<AsistenciasMes>($"Asistencias/AsistenciasUsuario?idUsuario={idUsuario}");

            ListaAsistenciasView.ItemsSource = asistencias;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se encontraron asistencias", "OK");
        }
    }

    private async void OnRegresar(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Regresa a la página anterior (Inicio)
    }
}
