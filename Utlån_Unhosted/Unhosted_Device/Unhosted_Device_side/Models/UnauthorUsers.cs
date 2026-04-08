namespace Unhosted_Device_side.Models;

public class UnauthorUsers
{
    public Guid Id { get; set; }
    public string First { get; set; } = "";
    public string Last { get; set; } = "";
    public string Email { get; set; } = "";
    public bool IsAuthorized { get; set; }
}