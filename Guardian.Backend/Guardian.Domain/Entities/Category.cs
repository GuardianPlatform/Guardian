using System.Collections.Generic;

namespace Guardian.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<Game> Games { get; set; }
    }
}
