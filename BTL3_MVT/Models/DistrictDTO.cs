using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Models
{
    public class DistrictDTO
    {
        public int DistrictId { get; set; }

        [StringLength(50, ErrorMessage = "Độ dài tối đa là 50 kí tự")]
        public string DistrictName { get; set; }

        public int CityId { get; set; }
        

    }
}
