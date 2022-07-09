using System;
using System.Collections.Generic;

namespace Guardian.Domain.Entities
{
    public class GameCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
