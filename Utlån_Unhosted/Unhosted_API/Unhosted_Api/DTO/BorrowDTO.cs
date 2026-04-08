namespace Unhosted_Api.DTO;

public class BorrowDto
{
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
}

public class BorrowOUTDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public bool Active { get; set; }
}