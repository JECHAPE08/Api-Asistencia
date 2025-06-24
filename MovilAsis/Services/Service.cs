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
            var datos = JsonSerializer.Serialize(data);

            var content = new StringContent(datos, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_urlBase}/{endpoint}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            return (response.IsSuccessStatusCode, responseContent);
        }

        public async Task<(bool Success, T Result)> PostObjetoAsync<T>(string endpoint, object data)
        {
            var datos = JsonSerializer.Serialize(data);

            var content = new StringContent(datos, Encoding.UTF8, "application/json");

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

                using var content = new StringContent(datos, Encoding.UTF8, "application/json");


                var response = await _httpClient.PutAsync($"{_urlBase}/{endpoint}", content);

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
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
    }
    
}

