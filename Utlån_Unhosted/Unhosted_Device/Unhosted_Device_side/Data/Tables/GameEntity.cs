using SQLite;

namespace Unhosted_Device_side.Data.Tables;

[Table("Games")]
public class GameEntity
{
    [PrimaryKey]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public int TotalQuantity { get; set; }

    public bool Loanable { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}