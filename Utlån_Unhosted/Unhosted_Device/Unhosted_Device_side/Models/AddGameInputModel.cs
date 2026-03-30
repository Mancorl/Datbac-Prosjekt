 
 namespace Unhosted_Device_side.Models;
 
 public class AddGameInputModel
    {
        public string GameTitle { get; set; } = string.Empty;
        public string GameDescription { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public bool Loanable { get; set; } = true;
    }