using SQLite;

namespace Unhosted_Device_side.Data.Tables;

[Table("Users")]
public class UserEntity
{
    [PrimaryKey]
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int Permission { get; set; }

    public string First { get; set; } = string.Empty;

    public string Last { get; set; } = string.Empty;

    public bool IsAuthorized { get; set; }
}