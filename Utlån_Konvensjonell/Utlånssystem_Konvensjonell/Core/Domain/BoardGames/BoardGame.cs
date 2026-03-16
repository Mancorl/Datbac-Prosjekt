using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utlånssystem_Konvensjonell.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames;

public class BoardGame
{
	public BoardGame(string name, int quantity = 1, bool loanable = true, string imagePath = "images/Default.jpg")
	{
		Id = Guid.NewGuid();
		Name = name;
		//Edition = edition;
		TotalQuantity = Quantity = quantity;
		Loanable = loanable;
		ImagePath = imagePath;
	}


	public string Name { get; protected set; }
	public float Edition {get; protected set;}
	public Guid Id{get;protected set;}
	public int Quantity{get;protected set;}
	public int TotalQuantity{get;protected set;}
	public bool Loanable {get;protected set;}
	public string ImagePath { get; protected set; }

	


}