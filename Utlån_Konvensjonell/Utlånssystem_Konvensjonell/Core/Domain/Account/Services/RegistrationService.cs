using Utlånssystem_Konvensjonell.Core.Domain.Account.Events;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Handlers;
namespace Utlånssystem_Konvensjonell.Core.Domain.Account.Services;
public class RegistrationService
{
    public event EventHandler<RegisteredEventArgs>? Registered;

    public async Task RegisterAsync(string email, string password, string firstName, string lastName)
    {
        // domain logic here

        Registered?.Invoke(this, new RegisteredEventArgs(
            email, password, firstName, lastName
        ));
    }
}