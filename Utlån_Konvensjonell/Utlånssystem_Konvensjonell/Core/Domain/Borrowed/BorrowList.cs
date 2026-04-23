namespace Utlånssystem_Konvensjonell.Pages;

public class BorrowList
{
    public Guid BorrowId { get; set; }
    public Guid GameId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = "images/Default.jpg";
}