using QuizAppFront.Services;
using QuizAppFront.Validations;

namespace QuizAppFront.Pages;

public partial class LoginPage : ContentPage
{
	private readonly UserService _userService;
    private readonly IValidator _validator;
	public LoginPage(UserService userService, IValidator validator)
	{
		InitializeComponent();

        _userService = userService;
        _validator = validator;
	}

    private async void loginClicked(object sender, EventArgs e)
    {
		if (string.IsNullOrEmpty(EntEmail.Text))
		{
			await DisplayAlert("Erro", "Digite um email", "OK");
            return;
		}
		if (string.IsNullOrEmpty(EntPassword.Text))
        {
            await DisplayAlert("Erro", "Digite uma senha", "OK");
            return;
        }
		var response = await _userService.Login(EntEmail.Text, EntPassword.Text);
        if (!response.HasError)
        {
            Application.Current!.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Erro", "Login falhou", "OK");
        }
    }
    private async void RegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage(_userService, _validator));
    }  
}