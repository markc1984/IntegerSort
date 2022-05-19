using System.ComponentModel.DataAnnotations;

namespace IntegerSortWebApp.Models
{
    public class Number
    {
        [Key] public int Id { get; set; }
        [Required] public int Integer { get; set; }
        public virtual SortPerformance SortPerformance { get; set; }
    }
}
