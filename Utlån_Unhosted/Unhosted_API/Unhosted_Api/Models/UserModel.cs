using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unhosted_Api.SharedKernel;
using System.Diagnostics.CodeAnalysis;
using BCrypt.Net;
namespace Unhosted_Api.Models;

public class User
{
	public User(string email, string password, string first, string last)
{
    Id = Guid.NewGuid();
    Email = email;
    Password = BCrypt.Net.BCrypt.HashPassword(password);
    First = first;
    Last = last;
    IsAuthorized = false;
}
	public Guid Id { get; protected set; }
	public string Email { get; set; }
	public string Password { get;protected set; }
	public Permission Permission { get; set; }
	public string First { get; protected set; }
	public string Last { get; protected set; }
	public bool IsAuthorized {get; set;}


	public void Authorize()
	{
		IsAuthorized = true;
	}
    



}
public enum Permission
{
    Lender,
    Admin
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




