

namespace Unhosted_Api.Models;


public class RegisteredUser
{
    public Guid Id { get; set; }
    
    public string Hashid { get; protected set; }
    public string Password { get; set; }
    public Rights IsAdmin { get; set; }

    public RegisteredUser(string hashid, string password)
    {
        Id = Guid.NewGuid();
        Hashid = hashid;
        Password = password;
        IsAdmin = Rights.User;
    }
}
public enum Rights
    {
        unauthorized,
        User,
        Admin
    }