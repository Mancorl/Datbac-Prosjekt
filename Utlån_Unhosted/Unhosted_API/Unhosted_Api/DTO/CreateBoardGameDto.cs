namespace Unhosted_Api.DTO;
public class CreateBoardGameDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string ImagePath {get; set;} = "images/Default.jpg";
    public string Description { get; set; }
}