using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Data.Entity
{
    public class Work
    {
        [Key]
        public int WorkId { get; set; }
        public string WorkName { get; set; }
    }
}
