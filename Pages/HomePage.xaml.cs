using System.Text.Json;

namespace QuizAppFront.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://seu-backend.com.br/");
        // Adicione o token se precisar
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "SEU_TOKEN");

        LoadQuizzes();
    }

    private async void LoadQuizzes()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Quiz/public?page=1&pageSize=20");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<PaginatedResult<QuizDTO>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            QuizCollectionView.ItemsSource = result.Items;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Não foi possível carregar os quizzes: " + ex.Message, "OK");
        }
    }
}