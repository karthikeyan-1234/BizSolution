using DataAccessLayer.Repositories;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public record PurchaseItemResult(int purchaseId, int purchaseItemId, DateTimeOffset purchaseDate);


    public interface IPurchaseCustomRepository<T>
    {
        IQueryable<PurchaseItemResult> GetPurchaseItemsForPurchase(int purchaseId);
    }

}
