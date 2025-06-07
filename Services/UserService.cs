using Microsoft.Extensions.Logging;
using QuizAppFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace QuizAppFront.Services;
public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress = "https://quizapp-g1io.onrender.com";
    private readonly ILogger<UserService> _logger;
    JsonSerializerOptions _jsonSerializerOptions;

    public UserService(HttpClient httpClient, ILogger<UserService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<ApiResponse<bool>> UserRegister(string name, string email, string password)
    {
        try
        {
            var register = new UserRegisterModel()
            {
                Name  = name,
                Email = email,
                Password = password
            };
            var json = JsonSerializer.Serialize(register, _jsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostRequest("/api/User/register", content);
            var resultContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Erro ao registrar usuário: {response.StatusCode}");
                return new ApiResponse<bool>
                {
                    ErrorMessage = "Erro ao registrar usuário"
                };
            }
            return new ApiResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao registrar usuário: {ex.Message}");
            return new ApiResponse<bool> { ErrorMessage = "Erro ao registrar usuário" };
        }
    }
    public async Task<ApiResponse<bool>> Login(string email, string password)
    {
        try
        {
            var login = new UserLoginModel()
            {
                Email = email,
                Password = password
            };
            var json = JsonSerializer.Serialize(login, _jsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostRequest("/api/User/login", content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Erro ao fazer login: {response.StatusCode}");
                return new ApiResponse<bool>
                {
                    ErrorMessage = "Erro ao registrar usuário"
                };
            }       
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TokenModel>(jsonResult, _jsonSerializerOptions);
            Preferences.Set("token", result!.Token);
            return new ApiResponse<bool> { Data = true };

        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao fazer login: {ex.Message}");
            return new ApiResponse<bool> { ErrorMessage = "Erro ao registrar usuário" };
        }
    }
    private async Task<HttpResponseMessage> PostRequest(string uri, HttpContent content)
    {
        var urlAddress = _baseAddress + uri;
        try
        {
            var result = await _httpClient.PostAsync(urlAddress, content);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao registrar usuário: {ex.Message}");
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}