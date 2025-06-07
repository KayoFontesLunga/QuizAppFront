using QuizAppFront.Services;
using QuizAppFront.Validations;

namespace QuizAppFront.Pages;

public partial class RegisterPage : ContentPage
{
	private readonly UserService _userService;
	private readonly IValidator _validator;
	public RegisterPage(UserService userService, IValidator validator)
	{
		InitializeComponent();

        _userService = userService;
		_validator = validator;
	}
	private async void OnRegisterClicked(object sender, EventArgs e)
	{
		if(await _validator.Validate(EntName.Text, EntEmail.Text, EntPassword.Text))
		{
            var response = await _userService.UserRegister(EntName.Text, EntEmail.Text, EntPassword.Text);
            if (!response.HasError)
            {
                await DisplayAlert("Sucesso", "Cadastro realizado com sucesso", "OK");
                await Navigation.PushAsync(new LoginPage(_userService, _validator));
            }
            else
            {
                await DisplayAlert("Erro", "Cadastro falhou", "OK");
            }
        }
		else
        {
            string errorMessage = "";
            errorMessage += _validator.NameError != null ? $"\n- {_validator.NameError}" : string.Empty;
            errorMessage += _validator.EmailError != null ? $"\n- {_validator.EmailError}" : string.Empty;
            errorMessage += _validator.PasswordError != null ? $"\n- {_validator.PasswordError}" : string.Empty;
            await DisplayAlert("Erro", errorMessage, "OK");
        }
	}
	private async void OnGoForLoginClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new LoginPage(_userService, _validator));
	}
}