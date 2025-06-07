using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuizAppFront.Validations;
internal class Validator : IValidator
{
    public string NameError { get; set; } = "";
    public string EmailError { get; set; } = "";
    public string PasswordError { get; set; } = "";

    private const string VoidNameErrorMsg = "Por favor, informe o seu nome";
    private const string InvalidNameErrorMsg = "Por favor, informe um nome válido";
    private const string VoidEmailErrorMsg = "Por favor, informe o seu email";
    private const string InvalidEmailErrorMsg = "Por favor, informe um email valido";
    private const string VoidPasswordErrorMsg = "Por favor, informe a sua senha";
    private const string InvalidPasswordErrorMsg = "Por favor, informe uma senha valida, a senha deve conter pelo menos 8 caracteres e conter pelo menos uma letra maiuscula e uma letra minuscula";


    public Task<bool> Validate(string name, string email, string password)
    {
        var isValidName =ValidateName(name);
        var isValidEmail = ValidateEmail(email);
        var isValidPassword = ValidatePassword(password);
        return Task.FromResult(isValidName && isValidEmail && isValidPassword);
    }
    private bool ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            NameError = VoidNameErrorMsg;
            return false;
        }
        if(name.Length < 3)
        {
            NameError = InvalidNameErrorMsg;
            return false;
        }
        NameError = "";
        return true;
    }
    private bool ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            EmailError = VoidEmailErrorMsg;
            return false;
        }
        if(!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            EmailError = InvalidEmailErrorMsg;
            return false;
        }
        EmailError = "";
        return true;
    }
    private bool ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            PasswordError = VoidPasswordErrorMsg;
            return false;
        }
        if (password.Length < 9 || !Regex.IsMatch(password, @"[a-zA-z]") || !Regex.IsMatch(password, @"\d"))
        {
            PasswordError = InvalidPasswordErrorMsg;
            return false;
        }
        PasswordError = "";
        return true;
    }
}
