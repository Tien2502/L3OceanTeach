namespace BTL3_MVT.Models.ViewModel
{
    public class PersonVM
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Work { get; set; }
        public string Ethnicity { get; set; }
        public string? IdentityCard { get; set; }
        public string? PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string SpecificAddress { get; set; }
        public string WorkCity { get; set; }
        public string WorkDistrict { get; set; }
        public string WorkWard { get; set; }
        public string WorkSpecificAddress { get; set; }
        public string Address => $"{SpecificAddress}, {Ward}, {District}, {City}";
        public string WorkAddress => $"{WorkSpecificAddress}, {WorkWard}, {WorkDistrict}, {WorkCity}";
    }
}
