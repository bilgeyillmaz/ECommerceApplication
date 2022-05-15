using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = Other.RoleAdmin)]
    public class UsersController : Controller
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public UsersController(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public IActionResult Index()
		{
			var users = _applicationDbContext.ApplicationUsers.ToList();	
			var role =_applicationDbContext.Roles.ToList();	
			var userRole= _applicationDbContext.UserRoles.ToList();
			foreach (var u in users)
			{
				var roleId = userRole.FirstOrDefault(usr => usr.UserId == u.Id).RoleId;
				u.Role=role.FirstOrDefault(usr =>usr.Id == roleId).Name;
			}
			return View(users); //farklı tablolarda bulunan kullanıcılar ve roller geliyor.
		}
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _applicationDbContext.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _applicationDbContext.ApplicationUsers.FindAsync(id);
            _applicationDbContext.ApplicationUsers.Remove(user);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
