
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Guardian.Domain.Entities
{
    public class User : ApplicationUser
    {
        public string Email { get; set; }
        public string Login { get; set; }

        [IgnoreDataMember]
        public List<GameUsers> GameUsers { get; set; }
        public ICollection<Game> Games { get; set; }
        public ICollection<Rating> Ratings { get; set; }

    }
}
