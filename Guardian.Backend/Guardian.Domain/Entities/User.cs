
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Guardian.Domain.Entities
{
    public class User : ApplicationUser
    {
        
        public string Email { get; set; }
        public string Login { get; set; }

        public List<Game> Games { get; set; }
        public List<Rating> Ratings { get; set; }

    }
}
