using DataAccessLayer.Contexts;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        IPurchaseRepository repo;

        public PurchaseController(IPurchaseRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("AddPurchase")]
        public async Task<IActionResult> AddPurchaseAsync(Purchase newPurchase)
        {

            var purchase = (repo.GetPurchaseItemsForPurchase(1)).ToList();

            var result = await repo.AddAsync(newPurchase);
            await repo.SaveChangesAsync();
            return Ok(result);
        }

        [HttpGet("GetallPurchase")]
        public async Task<IActionResult> GetallPurchasesAsync()
        {
            return Ok(await repo.GetAllAsync());
        }
    }
}
