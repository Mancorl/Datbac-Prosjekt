using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unhosted_Api.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace Unhosted_Api.Models;

public class Borrowing
{

    protected Borrowing() { }

	public Borrowing(Guid userId, Guid boardGameId, string email, bool active)
	{
		
		Id = Guid.NewGuid();
		UserId = userId;
		BoardGameId = boardGameId;
		Email = email;
		Active = active;
	}

	public Guid Id { get; protected set; }
	public Guid? UserId {get; protected set;}
    public Guid? BoardGameId {get; protected set;}
    public string Email {get; protected set;}
	public bool Active { get; set; }

}
