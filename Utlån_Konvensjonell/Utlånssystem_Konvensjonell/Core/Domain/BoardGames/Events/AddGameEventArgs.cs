using System;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;

public class AddGameEventArgs : EventArgs
{
    public AddGameEventArgs(string gameTitle, int quantity, bool loanable, string imagePath, string gameDescription)
    {
        GameTitle = gameTitle;
        Quantity = quantity;
        Loanable = loanable;
        ImagePath = imagePath;
        GameDescription = gameDescription;
    }

    public string GameTitle { get; set;}
    public int Quantity { get; set;}
    public bool Loanable { get; set; }
    public string ImagePath { get; set; }
    public string GameDescription { get; set; }
    
}