using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utlånssystem_Konvensjonell.SharedKernel;

namespace Utlånssystem_Konvensjonell.Core.Domain.Account;

public class User
{
	public User(Guid id, string email, string password)
	{
		Id = id;
		Email = email;
		Password = password;
	}
	public Guid Id { get; protected set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public Permission Permission { get; set; }
	required public Name Name { get; set; }


}


public class UserFirstNameValidator : IValidator<User>
{
	public (bool, string) IsValid(User user)
	{
		_ = user ?? throw new ArgumentNullException(nameof(user), "Cannot validate a null object");
		if (string.IsNullOrWhiteSpace(user.Name.First)) return (false, $"{nameof(user.Name.First)}name cannot be empty.");
		return (true, "");
	}
}

public class UserLastNameValidator : IValidator<User>
{
	public (bool, string) IsValid(User user)
	{
		_ = user ?? throw new ArgumentNullException(nameof(user), "Cannot validate a null object");
		if (string.IsNullOrWhiteSpace(user.Name.Last)) return (false, $"{nameof(user.Name.Last)}name cannot be empty.");
		return (true, "");
	}
}

public class UserEmailValidator : IValidator<User>
{
	public (bool, string) IsValid(User user)
	{
		_ = user ?? throw new ArgumentNullException(nameof(user), "Cannot validate a null object");
		string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
		if (string.IsNullOrWhiteSpace(user.Email)) return (false, $"{nameof(user.Email)} cannot be empty.");
		else if (!Regex.Match(user.Email, pattern).Success) return (false, $"{nameof(user.Email)} is not valid.");
		return (true, "");
	}
}