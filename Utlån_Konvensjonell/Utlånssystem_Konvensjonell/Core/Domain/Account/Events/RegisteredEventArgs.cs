using System;

namespace Utlånssystem_Konvensjonell.Core.Domain.Account.Events
{
    public class RegisteredEventArgs : EventArgs
    {
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public RegisteredEventArgs(
            string email,
            string password,
            string firstName,
            string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}