using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegerSortWebApp.Models
{
    public class Sort
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long SortTime { get; set; }
        [Required]
        public int SortDirection { get; set; }

        public virtual IEnumerable<Number> Numbers { get; set; }
    }
}