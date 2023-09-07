using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Data.Entity
{
    public class Ward
    {
        [Key]
        public int WardId { get; set; }
        public string WardName { get; set; }

        //relation ship

        public int DistrictId { get; set; }
        public District District { get; set; }

    }
}
