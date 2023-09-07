using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Models
{
    public class CityDTO
    {
        public int CityId { get; set; }

        [StringLength(50, ErrorMessage = "Độ dài tối đa là 50 kí tự")]
        public string CityName { get; set; }
    }
}
