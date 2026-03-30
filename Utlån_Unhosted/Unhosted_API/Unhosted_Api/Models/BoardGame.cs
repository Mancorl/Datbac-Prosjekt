using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unhosted_Api.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace Unhosted_Api.Models;

public class BoardGame
{
	public BoardGame(string name, int quantity = 1, bool loanable = true, string imagePath = "images/Default.jpg", string description = "Placeholder")
	{
		Id = Guid.NewGuid();
		Name = name;
		//Edition = edition;
		TotalQuantity = Quantity = quantity;
		Loanable = loanable;
		ImagePath = imagePath;
		Description = description;
	}


	public string Name { get; protected set; }
	public float Edition {get; protected set;}
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