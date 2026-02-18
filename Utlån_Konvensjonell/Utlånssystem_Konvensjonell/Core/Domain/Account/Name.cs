namespace Utlånssystem_Konvensjonell.Core.Domain.Account;

public class Name
{
	public Name(string first, string last){
		First = first;
		Last = last;

	}
	private Name(){}

	public string First { get; protected set; }
	public string Last { get; protected set; }


}

