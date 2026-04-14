

using System.Data.Common;

namespace Unhosted_Api.Models;


public class RegisteredUser
{
    public Guid Id { get; set; }
    
    public string Hashid { get; protected set; }
    public string Password { get; set; }
    public Rights IsAdmin { get; set; }
    public bool IsAuthorized { get; set; }

    private RegisteredUser() { }

    public RegisteredUser(Guid id, string hashid, string password)
    {
        Id = id;
        Hashid = BCrypt.Net.BCrypt.HashPassword(hashid);
        Password = password;
        IsAdmin = Rights.User;
        IsAuthorized = true;
    }
}
public enum Rights
    {
        unauthorized,
        User,
        Admin
    }
