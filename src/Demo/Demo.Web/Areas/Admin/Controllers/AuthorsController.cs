using AutoMapper;
using Demo.Application.Exceptions;
using Demo.Application.Services;
using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Infrastructure;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger,
        IMapper mapper) : Controller
    {

        private readonly IAuthorService? _authorService = authorService;
        private readonly ILogger<AuthorsController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Add()
        {
            var model = new AddAuthorModel();
            return View(model);
        }
        public IActionResult Update(Guid id)
        {
            var model = new UpdateAuthorModel();
            var author = _authorService.GetAuthor(id);
            _mapper.Map(author, model);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(UpdateAuthorModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var author = _mapper.Map<Author>(model);

                    _authorService.Update(author);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Author Updated",
                        Type = ResponseTypes.Success,
                    });
                    return RedirectToAction("Index");

                }
                catch (DuplicateAuthorNameException IO)
                {
                    ModelState.AddModelError("DuplicateAuthor", IO.Message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = IO.Message,
                        Type = ResponseTypes.Danger,
                    });
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Author added Failes",
                        Type = ResponseTypes.Danger,
                    });
                    _logger.LogError(ex, "Failed to Add Author");

                    return View(model);
                }
            }
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _authorService.DeleteAuthor(id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Author Delete Sucessfully",
                    Type = ResponseTypes.Success,
                });
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to Delete Author");
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Author Can't be deleted",
                    Type = ResponseTypes.Danger,
                });
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(AddAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
               
                    var author = _mapper.Map<Author>(model);
                    author.Id = IdentityGenerator.NewSequentialGuid();
                    _authorService.AddAuthor(author);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message= "Author added Sucessfully",
                        Type =ResponseTypes.Success,
                    });
                    return RedirectToAction("Index");
                
                }catch(DuplicateAuthorNameException IO)
                {
                    ModelState.AddModelError("DuplicateAuthor", IO.Message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = IO.Message,
                        Type = ResponseTypes.Danger,
                    });
                }
                catch(Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Author added Failes",
                        Type = ResponseTypes.Danger,
                    });
                    _logger.LogError(ex, "Failed to Add Author");

                    return View(model);
                }
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
