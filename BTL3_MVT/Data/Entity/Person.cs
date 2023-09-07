using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTL3_MVT.Data.Entity
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDay { get; set; }

        public string Gender { get; set; }
        
        public string? IdentityCard { get; set; }

        public string? PhoneNumber { get; set; }

        [StringLength(20)]
        public string SpecificAddress { get; set; }

        [StringLength(20)]
        public string WorkSpecificAddress { get; set; }

        //Relation ship
        public int CityId { get; set; }
        public City City { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int WardId { get; set; }
        public Ward Ward { get; set; }

        public int WorkCityId { get; set; }
        public City WorkCity { get; set; }

        public int WorkDistrictId { get; set; }
        public District WorkDistrict { get; set; }

        public int WorkWardId { get; set; }
        public Ward WorkWard { get; set; }

        public int EthnicityId { get; set; }
        public Ethnicity Ethnicity { get; set; }
        public int WorkId { get; set; }
        public Work Work { get; set; }

    }
}
