
using SQLite;

namespace Unhosted_Device_side.Data.Tables;

[Table("Rented")]
public class RentEntity
{
    [PrimaryKey]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid BoardGameId { get; set; }

    public string Email {get; set;} = string.Empty;


    public bool Active { get; set; }

}