namespace Library.Assignment1.Models
{
    public class Physician
    {
        public string? Name { get; set; }
        public string? LicenseNumber { get; set; }
        public string? GraduationDate { get; set; }
        public string? Specialization { get; set; }

        public override string ToString()
        {
            return $"{Name}, {LicenseNumber}, {GraduationDate}, {Specialization}";
        }
    }
}