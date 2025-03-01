using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {

        private readonly IAuthorService? _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Add()
        {
            var model = new AddAuthorModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(AddAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                _authorService.AddAuthor(new Author
                {
                    Name = model.Name
                });
            }
            return View(model);
        }
    }
}
