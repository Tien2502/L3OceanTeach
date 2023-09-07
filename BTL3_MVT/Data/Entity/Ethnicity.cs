using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Data.Entity
{
    public class Ethnicity
    {
        [Key]
        public int EthnicityId { get; set; }
        public string EthnicityName { get; set; }
    }
}
