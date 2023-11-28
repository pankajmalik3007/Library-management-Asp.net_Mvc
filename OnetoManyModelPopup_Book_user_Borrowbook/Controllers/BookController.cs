using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnetoManyModelPopup_Book_user_Borrowbook.Models;

namespace OnetoManyModelPopup_Book_user_Borrowbook.Controllers
{
    public class BookController : Controller
    {
       private readonly MainDBContext _dbContext;
        public BookController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var book = await _dbContext.Books.ToListAsync();
            return View(book);
        }
        public async Task<IActionResult>AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new Book());
            }
            else
            {
                var book = await _dbContext.Books.FindAsync(id);
                if(book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
        }
        [HttpPost]
        public async Task<IActionResult>AddOrEdit(int id, Book book)
        {
            if(id == 0)
            {
                _dbContext.Add(book);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _dbContext.Update(book);
                    await _dbContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!BookExist(book.BookId))
                    {
                        return NotFound();
                    }
                }
                return Json(new { isValid = true, html = Helper.viewtostring(this, "ViewAll", _dbContext.Books.ToListAsync()) });


            }
            return Json(new {isValid = false,html = Helper.viewtostring(this,"AddOrEdit",book) });
        }

        private bool BookExist(int bookId)
        {
            throw new NotImplementedException();
        }
        [HttpPost]  
        public async Task<IActionResult>Delete(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
            return Json(new { html = Helper.viewtostring(this,"_ViewAll",_dbContext.Books.ToListAsync()) });    
        }
    }
}
