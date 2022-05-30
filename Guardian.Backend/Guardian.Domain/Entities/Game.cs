using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guardian.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string License { get; set; }
        public List<Category> Categories { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<User> Users { get; set; }

        
       
    }
}
