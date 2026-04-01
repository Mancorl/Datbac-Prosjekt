namespace Unhosted_Api.DTO;
public class UserDto
{
    public Guid Id { get; set; }
    public string first { get; set; }
    public string last { get; set; }
    
    public string Email { get; set; }
    public string Password { get; set; }
}