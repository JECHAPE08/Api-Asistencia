using MovilAsis.Services;
using MovilAsis.Models;
using System.Diagnostics;

namespace MovilAsis.Views;

public partial class InicioControl : ContentPage
{
    private List<AsistenciaDelDiaDTO> asistencias = new();
    private readonly Service _apiService;
    public InicioControl()
    {
        _apiService = new Service();
        InitializeComponent();
        _ = ObtenerUsuariosConAsistencia();
    }

    //public async void Suscribir()
    //{
    //    try
    //    {
    //        await _topicService.SubscribeToTopicAsync("Asistecia");
    //        Debug.Write("Suscrito a Asistencia");
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.Write($"Error al suscribirse a Asistencia: {ex.Message}");
    //        await DisplayAlert("Error", "No se pudo suscribir a las notificaciones de asistencia.", "OK");

    //    }
    //}
    //}
    public async Task ObtenerUsuariosConAsistencia()
    {
        try
        {
            asistencias = await _apiService.GetAsync<AsistenciaDelDiaDTO>("Usuarios/Actuales");

            usuariosCollectionView.ItemsSource = asistencias;
        }
        catch (Exception)
        {
            return;
        }
    }

    private async void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        var usuarioSeleccionado = e.CurrentSelection.FirstOrDefault() as AsistenciaDelDiaDTO;
        if (usuarioSeleccionado != null)
        {
            await Navigation.PushAsync(new UsuarioDetalle(usuarioSeleccionado));
        }
        // Limpiar lista
        usuariosCollectionView.SelectedItem = null;
    }

    public async void OnCerrarSesion(object sender, EventArgs e)
    {
        Global.CerrarSesion();
        await Navigation.PushAsync(new Login());
        await Navigation.PopToRootAsync();
    }

    public async void OnUsuariosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosControl());
    }




    private async void OnAbrirMenu(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Ver usuarios", "Cerrar sesión");

        switch (action)
        {
            case "Cerrar sesión":
                OnCerrarSesion(sender, e);
                break;

            case "Ver usuarios":
                OnVerUsuarios(sender, e);
                break;
        }
    }

    private async void OnVerUsuarios(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsuariosControl());
    }

}
