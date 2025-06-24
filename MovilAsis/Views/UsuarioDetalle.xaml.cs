using MovilAsis.Models;
using TimeZoneConverter;
using MovilAsis.Services;
namespace MovilAsis.Views;

public partial class UsuarioDetalle : ContentPage
{
    public UsuarioDetalle(AsistenciaDelDiaDTO usuario)
	{
		InitializeComponent();
        BindingContext = usuario;
    }

    private async void OnAbrirMapaClicked(object sender, EventArgs e)
    {
        var asistencia = (AsistenciaDelDiaDTO)BindingContext;

        try
        {
            Global.AbrirMapa(asistencia.LatitudEntrada, asistencia.LongitudEntrada);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo abrir el mapa: ", "OK");
        }
    }

    public async void OnInicio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioControl());
    }


}