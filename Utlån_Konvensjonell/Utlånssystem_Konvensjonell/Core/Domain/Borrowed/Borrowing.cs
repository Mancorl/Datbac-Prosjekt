using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utlånssystem_Konvensjonell.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace Utlånssystem_Konvensjonell.Core.Domain.Borrowed;

public class Borrowing
{

    protected Borrowing() { }

	public Borrowing(Guid userId, Guid boardGameId, bool active = true)
	{
		
		Id = Guid.NewGuid();
		UserId = userId;
		BoardGameId = boardGameId;
		Active = active;

	}
	public Guid Id { get; protected set; }
	public Guid? UserId {get; protected set;}
    public Guid? BoardGameId {get; protected set;}
    public bool Active {get; protected set;}

}
