using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppFront.Services;
public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
}
