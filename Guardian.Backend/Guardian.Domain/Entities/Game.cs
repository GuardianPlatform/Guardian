using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Guardian.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string License { get; set; }

        [IgnoreDataMember]
        public List<GameCategory> GameCategories { get; set; }
        public ICollection<Category> Categories { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        [IgnoreDataMember]
        public List<GameUsers> GameUsers { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
