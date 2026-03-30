namespace Unhosted_Api.DTO;
public class CreateBoardGameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string ImagePath {get; set;} = "images/Default.jpg";
    public string Description { get; set; }

    public IFormFile? Image { get; set; }
}