using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegerSortWebApp.Models
{
    public class SortPerformance
    {
        [ForeignKey("Number")]
        public int Id { get; set; }
        [Required]
        public long SortTime { get; set; }
        [Required]
        public string? SortOrder { get; set; }
        public virtual Number Number { get; set; }
    }
}
