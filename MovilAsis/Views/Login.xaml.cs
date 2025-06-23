using MovilAsis.Models;
using MovilAsis.Services;
using System.Net.Http.Json;
using Microsoft.Maui.Storage;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


using Microsoft.Extensions.DependencyInjection;
namespace MovilAsis.Views;

public partial class Login : ContentPage
{
    private readonly Service _apiService;
    public Login()
	{
        _apiService = new Service();
		IniciarAutomaticamente();
		InitializeComponent();
        
    }

	public async void LoginButton_Clicked(object sender, EventArgs e)
	{

        try
        {
            var usuario = (UsuarioLogin)this.BindingContext;

            if (string.IsNullOrEmpty(usuario.Contrase�a) || string.IsNullOrEmpty(usuario.Matricula))
            {
                await DisplayAlert("ERROR", "LLene los datos necesarios", "OK");
                return;
            }

            try
            {

                var (response, UsuarioDTO) = await _apiService.PostObjetoAsync<UsuarioLoginDTO>("Usuarios/Login", usuario);
                if (response && UsuarioDTO.Rol == "Docente")
                {
                    Global.GuardarUsuario(usuario.Matricula, usuario.Contrase�a, UsuarioDTO.Id);
                    await Navigation.PushAsync(new InicioControl());
                }
                else
                {
                    Global.GuardarUsuario(usuario.Matricula, usuario.Contrase�a, UsuarioDTO.Id);
                    await Navigation.PushAsync(new Inicio());
                }

            }
            catch (HttpRequestException httpEx) when (httpEx.Message.Contains("401"))
            {
                // Captura espec�ficamente errores HTTP 401 (No autorizado).
                await DisplayAlert("ERROR", "Contrase�a incorrectas", "OK");
            }
            catch (HttpRequestException httpEx) when (httpEx.Message.Contains("404"))
            {
                // Captura espec�ficamente errores HTTP 401 (No autorizado).
                await DisplayAlert("ERROR", "Matr�cula no registrada", "OK");
            }
        }
        catch (Exception ex)
        {
            return;
        }

    }

	public async void RegisterButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Registro());
	}


	public async void IniciarAutomaticamente()
	{
		if (Preferences.ContainsKey("Matricula") && Preferences.ContainsKey("Contrase�a"))
		{
            try
            {
                var usuario = new UsuarioLogin
                {
                    Matricula = Preferences.Get("Matricula", string.Empty),
                    Contrase�a = Preferences.Get("Contrase�a", string.Empty)
                };

                var (response, UsuarioDTO) = await _apiService.PostObjetoAsync < UsuarioLoginDTO>("Usuarios/Login", usuario);

                if (response)
                {
                    if (response && UsuarioDTO.Rol == "Docente")
                    {
                        await Navigation.PushAsync(new InicioControl());
					}
					else
					{
                        await Navigation.PushAsync(new Inicio());
                    }
                   
                }
				
			}
			catch
			{
                return;
			}

		}

	}
}