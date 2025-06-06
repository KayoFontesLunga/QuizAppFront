namespace QuizAppFront.Pages;

public partial class LoginPage : ContentPage
{
	private readonly HttpClient _httpClient;
	public LoginPage(HttpClient httpClient)
	{
		InitializeComponent();

        _httpClient = httpClient;
	}
}