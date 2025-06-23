
using MovilAsis.Models;
using MovilAsis.Services;

namespace MovilAsis.Views;

public partial class Registro : ContentPage
{
    private readonly  Service _apiService;
    public Registro()
    {
        _apiService = new Service();
		InitializeComponent();
	}

    public async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = (Usuario)this.BindingContext;

            if (string.IsNullOrEmpty(usuario.Contraseña) || string.IsNullOrEmpty(usuario.Matricula) ||
                string.IsNullOrEmpty(usuario.Nombre))
            {
                await DisplayAlert("ERROR", "LLene los datos necesarios", "OK");
            }
           
            try
            {

                var (responsse, usuarioDTO) = await _apiService.PostObjetoAsync<UsuarioLoginDTO>("Usuarios/Registro", usuario);

                if (responsse)
                {
                    Global.GuardarUsuario(usuario.Matricula, usuario.Contraseña, usuarioDTO.Id);
                    await DisplayAlert("Registro", "Registro exitoso", "Aceptar");

                    if (usuarioDTO.Rol == "Docente")
                    {             
                        await Navigation.PushAsync(new InicioControl());
                    }
                    await Navigation.PushAsync(new Inicio());
                }
               

                
                await Navigation.PushAsync(new Inicio());
            }
            catch (HttpRequestException httpEx) when (httpEx.Message.Contains("401"))
            {
                // Captura específicamente errores HTTP 401 (No autorizado).
                await DisplayAlert("ERROR", "Matricula ya registrada", "OK");
            }
            catch (Exception ex)
            {
                // Maneja otros errores (ej: sin conexión, error en el servidor).
                await DisplayAlert("ERROR", $"Error al conectar", "OK");
            }



        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        //await Navigation.PopAsync();
    }

   

    public async void LoginButton_Cliked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
}