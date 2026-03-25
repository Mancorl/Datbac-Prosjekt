using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utlånssystem_Konvensjonell.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace Unhosted_Device_side.Data.Tables;

public class UserClass
{
	public UserClass(string email, string password, string first, string last)
	{
		
		Id = Guid.NewGuid();
		Email = email;
		Password = password;
		First = first;
		Last = last;
		IsAuthorized = false;

	}
	public Guid Id { get; protected set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public Permission Permission { get; set; }
	public string First { get; protected set; }
	public string Last { get; protected set; }
	public bool IsAuthorized {get; set;}


	public void Authorize()
	{
		IsAuthorized = true;
	}



}


public class UserFirstNameValidator : IValidator<UserClass>
{
	public (bool, string) IsValid(UserClass user)
	{
		_ = user ?? throw new ArgumentNullException(nameof(user), "Cannot validate a null object");
		if (string.IsNullOrWhiteSpace(user.First)) return (false, $"{nameof(user.First)}name cannot be empty.");
		return (true, "");
	}
}

public class UserLastNameValidator : IValidator<UserClass>
{
	public (bool, string) IsValid(UserClass user)
	{
		_ = user ?? throw new ArgumentNullException(nameof(user), "Cannot validate a null object");
		if (string.IsNullOrWhiteSpace(user.Last)) return (false, $"{nameof(user.Last)}name cannot be empty.");
		return (true, "");
	}
}

public class UserEmailValidator : IValidator<UserClass>
{
	public (bool, string) IsValid(UserClass user)
	{
		_ = user ?? throw new ArgumentNullException(nameof(user), "Cannot validate a null object");
		string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
		if (string.IsNullOrWhiteSpace(user.Email)) return (false, $"{nameof(user.Email)} cannot be empty.");
		else if (!Regex.Match(user.Email, pattern).Success) return (false, $"{nameof(user.Email)} is not valid.");
		return (true, "");
	}
}

