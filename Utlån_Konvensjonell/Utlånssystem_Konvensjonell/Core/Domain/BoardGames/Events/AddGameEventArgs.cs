using System;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;

public class AddGameEventArgs : EventArgs
{
    public AddGameEventArgs(string gameTitle, int quantity, bool loanable, string imagePath)
    {
        GameTitle = gameTitle;
        Quantity = quantity;
        Loanable = loanable;
        ImagePath = imagePath;
    }

    public string GameTitle { get; set;}
    public int Quantity { get; set;}
    public bool Loanable { get; set; }
    public string ImagePath { get; set; }
}