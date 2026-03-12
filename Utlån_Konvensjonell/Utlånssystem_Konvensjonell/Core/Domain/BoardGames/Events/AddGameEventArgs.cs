using System;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;

public class AddGameEventArgs : EventArgs
{
    public AddGameEventArgs(string gameTitle, int quantity, bool loanable)
    {
        GameTitle = gameTitle;
        Quantity = quantity;
        Loanable = loanable;
    }

    public string GameTitle { get; }
    public int Quantity { get; }
    public bool Loanable { get; }
}