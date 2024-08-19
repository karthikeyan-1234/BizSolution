using DataAccessLayer.Contexts;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IPurchaseRepository : IBaseRepository<Purchase,PurchaseContext>
    {
        public IQueryable<PurchaseItemResult> GetPurchaseItemsForPurchase(int purchaseId);
    }

    public abstract class PurchaseExtensionRepo : BaseRepository<Purchase, PurchaseContext>
    {
        protected PurchaseExtensionRepo(PurchaseContext db) : base(db)
        {
        }

        public abstract IQueryable<PurchaseItemResult> GetPurchaseItemsForPurchase(int purchaseId);
    }


    public class PurchaseRepository : PurchaseExtensionRepo,IPurchaseRepository
    {
        PurchaseContext db;

        public PurchaseRepository(PurchaseContext db) : base(db)
        {
            this.db = db;
        }


        public override IQueryable<PurchaseItemResult> GetPurchaseItemsForPurchase(int purchaseId)
        {
            var result = from purchase in db.Purchases
                         join purchaseItem in db.PurchaseItems on purchase.id equals purchaseItem.purchase_id
                         select new PurchaseItemResult(
                              purchase.id,
                             purchaseItem.id,
                             purchase.CreatedOn
                         );
            return result;
        }
    }
}
