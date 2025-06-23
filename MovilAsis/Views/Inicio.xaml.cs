using MovilAsis.Models;
using MovilAsis.Services;
namespace MovilAsis.Views;

public partial class Inicio : ContentPage
{
    private readonly Service _apiService;
    private double Longitud;
    private double Latitud;
    int idUsuario = Preferences.Get("Id", 0);
    TimeSpan horaEntradaMinima = new TimeSpan(11, 45, 0);
    TimeSpan horaEntradaMaxima = new TimeSpan(12, 30, 0);
    TimeSpan horaSalidaMinima = new TimeSpan(19, 45, 0);
    TimeSpan horaSalidaMaxima = new TimeSpan(21, 0, 0);
    public Inicio()
	{
        _apiService = new Service();
        InitializeComponent();
        ActualizarBoton();
        ObtenerUbicacion();
    }

    public void ActualizarBoton()
    {
        try
        {

            TimeSpan hora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            if (hora >= horaSalidaMinima && hora <= horaSalidaMaxima)
            {
                TextoBoton.IsEnabled = true;
                TextoBoton.Text = "Asistencia Salida";
            }
            else if (hora >= horaEntradaMinima && hora <= horaEntradaMaxima)
            {
                TextoBoton.Text = "Asistencia Entrada";
                if(!Preferences.ContainsKey("IdAsistencia"))
                {
                    TextoBoton.IsEnabled = true;
                }
            }
            else
            {
                TextoBoton.Text = "Fuera de horario";
                TextoBoton.IsEnabled = false;
            }
        }
        catch (Exception ex)
        {
            return;

        }
    }

    private async void OnVerHistorialClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistorialAsistencia());
    }

    private async void OnFuncionTiempo(object sender, EventArgs e)
    {
        
        try
        {
            if (Latitud == 0 && Longitud == 0)
            {
                await DisplayAlert("Error", "No se pudo obtener la ubicación al tomar la asistencia ", "OK");
                return;
            }
            TimeSpan hora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            if (hora >= horaSalidaMinima && hora <= horaSalidaMaxima)
            {
                await OnAsistenciaSalida();
            }
            else if (hora >= horaEntradaMinima && hora <= horaEntradaMaxima)
            {
                await OnAsistenciaEntrada();
            }
        }
        catch(Exception ex)
        {
            return;
        }
        
    }

    private async void ObtenerUbicacion()
    {
        try
        {
            // Verificar y solicitar permisos explícitamente
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Permiso denegado", "No se puede acceder a la ubicación sin permisos.", "OK");
            }

            // Obtener ubicación solo si hay permisos
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.Default.GetLocationAsync(request);

            Longitud = location.Longitude;
            Latitud = location.Latitude;
        }
        catch (PermissionException)
        {
            await DisplayAlert("Error", "Permisos insuficientes. Actívalos en Ajustes.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener la ubicación: {ex.Message}", "OK");
        }
    }


    private async void OnCerrarSesion(object sender, EventArgs e)
    {
        Global.CerrarSesion();
        await Navigation.PushAsync(new Login());
        await Navigation.PopToRootAsync();
    }
    
    
    private async Task OnAsistenciaEntrada()
    {      
        try
        {
            
            var entrada = CrearAsistenciaEntrada(Latitud, Longitud, idUsuario);
            var (response, idAsistencia) = await _apiService.PostAsync("Asistencia/RegistroEntrada", entrada);
            if (response)
            {
                await DisplayAlert("Asistencia", "Entrada registrada correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo registrar la entrada", "OK");
            }
        }
        catch (HttpRequestException httpEx) when (httpEx.Message.Contains("401"))
        {
            // Captura específicamente errores HTTP 401 (No autorizado).
            await DisplayAlert("Asistencia", "Asistencia ya registrada", "OK");
        }
        catch (Exception ex)
        {
            return;
        }
      
        


    }


    private async Task OnAsistenciaSalida()
    {
        try
        {
            TimeSpan hora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);

            if (hora < horaSalidaMinima && hora > horaEntradaMaxima)
            {
                await DisplayAlert("Asistencia", "Aún no es hora de salida", "OK");
                return;
            }
            int idAsistencia = await _apiService.GetValor($"Asistencia/RegistroId?id={idUsuario}");
            var salida = CrearAsistenciaSalida(Latitud, Longitud);

            var (response, contenido) = await _apiService.PutAsync($"Asistencia/RegistroSalida?id={idAsistencia}", salida);
            if (response)
            {
                Preferences.Remove("IdAsistencia");
                await DisplayAlert("Asistencia", "Salida registrada correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo registrar la salida por que no se registro una entrada en esat cuenta", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
        }
    }


    public Asistencia CrearAsistenciaEntrada(double latitud, double longitud, int usuarioId)
    {
        if (usuarioId <= 0)
            throw new ArgumentException("ID de usuario inválido");
        
        int fecha = ObtenerFecha();
        return new Asistencia
        {
            FechaEntrada = fecha,
            LatitudEntrada = latitud,
            LongitudEntrada = longitud,
            UsuarioId = usuarioId
            
        };
    }

    public Asistencia CrearAsistenciaSalida(double latitud, double longitud)
    {
        int fecha = ObtenerFecha();
        return new Asistencia
        {
            FechaSalida = fecha,
            LatitudSalida = latitud,
            LongitudSalida = longitud,
        };
    }


    public int ObtenerFecha()
    {
        DateTime fechaLocal = DateTime.Now;
        int epochFecha = (int)(new DateTimeOffset(fechaLocal).ToUnixTimeSeconds());
        return epochFecha;
    }

    
}