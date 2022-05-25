using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegerSortWebApp.Models
{
    public class Number
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public int Integer { get; set; }
        public int SortID { get; set; }
        [ForeignKey("SortID")]
        [JsonIgnore]
        public virtual Sort Sort { get; set; }
    }
}
