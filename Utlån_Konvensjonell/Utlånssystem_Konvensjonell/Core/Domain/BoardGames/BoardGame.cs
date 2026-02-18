namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames;

public class BoardGame
{
	public BoardGame(string name, float edition = 1, int quantity = 1, Loanable loanable = Loanable.Yes)
	{
		Id = Guid.NewGuid();
		Name = name;
		Edition = edition;
		TotalQuantity = Quantity = quantity;
		IsLoanable = loanable;
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