using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using OnetoManyModelPopup_Book_user_Borrowbook.Models;

namespace OnetoManyModelPopup_Book_user_Borrowbook.Controllers
{
    public class UserController : Controller
    {
       private readonly MainDBContext _dbContext;
        public UserController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

       public async Task<IActionResult> Index()
        {
            var user = await _dbContext.Users.ToListAsync();
            return View(user);  
        }
        public async Task<IActionResult>AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new User());
            }
            else
            {
                var user = await _dbContext.Users.FindAsync(id);
                if(user == null)
                {
                    return NotFound();

                }
                return View(user);
            }
        }
        [HttpPost]
        public async Task<IActionResult>AddOrEdit(int id,User user)
        {
            if(id == 0)
            {
                _dbContext.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _dbContext.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!UserExist(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.viewtostring(this, "_VIewAll", _dbContext.Users.ToListAsync()) });
                
            }
            return Json(new { isValid = false, html = Helper.viewtostring(this, "AddOrEdit", user) });
        }

        private bool UserExist(int userId)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            return Json(new { html = Helper.viewtostring(this, "_ViewAll", _dbContext.Users.ToListAsync()) });
        }
    }
}
