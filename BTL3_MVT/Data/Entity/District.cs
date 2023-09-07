using BTL3_MVT.Models;
using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Data.Entity
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        //relation ship
        public int CityId { get; set; }
        public City City { get; set; }

        public List<Ward>? Wards { get; set; }
    }
}
