using Microsoft.Maui.Controls;
using QuizAppFront.Pages;

namespace QuizAppFront
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RegisterPage());
        }
    }
}
