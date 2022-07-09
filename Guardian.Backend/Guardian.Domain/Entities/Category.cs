using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Guardian.Domain.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [IgnoreDataMember]
        public List<GameCategory> GameCategories { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
