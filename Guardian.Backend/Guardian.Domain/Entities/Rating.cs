using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Guardian.Domain.Entities
{
    public class Rating : BaseEntity

    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }

    }
}
