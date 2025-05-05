using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Dtos
{
    public class AuthorSearchDto
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public int? RatingFrom { get; set; }
        public int? RatingTo { get; set; }
    }
}
