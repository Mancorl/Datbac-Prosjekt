namespace Utlånssystem_Konvensjonell.Core.Domain.Account;

public class Name
{
	public Name(string firstname, string lastname, Guid id){
		First = firstname;
		Last = lastname;
		Id = id;
	}

	public string First { get; protected set; }
	public string Last { get; protected set; }
	public Guid Id {get; protected set;}


}

