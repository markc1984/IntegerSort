using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegerSortWebApp.Models
{
    public class Sort
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public long SortTime { get; set; }
        [Required]
        public int SortDirection { get; set; }

        public virtual IEnumerable<Number> Numbers { get; set; }
    }
}