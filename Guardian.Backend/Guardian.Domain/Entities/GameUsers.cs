using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Domain.Entities
{
    public class GameUsers
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
