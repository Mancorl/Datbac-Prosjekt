using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utlånssystem_Konvensjonell.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames;

public class BoardGame
{
	public BoardGame(string name, int quantity = 1, bool loanable = true )
	{
		Id = Guid.NewGuid();
		Name = name;
		//Edition = edition;
		TotalQuantity = Quantity = quantity;
		if(loanable){
			IsLoanable = Loanable.Yes;
		}else{
			IsLoanable = Loanable.No;
		}
	}


	public string Name { get; protected set; }
	public float Edition {get; protected set;}
	public Guid Id{get;protected set;}
	public int Quantity{get;protected set;}
	public int TotalQuantity{get;protected set;}
	public Loanable IsLoanable{get; protected set;}
	public enum Loanable
	{
		Yes,
		No
	}



}