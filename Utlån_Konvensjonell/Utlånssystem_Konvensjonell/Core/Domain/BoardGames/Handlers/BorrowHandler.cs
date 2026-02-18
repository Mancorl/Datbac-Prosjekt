using System;
using System.Threading.Tasks;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.Account;

namespace Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers
{
    public class BorrowHandler
    {
        private readonly BoardGameContext _db;

        public BorrowHandler(BoardGameContext db)
        {
            _db = db;
        }

        public async Task OnBorrow(object? sender)//, BorrowingService.BorrowEventArgs e)
        {
            Console.WriteLine("Henlo");//$"{e.Name}, {e.Id}");
            //var reimb = new Reimbursement(e.OrderId, e.SubTotal, Guid.NewGuid());
            //var offer = new Offer(Guid.NewGuid(), e.OrderId, reimb);
            //await _db.AddRangeAsync(reimb, offer);
            //await _db.SaveChangesAsync();
        }

    }
}
