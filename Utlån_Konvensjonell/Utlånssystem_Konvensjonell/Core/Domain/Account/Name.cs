namespace Utlånssystem_Konvensjonell.Core.Domain.Account;

public class Name
{
	public Name(string firstname, string lastname)
	{
		First = firstname;
        Last = lastname;
	}
	public string First { get; protected set; }
	public string Last { get; protected set; }


}