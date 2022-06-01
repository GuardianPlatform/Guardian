using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Guardian.Domain.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public List<Game> Games { get; set; }
    }
}
