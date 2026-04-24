namespace Utlånssystem_Konvensjonell.Pages;

public class CheckGameList
{
    public Guid BorrowId { get; set; }
    public Guid GameId { get; set; }

    public string GameName { get; set; } = "";
    public string Description { get; set; } = "";
    public string ImagePath { get; set; } = "";

    public string UserName { get; set; } = "";
    public string UserEmail { get; set; } = "";
}