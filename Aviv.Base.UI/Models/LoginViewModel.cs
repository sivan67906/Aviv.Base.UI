using System.ComponentModel.DataAnnotations;

namespace Aviv.Base.UI.Models.Authentication;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}

public class UserInfo
{
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
}

public class LoginResult
{
    public bool Successful { get; set; }
    public string? Error { get; set; }
    public string? RedirectUrl { get; set; }
}