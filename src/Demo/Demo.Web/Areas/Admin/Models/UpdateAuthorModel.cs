using System.ComponentModel.DataAnnotations;

namespace Demo.Web.Areas.Admin.Models
{
    public class UpdateAuthorModel
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string? Biography { get; set; }
        [Required, Range(1.00, 5.00), RegularExpression("^\\d+(\\.\\d{1,2})?$", ErrorMessage = "Rating should be beetween 1.00 to 5.00")]
        public double Rating { get; set; }
    }
}
