namespace Unhosted_Api.DTO;
public class CreateBoardGameDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string ImagePath {get; set;}
    public string Description { get; set; }
}