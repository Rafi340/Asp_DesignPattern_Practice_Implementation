using Demo.Domain;
using Demo.Domain.Services;
using System.Data;
using System.Web;

namespace Demo.Web.Areas.Admin.Models
{
    public class AuthorListModel : DataTables
    {
        public object GetAuthor(IAuthorService authorService)
        {
            try
            { 
                var result = authorService.GetAuthors(PageIndex, PageSize, FormatSortExpression("Name"), Search);
                var author = new
                {
                    recordsTotal = result.total,
                    recoardsFilters = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                record.Id.ToString()
                            }).ToArray()
                };
                return author;
            } catch 
            {
                return EmptyResult;
            }
                
        }
    }
}
