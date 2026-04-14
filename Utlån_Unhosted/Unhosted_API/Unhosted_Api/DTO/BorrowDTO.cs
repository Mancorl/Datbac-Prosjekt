namespace Unhosted_Api.DTO;

public class BorrowDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public string? Email { get; set; }
    public bool Active { get; set; }
    public string? Password { get; set; }
}