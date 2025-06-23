using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MovilAsis
{
    static class Global
    {
        public static void GuardarUsuario(string matricula, string contraseña, int id)
        {
            Preferences.Set("Id", id);
            Preferences.Set("Matricula", matricula);
            Preferences.Set("Contraseña", contraseña);
        }

        public static void CerrarSesion()
        {
            Preferences.Remove("Id");
            Preferences.Remove("Matricula");
            Preferences.Remove("Contraseña");

        }

        public static DateTime ConvertirFecha(int fecha)
        {
            DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(fecha).UtcDateTime;
            DateTime mexicoTime = utcTime.AddHours(-6);
            return DateTime.SpecifyKind(mexicoTime, DateTimeKind.Unspecified);
        }

        public static async void AbrirMapa(double LatitudEntrada, double LongitudEntrada)
        {
            var location = new Location(LatitudEntrada,LongitudEntrada);
            var options = new MapLaunchOptions
            {
                Name = "Ubicación de registro",
                NavigationMode = NavigationMode.None
            };

            try
            {
                await Map.Default.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }


        public static bool VerificarUbicacionEnRango(double latitudActual, double longitudActual)
        {
            // Ubicación central (punto de referencia)
            var ubicacionCentral = new Location(19.713221, -98.979263);

            // Ubicación actual a verificar
            var ubicacionActual = new Location(latitudActual, longitudActual);

            // Radio en metros (100 metros)
            const double radioMetros = 100;

            // Calcular distancia
            double distanciaMetros = ubicacionCentral.CalculateDistance(ubicacionActual, DistanceUnits.Kilometers) * 1000;

            // Verificar si está dentro del rango
            return distanciaMetros <= radioMetros;
        }

    }

    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && parameter is string colors)
            {
                var colorParts = colors.Split('|');
                if (colorParts.Length == 2)
                {
                    return boolValue ? colorParts[0] : colorParts[1];
                }
            }
            return "#000000"; // Color por defecto en caso de error
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
