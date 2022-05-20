using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegerSortWebApp.Models
{
    public class Number
    {
        [Key] 
        public int Id { get; set; }
        [Required] 
        public int Integer { get; set; }
        public int SortID { get; set; }
        [ForeignKey("SortID")]
        public Sort Sort { get; set; }
    }
}
