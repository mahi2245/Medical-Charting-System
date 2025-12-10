namespace Library.Assignment1.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Birthdate { get; set; }
        public string? Race { get; set; }
        public string? Gender { get; set; }

        public List<MedicalNote> MedicalNotes { get; set; } = new List<MedicalNote>();

        public override string ToString()
        {
            return $"{Id}. {Name}, {Address}, {Birthdate}, {Race}, {Gender}";
        }

    }
}
