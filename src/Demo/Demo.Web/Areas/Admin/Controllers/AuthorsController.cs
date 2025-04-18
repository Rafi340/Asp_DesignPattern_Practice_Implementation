using Demo.Application.Services;
using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger) : Controller
    {

        private readonly IAuthorService? _authorService = authorService;
        private readonly ILogger<AuthorsController> _logger = logger;

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
                    Name = model.Name,
                    Biography = string.Empty,
                    Rating = 4.4
                });
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult GetAuthorJsonData([FromBody]AuthorListModel model)
        {
            try
            {
                var (data, total, totalDisplay) = _authorService.GetAuthors(model.PageIndex, model.PageSize, model.FormatSortExpression("Name","Biography","Rating","Id"), model.Search);
                var author = new
                {
                    recordsTotal = total,
                    recoardsFilters = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.Biography), 
                                HttpUtility.HtmlEncode(record.Rating),
                                record.Id.ToString()
                            }).ToArray()
                };
                return Json(author);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was a problem with author");
                return Json(DataTables.EmptyResult);
            }
            
        }
    }
}
