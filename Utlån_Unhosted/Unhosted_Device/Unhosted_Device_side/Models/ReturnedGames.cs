namespace Unhosted_Device_side.Models;

public class ReturnedGames
{
    public Guid BorrowId { get; set; }
    public Guid GameId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImagePath { get; set; }

    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }
}