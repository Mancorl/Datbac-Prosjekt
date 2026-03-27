using System.Text.RegularExpressions;
using SharedKernel;

namespace Unhosted_Device_side.Models;

public class Game
{

    public Game() { }
    
	public Game(string name, int quantity = 1, bool loanable = true, string imagePath = "images/Default.jpg", string description = "Placeholder")
	{
		Id = Guid.NewGuid();
		Name = name;
		TotalQuantity = Quantity = quantity;
		Loanable = loanable;
		ImagePath = imagePath;
		Description = description;
	}


	public string Name { get; protected set; }
	public Guid Id{get;protected set;}
	public int Quantity{get; set;}
	public int TotalQuantity{get; set;}
	public bool Loanable {get;protected set;}
	public string ImagePath { get; protected set; }
	public string Description { get; protected set; }


	 public void Edit(string name, int quantity, string description, string? imagePath = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        Name = name;
        Quantity = quantity;
        Description = description;

        if (!string.IsNullOrWhiteSpace(imagePath))
        {
            ImagePath = imagePath;
        }
    }


	 public void RentOne()
    {
        if (!Loanable)
            throw new InvalidOperationException("This game cannot be borrowed.");

        if (Quantity <= 0)
            throw new InvalidOperationException("No copies available.");

        Quantity--;
    }


	


}