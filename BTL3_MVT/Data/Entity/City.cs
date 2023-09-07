using BTL3_MVT.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BTL3_MVT.Data.Entity
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }

        //relation ship
        public List<District>? Districts { get; set; }
    }
}
