using System.Text.RegularExpressions;
using SharedKernel;

namespace Unhosted_Device_side.Models;

public class Rent
{
    public Rent() { }

    public Rent(Guid userId, Guid gameId, bool active = true)
    {
        Id = Guid.NewGuid();
		UserId = userId;
		GameId = gameId;
		Active = active;

    }

    public Guid Id { get; protected set; }
	public Guid UserId {get; protected set;}
    public Guid GameId {get; protected set;}
    public bool Active {get; protected set;}


}
