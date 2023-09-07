using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Models
{
    public class WardDTO
    {
        public int WardId { get; set; }

        [StringLength(50, ErrorMessage = "Độ dài tối đa là 50 kí tự")]
        public string WardName { get; set; }

        public int DistrictId { get; set; }
        
    }
}
