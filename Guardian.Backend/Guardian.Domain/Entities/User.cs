
using System.Collections.Generic;

namespace Guardian.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public List<Game> Games { get; set; }

    }
}
