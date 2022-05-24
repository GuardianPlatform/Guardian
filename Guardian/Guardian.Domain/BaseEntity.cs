using System.ComponentModel.DataAnnotations;

namespace Guardian.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
