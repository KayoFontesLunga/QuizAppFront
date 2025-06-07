using Microsoft.Maui.Controls;
using QuizAppFront.Pages;
using QuizAppFront.Services;
using QuizAppFront.Validations;

namespace QuizAppFront
{
    public partial class App : Application
    {
        private readonly UserService _userService;
        private readonly IValidator _validator;
        public App(UserService userService, IValidator validator)
        {
            InitializeComponent();
            _userService = userService;
            _validator = validator;
            SetMainPage();

        }
        private void SetMainPage()
        {

            var token = Preferences.Get("token", string.Empty);
            if (string.IsNullOrEmpty(token))
            {
                MainPage = new NavigationPage(new RegisterPage(_userService, _validator));
                return;
            }
            MainPage = new AppShell();
        }
    }
}