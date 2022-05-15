using ECommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using ECommerce.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public ShoppingCardViewModel ShoppingCardViewModel { get; set; }

        public CartsController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCardViewModel = new ShoppingCardViewModel()
            {
                OrderHeader = new Models.OrderHeader(),
                ListCart = _applicationDbContext.ShoppingCarts.Where(u => u.ApplicationUserId == claim.Value).Include(u => u.Product)
            };
            ShoppingCardViewModel.OrderHeader.OrderTotal = 0;
            ShoppingCardViewModel.OrderHeader.ApplicationUser = _applicationDbContext.ApplicationUsers.FirstOrDefault(u => u.Id == claim.Value);
            foreach (var item in ShoppingCardViewModel.ListCart)
            {
                ShoppingCardViewModel.OrderHeader.OrderTotal += (item.Count * item.Product.Price);
            }
            return View(ShoppingCardViewModel);
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Add(int cardId)
        {
            var cart = _applicationDbContext.ShoppingCarts.FirstOrDefault(i => i.Id == cardId);
            cart.Count += 1;
            _applicationDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Decrease(int cardId)
        {
            var cart = _applicationDbContext.ShoppingCarts.FirstOrDefault(i => i.Id == cardId);
            if (cart.Count == 1)
            {
                var count = _applicationDbContext.ShoppingCarts.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
                _applicationDbContext.ShoppingCarts.Remove(cart);
                _applicationDbContext.SaveChanges();
                HttpContext.Session.SetInt32(Other.SessionShoppingCart, count - 1);
            }
            else
            {
                cart.Count -= 1;
                _applicationDbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cardId)
        {
            var cart = _applicationDbContext.ShoppingCarts.FirstOrDefault(i => i.Id == cardId);
            var count = _applicationDbContext.ShoppingCarts.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
            _applicationDbContext.ShoppingCarts.Remove(cart);
            _applicationDbContext.SaveChanges();
            HttpContext.Session.SetInt32(Other.SessionShoppingCart, count - 1);
            return RedirectToAction(nameof(Index));
        }
    }
}
