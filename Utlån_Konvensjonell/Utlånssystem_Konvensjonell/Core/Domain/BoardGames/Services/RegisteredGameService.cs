using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers;
namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Services;
public class RegisteredGameService
{
    public event EventHandler<AddGameEventArgs>? Registered;

    public async Task RegisterAsync(string GameTitle, int Quantity, bool Loanable, string ImagePath, string GameDescription)
    {
        // domain logic here

        Registered?.Invoke(this, new AddGameEventArgs(
            GameTitle, Quantity, Loanable, ImagePath, GameDescription
        ));
    }
}
