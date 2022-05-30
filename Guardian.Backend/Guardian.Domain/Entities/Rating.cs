using System;
using System.Collections.Generic;

namespace Guardian.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }

    }
}
