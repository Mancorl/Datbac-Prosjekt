using System;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;

public class AddGameEventArgs : EventArgs
{
    public AddGameEventArgs(string gameTitle, int quantity)
    {
        GameTitle = gameTitle;
        Qmail = quantity;
    }

    public string GameTitle { get; }
    public int Quantity { get; }
}