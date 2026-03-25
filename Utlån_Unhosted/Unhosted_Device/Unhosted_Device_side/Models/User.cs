using System.Text.RegularExpressions;
using SharedKernel;

namespace Unhosted_Device_side.Models;

public class User
{
    public User() { }

    public User(string email, string password, string first, string last)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        First = first;
        Last = last;
        IsAuthorized = false;
    }

    public Guid Id { get; protected set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Permission Permission { get; set; }
    public string First { get; protected set; } = string.Empty;
    public string Last { get; protected set; } = string.Empty;
    public bool IsAuthorized { get; set; }

    public void Authorize()
    {
        IsAuthorized = true;
    }
}

public enum Permission
{
    Lender,
    Admin
}