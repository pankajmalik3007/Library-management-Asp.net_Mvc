using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnetoManyModelPopup_Book_user_Borrowbook.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnetoManyModelPopup_Book_user_Borrowbook.Controllers
{
    public class BorrowBookController : Controller
    {
        private readonly MainDBContext _dbContext;
        public BorrowBookController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
           var borrow = await _dbContext.BorrowBooks.Include(d=>d.User).Include(d=>d.Book).ToListAsync();
            return View(borrow);
        }
        [HttpGet]
        public async Task<IActionResult>AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                ViewData["BookId"] = new SelectList(_dbContext.Books, "BookId", "Title", "ISBN");
                ViewData["UserId"] = new SelectList(_dbContext.Users, "UserId", "UserName");
                return View(new BorrowBook());
            }
            else
            {
                var borrow = await _dbContext.BorrowBooks.FindAsync(id);
                if(borrow == null)
                {
                    return NotFound();
                }
                return View(borrow);
            }

        }
        [HttpPost]
        public async Task<IActionResult>AddOrEdit(int id, [Bind("BorrowBookId,BookId,UserId,BorrowDate,ReturnDate")]BorrowBook borrow)
        {
            if (id == 0)
            {
                ViewData["BookId"] = new SelectList(_dbContext.Books, "BookId", "Title", "ISBN");
                ViewData["UserId"] = new SelectList(_dbContext.Users, "UserId", "UserName");
                _dbContext.Add(borrow);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    ViewData["BookId"] = new SelectList(_dbContext.Books, "BookId", "Title", "ISBN");
                    ViewData["UserId"] = new SelectList(_dbContext.Users, "UserId", "UserName");
                    _dbContext.Update(borrow);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoorrowBookExist(borrow.BorrowBookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.viewtostring(this, "_ViewAll", _dbContext.BorrowBooks.ToListAsync()) });

            }
            return Json(new { isValid = false, html = Helper.viewtostring(this, "AddOrEdit", borrow) });
          
        }

        private bool BoorrowBookExist(int borrowBookId)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            
            var borrow = await _dbContext.BorrowBooks.Include(d=>d.User).Include(d=>d.Book).FirstOrDefaultAsync(d=>d.BorrowBookId==id);
            _dbContext.BorrowBooks.Remove(borrow);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.viewtostring(this, "ViewAll", borrow) });
        }

        public IActionResult BorrowBookDetailsByTitleName()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BorrowBookDetailsByTitleName(string bookTitle)
        {
            if (string.IsNullOrEmpty(bookTitle))
            {
                // Handle empty search query
                return RedirectToAction(nameof(Index));
            }

            // Perform a case-insensitive search for books
            var borrowedBooks = _dbContext.BorrowBooks
                .Include(bb => bb.Book)
                .Include(bb => bb.User)
                .Where(bb => bb.Book.Title.ToLower().Contains(bookTitle.ToLower()))
                .ToList();

            return View(borrowedBooks);
        }
        public IActionResult BooksBorrowedByUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BooksBorrowedByUser(int userId)
        {
            var borrowedBooks = _dbContext.BorrowBooks
                .Include(bb => bb.Book)
                .Include(bb => bb.User)
                .Where(bb => bb.UserId == userId)
                .ToList();

            return View(borrowedBooks);
        }

        public IActionResult BorrowDateByUserId()
        {
            return View();
        }

          [HttpPost]
          public IActionResult BorrowDateByUserId(int userId)
          {
              var borrowedBooks = _dbContext.BorrowBooks
                  .Where(b => b.UserId == userId)
                  .ToList();

              return View(borrowedBooks);
          }
        public IActionResult BorrowedBooksByUserName()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BorrowedBooksByUserName(string userName)
        {
            // Retrieve the user's ID based on the provided username
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                var borrowedBooks = _dbContext.BorrowBooks
                    .Where(b => b.UserId == user.UserId).Include(e => e.Book).Include(e => e.User)
                    .ToList();

                return View(borrowedBooks);
            }
            else
            {
                // Handle the case where the username is not found
                ModelState.AddModelError(string.Empty, "User not found.");
            }

            return View();
        }





    }
}
