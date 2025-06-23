using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovilAsis.Services
{


    public class Service
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase = "https://Manuel05.bsite.net/api";
        
        public Service()
        {
            _httpClient = new HttpClient();

        }
        public async Task<int> GetValor(string endpoint)
        {
            var respuesta = await _httpClient.GetStringAsync($"{_urlBase}/{endpoint}");
            var valor = JsonSerializer.Deserialize<int>(respuesta);
            return valor;
        }
        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            var respuesta = await _httpClient.GetStringAsync($"{_urlBase}/{endpoint}");
            var lista = JsonSerializer.Deserialize<List<T>>(respuesta);
            return lista;
        }

        public async Task<(bool Success, string Message)> PostAsync(string endpoint, object data)
        {
            // Serializa el objeto `data` a JSON
            var datos = JsonSerializer.Serialize(data);

            // Crea el contenido HTTP con el JSON serializado
            var content = new StringContent(datos, Encoding.UTF8, "application/json");

            // Realiza la solicitud POST
            var response = await _httpClient.PostAsync($"{_urlBase}/{endpoint}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            // Asegura que la respuesta sea exitosa
            response.EnsureSuccessStatusCode();
            return (response.IsSuccessStatusCode, responseContent);
        }

        public async Task<(bool Success, T Result)> PostObjetoAsync<T>(string endpoint, object data)
        {
            var datos = JsonSerializer.Serialize(data);

            // Crea el contenido HTTP con el JSON serializado
            var content = new StringContent(datos, Encoding.UTF8, "application/json");

            // Realiza la solicitud POST
            var response = await _httpClient.PostAsync($"{_urlBase}/{endpoint}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<T>(responseContent);

            return (response.IsSuccessStatusCode, result);
        }

        public async Task<(bool Success, string Message)> PutAsync(string endpoint, object data)
        {
            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false
                };

                var datos = JsonSerializer.Serialize(data, jsonOptions);

                // Crea el contenido HTTP con el JSON serializado
                using var content = new StringContent(datos, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT
                var response = await _httpClient.PutAsync($"{_urlBase}/{endpoint}", content);

                // Lee el contenido de la respuesta
                var responseContent = await response.Content.ReadAsStringAsync();

                // Verifica el estado de la respuesta
                if (!response.IsSuccessStatusCode)
                {
                    return (false, $"Error: {response.StatusCode} - {responseContent}");
                }

                return (true, responseContent);
            }
            catch (HttpRequestException ex)
            {
                return (false, $"Error de conexión: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return (false, $"Error al serializar los datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteAsync(string endpoint)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{_urlBase}/{endpoint}");

                // Configuración consistente con otros métodos
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Si necesitas autenticación (igual que en PostAsync/PutAsync)
                // request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "tu-token");

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return (false, $"Error: {response.StatusCode} - {responseContent}");
                }

                return (true, responseContent);
            }
            catch (HttpRequestException ex)
            {
                return (false, $"Error de conexión: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }
        public async Task<(bool Success, string Result)> DeleteAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_urlBase}/{endpoint}");

                if (!response.IsSuccessStatusCode)
                {
                    return (false, $"Error: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                return (true, content);
            }
            catch (Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }




        //public async Task<List<T>> ObtenerLista<T>(string endPoint)
        //{
        //    peticionHttp.PedirComunicacion(endPoint, MetodoHTTP.GET, TipoContenido.JSON);
        //    string jsonResponse = await peticionHttp.ObtenerJson();

        //    List<T> lista = JsonConvertidor.Json_ListaObjeto<T>(jsonResponse);
        //    return lista;
        //}

        //public async Task<T> ObtenerValor<T>(string endPoint)
        //{
        //    peticionHttp.PedirComunicacion(endPoint, MetodoHTTP.GET, TipoContenido.JSON);
        //    string jsonResponse = await peticionHttp.ObtenerJson();

        //    T lista = JsonConvertidor.Json_Objeto<T>(jsonResponse);
        //    return lista;
        //}

        //public async Task<string> EnviarDatos(string endPoint, object data)
        //{
        //    peticionHttp.PedirComunicacion(endPoint, MetodoHTTP.POST, TipoContenido.JSON);
        //    await peticionHttp.enviarDatosAsync(JsonConvertidor.Objeto_Json(data));
        //    string json = await peticionHttp.ObtenerJson();
        //    return json;
        //}

        //public async Task<string> EditarDatos(string endPoint, object data)
        //{
        //    peticionHttp.PedirComunicacion(endPoint, MetodoHTTP.PUT, TipoContenido.JSON);
        //    await peticionHttp.enviarDatosAsync(JsonConvertidor.Objeto_Json(data));
        //    string respuesta = await peticionHttp.ObtenerJson();
        //    return respuesta;
        //}

        //public async Task<string> EliminarDatos(string endPoint)
        //{
        //    peticionHttp.PedirComunicacion(endPoint, MetodoHTTP.DELETE, TipoContenido.JSON);
        //    string respuesta = await peticionHttp.ObtenerJson();
        //    return respuesta;
        //}
        //public async Task<List<T>> GetAsync<T>(string endpoint)
        //{
        //    var respuesta = await _httpClient.GetStringAsync($"{_urlBase}/{endpoint}");
        //    var lista = JsonSerializer.Deserialize<List<T>>(respuesta);
        //    return lista;
        //}


    }
    
}

