using System;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment;
using UiS.Dat240.Lab3.Infrastructure.Data;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Services;
using UiS.Dat240.Lab3.Infrastructure.Data;
namespace UiS.Dat240.Lab3.Core.Domain.Cart.Handlers
{
    public class OrderPlacedHandler
    {
        private readonly FulfilmentContext _db;

        public OrderPlacedHandler(FulfilmentContext db)
        {
            _db = db;
        }

        public async Task OnOrderPlaced(object? sender, OrderingService.OrderPlacedEventArgs e)
        {
            Console.WriteLine($"{e.OrderId}, {e.SubTotal}");
            var reimb = new Reimbursement(e.OrderId, e.SubTotal, Guid.NewGuid());
            var offer = new Offer(Guid.NewGuid(), e.OrderId, reimb);
            await _db.AddRangeAsync(reimb, offer);
            await _db.SaveChangesAsync();
        }

    }
}
