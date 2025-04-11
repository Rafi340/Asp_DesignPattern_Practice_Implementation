using Demo.Domain;
using Demo.Domain.Services;
using System.Data;

namespace Demo.Web.Areas.Admin.Models
{
    public class AuthorListModel : DataTables
    {
        public object GetAuthor(IAuthorService authorService)
        {
          var result =  authorService.GetAuthors(PageIndex, PageSize, FormatSortExpression("Name"), Search);
          return result;
        }
    }
}
